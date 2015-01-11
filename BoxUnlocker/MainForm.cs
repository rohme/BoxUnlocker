using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FFACETools;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections;

namespace BoxUnlocker    
{
    public partial class MainForm : Form
    {
        private Constant _Constant = new Constant();
        private Settings _Settings = new Settings();
        private FFACE _FFACE { get; set; }

        private Boolean Running = false;
        private static ThreadStart ts;
        private static Thread workerThread;

        public MainForm()
        {
            InitializeComponent();
        }

        #region イベント関連
        /// <summary>
        /// フォームLoadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //タイトル初期化
            System.Reflection.AssemblyTitleAttribute asmttl = (System.Reflection.AssemblyTitleAttribute)
                                                                Attribute.GetCustomAttribute(
                                                                System.Reflection.Assembly.GetExecutingAssembly(), 
                                                                typeof(System.Reflection.AssemblyTitleAttribute));
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            System.Version ver = asm.GetName().Version;
            this.Text = asmttl.Title + " " + ver.Major + "." + ver.Minor + "." + ver.Build;

            //フォーム位置初期化
            this.Left = _Settings.FormPosX;
            this.Top = _Settings.FormPosY;
            
            //実行対象初期化
            chkExecField.Checked = _Settings.ExecField;
            chkExecMum.Checked = _Settings.ExecMum;
            chkExecAbyssea.Checked = _Settings.ExecAbyssea;
            
            //MUMゲーム初期化
            DataTable MumGameTable = new DataTable();
            MumGameTable.Columns.Add("ID", typeof(BoxTypeKind));
            MumGameTable.Columns.Add("NAME", typeof(string)); 
            for (int i = 0; i < _Constant.GetMumGameType().GetLength(0); i++)
            {
                DataRow row = MumGameTable.NewRow();
                row["ID"] = _Constant.MUMGAMEINFO[i].Id;
                row["NAME"] = _Constant.MUMGAMEINFO[i].Name;
                MumGameTable.Rows.Add(row);
            }
            MumGameTable.AcceptChanges();
            cmbMumGameId.Items.Clear();
            cmbMumGameId.DataSource = MumGameTable;
            cmbMumGameId.DisplayMember = "NAME";
            cmbMumGameId.ValueMember = "ID";
            cmbMumGameId.SelectedValue = _Settings.MumGameId;

            //MUM挑戦回数初期化
            txtMumTryCount.Text = _Settings.MumTryCount.ToString();

            //候補初期化
            txtTargets.Text = "";

            //enterminity初期化
            chkUseEnternity.Checked = _Settings.UseEnternity;

            //実行ボタン初期化
            btnExec.Text = "開　　始";

            //メッセージ初期化
            lblMessage.Text = "";

            //POLを検索する
            Process[] pol = Process.GetProcessesByName("pol");
            if (1 > pol.Length)
            {
                MessageBox.Show("FFXIを起動してください。");
                System.Environment.Exit(0);
            }

            DataTable PolTable = new DataTable();
            PolTable.Columns.Add("ID", typeof(int));
            PolTable.Columns.Add("NAME", typeof(string));
            foreach (Process v in pol)
            {
                DataRow row = PolTable.NewRow();
                row["ID"] = v.Id;
                row["NAME"] = v.MainWindowTitle;
                PolTable.Rows.Add(row);
            }
            PolTable.AcceptChanges();
            cmbPol.DataSource = PolTable;
            cmbPol.DisplayMember = "NAME";
            cmbPol.ValueMember = "ID";
            cmbPol.SelectedIndex = 0;
            AttachPol(pol[0].Id);

            //監視を自動的に開始する
            Exec(true);
        }
        /// <summary>
        /// フォーム閉じたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            if (workerThread != null && workerThread.IsAlive)
            {
                workerThread.Abort();
            }
        }
        /// <summary>
        /// polコンボボックス変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPol_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(cmbPol.SelectedValue.ToString());
            int i;
            if ( int.TryParse( cmbPol.SelectedValue.ToString() ,out i) )
            {
                //Console.WriteLine("pol=" + cmbPol.SelectedValue.ToString());
                int pol = int.Parse(cmbPol.SelectedValue.ToString());
                AttachPol(pol);
            }
        }
        /// <summary>
        /// MUM実行回数テキスト変更イベント
        /// 数字以外入力不可にしている
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMumTryCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 実行ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExec_Click(object sender, EventArgs e)
        {
            if (Running)
            {
                Exec(false);
            }
            else
            {
                Exec(true);
            }
        }
        /// <summary>
        /// フォームのサイズ変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_ClientSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                // フォームが最小化の状態であればフォームを非表示にする  
                this.Hide();
                // トレイリストのアイコンを表示する  
                notifyIcon1.Visible = true;
            }
        }
        /// <summary>
        /// タスクトレイアイコンのダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            // フォームを表示する  
            this.Visible = true;
            // 現在の状態が最小化の状態であれば通常の状態に戻す  
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                // トレイリストのアイコンを非表示にする  
                notifyIcon1.Visible = false;
            }
            // フォームをアクティブにする  
            this.Activate();
        }
        #endregion

        /// <summary>
        /// フォームのコントロールをロックする
        /// </summary>
        /// <param name="isLock">True:ロック False:解除</param>
        /// <param name="iWithExecButton">True:実行ボタンもロック Fale:実行ボタン以外をロック</param>
        private void lockControl(Boolean isLock,Boolean iWithExecButton)
        {
            Boolean enable;
            if (isLock){
                enable = false;
                if (iWithExecButton)
                {
                    SetFormBackcolor(Color.DarkGray);
                }
                else
                {
                    SetFormBackcolor(Color.LimeGreen);
                }
            }
            else
            {
                enable = true;
                SetFormBackcolor(SystemColors.Control);
            }
            SetCheckEnable(chkExecField, enable);
            SetCheckEnable(chkExecMum, enable);
            SetCheckEnable(chkExecAbyssea, enable);
            SetComboEnable(cmbMumGameId, enable);
            SetCheckEnable(chkUseEnternity, enable);
            SetTextboxEnabled(txtMumTryCount, enable);
            SetComboEnable(cmbPol, enable);
            if (iWithExecButton)
            {
                SetButtonEnable(btnExec, enable);
            }
        }
        /// <summary>
        /// 設定内容をINIに保存する
        /// </summary>
        private void SaveSettings()
        {
            if(this.Left >= 0) _Settings.FormPosX = this.Left;
            if (this.Top >= 0) _Settings.FormPosY = this.Top;
            _Settings.ExecField = chkExecField.Checked;
            _Settings.ExecMum = chkExecMum.Checked;
            _Settings.ExecAbyssea = chkExecAbyssea.Checked;
            _Settings.MumGameId = (BoxTypeKind)cmbMumGameId.SelectedValue;
            _Settings.MumTryCount = int.Parse(txtMumTryCount.Text);
            _Settings.UseEnternity = chkUseEnternity.Checked;
            _Settings.Save();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        private void Exec(Boolean start)
        {
            if (start)
            {
                SetMessage("待機処理を開始しました");
                SetButtonText(btnExec, "停　　止");
                SetTextboxText(txtTargets, "");
                //INIに設定保存
                SaveSettings();
                //処理開始
                Running = true;
                //コントロールのロック処理
                lockControl(true,false);
                //監視開始
                ts = new ThreadStart(AWait);
                workerThread = new Thread(ts);
                workerThread.IsBackground = true;
                workerThread.Start();
            }
            else
            {
                SetButtonText(btnExec, "開　　始");
                SetTextboxText(txtTargets, "");
                Running = false;
                //コントロールのアンロック処理
                lockControl(false, false);
                if (workerThread != null && workerThread.IsAlive)
                {
                    workerThread.Abort();
                }
            }
        }
        /// <summary>
        /// 箱空け待機処理
        /// </summary>
        private void AWait()
        {
            while( Running )
            {
                SetMessage("箱やNPCが選択されるまで待機中");
                if (_FFACE.Menu.IsOpen)
                {
                    //フィールドチェック
                    if (_Settings.ExecField)
                    {
                        if (_FFACE.Target.Name == "Treasure Casket" &&
                            MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum1) || // ^どうする？$ 
                            MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum3) ) //^どうする？（残り([0-9]*)回）$
                        {
                            SetMessage("箱空け実行中");
                            Boolean r = StartField();
                            if (!r)
                            {
                                break;
                            }
                        }
                    }
                    //Mumチェック
                    if(_Settings.ExecMum)
                    {
                        
                        if(_FFACE.Player.Zone == Zone.Western_Adoulin &&
                            _FFACE.Target.Name == "Kerney" &&
                            _FFACE.Menu.IsOpen &&
                            (MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum1) || // ^どうする？$
                             MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum3) || //^どうする？（残り([0-9]*)回）$
                             MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum4)))  //^もう1度やりますか？(.*)$

                        {
                            Constant.MumStartRet r = StartMum();
                            if (r == Constant.MumStartRet.異常終了)
                            {
                                break;
                            }
                        }
                    }
                }
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            Exec(false);
        }
        /// <summary>
        /// フィールド箱空け処理
        /// </summary>
        /// <returns>True:正常終了 False:異常終了</returns>
        private Boolean StartField()
        {
            FFACE.ChatTools.ChatLine cl = new FFACE.ChatTools.ChatLine();
            Constant.MumGameInfo gi = _Constant.GetMumGameInfo(_Settings.MumGameId);
            Box box = new Box(BoxTypeKind.Field);
            int boxOpenRemain = _Settings.MumTryCount;
            int targetId = _FFACE.Target.ID;
            SetMessage("FOV･GOV箱空け実行中");
            while (Running)
            {
                //どうする？
                if (_FFACE.Menu.IsOpen && MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringField1))
                {
                    //開錠するを選択
                    if (!SetDialogOptionIndex(1, true)) return false;
                    if (!WaitOpenDialog(Constant.DialogStringField2, false)) return false;
                }
                //どうする？（残り([0-9]*)回）
                if (_FFACE.Menu.IsOpen && MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringField2))
                {
                    //残り回数取得
                    if (MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringField2))
                    {
                        ArrayList al = MiscTools.GetRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringField2);
                        box.TryRemainCnt = int.Parse(al[0].ToString());
                    }
                    //string[] r = getMatchDialogQuestionValue(Constant.DialogStringMum3);
                    //box.TryRemainCnt = int.Parse(r[1]);
                    //チャットバッファクリア
                    cl = _FFACE.Chat.GetNextLine(LineSettings.CleanAll);
                    //次の操作取得
                    BoxOperation bo = box.GetNextOperation();
                    if (bo.Operation == BoxOperationKind.INPUTNO)
                    {
                        SetMessage("箱操作：" + bo.InputNo.ToString() + "を入力");
                        if (!SetDialogOptionIndex(1, true)) return false;//数字を入力するを選択
                        if (!_Settings.UseEnternity)
                        {
                            System.Threading.Thread.Sleep(_Settings.BaseWait);
                            _FFACE.Windower.SendKeyPress(KeyCode.EnterKey);
                        }
                        if (!InputNumber(bo.InputNo.ToString(), true)) return false;//数字を入力する
                    }
                    else if (bo.Operation == BoxOperationKind.GETHINTS)
                    {
                        SetMessage("箱操作：カギを調べる");
                        if (!SetDialogOptionIndex(2, true)) return false;//カギを調べるを選択
                    }
                    System.Threading.Thread.Sleep(_Settings.NumberInputWait);
                    setChatToBox(box);
                    SetTextboxText(txtTargets, box.EnableNoLine);

                    //箱空け完了かの判断
                    bo = box.GetNextOperation();
                    if (bo.Operation == BoxOperationKind.OPENSUCCESS){
                        SetMessage("箱開けに成功しました");
                        return true;
                    }
                    else if(bo.Operation == BoxOperationKind.OPENFAILED)
                    {
                        SetMessage("箱開けに失敗しました");
                        return true;
                    }
                    else
                    {
                        //箱をターゲットする
                        if (!SetTargetFromId(targetId)) return false;
                        _FFACE.Windower.SendKeyPress(KeyCode.EnterKey);//ENTER
                        if (!WaitOpenDialog(Constant.DialogStringField1, true)) return false;
                    }
                }
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            return true;
        }
        /// <summary>
        /// キーナンバー９９　箱空け処理
        /// </summary>
        /// <returns></returns>
        private Constant.MumStartRet StartMum()
        {
            FFACE.ChatTools.ChatLine cl = new FFACE.ChatTools.ChatLine();
            Constant.MumGameInfo gi = _Constant.GetMumGameInfo(_Settings.MumGameId);
            Box box = new Box(gi.Id);
            int boxOpenRemain = _Settings.MumTryCount;
            SetMessage("MUM箱空け実行中");
            while (Running)
            {
                //どうする？
                if (_FFACE.Menu.IsOpen && MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum1) )
                {
                    box = new Box(gi.Id);
                    SetTextboxText(txtTargets, box.EnableNoLine);
                    switch (gi.Type)
                    {
                        case Constant.MumGameType.BAYLD:
                            if (!SetDialogOptionIndex(1,true)) return Constant.MumStartRet.異常終了;
                            break;
                        case Constant.MumGameType.GIL:
                            if (!SetDialogOptionIndex(2, true)) return Constant.MumStartRet.異常終了;
                            break;
                    }
                    if (!WaitOpenDialog(Constant.DialogStringMum2, false)) return Constant.MumStartRet.異常終了;
                }
                //難易度を選択してください((.*):([0-9]*))
                if (_FFACE.Menu.IsOpen && MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum2))
                {
                    if (!SetDialogOptionIndex(gi.MenuIndex, true)) return Constant.MumStartRet.異常終了;
                    if (!WaitOpenDialog(Constant.DialogStringMum3, false)) return Constant.MumStartRet.異常終了;
                    box = new Box(gi.Id);
                }
                //どうする？（残り([0-9]*)回）
                if (_FFACE.Menu.IsOpen && MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum3))
                {
                    //残り回数取得
                    if (MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum3))
                    {
                        ArrayList al = MiscTools.GetRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum3);
                        box.TryRemainCnt = int.Parse(al[0].ToString());
                    }
                    //string[] r = getMatchDialogQuestionValue(Constant.DialogStringMum3);
                    //box.TryRemainCnt = int.Parse(r[1]);
                    //チャットバッファクリア
                    cl = _FFACE.Chat.GetNextLine(LineSettings.CleanAll);
                    //次の操作取得
                    BoxOperation bo = box.GetNextOperation();
                    if(bo.Operation == BoxOperationKind.INPUTNO)
                    {
                        SetMessage("箱操作："+bo.InputNo.ToString()+"を入力");
                        if (!SetDialogOptionIndex(0, true)) return Constant.MumStartRet.異常終了;//数字を入力するを選択
                        if (!_Settings.UseEnternity)
                        {
                            System.Threading.Thread.Sleep(_Settings.BaseWait);
                            _FFACE.Windower.SendKeyPress(KeyCode.EnterKey);
                        }
                        if (!InputNumber(bo.InputNo.ToString(), true)) return Constant.MumStartRet.異常終了;//数字を入力する
                    }
                    else if (bo.Operation == BoxOperationKind.GETHINTS)
                    {
                        SetMessage("箱操作：カギを調べる");
                        if (!SetDialogOptionIndex(1, true)) return Constant.MumStartRet.異常終了;//カギを調べるを選択
                    }
                    //else if (bo.Operation == BoxOperationKind.FINISHED)
                    //{
                    //    return Constant.MumStartRet.正常終了;
                    //}
                    System.Threading.Thread.Sleep(_Settings.NumberInputWait);
                    setChatToBox(box);
                    SetTextboxText(txtTargets, box.EnableNoLine);

                    //箱空け完了かの判断
                    bo = box.GetNextOperation();
                    if (bo.Operation == BoxOperationKind.OPENSUCCESS ||
                        bo.Operation == BoxOperationKind.OPENFAILED)
                    {
                        //残り回数の判定
                        boxOpenRemain--;
                        if (_Settings.MumTryCount != 0)
                        {
                            if (boxOpenRemain > 0)
                            {
                                if (bo.Operation == BoxOperationKind.OPENSUCCESS)
                                {
                                    SetMessage("箱空け成功 残り回数：" + boxOpenRemain);
                                }
                                else
                                {
                                    SetMessage("箱空け失敗 残り回数：" + boxOpenRemain);
                                }
                            }
                        }
                    }
                }
                //もう1度やりますか？
                if (_FFACE.Menu.IsOpen && MiscTools.IsRegexString(_FFACE.Menu.DialogText.Question, Constant.DialogStringMum4))
                {
                    //規定回数開けてたら終了
                    if (_Settings.MumTryCount != 0 &&
                        boxOpenRemain <= 0)
                    {
                        SetMessage("規定回数に達したので停止しました");
                        if (!SetDialogOptionIndex(0, true)) return Constant.MumStartRet.異常終了;
                        return Constant.MumStartRet.指定回数実行;
                    }
                    box = new Box(gi.Id);
                    SetTextboxText(txtTargets, box.EnableNoLine);
                    switch (gi.Type)
                    {
                        case Constant.MumGameType.BAYLD:
                            if (!SetDialogOptionIndex(1,true)) return Constant.MumStartRet.異常終了;
                            break;
                        case Constant.MumGameType.GIL:
                            if (!SetDialogOptionIndex(2, true)) return Constant.MumStartRet.異常終了;
                            break;
                    }
                    if (!WaitOpenDialog(Constant.DialogStringMum3, false)) return Constant.MumStartRet.異常終了;
                }
                
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            return Constant.MumStartRet.正常終了;
        }
        /// <summary>
        /// 取得したヒントをＢｏｘクラスへセットする
        /// </summary>
        /// <param name="iBox"></param>
        private void setChatToBox(Box iBox)
        {
            FFACE.ChatTools.ChatLine cl = new FFACE.ChatTools.ChatLine();
            ArrayList al = new ArrayList();
            cl = _FFACE.Chat.GetNextLine(LineSettings.CleanAll);
            while (cl != null)
            {
                al.Add(cl.Text);
                cl = _FFACE.Chat.GetNextLine(LineSettings.CleanAll);
            }
            iBox.SetHints(al);
        }
        /// <summary>
        /// 指定されたダイアログIDのダイアログが表示されるまで待つ
        /// </summary>
        /// <param name="iWaitString">ダイアログ文字列</param>
        /// <param name="iEnter">True:待ってる間Enter連打する False:Enter連打しない</param>
        /// <returns>True:ダイアログが表示された False:ダイアログが表示されなかった</returns>
        private Boolean WaitOpenDialog(string iWaitString, Boolean iEnter)
        {
            for (int i = 0; (i < Constant.MaxLoopCnt) && Running; i++)
            {
                Regex reg = new Regex(iWaitString, RegexOptions.IgnoreCase);
                Match ma = reg.Match(_FFACE.Menu.DialogText.Question);
                if (_FFACE.Menu.IsOpen && ma.Success)
                {
                    return true;
                }
                if (!_Settings.UseEnternity && iEnter)
                {
                    _FFACE.Windower.SendKeyPress(KeyCode.EnterKey);//ENTER
                }
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            return false;
        }

        /// <summary>
        /// ダイアログへ文字を入力する
        /// </summary>
        /// <param name="iNum">入力する数字</param>
        /// <param name="iWithEnter">数字入力後にエンターを入力する</param>
        /// <returns></returns>
        private Boolean InputNumber(string iNum,Boolean iWithEnter)
        {
            if (iNum.Length == 0) return false;
            System.Threading.Thread.Sleep(_Settings.BaseWait);
            for (int i = 0; i < iNum.Length; i++)
            {
                string targetNum = iNum.Substring(i, 1);
                switch (targetNum)
                {
                    case "0":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number0);
                        break;
                    case "1":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number1);
                        break;
                    case "2":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number2);
                        break;
                    case "3":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number3);
                        break;
                    case "4":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number4);
                        break;
                    case "5":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number5);
                        break;
                    case "6":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number6);
                        break;
                    case "7":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number7);
                        break;
                    case "8":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number8);
                        break;
                    case "9":
                        _FFACE.Windower.SendKeyPress(KeyCode.Number9);
                        break;
                }
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            if (iWithEnter)
            {
                _FFACE.Windower.SendKeyPress(KeyCode.EnterKey);
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            return true;
        }

        /// <summary>
        /// 指定したダイアログインデックスへカーソルを移動させる
        /// </summary>
        /// <param name="iIdx"></param>
        /// <param name="iWithEnter"></param>
        /// <returns></returns>
        private Boolean SetDialogOptionIndex(short iIdx, Boolean iWithEnter)
        {
            for (int i = 0; (i < Constant.MaxLoopCnt) && Running; i++)
            {
                if (_FFACE.Menu.DialogOptionIndex == iIdx)
                {
                    if (iWithEnter)
                    {
                        _FFACE.Windower.SendKeyPress(KeyCode.EnterKey);///Enter
                    }
                    return true;
                }
                else if (_FFACE.Menu.DialogOptionIndex > iIdx)
                {
                    if ((_FFACE.Menu.DialogOptionIndex - iIdx) >= 3)
                    {
                        _FFACE.Windower.SendKeyPress(KeyCode.LeftArrow);//右矢印
                    }
                    else
                    {
                        _FFACE.Windower.SendKeyPress(KeyCode.UpArrow);//上矢印
                    }
                }
                else if (_FFACE.Menu.DialogOptionIndex < iIdx)
                {
                    if ((iIdx - _FFACE.Menu.DialogOptionIndex) >= 3)
                    {
                        _FFACE.Windower.SendKeyPress(KeyCode.RightArrow);//左矢印
                    }
                    else
                    {
                        _FFACE.Windower.SendKeyPress(KeyCode.DownArrow);//下矢印
                    }
                }
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            SetMessage("DislogId:" + _FFACE.Menu.DialogID + " OptIndex: iIdx" + iIdx + "が選択できません");
            return false;
        }
        /// <summary>
        /// 指定したNPCをターゲットする
        /// </summary>
        /// <param name="iId">NpcID</param>
        /// <returns>True:ターゲット完了 False:ターゲット出来なかった</returns>
        private Boolean SetTargetFromId(int iId)
        {
            for (int i = 0; (i < Constant.MaxLoopCnt) && Running; i++)
            {
                if (_FFACE.Target.ID == iId)
                {
                    return true;
                }
                _FFACE.Windower.SendKeyPress(KeyCode.TabKey);//TAB
                System.Threading.Thread.Sleep(_Settings.BaseWait);
            }
            SetMessage("NpcId:" + iId.ToString() + "が見つかりませんでした");
            return false;
        }
        /// <summary>
        /// polにアタッチする
        /// </summary>
        /// <param name="id">polプロセスID</param>
        private void AttachPol(int id)
        {
            if (id > 0)
            {
                lockControl(true, true);
                _FFACE = new FFACE(id);

                Process pol = Process.GetProcessById(id);
                SetMessage(pol.MainWindowTitle + "(" + id + ")に接続しました");
                lockControl(false, true);
            }
        }

        #region Invoke群
        private void SetMessage(string iStr)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { this.lblMessage.Text = iStr; });
                return;
            }
            this.lblMessage.Text = iStr;
        }
        private void SetRefresh()
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { this.Refresh(); });
                return;
            }
            this.Refresh();
        }
        private void SetFormBackcolor(Color iColor)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { this.BackColor = iColor; });
                return;
            }
            this.BackColor = iColor;
        }

        private void SetButtonText(Button iButton, string iStr)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { iButton.Text = iStr; });
                return;
            }
            iButton.Text = iStr;
        }
        private void SetButtonEnable(Button iButton, Boolean iEnabled)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { iButton.Enabled = iEnabled; });
                return;
            }
            iButton.Enabled = iEnabled;
        }

        private void SetCheckEnable(CheckBox iCheckBox, Boolean iEnabled)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { iCheckBox.Enabled = iEnabled; });
                return;
            }
            iCheckBox.Enabled = iEnabled;
        }
        private void SetComboEnable(ComboBox iComboBox, Boolean iEnabled)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { iComboBox.Enabled = iEnabled; });
                return;
            }
            iComboBox.Enabled = iEnabled;
        }
        private void SetTextboxText(TextBox iTextBox, string iStr)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { iTextBox.Text = iStr; });
                return;
            }
            iTextBox.Text = iStr;
        }
        private void SetTextboxEnabled(TextBox iTextBox, Boolean iEnabled)
        {
            if (InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate() { iTextBox.Enabled = iEnabled; });
                return;
            }
            iTextBox.Enabled = iEnabled;
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Livet;
using log4net;
using EliteAPIWrapper;

namespace BoxUnlocker.Models
{
    public class Unlocker : NotificationObject
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="iSettings"></param>
        public Unlocker(Settings iSettings, Pol iPol)
        {
            this.Box = new Box();
            settings = iSettings;
            pol = iPol;
            
            IsMonitoring = false;
            IsCancelling = false;
            TargetBoxName = string.Empty;
            StatusText = string.Empty;
            this.MumRemainCount = 0;
        }

        #region メンバー
        private Settings settings;
        private Pol pol;
        private BoxType targetBoxType = BoxType.None;

        // 入力数値とKeyCodeの対応
        private Dictionary<string, KeyCode> KeyCodeMap = new Dictionary<string, KeyCode>
        {
            { "0", KeyCode.Number0 },
            { "1", KeyCode.Number1 },
            { "2", KeyCode.Number2 },
            { "3", KeyCode.Number3 },
            { "4", KeyCode.Number4 },
            { "5", KeyCode.Number5 },
            { "6", KeyCode.Number6 },
            { "7", KeyCode.Number7 },
            { "8", KeyCode.Number8 },
            { "9", KeyCode.Number9 },
        };
        // 数字を入力する場合のメニューインデックス
        private Dictionary<BoxType, int> MenuIndexInputNumber = new Dictionary<BoxType, int>{
            { BoxType.Field, 1 },
            { BoxType.Mum,   0 },
        };
        // カギを調べる場合のメニューインデックス
        private Dictionary<BoxType, int> MenuIndexGetHints = new Dictionary<BoxType, int>{
            { BoxType.Field, 2 },
            { BoxType.Mum,   1 },
        };

        #region Box変更通知プロパティ
        private Box _Box;
        public Box Box
        {
            get
            { return _Box; }
            set
            { 
                if (_Box == value)
                    return;
                _Box = value;
                RaisePropertyChanged("Box");
            }
        }
        #endregion
        #region IsMonitoring変更通知プロパティ
        private bool _IsMonitoring;
        public bool IsMonitoring
        {
            get
            { return _IsMonitoring; }
            private set
            { 
                if (_IsMonitoring == value)
                    return;
                _IsMonitoring = value;
                RaisePropertyChanged("IsMonitoring");
            }
        }
        #endregion
        #region IsCancelling変更通知プロパティ
        private bool _IsCancelling;
        public bool IsCancelling
        {
            get
            { return _IsCancelling; }
            set
            { 
                if (_IsCancelling == value)
                    return;
                _IsCancelling = value;
                RaisePropertyChanged("IsCancelling");
            }
        }
        #endregion
        #region TargetBoxName変更通知プロパティ
        private string _TargetBoxName;
        public string TargetBoxName
        {
            get { return _TargetBoxName; }
            private set
            {
                if (_TargetBoxName == value) return;
                _TargetBoxName = value;
                RaisePropertyChanged("TargetBoxName");
            }
        }
        #endregion
        #region StatusText変更通知プロパティ
        private string _StatusText;
        public string StatusText
        {
            get
            { return _StatusText; }
            set
            { 
                if (_StatusText == value)
                    return;
                _StatusText = value;
                RaisePropertyChanged("StatusText");
            }
        }
        #endregion
        #region MumRemainCount変更通知プロパティ
        private int _MumRemainCount;
        public int MumRemainCount
        {
            get
            { return _MumRemainCount; }
            set
            { 
                if (_MumRemainCount == value)
                    return;
                _MumRemainCount = value;
                RaisePropertyChanged("MumRemainCount");
            }
        }
        #endregion
        #endregion

        #region メソッド
        /// <summary>
        /// 監視の開始
        /// </summary>
        public void StartMonitoring()
        {
            if (IsMonitoring || IsCancelling) return;
            SetStatusText("監視を開始しました");
            new Thread(new ThreadStart(this.Monitoring))
            {
                IsBackground = true
            }.Start();
        }
        /// <summary>
        /// 監視の停止
        /// </summary>
        public void StopMonitoring()
        {
            SetStatusText("監視を停止しました");
            IsCancelling = true;
        }
        /// <summary>
        /// 監視メイン処理
        /// </summary>
        /// <returns></returns>
        private void Monitoring()
        {
            try
            {
                logger.Debug("監視の開始");
                this.IsMonitoring = true;
                while (!this.IsCancelling)
                {
                    if (pol.Api.Menu.IsMenuOpen)
                    {
                        if (settings.MonitoringField &&
                            pol.Api.Target.TargetName == Constants.BoxNameTreasureCasket &&
                            EliteAPIWrapper.Utils.RegexUtil.IsMatch(pol.Api.Dialog.Question, Constants.BoxQuestionWhatWillYouDo))
                        {
                            // フィールド箱を開ける
                            OpenFieldBox();
                        }
                        else if (settings.MonitoringMum &&
                                 pol.Api.Target.TargetName == Constants.MumNpcName &&
                                 pol.Api.Player.Zone == Zone.WesternAdoulin)
                        {
                            // MUM箱を開ける
                            OpenMumBox();
                        }
                    }
                    Thread.Sleep(settings.BaseWait);
                }
            }
            catch(Exception e)
            {
                logger.Error(e.Message, e);
                return;
            }
            finally
            {
                logger.Debug("監視の停止");
                this.IsMonitoring = false;
                this.IsCancelling = false;
            }
        }
        private bool OpenMumBox()
        {
            try
            {
                targetBoxType = BoxType.Mum;
                this.TargetBoxName = TypeMaps.BoxTypeMap[targetBoxType];
                SetStatusText("MUM箱を開けます");
                this.Box.ResetBox();
                SetViewData();
                Operation operation = new Operation();
                this.MumRemainCount = settings.MumMaxCount;

                while (!this.IsCancelling)
                {
                    if (pol.Api.Menu.IsMenuOpen)
                    {
                        // どうする？
                        if (EliteAPIWrapper.Utils.RegexUtil.IsMatch(pol.Api.Dialog.Question, Constants.MumQuestionWhatWillYouDo))
                        {
                            if(settings.MumBoxType == MumType.Bayld20 ||
                               settings.MumBoxType == MumType.Bayld40 ||
                               settings.MumBoxType == MumType.Bayld60 ||
                               settings.MumBoxType == MumType.Bayld80 ||
                               settings.MumBoxType == MumType.Bayld100 )
                            {
                                pol.Api.Dialog.SetDialogIndex(1, true, settings.BaseWait); // 同盟戦績で挑戦する
 
                            }
                            else if (settings.MumBoxType == MumType.Gil100 ||
                                     settings.MumBoxType == MumType.Gil200 ||
                                     settings.MumBoxType == MumType.Gil300 ||
                                     settings.MumBoxType == MumType.Gil400 ||
                                     settings.MumBoxType == MumType.Gil500)
                            {
                                pol.Api.Dialog.SetDialogIndex(2, true, settings.BaseWait); // ギルで挑戦する

                            }
                        }
                        // 難易度を選択してください((.*):([0-9]*))
                        if (EliteAPIWrapper.Utils.RegexUtil.IsMatch(pol.Api.Dialog.Question, Constants.MumQuestionSelectLevel))
                        {
                            if(settings.MumBoxType == MumType.Gil100 || settings.MumBoxType == MumType.Bayld20)
                                pol.Api.Dialog.SetDialogIndex(1, true, settings.BaseWait); // ピジョン
                            else if (settings.MumBoxType == MumType.Gil200 || settings.MumBoxType == MumType.Bayld40)
                                pol.Api.Dialog.SetDialogIndex(2, true, settings.BaseWait); // ハニムーナー
                            else if (settings.MumBoxType == MumType.Gil300 || settings.MumBoxType == MumType.Bayld60)
                                pol.Api.Dialog.SetDialogIndex(3, true, settings.BaseWait); // シル
                            else if (settings.MumBoxType == MumType.Gil400 || settings.MumBoxType == MumType.Bayld80)
                                pol.Api.Dialog.SetDialogIndex(4, true, settings.BaseWait); // プリーミアム
                            else if (settings.MumBoxType == MumType.Gil500 || settings.MumBoxType == MumType.Bayld100)
                                pol.Api.Dialog.SetDialogIndex(5, true, settings.BaseWait); // シャーク
                        }
                        // ^どうする？（残り([0-9]*)回）$
                        else if (EliteAPIWrapper.Utils.RegexUtil.IsMatch(pol.Api.Dialog.Question, Constants.MumQuestionWhatWillYouDoWithRemain))
                        {
                            // 箱の初期化
                            if (operation.OperationType != OperationType.GetHints && operation.OperationType != OperationType.InputNumber)
                            {
                                operation = this.Box.StartUnlockMumBox(settings.MumBoxType);
                                SetViewData();
                            }
                            // 箱の操作
                            if (!DoOperation(BoxType.Mum, ref operation)) return false;
                            if (operation.OperationType == OperationType.Success) SetStatusText("箱空け成功");
                            else if (operation.OperationType == OperationType.Failed) SetStatusText("箱空け失敗");
                        }
                        // ^もう1度やりますか？(.*)$
                        else if (EliteAPIWrapper.Utils.RegexUtil.IsMatch(pol.Api.Dialog.Question, Constants.MumQuestionTryAgain))
                        {
                            // 挑戦回数の判定
                            this.MumRemainCount--;
                            if(this.MumRemainCount <= 0)
                            {
                                pol.Api.Dialog.SetDialogIndex(0, true, settings.BaseWait); // あきらめる
                                SetStatusText("規定回数に達したので、MUM箱空けを終了しました。");
                                break;
                            }
                            if (settings.MumBoxType == MumType.Bayld20 ||
                               settings.MumBoxType == MumType.Bayld40 ||
                               settings.MumBoxType == MumType.Bayld60 ||
                               settings.MumBoxType == MumType.Bayld80 ||
                               settings.MumBoxType == MumType.Bayld100)
                            {
                                pol.Api.Dialog.SetDialogIndex(1, true, settings.BaseWait); // 部屋縷度で再挑戦する
                            }
                            else if (settings.MumBoxType == MumType.Gil100 ||
                                     settings.MumBoxType == MumType.Gil200 ||
                                     settings.MumBoxType == MumType.Gil300 ||
                                     settings.MumBoxType == MumType.Gil400 ||
                                     settings.MumBoxType == MumType.Gil500)
                            {
                                pol.Api.Dialog.SetDialogIndex(2, true, settings.BaseWait); // ギルで再挑戦する

                            }
                        }
                    }
                    Thread.Sleep(settings.BaseWait);
                }
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                return false;
            }

        }
        private bool OpenFieldBox()
        {
            try
            {
                targetBoxType = BoxType.Field;
                this.TargetBoxName = TypeMaps.BoxTypeMap[targetBoxType];
                uint boxTargetIndex = pol.Api.Target.TargetIndex;
                SetStatusText("フィールド箱を開けます 箱Index:{0}", boxTargetIndex);
                this.Box.ResetBox();
                SetViewData();
                Operation operation = new Operation();
                this.MumRemainCount = 0;

                while (!this.IsCancelling)
                {
                    if (pol.Api.Menu.IsMenuOpen)
                    {
                        if(pol.Api.Target.TargetName == Constants.BoxNameTreasureCasket){
                            // どうする？
                            if (EliteAPIWrapper.Utils.RegexUtil.IsMatch(pol.Api.Dialog.Question, Constants.BoxQuestionWhatWillYouDo))
                            {
                                pol.Api.Dialog.SetDialogIndex(1, true, settings.BaseWait); // 開錠する
                            }
                            // どうする？（残り(.*)回）
                            else if (EliteAPIWrapper.Utils.RegexUtil.IsMatch(pol.Api.Dialog.Question, Constants.BoxQuestionWhatWillYouDoWithRemain))
                            {
                                // 箱の初期化
                                if (operation.OperationType != OperationType.GetHints && operation.OperationType != OperationType.InputNumber)
                                {
                                    var val = EliteAPIWrapper.Utils.RegexUtil.GetMatchValue(pol.Api.Dialog.Question, Constants.BoxQuestionWhatWillYouDoWithRemain);
                                    operation = this.Box.StartUnlockFieldBox(int.Parse(val[0]));
                                    SetViewData();
                                }
                                // 箱の操作
                                if (!DoOperation(BoxType.Field, ref operation)) return false;
                            }
                        }
                    }
                    else
                    {
                        if (operation.OperationType == OperationType.Success )
                        {
                            // 箱空け成功
                            SetStatusText("箱空け成功");
                            operation = new Operation();
                            this.Box.ResetBox();
                            break;
                        }
                        else if (operation.OperationType == OperationType.Failed)
                        {
                            // 箱空け失敗
                            SetStatusText("箱空け失敗");
                            operation = new Operation();
                            this.Box.ResetBox();
                            break;
                        }
                        else
                        {
                            // 箱をタゲる
                            logger.DebugFormat("箱をターゲット TargetIndex:{0}", boxTargetIndex);
                            while (pol.Api.Target.TargetIndex != boxTargetIndex)
                            {
                                pol.Api.Target.TargetIndex = boxTargetIndex;
                                while (!pol.Api.Menu.IsMenuOpen)
                                {
                                    pol.Api.ThirdParty.KeyPress(KeyCode.EnterKey);
                                    Thread.Sleep(settings.BaseWait);
                                }
                            }
                        }
                    }
                    Thread.Sleep(settings.BaseWait);
                }
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                return false;
            }
        }
        /// <summary>
        /// 指定された操作を箱に行う
        /// </summary>
        /// <param name="iBoxType">箱タイプ</param>
        /// <param name="rOperation">Operation</param>
        /// <returns>成功した場合True</returns>
        private bool DoOperation(BoxType iBoxType, ref Operation rOperation)
        {
            if (rOperation.OperationType == OperationType.GetHints)
            {
                // カギを調べる
                SetStatusText("カギを調べる");
                pol.Api.Chat.ResetBuffer();
                pol.Api.Dialog.SetDialogIndex(MenuIndexGetHints[iBoxType], true, settings.BaseWait); // カギを調べる
            }
            else if (rOperation.OperationType == OperationType.InputNumber)
            {
                // 数値の入力
                SetStatusText("{0}を入力する", rOperation.InputNumber);
                pol.Api.Chat.ResetBuffer();
                pol.Api.Dialog.SetDialogIndex(MenuIndexInputNumber[iBoxType], true, settings.BaseWait); // 数字を入力する
                Thread.Sleep(settings.ChatWait);
                InputNumber(rOperation.InputNumber);
            }
            else
            {
                return false;
            }

            Thread.Sleep(settings.ChatWait);
            // ヒント追加
            rOperation = this.Box.AddHint(GetHintFromChat());
            SetViewData();
            return true;
        }
        /// <summary>
        /// ダイアログに数値を入力
        /// </summary>
        /// <param name="iNumber">入力する数値</param>
        private void InputNumber(int iNumber)
        {
            for (int i = 0; i < iNumber.ToString().Length; i++)
            {
                pol.Api.ThirdParty.KeyPress(KeyCodeMap[iNumber.ToString().Substring(i, 1)]);
                Thread.Sleep(settings.BaseWait);
            }
            pol.Api.ThirdParty.KeyPress(KeyCode.EnterKey);
        }
        /// <summary>
        /// チャットよりヒントを取得
        /// </summary>
        /// <returns></returns>
        private string GetHintFromChat()
        {
            string ret = string.Empty;
            while (string.IsNullOrEmpty(ret))
            {
                ChatLine cl = pol.Api.Chat.GetNextChatLine();
                if (cl != null)
                {
                    Console.WriteLine(cl.Text);
                    if (this.Box.IsHint(cl.Text))
                    {
                        ret = cl.Text;
                    }
                }
            }
            return ret;
        }
        private void OpenMumBox(CancellationTokenSource iCts)
        {
            logger.Debug("MUM箱を開ける");
        }
        private void OpenBox(BoxType iBoxType)
        {
        }
        /// <summary>
        /// ステータス文字列の設定
        /// </summary>
        /// <param name="iMessage">メッセージ</param>
        /// <param name="iParam">メッセージパラメータ</param>
        public void SetStatusText(string iMessage, params object[] iParam)
        {
            StatusText = string.Format(iMessage, iParam);
            logger.InfoFormat(iMessage, iParam);
            Console.WriteLine(iMessage, iParam);
        }
        /// <summary>
        /// 画面表示用データの設定
        /// </summary>
        private void SetViewData() 
        {

        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxUnlocker
{
    public enum BoxTypeKind : int
    {
        Field = 0,
        MumGil100 = 10,
        MumGil200 = 11,
        MumGil300 = 12,
        MumGil400 = 13,
        MumGil500 = 14,
        MumBayld20 = 21,
        MumBayld40 = 22,
        MumBayld60 = 23,
        MumBayld80 = 24,
        MumBayld100 = 25,
    }
    public enum BoxOperationKind :int
    {
        GETHINTS,
        INPUTNO,
        OPENSUCCESS,
        OPENFAILED,
    }

    public class BoxOperation
    {
        public BoxOperationKind Operation { get; set; }
        public int InputNo { get; set; }
    }

    class Box
    {
        public BoxTypeKind BoxType { get; set; }
        public ArrayList EnableNo { get; set; }
        public string EnableNoLine { get; set; }
        public int TryRemainCnt { get; set; }
        public ArrayList Hints { get; set; }

        public const string BoxHintsString1 = "カギの数字は([0-9]*)より(大きい|小さい)ようだ……。";
        public const string BoxHintsString2 = "カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。";
        public const string BoxHintsString3 = "カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。";
        public const string BoxHintsString4 = "カギの2桁の数字のどちらかは([0-9])のようだ……。";
        public const string BoxHintsString5 = "カギの数字の([0-9])桁目は(偶数|奇数)のようだ……。";
        public const string BoxHintsString6 = "(.*)は、開錠に成功した！";
        public const string BoxHintsString7 = "(.*)は、開錠に失敗した……。";
        public readonly ArrayList BoxHintsStringArr = new ArrayList() { BoxHintsString1, BoxHintsString2, BoxHintsString3, BoxHintsString4, BoxHintsString5, BoxHintsString6, BoxHintsString7 };

        private Boolean isOpenSuccess = false;
        private Boolean isOpenFailed = false;
        private readonly int[] openableCnt = new int[] { 0, 1, 3, 7, 15, 31, 62 };
        
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Box()
            : this(BoxTypeKind.Field)
        {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="iBoxType">箱のタイプ</param>
        public Box(BoxTypeKind iBoxType)
        {
            isOpenSuccess = false;
            isOpenFailed = false;
            BoxType = iBoxType;
            reflesh();
            Hints = new ArrayList();
        }
        #endregion
        /// <summary>
        /// 次の操作を取得する
        /// </summary>
        /// <returns>BoxOperation</returns>
        public BoxOperation GetNextOperation()
        {
            BoxOperation bo = new BoxOperation();
            if (isOpenSuccess)
            {
                bo.Operation = BoxOperationKind.OPENSUCCESS;
                bo.InputNo = 0;
            }
            else if (isOpenFailed)
            {
                bo.Operation = BoxOperationKind.OPENFAILED;
                bo.InputNo = 0;
            }
            else
            {
                if (isBoxOpenable() || TryRemainCnt == 1)
                {
                    bo.Operation = BoxOperationKind.INPUTNO;
                    bo.InputNo = int.Parse(getNextInputNoIndex());
                }
                else
                {
                    bo.Operation = BoxOperationKind.GETHINTS;
                    bo.InputNo = 0;
                }
            }
            Tools.DebugMessage("残り回数： " + TryRemainCnt.ToString() + " 指示：" + bo.Operation.ToString() + "/" + bo.InputNo.ToString());
            return bo;
        }
        /// <summary>
        /// 受け取ったチャット内容より
        /// </summary>
        /// <param name="iChat"></param>
        public void SetHints(ArrayList iChatArr)
        {
            ArrayList tmpArr = new ArrayList();
            ArrayList reg = new ArrayList();
            for (int i = 0; i < iChatArr.Count; i++)
            {
                if (!Tools.IsRegexString(iChatArr[i].ToString(), BoxHintsStringArr)) continue;
                Tools.DebugMessage("指示文字列：" + iChatArr[i].ToString());
                for (int j = 0; j < EnableNo.Count; j++)
                {
                    //カギの数字は([0-9]*)より(大きい|小さい)ようだ……。
                    reg = Tools.GetRegexString(iChatArr[i].ToString(), BoxHintsString1);
                    if (reg.Count > 0)
                    {
                        Hints.Add(iChatArr[i]);
                        if (reg[1].ToString().Equals("大きい"))
                        {
                            if (int.Parse(EnableNo[j].ToString()) > int.Parse(reg[0].ToString()))
                            {
                                tmpArr.Add(EnableNo[j]);
                            }
                        }
                        else if (reg[1].ToString().Equals("小さい"))
                        {
                            if (int.Parse(EnableNo[j].ToString()) < int.Parse(reg[0].ToString())) tmpArr.Add(EnableNo[j]);
                        }
                    }
                    //カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。
                    reg = Tools.GetRegexString(iChatArr[i].ToString(), BoxHintsString2);
                    if (reg.Count > 0)
                    {
                        if (int.Parse(EnableNo[j].ToString()) > int.Parse(reg[0].ToString()) &&
                            int.Parse(EnableNo[j].ToString()) < int.Parse(reg[1].ToString())) tmpArr.Add(EnableNo[j]);
                    }
                    //カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。
                    reg = Tools.GetRegexString(iChatArr[i].ToString(), BoxHintsString3);
                    if (reg.Count > 0)
                    {
                        int col = 0;
                        if (reg[0].Equals("1"))
                        {
                            col = 1;
                        }
                        else if (reg[0].Equals("2"))
                        {
                            col = 0;
                        }
                        if (EnableNo[j].ToString().Substring(col, 1) == reg[1].ToString() ||
                            EnableNo[j].ToString().Substring(col, 1) == reg[2].ToString() ||
                            EnableNo[j].ToString().Substring(col, 1) == reg[3].ToString()) tmpArr.Add(EnableNo[j]);
                    }
                    //カギの2桁の数字のどちらかは([0-9])のようだ……。
                    reg = Tools.GetRegexString(iChatArr[i].ToString(), BoxHintsString4);
                    if (reg.Count > 0)
                    {
                        if (EnableNo[j].ToString().Substring(0, 1) == reg[0].ToString() ||
                            EnableNo[j].ToString().Substring(1, 1) == reg[0].ToString()) tmpArr.Add(EnableNo[j]);
                    }
                    //カギの数字の([0-9])桁目は(偶数|奇数)のようだ……。
                    reg = Tools.GetRegexString(iChatArr[i].ToString(), BoxHintsString5);
                    if (reg.Count > 0)
                    {
                        int col = 0;
                        if (reg[0].Equals("1"))
                        {
                            col = 1;
                        }
                        else if (reg[0].Equals("2"))
                        {
                            col = 0;
                        }
                        if (reg[1].Equals("偶数") && (int.Parse(EnableNo[j].ToString().Substring(col, 1)) % 2 == 0)) tmpArr.Add(EnableNo[j]);
                        if (reg[1].Equals("奇数") && (int.Parse(EnableNo[j].ToString().Substring(col, 1)) % 2 == 1)) tmpArr.Add(EnableNo[j]);
                    }
                    //(.*)は、開錠に成功した！
                    if (Tools.GetRegexString(iChatArr[i].ToString(), BoxHintsString6).Count > 0)
                    {
                        isOpenSuccess = true;
                    }
                    //(.*)は、開錠に失敗した……。
                    if (Tools.GetRegexString(iChatArr[i].ToString(), BoxHintsString7).Count > 0)
                    {
                        isOpenFailed = true;
                    }
                }
                EnableNo = tmpArr;
                EnableNoLine = getEnableNoLine();
                Tools.DebugMessage("指示文字列結果：" + EnableNoLine);
            }
        }
        /// <summary>
        /// 入力する数字を取得
        /// </summary>
        /// <returns>入力する数字</returns>
        private string getNextInputNoIndex()
        {
            double enableNoCnt = EnableNo.Count;
            enableNoCnt = enableNoCnt / 2;
            int ret = (int)Math.Ceiling(enableNoCnt)-1;
            return EnableNo[ret].ToString();
        }
        /// <summary>
        /// 残り回数から箱空け可能かを判断
        /// </summary>
        /// <returns></returns>
        private Boolean isBoxOpenable()
        {
            if (EnableNo.Count <= openableCnt[TryRemainCnt])
            {
                return true;
            }
            /*
            double cnt = (double)EnableNo.Count;
            for (int i = 0; i < TryRemainCnt; i++)
            {
                cnt = (cnt - 1) / 2.0;
                cnt = Math.Ceiling(cnt);
                if (cnt <= 1) return true;
            }
             */
            return false;
        }
        /// <summary>
        /// 初期化処理
        /// </summary>
        private void reflesh()
        {
            int fromNo = 0;
            int toNo = 0;
            switch (BoxType)
            {
                case BoxTypeKind.MumGil100:
                case BoxTypeKind.MumBayld20:
                    fromNo = 50;
                    toNo = 99;
                    break;
                case BoxTypeKind.MumGil200:
                case BoxTypeKind.MumBayld40:
                    fromNo = 30;
                    toNo = 99;
                    break;
                default:
                    fromNo = 10;
                    toNo = 99;
                    break;
            }
            ArrayList enableNo = new ArrayList();
            for (int i = fromNo; i <= toNo; i++)
            {
                enableNo.Add(i.ToString());
            }
            EnableNo = enableNo;
            EnableNoLine = getEnableNoLine();
            TryRemainCnt = 0;
        }
        /// <summary>
        /// 現在有効な数字の羅列を返す
        /// </summary>
        /// <returns></returns>
        private string getEnableNoLine()
        {
            string retStr = "";
            for (int i = 0; i < EnableNo.Count; i++)
            {
                if (i != (EnableNo.Count - 1))
                {
                    retStr = retStr + EnableNo[i].ToString() + " ";
                }
                else
                {
                    retStr = retStr + EnableNo[i].ToString();
                }
            }
            return retStr;
        }

    }
}

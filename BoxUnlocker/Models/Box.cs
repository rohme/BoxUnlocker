using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Livet;
using log4net;

namespace BoxUnlocker.Models
{
    public class Box : NotificationObject
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // MUM箱のゲーム設定
        public readonly Dictionary<MumType, MumGame> MumConfigMap = new Dictionary<MumType, MumGame>
        {
            { MumType.Gil100,   new MumGame(){ NumberFrom = 50, NumberTo = 99, HintCount = 6 }},
            { MumType.Gil200,   new MumGame(){ NumberFrom = 30, NumberTo = 99, HintCount = 6 }},
            { MumType.Gil300,   new MumGame(){ NumberFrom = 10, NumberTo = 99, HintCount = 6 }},
            { MumType.Gil400,   new MumGame(){ NumberFrom = 10, NumberTo = 99, HintCount = 5 }},
            { MumType.Gil500,   new MumGame(){ NumberFrom = 10, NumberTo = 99, HintCount = 5 }},
            { MumType.Bayld20,  new MumGame(){ NumberFrom = 50, NumberTo = 99, HintCount = 6 }},
            { MumType.Bayld40,  new MumGame(){ NumberFrom = 30, NumberTo = 99, HintCount = 6 }},
            { MumType.Bayld60,  new MumGame(){ NumberFrom = 10, NumberTo = 99, HintCount = 6 }},
            { MumType.Bayld80,  new MumGame(){ NumberFrom = 10, NumberTo = 99, HintCount = 5 }},
            { MumType.Bayld100, new MumGame(){ NumberFrom = 10, NumberTo = 99, HintCount = 5 }},
        };

        /// <summary>
        /// コンストラクター
        /// </summary>
        public Box()
        {
            this.Hints = new List<string>();
            this.ValidNumbers = new List<int>();
            ResetBox();
        }

        #region メンバー
        private OperationType lastOperationType = OperationType.None;
        #region CurrentCount変更通知プロパティ
        private int _CurrentCount;
        public int CurrentCount
        {
            get
            { return _CurrentCount; }
            private set
            { 
                if (_CurrentCount == value)
                    return;
                _CurrentCount = value;
                RaisePropertyChanged("CurrentCount");
            }
        }
        #endregion
        #region MaxCount変更通知プロパティ
        private int _MaxCount;
        public int MaxCount
        {
            get
            { return _MaxCount; }
            private set
            { 
                if (_MaxCount == value)
                    return;
                _MaxCount = value;
                RaisePropertyChanged("MaxCount");
            }
        }
        #endregion
        public List<string> Hints { get; private set; }
        public string HintsForDisplay
        {
            get
            {
                string ret = string.Empty;
                foreach (var v in this.Hints)
                {
                    if (ret != string.Empty) ret += "\r\n";
                    ret += v;
                }
                return ret;
            }
        }
        public List<int> ValidNumbers { get; private set; }
        public string ValidNumbersForDisplay
        {
            get
            {
                string ret = string.Empty;
                foreach (var v in this.ValidNumbers)
                {
                    if (ret != string.Empty) ret += " ";
                    ret += v.ToString();
                } 
                return ret;
            }                

        }
        #endregion

        #region メソッド
        /// <summary>
        /// 箱の初期化
        /// </summary>
        public void ResetBox()
        {
            lastOperationType = OperationType.None;
            this.CurrentCount = 0;
            this.MaxCount = 0;
            this.Hints.Clear();
            RaisePropertyChanged("HintsForDisplay");
            this.ValidNumbers.Clear();
            RaisePropertyChanged("ValidNumbersForDisplay");
        }
        /// <summary>
        /// フィールド箱開始処理
        /// </summary>
        /// <param name="iMaxCount">初期残回数</param>
        public Operation StartUnlockFieldBox(int iMaxCount)
        {
            logger.DebugFormat("フィールド箱の初期化 MaxCount:{0}", iMaxCount);
            ResetBox();
            this.MaxCount = iMaxCount;
            this.ValidNumbers = GetInitValidNumbers(BoxType.Field);
            return GetNextOperation();
        }
        /// <summary>
        /// MUM箱開始処理
        /// </summary>
        /// <param name="iMumType">MUM箱タイプ</param>
        public Operation StartUnlockMumBox(MumType iMumType)
        {
            logger.DebugFormat("フィールド箱の初期化 MumType:{0}", iMumType);
            ResetBox();
            this.MaxCount = MumConfigMap[iMumType].HintCount;
            this.ValidNumbers = GetInitValidNumbers(BoxType.Mum, iMumType);
            return GetNextOperation();
        }
        /// <summary>
        /// ValidNumberの初期設定
        /// </summary>
        /// <param name="iBoxType">箱タイプ</param>
        /// <param name="iMumType">MUMタイプ（箱タイプをフィールドにした場合、指定不可）</param>
        /// <returns>ValidNumber</returns>
        private List<int> GetInitValidNumbers(BoxType iBoxType, params MumType[] iMumType)
        {
            var ret = new List<int>();
            int from = 0;
            int to = 0;
            //パラメータチェック
            if (iBoxType == BoxType.Field){
                if (iMumType.Count() > 0)
                {
                    logger.Warn("BoxTypeにFieldを指定している場合、MumTypeは指定できない");
                    return new List<int>();
                }
            }
            else if (iBoxType == BoxType.Mum)
            {
                if (iMumType.Count() == 0)
                {
                    logger.Warn("BoxTypeにMumを指定している場合、MumTypeは省略できない");
                    return new List<int>();
                }
                else if (iMumType.Count() > 1)
                {
                    logger.Warn("BoxTypeにMumを指定している場合、MumTypeは複数指定できない");
                    return new List<int>();
                }
            }
            //戻り値の設定
            if (iBoxType == BoxType.Field)
            {
                from = 10;
                to = 99;
            }
            else if (iBoxType == BoxType.Mum)
            {
                from = MumConfigMap[iMumType[0]].NumberFrom;
                to = MumConfigMap[iMumType[0]].NumberTo;
            }
            for (int i = from; i <= to; i++) ret.Add(i);
            return ret;
        }
        /// <summary>
        /// ヒントを追加し、次の操作を取得する
        /// </summary>
        /// <param name="iHint">ヒント文字列</param>
        /// <returns>Operation</returns>
        public Operation AddHint(string iHint)
        {
            logger.InfoFormat("ヒントを追加 {0}", iHint);
            var hintValue = new List<string>();

            //入力文字列チェック
            var hintType = GetHintType(iHint, out hintValue);
            if (hintType == HintType.None)
            {
                logger.WarnFormat("規定外のヒントが指定された Hint:{0}", iHint);
                return new Operation() { OperationType = OperationType.ErrorNotHint };
            }
            // ヒントに追加
            Hints.Add(iHint);
            RaisePropertyChanged("HintsForDisplay");
            if (hintType == HintType.Success)
            {
                logger.DebugFormat("箱空け成功 Hint:{0}", iHint);
                CurrentCount++; // 現在の回数をカウントアップ
                return new Operation() { OperationType = OperationType.Success };
            }
            else if (hintType == HintType.Failed)
            {
                logger.DebugFormat("箱空け失敗 Hint:{0}", iHint);
                CurrentCount++; // 現在の回数をカウントアップ
                return new Operation() { OperationType = OperationType.Failed };
            }
            // 候補の絞込み
            string log = string.Empty;
            foreach (var v in ValidNumbers) log += string.Format("{0:00} ", v);
            logger.DebugFormat("絞込前:{0}", log.Trim());
            logger.DebugFormat("絞込前件数:{0}", ValidNumbers.Count);
            if (!NarrowingValidNumbers(hintType, hintValue))
            {
                logger.WarnFormat("候補の絞込みで何かしらエラーが発生した Hint:{0}", iHint);
                return new Operation() { OperationType = OperationType.ErrorNarrowing };
            }
            log = string.Empty;
            foreach (var v in ValidNumbers) log += string.Format("{0:00} ", v);
            logger.InfoFormat("絞込後:{0}", log.Trim());
            logger.DebugFormat("絞込後件数:{0}", ValidNumbers.Count);
            // 現在の回数をカウントアップ
            CurrentCount++;
            logger.DebugFormat("回数:{0}/{1} 残り:{2}", CurrentCount, MaxCount, MaxCount - CurrentCount);
            // 次の操作を取得
            return GetNextOperation();
        }
        /// <summary>
        /// 現在の状況から、次の操作を取得する
        /// </summary>
        /// <returns></returns>
        private Operation GetNextOperation()
        {
            Operation ret = new Operation();
            int remainCount = this.MaxCount - this.CurrentCount;
            // 候補の数が、数値入力だけで開錠できる数を取得
            // 残り回数 ValidNumbersの数
            //    1           1
            //    2           3
            //    3           7
            //    4          15
            //    5          31
            //    6          63
            int inputOnlyOkCount = 0;
            for (int i = 1; i <= remainCount; i++)
            {
                if (i == 1)
                    inputOnlyOkCount = 1;
                else
                    inputOnlyOkCount = inputOnlyOkCount * 2 + 1;
            }
            logger.DebugFormat("{0}件以下なら数値入力 現在:{1}件", inputOnlyOkCount, ValidNumbers.Count);

            // 数値入力で箱空け可能な場合か、残り回数が１以下の場合、数値を入力する
            if (ValidNumbers.Count <= inputOnlyOkCount || remainCount <= 1)
            {
                // 数値入力
                ret.OperationType = OperationType.InputNumber;
                ret.InputNumber = ValidNumbers[(int)Math.Ceiling((double)ValidNumbers.Count / 2.0D) - 1];
            }
            else
            {
                // ヒントを聞く
                ret.OperationType = OperationType.GetHints;
                ret.InputNumber = 0;
            }
            lastOperationType = ret.OperationType;
            logger.DebugFormat("次の操作 OperationType:{0} InputNumber:{1}", ret.OperationType, ret.InputNumber);
            return ret;
        }
        /// <summary>
        /// ValidNumberを絞り込む
        /// </summary>
        /// <param name="iHintType">ヒントタイプ</param>
        /// <param name="iHintValue">ヒント補助文字列</param>
        /// <returns>成功した場合Trueを返す</returns>
        private bool NarrowingValidNumbers(HintType iHintType, List<string> iHintValue)
        {
            int dig = 0;
            //ValidNumbersのClone作成
            //var validNumbers = new List<int>(this.ValidNumbers);
            var validNumbers = new List<int>();
            foreach (var v in this.ValidNumbers) validNumbers.Add(v);
                RaisePropertyChanged("ValidNumbersForDisplay");
            //絞り込み処理
            foreach (var v in this.ValidNumbers)
            {
                switch (iHintType)
                {
                    case HintType.None:      // ヒントでは無かった時用
                        return false;
                    case HintType.Higher:    // カギの数字は([0-9]*)より大きいようだ……。
                        if (iHintValue.Count != 1) return false;
                        if (lastOperationType == OperationType.InputNumber)
                        {
                            if (!(v > int.Parse(iHintValue[0])))
                                validNumbers.Remove(v);
                        }
                        else
                        {
                            if (!(v >= int.Parse(iHintValue[0])))
                                validNumbers.Remove(v);
                        }
                        RaisePropertyChanged("ValidNumbersForDisplay");
                        break;
                    case HintType.Lower:     // カギの数字は([0-9]*)より小さいようだ……。
                        if (iHintValue.Count != 1) return false;
                        if (lastOperationType == OperationType.InputNumber)
                        {
                            if (!(v < int.Parse(iHintValue[0])))
                                validNumbers.Remove(v);
                        }
                        else
                        {
                            if (!(v <= int.Parse(iHintValue[0])))
                                validNumbers.Remove(v);
                        }
                        RaisePropertyChanged("ValidNumbersForDisplay");
                        break;
                    case HintType.Between:   // カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。
                        if (iHintValue.Count != 2) return false;
                        if (!(v >= int.Parse(iHintValue[0]) && v <= int.Parse(iHintValue[1])))
                            validNumbers.Remove(v);
                        RaisePropertyChanged("ValidNumbersForDisplay");
                        break;
                    case HintType.OneOfThem: // カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。
                        if (iHintValue.Count != 4) return false;
                        dig = (iHintValue[0] == "1") ? 1 : 0;
                        if (!(v.ToString().Substring(dig, 1) == iHintValue[1] ||
                              v.ToString().Substring(dig, 1) == iHintValue[2] ||
                              v.ToString().Substring(dig, 1) == iHintValue[3])) 
                            validNumbers.Remove(v);
                        RaisePropertyChanged("ValidNumbersForDisplay");
                        break;
                    case HintType.Either:    // カギの2桁の数字のどちらかは([0-9])のようだ……。
                        if (iHintValue.Count != 1) return false;
                        if (!(v.ToString().IndexOf(iHintValue[0]) >= 0))
                            validNumbers.Remove(v);
                        RaisePropertyChanged("ValidNumbersForDisplay");
                        break;
                    case HintType.Even:      // カギの数字の([0-9])桁目は偶数のようだ……。
                        if (iHintValue.Count != 1) return false;
                        dig = (iHintValue[0] == "1") ? 1 : 0;
                        if (!(int.Parse(v.ToString().Substring(dig, 1)) % 2 == 0))
                            validNumbers.Remove(v);
                        RaisePropertyChanged("ValidNumbersForDisplay");
                        break;
                    case HintType.Odd:       // カギの数字の([0-9])桁目は奇数のようだ……。
                        if (iHintValue.Count != 1) return false;
                        dig = (iHintValue[0] == "1") ? 1 : 0;
                        if (!(int.Parse(v.ToString().Substring(dig, 1)) % 2 == 1))
                            validNumbers.Remove(v);
                        RaisePropertyChanged("ValidNumbersForDisplay");
                        break;
                    case HintType.NoHint:    // 何も分からなかった……。
                        break;
                    case HintType.Success:   // (.*)は、開錠に成功した！
                        break;
                    case HintType.Failed:    // (.*)は、開錠に失敗した……。
                        break;
                    default:
                        return false;
                }
            }
            this.ValidNumbers = validNumbers;
            return true;
        }
        /// <summary>
        /// 文字列からHintTypeを取得
        /// </summary>
        /// <param name="iHint">ヒント</param>
        /// <returns>HintType ヒントではなかった場合Noneを返す</returns>
        public HintType GetHintType(string iHint, out List<string> oRegexValue)
        {
            oRegexValue = new List<string>();
            foreach (KeyValuePair<HintType, string> pairHint in TypeMaps.HintTypeMap)
            {
                Regex r = new Regex(pairHint.Value, RegexOptions.IgnoreCase);
                Match m = r.Match(iHint);
                if (m.Success)
                {
                    foreach (Group g in m.Groups)
                    {
                        foreach (Capture c in g.Captures)
                        {
                            if (c.Value != iHint)
                            {
                                oRegexValue.Add(c.Value);
                            }
                        }
                    }
                    return pairHint.Key;
                }
            }
            return HintType.None;
        }
        /// <summary>
        /// 文字列がヒントかを判定
        /// </summary>
        /// <param name="iHint">ヒント</param>
        /// <returns>ヒントの場合Trueを返す</returns>
        public bool IsHint(string iHint)
        {
            List<string> regexValue = new List<string>();
            return (GetHintType(iHint, out regexValue) != HintType.None);
        }
        #endregion
    }
}

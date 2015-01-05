using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace BoxUnlocker
{
    public class Constant
    {
        public readonly MumGameInfo[] MUMGAMEINFO = new MumGameInfo[10];
        public readonly int[] OpenableCnt = new int[] { 0, 1, 3, 7, 15, 31, 62 };
        public const int MaxLoopCnt = 100;//無限ループを防ぐためのループ制限回数
        //INI関連
        public const string IniFileName = "BoxUnlocker.ini";
        public const string IniSectionName = "BoxUnlocker";
        public const string IniKeyFormPosX = "PosX";
        public const string IniKeyFormPosY = "PosY";
        public const string IniKeyExecField = "ExecField";
        public const string IniKeyExecMum = "ExecMum";
        public const string IniKeyExecAbyssea = "ExecAbyssea";
        public const string IniKeyMumGameId = "MumGameID";
        public const string IniKeyMumTryCount = "MumTryCount";
        public const string IniKeyBaseWait = "BaseWait";
        public const string IniKeyNumberInputWait = "NumberInputWait";
        public const string IniKeyUseEnternity = "UseEnternity";
        //Fieldダイアログ関連
        public const string DialogStringField1 = "^どうする？$";
        public const string DialogStringField2 = "^どうする？（残り([0-9]*)回）$";
        //Mumダイアログ関連
        public const string DialogStringMum1 = "^どうする？$";
        public const string DialogStringMum2 = "難易度を選択してください((.*):([0-9]*))";
        public const string DialogStringMum3 = "^どうする？（残り([0-9]*)回）$";
        public const string DialogStringMum4 = "^もう1度やりますか？(.*)$";
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Constant()
        {
            #region MUMGAMEINFO[]
            MUMGAMEINFO[0].Id = BoxTypeKind.MumBayld20;
            MUMGAMEINFO[0].MenuIndex = 1;
            MUMGAMEINFO[0].Type = MumGameType.BAYLD;
            MUMGAMEINFO[0].Name = "ピジョン（20ベヤルド）";

            MUMGAMEINFO[1].Id = BoxTypeKind.MumBayld40;
            MUMGAMEINFO[1].MenuIndex = 2;
            MUMGAMEINFO[1].Type = MumGameType.BAYLD;
            MUMGAMEINFO[1].Name = "ハニムーナー（40ベヤルド）";

            MUMGAMEINFO[2].Id = BoxTypeKind.MumBayld60;
            MUMGAMEINFO[2].MenuIndex = 3;
            MUMGAMEINFO[2].Type = MumGameType.BAYLD;
            MUMGAMEINFO[2].Name = "シル（60ベヤルド）";

            MUMGAMEINFO[3].Id = BoxTypeKind.MumBayld80;
            MUMGAMEINFO[3].MenuIndex = 4;
            MUMGAMEINFO[3].Type = MumGameType.BAYLD;
            MUMGAMEINFO[3].Name = "プリーミアム（80ベヤルド）";

            MUMGAMEINFO[4].Id = BoxTypeKind.MumBayld100;
            MUMGAMEINFO[4].MenuIndex = 5;
            MUMGAMEINFO[4].Type = MumGameType.BAYLD;
            MUMGAMEINFO[4].Name = "シャーク（100ベヤルド）";

            MUMGAMEINFO[5].Id = BoxTypeKind.MumGil100;
            MUMGAMEINFO[5].MenuIndex = 1;
            MUMGAMEINFO[5].Type = MumGameType.GIL;
            MUMGAMEINFO[5].Name = "ピジョン（100ギル）";

            MUMGAMEINFO[6].Id = BoxTypeKind.MumGil200;
            MUMGAMEINFO[6].MenuIndex = 2;
            MUMGAMEINFO[6].Type = MumGameType.GIL;
            MUMGAMEINFO[6].Name = "ハニムーナー（200ギル）";

            MUMGAMEINFO[7].Id = BoxTypeKind.MumGil300;
            MUMGAMEINFO[7].MenuIndex = 3;
            MUMGAMEINFO[7].Type = MumGameType.GIL;
            MUMGAMEINFO[7].Name = "シル（300ギル）";

            MUMGAMEINFO[8].Id = BoxTypeKind.MumGil400;
            MUMGAMEINFO[8].MenuIndex = 4;
            MUMGAMEINFO[8].Type = MumGameType.GIL;
            MUMGAMEINFO[8].Name = "プリーミアム（400ギル）";

            MUMGAMEINFO[9].Id = BoxTypeKind.MumGil500;
            MUMGAMEINFO[9].MenuIndex = 5;
            MUMGAMEINFO[9].Type = MumGameType.GIL;
            MUMGAMEINFO[9].Name = "シャーク（500ギル）";
            #endregion

        }
        /// <summary>
        /// キーナンバー９９の種類
        /// </summary>
        /*public enum MumGameID : int
        {
            BAYLD20 = 11,
            BAYLD40 = 12,
            BAYLD60 = 13,
            BAYLD80 = 14,
            BAYLD100 = 15,
            GIL100 = 21,
            GIL200 = 22,
            GIL300 = 23,
            GIL400 = 24,
            GIL500 = 25,
        }*/
        //キーナンバー９９の種類
        public enum MumGameType : int
        {
            BAYLD = 0,
            GIL = 1,
        }
        //動作モード
        /*
        public enum ExecMode : int
        {
            Stop = 0,
            Wait = 1,
            Execute = 2,
        }
        */
        public enum MumStartRet : int
        {
            異常終了 = -1,
            正常終了 = 0,
            指定回数実行 = 1,

        }
        public enum BoxOpenMode : int
        {
            Field = 0,
            Mum = 1,
            Abyssea = 2,
        }
        public struct MumGameInfo
        {
            public BoxTypeKind Id;
            public short MenuIndex;
            public MumGameType Type;
            public string Name;
        }

        public MumGameInfo GetMumGameInfo(BoxTypeKind iID)
        {
            MumGameInfo tmp = new MumGameInfo();
            for (int i = 0; i < MUMGAMEINFO.Length; i++)
            {
                if (MUMGAMEINFO[i].Id == iID)
                {
                    tmp = MUMGAMEINFO[i];
                }
            }
            return tmp;
        }
        public MumGameInfo[] GetMumGameType()
        {
            return MUMGAMEINFO;
        }

    }

}

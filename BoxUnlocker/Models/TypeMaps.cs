using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxUnlocker.Models
{
    public static class TypeMaps
    {
        // 箱タイプ
        public static Dictionary<BoxType, string> BoxTypeMap = new Dictionary<BoxType, string>
        {
            { BoxType.Field, "フィールド箱" },
            { BoxType.Mum,   "ＭＵＭ箱" },
        };
        // MUMタイプ
        public static Dictionary<MumType, string> MumTypeMap = new Dictionary<MumType, string>
        {
            { MumType.Gil100,   "MUM 100ギル" },
            { MumType.Gil200,   "MUM 200ギル" },
            { MumType.Gil300,   "MUM 300ギル" },
            { MumType.Gil400,   "MUM 400ギル" },
            { MumType.Gil500,   "MUM 500ギル" },
            { MumType.Bayld20,  "MUM 20ベヤルド" },
            { MumType.Bayld40,  "MUM 40ベヤルド" },
            { MumType.Bayld60,  "MUM 60ベヤルド" },
            { MumType.Bayld80,  "MUM 80ベヤルド" },
            { MumType.Bayld100, "MUM 100ベヤルド" },
        };
        // ステータス
        public static Dictionary<StatusType, string> StatusTypeMap = new Dictionary<StatusType, string>{
            {StatusType.StartupComplete,"起動が完了しました"},
            {StatusType.MonitoringStart,"監視を開始しました"},
            {StatusType.MonitoringStop, "監視を停止しました"},
        };
        // 箱のヒント
        public static Dictionary<HintType, string> HintTypeMap = new Dictionary<HintType, string>
        {
            { HintType.Higher,    "カギの数字は([0-9]*)より大きいようだ……。" },
            { HintType.Lower,     "カギの数字は([0-9]*)より小さいようだ……。" },
            { HintType.Between,   "カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。" },
            { HintType.OneOfThem, "カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。" },
            { HintType.Either,    "カギの2桁の数字のどちらかは([0-9])のようだ……。" },
            { HintType.Even,      "カギの数字の([0-9])桁目は偶数のようだ……。" },
            { HintType.Odd,       "カギの数字の([0-9])桁目は奇数のようだ……。" },
            { HintType.NoHint,    "何も分からなかった……。" },
            { HintType.Success,   "(.*)は、開錠に成功した！" },
            { HintType.Failed,    "(.*)は、開錠に失敗した……。" },
        };
    }
}

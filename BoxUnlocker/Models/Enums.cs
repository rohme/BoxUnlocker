using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxUnlocker.Models
{
    // 箱タイプ
    public enum BoxType
    {
        None,
        Field,
        Mum,
    }
    // MUMタイプ
    public enum MumType
    {
        Gil100 = 0,
        Gil200 = 1,
        Gil300 = 2,
        Gil400 = 3,
        Gil500 = 4,
        Bayld20 = 10,
        Bayld40 = 11,
        Bayld60 = 12,
        Bayld80 = 13,
        Bayld100 = 14,
    }
    // ステータス
    public enum StatusType
    {
        StartupComplete,
        MonitoringStart,
        MonitoringStop,
    }
    // 箱の操作
    public enum OperationType
    {
        None,           // 初期値
        InputNumber,    // 数値入力
        GetHints,       // ヒント取得
        Success,        // 開錠成功
        Failed,         // 開錠失敗
        ErrorNotHint,   // ヒントではない文字列が渡ってきた場合
        ErrorNarrowing, //  数字絞込み中にエラーが発生した場合
    }
    // 箱のヒント
    public enum HintType
    {
        None,      // ヒントでは無かった時用
        Higher,    // カギの数字は([0-9]*)より大きいようだ……。
        Lower,     // カギの数字は([0-9]*)より小さいようだ……。
        Between,   // カギの数字は([0-9]*)より大きく、([0-9]*)より小さいようだ……。
        OneOfThem, // カギの数字の([0-9])桁目は([0-9])か([0-9])か([0-9])のどれかのようだ……。
        Either,    // カギの2桁の数字のどちらかは([0-9])のようだ……。
        Even,      // カギの数字の([0-9])桁目は偶数のようだ……。
        Odd,       // カギの数字の([0-9])桁目は奇数のようだ……。
        NoHint,    // 何も分からなかった……。
        Success,   // (.*)は、開錠に成功した！
        Failed,    // (.*)は、開錠に失敗した……。
    }

}


using BoxUnlocker.Models;
namespace BoxUnlocker
{
    public static class Constants
    {
        // INI関連
        public const string IniFileName = "BoxUnlocker.ini";
        public const string IniSection = "BoxUnlocker";
        public const string IniKeyWindowTop = "WindowTop";
        public const int IniDefaultWindowTop = 0;
        public const string IniKeyWindowLeft = "WindowLeft";
        public const int IniDefaultWindowLeft = 0;
        public const string IniKeyWindowHeight = "WindowHeight";
        public const int IniDefaultWindowHeight = 275;
        public const string IniKeyWindowWidth = "WindowWidth";
        public const int IniDefaultWindowWidth = 300;
        public const string IniKeyBaseWait = "BaseWait";
        public const int IniDefaultBaseWait = 200;
        public const string IniKeyChatWait = "ChatWait";
        public const int IniDefaultChatWait = 1000;
        public const string IniKeyMonitoringField = "MonitoringField";
        public const bool IniDefaultMonitoringField = true;
        public const string IniKeyMonitoringMum = "MonitoringMum";
        public const bool IniDefaultMonitoringMum = true;
        public const string IniKeyMumBoxType = "MumBoxType";
        public const MumType IniDefaultMumBoxType = MumType.Gil300;
        public const string IniKeyMumMaxCount = "MumMaxCount";
        public const int IniDefaultMumMaxCount = 100;

        // フィールド箱関連
        public const string BoxNameTreasureCasket = "Treasure Casket";
        public const string BoxQuestionWhatWillYouDo = "^どうする？$";
        public const string BoxQuestionWhatWillYouDoWithRemain = "^どうする？（残り([0-9]*)回）$";
        // MUM箱関連
        public const string MumNpcName = "Kerney";
        public const string MumQuestionWhatWillYouDo = "^どうする？$";
        public const string MumQuestionSelectLevel = "難易度を選択してください((.*):([0-9]*))";
        public const string MumQuestionWhatWillYouDoWithRemain = "^どうする？（残り([0-9]*)回）$";
        public const string MumQuestionTryAgain = "^もう1度やりますか？(.*)$";



        // その他
        public const string DefaultPlayerName = "<None>";
    }
}

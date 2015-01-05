using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxUnlocker
{
    class Pol
    {
        public int ProcId { get; set; }
        public string Name { get; set; }
    }
    class Settings
    {
        public int PolProcId { get; set; }
        public string PolPlayerName { get; set; }
        public int FormPosX { get; set; }
        public int FormPosY { get; set; }
        public Boolean ExecField { get; set; }
        public Boolean ExecMum  { get; set; }
        public Boolean ExecAbyssea { get; set; }
        public BoxTypeKind MumGameId { get; set; }
        public int MumTryCount { get; set; }
        public int BaseWait { get; set; }
        public int NumberInputWait { get; set; }
        public Boolean UseEnternity { get; set; }

        public Pol[] PolList { get; set; }

        private string IniFileName;
        private IniControl ini;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Settings()
        {
            IniFileName = Environment.CurrentDirectory + "\\" + Constant.IniFileName;
            ini = new IniControl(IniFileName);
            Load();
            //もしINIファイルが存在していなかった場合、INIファイルを初期値で作成する。
            if ( !File.Exists(IniFileName)) 
            {
                Save();
            }
        }
        /// <summary>
        /// INIファイルから設定を読み込む
        /// </summary>
        public void Load()
        {
            FormPosX = int.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyFormPosX, "0"));
            FormPosY = int.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyFormPosY, "0"));
            ExecField = Boolean.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyExecField, "True"));
            ExecMum = Boolean.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyExecMum, "True"));
            ExecAbyssea = Boolean.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyExecAbyssea, "True"));
            MumGameId = (BoxTypeKind)int.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyMumGameId, "21"));
            MumTryCount = int.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyMumTryCount, "10"));
            BaseWait = int.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyBaseWait, "300"));
            NumberInputWait = int.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyNumberInputWait, "1000"));
            UseEnternity = Boolean.Parse(ini.GetIniValue(Constant.IniSectionName, Constant.IniKeyUseEnternity, "true"));
        }
        /// <summary>
        /// INIファイルへ設定を保存する
        /// </summary>
        public void Save()
        {
            Boolean r;
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyFormPosX, FormPosX.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyFormPosY, FormPosY.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyExecField, ExecField.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyExecMum, ExecMum.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyExecAbyssea, ExecAbyssea.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyMumGameId, MumGameId.ToString("D"));
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyMumTryCount, MumTryCount.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyBaseWait, BaseWait.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyNumberInputWait, NumberInputWait.ToString());
            r = ini.SetIniValue(Constant.IniSectionName, Constant.IniKeyUseEnternity, UseEnternity.ToString());
        }

    }
}

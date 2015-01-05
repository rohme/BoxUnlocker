using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BoxUnlocker
{
    class IniControl
    {
        public string FileName { get; set; }
        public string DefaultValue { get; set; }
        public int BufferLength { get; set; }

        #region INI関連のDLL定義
        private class IniFileHandler
        {
            [DllImport("KERNEL32.DLL")]
            public static extern uint
              GetPrivateProfileString(string lpAppName,
              string lpKeyName, string lpDefault,
              StringBuilder lpReturnedString, uint nSize,
              string lpFileName);
            /*
            [DllImport("KERNEL32.DLL",
                EntryPoint = "GetPrivateProfileStringA")]
            public static extern uint
              GetPrivateProfileStringByByteArray(string lpAppName,
              string lpKeyName, string lpDefault,
              byte[] lpReturnedString, uint nSize,
              string lpFileName);
            
            [DllImport("KERNEL32.DLL")]
            public static extern uint
              GetPrivateProfileInt(string lpAppName,
              string lpKeyName, int nDefault, string lpFileName);
            */
            [DllImport("KERNEL32.DLL")]
            public static extern uint WritePrivateProfileString(
              string lpAppName,
              string lpKeyName,
              string lpString,
              string lpFileName);
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="iIniFilename">INIファイル名</param>
        public IniControl(string iIniFilename)
        {
            FileName = iIniFilename;
            DefaultValue = null;
            BufferLength = 1024;
        }

        /// <summary>
        /// INIファイルから読み込む
        /// </summary>
        /// <param name="iSection">セクション</param>
        /// <param name="iKey">キー</param>
        /// <returns>INIファイルから読み込んだ値を返す</returns>
        public string GetIniValue(string iSection, string iKey)
        {
            return GetIniValue(iSection, iKey, DefaultValue);
        }
        /// <summary>
        /// INIファイルから読み込む
        /// </summary>
        /// <param name="iSection">セクション</param>
        /// <param name="iKey">キー</param>
        /// <param name="iDefault">デフォルト文字列</param>
        /// <returns>INIファイルから読み込んだ値を返す</returns>
        public string GetIniValue(string iSection, string iKey, string iDefault)
        {
            StringBuilder sb = new StringBuilder(BufferLength);
            IniFileHandler.GetPrivateProfileString( iSection, iKey, iDefault, sb, (uint)sb.Capacity, FileName);
            if (sb.Equals(null))
            {
                return null;
            }
            return sb.ToString(); ;
        }

        /// <summary>
        /// INIファイルに保存
        /// </summary>
        /// <param name="iSection">セクション</param>
        /// <param name="iKey">キー</param>
        /// <param name="iValue">値</param>
        /// <returns></returns>
        public Boolean SetIniValue(string iSection, string iKey, string iValue)
        {
            uint ret = IniFileHandler.WritePrivateProfileString(iSection, iKey, iValue, FileName);
            if (ret != 0) return false;
            return true;
        }
    }
}

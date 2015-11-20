using System;
using System.IO;
using System.Reflection;
using Livet;
using log4net;


namespace BoxUnlocker.Models
{
    public class Settings : NotificationObject
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private EliteAPIWrapper.Utils.IniFileUtil iniHandler;

        public Settings()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), 
                                       Constants.IniFileName);
            iniHandler = new EliteAPIWrapper.Utils.IniFileUtil(path);
        }

        #region メンバー
        #region BaseWait変更通知プロパティ
        private int _BaseWait;
        public int BaseWait
        {
            get
            { return _BaseWait; }
            set
            { 
                if (_BaseWait == value)
                    return;
                _BaseWait = value;
                RaisePropertyChanged("BaseWait");
            }
        }
        #endregion

        #region ChatWait変更通知プロパティ
        private int _ChatWait;
        public int ChatWait
        {
            get
            { return _ChatWait; }
            set
            { 
                if (_ChatWait == value)
                    return;
                _ChatWait = value;
                RaisePropertyChanged("ChatWait");
            }
        }
        #endregion

        #region MonitoringField変更通知プロパティ
        private bool _MonitoringField;
        public bool MonitoringField
        {
            get
            { return _MonitoringField; }
            set
            { 
                if (_MonitoringField == value)
                    return;
                _MonitoringField = value;
                RaisePropertyChanged("MonitoringField");
            }
        }
        #endregion

        #region MonitoringMum変更通知プロパティ
        private bool _MonitoringMum;
        public bool MonitoringMum
        {
            get
            { return _MonitoringMum; }
            set
            { 
                if (_MonitoringMum == value)
                    return;
                _MonitoringMum = value;
                RaisePropertyChanged("MonitoringMum");
            }
        }
        #endregion

        #region MumBoxType変更通知プロパティ
        private MumType _MumBoxType;
        public MumType MumBoxType
        {
            get
            { return _MumBoxType; }
            set
            { 
                if (_MumBoxType == value)
                    return;
                _MumBoxType = value;
                RaisePropertyChanged("MumBoxType");
            }
        }
        #endregion

        #region MumMaxCount変更通知プロパティ
        private int _MumMaxCount;
        public int MumMaxCount
        {
            get
            { return _MumMaxCount; }
            set
            { 
                if (_MumMaxCount == value)
                    return;
                _MumMaxCount = value;
                RaisePropertyChanged("MumMaxCount");
            }
        }
        #endregion
        #endregion

        #region メソッド
        /// <summary>
        /// 設定の読込
        /// </summary>
        /// <returns>成功した場合Trueを返す</returns>
        public bool Load()
        {
            try
            {
                logger.Debug("INIファイル読込");
                this.BaseWait = iniHandler.GetInt(Constants.IniSection, Constants.IniKeyBaseWait, Constants.IniDefaultBaseWait);
                this.ChatWait = iniHandler.GetInt(Constants.IniSection, Constants.IniKeyChatWait, Constants.IniDefaultChatWait);
                this.MonitoringField = iniHandler.GetBool(Constants.IniSection, Constants.IniKeyMonitoringField, Constants.IniDefaultMonitoringField);
                this.MonitoringMum = iniHandler.GetBool(Constants.IniSection, Constants.IniKeyMonitoringMum, Constants.IniDefaultMonitoringMum);
                this.MumBoxType = (MumType)iniHandler.GetInt(Constants.IniSection, Constants.IniKeyMumBoxType, (int)Constants.IniDefaultMumBoxType);
                this.MumMaxCount = iniHandler.GetInt(Constants.IniSection, Constants.IniKeyMumMaxCount, Constants.IniDefaultMumMaxCount);
                logger.Info("INIファイル読込完了");
                return true;
            }
            catch (Exception e)
            {
                this.BaseWait = Constants.IniDefaultBaseWait;
                this.ChatWait = Constants.IniDefaultChatWait;
                this.MonitoringField = Constants.IniDefaultMonitoringField;
                this.MonitoringMum = Constants.IniDefaultMonitoringMum;
                this.MumBoxType = Constants.IniDefaultMumBoxType;
                this.MumMaxCount = Constants.IniDefaultMumMaxCount;
                logger.Error("INIファイルの読込でエラーが発生したので、初期値を設定しました。", e);
                return false;
            }
            finally
            {
                OutputLogValue();
            }
        }
        /// <summary>
        /// 設定の保存
        /// </summary>
        /// <returns>成功した場合Trueを返す</returns>
        public bool Save()
        {
            try
            {
                logger.Debug("INIファイル保存");
                OutputLogValue();
                if (!iniHandler.SetInt(Constants.IniSection, Constants.IniKeyBaseWait, this.BaseWait)) throw new Exception("BaseWait");
                if (!iniHandler.SetInt(Constants.IniSection, Constants.IniKeyChatWait, this.ChatWait)) throw new Exception("ChatWait");
                if (!iniHandler.SetBool(Constants.IniSection, Constants.IniKeyMonitoringField, this.MonitoringField)) throw new Exception("MonitoringField");
                if (!iniHandler.SetBool(Constants.IniSection, Constants.IniKeyMonitoringMum, this.MonitoringMum)) throw new Exception("MonitoringMum");
                if (!iniHandler.SetInt(Constants.IniSection, Constants.IniKeyMumBoxType, (int)this.MumBoxType)) throw new Exception("MumBoxType");
                if (!iniHandler.SetInt(Constants.IniSection, Constants.IniKeyMumMaxCount, this.MumMaxCount)) throw new Exception("MumMaxCount");
                logger.Debug("INIファイル保存完了");
                return true;
            }
            catch (Exception e)
            {
                logger.Error("INIファイル保存中にエラーが発生しました", e);
                return false;
            }
        }
        /// <summary>
        /// 設定内容をログ出力
        /// </summary>
        private void OutputLogValue()
        {
            logger.DebugFormat("  BaseWait:        {0}", this.BaseWait);
            logger.DebugFormat("  ChatWait:        {0}", this.ChatWait);
            logger.DebugFormat("  MonitoringField: {0}", this.MonitoringField);
            logger.DebugFormat("  MonitoringMum:   {0}", this.MonitoringMum);
            logger.DebugFormat("  MumBoxType:      {0}", this.MumBoxType);
            logger.DebugFormat("  MumMaxCount:     {0}", this.MumMaxCount);
        }
        #endregion
    }
}

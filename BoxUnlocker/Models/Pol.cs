using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Livet;
using log4net;

namespace BoxUnlocker.Models
{
    public class Pol : NotificationObject
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Pol()
        {
            this.PlayerName = Constants.DefaultPlayerName;
            this.IsAttaching = false;
        }

        #region メンバー
        public EliteAPIWrapper.EliteAPI Api { get; private set; }

        #region PolProcess変更通知プロパティ
        private Process _PolProcess;
        public Process PolProcess
        {
            get
            { return _PolProcess; }
            private set
            { 
                if (_PolProcess == value)
                    return;
                _PolProcess = value;
                RaisePropertyChanged("PolProcess");
            }
        }
        #endregion

        #region PlayerName変更通知プロパティ
        private string _PlayerName;
        public string PlayerName
        {
            get
            { return _PlayerName; }
            private set
            { 
                if (_PlayerName == value)
                    return;
                _PlayerName = value;
                RaisePropertyChanged("PlayerName");
            }
        }
        #endregion

        #region IsAttaching変更通知プロパティ
        private bool _IsAttaching;
        public bool IsAttaching
        {
            get
            { return _IsAttaching; }
            set
            { 
                if (_IsAttaching == value)
                    return;
                _IsAttaching = value;
                RaisePropertyChanged("IsAttaching");
            }
        }
        #endregion
        #endregion

        #region メソッド
        /// <summary>
        /// Polプロセスをアタッチ
        /// </summary>
        /// <param name="iProcess">Polプロセス</param>
        public void AttachPol(Process iProcess)
        {
            try
            {
                if (iProcess == null) return;
                logger.DebugFormat("プロセスアタッチ プロセスID:{0} ", iProcess.Id);
                this.IsAttaching = true;
                if (this.PolProcess == null)
                {
                    this.Api = new EliteAPIWrapper.EliteAPI(iProcess.Id);
                }
                else
                {
                    this.Api.Reinitialize(iProcess.Id);
                }
                this.PolProcess = iProcess;
                this.PlayerName = Api.Player.Name;

                logger.InfoFormat("プロセスアタッチ完了 プロセスID:{0} ", PolProcess.Id);
            }
            catch (Exception e)
            {
                logger.ErrorFormat("プロセスアタッチでエラーが発生 プロセスID:{0} ", PolProcess.Id, e);
            }
            finally
            {
                this.IsAttaching = false;
            }
        }
        #endregion
    }
}

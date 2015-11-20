using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace BoxUnlocker.Models
{
    public class PolList : NotificationObject
    {
        public PolList()
        {
            RefreshPolList();
        }
        public void RefreshPolList()
        {
            this._Pols.Clear();
            var Processes = EliteAPIWrapper.EliteAPI.GetPolProcessList();
            foreach (Process v in Processes)
            {
                _Pols.Add(v);
            }
        }

        #region PolList変更通知プロパティ
        private readonly ObservableCollection<Process> _Pols = new ObservableCollection<Process>();
        public ObservableCollection<Process> Pols
        {
            get { return _Pols; }
        }
        #endregion
 
    }
}

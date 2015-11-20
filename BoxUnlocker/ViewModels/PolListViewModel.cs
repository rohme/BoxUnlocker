using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using BoxUnlocker.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BoxUnlocker.ViewModels
{
    public class PolListViewModel : ViewModel
    {
        private PolList polList = new PolList();

        public void Initialize()
        {
            SelectedPol = null;
            Cancelled = true;
            polList.RefreshPolList();
        }

        public ObservableCollection<Process> PolList { get { return polList.Pols; } }

        #region メンバー
        #region SelectedPol変更通知プロパティ
        private Process _SelectedPol;
        public Process SelectedPol
        {
            get
            { return _SelectedPol; }
            set
            { 
                if (_SelectedPol == value)
                    return;
                _SelectedPol = value;
                RaisePropertyChanged("SelectedPol");
                SelectPolCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Cancelled変更通知プロパティ
        private bool _Cancelled;
        public bool Cancelled
        {
            get
            { return _Cancelled; }
            set
            { 
                if (_Cancelled == value)
                    return;
                _Cancelled = value;
                RaisePropertyChanged("Cancelled");
            }
        }
        #endregion
        #endregion

        #region コマンド
        #region SelectPolCommand
        private ViewModelCommand _SelectPolCommand;
        public ViewModelCommand SelectPolCommand
        {
            get
            {
                if (_SelectPolCommand == null)
                {
                    _SelectPolCommand = new ViewModelCommand(SelectPol, CanSelectPol);
                }
                return _SelectPolCommand;
            }
        }
        public bool CanSelectPol()
        {
            return (SelectedPol != null && SelectedPol.Id > 0);
        }
        public void SelectPol()
        {
            Cancelled = false;
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }
        #endregion

        #region RefreshPolListCommand
        private ViewModelCommand _RefreshPolListCommand;
        public ViewModelCommand RefreshPolListCommand
        {
            get
            {
                if (_RefreshPolListCommand == null)
                {
                    _RefreshPolListCommand = new ViewModelCommand(RefreshPolList);
                }
                return _RefreshPolListCommand;
            }
        }
        public void RefreshPolList()
        {
            polList.RefreshPolList();
        }
        #endregion
        #endregion

        #region メソッド
        #endregion
    }
}

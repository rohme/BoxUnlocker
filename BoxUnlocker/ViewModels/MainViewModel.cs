using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.EventListeners;
using log4net;

using BoxUnlocker.Models;

namespace BoxUnlocker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainViewModel()
        {
            this.Settings = new Settings();
            this.Pol = new Pol();
            this.Unlocker = new Unlocker(this.Settings, this.Pol);

            //var unlockerListener = new PropertyChangedEventListener(unlocker, (sender, e) => { RaisePropertyChanged(e.PropertyName); });
            //CompositeDisposable.Add(unlockerListener);

            this.Unlocker.SetStatusText("起動が完了しました");
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            //設定読込
            this.Settings.Load();

            // バージョン情報の設定
            var ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            ApplicationName = ver.ProductName;
            ApplicationVersion = string.Format("Ver{0}.{1}.{2}", ver.ProductMajorPart, ver.ProductMinorPart, ver.ProductBuildPart);

            // PolSelectWindowを開く
            var pols = EliteAPIWrapper.EliteAPI.GetPolProcessList();
            Process selectedPol;
            if (pols.Count == 1)
            {
                // Polがひとつしか見つからなかった場合、自動的に選択
                selectedPol = pols[0];
            }
            else
            {
                // POLが複数あるか、見つからなかった場合PolSelectWindowを開く
                selectedPol = SelectPolList();
            }

            if (selectedPol != null)
            {
                // プロセスアタッチ
                this.Pol.AttachPol(selectedPol);
                ExecuteCommand.RaiseCanExecuteChanged();
                // 監視実行
                if (this.Pol.PolProcess != null) Execute();
            }
        }
        /// <summary>
        /// ウィンドウクローズ処理
        /// </summary>
        public void WindowClose()
        {
            // 設定保存
            this.Settings.Save();
        }

        #region メンバー
        #region Settings変更通知プロパティ
        private Settings _Settings;
        public Settings Settings
        {
            get
            { return _Settings; }
            set
            { 
                if (_Settings == value)
                    return;
                _Settings = value;
                RaisePropertyChanged("Settings");
            }
        }
        #endregion
        #region Pol変更通知プロパティ
        private Pol _Pol;
        public Pol Pol
        {
            get
            { return _Pol; }
            set
            { 
                if (_Pol == value)
                    return;
                _Pol = value;
                RaisePropertyChanged("Pol");
            }
        }
        #endregion
        #region Unlocker変更通知プロパティ
        private Unlocker _Unlocker;
        public Unlocker Unlocker
        {
            get
            { return _Unlocker; }
            set
            { 
                if (_Unlocker == value)
                    return;
                _Unlocker = value;
                RaisePropertyChanged("Unlocker");
            }
        }
        #endregion
        #region メイン
        #region ExecuteToopTip変更通知プロパティ
        private string _ExecuteToopTip;
        public string ExecuteToopTip
        {
            get
            { return _ExecuteToopTip; }
            set
            { 
                if (_ExecuteToopTip == value)
                    return;
                _ExecuteToopTip = value;
                RaisePropertyChanged("ExecuteToopTip");
            }
        }
        #endregion
        #endregion
        #region 設定
        public int MinHeight
        {
            get { return Constants.IniDefaultWindowHeight; }
        }
        public int MinWidth
        {
            get { return Constants.IniDefaultWindowWidth; }
        }
        public Dictionary<MumType, string> MumTypeMap
        {
            get { return TypeMaps.MumTypeMap; }
        }
        #region ApplicationName変更通知プロパティ
        private string _ApplicationName;
        public string ApplicationName
        {
            get
            { return _ApplicationName; }
            set
            { 
                if (_ApplicationName == value)
                    return;
                _ApplicationName = value;
                RaisePropertyChanged("ApplicationName");
            }
        }
        #endregion
        #region ApplicationVersion変更通知プロパティ
        private string _ApplicationVersion;
        public string ApplicationVersion
        {
            get
            { return _ApplicationVersion; }
            set
            { 
                if (_ApplicationVersion == value)
                    return;
                _ApplicationVersion = value;
                RaisePropertyChanged("ApplicationVersion");
            }
        }
        #endregion
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
                    _SelectPolCommand = new ViewModelCommand(SelectPol);
                }
                return _SelectPolCommand;
            }
        }
        public void SelectPol()
        {
            var selectedPol = SelectPolList();
            if (selectedPol != null)
            {
                this.Pol.AttachPol(selectedPol);
                ExecuteCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion
        
        #region ExecuteCommand
        private ViewModelCommand _ExecuteCommand;
        public ViewModelCommand ExecuteCommand
        {
            get
            {
                if (_ExecuteCommand == null)
                {
                    _ExecuteCommand = new ViewModelCommand(Execute, CanExecute);
                }
                return _ExecuteCommand;
            }
        }
        public bool CanExecute()
        {
            return (this.Pol.PlayerName != Constants.DefaultPlayerName);
        }
        public void Execute()
        {
            // 設定保存
            this.Settings.Save();
            if (!this.Unlocker.IsMonitoring)
            {
                // 監視実行
                this.Unlocker.StartMonitoring();
            }
            else
            {
                // 監視停止
                this.Unlocker.StopMonitoring();
            }
        }
        #endregion

        #region GitHubCommand
        private ViewModelCommand _GitHubCommand;
        public ViewModelCommand GitHubCommand
        {
            get
            {
                if (_GitHubCommand == null)
                {
                    _GitHubCommand = new ViewModelCommand(GitHub);
                }
                return _GitHubCommand;
            }
        }
        public void GitHub()
        {
            System.Diagnostics.Process.Start("https://github.com/rohme/BoxUnlocker"); 
        }
        #endregion
        #endregion

        #region メソッド
        /// <summary>
        /// Pol選択ウィンドウを表示する
        /// </summary>
        /// <returns>プロセス キャンセルされた場合はnullを返す</returns>
        private Process SelectPolList()
        {
            var polList = new PolListViewModel();
            var message = new TransitionMessage(typeof(Views.PolWindow), polList, TransitionMode.Modal);
            Messenger.Raise(message);

            if (polList.Cancelled)
            {
                return null;
            }
            return polList.SelectedPol;
        }
        #endregion

    }
}

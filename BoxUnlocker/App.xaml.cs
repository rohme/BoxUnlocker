using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using Livet;
using log4net;

namespace BoxUnlocker
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.UIDispatcher = Dispatcher;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            logger.InfoFormat("{0} {1} 起動 =========================", ver.ProductName, string.Format("Ver{0}.{1}.{2}", ver.ProductMajorPart, ver.ProductMinorPart, ver.ProductBuildPart));

            var vm = new ViewModels.MainViewModel();
            var v = new Views.MainWindow() { DataContext = vm };
            v.Show();
        }

        /// <summary>
        /// 集約エラーハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null) ShowError(ex, "UnhandledException");
            System.Environment.Exit(1); // プログラム終了
        }

        /// <summary>
        /// エラーメッセージの表示
        /// </summary>
        /// <param name="e"></param>
        /// <param name="title"></param>
        private void ShowError(Exception e, string title)
        {
            logger.Fatal(title, e);
            string msg = string.Format("補足されないエラーが発生しました。\r詳細はログファイルを参照してください。\r\r{0}\r{1}", e.Message, e.StackTrace);
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

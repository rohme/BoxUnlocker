using System.Windows;
using System.Diagnostics;
using System.Reflection;

using Livet;
using BoxUnlocker.Views;
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
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            logger.InfoFormat("{0} {1} 起動 =========================", ver.ProductName, string.Format("Ver{0}.{1}.{2}", ver.ProductMajorPart, ver.ProductMinorPart, ver.ProductBuildPart));

            Views.MainWindow mw = new MainWindow();
            mw.Show();
        }

        //集約エラーハンドラ
        //private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    //TODO:ロギング処理など
        //    MessageBox.Show(
        //        "不明なエラーが発生しました。アプリケーションを終了します。",
        //        "エラー",
        //        MessageBoxButton.OK,
        //        MessageBoxImage.Error);
        //
        //    Environment.Exit(1);
        //}
    }
}

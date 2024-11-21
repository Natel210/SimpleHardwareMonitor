using SimpleFileIO.Log.Text;
using SimpleHardwareMonitor;
using System.Windows;
using System.Windows.Threading;

namespace SimpleHardwareMonitorGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();
        private ITextLog? _errorLog;

        protected override void OnStartup(StartupEventArgs e)
        {
            SimpleFileIO.Manager.CreateTextLog("ErrorLog", new() { RootDirectory = new("./"), FileName = "ErrorLog", Extension = ".errorlog" });
            _errorLog = SimpleFileIO.Manager.GetTextLog("ErrorLog");
            // UI 스레드에서 처리되지 않은 예외를 캐치합니다.
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            // 모든 스레드에서 처리되지 않은 예외를 캐치합니다.
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            // Task에서 발생하고 관찰되지 않은 예외를 캐치합니다.
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            SimpleOverlayTheme.ThemeSystem.Manager.InitializeModule();
            base.OnStartup(e);
            HardwareMonitor.Initialized();
            //AllocConsole();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            HardwareMonitor.Release();
            base.OnExit(e);
        }


        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _errorLog?.Add("=============================================");
            _errorLog?.Add("============ 프로그램 비정상 검출 ===========");
            _errorLog?.Add($"=> 메세지\r\n{e.Exception.Message}");
            if (e.Exception.InnerException != null)
            {
                _errorLog?.Add($"----------------------------------------");
                _errorLog?.Add($"=>  세부사항\r\n{e.Exception.InnerException?.ToString()}");
            }
            _errorLog?.Add($"----------------------------------------");
            _errorLog?.Add($"=> 호출 스택\r\n{e.Exception.StackTrace}");
            _errorLog?.Add("=============================================\r\n");
            _errorLog?.Add("=============================================");
            _errorLog?.Add("============ 프로그램 비정상 검출 ===========");
            _errorLog?.Add("=============================================\r\n");
            
            _errorLog?.Write();

            MessageBox.Show($"프로그램이 비정상적인 동작이 확인되었습니다.\r\n" +
                $"시스템 로그를 확인해주세요.\r\n" +
                $"확인 후 프로그램을 종료합니다.\r\n" +
                $"-------------------------\r\n" +
                $"{e.Exception.Message}\r\n" +
                $"{e.Exception.StackTrace}\r\n",
                "프로그렘 비정상 동작(UI)",
                MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true; // 예외가 처리되었음을 시스템에 알립니다.
            Application.Current.Shutdown();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _errorLog?.Add("=============================================");
            _errorLog?.Add("============ 프로그램 비정상 검출 ===========");
            _errorLog?.Add($"=> 예외 발생(쓰레드) : {e.ExceptionObject.ToString()}");
            _errorLog?.Add("=============================================\r\n");
            _errorLog?.Add("=============================================");
            _errorLog?.Add("============ 프로그램 비정상 검출 ===========");
            _errorLog?.Add("=============================================\r\n");
            _errorLog?.Write();

            MessageBox.Show($"프로그램이 비정상적인 동작이 확인되었습니다.\r\n" +
                $"시스템 로그를 확인해주세요.\r\n" +
                $"확인 후 프로그램을 종료합니다.\r\n" +
                $"-------------------------\r\n" +
                $"{e.ExceptionObject.ToString()}\r\n",
                "프로그렘 비정상 동작(ALL THREAD)",
                MessageBoxButton.OK, MessageBoxImage.Error);

            Application.Current.Shutdown();
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            _errorLog?.Add("=============================================");
            _errorLog?.Add("============ 프로그램 비정상 검출 ===========");
            _errorLog?.Add($"=> 메세지\r\n{e.Exception.Message}");
            _errorLog?.Add($"----------------------------------------");
            _errorLog?.Add($"=>  세부사항\r\n{e.Exception.InnerException?.ToString()}");
            _errorLog?.Add($"----------------------------------------");
            _errorLog?.Add($"=> 호출 스택\r\n{e.Exception.StackTrace}");
            _errorLog?.Add("=============================================\r\n");
            _errorLog?.Add("=============================================");
            _errorLog?.Add("============ 프로그램 비정상 검출 ===========");
            _errorLog?.Add("=============================================\r\n");
            _errorLog?.Write();

            MessageBox.Show($"프로그램이 비정상적인 동작이 확인되었습니다.\r\n" +
                $"시스템 로그를 확인해주세요.\r\n" +
                $"확인 후 프로그램을 종료합니다.\r\n" +
                $"-------------------------\r\n" +
                $"{e.Exception.Message}\r\n" +
                $"{e.Exception.StackTrace}\r\n",
                "프로그렘 비정상 동작(TASK)",
                MessageBoxButton.OK, MessageBoxImage.Error);

            e.SetObserved(); // 예외가 관찰되었음을 시스템에 알립니다.
            Application.Current.Shutdown();
        }
    }
}

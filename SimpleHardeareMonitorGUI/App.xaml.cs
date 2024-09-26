using SimpleHardwareMonitor;
using SimpleOverlayTheme.ThemeSystem;
using System.Windows;

namespace SimpleHardwareMonitorGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            HardwareMonitor.Initialized();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            HardwareMonitor.Release();
            base.OnExit(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Manager.InitializeModule();
            Manager.CurrentThemeName = "Dark";
        }
    }
}

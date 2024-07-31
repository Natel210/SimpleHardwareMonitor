using SimpleHardwareMonitor;
using System.Configuration;
using System.Data;
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
    }
}

using SimpleOverlayTheme.ThemeSystem;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SimpleHardWareDataParser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Manager.InitializeModule();
            Manager.CurrentThemeName = "Dark";
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }

}

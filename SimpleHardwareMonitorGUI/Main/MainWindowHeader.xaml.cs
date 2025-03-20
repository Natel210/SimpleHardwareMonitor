using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using SimpleHardwareMonitorGUI.Setting;

namespace SimpleHardwareMonitorGUI.Main
{
    public partial class MainWindowHeader : UserControl
    {
        public MainWindowHeader()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MainWindowTitleProperty =
            DependencyProperty.Register("MainWindowTitle", typeof(string), typeof(MainWindowHeader), new PropertyMetadata(""));

        //public string MainWindowTitle
        //{
        //    get { return (string)GetValue(MainWindowTitleProperty); }
        //    set { SetValue(MainWindowTitleProperty, value); }
        //}

        //public static readonly DependencyProperty MainWindowLoggingProperty =
        //    DependencyProperty.Register(nameof(MainWindowLogging), typeof(bool), typeof(MainWindowHeader), new PropertyMetadata(false));

        //public bool MainWindowLogging
        //{
        //    get { return (bool)GetValue(MainWindowLoggingProperty); }
        //    set { SetValue(MainWindowLoggingProperty, value); }
        //}

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            new SettingWindow() { Owner = Window.GetWindow(this) }.ShowDialog();
        }

        private void PART_Open_Logging_Folder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "./");
        }
    }
}

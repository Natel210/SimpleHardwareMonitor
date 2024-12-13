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

        public string MainWindowTitle
        {
            get { return (string)GetValue(MainWindowTitleProperty); }
            set { SetValue(MainWindowTitleProperty, value); }
        }

        public static readonly DependencyProperty MainWindowLoggingProperty =
            DependencyProperty.Register(nameof(MainWindowLogging), typeof(bool), typeof(MainWindowHeader), new PropertyMetadata(false));

        public bool MainWindowLogging
        {
            get { return (bool)GetValue(MainWindowLoggingProperty); }
            set { SetValue(MainWindowLoggingProperty, value); }
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            // 설정 버튼 클릭 시 처리할 내용
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.DataContext = this.DataContext;
            settingWindow.Owner = Window.GetWindow(this);
            settingWindow.ShowDialog();

        }
    }
}

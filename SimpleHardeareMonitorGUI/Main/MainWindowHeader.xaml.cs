using System.Windows;
using System.Windows.Controls;
using SimpleHardwareMonitorGUI.Setting;
using SimpleOverlayTheme.ThemeSystem;
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




        static bool testcode = false;
        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            if (testcode is true)
            {
                //GrayThemeUI.Main.ThemeDark();
            }
            else
            {
                //GrayThemeUI.Main.ThemeLight();
            }
            testcode = !testcode;

            // 설정 버튼 클릭 시 처리할 내용
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.ShowDialog();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.CurrentThemeName.CompareTo("Dark") is 0)
                Manager.CurrentThemeName = "Light";
            else if (Manager.CurrentThemeName.CompareTo("Light") is 0)
                Manager.CurrentThemeName = "Dark";
        }
    }
}

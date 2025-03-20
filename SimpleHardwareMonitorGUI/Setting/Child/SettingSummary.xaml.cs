using SimpleHardwareMonitorGUI.Model;
using System.Windows;
using System.Windows.Controls;

namespace SimpleHardwareMonitorGUI.Setting.Child
{
    /// <summary>
    /// SettingSummary.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingSummary : UserControl
    {
        public SettingSummary()
        {
            InitializeComponent();
            UpdateTheme();
            UpdateLoggingAutoCheck();
        }

        private void PART_SettingSeummay_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTheme();
            UpdateLoggingAutoCheck();
        }

        private void PART_MainWindowNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            GlobalModel.Instance.CommonData.MainWindowName = PART_MainWindowNameTextBox.Text;
            GlobalModel.Instance.Save();
        }

        private void PART_ThemeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PART_ThemeList.SelectedValue is string selectString)
                SimpleOverlayTheme.ThemeSystem.Manager.CurrentThemeName = selectString;
            GlobalModel.Instance.Save();
        }

        private void PART_LoggingAutoCheck_Checked(object sender, RoutedEventArgs e)
        {
            GlobalModel.Instance.RawLoggingData.EnableAutoSave_ProgramStartup = true;
            UpdateLoggingAutoCheck();
            GlobalModel.Instance.Save();
        }

        private void PART_LoggingAutoCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            GlobalModel.Instance.RawLoggingData.EnableAutoSave_ProgramStartup = false;
            UpdateLoggingAutoCheck();
            GlobalModel.Instance.Save();
        }


    }

    //internal
    public partial class SettingSummary : UserControl
    {
        private void UpdateTheme()
        {
            if (PART_ThemeList is null)
                return;
            PART_ThemeList.Items.Clear();
            foreach (var item in SimpleOverlayTheme.ThemeSystem.Manager.DataDictionary.Keys)
                PART_ThemeList.Items.Add(item);
            PART_ThemeList.SelectedValue = SimpleOverlayTheme.ThemeSystem.Manager.CurrentThemeName;
        }

        private void UpdateLoggingAutoCheck()
        {
            if (PART_LoggingAutoCheck is null)
                return;
            if (PART_LoggingAutoCheck_LabelText is null)
                return;
            if (PART_LoggingAutoCheck.IsChecked is null)
                throw new InvalidProgramException("LoggingAutoCheck is always true or false.");
            if (GlobalModel.Instance.RawLoggingData.EnableAutoSave_ProgramStartup is true)
                PART_LoggingAutoCheck_LabelText.Text = "Check";
            else
                PART_LoggingAutoCheck_LabelText.Text = "UnCheck";

            if (PART_LoggingAutoCheck.IsChecked != GlobalModel.Instance.RawLoggingData.EnableAutoSave_ProgramStartup)
                PART_LoggingAutoCheck.IsChecked = GlobalModel.Instance.RawLoggingData.EnableAutoSave_ProgramStartup;
        }
    }

}

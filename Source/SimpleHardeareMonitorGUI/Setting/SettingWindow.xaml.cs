using SimpleHardwareMonitorGUI.Rawdata;
using System.Windows;
using System.Windows.Controls;
using SimpleOverlayTheme.ThemeSystem;

namespace SimpleHardwareMonitorGUI.Setting
{
    /// <summary>
    /// SettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            UpdateTheme();
            //var enumValues = Enum.GetValues(typeof(ERawDataInterval)).Cast<ERawDataInterval>().Select(e => new
            //{
            //    Value = e,
            //    Group = GetEnumDescription(e)
            //});

            //var collectionView = new CollectionViewSource { Source = enumValues };
            //collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            //ComboBoxLoggingInterval.ItemsSource = collectionView.View;



        }

        private void UpdateTheme()
        {
            if (PART_ThemeList is null)
                return;
            PART_ThemeList.Items.Clear();
            foreach (var item in Manager.DataDictionary.Keys)
                PART_ThemeList.Items.Add(item);
            PART_ThemeList.SelectedValue = Manager.CurrentThemeName;
        }

        private void PART_ThemeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PART_ThemeList.SelectedValue is string selectString)
                Manager.CurrentThemeName = selectString;
        }

        private void Theme_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("준비 중입니다.","미완성");
            UpdateTheme();
        }

        private void LoggingInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (PART_ThemeList.SelectedValue is string selectString)
            //    Manager. = selectString;
        }
    }
}

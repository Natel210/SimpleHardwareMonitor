using SimpleHardWareDataParser.Rawdata;
using SimpleHardWareDataParser.Settting;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SimpleHardWareDataParser.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RawdataRecordManager.Load(new DirectoryInfo ("./"));
            DateTime startTime1 = new DateTime(2024, 10, 22, 13, 10, 0);
            DateTime endTime1 = new DateTime(2024, 10, 22, 13, 20, 0);
            RawdataRecordManager._rawdataSplitInfo.Add("TEST1",
                new() { SplitStart = startTime1, SplitEnd = endTime1 });
            DateTime startTime2 = new DateTime(2024, 10, 22, 13, 15, 0);
            DateTime endTime2 = new DateTime(2024, 10, 22, 13, 30, 0);
            RawdataRecordManager._rawdataSplitInfo.Add("TEST2",
                new() { SplitStart = startTime2, SplitEnd = endTime2 });

            RawdataRecordManager.Split();


            PART_TabItem.ItemsSource = RawdataRecordManager._rawdataSplitInfo.Keys;
            PART_TabItem.SelectionChanged += (object sender, SelectionChangedEventArgs e) => {
                if (PART_TabItem.SelectedItem.ToString() is string selectItemName )
                {
                    dataGrid.ItemsSource = RawdataRecordManager.Data[selectItemName].Values; ;
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.Show();
        }
    }
}
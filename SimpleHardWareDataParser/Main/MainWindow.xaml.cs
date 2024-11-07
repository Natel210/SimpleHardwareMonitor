using SimpleHardWareDataParser.Main.RawDataList;
using SimpleHardWareDataParser.Rawdata;
using SimpleHardWareDataParser.Settting;
using System.Dynamic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            RawdataRecordManager.Load(new DirectoryInfo("./"));
            if (RawdataRecordManager.OriginData.Count is 0)
                return;
            RawdataRecordManager.RawDataSplitInfo.Add(
                "ALL",
                new() {
                    SplitName = "ALL",
                    SplitStart = RawdataRecordManager.OriginData.First().Key,
                    SplitEnd = RawdataRecordManager.OriginData.Last().Key
                });

            RawdataRecordManager.RawDataSplitInfo.Add(
                "6-10-45",
                new() {
                    SplitName = "6-10-45",
                    SplitStart = new DateTime(2024, 11, 05, 18, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 06, 10, 45, 00)
                });

            RawdataRecordManager.RawDataSplitInfo.Add(
                "6-13-05",
                new() {
                    SplitName = "6-13-05",
                    SplitStart = new DateTime(2024, 11, 06, 10, 55, 01),
                    SplitEnd = new DateTime(2024, 11, 06, 13, 05, 00)
                });

            RawdataRecordManager.RawDataSplitInfo.Add(
                "6-17-05",
                new() {
                    SplitName = "6-17-05",
                    SplitStart = new DateTime(2024, 11, 06, 13, 05, 01),
                    SplitEnd = new DateTime(2024, 11, 06, 17, 05, 00)
                });

            RawdataRecordManager.RawDataSplitInfo.Add(
                "7-08-45",
                new() {
                    SplitName = "7-08-45",
                    SplitStart = new DateTime(2024, 11, 06, 17, 05, 01),
                    SplitEnd = new DateTime(2024, 11, 07, 08, 45, 00)
                });

            RawdataRecordManager.RawDataSplitInfo.Add(
                "7-11-30",
                new() {
                    SplitName = "7-11-30",
                    SplitStart = new DateTime(2024, 11, 07, 08, 45, 01),
                    SplitEnd = new DateTime(2024, 11, 07, 11, 30, 00)
                });

            RawdataRecordManager.RawDataSplitInfo.Add(
                "7-14-00",
                new() {
                    SplitName = "7-14-00",
                    SplitStart = new DateTime(2024, 11, 07, 11, 30, 01),
                    SplitEnd = new DateTime(2024, 11, 07, 14, 00, 00)
                });


            RawdataRecordManager.Split();

            PART_TabItem.Items.Clear();

            foreach (var item in RawdataRecordManager.RawDataSplitInfo.Keys)
            {
                TabItem tabItem = new()
                {
                    Header = item
                };
                var frame = new Frame();
                frame.Content = new RawDataList.RawDataList(RawdataRecordManager.RawDataSplitInfo[item], RawdataRecordManager.Data[item]);
                tabItem.Content = frame;
                PART_TabItem.Items.Add(tabItem);
            }


            //PART_TabItem.ItemsSource = RawdataRecordManager.RawDataSplitInfo.Keys;

            //PART_TabItem.Items["11"]


            //InitializeComponent();
            //PART_TabItem.ItemsSource = null;
            //PART_TabItem.Items.Clear();
            //foreach (var key in RawdataRecordManager.RawDataSplitInfo.Keys)
            //{
            //    //var tabItem = new TabItem
            //    //{
            //    //    Header = key // 탭의 제목으로 key를 설정
            //    //};

            //    //// 페이지나 유저 컨트롤을 생성하여 탭의 Content로 추가합니다.
            //    //// 예시로 UserControlExample이라는 유저 컨트롤을 추가
            //    //var userControl = new RawdataGrid(); // UserControlExample은 원하는 컨트롤로 대체 가능
            //    //userControl.dataGrid.ItemsSource = RawdataRecordManager.Data[key].Values;
            //    //// 또는 특정 페이지를 추가할 수도 있습니다.
            //    //// var page = new PageExample(); // PageExample은 원하는 페이지로 대체 가능

            //    //var frame = new Frame();
            //    //frame.Content = userControl;
            //    //tabItem.Content = frame; // 또는 page로 설정 가능
            //    //PART_TabItem.Items.Add(tabItem);








            //    PART_TabItem.ItemsSource = RawdataRecordManager.RawDataSplitInfo.Keys;
            //    PART_TabItem.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            //    {


            //        List<dynamic> dynamicDataTable = new();

            //        if (PART_TabItem.SelectedItem.ToString() is string selectItemName)
            //        {
            //            foreach (var item in RawdataRecordManager.Data[selectItemName].Values)
            //            {
            //                dynamic dynamicRecord = new ExpandoObject();
            //                dynamicRecord.Date = item.DateTime;
            //                dynamicRecord.CpuUse = item.CpuUse;
            //                dynamicRecord.CpuPower = item.CpuPower;
            //                dynamicRecord.CpuTemperature = item.CpuTemperature;
            //                //dynamicRecord.CpuUse = item.CpuUse;
            //                //dynamicRecord.CpuUse = item.CpuUse;
            //                dynamicDataTable.Add(dynamicRecord);
            //            }

            //            PART_DataGrid.ItemsSource = dynamicDataTable;

            //            //PART_DataGrid.ItemsSource = RawdataRecordManager.Data[selectItemName].Values;
            //        }

            //        PART_DataGrid.Columns.Clear();
            //        PART_DataGrid.Columns.Add(new DataGridTextColumn
            //        {
            //            Header = "Date",
            //            Binding = new Binding("Date")
            //        });

            //        PART_DataGrid.Columns.Add(new DataGridTextColumn
            //        {
            //            Header = "CPU Use (%)",
            //            Binding = new Binding("CpuUse")
            //        });

            //        PART_DataGrid.Columns.Add(new DataGridTextColumn
            //        {
            //            Header = "CPU Power (W)",
            //            Binding = new Binding("CpuPower")
            //        });

            //        PART_DataGrid.Columns.Add(new DataGridTextColumn
            //        {
            //            Header = "CPU Temperature (C)",
            //            Binding = new Binding("CpuTemperature")
            //        });

            //        // DataGrid에 데이터 바인딩
            //        PART_DataGrid.ItemsSource = dynamicDataTable;

            //    };
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.Show();
        }
    }
}
using SimpleHardWareDataParser.Main.RawDataList;
using SimpleHardWareDataParser.Rawdata;
using SimpleHardWareDataParser.Settting;
using System.Dynamic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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

            MakeGrid();


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

        private void ChangeRootPath_Button_Click(object sender, RoutedEventArgs e)
        {
            MakeGrid();
        }


        private void MakeGrid()
        {
            PART_TabItem.Items.Clear();
            var mainWindowViewmodel = DataContext as MainWindowViewmodel;
            string getRootDirectory;
            if (mainWindowViewmodel is not null)
                getRootDirectory = mainWindowViewmodel.RootDirectory;
            else
                getRootDirectory = "./Admin/";
            RawdataRecordManager.Load(new DirectoryInfo(getRootDirectory));
            if (RawdataRecordManager.OriginData.Count is 0)
                return;
            MakeSplitFormat();
            RawdataRecordManager.Split();

            string getterAllDatas = "";
            List<string> getterDatas = [];
            foreach (var item in RawdataRecordManager.RawDataSplitInfo.Keys)
            {
                TabItem tabItem = new()
                {
                    Header = item
                };
                var frame = new Frame();
                var rowList = new RawDataList.RawDataList(RawdataRecordManager.RawDataSplitInfo[item], RawdataRecordManager.Data[item]);

                if (item is "ALL")
                {
                    getterAllDatas = $"{rowList.CPU_USE_MIN}\t{rowList.CPU_USE_AVG}\t{rowList.CPU_USE_MAX}\t{rowList.CPU_TEMP_MIN}\t{rowList.CPU_TEMP_AVG}\t{rowList.CPU_TEMP_MAX}\t{rowList.CPU_POW_MIN}\t{rowList.CPU_POW_AVG}\t{rowList.CPU_POW_MAX}";
                }
                else
                {
                    getterDatas.Add($"{rowList.CPU_USE_MIN}\t{rowList.CPU_USE_AVG}\t{rowList.CPU_USE_MAX}\t{rowList.CPU_TEMP_MIN}\t{rowList.CPU_TEMP_AVG}\t{rowList.CPU_TEMP_MAX}\t{rowList.CPU_POW_MIN}\t{rowList.CPU_POW_AVG}\t{rowList.CPU_POW_MAX}");
                }


                frame.Content = rowList;
                tabItem.Content = frame;
                PART_TabItem.Items.Add(tabItem);
            }

            using (StreamWriter writer = new StreamWriter("./getteringALLData.datas", true, Encoding.UTF8))
            {
                writer.WriteLine(getterAllDatas);
            }

            using (StreamWriter writer = new StreamWriter("./getteringData.datas", false, Encoding.UTF8))
            {
                foreach (var item in getterDatas)
                {
                    writer.WriteLine(item);
                }
            }
        }

        private void MakeSplitFormat()
        {
            RawdataRecordManager.RawDataSplitInfo.Clear();
            RawdataRecordManager.RawDataSplitInfo.Add(
                "ALL",
                new()
                {
                    SplitName = "ALL",
                    SplitStart = RawdataRecordManager.OriginData.First().Key,
                    SplitEnd = RawdataRecordManager.OriginData.Last().Key
                });

            RawdataRecordManager.RawDataSplitInfo.Add(
                "1",
                new()
                {
                    SplitName = "1",
                    //SplitStart = new DateTime(2024, 11, 11, 10, 00, 00),
                    //SplitEnd = new DateTime(2024, 11, 11, 16, 00, 00)

                    SplitStart = new DateTime(2024, 11, 11, 18, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 12, 00, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "2",
                new()
                {
                    SplitName = "2",
                    //SplitStart = new DateTime(2024, 11, 11, 16, 00, 00),
                    //SplitEnd = new DateTime(2024, 11, 12, 01, 00, 00)

                    SplitStart = new DateTime(2024, 11, 12, 00, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 12, 09, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "3",
                new()
                {
                    SplitName = "3",
                    //SplitStart = new DateTime(2024, 11, 12, 01, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 12, 07, 00, 00)

                    SplitStart = new DateTime(2024, 11, 12, 09, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 12, 15, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "4",
                new()
                {
                    SplitName = "4",
                    //SplitStart = new DateTime(2024, 11, 12, 07, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 12, 11, 00, 00)

                    SplitStart = new DateTime(2024, 11, 12, 15, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 12, 19, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "5",
                new()
                {
                    SplitName = "5",
                    //SplitStart = new DateTime(2024, 11, 12, 11, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 13, 01, 00, 00)

                    SplitStart = new DateTime(2024, 11, 12, 19, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 13, 09, 00, 00)
                });



            //RawdataRecordManager.RawDataSplitInfo.Add(
            //    "1",
            //    new()
            //    {
            //        SplitName = "1",
            //        SplitStart = new DateTime(2024, 11, 09, 07, 00, 00),
            //        SplitEnd = new DateTime(2024, 11, 09, 16, 00, 00)

            //        //SplitStart = new DateTime(2024, 11, 09, 15, 00, 00),
            //        //SplitEnd = new DateTime(2024, 11, 10, 00, 00, 00)
            //    });

            //RawdataRecordManager.RawDataSplitInfo.Add(
            //    "2",
            //    new()
            //    {
            //        SplitName = "2",
            //        SplitStart = new DateTime(2024, 11, 09, 16, 00, 01),
            //        SplitEnd = new DateTime(2024, 11, 10, 00, 00, 00)
            //    });

            //RawdataRecordManager.RawDataSplitInfo.Add(
            //    "3",
            //    new()
            //    {
            //        SplitName = "3",
            //        SplitStart = new DateTime(2024, 11, 10, 00, 00, 01),
            //        SplitEnd = new DateTime(2024, 11, 10, 08, 00, 00)
            //    });

            //RawdataRecordManager.RawDataSplitInfo.Add(
            //    "4",
            //    new()
            //    {
            //        SplitName = "4",
            //        SplitStart = new DateTime(2024, 11, 10, 08, 00, 01),
            //        SplitEnd = new DateTime(2024, 11, 10, 16, 00, 00)
            //    });

            //RawdataRecordManager.RawDataSplitInfo.Add(
            //    "5",
            //    new()
            //    {
            //        SplitName = "5",
            //        SplitStart = new DateTime(2024, 11, 10, 16, 00, 01),
            //        SplitEnd = new DateTime(2024, 11, 11, 02, 00, 00)
            //    });


        }
    }
}
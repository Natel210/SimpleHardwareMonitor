using SimpleHardWareDataParser.Main.RawDataList;
using SimpleHardWareDataParser.Rawdata;
using SimpleHardWareDataParser.Settting;
using System;
using System.Dynamic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

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
            if (DataContext is MainWindowViewmodel vm)
                settingWindow.DataContext = vm.Setting;
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
            getterDatas.Add("id,date,cpu_use_min,cpu_use_avg,cpu_use_max,cpu_temp_min,cpu_temp_avg,cpu_temp_max,cpu_pow_min,cpu_pow_avg,cpu_pow_max");
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
                    getterAllDatas = $"{"TEMP"},{RawdataRecordManager.RawDataSplitInfo[item].SplitStart.ToString("yyyy-MM-dd HH:mm")},{RawdataRecordManager.RawDataSplitInfo[item].SplitEnd.ToString("yyyy-MM-dd HH:mm")},{rowList.CPU_USE_MIN},{rowList.CPU_USE_AVG},{rowList.CPU_USE_MAX},{rowList.CPU_TEMP_MIN},{rowList.CPU_TEMP_AVG},{rowList.CPU_TEMP_MAX},{rowList.CPU_POW_MIN},{rowList.CPU_POW_AVG},{rowList.CPU_POW_MAX}";
                }
                else
                {
                    getterDatas.Add($"{"TEMP"},{RawdataRecordManager.RawDataSplitInfo[item].SplitEnd.ToString("yyyy-MM-dd HH:mm")},{rowList.CPU_USE_MIN},{rowList.CPU_USE_AVG},{rowList.CPU_USE_MAX},{rowList.CPU_TEMP_MIN},{rowList.CPU_TEMP_AVG},{rowList.CPU_TEMP_MAX},{rowList.CPU_POW_MIN},{rowList.CPU_POW_AVG},{rowList.CPU_POW_MAX}");
                }


                frame.Content = rowList;
                tabItem.Content = frame;
                PART_TabItem.Items.Add(tabItem);
            }

            System.IO.Directory.CreateDirectory("./gettering/");

            bool exist = false;
            if(File.Exists("./gettering/getteringALLData.datas"))
                exist = true;

            using (StreamWriter writer = new StreamWriter("./gettering/getteringALLData.datas", true, Encoding.UTF8))
            {
                if (exist is false)
                {
                    writer.WriteLine("id,start_date,end_date,cpu_use_min,cpu_use_avg,cpu_use_max,cpu_temp_min,cpu_temp_avg,cpu_temp_max,cpu_pow_min,cpu_pow_avg,cpu_pow_max");
                }
                writer.WriteLine(getterAllDatas);
            }
            
            string lastFolder = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(getRootDirectory + "/"));
            using (StreamWriter writer = new StreamWriter($"./gettering/{lastFolder}.datas", false, Encoding.UTF8))
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
                    //SplitStart = new DateTime(2024, 11, 25, 01, 00, 00),
                    //SplitEnd = new DateTime(2024, 11, 25, 10, 00, 00)

                    SplitStart = new DateTime(2024, 11, 25, 09, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 25, 18, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "2",
                new()
                {
                    SplitName = "2",
                    //SplitStart = new DateTime(2024, 11, 25, 10, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 26, 01, 00, 00)

                    SplitStart = new DateTime(2024, 11, 25, 18, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 26, 09, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "3",
                new()
                {
                    SplitName = "3",
                    //SplitStart = new DateTime(2024, 11, 26, 01, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 26, 10, 00, 00)

                    SplitStart = new DateTime(2024, 11, 26, 09, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 26, 18, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "4",
                new()
                {
                    SplitName = "4",
                    //SplitStart = new DateTime(2024, 11, 26, 10, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 27, 01, 00, 00)

                    SplitStart = new DateTime(2024, 11, 26, 18, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 27, 09, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "5",
                new()
                {
                    SplitName = "5",
                    //SplitStart = new DateTime(2024, 11, 27, 01, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 27, 10, 00, 00)

                    SplitStart = new DateTime(2024, 11, 27, 09, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 27, 18, 00, 00)
                });




        }
    }
}
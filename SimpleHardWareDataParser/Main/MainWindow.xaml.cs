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
                    var startDateTime = RawdataRecordManager.RawDataSplitInfo[item].SplitStart;
                    var endDateTime = RawdataRecordManager.RawDataSplitInfo[item].SplitEnd;
                    if (mainWindowViewmodel is not null)
                    {
                        startDateTime = startDateTime.Add(mainWindowViewmodel.Setting.TimeZoneDifference);
                        endDateTime = endDateTime.Add(mainWindowViewmodel.Setting.TimeZoneDifference);
                    }


                    getterAllDatas = $"{"TEMP"},{startDateTime.ToString("yyyy-MM-dd HH:mm")},{endDateTime.ToString("yyyy-MM-dd HH:mm")},{rowList.CPU_USE_MIN},{rowList.CPU_USE_AVG},{rowList.CPU_USE_MAX},{rowList.CPU_TEMP_MIN},{rowList.CPU_TEMP_AVG},{rowList.CPU_TEMP_MAX},{rowList.CPU_POW_MIN},{rowList.CPU_POW_AVG},{rowList.CPU_POW_MAX}";
                }
                else
                {
                    var startDateTime = RawdataRecordManager.RawDataSplitInfo[item].SplitStart;
                    var endDateTime = RawdataRecordManager.RawDataSplitInfo[item].SplitEnd;
                    if (mainWindowViewmodel is not null)
                    {
                        startDateTime = startDateTime.Add(mainWindowViewmodel.Setting.TimeZoneDifference);
                        endDateTime = endDateTime.Add(mainWindowViewmodel.Setting.TimeZoneDifference);
                    }

                    getterDatas.Add($"{"TEMP"},{endDateTime.ToString("yyyy-MM-dd HH:mm")},{rowList.CPU_USE_MIN},{rowList.CPU_USE_AVG},{rowList.CPU_USE_MAX},{rowList.CPU_TEMP_MIN},{rowList.CPU_TEMP_AVG},{rowList.CPU_TEMP_MAX},{rowList.CPU_POW_MIN},{rowList.CPU_POW_AVG},{rowList.CPU_POW_MAX}");
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
                    SplitStart = new DateTime(2024, 11, 28, 10, 00, 00),
                    SplitEnd = new DateTime(2024, 11, 29, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 28, 18, 00, 00),
                    //SplitEnd = new DateTime(2024, 11, 29, 09, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "2",
                new()
                {
                    SplitName = "2",
                    SplitStart = new DateTime(2024, 11, 29, 01, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 29, 10, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 29, 09, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 29, 18, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "3",
                new()
                {
                    SplitName = "3",
                    SplitStart = new DateTime(2024, 11, 29, 10, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 30, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 29, 18, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 30, 09, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "4",
                new()
                {
                    SplitName = "4",
                    SplitStart = new DateTime(2024, 11, 30, 01, 00, 01),
                    SplitEnd = new DateTime(2024, 11, 30, 10, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 30, 09, 00, 01),
                    //SplitEnd = new DateTime(2024, 11, 30, 18, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "5",
                new()
                {
                    SplitName = "5",
                    SplitStart = new DateTime(2024, 11, 30, 10, 00, 01),
                    SplitEnd = new DateTime(2024, 12, 01, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 11, 30, 18, 00, 01),
                    //SplitEnd = new DateTime(2024, 12, 01, 09, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "6",
                new()
                {
                    SplitName = "6",
                    SplitStart = new DateTime(2024, 12, 01, 01, 00, 01),
                    SplitEnd = new DateTime(2024, 12, 01, 10, 00, 00)

                    //SplitStart = new DateTime(2024, 12, 01, 09, 00, 01),
                    //SplitEnd = new DateTime(2024, 12, 01, 18, 00, 00)
                });
            RawdataRecordManager.RawDataSplitInfo.Add(
                "7",
                new()
                {
                    SplitName = "7",
                    SplitStart = new DateTime(2024, 12, 01, 10, 00, 01),
                    SplitEnd = new DateTime(2024, 12, 02, 01, 00, 00)

                    //SplitStart = new DateTime(2024, 12, 01, 18, 00, 01),
                    //SplitEnd = new DateTime(2024, 12, 02, 09, 00, 00)
                });


        }
    }
}
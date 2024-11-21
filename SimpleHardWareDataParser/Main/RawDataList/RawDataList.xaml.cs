using SimpleHardWareDataParser.Rawdata;
using SimpleOverlayTheme.ThemeSystem;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleHardWareDataParser.Main.RawDataList
{
    /// <summary>
    /// RawDataList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RawDataList : UserControl
    {
        public string CPU_USE_MIN { get; private set; }
        public string CPU_USE_AVG { get; private set; }
        public string CPU_USE_MAX { get; private set; }
        public string CPU_POW_MIN { get; private set; }
        public string CPU_POW_AVG { get; private set; }
        public string CPU_POW_MAX { get; private set; }
        public string CPU_TEMP_MIN { get; private set; }
        public string CPU_TEMP_AVG { get; private set; }
        public string CPU_TEMP_MAX { get; private set; }

        public RawDataList()
        {
            InitializeComponent();
            SplitInfo = new();
            Data = new();
        }

        internal RawDataList(RawdataSplitInfo splitInfo, RawdataRecordDictionary data)
        {
            //internal RawdataSplitInfo
            InitializeComponent();
            SplitInfo = splitInfo;
            Data = data;
            UpdateGridList();
        }

        internal void SetData(RawdataSplitInfo splitInfo, RawdataRecordDictionary data)
        {
            SplitInfo = splitInfo;
            Data = data;
        }

        internal void UpdateGridList()
        {
            if (RawdataRecordManager.Data.ContainsKey(SplitInfo.SplitName) is false)
                return;







            List<float> cpuUseList = [];
            List<float> cpuPowList = [];
            List<float> cpuTempList = [];

            List<dynamic> dynamicDataTable = new();
            foreach (var item in RawdataRecordManager.Data[SplitInfo.SplitName].Values)
            {
                dynamic dynamicRecord = new ExpandoObject();
                dynamicRecord.DateTime = item.DateTime.ToString("yy.MM.dd HH:mm:ss");
                dynamicRecord.CpuUse = $"{item.CpuUse:F1}";
                dynamicRecord.CpuPower = $"{item.CpuPower:F1}";
                dynamicRecord.CpuTemperature = $"{item.CpuTemperature:F1}";

                if (item.CpuUse is not 0.0f)
                    cpuUseList.Add(item.CpuUse);
                if (item.CpuPower is not 0.0f)
                    cpuPowList.Add(item.CpuPower);
                if (item.CpuTemperature is not 0.0f)
                    cpuTempList.Add(item.CpuTemperature);

                dynamicDataTable.Add(dynamicRecord);
            }
            PART_DataGrid.Columns.Clear();
            PART_DataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "DateTime",
                Binding = new Binding("DateTime"),
                IsReadOnly = true,
                CanUserReorder = false,
                EditingElementStyle = (Style)FindResource("SimpleOverlayTheme.TextBox.Default")

            });

            PART_DataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "CPU Use(%)",
                Binding = new Binding("CpuUse"),
                EditingElementStyle = (Style)FindResource("SimpleOverlayTheme.TextBox.Default")
            });

            PART_DataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "CPU Pow(W)",
                Binding = new Binding("CpuPower"),
                EditingElementStyle = (Style)FindResource("SimpleOverlayTheme.TextBox.Default")
            });

            PART_DataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "CPU Temp(°C)",
                Binding = new Binding("CpuTemperature"),
                EditingElementStyle = (Style)FindResource("SimpleOverlayTheme.TextBox.Default")
            });

            // DataGrid에 데이터 바인딩
            PART_DataGrid.ItemsSource = dynamicDataTable;
            if (cpuUseList.Count is not 0)
            {
                CPU_USE_MIN = $"{cpuUseList.Min():F1}";
                CPU_USE_AVG = $"{cpuUseList.Average():F1}";
                CPU_USE_MAX = $"{ cpuUseList.Max():F1}";
                Part_CpuUseAvg.Content = $"Use Avg: {cpuUseList.Average():F1} %";
                Part_CpuUseMin.Content = $"Use Min: {cpuUseList.Min():F1} %";
                Part_CpuUseMax.Content = $"Use Max: {cpuUseList.Max():F1} %";
            }
            else
            {
                CPU_USE_MIN = $"";
                CPU_USE_AVG = $"";
                CPU_USE_MAX = $"";
                Part_CpuUseAvg.Content = $"Use Avg: N/A";
                Part_CpuUseMin.Content = $"Use Min: N/A";
                Part_CpuUseMax.Content = $"Use Max: N/A";
            }

            if (cpuPowList.Count is not 0)
            {
                CPU_POW_MIN = $"{cpuPowList.Min():F1}";
                CPU_POW_AVG = $"{cpuPowList.Average():F1}";
                CPU_POW_MAX = $"{cpuPowList.Max():F1}";
                Part_CpuPowAvg.Content = $"Pow Avg: {cpuPowList.Average():F1} W";
                Part_CpuPowMin.Content = $"Pow Min: {cpuPowList.Min():F1} W";
                Part_CpuPowMax.Content = $"Pow Max: {cpuPowList.Max():F1} W";
            }
            else
            {
                CPU_POW_MIN = $"";
                CPU_POW_AVG = $"";
                CPU_POW_MAX = $"";
                Part_CpuPowAvg.Content = $"Pow Avg: N/A";
                Part_CpuPowMin.Content = $"Pow Min: N/A";
                Part_CpuPowMax.Content = $"Pow Max: N/A";
            }

            if (cpuTempList.Count is not 0)
            {
                CPU_TEMP_MIN = $"{cpuTempList.Min():F1}";
                CPU_TEMP_AVG = $"{cpuTempList.Average():F1}";
                CPU_TEMP_MAX = $"{cpuTempList.Max():F1}";
                Part_CpuTempAvg.Content = $"Temp Avg: {cpuTempList.Average():F1} °C";
                Part_CpuTempMin.Content = $"Temp Min: {cpuTempList.Min():F1} °C";
                Part_CpuTempMax.Content = $"Temp Max: {cpuTempList.Max():F1} °C";
            }
            else
            {
                CPU_TEMP_MIN = $"";
                CPU_TEMP_AVG = $"";
                CPU_TEMP_MAX = $"";
                Part_CpuTempAvg.Content = $"Temp Avg: N/A";
                Part_CpuTempMin.Content = $"Temp Min: N/A";
                Part_CpuTempMax.Content = $"Temp Max: N/A";
            }

            

            



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public partial class RawDataList
    {
        public RawdataSplitInfo SplitInfo { get; private set; }
        public RawdataRecordDictionary Data { get; private set; }



    }
}

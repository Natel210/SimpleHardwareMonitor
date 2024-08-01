using System.Windows.Controls;
using System.Windows;

namespace SimpleHardwareMonitorGUI.Items
{
    /// <summary>
    /// CpuCategory.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CpuCategory : UserControl
    {
        public CpuCategory()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UsedDataProperty =
            DependencyProperty.Register("UsedData", typeof(string), typeof(CpuCategory), new PropertyMetadata("000.0"));

        public string UsedData
        {
            get { return (string)GetValue(UsedDataProperty); }
            set { SetValue(UsedDataProperty, value); }
        }

        public static readonly DependencyProperty TemperatureDataProperty =
            DependencyProperty.Register("TemperatureData", typeof(string), typeof(CpuCategory), new PropertyMetadata("000.0"));

        public string TemperatureData
        {
            get { return (string)GetValue(TemperatureDataProperty); }
            set { SetValue(TemperatureDataProperty, value); }
        }

        public static readonly DependencyProperty PowerDataProperty =
            DependencyProperty.Register("PowerData", typeof(string), typeof(CpuCategory), new PropertyMetadata("000.0"));

        public string PowerData
        {
            get { return (string)GetValue(PowerDataProperty); }
            set { SetValue(PowerDataProperty, value); }
        }
    }
}
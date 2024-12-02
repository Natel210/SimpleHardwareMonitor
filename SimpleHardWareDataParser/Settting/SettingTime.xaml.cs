using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SimpleHardWareDataParser.Settting
{
    /// <summary>
    /// SettingTime.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingTime : UserControl
    {
        public SettingTime()
        {
            InitializeComponent();

            var a = TimeZoneInfo.GetSystemTimeZones();
            //TimeZoneInfo.Local
            //foreach (var t in a)
            //{
            //    t.DisplayName
            //}
            SrcUTC.ItemsSource = a;
            DestUTC.ItemsSource = a;
        }
    }
}

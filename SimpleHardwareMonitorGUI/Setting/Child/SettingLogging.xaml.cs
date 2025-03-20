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

namespace SimpleHardwareMonitorGUI.Setting.Child
{
    /// <summary>
    /// SettingLogging.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingLogging : UserControl
    {
        public SettingLogging()
        {
            InitializeComponent();
        }

        private void PART_SettingLogging_Load(object sender, RoutedEventArgs e)
        {

        }

        private void PART_LoggingAutoCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateLoggingAutoCheck();
        }
    }

    //internal
    public partial class SettingLogging : UserControl
    {
        private void UpdateLoggingAutoCheck()
        {
            //if (PART_LoggingAutoCheck is null)
            //    return;
            //if (PART_LoggingAutoCheck.IsChecked is true)
            //    PART_LoggingAutoCheck_LabelText.Text = "Check";
            //else
            //    PART_LoggingAutoCheck_LabelText.Text = "UnCheck";
        }
    }
}

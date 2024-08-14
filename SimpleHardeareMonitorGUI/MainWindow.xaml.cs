using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleHardwareMonitor;
using SimpleHardwareMonitor.data;
using SimpleHardwareMonitor.viewmodel;
using SimpleHardwareMonitorGUI.Items;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleHardwareMonitorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }


    }
}
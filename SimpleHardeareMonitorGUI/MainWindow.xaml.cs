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
using static System.Net.Mime.MediaTypeNames;

namespace SimpleHardwareMonitorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private MonitorInterface _monitorInterface;
        private HardwareMonitorViewmodel _cpuViewmodel;
        private Timer updateTimer;

        public MainWindow()
        {
            InitializeComponent();
            //HardwareMonitor.Initialized();
            _cpuViewmodel = new HardwareMonitorViewmodel(SynchronizationContext.Current);
            DataContext = _cpuViewmodel;
            updateTimer = new Timer(_ => { _cpuViewmodel.UpdateData(); }, this, 0, 250);
            //_monitorInterface = new MonitorInterface(SynchronizationContext.Current);
            //DataContext = _monitorInterface;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //HardwareMonitor.Release();
        }
    }
}
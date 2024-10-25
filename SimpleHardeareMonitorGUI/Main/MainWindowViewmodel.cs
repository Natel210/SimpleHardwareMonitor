using SimpleHardwareMonitor;
using SimpleHardwareMonitorGUI.Common;
using SimpleHardwareMonitorGUI.Rawdata;

namespace SimpleHardwareMonitorGUI.Main
{
    public partial class MainWindowViewmodel : AViewModelBase_None
    {

        public MainWindowViewmodel()
        {
            _hardwareMonitorViewmodel = HardwareMonitorVM.instance ?? throw new ArgumentNullException(nameof(HardwareMonitorVM.instance));
            _rawdataViewmodel = RawdataViewmodel.instance ?? throw new ArgumentNullException(nameof(RawdataViewmodel.instance));
        }

        ~MainWindowViewmodel()
        {
        }


        public HardwareMonitorVM HW
        {
            get => _hardwareMonitorViewmodel;
            set => Set(ref _hardwareMonitorViewmodel, value, nameof(HW));
        }

        public RawdataViewmodel RawData
        {
            get => _rawdataViewmodel;
            set => Set(ref _rawdataViewmodel, value, nameof(RawData));
        }

        public string TitleName
        {
            get => _titleName;
            set => Set(ref _titleName, value, nameof(TitleName));
        }
    }


    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        private HardwareMonitorVM _hardwareMonitorViewmodel;
        private RawdataViewmodel _rawdataViewmodel;
        private string _titleName = "HardWare Monitor";
    }
}

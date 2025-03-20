using SimpleHardwareMonitor;
using SimpleHardwareMonitorGUI.Common;
using SimpleHardwareMonitorGUI.Model;
using SimpleHardwareMonitorGUI.Setting;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleHardwareMonitorGUI.Main
{
    public partial class MainWindowViewmodel : INotifyPropertyChanged
    {

        public MainWindowViewmodel()
        {
            _hardwareMonitorViewmodel = HardwareMonitorVM.instance ?? throw new ArgumentNullException(nameof(HardwareMonitorVM.instance));
            //_rawdataViewmodel = RawdataViewmodel.instance ?? throw new ArgumentNullException(nameof(RawdataViewmodel.instance));
            //RawData.TitleName = GlobalModel.Instance.CommonData.MainWindowName;
            //RawData.LoggingEnabled = GlobalModel.Instance.RawLoggingData.EnableAutoSave_ProgramStartup;
            //RawData.LoggingInterval = GlobalModel.Instance.RawLoggingData.LoggingInterval;
        }

        ~MainWindowViewmodel()
        {
        }


        public HardwareMonitorVM HW
        {
            get => _hardwareMonitorViewmodel;
            set => Set(ref _hardwareMonitorViewmodel, value, nameof(HW));
        }

        //public RawdataViewmodel RawData
        //{
        //    get => _rawdataViewmodel;
        //    set => Set(ref _rawdataViewmodel, value, nameof(RawData));
        //}

        public string TitleName
        {
            get => GlobalModel.Instance.CommonData.MainWindowName;
            set {
                string _titleName = GlobalModel.Instance.CommonData.MainWindowName;
                if(Set(ref _titleName, value, nameof(TitleName)))
                {
                    GlobalModel.Instance.CommonData.MainWindowName = value;
                    //RawData.TitleName = value;
                }
            }
                
        }




    }
    public partial class MainWindowViewmodel
    {
    }

    public partial class MainWindowViewmodel
    {
        private HardwareMonitorVM _hardwareMonitorViewmodel;
        //private RawdataViewmodel _rawdataViewmodel;
    }

    //INotifyPropertyChanged
    public partial class MainWindowViewmodel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null) where T : notnull
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

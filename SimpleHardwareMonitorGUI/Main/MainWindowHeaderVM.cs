using SimpleHardwareMonitorGUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardwareMonitorGUI.Main
{
    public partial class MainWindowHeaderVM : INotifyPropertyChanged
    {
        private GlobalModel _model => GlobalModel.Instance;

        public MainWindowHeaderVM()
        {
            _model.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            _model.CommonData.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            _model.RawLoggingData.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
        }

        public string MainWindowName
        {
            get => _model.CommonData.MainWindowName;
            set => _model.CommonData.MainWindowName = value;
        }

        public bool IsLoggingRunning
        {
            get => _model.RawLoggingData.IsLoggingRunning;
            set => _model.RawLoggingData.IsLoggingRunning = value;
        }

        public Model.Child.RawLoggingItem.Interval LoggingInterval
        {
            get => GlobalModel.Instance.RawLoggingData.LoggingInterval;
            set => GlobalModel.Instance.RawLoggingData.LoggingInterval = value;
        }
    }

    //INotifyPropertyChanged
    public partial class MainWindowHeaderVM
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using SimpleHardwareMonitorGUI.Model;
using SimpleHardwareMonitorGUI.Model.Child.RawLoggingItem;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleHardwareMonitorGUI.Setting.Child
{
    class SettingSummaryVM : INotifyPropertyChanged
    {
        private GlobalModel _model => GlobalModel.Instance;

        public SettingSummaryVM()
        {
            _model.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            _model.CommonData.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            _model.HardwareMonitorData.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            _model.RawLoggingData.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
            _model.ThemeData.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
        }

        public string MainWindowName
        {
            get => _model.CommonData.MainWindowName;
            set => _model.CommonData.MainWindowName = value;
        }

        public string CurrentTheme
        {
            get => _model.ThemeData.CurrentTheme;
            set => _model.ThemeData.CurrentTheme = value;
        }

        public Interval LoggingInterval
        {
            get => _model.RawLoggingData.LoggingInterval;
            set => _model.RawLoggingData.LoggingInterval = value;
        }

        public bool EnableAutoSave_ProgramStartup
        {
            get => _model.RawLoggingData.EnableAutoSave_ProgramStartup;
            set => _model.RawLoggingData.EnableAutoSave_ProgramStartup = value;
        }

        public bool IsLoggingRunning
        {
            get => _model.RawLoggingData.IsLoggingRunning;
            set => _model.RawLoggingData.IsLoggingRunning = value;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

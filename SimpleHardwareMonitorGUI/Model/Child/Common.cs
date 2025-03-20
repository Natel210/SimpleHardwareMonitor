using SimpleHardwareMonitorGUI.Common.SyncThread;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleHardwareMonitorGUI.Model.Child
{
    public partial class Common : INotifyPropertyChanged
    {
        internal string MainWindowName {
            get => _mainWindowName.Value;
            set
            {
                if (EqualityComparer<string>.Default.Equals(_mainWindowName.Value, value))
                    return;
                _mainWindowName.Value = value;
                OnPropertyChanged(nameof(MainWindowName));
            }
        }
        // Member
        private ISync<string> _mainWindowName = new SyncString(mainWindowNameDefault);
        // Default
        internal const string mainWindowNameDefault = "HardWare Monitor";


        internal Dictionary<string, bool> SettingSummaryFold { get; set; } = new();






    }

    public partial class Common : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

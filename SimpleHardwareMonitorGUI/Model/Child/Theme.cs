using SimpleHardwareMonitorGUI.Common.SyncThread;
using System.ComponentModel;
using System.Linq;

namespace SimpleHardwareMonitorGUI.Model.Child
{
    public partial class Theme : INotifyPropertyChanged
    {
        public string CurrentTheme
        {
            get => _currentTheme.Value;
            set
            {
                if (SimpleOverlayTheme.ThemeSystem.Manager.GetThemeNameList().Contains(value) is false)
                    return;
                if (EqualityComparer<string>.Default.Equals(_currentTheme.Value, value))
                    return;
                _currentTheme.Value = value;
                SimpleOverlayTheme.ThemeSystem.Manager.CurrentThemeName = value;
                OnPropertyChanged(nameof(CurrentTheme));
            }
        }
        // Member
        private ISync<string> _currentTheme = new SyncString(currentThemeDefault);
        // Default
        internal const string currentThemeDefault = "Light";
    }

    public partial class Theme
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


using System.ComponentModel;

namespace SimpleHardwareMonitorGUI.Model.Child
{
    public class HardwareMonitor : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

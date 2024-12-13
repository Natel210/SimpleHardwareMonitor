using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class CpuVM : INotifyPropertyChanged
    {
        public static CpuVM instance = new CpuVM();

        public string Name
        {
            get => _name;
            internal set => Set(ref _name, value);
        }

        public int CoreCount
        {
            get => _coreCount;
            internal set => Set(ref _coreCount, value);
        }
        public int ProcessorCount
        {
            get => _processorCount;
            internal set => Set(ref _processorCount, value);
        }
        public float Use
        {
            get => _use;
            internal set => Set(ref _use, value);
        }
        public ObservableCollection<float> UseByThreads
        {
            get => _useByThreads;
            internal set => Set(ref _useByThreads, value);
        }
        public float Voltage
        {
            get => _voltage;
            internal set => Set(ref _voltage, value);
        }
        public ObservableCollection<float> VoltageByCore
        {
            get => _voltageByCore;
            internal set => Set(ref _voltageByCore, value);
        }
        public float Power
        {
            get => _power;
            internal set => Set(ref _power, value);
        }
        public ObservableCollection<float> PowerByCore
        {
            get => _powerByCore;
            internal set => Set(ref _powerByCore, value);
        }
        public float Temperature
        {
            get => _temperature;
            internal set => Set(ref _temperature, value);
        }
        public ObservableCollection<float> TemperatureyByCore
        {
            get => _temperatureByCore;
            internal set => Set(ref _temperatureByCore, value);
        }
    }
    public partial class CpuVM : INotifyPropertyChanged
    {
        private string _name;
        private int _coreCount;
        private int _processorCount;
        private float _use;
        private ObservableCollection<float> _useByThreads = new ObservableCollection<float>();
        private float _voltage;
        private ObservableCollection<float> _voltageByCore = new ObservableCollection<float>();
        private float _power;
        private ObservableCollection<float> _powerByCore = new ObservableCollection<float>();
        private float _temperature;
        private ObservableCollection<float> _temperatureByCore = new ObservableCollection<float>();
        private readonly SynchronizationContext _syncContext;
        public event PropertyChangedEventHandler PropertyChanged;
        private CpuVM() { _syncContext = SynchronizationContext.Current; }
        private bool Set<T>(ref T field, T newValue = default(T), [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

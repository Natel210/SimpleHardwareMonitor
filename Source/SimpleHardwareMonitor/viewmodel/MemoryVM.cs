using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class MemoryVM : INotifyPropertyChanged
    {
        public static MemoryVM instance = new MemoryVM();
        private float _use;
        public float Use
        {
            get => _use;
            internal set => Set(ref _use, value);
        }
        private float _value;
        /// <summary>
        /// actually displayed in TaskMng.
        /// </summary>
        public float Value
        {
            get => _value;
            internal set => Set(ref _value, value);
        }
        private float _available;
        public float Available
        {
            get => _available;
            internal set => Set(ref _available, value);
        }
        private float _virtualUse;
        public float VirtualUse
        {
            get => _virtualUse;
            internal set => Set(ref _virtualUse, value);
        }
        private float _virtualValue;
        public float VirtualValue
        {
            get => _virtualValue;
            internal set => Set(ref _virtualValue, value);
        }
        private float _virtualAvailable;
        public float VirtualAvailable
        {
            get => _virtualAvailable;
            internal set => Set(ref _virtualAvailable, value);
        }
    }

    public partial class MemoryVM : INotifyPropertyChanged
    {
        private readonly SynchronizationContext _syncContext;
        public event PropertyChangedEventHandler PropertyChanged;
        private MemoryVM() { _syncContext = SynchronizationContext.Current; }
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

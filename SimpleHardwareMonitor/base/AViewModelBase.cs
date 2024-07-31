using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SimpleHardwareMonitor.@base
{
    public abstract class AViewModelBase : INotifyPropertyChanged
    {
        protected readonly SynchronizationContext _syncContext;
        protected AViewModelBase(SynchronizationContext syncContext)
        {
            _syncContext = syncContext ?? SynchronizationContext.Current;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(ref T field, T newValue = default(T), [CallerMemberName] string propertyName = null)
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
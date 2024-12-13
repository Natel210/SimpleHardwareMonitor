using SimpleHardwareMonitor.data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class StorageVM : INotifyPropertyChanged
    {
        public static StorageVM instance = new StorageVM();

        public ObservableCollection<StorageDataNameItem> StorageDataNameItems
        {
            get => _storageDataNameItems;
            internal set => Set(ref _storageDataNameItems, value);
        }
    }


    public partial class StorageVM : INotifyPropertyChanged
    {
        private ObservableCollection<StorageDataNameItem> _storageDataNameItems = new ObservableCollection<StorageDataNameItem>();

        private readonly SynchronizationContext _syncContext;
        public event PropertyChangedEventHandler PropertyChanged;
        private StorageVM() { _syncContext = SynchronizationContext.Current; }
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

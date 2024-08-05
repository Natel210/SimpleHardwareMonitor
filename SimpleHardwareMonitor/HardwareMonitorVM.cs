using HidSharp.Utility;
using LibreHardwareMonitor.Hardware.Motherboard;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor
{
    /// <summary>
    /// hardware monitor view models unity class.
    /// </summary>
    public partial class HardwareMonitorVM : INotifyPropertyChanged
    {
        public static HardwareMonitorVM instance = new HardwareMonitorVM();

        public bool Runing
        {
            get => HardwareMonitor.Runing;
            internal set {
                OnPropertyChanged(nameof(Runing));
            }
        }

        public int UpdateInterval
        {
            get => HardwareMonitor.UpdateInterval;
            set
            {
                HardwareMonitor.SetUpdateInterval(value);
                OnPropertyChanged(nameof(UpdateInterval));
            }
        }
        public MotherboardVM Motherboard
        {
            get => MotherboardVM.instance;
            private set => Set(ref MotherboardVM.instance, value);
        }
        public SuperIOVM SuperIO
        {
            get => SuperIOVM.instance;
            private set => Set(ref SuperIOVM.instance, value);
        }
        public CpuVM Cpu
        {
            get => CpuVM.instance;
            private set => Set(ref CpuVM.instance, value);
        }
        public MemoryVM Memory
        {
            get => MemoryVM.instance;
            private set => Set(ref MemoryVM.instance, value);
        }
        public GpuAmdVM GpuAmd
        {
            get => GpuAmdVM.instance;
            private set => Set(ref GpuAmdVM.instance, value);
        }
        public GpuIntelVM GpuIntel
        {
            get => GpuIntelVM.instance;
            private set => Set(ref GpuIntelVM.instance, value);
        }
        public GpuNvidiaVM GpuNvidia
        {
            get => GpuNvidiaVM.instance;
            private set => Set(ref GpuNvidiaVM.instance, value);
        }
        public StorageVM Storage
        {
            get => StorageVM.instance;
            private set => Set(ref StorageVM.instance, value);
        }
        public CoolerVM Cooler
        {
            get => CoolerVM.instance;
            private set => Set(ref CoolerVM.instance, value);
        }
        public EmbeddedControllerVM EmbeddedController
        {
            get => EmbeddedControllerVM.instance;
            private set => Set(ref EmbeddedControllerVM.instance, value);
        }
        public PsuVM Psu
        {
            get => PsuVM.instance;
            private set => Set(ref PsuVM.instance, value);
        }
        public BatteryVM Battery
        {
            get => BatteryVM.instance;
            private set => Set(ref BatteryVM.instance, value);
        }

    }

    public partial class HardwareMonitorVM : INotifyPropertyChanged
    {
        private readonly SynchronizationContext _syncContext;
        public event PropertyChangedEventHandler PropertyChanged;
        private HardwareMonitorVM() { _syncContext = SynchronizationContext.Current; }
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

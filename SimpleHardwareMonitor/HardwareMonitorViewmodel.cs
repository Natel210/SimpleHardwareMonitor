using HidSharp.Utility;
using LibreHardwareMonitor.Hardware.Motherboard;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.viewmodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor
{
    /// <summary>
    /// hardware monitor view models unity class.
    /// </summary>
    public partial class HardwareMonitorViewmodel : AHardwareMonitorViewmodel
    {
        public bool Runing
        {
            get => HardwareMonitor.Runing;
            private set
            {
                if (EqualityComparer<bool>.Default.Equals(HardwareMonitor.Runing, value))
                    return;
                OnPropertyChanged(null);
                return;
            }
        }
        public int UpdateInterval
        {
            get => HardwareMonitor.UpdateInterval;
            set
            {
                if (EqualityComparer<int>.Default.Equals(HardwareMonitor.UpdateInterval, value))
                    return;
                HardwareMonitor.UpdateInterval = value;
                OnPropertyChanged(null);
                _updateTimer.Change(0, HardwareMonitor.UpdateInterval);
                return;
            }
        }
        public MotherboardViewmodel Motherboard
        {
            get => _motherboardVM;
            private set => Set(ref _motherboardVM, value);
        }
        public SuperIOViewmodel SuperIO
        {
            get => _superIOVM;
            private set => Set(ref _superIOVM, value);
        }
        public CpuViewmodel Cpu
        {
            get => _cpuVM;
            private set => Set(ref _cpuVM, value);
        }
        public MemoryViewmodel Memory
        {
            get => _memoryVM;
            private set => Set(ref _memoryVM, value);
        }
        public GpuAmdViewmodel GpuAmd
        {
            get => _gpuAmdVM;
            private set => Set(ref _gpuAmdVM, value);
        }
        public GpuIntelViewmodel GpuIntel
        {
            get => _gpuIntelVM;
            private set => Set(ref _gpuIntelVM, value);
        }
        public GpuNvidiaViewmodel GpuNvidia
        {
            get => _gpuNvidiaVM;
            private set => Set(ref _gpuNvidiaVM, value);
        }
        public StorageViewmodel Storage
        {
            get => _storageVM;
            private set => Set(ref _storageVM, value);
        }
        public CoolerViewmodel Cooler
        {
            get => _coolerVM;
            private set => Set(ref _coolerVM, value);
        }
        public EmbeddedControllerViewmodel EmbeddedController
        {
            get => _embeddedControllerVM;
            private set => Set(ref _embeddedControllerVM, value);
        }
        public PsuViewmodel Psu
        {
            get => _psuVM;
            private set => Set(ref _psuVM, value);
        }
        public BatteryViewmodel Battery
        {
            get => _batteryVM;
            private set => Set(ref _batteryVM, value);
        }
    }

    public partial class HardwareMonitorViewmodel : AHardwareMonitorViewmodel
    {
        private MotherboardViewmodel _motherboardVM;
        private SuperIOViewmodel _superIOVM;
        private CpuViewmodel _cpuVM;
        private MemoryViewmodel _memoryVM;
        private GpuAmdViewmodel _gpuAmdVM;
        private GpuIntelViewmodel _gpuIntelVM;
        private GpuNvidiaViewmodel _gpuNvidiaVM;
        private StorageViewmodel _storageVM;
        private CoolerViewmodel _coolerVM;
        private EmbeddedControllerViewmodel _embeddedControllerVM;
        private PsuViewmodel _psuVM;
        private BatteryViewmodel _batteryVM;
        private Timer _updateTimer;
        public HardwareMonitorViewmodel(SynchronizationContext syncContext) : base(syncContext)
        {
            Runing = HardwareMonitor.Runing;

            Motherboard = new MotherboardViewmodel(syncContext);
            SuperIO = new SuperIOViewmodel(syncContext);
            Cpu = new CpuViewmodel(syncContext);
            Memory = new MemoryViewmodel(syncContext);
            GpuAmd = new GpuAmdViewmodel(syncContext);
            GpuIntel = new GpuIntelViewmodel(syncContext);
            GpuNvidia = new GpuNvidiaViewmodel(syncContext);
            Storage = new StorageViewmodel(syncContext);
            Cooler = new CoolerViewmodel(syncContext);
            EmbeddedController = new EmbeddedControllerViewmodel(syncContext);
            Psu = new PsuViewmodel(syncContext);
            Battery = new BatteryViewmodel(syncContext);
            _updateTimer = new Timer(_=> { UpdateData(); }, null, 0, UpdateInterval);
        }

        protected override bool UpdateData_Inner()
        {
            try
            {
                Runing = HardwareMonitor.Runing;
                UpdateInterval = HardwareMonitor.UpdateInterval;
                bool oppositeResult = false;
                oppositeResult |= !Motherboard.UpdateData();
                oppositeResult |= !SuperIO.UpdateData();
                oppositeResult |= !Cpu.UpdateData();
                oppositeResult |= !Memory.UpdateData();
                oppositeResult |= !GpuAmd.UpdateData();
                oppositeResult |= !GpuIntel.UpdateData();
                oppositeResult |= !GpuNvidia.UpdateData();
                oppositeResult |= !Storage.UpdateData();
                oppositeResult |= !Cooler.UpdateData();
                oppositeResult |= !EmbeddedController.UpdateData();
                oppositeResult |= !Psu.UpdateData();
                oppositeResult |= !Battery.UpdateData();
                return !oppositeResult;
            }
            catch (Exception) { return false; }
        }
    }
}

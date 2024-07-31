using SimpleHardwareMonitor.@base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace SimpleHardwareMonitor.viewmodel
{
    public partial class CpuViewmodel : AHardwareMonitorViewmodel
    {
        public int CoreCount
        {
            get => _coreCount;
            private set => Set(ref _coreCount, value);
        }

        public int ProcessorCount
        {
            get => _processorCount;
            private set => Set(ref _processorCount, value);
        }


        public float Use
        {
            get => _use;
            private set => Set(ref _use, value);
        }

        public ObservableCollection<float> UseByThreads
        {
            get => _useByThreads;
            private set => Set(ref _useByThreads, value);
        }

        public float Voltage
        {
            get => _voltage;
            private set => Set(ref _voltage, value);
        }

        public ObservableCollection<float> VoltageByCore
        {
            get => _voltageByCore;
            private set => Set(ref _voltageByCore, value);
        }

        public float Power
        {
            get => _power;
            private set => Set(ref _power, value);
        }

        public ObservableCollection<float> PowerByCore
        {
            get => _powerByCore;
            private set => Set(ref _powerByCore, value);
        }

        public float Temperature
        {
            get => _temperature;
            private set => Set(ref _temperature, value);
        }

        public ObservableCollection<float> TemperatureyByCore
        {
            get => _temperatureByCore;
            private set => Set(ref _temperatureByCore, value);
        }
    }

    public partial class CpuViewmodel : AHardwareMonitorViewmodel
    {
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
        public CpuViewmodel(SynchronizationContext syncContext) : base(syncContext)
        {

        }

        protected override bool UpdateData_Inner()
        {
            CoreCount = HardwareMonitor.Cpu.Data.CoreCount;
            ProcessorCount = HardwareMonitor.Cpu.Data.ProcessorCount;
            Use = HardwareMonitor.Cpu.Data.Use;
            Voltage = HardwareMonitor.Cpu.Data.Voltage;
            Power = HardwareMonitor.Cpu.Data.Power;
            Temperature = HardwareMonitor.Cpu.Data.Temperature;

            Action<List<float>, ObservableCollection<float>> updateDetailInfo = (List<float> src, ObservableCollection<float> dest) => {
                dest.Clear();
                foreach (var item in src)
                    dest.Add(item);
            };
            updateDetailInfo(new List<float>(HardwareMonitor.Cpu.Data.UseByThreads), UseByThreads);

            return true;
        }
    }
}

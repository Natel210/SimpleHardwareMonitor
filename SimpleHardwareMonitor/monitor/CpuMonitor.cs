using System.Diagnostics;
using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.data;
using SimpleHardwareMonitor.common;
using System.Collections.Generic;
using System;
using System.Management;
using System.Collections.ObjectModel;
using SimpleHardwareMonitor.viewmodel;

namespace SimpleHardwareMonitor.monitor
{
    internal partial class CpuMonitor : AHardwareMonitor<CpuData>
    {
        //private PerformanceCounter _usePerformance;
        private PerformanceCounter _usePerformance;
        private List<PerformanceCounter> _usePerformanceByThreads;
        public CpuMonitor(IHardware hardware) : base(hardware) { }
        protected sealed override void Init()
        {
            ManagementObjectSearcher searcherName = new ManagementObjectSearcher("select Name from Win32_Processor");
            foreach (ManagementObject obj in searcherName.Get())
            {
                _data.Name = obj["Name"].ToString();
                break;
            }
            CpuVM.instance.Name = _data.Name;
            _data.CoreCount = GetPhysicalCoreCount();
            CpuVM.instance.CoreCount = _data.CoreCount;
            _data.ProcessorCount = Environment.ProcessorCount;
            CpuVM.instance.ProcessorCount = _data.ProcessorCount;
            _usePerformance = new PerformanceCounter("Processor Information", "% Processor Utility", "0,_Total", true);
            _data.UseByThreads = new List<float>();
            _usePerformanceByThreads = new List<PerformanceCounter>();
            for (int i = 0; i < Environment.ProcessorCount; ++i)
            {
                _usePerformanceByThreads.Add(new PerformanceCounter("Processor Information", "% Processor Utility", $"0,{i}"));
                _data.UseByThreads.Add(0.0f);
            }
            _data.VoltageByCore = new List<float>();
            _data.PowerByCore = new List<float>();
            _data.TemperatureByCore = new List<float>();
        }
        protected sealed override void PrevUpdate()
        {
            _data.Use = _usePerformance.NextValue();
            CpuVM.instance.Use = _data.Use;
            for (int i = 0; i < Environment.ProcessorCount; ++i)
                _data.UseByThreads[i] = _usePerformanceByThreads[i].NextValue();
            CpuVM.instance.UseByThreads = listToObservableCollection(_data.UseByThreads);
            if (_hardware.HardwareType != HardwareType.Cpu)
                return;
        }

        private int GetPhysicalCoreCount()
        {
            int coreCount = 0;
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select NumberOfCores from Win32_Processor"))
                {
                    foreach (ManagementObject item in searcher.Get())
                        coreCount += int.Parse(item["NumberOfCores"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching physical core count: {ex.Message}", ex);
            }
            return coreCount;
        }
        private ObservableCollection<float> listToObservableCollection(List<float> src)
        {
            ObservableCollection<float> temp = new ObservableCollection<float>();
            foreach (var item in src)
                temp.Add(item);
            return temp;
        }
    }


    internal partial class CpuMonitor : AHardwareMonitor<CpuData>
    {
        protected override sealed void Update_Voltage(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Voltage)
                return;
            switch (sensor.Name.ToLower())
            {
                case "cpu core":
                    _data.Voltage = sensor.Value.GetValueOrDefault(0.0f);
                    CpuVM.instance.Voltage = _data.Voltage;
                    break;
                default:
                    break;
            }
        }
        protected override sealed void Update_Current(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Current)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Power(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Power)
                return;
            switch (sensor.Name.ToLower())
            {
                case "cpu package":
                    _data.Power = sensor.Value.GetValueOrDefault(0.0f);
                    CpuVM.instance.Power = _data.Power;
                    break;
                default:
                    break;
            }
        }
        protected override sealed void Update_Clock(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Clock)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Temperature(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Temperature)
                return;
            switch (sensor.Name.ToLower())
            {
                case "cpu package":
                    _data.Temperature = sensor.Value.GetValueOrDefault(0.0f);
                    CpuVM.instance.Temperature = _data.Temperature;
                    break;
                default:
                    break;
            }
        }
        protected override sealed void Update_Load(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Load)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Frequency(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Frequency)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Fan(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Fan)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Flow(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Flow)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Control(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Control)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Level(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Level)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Factor(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Factor)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Data(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Data)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_SmallData(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.SmallData)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Throughput(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Throughput)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_TimeSpan(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.TimeSpan)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Energy(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Energy)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
        protected override sealed void Update_Noise(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Noise)
                return;
            switch (sensor.Name.ToLower())
            {
                default:
                    break;
            }
        }
    }
}

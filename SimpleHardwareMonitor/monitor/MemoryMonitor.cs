using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.common;
using SimpleHardwareMonitor.data;
using SimpleHardwareMonitor.viewmodel;
using System.Diagnostics;
namespace SimpleHardwareMonitor.monitor
{
    internal partial class MemoryMonitor : AHardwareMonitor<MemoryData>
    {
        private PerformanceCounter _usePerformance;
        public MemoryMonitor(IHardware hardware) : base(hardware)
        {
            _usePerformance = new PerformanceCounter("Memory", "% Committed Bytes In Use", true);
        }

        protected sealed override void PrevUpdate()
        {
            //_data.Use = _usePerformance.NextValue();
            //MemoryVM.instance.Use = _data.Use;

        }
    }

    internal partial class MemoryMonitor
    {
        protected override sealed void Update_Voltage(ISensor sensor)
        {
            if (sensor is null || sensor.SensorType != SensorType.Voltage)
                return;
            switch (sensor.Name.ToLower())
            {
                //case "cpu core":
                //    _data.Voltage = sensor.Value.GetValueOrDefault(0.0f);
                //    CpuVM.instance.Voltage = _data.Voltage;
                //    break;
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
                //case "cpu package":
                //    _data.Power = sensor.Value.GetValueOrDefault(0.0f);
                //    CpuVM.instance.Power = _data.Power;
                //    break;
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
                //case "cpu package":
                //    _data.Temperature = sensor.Value.GetValueOrDefault(0.0f);
                //    CpuVM.instance.Temperature = _data.Temperature;
                //    break;
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
                case "memory":
                    _data.Value = sensor.Value.GetValueOrDefault(0.0f);
                    MemoryVM.instance.Value = _data.Value;
                    break;
                case "virtual memory":
                    _data.VirtualValue = sensor.Value.GetValueOrDefault(0.0f);
                    MemoryVM.instance.VirtualValue = _data.VirtualValue;
                    break;
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
                case "memory used":
                    _data.Use = sensor.Value.GetValueOrDefault(0.0f);
                    MemoryVM.instance.Use = _data.Use;
                    break;
                case "memory available":
                    _data.Available = sensor.Value.GetValueOrDefault(0.0f);
                    MemoryVM.instance.Available = _data.Available;
                    break;
                case "virtual memory used":
                    _data.VirtualUse = sensor.Value.GetValueOrDefault(0.0f);
                    MemoryVM.instance.VirtualUse = _data.VirtualUse;
                    break;
                case "virtual memory available":
                    _data.VirtualAvailable = sensor.Value.GetValueOrDefault(0.0f);
                    MemoryVM.instance.VirtualAvailable = _data.VirtualAvailable;
                    break;
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

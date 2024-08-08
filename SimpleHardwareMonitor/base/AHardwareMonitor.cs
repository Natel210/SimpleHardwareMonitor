using System;
using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.viewmodel;

namespace SimpleHardwareMonitor.@base
{
    // interface of surface.
    internal abstract partial class AHardwareMonitor<T> : IDisposable where T : struct
    {
        /// <summary>
        /// get data.
        /// </summary>
        public T Data { get { return _data; } }
        internal AHardwareMonitor(IHardware hardware)
        {
            _hardware = hardware ?? throw new ArgumentNullException(nameof(hardware), "Hardware cannot be null");
            Init();
        }
        /// <summary>
        /// to track whether dispose has been called.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;
        /// <summary>
        /// implementing idisposable pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose_Inner(true);
            GC.SuppressFinalize(this);
        }
        internal void UpdateHardWare()
        {
            _hardware.Update();
            PrevUpdate();
            foreach (var sensor in _hardware.Sensors)
            {
                switch (sensor.SensorType)
                {
                    case SensorType.Voltage:
                        Update_Voltage(sensor);
                        break;
                    case SensorType.Current:
                        Update_Current(sensor);
                        break;
                    case SensorType.Power:
                        Update_Power(sensor);
                        break;
                    case SensorType.Clock:
                        Update_Clock(sensor);
                        break;
                    case SensorType.Temperature:
                        Update_Temperature(sensor);
                        break;
                    case SensorType.Load:
                        Update_Load(sensor);
                        break;
                    case SensorType.Frequency:
                        Update_Frequency(sensor);
                        break;
                    case SensorType.Fan:
                        Update_Fan(sensor);
                        break;
                    case SensorType.Flow:
                        Update_Flow(sensor);
                        break;
                    case SensorType.Control:
                        Update_Control(sensor);
                        break;
                    case SensorType.Level:
                        Update_Level(sensor);
                        break;
                    case SensorType.Factor:
                        Update_Factor(sensor);
                        break;
                    case SensorType.Data:
                        Update_Data(sensor);
                        break;
                    case SensorType.SmallData:
                        Update_SmallData(sensor);
                        break;
                    case SensorType.Throughput:
                        Update_Throughput(sensor);
                        break;
                    case SensorType.TimeSpan:
                        Update_TimeSpan(sensor);
                        break;
                    case SensorType.Energy:
                        Update_Energy(sensor);
                        break;
                    case SensorType.Noise:
                        Update_Noise(sensor);
                        break;
                    default:
                        break;
                }
            }


        }
    }

    // inheritance methods.
    internal abstract partial class AHardwareMonitor<T>
    {
        /// <summary>
        /// data instance.
        /// </summary>
        protected T _data = new T();
        /// <summary>
        /// librehardwaremonitor instance.
        /// </summary>
        protected readonly IHardware _hardware;
        /// <summary>
        /// additional initialization required during creation.
        /// </summary>
        protected virtual void Init() { }
        /// <summary>
        /// method that can be overridden in derived classes to clean up resources.
        /// </summary>
        protected virtual void Dispose(bool disposing) { }
        /// <summary>
        /// update logic to insert values ​​into the actual [T] structure.</br>
        /// and define what needs to be updated in advance
        /// </summary>
        protected virtual void PrevUpdate() { }
        protected virtual void Update_Voltage(ISensor sensor) { }
        protected virtual void Update_Current(ISensor sensor) { }
        protected virtual void Update_Power(ISensor sensor) { }
        protected virtual void Update_Clock(ISensor sensor) { }
        protected virtual void Update_Temperature(ISensor sensor) { }
        protected virtual void Update_Load(ISensor sensor) { }
        protected virtual void Update_Frequency(ISensor sensor) { }
        protected virtual void Update_Fan(ISensor sensor) { }
        protected virtual void Update_Flow(ISensor sensor) { }
        protected virtual void Update_Control(ISensor sensor) { }
        protected virtual void Update_Level(ISensor sensor) { }
        protected virtual void Update_Factor(ISensor sensor) { }
        protected virtual void Update_Data(ISensor sensor) { }
        protected virtual void Update_SmallData(ISensor sensor) { }
        protected virtual void Update_Throughput(ISensor sensor) { }
        protected virtual void Update_TimeSpan(ISensor sensor) { }
        protected virtual void Update_Energy(ISensor sensor) { }
        protected virtual void Update_Noise(ISensor sensor) { }
    }

    // backend components.
    internal abstract partial class AHardwareMonitor<T>
    {
        /// <summary>
        /// to synchronize state changes.
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// dispose internal function.
        /// </summary>
        private void Dispose_Inner(bool disposing)
        {
            if (!IsDisposed)
            {
                lock (_lock)
                {
                    if (!IsDisposed)
                    {
                        if (disposing)
                        {
                            // call the dispose method of the derived class.
                            Dispose(disposing); 
                            // dispose _hardware if it implements idisposable.
                            if (_hardware is IDisposable disposableHardware)
                            {
                                disposableHardware.Dispose();
                            }
                        }
                        IsDisposed = true;
                    }
                }
            }
        }
        ~AHardwareMonitor()
        {
            Dispose_Inner(false);
        }
    }
}
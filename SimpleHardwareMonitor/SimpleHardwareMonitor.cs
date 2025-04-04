using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Hardware.Cpu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleHardwareMonitor
{
    public partial class SimpleHardwareMonitor
    {
        static public SimpleHardwareMonitor Instance { get; } = new SimpleHardwareMonitor();
        public void Start() { _isRun = true; }
        public void End() { _isRun = false; }
        public string SenserTypeToUnitString(SensorType sensorType) { return GetSenserTypeToUnitString(sensorType); }
        public long Interval {
            get => _updateInterval;
            set => _updateInterval = value; }
        public Dictionary<string, Data.Motherboard> Motherboard { get => _motherboard.Data; }
        public Dictionary<string, Data.SuperIO> SuperIO { get => _superIO.Data; }
        public Dictionary<string, Data.Cpu> Cpu { get => _cpu.Data; }
        public Dictionary<string, Data.Memory> Memory { get => _memory.Data; }
        public Dictionary<string, Data.Gpu> Gpu { get => _gpu.Data; }
        public Dictionary<string, Data.Storage> Storage { get => _storage.Data; }
        public Dictionary<string, Data.Network> Network { get => _network.Data; }
        public Dictionary<string, Data.Cooler> Cooler { get => _cooler.Data; }
        public Dictionary<string, Data.EmbeddedController> EmbeddedController { get => _embeddedController.Data; }
        public Dictionary<string, Data.Psu> Psu { get => _psu.Data; }
        public Dictionary<string, Data.Battery> Battery { get => _battery.Data; }
    }



    public partial class SimpleHardwareMonitor
    {
        private SimpleHardwareMonitor()
        {
            Init();
        }

        ~SimpleHardwareMonitor()
        {
            Release();
        }

        private void Init()
        {
            _computer.Open();
            _computer.IsCpuEnabled = true;
            _computer.IsMotherboardEnabled = true;
            _computer.IsMemoryEnabled = true;
            _computer.IsGpuEnabled = true;
            _computer.IsStorageEnabled = true;
            _computer.IsNetworkEnabled = true;
            _computer.IsControllerEnabled = true;
            _computer.IsPsuEnabled = true;
            _computer.IsBatteryEnabled = true;
            FillUpdateHardware();

            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            _updateTask = Task.Run(async () => await Update(cancellationToken), cancellationToken);
        }

        private void Release()
        {
            _cancellationTokenSource.Cancel(); // Cancel the running task
            if (_updateTask.Wait(TimeSpan.FromSeconds(5)) is false) // Wait for the task to complete with timeout
            {
                // If the task did not complete in the given time, forcefully dispose the cancellation token source
                _cancellationTokenSource.Dispose();
                throw new Exception("Update task did not complete in time and was forcefully terminated.");
            }
            else
                _cancellationTokenSource.Dispose();
            _computer.Close();
        }

        ItemList.Motherboard _motherboard = new ItemList.Motherboard();
        ItemList.SuperIO _superIO = new ItemList.SuperIO();
        ItemList.Cpu _cpu = new ItemList.Cpu();
        ItemList.Memory _memory = new ItemList.Memory();
        ItemList.Gpu _gpu = new ItemList.Gpu();
        ItemList.Storage _storage = new ItemList.Storage();
        ItemList.Network _network = new ItemList.Network();
        ItemList.Cooler _cooler = new ItemList.Cooler();
        ItemList.EmbeddedController _embeddedController = new ItemList.EmbeddedController();
        ItemList.Psu _psu = new ItemList.Psu();
        ItemList.Battery _battery = new ItemList.Battery();

        private void FillUpdateHardware()
        {
            foreach (var hardware in _computer.Hardware)
            {
                switch (hardware.HardwareType)
                {
                    case HardwareType.Motherboard:
                        _motherboard.Add(hardware);
                        break;
                    case HardwareType.SuperIO:
                        _superIO.Add(hardware);
                        break;
                    case HardwareType.Cpu:
                        _cpu.Add(hardware);
                        break;
                    case HardwareType.Memory:
                        _memory.Add(hardware);
                        break;
                    case HardwareType.GpuNvidia:
                    case HardwareType.GpuAmd:
                    case HardwareType.GpuIntel:
                        _gpu.Add(hardware);
                        break;
                    case HardwareType.Storage:
                        _storage.Add(hardware);
                        break;
                    case HardwareType.Network:
                        _network.Add(hardware);
                        break;
                    case HardwareType.Cooler:
                        _cooler.Add(hardware);
                        break;
                    case HardwareType.EmbeddedController:
                        _embeddedController.Add(hardware);
                        break;
                    case HardwareType.Psu:
                        _psu.Add(hardware);
                        break;
                    case HardwareType.Battery:
                        _battery.Add(hardware);
                        break;
                    default:
                        break;
                }

            }
            _updateHardwareMethods = new Dictionary<HardwareType, Func<IHardware, bool>>()
            {
                { HardwareType.Motherboard, _motherboard.Update },
                { HardwareType.SuperIO, _superIO.Update },
                { HardwareType.Cpu, _cpu.Update },
                { HardwareType.Memory, _memory.Update },
                { HardwareType.GpuNvidia, _gpu.Update },
                { HardwareType.GpuAmd, _gpu.Update },
                { HardwareType.GpuIntel, _gpu.Update },
                { HardwareType.Storage, _storage.Update },
                //{ HardwareType.Network, _network.Update },
                { HardwareType.Cooler, _cooler.Update },
                { HardwareType.EmbeddedController, _embeddedController.Update },
                { HardwareType.Psu, _psu.Update },
                { HardwareType.Battery, _battery.Update }

            };
        }

        private async Task Update(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (CheckUpdateable() is true)
                        UpdateHardWare();
                    await Task.Yield();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("!!!!");
            }
        }

        private bool CheckUpdateable()
        {
            var newTicks = DateTime.UtcNow.Ticks;
            if (_lastTicks - newTicks > 0)
                return false;
            _lastTicks = newTicks;

            if (_isRun is false)
                return false;
            return true;
        }

        private void UpdateHardWare()
        {
            foreach (var hardware in _computer.Hardware)
            {
                if (_updateHardwareMethods.TryGetValue(hardware.HardwareType, out var updateMethod))
                    updateMethod(hardware);
            }
        }

        private string GetSenserTypeToUnitString(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Voltage:
                    return "V";
                case SensorType.Current:
                    return "A";
                case SensorType.Power:
                    return "W";
                case SensorType.Clock:
                    return "MHz";
                case SensorType.Temperature:
                    return "°C";
                case SensorType.Load:
                    return "%";
                case SensorType.Frequency:
                    return "Hz";
                case SensorType.Fan:
                    return "RPM";
                case SensorType.Flow:
                    return "L/h";
                case SensorType.Control:
                    return "";
                case SensorType.Level:
                    return "%";
                case SensorType.Factor:
                    return "";
                case SensorType.Data:
                    return "GB";
                case SensorType.SmallData:
                    return "MB";
                case SensorType.Throughput:
                    return "Byte/s";
                case SensorType.TimeSpan:
                    return "";
                case SensorType.Energy:
                    return "mWh";
                case SensorType.Noise:
                    return "dBA";
                case SensorType.Conductivity:
                    return "µS/cm";
                case SensorType.Humidity:
                    return "";
                default:
                    return "";
            }
        }

        private bool _isRun = false;

        private long _updateInterval = 500;
        private long _lastTicks = 0;

        private Task _updateTask = null;
        private CancellationTokenSource _cancellationTokenSource = null;

        private Computer _computer = new Computer();
        private Dictionary<HardwareType, Func<IHardware, bool>> _updateHardwareMethods;
    }
}
//public Data.Gpu GpuNvidia { get; }
//public Data.GpuAmd GpuAmd { get; }
//public Data.GpuIntel GpuIntel { get; }
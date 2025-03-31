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
        public Data.Motherboard Motherboard { get; }
        public Data.SuperIO SuperIO { get; }
        public Dictionary<string, Data.Cpu> Cpu { get => _cpu.Data; }
        public Dictionary<string, Data.Memory> Memory { get => _memory.Data; }
        public Dictionary<string, Data.Gpu> Gpu { get => _gpu.Data; }
        public Dictionary<string, Data.Storage> Storage { get; }
        public Data.Network Network { get; }
        public Data.Cooler Cooler { get; }
        public Data.EmbeddedController EmbeddedController { get; }
        public Data.Psu Psu { get; }
        public Data.Battery Battery { get; }
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

        Dictionary<string, Item.Motherboard> _motherboardList = new Dictionary<string, Item.Motherboard>();
        Dictionary<string, Item.SuperIO> _superIOList = new Dictionary<string, Item.SuperIO>();

        ItemList.Cpu _cpu = new ItemList.Cpu();
        ItemList.Memory _memory = new ItemList.Memory();
        ItemList.Gpu _gpu = new ItemList.Gpu();
        ItemList.Storage _storage = new ItemList.Storage();
        Dictionary<string, Item.Network> _networkList = new Dictionary<string, Item.Network>();
        Dictionary<string, Item.Cooler> _coolerList = new Dictionary<string, Item.Cooler>();
        Dictionary<string, Item.EmbeddedController> _embeddedControllerList = new Dictionary<string, Item.EmbeddedController>();
        Dictionary<string, Item.Psu> _psuList = new Dictionary<string, Item.Psu>();
        Dictionary<string, Item.Battery> _batteryList = new Dictionary<string, Item.Battery>();






        private void FillUpdateHardware()
        {
            foreach (var hardware in _computer.Hardware)
            {
                switch (hardware.HardwareType)
                {
                    case HardwareType.Motherboard:
                        _motherboardList.Add(hardware.Name, new Item.Motherboard(hardware.Name, hardware.HardwareType));
                        break;
                    case HardwareType.SuperIO:
                        _superIOList.Add(hardware.Name, new Item.SuperIO(hardware.Name, hardware.HardwareType));
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
                        _networkList.Add(hardware.Name, new Item.Network(hardware.Name, hardware.HardwareType));
                        break;
                    case HardwareType.Cooler:
                        _coolerList.Add(hardware.Name, new Item.Cooler(hardware.Name, hardware.HardwareType));
                        break;
                    case HardwareType.EmbeddedController:
                        _embeddedControllerList.Add(hardware.Name, new Item.EmbeddedController(hardware.Name, hardware.HardwareType));
                        break;
                    case HardwareType.Psu:
                        _psuList.Add(hardware.Name, new Item.Psu(hardware.Name, hardware.HardwareType));
                        break;
                    case HardwareType.Battery:
                        _batteryList.Add(hardware.Name, new Item.Battery(hardware.Name, hardware.HardwareType));
                        break;
                    default:
                        break;
                }

            }
            _updateHardwareMethods = new Dictionary<HardwareType, Func<IHardware, bool>>()
            {
                { HardwareType.Cpu, _cpu.Update },
                { HardwareType.Memory, _memory.Update },
                { HardwareType.GpuNvidia, _gpu.Update },
                { HardwareType.GpuAmd, _gpu.Update },
                { HardwareType.GpuIntel, _gpu.Update },
                { HardwareType.Storage, _storage.Update },


            };
            //_updateHardwareMethods = new Dictionary<HardwareType, Func<IHardware, bool>>() {
            //    { HardwareType.Motherboard, _motherboard.Update },
            //    { HardwareType.SuperIO, _superIO.Update },
            //    { HardwareType.Cpu, _cpu.Update },
            //    { HardwareType.Memory, _memory.Update },
            //    { HardwareType.GpuNvidia, _gpuNvidia.Update },
            //    { HardwareType.GpuAmd, _gpuAmd.Update },
            //    { HardwareType.GpuIntel, _gpuIntel.Update },
            //    { HardwareType.Storage, _storage.Update },
            //    { HardwareType.Network, _network.Update },
            //    { HardwareType.Cooler, _cooler.Update },
            //    { HardwareType.EmbeddedController, _embeddedController.Update },
            //    { HardwareType.Psu, _psu.Update },
            //    { HardwareType.Battery, _battery.Update }
            //};
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
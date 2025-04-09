// MS
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
// Library
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor
{
    /// <summary>
    /// Provides a simplified interface for hardware monitoring.
    /// Singleton-based entry point for accessing hardware data.
    /// </summary>
    public partial class SimpleHardwareMonitor
    {
        /// <summary>
        /// Singleton instance of SimpleHardwareMonitor.
        /// </summary>
        static public SimpleHardwareMonitor Instance { get; } = new SimpleHardwareMonitor();

        /// <summary>
        /// Private constructor to enforce singleton pattern.
        /// </summary>
        private SimpleHardwareMonitor() { }

        /// <summary>
        /// Destructor to ensure resources are released.
        /// </summary>
        ~SimpleHardwareMonitor() { Release(); }

        /// <summary>
        /// Initializes the hardware monitor and starts the background update task.
        /// </summary>
        public void Init()
        {
            _computer.IsCpuEnabled = true;
            _computer.IsMotherboardEnabled = true;
            _computer.IsMemoryEnabled = true;
            _computer.IsGpuEnabled = true;
            _computer.IsStorageEnabled = true;
            _computer.IsNetworkEnabled = true;
            _computer.IsControllerEnabled = true;
            _computer.IsPsuEnabled = true;
            _computer.IsBatteryEnabled = true;
            _computer.Open();
            RegisterUpdateHardware();

            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            _updateTask = Task.Run(async () => await Update(cancellationToken), cancellationToken);
        }

        /// <summary>
        /// Starts the internal update loop.
        /// </summary>
        public void Start() { _isRun = true; }

        /// <summary>
        /// Stops the internal update loop.
        /// </summary>
        public void End() { _isRun = false; }

        /// <summary>
        /// Retrieves SMBIOS information.
        /// </summary>
        /// <returns>SMBios instance if available, otherwise null.</returns>
        public SMBios GetSMBios()
        {
            if (_computer != null)
                return _computer.SMBios;
            return null;
        }

        /// <summary>
        /// Converts a SensorType to its corresponding unit string.
        /// </summary>
        /// <param name="sensorType">Type of the sensor (Voltage, Temperature, etc.)</param>
        /// <returns>String representation of the sensor's unit.</returns>
        public static string SenserTypeToUnitString(SensorType sensorType) { return GetSenserTypeToUnitString(sensorType); }

        /// <summary>
        /// Gets or sets the interval between hardware updates (in milliseconds).
        /// </summary>
        public long Interval { get => _updateInterval; set => _updateInterval = value; }

        /// <summary>
        /// Dictionary of motherboard-related sensor data.
        /// </summary>
        public Dictionary<string, Model.Motherboard> Motherboard { get => _motherboard.ModelGroup; }

        /// <summary>
        /// Dictionary of SuperIO sensor data.
        /// </summary>
        public Dictionary<string, Model.SuperIO> SuperIO { get => _superIO.ModelGroup; }

        /// <summary>
        /// Dictionary of CPU sensor data.
        /// </summary>
        public Dictionary<string, Model.Cpu> Cpu { get => _cpu.ModelGroup; }

        /// <summary>
        /// Dictionary of memory sensor data.
        /// </summary>
        public Dictionary<string, Model.Memory> Memory { get => _memory.ModelGroup; }

        /// <summary>
        /// Dictionary of GPU sensor data.
        /// </summary>
        public Dictionary<string, Model.Gpu> Gpu { get => _gpu.ModelGroup; }

        /// <summary>
        /// Dictionary of storage device sensor data.
        /// </summary>
        public Dictionary<string, Model.Storage> Storage { get => _storage.ModelGroup; }

        /// <summary>
        /// Dictionary of network sensor data.
        /// </summary>
        public Dictionary<string, Model.Network> Network { get => _network.ModelGroup; }

        /// <summary>
        /// Dictionary of cooling system sensor data.
        /// </summary>
        public Dictionary<string, Model.Cooler> Cooler { get => _cooler.ModelGroup; }

        /// <summary>
        /// Dictionary of embedded controller sensor data.
        /// </summary>
        public Dictionary<string, Model.EmbeddedController> EmbeddedController { get => _embeddedController.ModelGroup; }

        /// <summary>
        /// Dictionary of PSU (Power Supply Unit) sensor data.
        /// </summary>
        public Dictionary<string, Model.Psu> Psu { get => _psu.ModelGroup; }

        /// <summary>
        /// Dictionary of battery sensor data.
        /// </summary>
        public Dictionary<string, Model.Battery> Battery { get => _battery.ModelGroup; }
    }

    public partial class SimpleHardwareMonitor
    {
        private bool _isRun = false;
        private long _updateInterval = 500;
        private long _lastTicks = 0;
        private Task _updateTask = null;
        private CancellationTokenSource _cancellationTokenSource = null;

        private Computer _computer = new Computer();
        private Dictionary<HardwareType, Func<IHardware, bool>> _updateHardwareMethods;

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

        /// <summary>
        /// Releases the computer and stops background tasks.
        /// </summary>
        private void Release()
        {
            _cancellationTokenSource.Cancel();
            if (_updateTask.Wait(TimeSpan.FromSeconds(5)) is false)
            {
                _cancellationTokenSource.Dispose();
                throw new Exception("Update task did not complete in time and was forcefully terminated.");
            }
            else
                _cancellationTokenSource.Dispose();

            _computer.Close();
        }

        /// <summary>
        /// Registers the update handlers for each hardware type.
        /// </summary>
        private void RegisterUpdateHardware()
        {
            foreach (var hardware in _computer.Hardware)
            {
                switch (hardware.HardwareType)
                {
                    case HardwareType.Motherboard:
                        _motherboard.AddNodeGroup(hardware);
                        break;
                    case HardwareType.SuperIO:
                        _superIO.AddNodeGroup(hardware);
                        break;
                    case HardwareType.Cpu:
                        _cpu.AddNodeGroup(hardware);
                        break;
                    case HardwareType.Memory:
                        _memory.AddNodeGroup(hardware);
                        break;
                    case HardwareType.GpuNvidia:
                    case HardwareType.GpuAmd:
                    case HardwareType.GpuIntel:
                        _gpu.AddNodeGroup(hardware);
                        break;
                    case HardwareType.Storage:
                        _storage.AddNodeGroup(hardware);
                        break;
                    case HardwareType.Network:
                        _network.AddNodeGroup(hardware);
                        break;
                    case HardwareType.Cooler:
                        _cooler.AddNodeGroup(hardware);
                        break;
                    case HardwareType.EmbeddedController:
                        _embeddedController.AddNodeGroup(hardware);
                        break;
                    case HardwareType.Psu:
                        _psu.AddNodeGroup(hardware);
                        break;
                    case HardwareType.Battery:
                        _battery.AddNodeGroup(hardware);
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
                { HardwareType.Network, _network.Update },
                { HardwareType.Cooler, _cooler.Update },
                { HardwareType.EmbeddedController, _embeddedController.Update },
                { HardwareType.Psu, _psu.Update },
                { HardwareType.Battery, _battery.Update }
            };
        }

        /// <summary>
        /// Asynchronous loop to update hardware data.
        /// </summary>
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
                throw new Exception("An error occurred during the update process.", ex);
            }
        }

        /// <summary>
        /// Determines whether a hardware update should occur based on timing and state.
        /// </summary>
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

        /// <summary>
        /// Calls registered update methods for each hardware item.
        /// </summary>
        private void UpdateHardWare()
        {
            foreach (var hardware in _computer.Hardware)
            {
                if (_updateHardwareMethods.TryGetValue(hardware.HardwareType, out var updateMethod))
                    updateMethod(hardware);
            }
        }

        /// <summary>
        /// Returns the unit string corresponding to a SensorType.
        /// </summary>
        private static string GetSenserTypeToUnitString(SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Voltage: return "V";
                case SensorType.Current: return "A";
                case SensorType.Power: return "W";
                case SensorType.Clock: return "MHz";
                case SensorType.Temperature: return "°C";
                case SensorType.Load: return "%";
                case SensorType.Frequency: return "Hz";
                case SensorType.Fan: return "RPM";
                case SensorType.Flow: return "L/h";
                case SensorType.Control: return "";
                case SensorType.Level: return "%";
                case SensorType.Factor: return "";
                case SensorType.Data: return "GB";
                case SensorType.SmallData: return "MB";
                case SensorType.Throughput: return "Byte/s";
                case SensorType.TimeSpan: return "";
                case SensorType.Energy: return "mWh";
                case SensorType.Noise: return "dBA";
                case SensorType.Conductivity: return "µS/cm";
                case SensorType.Humidity: return "";
                default: return "";
            }
        }
    }
}

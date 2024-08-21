//#define USE_BATTERY
using System.Threading;
using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.monitor;
using SimpleHardwareMonitor.data;
using System;
using System.Threading.Tasks;
using SimpleHardwareMonitor.common;

namespace SimpleHardwareMonitor
{
    public static partial class HardwareMonitor
    {
        private static bool _runing = false;
        public static bool Runing
        {
            get => _runing;
            private set
            {
                _runing = value;
                HardwareMonitorVM.instance.Runing = value;
            }
        }
        internal static int _updateInterval = 50;
        internal static void SetUpdateInterval(int updateInterval) { _updateInterval = updateInterval; }
        public static int UpdateInterval { get => _updateInterval; set => HardwareMonitorVM.instance.UpdateInterval = value; }
        public static MotherboardData Motherboard { get => _motherboard.Data; }
        public static SuperIOData SuperIO { get => _superIO.Data; }
        public static CpuData Cpu { get => _cpu.Data; }
        public static MemoryData Memory { get => _memory.Data; }
        public static GpuNvidiaData GpuNvidia { get => _gpu_Nvidia.Data; }
        public static GpuAmdData GpuAmd { get => _gpu_Amd.Data; }
        public static GpuIntelData GpuIntel { get => _gpu_Intel.Data; }
        public static StorageData Storage { get => _storage.Data; }
        public static NetworkData Network { get => _network.Data; }
        public static CoolerData Cooler { get => _cooler.Data; }
        public static EmbeddedControllerData EmbeddedController { get => _embeddedController.Data; }
        public static PsuData Psu { get => _psu.Data; }
        public static BatteryData Battery { get => _battery.Data; }
        /// <summary>
        /// Initialized.
        /// </summary>
        public static void Initialized()
        {
            if (Runing is true)
                return;
            computer.IsCpuEnabled = true;
            computer.IsMotherboardEnabled = true;
            computer.IsMemoryEnabled = true;
            computer.IsGpuEnabled = true;
            computer.IsStorageEnabled = true;
            computer.IsNetworkEnabled = true;
            computer.IsControllerEnabled = true;
            computer.IsPsuEnabled = true;
#if USE_BATTERY
            computer.IsBatteryEnabled = true; // unable
#endif
            computer.Open();

            foreach (var hardware in computer.Hardware)
                CheckCreateHardware(hardware);
            Runing = true;
        }

        /// <summary>
        /// Release.
        /// </summary>
        public static void Release()
        {
            if (Runing is false)
                return;
            Runing = false;

            CheckReleaseHardware(_motherboard);
            CheckReleaseHardware(_superIO);
            CheckReleaseHardware(_cpu);
            CheckReleaseHardware(_memory);
            CheckReleaseHardware(_gpu_Nvidia);
            CheckReleaseHardware(_gpu_Amd);
            CheckReleaseHardware(_gpu_Intel);
            CheckReleaseHardware(_storage);
            CheckReleaseHardware(_network);
            CheckReleaseHardware(_cooler);
            CheckReleaseHardware(_embeddedController);
            CheckReleaseHardware(_psu);
            CheckReleaseHardware(_battery);

            _cancellationTokenSource.Cancel(); // Cancel the running task
            if (_updateTask.Wait(TimeSpan.FromSeconds(5)) is false) // Wait for the task to complete with timeout
            {
                // If the task did not complete in the given time, forcefully dispose the cancellation token source
                _cancellationTokenSource.Dispose();
#if DEBUG
                throw new Exception("Update task did not complete in time and was forcefully terminated.");
#endif
            }
            else
                _cancellationTokenSource.Dispose();
            computer.Close();
        }
    }

    public static partial class HardwareMonitor
    {
        /// <summary>
        /// LibreHardwareMonitor maint.
        /// </summary>
        private static Computer computer = new Computer();
        private static AHardwareMonitor<MotherboardData> _motherboard = null;
        private static AHardwareMonitor<SuperIOData> _superIO = null;
        private static AHardwareMonitor<CpuData> _cpu = null;
        private static AHardwareMonitor<MemoryData> _memory = null;
        private static AHardwareMonitor<GpuNvidiaData> _gpu_Nvidia = null;
        private static AHardwareMonitor<GpuAmdData> _gpu_Amd = null;
        private static AHardwareMonitor<GpuIntelData> _gpu_Intel = null;
        private static AHardwareMonitor<StorageData> _storage = null;
        private static AHardwareMonitor<NetworkData> _network = null;
        private static AHardwareMonitor<CoolerData> _cooler = null;
        private static AHardwareMonitor<EmbeddedControllerData> _embeddedController = null;
        private static AHardwareMonitor<PsuData> _psu = null;
        private static AHardwareMonitor<BatteryData> _battery = null;
        /// <summary>
        /// task instance.
        /// </summary>
        static private Task _updateTask = null;
        /// <summary>
        /// to cancel an asynchronous operation.
        /// </summary>
        static private CancellationTokenSource _cancellationTokenSource = null;
        static HardwareMonitor()
        {
            Initialized();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            _updateTask = Task.Run(async () => await UpdateHardwareAsync(cancellationToken), cancellationToken);
        }
        private static async Task UpdateHardwareAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {

                    UpdateHardware(_motherboard);
                    UpdateHardware(_superIO);
                    UpdateHardware(_cpu);
                    UpdateHardware(_memory);
                    UpdateHardware(_gpu_Nvidia);
                    UpdateHardware(_gpu_Amd);
                    UpdateHardware(_gpu_Intel);
                    UpdateHardware(_storage);
                    UpdateHardware(_network);
                    UpdateHardware(_cooler);
                    UpdateHardware(_embeddedController);
                    UpdateHardware(_psu);
                    UpdateHardware(_battery);
                    await Task.Delay(UpdateInterval);
                }
            }
            catch (TaskCanceledException)
            {
                // task was canceled, no need to handle this as an error.
            }
            catch (Exception ex)
            {
                // rethrow the exception to be handled by the caller.
                throw new Exception($"Exception occurred during update: {ex.Message}", ex);
            }
        }
        private static void CheckCreateHardware<data, T>(ref AHardwareMonitor<data> oldItem, T newItem)
            where data : struct
            where T : AHardwareMonitor<data>
        {
            if (oldItem != null)
            {
                oldItem.Dispose();
                oldItem = null;
            }
            oldItem = newItem;
        }
        private static void CheckCreateHardware(IHardware hardware)
        {
            switch (hardware.HardwareType)
            {
                case HardwareType.Motherboard:
                    CheckCreateHardware(ref _motherboard, new MotherboardMonitor(hardware));
                    break;
                case HardwareType.SuperIO:
                    CheckCreateHardware(ref _superIO, new SuperIOMonitor(hardware));
                    break;
                case HardwareType.Cpu:
                    CheckCreateHardware(ref _cpu, new CpuMonitor(hardware));
                    break;
                case HardwareType.Memory:
                    CheckCreateHardware(ref _memory, new MemoryMonitor(hardware));
                    break;
                case HardwareType.GpuNvidia:
                    CheckCreateHardware(ref _gpu_Nvidia, new GpuNvidiaMonitor(hardware));
                    break;
                case HardwareType.GpuAmd:
                    CheckCreateHardware(ref _gpu_Amd, new GpuAmdMonitor(hardware));
                    break;
                case HardwareType.GpuIntel:
                    CheckCreateHardware(ref _gpu_Intel, new GpuIntelMonitor(hardware));
                    break;
                case HardwareType.Storage:
                    CheckCreateHardware(ref _storage, new StorageMonitor(hardware));
                    break;
                case HardwareType.Network:
                    CheckCreateHardware(ref _network, new NetworkMonitor(hardware));
                    break;
                case HardwareType.Cooler:
                    CheckCreateHardware(ref _cooler, new CoolerMonitor(hardware));
                    break;
                case HardwareType.EmbeddedController:
                    CheckCreateHardware(ref _embeddedController, new EmbeddedControllerMonitor(hardware));
                    break;
                case HardwareType.Psu:
                    CheckCreateHardware(ref _psu, new PsuMonitor(hardware));
                    break;
                case HardwareType.Battery: // unable
                    CheckCreateHardware(ref _battery, new BatteryMonitor(hardware));
                    break;
                default:
                    break;
            }
        }
        private static void UpdateHardware<data>(AHardwareMonitor<data> item) where data : struct
        {
            if (item is null)
                return;
            item.UpdateHardWare();
        }
        private static void CheckReleaseHardware<data>(AHardwareMonitor<data> item) where data : struct
        {
            if (item != null)
            {
                item.Dispose();
                item = null;
            }
        }
    }
}

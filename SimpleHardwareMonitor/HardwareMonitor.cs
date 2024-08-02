using System.Threading;
using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.monitor;
using SimpleHardwareMonitor.@base;
using SimpleHardwareMonitor.data;

namespace SimpleHardwareMonitor
{
    static public partial class HardwareMonitor
    {
        public static AHardwareMonitor<MotherboardData> Motherboard { get; private set; } = null;
        public static AHardwareMonitor<SuperIOData> SuperIO { get; private set; } = null;
        public static AHardwareMonitor<CpuData> Cpu { get; private set; } = null;
        public static AHardwareMonitor<MemoryData> Memory { get; private set; } = null;
        public static AHardwareMonitor<GpuNvidiaData> Gpu_Nvidia { get; private set; } = null;
        public static AHardwareMonitor<GpuAmdData> Gpu_Amd { get; private set; } = null;
        public static AHardwareMonitor<GpuIntelData> Gpu_Intel { get; private set; } = null;
        public static AHardwareMonitor<StorageData> Storage { get; private set; } = null;
        public static AHardwareMonitor<NetworkData> Network { get; private set; } = null;
        public static AHardwareMonitor<CoolerData> Cooler { get; private set; } = null;
        public static AHardwareMonitor<EmbeddedControllerData> EmbeddedController { get; private set; } = null;
        public static AHardwareMonitor<PsuData> Psu { get; private set; } = null;
        //public static AHardwareMonitor<BatteryData> Battery { get; private set; } = null;
        /// <summary>
        /// ms
        /// </summary>
        public static int UpdateInterval { get; set; } = 1000;
        public static bool Runing { get; private set; }

        /// <summary>
        /// Initialized.
        /// </summary>
        public static void Initialized()
        {
            computer.IsCpuEnabled = true;
            computer.IsMotherboardEnabled = true;
            computer.IsMemoryEnabled = true;
            computer.IsGpuEnabled = true;
            computer.IsStorageEnabled = true;
            computer.IsNetworkEnabled = true;
            computer.IsControllerEnabled = true;
            computer.IsPsuEnabled = true;
            //computer.IsBatteryEnabled = true; // unable
            computer.Open();
            foreach (var hardware in computer.Hardware)
            {
                switch (hardware.HardwareType)
                {
                    case HardwareType.Motherboard:
                        if (Motherboard != null)
                        {
                            Motherboard.Dispose();
                            Motherboard = null;
                        }
                        Motherboard = new MotherboardMonitor(hardware);
                        break;
                    case HardwareType.SuperIO:
                        if (SuperIO != null)
                        {
                            SuperIO.Dispose();
                            SuperIO = null;
                        }
                        SuperIO = new SuperIOMonitor(hardware);
                        break;
                    case HardwareType.Cpu:
                        if (Cpu != null)
                        {
                            Cpu.Dispose();
                            Cpu = null;
                        }
                        Cpu = new CpuMonitor(hardware);
                        break;
                    case HardwareType.Memory:
                        if (Memory != null)
                        {
                            Memory.Dispose();
                            Memory = null;
                        }
                        Memory = new MemoryMonitor(hardware);
                        break;
                    case HardwareType.GpuNvidia:
                        if (Gpu_Nvidia != null)
                        {
                            Gpu_Nvidia.Dispose();
                            Gpu_Nvidia = null;
                        }
                        Gpu_Nvidia = new GpuNvidiaMonitor(hardware);
                        break;
                    case HardwareType.GpuAmd:
                        if (Gpu_Amd != null)
                        {
                            Gpu_Amd.Dispose();
                            Gpu_Amd = null;
                        }
                        Gpu_Amd = new GpuAmdMonitor(hardware);
                        break;
                    case HardwareType.GpuIntel:
                        if (Gpu_Intel != null)
                        {
                            Gpu_Intel.Dispose();
                            Gpu_Intel = null;
                        }
                        Gpu_Intel = new GpuIntelMonitor(hardware);
                        break;
                    case HardwareType.Storage:
                        if (Storage != null)
                        {
                            Storage.Dispose();
                            Storage = null;
                        }
                        Storage = new StorageMonitor(hardware);
                        break;
                    case HardwareType.Network:
                        if (Network != null)
                        {
                            Network.Dispose();
                            Network = null;
                        }
                        Network = new NetworkMonitor(hardware);
                        break;
                    case HardwareType.Cooler:
                        if (Cooler != null)
                        {
                            Cooler.Dispose();
                            Cooler = null;
                        }
                        Cooler = new CoolerMonitor(hardware);
                        break;
                    case HardwareType.EmbeddedController:
                        if (EmbeddedController != null)
                        {
                            EmbeddedController.Dispose();
                            EmbeddedController = null;
                        }
                        EmbeddedController = new EmbeddedControllerMonitor(hardware);
                        break;
                    case HardwareType.Psu:
                        if (Psu != null)
                        {
                            Psu.Dispose();
                            Psu = null;
                        }
                        Psu = new PsuMonitor(hardware);
                        break;
                    //case HardwareType.Battery: // unable
                    //    if (Battery != null)
                    //    {
                    //        Battery.Dispose();
                    //        Battery = null;
                    //    }
                    //    Battery = new BatteryMonitor(hardware);
                    //    break;
                    default:
                        break;
                }
            }
            Runing = true;
        }
        /// <summary>
        /// Release.
        /// </summary>
        static public void Release()
        {
            Runing = false;
            if (Motherboard != null)
            {
                Motherboard.Dispose();
                Motherboard = null;
            }
            if (SuperIO != null)
            {
                SuperIO.Dispose();
                SuperIO = null;
            }
            if (Cpu != null)
            {
                Cpu.Dispose();
                Cpu = null;
            }
            if (Memory != null)
            {
                Memory.Dispose();
                Memory = null;
            }
            if (Gpu_Nvidia != null)
            {
                Gpu_Nvidia.Dispose();
                Gpu_Nvidia = null;
            }
            if (Gpu_Amd != null)
            {
                Gpu_Amd.Dispose();
                Gpu_Amd = null;
            }
            if (Gpu_Intel != null)
            {
                Gpu_Intel.Dispose();
                Gpu_Intel = null;
            }
            if (Storage != null)
            {
                Storage.Dispose();
                Storage = null;
            }
            if (Network != null)
            {
                Network.Dispose();
                Network = null;
            }
            if (Cooler != null)
            {
                Cooler.Dispose();
                Cooler = null;
            }
            if (EmbeddedController != null)
            {
                EmbeddedController.Dispose();
                EmbeddedController = null;
            }
            if (Psu != null)
            {
                Psu.Dispose();
                Psu = null;
            }
            //if (Battery != null)
            //{
            //    Battery.Dispose();
            //    Battery = null;
            //}
            computer.Close();
        }
    }

    static public partial class HardwareMonitor
    {
        /// <summary>
        /// LibreHardwareMonitor maint.
        /// </summary>
        private static Computer computer = new Computer();
    }
}

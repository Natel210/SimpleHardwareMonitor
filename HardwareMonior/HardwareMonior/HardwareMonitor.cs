using System;
using System.Threading;
using System.Diagnostics;
using LibreHardwareMonitor.Hardware;
using System.Linq.Expressions;
using System.Linq;

namespace HardwareMonitor
{



    static public class HardwareMonitor
    {
        static CpuMonitor cpuMonitor;
        static HardwareMonitor()
        {
            cpuMonitor = new CpuMonitor(computer);
        }


        static Computer computer = new Computer
        {
            IsCpuEnabled = true,
            IsMotherboardEnabled = true
        };


        static private Timer run_timer;

        static public void Init()
        {
            computer.Open();

            run_timer = new Timer(Update);
            run_timer.Change(15000,1000);
        }
        
        private static void Update(object state)
        {
            //CpuMonitor.CPU_USE.Update();

            foreach (var hardware in computer.Hardware)
            {
                switch (hardware.HardwareType)
                {
                    case HardwareType.Cpu:
                        UpdateValueAsync_CPU();
                        break;

                    //미사용 하드웨어
                    case HardwareType.Motherboard:
                    case HardwareType.SuperIO:
                    case HardwareType.Memory:
                    case HardwareType.GpuNvidia:
                    case HardwareType.GpuAmd:
                    case HardwareType.GpuIntel:
                    case HardwareType.Storage:
                    case HardwareType.Network:
                    case HardwareType.Cooler:
                    case HardwareType.EmbeddedController:
                    case HardwareType.Psu:
                    case HardwareType.Battery:
                    default:
                        break;
                }
            }
        }

        static void UpdateValueAsync_CPU()
        {

        }



    }
}

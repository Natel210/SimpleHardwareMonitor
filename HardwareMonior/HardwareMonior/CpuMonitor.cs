using HardwareMonitor.Items;
using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace HardwareMonitor
{
    public class CpuMonitor
    {
        private readonly Computer _computer = null;

        public CpuMonitor(Computer computer)
        {
            _computer = computer;
        }


        public WindowsPerformanceCounterWrapper CpuUse { get; } =
            new WindowsPerformanceCounterWrapper("Processor Information",
                "% Processor Utility", "0,_Total");


        void 


    }
}

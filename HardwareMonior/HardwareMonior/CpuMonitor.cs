using HardwareMonior.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace HardwareMonior
{
    static public class CpuMonitor
    {
        static public WindowsPerformanceCounterWrapper CPU_USE { get; } = new WindowsPerformanceCounterWrapper("Processor Information", "% Processor Utility", "0,_Total");



    }
}

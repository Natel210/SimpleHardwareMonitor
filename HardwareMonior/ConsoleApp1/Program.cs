using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {


            HardwareMonitor.HardwareMonitor.Init();
            
            while (true)
            {
                Console.Clear();
                // 작업 관리자의 CPU 사용량
                Console.WriteLine(Math.Round(HardwareMonitor.CpuMonitor.CPU_USE.GetValue(), 2));

            }
            
        }
    }
}

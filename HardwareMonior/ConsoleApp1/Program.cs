using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleHardwareMonitor;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {


            HardwareMonitor.Initialized();
            
            while (true)
            {
                Console.Clear();
                // 작업 관리자의 CPU 사용량
                Console.WriteLine(Math.Round(HardwareMonitor.Cpu.Data.Use, 2));
                Console.WriteLine(Math.Round(HardwareMonitor.Cpu.Data.Temperature, 2));
                Console.WriteLine(Math.Round(HardwareMonitor.Cpu.Data.Power, 2));
                Thread.Sleep(1000);
            }
            
        }
    }
}

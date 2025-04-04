using SimpleHardwareMonitor.Data;

namespace SimpleHardWareTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Start();
            Outputs outputs = new Outputs();
            outputs.OutputMode = OutputMode.Summary;
            bool autoMode = false;
            outputs.Ouput();
            while (true)
            {
                if (Console.KeyAvailable) // 키 입력이 있는지 확인
                {
                    ConsoleKeyInfo key = Console.ReadKey(true); // 키를 읽고 출력하지 않음
                    switch (key.Key)
                    {
                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                            outputs.OutputMode = OutputMode.Summary;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            outputs.OutputMode = OutputMode.Motherboard;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            outputs.OutputMode = OutputMode.SuperIO;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            outputs.OutputMode = OutputMode.Cpu;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            outputs.OutputMode = OutputMode.Memory;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5:
                            outputs.OutputMode = OutputMode.Graphics;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6:
                            outputs.OutputMode = OutputMode.Storage;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D7:
                        case ConsoleKey.NumPad7:
                            outputs.OutputMode = OutputMode.Network;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D8:
                        case ConsoleKey.NumPad8:
                            outputs.OutputMode = OutputMode.Cooler;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.D9:
                        case ConsoleKey.NumPad9:
                            outputs.OutputMode = OutputMode.EmbeddedController;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.Subtract:
                            outputs.OutputMode = OutputMode.Psu;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.Add:
                            outputs.OutputMode = OutputMode.Battery;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.A:
                            autoMode = true;
                            break;
                        case ConsoleKey.S:
                            autoMode = false;
                            break;
                        case ConsoleKey.R:
                            outputs.Ouput();
                            break;
                        case ConsoleKey.Q://quit
                            return;
                        default:
                            break;
                    }
                }
                if (autoMode is true)
                    outputs.Ouput();
                
            }
        }
    }
}


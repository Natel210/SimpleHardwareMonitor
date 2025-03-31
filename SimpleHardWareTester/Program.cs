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
                        case ConsoleKey.NumPad0:
                            outputs.OutputMode = OutputMode.Summary;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.NumPad1:
                            outputs.OutputMode = OutputMode.Cpu;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.NumPad2:
                            outputs.OutputMode = OutputMode.Memory;
                            outputs.Ouput();
                            break;
                        case ConsoleKey.NumPad3:
                            outputs.OutputMode = OutputMode.Graphics;
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


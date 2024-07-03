// See https://aka.ms/new-console-template for more information
using HardwareMonior;

Console.WriteLine("Hello, World!");
MonitorManager a = new MonitorManager();
a.Init();
//while (true)
//{
    Console.Clear();
    a.Update();
//}
Console.ReadKey();
a.Close ();
# SimpleHardwareMonitor

## Summary
This project provides an easier way to access hardware monitoring data through structured types.</br>
It is based on <code>LibreHardwareMonitor</code> and includes an internal threading mechanism that automatically updates values such as CPU usage.</br>
Users can simply retrieve the desired data through <code>get</code> accessors.</br>
Currently in preview (experimental) stage.</br>

## Key Features

- Hardware monitoring
- Easy singleton-style access

## How to

### Start & Stop Monitoring
```cs
// Start monitoring updates
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Start();
// Stop monitoring updates
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.End();
```

### Get
```cs
// Example shown here is for CPU monitoring.
// Similar structures and access patterns apply for other hardware types,
// such as GPU, RAM, Storage, and more via the same interface.

// A value of 0 indicates that the measurement was not available.

// Helper to convert a List<float> to a readable string
var listToString = (List<float> list) => {
    string temp = "";
    foreach (var item in list)
        temp += $"[{item}],";
    return temp;
};
// Get current CPU data
var datas = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cpu;
// Iterate over all CPU entries (may contain duplicates for each hardware instance)
foreach (var item in datas.Values)
{
    Console.WriteLine($"--------------------");
    Console.WriteLine($"Name:{item.Name}");
    Console.WriteLine($"CoreCount:{item.CoreCount}");
    Console.WriteLine($"ProcessorCount:{item.ProcessorCount}");
    //Use
    Console.WriteLine("");
    Console.WriteLine($"Use:{item.Use}");
    Console.WriteLine($"Use_ByThreads:{listToString(item.Use_ByThreads)}");
    //Load_Load
    Console.WriteLine("");
    Console.WriteLine($"Load_Load Total:{item.Load_Total}");
    Console.WriteLine($"Load_Load Max:{item.Load_Max}");
    Console.WriteLine($"Load_ByThreads:{listToString(item.Load_ByThreads)}");
    //Temp
    Console.WriteLine("");
    Console.WriteLine($"Temperature_Package:{item.Temperature_Package}");
    Console.WriteLine($"Temperature_Max:{item.Temperature_Max}");
    Console.WriteLine($"Temperature_Average:{item.Temperature_Average}");
    Console.WriteLine($"Temperature_ByCore:{listToString(item.Temperature_ByCore)} ");
    Console.WriteLine($"Temperature_Distanceto_Tj_Max_ByCore:{listToString(item.   Temperature_Distanceto_Tj_Max_ByCore)}");

    //Clock
    Console.WriteLine("");
    Console.WriteLine($"Clock_Bus_Speed:{item.Clock_Bus_Speed}");
    Console.WriteLine($"Clock_ByCore:{listToString(item.Clock_ByCore)}");

    //Vol
    Console.WriteLine("");
    Console.WriteLine($"Voltage:{item.Voltage}");
    Console.WriteLine($"Voltage_ByCore:{listToString(item.Voltage_ByCore)}");

    Console.WriteLine("");
    Console.WriteLine($"Power_Package:{item.Power_Package}");
    Console.WriteLine($"Power_Cores:{item.Power_Cores}");
    Console.WriteLine($"Power_Memory:{item.Power_Memory}");
    Console.WriteLine($"Power_Platform:{item.Power_Platform}");
}
```


## Third-Party Libraries

### LibreHardwareMonitorLib
- Version: 0.9.4
- License: Mozilla Public License 2.0 (MPL-2.0)
- Copyright: © LibreHardwareMonitor contributors
- Project URL: [LibreHardwareMonitor Project](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor)
- Nuget: [LibreHardwareMonitor NuGet](https://www.nuget.org/packages/LibreHardwareMonitorLib/0.9.4)
- Usage:  
  - Used for monitoring hardware sensors including temperature, fan speed, voltage, CPU/GPU load and clock.
- Dependencies:
  - HidSharp (>= 2.1.0)
    - License: Apache License 2.0
    - Project URL: [HidSharp Project](http://www.zer7.com/software/hidsharp)
  - Mono.Posix.NETStandard** (>= 1.0.0)
    - License: MIT, BSD, MS-PL
    - Project URL: [Mono Project](https://github.com/mono/mono)

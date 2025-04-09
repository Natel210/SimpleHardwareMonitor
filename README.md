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
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Init();
// Start monitoring updates
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Start();

// Add Logics ...

// Stop monitoring updates
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.End();
```

### Get
```cs
// 단순하게 해당 모듈을 불러와서 원하는 파라미터를 사용하시면됩니다.
var motherboard = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Motherboard;
var superIO = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SuperIO;
var cpu = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cpu;
var memory = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Memory;
var graphics = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Gpu;
var storage = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Storage;
var network = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Network;
var cooler = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cooler;
var embeddedController = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.EmbeddedController;
var psu = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Psu;
var battery = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Battery;

Console.WriteLine(string.Format(_titleFormat, $"Summary"));
foreach (var model in motherboard)
    Console.WriteLine($"** Motherboard : {model.Value.Name}");
foreach (var model in superIO)
    Console.WriteLine($"** SuperIO : {model.Value.Name}");
foreach (var model in cpu)
    Console.WriteLine($"** Cpu : {model.Value.Name} - {model.Value.Load_Total:F01}{_loadUnit}");
foreach (var model in memory)
    Console.WriteLine($"** Memory : {model.Value.Name} - {model.Value.Load_Memory:F01}{_loadUnit}");
foreach (var model in graphics)
{
    if (model.Value.Load_D3D_3D.Count != 0)
        Console.WriteLine($"** Graphics : {model.Value.Name} - {model.Value.Load_D3D_3D.Max():F01}{_loadUnit}");
    else
        Console.WriteLine($"** Graphics : {model.Value.Name} - XX.X {_loadUnit}");
}
foreach (var model in storage)
    Console.WriteLine($"** Storage : {model.Value.Name} - {100.0f - model.Value.Load_Used_Space:F01}{_loadUnit}");
foreach (var model in network)
    Console.WriteLine($"** Network : {model.Value.Name}");
foreach (var model in cooler)
    Console.WriteLine($"** Cooler : {model.Value.Name}");
foreach (var model in embeddedController)
    Console.WriteLine($"** EmbeddedController : {model.Value.Name}");
foreach (var model in psu)
    Console.WriteLine($"** Psu : {model.Value.Name}");
foreach (var model in battery)
    Console.WriteLine($"** Battery : {model.Value.Name} - {model.Value.Level_Charge}{_levelUnit}");
}
```
```cs
//사용한 람다식
var name_ToString = (string name) =>
{
    if (string.IsNullOrEmpty(name) is false)
        return $"** Name : {name}";
    else
        return $"** Name : N/A";
};
var model_ToString = (string header, float value, string unit) =>
{
    if (value != 0f)
        return string.Format(_itemFormat, header, value, unit);
    return string.Format(_itemFormat, header, "N/A", unit);
};
```
```cs
// 사용한 변수
private static readonly string _voltageUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Voltage);
private static readonly string _currentUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Current);
private static readonly string _clockUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Clock);
private static readonly string _powerUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Power);
private static readonly string _levelUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Level);
private static readonly string _loadUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Load);
private static readonly string _temperatureUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Temperature);
private static readonly string _dataUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Data);
private static readonly string _smallDataUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.SmallData);
private static readonly string _energyUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Energy);
private static readonly string _throughputUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Throughput);
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

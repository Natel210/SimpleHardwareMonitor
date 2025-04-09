# SimpleHardwareMonitor

## 🔍 Summary
SimpleHardwareMonitor provides an easy and structured way to access live hardware monitoring data in .NET applications.
Built on top of `LibreHardwareMonitor`, it supports real-time updates through internal background threading.

> ⚠️ This project is currently in **experimental/preview** stage. APIs and behavior are subject to change.


## ✨ Key Features
- Access hardware sensor data in a structured format
- Background update via `Init()` and `Start()`
- Singleton access pattern
- Support for CPU, GPU, memory, storage, fan, PSU, battery, and more


## 🚀 How to Use

### 1. Start & Stop Monitoring
```csharp
// Start monitoring
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Init();
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Start();

// Add your logic here...

// Stop monitoring
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.End();
```

### 2. Retrieve Sensor Data
```csharp
// Access hardware categories
var cpu = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cpu; // Dictionary<string, Model.Cpu>
var memory = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Memory; // Dictionary<string, Model.Memory>
// ... and so on for:
// Motherboard, SuperIO, Gpu, Storage, Network, Cooler, EmbeddedController, Psu, Battery

// Example: Read CPU usage per item
foreach (var item in cpu)
{
    Console.WriteLine($"CPU: {item.Value.Name} - {item.Value.Load_Total:F1}%");
}
```

> ✅ Each property (e.g. `Cpu`, `Memory`) returns a `Dictionary<string, Model.X>` where `X` is a data structure representing that hardware.


### 3. Format Utilities (Optional)
```csharp
// Lambda helpers
var name_ToString = (string name) => string.IsNullOrEmpty(name) ? "** Name : N/A" : $"** Name : {name}";
var model_ToString = (string header, float value, string unit) =>
    value != 0f ? string.Format(_itemFormat, header, value, unit) : string.Format(_itemFormat, header, "N/A", unit);

// Sensor unit suffixes (auto-generated per SensorType)
private static readonly string _loadUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(SensorType.Load);
private static readonly string _temperatureUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(SensorType.Temperature);
private static readonly string _levelUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(SensorType.Level);
// etc...
```

## 🧪 Sample Console Output (SimpleHardWareTester)
```text
========  Summary  ========
** Motherboard : ASRock B650M
** SuperIO : Nuvoton NCT6797D
** CPU : AMD Ryzen 9 - 32.4 %
** Memory : DDR5-6000 - 71.6 %
** Graphics : NVIDIA RTX 4080 - 97.2 %
** Storage : Samsung SSD 980 - 88.4 %
** Network : Intel Ethernet I225-V
** Cooler : Be Quiet! Pure Rock 2
** Embedded Controller : EC Ver.1.02
** PSU : Corsair RM850x
** Battery : ASUS Battery - 95 %
```


## 📦 Third-Party Libraries

### LibreHardwareMonitorLib
- Version: 0.9.4
- License: Mozilla Public License 2.0 (MPL-2.0)
- Project URL: [LibreHardwareMonitor](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor)
- NuGet: [LibreHardwareMonitorLib](https://www.nuget.org/packages/LibreHardwareMonitorLib)
- Used For:
  - Sensor data: temperature, fan speed, voltage, CPU/GPU load and clocks

#### Dependencies:
- HidSharp (>= 2.1.0)
  - License: Apache License 2.0
  - Project URL: [HidSharp Project](http://www.zer7.com/software/hidsharp)
- Mono.Posix.NETStandard (>= 1.0.0)
  - License: MIT, BSD, MS-PL
  - Project URL: [Mono Project](https://github.com/mono/mono)
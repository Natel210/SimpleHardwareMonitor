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
- SMBIOS information extraction


## 🚀 Getting Started

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
var motherboard = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Motherboard; // Dictionary<string, Model.Motherboard>
var cpu = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cpu; // Dictionary<string, Model.Cpu>
var memory = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Memory; // Dictionary<string, Model.Memory>
// ... and so on for:
// Motherboard, SuperIO, Gpu, Storage, Network, Cooler, EmbeddedController, Psu, Battery

// Example: Print motherboard info
foreach (var item in motherboard)
{
    Console.WriteLine($"Motherboard: {item.Value.Name}");
}

// Example: Read CPU usage per item
foreach (var item in cpu)
{
    Console.WriteLine($"CPU: {item.Value.Name} - {item.Value.Load_Total:F1}%");
}
```

> ✅ Each property (e.g. `Cpu`, `Memory`) returns a `Dictionary<string, Model.X>` where `X` is a data structure representing that hardware.


### 3. Optional Advanced Usage

#### Retrieve SMBIOS Information
```csharp
// Returns structured BIOS, motherboard, and system information
var smbios = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.GetSMBios();
if (smbios != null)
{
    Console.WriteLine("BIOS Vendor: " + smbios.Bios.Vendor);
    Console.WriteLine("BIOS Version: " + smbios.Bios.Version);
    Console.WriteLine("BIOS Date: " + smbios.Bios.Date);

    Console.WriteLine("System Manufacturer: " + smbios.System.ManufacturerName);
    Console.WriteLine("System Name: " + smbios.System.ProductName);
    Console.WriteLine("System Version: " + smbios.System.Version);

    Console.WriteLine("Motherboard Manufacturer: " + smbios.Board.ManufacturerName);
    Console.WriteLine("Motherboard Name: " + smbios.Board.ProductName);
    Console.WriteLine("Motherboard Version: " + smbios.Board.Version);
}
```

#### Set Update Interval
```csharp
// Set the interval between sensor updates (in milliseconds)
SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Interval = 1000; // 1 second
```

### 4. Optional Unit Formatting Helpers
```csharp
// Sensor unit suffixes used for formatting output values (optional, for display)
private static readonly string _loadUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(SensorType.Load);
private static readonly string _temperatureUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(SensorType.Temperature);
private static readonly string _levelUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.SenserTypeToUnitString(SensorType.Level);
// etc...
```

## 🧪 Console Output
```text
======== Summary ========
Motherboard : ASRock B650M
SuperIO : Nuvoton NCT6797D
CPU : AMD Ryzen 9 - 32.4 %
Memory : DDR5-6000 - 71.6 %
Graphics : NVIDIA RTX 4080 - 97.2 %
Storage : Samsung SSD 980 - 88.4 %
Network : Intel Ethernet I225-V
Cooler : Be Quiet! Pure Rock 2
Embedded Controller : EC Ver.1.02
PSU : Corsair RM850x
Battery : ASUS Battery - 95 %
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
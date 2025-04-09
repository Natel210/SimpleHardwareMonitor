using HidSharp.Reports;
using LibreHardwareMonitor.Hardware.Motherboard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardWareTester
{



    internal enum OutputMode
    {
        Summary,
        Motherboard,
        SuperIO,
        Cpu,
        Memory,
        Graphics,
        Storage,
        Network,
        Cooler,
        EmbeddedController,
        Psu,
        Battery
    }

    internal class Outputs
    {
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


        private const string _menuEdgeFormat = "================================================================";
        private const string _titleFormat = "========  {0}  ========";
        private const string _endHardwareSeparator = "--------------------------------";
        private const string _senserFormat = "----[ {0} ]----";
        private const string _itemFormat = "** {0}({2}) : {1}";


        internal OutputMode OutputMode { get; set; }

        internal void Ouput()
        {
            Thread.Sleep(1000);
            Console.Clear();

            var createMenu = () => {
                Console.WriteLine(_menuEdgeFormat);
                string temp = string.Join(" ", Enum.GetValues(typeof(OutputMode)).Cast<OutputMode>().Select(v => v == OutputMode ? $">>[{v}]" : $"[{v}]"));
                Console.WriteLine($"{temp}");
                Console.WriteLine(_menuEdgeFormat);
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("");
                Console.WriteLine("");
            };
            createMenu();
            switch (OutputMode)
            {
                case OutputMode.Summary:
                    Summary();
                    break;
                case OutputMode.Motherboard:
                    Motherboard();
                    break;
                case OutputMode.SuperIO:
                    SuperIO();
                    break;
                case OutputMode.Cpu:
                    Cpu();
                    break;
                case OutputMode.Memory:
                    Memory();
                    break;
                case OutputMode.Graphics:
                    Graphics();
                    break;
                case OutputMode.Storage:
                    Storage();
                    break;
                case OutputMode.Network:
                    Network();
                    break;
                case OutputMode.Cooler:
                    Cooler();
                    break;
                case OutputMode.EmbeddedController:
                    EmbeddedController();
                    break;
                case OutputMode.Psu:
                    Psu();
                    break;
                case OutputMode.Battery:
                    Battery();
                    break;
                default:
                    break;
            }
        }

        private void Summary()
        {
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


        private void Motherboard()
        {
            var model_ToString = (string header, float value, string unit) =>
            {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Motherboard;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Motherboard - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void SuperIO()
        {
            var model_ToString = (string header, float value, string unit) =>
            {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SuperIO;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"SuperIO - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Cpu()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };
            var dictionary_ToString = (string header, Dictionary<int, float> dic, string unit) => {
                string temp = string.Join(" ", dic.Select(v => $"[{v.Key}]({(v.Value == 0 ? "N/A" : v.Value.ToString("0.##"))})"));
                return string.Format(_itemFormat, header, temp, unit);
            };
            var load_ByThreads_ToString = (string header, Dictionary<int, Dictionary<int, float>> dic, string unit) => {
                string temp = string.Join(" ", dic.SelectMany(outer => outer.Value.Select(inner => $"[{outer.Key}-{inner.Key}][{(inner.Value == 0 ? "N/A" : inner.Value.ToString("0.##"))}]" )));
                return string.Format(_itemFormat, header, temp, unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cpu;
            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"CPU - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                //if (item.CoreCount > 0)
                //    Console.WriteLine($"Core Count : {item.CoreCount}");
                //else
                //    Console.WriteLine($"Core Count : N/A");
                //if (item.ProcessorCount > 0)
                //    Console.WriteLine($"Processor Count : {item.ProcessorCount}");
                //else
                //    Console.WriteLine($"Processor Count : N/A");
                Console.WriteLine("");

                //// Used
                //Console.WriteLine(string.Format(_senserFormat, "Used"));
                //Console.WriteLine(dataCheck("Use", item.Use, _usedUnit));
                //Console.WriteLine(DicToString("Threads", item.Use_ByThreads, _usedUnit));
                //Console.WriteLine("");

                // Voltage
                Console.WriteLine(string.Format(_senserFormat, "Voltage"));
                Console.WriteLine(model_ToString("Voltage", model.Voltage, _voltageUnit));
                Console.WriteLine(dictionary_ToString("Cores", model.Voltage_ByCore, _voltageUnit));
                Console.WriteLine("");

                // Power
                Console.WriteLine(string.Format(_senserFormat, "Power"));
                Console.WriteLine(model_ToString("Package", model.Power_Package, _powerUnit));
                Console.WriteLine(model_ToString("Cores", model.Power_Cores, _powerUnit));
                Console.WriteLine(model_ToString("Memory", model.Power_Memory, _powerUnit));
                Console.WriteLine(model_ToString("Platform", model.Power_Platform, _powerUnit));
                Console.WriteLine("");

                // Clock
                Console.WriteLine(string.Format(_senserFormat, "Clock"));
                Console.WriteLine(model_ToString("Bus_Speed", model.Clock_Bus_Speed, _clockUnit));
                Console.WriteLine(dictionary_ToString("Cores", model.Clock_ByCore, _clockUnit));
                Console.WriteLine("");

                // Temperature
                Console.WriteLine(string.Format(_senserFormat, "Temperature"));
                Console.WriteLine(model_ToString("Package", model.Temperature_Package, _temperatureUnit));
                Console.WriteLine(model_ToString("Max", model.Temperature_Max, _temperatureUnit));
                Console.WriteLine(model_ToString("Average", model.Temperature_Average, _temperatureUnit));
                Console.WriteLine(dictionary_ToString("Cores", model.Temperature_ByCore, _temperatureUnit));
                Console.WriteLine(dictionary_ToString("Distanceto_Tj_Max_ByCores", model.Temperature_Distanceto_Tj_Max_ByCore, _temperatureUnit));
                Console.WriteLine("");

                // Load
                Console.WriteLine(string.Format(_senserFormat, "Load"));
                Console.WriteLine(model_ToString("Total", model.Load_Total, _loadUnit));
                Console.WriteLine(model_ToString("Max", model.Load_Core_Max, _loadUnit));
                Console.WriteLine(load_ByThreads_ToString("Threads", model.Load_ByThreads, _loadUnit));
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Memory()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };
            var data_ToString = (string header, float dataValue, float smallDataValue) => {
                if (dataValue != 0f)
                    return string.Format(_itemFormat, header, dataValue, _dataUnit);
                if (smallDataValue != 0f)
                    return string.Format(_itemFormat, header, smallDataValue, _smallDataUnit);
                return string.Format(_itemFormat, header, "N/A", "-");
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Memory;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Memory - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");

                // Load
                Console.WriteLine(string.Format(_senserFormat, "Load"));
                Console.WriteLine(model_ToString("Load", model.Load_Memory, _loadUnit));
                Console.WriteLine(model_ToString("Virtual Load", model.Load_Virtual_Memory, _loadUnit));
                Console.WriteLine("");

                // Data
                Console.WriteLine(string.Format(_senserFormat, "Data"));
                Console.WriteLine(data_ToString("Used", model.Data_Used, model.SmallData_Used));
                Console.WriteLine(data_ToString("Available", model.Data_Available, model.SmallData_Available));
                Console.WriteLine(data_ToString("Virtual Used", model.Data_Virtual_Used, model.SmallData_Virtual_Used));
                Console.WriteLine(data_ToString("Virtual Available",model.Data_Virtual_Available, model.SmallData_Virtual_Available));
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Graphics()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };
            var listModel_ToString = (string header, List<float> list, string unit) => {
                string temp = $"[{string.Join("],[", list.Select(v => v == 0 ? "N/A" : v.ToString()))}]";
                return string.Format(_itemFormat, header, temp, unit);
            };
            var data_ToString = (string header, float dataValue, float smallDataValue) => {
                if (dataValue != 0f)
                    return string.Format(_itemFormat, header, dataValue, _dataUnit);
                if (smallDataValue != 0f)
                    return string.Format(_itemFormat, header, smallDataValue, _smallDataUnit);
                return string.Format(_itemFormat, header, "N/A", "-");
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Gpu;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Graphics - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                if (string.IsNullOrEmpty(model.Gpu_Type) is false)
                    Console.WriteLine($"Graphics Type : {model.Gpu_Type}");
                else
                    Console.WriteLine($"Graphics Type : N/A");

                // Power
                Console.WriteLine(string.Format(_senserFormat, "Power"));
                Console.WriteLine(model_ToString("Power", model.Power, _powerUnit));
                Console.WriteLine("");

                // Clock
                Console.WriteLine(string.Format(_senserFormat, "Clock"));
                Console.WriteLine(model_ToString("Core", model.Clock_Core, _clockUnit));
                Console.WriteLine(model_ToString("Memory", model.Clock_Memory, _clockUnit));
                Console.WriteLine("");

                // Temperature
                Console.WriteLine(string.Format(_senserFormat, "Temperature"));
                Console.WriteLine(model_ToString("Core", model.Temperature_Core, _temperatureUnit));
                Console.WriteLine(model_ToString("Hot Spot", model.Temperature_Hot_Spot, _temperatureUnit));
                Console.WriteLine("");

                // Laod
                Console.WriteLine(string.Format(_senserFormat, "Load"));
                Console.WriteLine(model_ToString("Core", model.Load_Core, _loadUnit));
                Console.WriteLine(model_ToString("Memory Controller", model.Load_Memory_Controller, _loadUnit));
                Console.WriteLine(model_ToString("Video Engine", model.Load_Video_Engine, _loadUnit));
                Console.WriteLine(model_ToString("Bus", model.Load_Bus, _loadUnit));
                Console.WriteLine(model_ToString("Memory", model.Load_Memory, _loadUnit));
                Console.WriteLine(listModel_ToString("D3D 3D", model.Load_D3D_3D, _loadUnit));
                Console.WriteLine(listModel_ToString("D3D Video Decode", model.Load_D3D_VideoDecode, _loadUnit));
                Console.WriteLine(listModel_ToString("D3D Copy", model.Load_D3D_Copy, _loadUnit));
                Console.WriteLine(listModel_ToString("D3D Video Processing", model.Load_D3D_VideoProcessing, _loadUnit));
                Console.WriteLine(listModel_ToString("D3D GDIRender", model.Load_D3D_GDIRender, _loadUnit));
                Console.WriteLine(listModel_ToString("D3D Overlay", model.Load_D3D_Overlay, _loadUnit));
                Console.WriteLine(listModel_ToString("Ohters", model.Load_Ohters, _loadUnit));
                Console.WriteLine("");

                // Data
                Console.WriteLine(string.Format(_senserFormat, "Data"));
                Console.WriteLine(data_ToString("Memory Total", model.Data_Memory_Total, model.SmallData_Memory_Total));
                Console.WriteLine(data_ToString("Memory Free", model.Data_Memory_Free, model.SmallData_Memory_Free));
                Console.WriteLine(data_ToString("Memory Used", model.Data_Memory_Used, model.SmallData_Memory_Used));
                Console.WriteLine(data_ToString("D3D Shared Memory Total", model.Data_D3D_Shared_Memory_Total, model.SmallData_D3D_Shared_Memory_Total));
                Console.WriteLine(data_ToString("D3D Shared Memory Free", model.Data_D3D_Shared_Memory_Free, model.SmallData_D3D_Shared_Memory_Free));
                Console.WriteLine(data_ToString("D3D Shared Memory Used", model.Data_D3D_Shared_Memory_Used, model.SmallData_D3D_Shared_Memory_Used));
                Console.WriteLine("");

                //Throughput
                Console.WriteLine(string.Format(_senserFormat, "Throughput"));
                Console.WriteLine(model_ToString("PCIe Rx", model.Throughput_PCIe_Rx / 1024, "KB/s"));
                Console.WriteLine(model_ToString("PCIe Tx", model.Throughput_PCIe_Tx / 1024, "KB/s"));
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Storage()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };
            var listModel_ToString = (string header, List<float> list, string unit) => {
                string temp = $"[{string.Join("],[", list.Select(v => v == 0 ? "N/A" : v.ToString()))}]";
                return string.Format(_itemFormat, header, temp, unit);
            };
            var data_ToString = (string header, float dataValue, float smallDataValue) => {
                if (dataValue != 0f)
                    return string.Format(_itemFormat, header, dataValue, _dataUnit);
                if (smallDataValue != 0f)
                    return string.Format(_itemFormat, header, smallDataValue, _smallDataUnit);
                return string.Format(_itemFormat, header, "N/A", "-");
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Storage;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Storage - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");
                // Temperature
                Console.WriteLine(string.Format(_senserFormat, "Temperature"));
                Console.WriteLine(listModel_ToString("Temperature", model.Temperature, _temperatureUnit));
                Console.WriteLine("");
                // Load
                Console.WriteLine(model_ToString("Used Space", model.Load_Used_Space, _loadUnit));
                Console.WriteLine(model_ToString("Read Activity", model.Load_Read_Activity, _loadUnit));
                Console.WriteLine(model_ToString("Write Activity", model.Load_Write_Activity, _loadUnit));
                Console.WriteLine(model_ToString("Total Activity", model.Load_Total_Activity, _loadUnit));
                Console.WriteLine("");
                // Level
                Console.WriteLine(string.Format(_senserFormat, "Level"));
                Console.WriteLine(model_ToString("Available Spare", model.Level_Available_Spare, _loadUnit));
                Console.WriteLine(model_ToString("Available Spare Threshold", model.Level_Available_Spare_Threshold, _loadUnit));
                Console.WriteLine(model_ToString("Percentage Used", model.Level_Percentage_Used, _loadUnit));
                Console.WriteLine("");
                // Data
                Console.WriteLine(string.Format(_senserFormat, "Data"));
                Console.WriteLine(data_ToString("Read", model.Data_Read, model.SmallData_Read));
                Console.WriteLine(data_ToString("Written", model.Data_Written, model.SmallData_Written));
                Console.WriteLine("");
                // Throughput
                Console.WriteLine(string.Format(_senserFormat, "Throughput"));
                Console.WriteLine(model_ToString("Read Rate", model.Throughput_Read_Rate / 1024, "KB/s"));
                Console.WriteLine(model_ToString("Write Rate", model.Throughput_Write_Rate / 1024, "KB/s"));
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Network()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Network;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Network - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Cooler()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cooler;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Cooler - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void EmbeddedController()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cooler;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Cooler - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Psu()
        {
            var model_ToString = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cooler;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Cooler - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Battery()
        {
            var model_ToString = (string header, float value, string unit) =>
            {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };

            // Get Data
            var models = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Battery;

            // Title
            int count = 0;
            foreach (var model in models.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"Battery - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(model.Name) is false)
                    Console.WriteLine($"** Name : {model.Name}");
                else
                    Console.WriteLine($"** Name : N/A");
                Console.WriteLine("");
                // Voltage
                Console.WriteLine(string.Format(_senserFormat, "Voltage"));
                Console.WriteLine(model_ToString("Voltage", model.Voltage, _voltageUnit));
                Console.WriteLine("");
                // Current
                Console.WriteLine(string.Format(_senserFormat, "Current"));
                Console.WriteLine(model_ToString("Charge Discharge", model.Current_Charge_Discharge, _currentUnit));
                Console.WriteLine("");
                // Power
                Console.WriteLine(string.Format(_senserFormat, "Power"));
                Console.WriteLine(model_ToString("Charge Discharge Rate", model.Power_Charge_Discharge_Rate, _powerUnit));
                Console.WriteLine("");
                // Level
                Console.WriteLine(string.Format(_senserFormat, "Level"));
                Console.WriteLine(model_ToString("Degradation", model.Level_Degradation, _levelUnit));
                Console.WriteLine(model_ToString("Charge", model.Level_Charge, _levelUnit));
                Console.WriteLine("");
                // Level
                Console.WriteLine(string.Format(_senserFormat, "Energy"));
                Console.WriteLine(model_ToString("Designed Capacity", model.Energy_Designed_Capacity, _energyUnit));
                Console.WriteLine(model_ToString("Fully Charged Capacity", model.Energy_Fully_Charged_Capacity, _energyUnit));
                Console.WriteLine(model_ToString("Remaining Capacity", model.Energy_Remaining_Capacity, _energyUnit));
                Console.WriteLine("");

                Console.WriteLine(_endHardwareSeparator);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}

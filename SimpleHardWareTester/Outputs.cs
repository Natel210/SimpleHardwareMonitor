using HidSharp.Reports;
using SimpleHardwareMonitor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardWareTester
{



    internal enum OutputMode
    {
        Summary,
        Cpu,
        Memory,
        Graphics,

    }

    internal class Outputs
    {
        private static readonly string _usedUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Load);
        private static readonly string _loadUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Load);
        private static readonly string _smallDataUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.SmallData);
        private static readonly string _dataUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Data);
        private static readonly string _temperatureUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Temperature);
        private static readonly string _powerUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Power);
        private static readonly string _voltageUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Voltage);
        private static readonly string _clockUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Clock);
        private static readonly string _throughputUnit = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.SenserTypeToUnitString(LibreHardwareMonitor.Hardware.SensorType.Throughput);


        private const string _menuEdgeFormat = "================================================================";
        private const string _titleFormat = "========  {0}  ========";
        private const string _newHardwareFormat = "--------------------------------";
        private const string _senserFormat = "----[ {0} ]----";
        private const string _itemFormat = "** {0}({2}) : {1}";


        internal OutputMode OutputMode { get; set; }

        internal void Ouput()
        {
            Thread.Sleep(1000);
            Console.Clear();

            var createMenu = (string selectMenu) => {
                Console.WriteLine(_menuEdgeFormat);
                Console.WriteLine($"{selectMenu}");
                Console.WriteLine(_menuEdgeFormat);
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("");
                Console.WriteLine("");
            };

            switch (OutputMode)
            {
                case OutputMode.Summary:
                    createMenu($">>[Summary] [CPU][Memory][Graphics]");
                    Summary();
                    break;
                case OutputMode.Cpu:
                    createMenu($"[Summary] >>[CPU] [Memory][Graphics]");
                    Cpu();
                    break;
                case OutputMode.Memory:
                    createMenu($"[Summary][CPU] >>[Memory] [Graphics]");
                    Memory();
                    break;
                case OutputMode.Graphics:
                    createMenu($"[Summary][CPU][Memory] >>[Graphics]");
                    Gpu();
                    break;
                default:
                    break;
            }
        }

        private void Summary()
        {
            Console.WriteLine("");
            Console.WriteLine($"========    Summary    ========");
            Console.WriteLine($"--------------------");
        }

        private void Cpu()
        {
            var dataCheck = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };
            var listToString = (string header, List<float> list, string unit) => {
                string temp = $"[{string.Join("],[", list.Select(v => v == 0 ? "N/A" : v.ToString()))}]";
                return string.Format(_itemFormat, header, temp, unit);
            };

            // Get Data
            var datas = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Cpu;
            // Title
            int count = 0;
            foreach (var item in datas.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"CPU - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(item.Name) is false)
                    Console.WriteLine($"Name : {item.Name}");
                else
                    Console.WriteLine($"Name : N/A");
                if (item.CoreCount > 0)
                    Console.WriteLine($"Core Count : {item.CoreCount}");
                else
                    Console.WriteLine($"Core Count : N/A");
                if (item.ProcessorCount > 0)
                    Console.WriteLine($"Processor Count : {item.ProcessorCount}");
                else
                    Console.WriteLine($"Processor Count : N/A");
                Console.WriteLine("");
                //Used
                Console.WriteLine(string.Format(_senserFormat, "Used"));
                Console.WriteLine(dataCheck("Use", item.Use, _usedUnit));
                Console.WriteLine(listToString("Threads", item.Use_ByThreads, _usedUnit));
                Console.WriteLine("");
                //Load
                Console.WriteLine(string.Format(_senserFormat, "Load"));
                Console.WriteLine(dataCheck("Total", item.Load_Total, _loadUnit));
                Console.WriteLine(dataCheck("Max", item.Load_Max, _loadUnit));
                Console.WriteLine(listToString("Threads", item.Load_ByThreads, _loadUnit));
                Console.WriteLine("");
                //Temperature
                Console.WriteLine(string.Format(_senserFormat, "Temperature"));
                Console.WriteLine(dataCheck("Package", item.Temperature_Package, _temperatureUnit));
                Console.WriteLine(dataCheck("Max", item.Temperature_Max, _temperatureUnit));
                Console.WriteLine(dataCheck("Average", item.Temperature_Average, _temperatureUnit));
                Console.WriteLine(listToString("Cores", item.Temperature_ByCore, _temperatureUnit));
                Console.WriteLine(listToString("Distanceto_Tj_Max_ByCores", item.Temperature_Distanceto_Tj_Max_ByCore, _temperatureUnit));
                Console.WriteLine("");
                //Clock
                Console.WriteLine(string.Format(_senserFormat, "Clock"));
                Console.WriteLine(dataCheck("Bus_Speed", item.Clock_Bus_Speed, _clockUnit));
                Console.WriteLine(listToString("Cores", item.Clock_ByCore, _clockUnit));
                Console.WriteLine("");
                //Voltage
                Console.WriteLine(string.Format(_senserFormat, "Voltage"));
                Console.WriteLine(dataCheck("Voltage", item.Voltage, _voltageUnit));
                Console.WriteLine(listToString("Cores", item.Voltage_ByCore, _voltageUnit));
                Console.WriteLine("");
                //Power
                Console.WriteLine(string.Format(_senserFormat, "Temperature"));
                Console.WriteLine(dataCheck("Package", item.Power_Package, _powerUnit));
                Console.WriteLine(dataCheck("Cores", item.Power_Cores, _powerUnit));
                Console.WriteLine(dataCheck("Memory", item.Power_Memory, _powerUnit));
                Console.WriteLine(dataCheck("Platform", item.Power_Platform, _powerUnit));
                Console.WriteLine("");
                Console.WriteLine(_newHardwareFormat);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private void Memory()
        {
            var dataCheck = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };
            var parserData = (string header, float dataValue, float smallDataValue) => {
                if (dataValue != 0f)
                    return string.Format(_itemFormat, header, dataValue, _dataUnit);
                if (smallDataValue != 0f)
                    return string.Format(_itemFormat, header, smallDataValue, _smallDataUnit);
                return string.Format(_itemFormat, header, "N/A", "-");
            };

            // Get Data
            var datas = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Memory;

            // Title
            int count = 0;
            foreach (var item in datas.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"CPU - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(item.Name) is false)
                    Console.WriteLine($"Name : {item.Name}");
                else
                    Console.WriteLine($"Name : N/A");
                Console.WriteLine("");
                // Load
                Console.WriteLine(string.Format(_senserFormat, "Load"));
                Console.WriteLine(dataCheck("Load", item.Load_Load, _loadUnit));
                Console.WriteLine(dataCheck("Virtual Load", item.Load_Virtual_Load, _loadUnit));
                Console.WriteLine("");
                // Data
                Console.WriteLine(string.Format(_senserFormat, "Data"));
                Console.WriteLine(parserData("Used", item.Data_Used, item.SmallData_Used));
                Console.WriteLine(parserData("Available", item.Data_Available, item.SmallData_Available));
                Console.WriteLine(parserData("Virtual Used", item.Data_Virtual_Used, item.SmallData_Virtual_Used));
                Console.WriteLine(parserData("Virtual Available",item.Data_Virtual_Available, item.SmallData_Virtual_Available));
                Console.WriteLine("");
                Console.WriteLine(_newHardwareFormat);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }


        private void Gpu()
        {
            var dataCheck = (string header, float value, string unit) => {
                if (value != 0f)
                    return string.Format(_itemFormat, header, value, unit);
                return string.Format(_itemFormat, header, "N/A", unit);
            };
            var listToString = (string header, List<float> list, string unit) => {
                string temp = $"[{string.Join("],[", list.Select(v => v == 0 ? "N/A" : v.ToString()))}]";
                return string.Format(_itemFormat, header, temp, unit);
            };
            var parserData = (string header, float dataValue, float smallDataValue) => {
                if (dataValue != 0f)
                    return string.Format(_itemFormat, header, dataValue, _dataUnit);
                if (smallDataValue != 0f)
                    return string.Format(_itemFormat, header, smallDataValue, _smallDataUnit);
                return string.Format(_itemFormat, header, "N/A", "-");
            };

            

            // Get Data
            var datas = SimpleHardwareMonitor.SimpleHardwareMonitor.Instance.Gpu;

            // Title
            int count = 0;
            foreach (var item in datas.Values)
            {
                Console.WriteLine(string.Format(_titleFormat, $"CPU - {count++}"));
                Console.WriteLine(string.Format(_senserFormat, "Common"));
                if (string.IsNullOrEmpty(item.Name) is false)
                    Console.WriteLine($"Name : {item.Name}");
                else
                    Console.WriteLine($"Name : N/A");
                if (string.IsNullOrEmpty(item.Gpu_Type) is false)
                    Console.WriteLine($"Gpu Type : {item.Gpu_Type}");
                else
                    Console.WriteLine($"Gpu Type : N/A");
                // Power
                Console.WriteLine(string.Format(_senserFormat, "Power"));
                Console.WriteLine(dataCheck("Power", item.Power, _powerUnit));
                Console.WriteLine("");
                // Clock
                Console.WriteLine(string.Format(_senserFormat, "Clock"));
                Console.WriteLine(dataCheck("Core", item.Clock_Core, _clockUnit));
                Console.WriteLine(dataCheck("Memory", item.Clock_Memory, _clockUnit));
                Console.WriteLine("");
                // Temperature
                Console.WriteLine(string.Format(_senserFormat, "Temperature"));
                Console.WriteLine(dataCheck("Core", item.Temperature_Core, _temperatureUnit));
                Console.WriteLine(dataCheck("Hot Spot", item.Temperature_Hot_Spot, _temperatureUnit));
                Console.WriteLine("");
                // Laod
                Console.WriteLine(string.Format(_senserFormat, "Load"));
                Console.WriteLine(dataCheck("Core", item.Load_Core, _loadUnit));
                Console.WriteLine(dataCheck("Memory Controller", item.Load_Memory_Controller, _loadUnit));
                Console.WriteLine(dataCheck("Video Engine", item.Load_Video_Engine, _loadUnit));
                Console.WriteLine(dataCheck("Bus", item.Load_Bus, _loadUnit));
                Console.WriteLine(dataCheck("Memory", item.Load_Memory, _loadUnit));
                Console.WriteLine(listToString("D3D 3D", item.Load_D3D_3D, _loadUnit));
                Console.WriteLine(listToString("D3D Video Decode", item.Load_D3D_VideoDecode, _loadUnit));
                Console.WriteLine(listToString("D3D Copy", item.Load_D3D_Copy, _loadUnit));
                Console.WriteLine(listToString("D3D Video Processing", item.Load_D3D_VideoProcessing, _loadUnit));
                Console.WriteLine(listToString("D3D GDIRender", item.Load_D3D_GDIRender, _loadUnit));
                Console.WriteLine(listToString("D3D Overlay", item.Load_D3D_Overlay, _loadUnit));
                Console.WriteLine(listToString("Ohters", item.Load_Ohters, _loadUnit));
                Console.WriteLine("");
                // Data
                Console.WriteLine(string.Format(_senserFormat, "Data"));
                Console.WriteLine(parserData("Memory Total", item.Data_Memory_Total, item.SmallData_Memory_Total));
                Console.WriteLine(parserData("Memory Free", item.Data_Memory_Free, item.SmallData_Memory_Free));
                Console.WriteLine(parserData("Memory Used", item.Data_Memory_Used, item.SmallData_Memory_Used));
                Console.WriteLine(parserData("D3D Shared Memory Total", item.Data_D3D_Shared_Memory_Total, item.SmallData_D3D_Shared_Memory_Total));
                Console.WriteLine(parserData("D3D Shared Memory Free", item.Data_D3D_Shared_Memory_Free, item.SmallData_D3D_Shared_Memory_Free));
                Console.WriteLine(parserData("D3D Shared Memory Used", item.Data_D3D_Shared_Memory_Used, item.SmallData_D3D_Shared_Memory_Used));
                Console.WriteLine(string.Format(_senserFormat, "Throughput"));
                Console.WriteLine(dataCheck("PCIe Rx", item.Throughput_PCIe_Rx / 1024, "KB"));
                Console.WriteLine(dataCheck("PCIe Tx", item.Throughput_PCIe_Tx / 1024, "KB"));
                Console.WriteLine(_newHardwareFormat);
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}

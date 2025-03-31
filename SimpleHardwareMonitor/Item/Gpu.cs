using LibreHardwareMonitor.Hardware;
using SimpleHardwareMonitor.Item.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Gpu : AItem<Data.Gpu>
    {
        protected sealed override void Init()
        {
            _data.Load_D3D_3D = new List<float>();
            _data.Load_D3D_VideoDecode = new List<float>();
            _data.Load_D3D_Copy = new List<float>();
            _data.Load_D3D_VideoProcessing = new List<float>();
            _data.Load_D3D_GDIRender = new List<float>();
            _data.Load_D3D_Overlay = new List<float>();
            _data.Load_Ohters = new List<float>();
            FillSensorMethods();
        }

        protected sealed override bool PrevUpdate()
        {
            _data.Load_D3D_3D.Clear();
            _data.Load_D3D_VideoDecode.Clear();
            _data.Load_D3D_Copy.Clear();
            _data.Load_D3D_VideoProcessing.Clear();
            _data.Load_D3D_GDIRender.Clear();
            _data.Load_D3D_Overlay.Clear();
            _data.Load_Ohters.Clear();

            //


            return true;
        }

        public Gpu(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) {
            _data.Gpu_Type = hardWareType.ToString();
        }

        private void FillSensorMethods()
        {
            _updateSensorMethods.Clear();

            // Power
            _updateSensorMethods[SensorType.Power] = new SensorMethodItem() {
                { "gpu power", (ISensor sensor)=>{ _data.Power = sensor.Value ?? -1; } },
            };

            // Clock
            _updateSensorMethods[SensorType.Clock] = new SensorMethodItem() {
                { "gpu core", (ISensor sensor)=>{ _data.Clock_Core = sensor.Value ?? -1; } },
                { "gpu memory", (ISensor sensor)=>{ _data.Clock_Memory = sensor.Value ?? -1; } },
            };

            // Temperature
            _updateSensorMethods[SensorType.Temperature] = new SensorMethodItem() {
                { "gpu core", (ISensor sensor)=>{ _data.Temperature_Core = sensor.Value ?? -1; } },
                { "gpu hot spot", (ISensor sensor)=>{ _data.Temperature_Hot_Spot = sensor.Value ?? -1; } },
            };

            // Load
            _updateSensorMethods[SensorType.Load] = new SensorMethodItem() {
                { "gpu core", (ISensor sensor)=>{ _data.Load_Core = sensor.Value ?? -1; } },
                { "gpu memory controller", (ISensor sensor)=>{ _data.Load_Memory_Controller = sensor.Value ?? -1; } },
                { "gpu video engine", (ISensor sensor)=>{ _data.Load_Video_Engine = sensor.Value ?? -1; } },
                { "gpu bus", (ISensor sensor)=>{ _data.Load_Bus = sensor.Value ?? -1; } },
                { "gpu memory", (ISensor sensor)=>{ _data.Load_Memory = sensor.Value ?? -1; } },
                { "d3d 3d", (ISensor sensor)=>{ _data.Load_D3D_3D.Add(sensor.Value ?? -1); } },
                { "d3d video decode", (ISensor sensor)=>{ _data.Load_D3D_VideoDecode.Add(sensor.Value ?? -1); } },
                { "d3d copy", (ISensor sensor)=>{ _data.Load_D3D_Copy.Add(sensor.Value ?? -1); } },
                { "d3d video processing", (ISensor sensor)=>{ _data.Load_D3D_VideoProcessing.Add(sensor.Value ?? -1); } },
                { "d3d overlay", (ISensor sensor)=>{ _data.Load_D3D_Overlay.Add(sensor.Value ?? -1); } },
                { "d3d gdi render", (ISensor sensor)=>{ _data.Load_D3D_GDIRender.Add(sensor.Value ?? -1); } },
                { "d3d other", (ISensor sensor)=>{ _data.Load_Ohters.Add(sensor.Value ?? -1); } },
            };

            // Data
            _updateSensorMethods[SensorType.Data] = new SensorMethodItem() {
                { "gpu memory total", (ISensor sensor)=>{ _data.Data_Memory_Total = sensor.Value ?? -1; } },
                { "gpu memory free", (ISensor sensor)=>{ _data.Data_Memory_Free = sensor.Value ?? -1; } },
                { "gpu memory used", (ISensor sensor)=>{ _data.Data_Memory_Used = sensor.Value ?? -1; } },
                { "d3d shared memory total", (ISensor sensor)=>{ _data.Data_D3D_Shared_Memory_Total = sensor.Value ?? -1; } },
                { "d3d shared memory free", (ISensor sensor)=>{ _data.Data_D3D_Shared_Memory_Free = sensor.Value ?? -1; } },
                { "d3d shared memory used", (ISensor sensor)=>{ _data.Data_D3D_Shared_Memory_Used = sensor.Value ?? -1; } },
            };

            // SmallData
            _updateSensorMethods[SensorType.SmallData] = new SensorMethodItem() {
                { "gpu memory total", (ISensor sensor)=>{ _data.SmallData_Memory_Total = sensor.Value ?? -1; } },
                { "gpu memory free", (ISensor sensor)=>{ _data.SmallData_Memory_Free = sensor.Value ?? -1; } },
                { "gpu memory used", (ISensor sensor)=>{ _data.SmallData_Memory_Used = sensor.Value ?? -1; } },
                { "d3d shared memory total", (ISensor sensor)=>{ _data.SmallData_D3D_Shared_Memory_Total = sensor.Value ?? -1; } },
                { "d3d shared memory free", (ISensor sensor)=>{ _data.SmallData_D3D_Shared_Memory_Free = sensor.Value ?? -1; } },
                { "d3d shared memory used", (ISensor sensor)=>{ _data.SmallData_D3D_Shared_Memory_Used = sensor.Value ?? -1; } },
            };

            // Throughput
            _updateSensorMethods[SensorType.Throughput] = new SensorMethodItem() {
                { "gpu pcie rx", (ISensor sensor)=>{ _data.Throughput_PCIe_Rx = sensor.Value ?? -1; } },
                { "gpu pcie tx", (ISensor sensor)=>{ _data.Throughput_PCIe_Tx = sensor.Value ?? -1; } },
            };

        }

    }
}

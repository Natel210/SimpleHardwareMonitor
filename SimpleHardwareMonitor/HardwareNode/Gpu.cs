using System.Collections.Generic;
using LibreHardwareMonitor.Hardware;

namespace SimpleHardwareMonitor.HardwareNode
{
    internal class Gpu : AHardwareNode<Model.Gpu>
    {
        internal Gpu(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType)
        {
            _model.Gpu_Type = hardWareType.ToString();
        }

        protected sealed override void Init()
        {
            _model.Load_D3D_3D = new List<float>();
            _model.Load_D3D_VideoDecode = new List<float>();
            _model.Load_D3D_Copy = new List<float>();
            _model.Load_D3D_VideoProcessing = new List<float>();
            _model.Load_D3D_GDIRender = new List<float>();
            _model.Load_D3D_Overlay = new List<float>();
            _model.Load_Ohters = new List<float>();
        }

        protected sealed override bool PrevUpdate()
        {
            _model.Load_D3D_3D.Clear();
            _model.Load_D3D_VideoDecode.Clear();
            _model.Load_D3D_Copy.Clear();
            _model.Load_D3D_VideoProcessing.Clear();
            _model.Load_D3D_GDIRender.Clear();
            _model.Load_D3D_Overlay.Clear();
            _model.Load_Ohters.Clear();
            return true;
        }

        protected sealed override void RegisterPowerSensorMethods()
        {
            _updateSensorMethods[SensorType.Power] = new Functional.SensorMethodItem() {
                { "gpu power", (ISensor sensor)=>{ _model.Power = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterClockSensorMethods()
        {
            _updateSensorMethods[SensorType.Clock] = new Functional.SensorMethodItem() {
                { "gpu core", (ISensor sensor)=>{ _model.Clock_Core = sensor.Value ?? -1; } },
                { "gpu memory", (ISensor sensor)=>{ _model.Clock_Memory = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterTemperatureSensorMethods()
        {
            _updateSensorMethods[SensorType.Temperature] = new Functional.SensorMethodItem() {
                { "gpu core", (ISensor sensor)=>{ _model.Temperature_Core = sensor.Value ?? -1; } },
                { "gpu hot spot", (ISensor sensor)=>{ _model.Temperature_Hot_Spot = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterLoadSensorMethods()
        {
            _updateSensorMethods[SensorType.Load] = new Functional.SensorMethodItem() {
                { "gpu core", (ISensor sensor)=>{ _model.Load_Core = sensor.Value ?? -1; } },
                { "gpu memory controller", (ISensor sensor)=>{ _model.Load_Memory_Controller = sensor.Value ?? -1; } },
                { "gpu video engine", (ISensor sensor)=>{ _model.Load_Video_Engine = sensor.Value ?? -1; } },
                { "gpu bus", (ISensor sensor)=>{ _model.Load_Bus = sensor.Value ?? -1; } },
                { "gpu memory", (ISensor sensor)=>{ _model.Load_Memory = sensor.Value ?? -1; } },
                { "d3d 3d", (ISensor sensor)=>{ _model.Load_D3D_3D.Add(sensor.Value ?? -1); } },
                { "d3d video decode", (ISensor sensor)=>{ _model.Load_D3D_VideoDecode.Add(sensor.Value ?? -1); } },
                { "d3d copy", (ISensor sensor)=>{ _model.Load_D3D_Copy.Add(sensor.Value ?? -1); } },
                { "d3d video processing", (ISensor sensor)=>{ _model.Load_D3D_VideoProcessing.Add(sensor.Value ?? -1); } },
                { "d3d overlay", (ISensor sensor)=>{ _model.Load_D3D_Overlay.Add(sensor.Value ?? -1); } },
                { "d3d gdi render", (ISensor sensor)=>{ _model.Load_D3D_GDIRender.Add(sensor.Value ?? -1); } },
                { "d3d other", (ISensor sensor)=>{ _model.Load_Ohters.Add(sensor.Value ?? -1); } },
            };
        }

        protected sealed override void RegisterDataSensorMethods()
        {
            _updateSensorMethods[SensorType.Data] = new Functional.SensorMethodItem() {
                { "gpu memory total", (ISensor sensor)=>{ _model.Data_Memory_Total = sensor.Value ?? -1; } },
                { "gpu memory free", (ISensor sensor)=>{ _model.Data_Memory_Free = sensor.Value ?? -1; } },
                { "gpu memory used", (ISensor sensor)=>{ _model.Data_Memory_Used = sensor.Value ?? -1; } },
                { "d3d shared memory total", (ISensor sensor)=>{ _model.Data_D3D_Shared_Memory_Total = sensor.Value ?? -1; } },
                { "d3d shared memory free", (ISensor sensor)=>{ _model.Data_D3D_Shared_Memory_Free = sensor.Value ?? -1; } },
                { "d3d shared memory used", (ISensor sensor)=>{ _model.Data_D3D_Shared_Memory_Used = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterSmallDataSensorMethods()
        {
            _updateSensorMethods[SensorType.SmallData] = new Functional.SensorMethodItem() {
                { "gpu memory total", (ISensor sensor)=>{ _model.SmallData_Memory_Total = sensor.Value ?? -1; } },
                { "gpu memory free", (ISensor sensor)=>{ _model.SmallData_Memory_Free = sensor.Value ?? -1; } },
                { "gpu memory used", (ISensor sensor)=>{ _model.SmallData_Memory_Used = sensor.Value ?? -1; } },
                { "d3d shared memory total", (ISensor sensor)=>{ _model.SmallData_D3D_Shared_Memory_Total = sensor.Value ?? -1; } },
                { "d3d shared memory free", (ISensor sensor)=>{ _model.SmallData_D3D_Shared_Memory_Free = sensor.Value ?? -1; } },
                { "d3d shared memory used", (ISensor sensor)=>{ _model.SmallData_D3D_Shared_Memory_Used = sensor.Value ?? -1; } },
            };
        }

        protected sealed override void RegisterThroughputSensorMethods()
        {
            _updateSensorMethods[SensorType.Throughput] = new Functional.SensorMethodItem() {
                { "gpu pcie rx", (ISensor sensor)=>{ _model.Throughput_PCIe_Rx = sensor.Value ?? -1; } },
                { "gpu pcie tx", (ISensor sensor)=>{ _model.Throughput_PCIe_Tx = sensor.Value ?? -1; } },
            };
        }
    }
}

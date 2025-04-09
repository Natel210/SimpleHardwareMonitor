using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Gpu : AHardwareGroup<Model.Gpu, HardwareNode.Gpu>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Gpu(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Gpu> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Gpu>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Gpu()
                {
                    /*---- [ Common ] ----------------------------------------*/
                    Name = node.Key,
                    Gpu_Type = node.Value.Model.Gpu_Type,

                    /*---- [ Voltage ] ---------------------------------------*/
                    /*---- [ Current ] ---------------------------------------*/

                    /*---- [ Power ] -----------------------------------------*/
                    Power = node.Value.Model.Power,

                    /*---- [ Clock ] -----------------------------------------*/
                    Clock_Core = node.Value.Model.Clock_Core,
                    Clock_Memory = node.Value.Model.Clock_Memory,

                    /*---- [ Temperature ] -----------------------------------*/
                    Temperature_Core = node.Value.Model.Temperature_Core,
                    Temperature_Hot_Spot = node.Value.Model.Temperature_Hot_Spot,

                    /*---- [ Load ] ------------------------------------------*/
                    Load_Core = node.Value.Model.Load_Core,
                    Load_Memory_Controller = node.Value.Model.Load_Memory_Controller,
                    Load_Video_Engine = node.Value.Model.Load_Video_Engine,
                    Load_Bus = node.Value.Model.Load_Bus,
                    Load_Memory = node.Value.Model.Load_Memory,
                    Load_D3D_3D = new List<float>(node.Value.Model.Load_D3D_3D),
                    Load_D3D_VideoDecode = new List<float>(node.Value.Model.Load_D3D_VideoDecode),
                    Load_D3D_Copy = new List<float>(node.Value.Model.Load_D3D_Copy),
                    Load_D3D_VideoProcessing = new List<float>(node.Value.Model.Load_D3D_VideoProcessing),
                    Load_D3D_GDIRender = new List<float>(node.Value.Model.Load_D3D_GDIRender),
                    Load_D3D_Overlay = new List<float>(node.Value.Model.Load_D3D_Overlay),
                    Load_Ohters = new List<float>(node.Value.Model.Load_Ohters),

                    /*---- [ Frequency ] -------------------------------------*/
                    /*---- [ Fan ] -------------------------------------------*/
                    /*---- [ Flow ] ------------------------------------------*/
                    /*---- [ Control ] ---------------------------------------*/
                    /*---- [ Level ] -----------------------------------------*/

                    /*---- [ Data ] ------------------------------------------*/
                    Data_Memory_Total = node.Value.Model.Data_Memory_Total,
                    Data_Memory_Free = node.Value.Model.Data_Memory_Free,
                    Data_Memory_Used = node.Value.Model.Data_Memory_Used,
                    Data_D3D_Shared_Memory_Total = node.Value.Model.Data_D3D_Shared_Memory_Total,
                    Data_D3D_Shared_Memory_Free = node.Value.Model.Data_D3D_Shared_Memory_Free,
                    Data_D3D_Shared_Memory_Used = node.Value.Model.Data_D3D_Shared_Memory_Used,

                    /*---- [ Small Data ] ------------------------------------*/
                    SmallData_Memory_Total = node.Value.Model.SmallData_Memory_Total,
                    SmallData_Memory_Free = node.Value.Model.SmallData_Memory_Free,
                    SmallData_Memory_Used = node.Value.Model.SmallData_Memory_Used,
                    SmallData_D3D_Shared_Memory_Total = node.Value.Model.SmallData_D3D_Shared_Memory_Total,
                    SmallData_D3D_Shared_Memory_Free = node.Value.Model.SmallData_D3D_Shared_Memory_Free,
                    SmallData_D3D_Shared_Memory_Used = node.Value.Model.SmallData_D3D_Shared_Memory_Used,

                    /*---- [ Throughput ] ------------------------------------*/
                    Throughput_PCIe_Rx = node.Value.Model.Throughput_PCIe_Rx,
                    Throughput_PCIe_Tx = node.Value.Model.Throughput_PCIe_Tx,

                    /*---- [ Time Span ] -------------------------------------*/
                    /*---- [ Energy ] ----------------------------------------*/
                    /*---- [ Noise ] -----------------------------------------*/
                };
                resultModelGroup.Add(node.Key, tempModel);
            }
            return resultModelGroup;
        }

    }
}

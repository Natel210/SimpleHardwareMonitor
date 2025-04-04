using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Gpu : AItemList<Data.Gpu, Item.Gpu>
    {
        protected sealed override void AddChild(IHardware hardware)
        {
            _item.Add(hardware.Name, new Item.Gpu(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Data.Gpu> MakeDataListChild()
        {
            var dataList = new Dictionary<string, Data.Gpu>();
            foreach (var item in _item)
            {
                var tempData = new Data.Gpu();
                var getData = item.Value.Data;

                // Common
                tempData.Name = item.Key;
                tempData.Gpu_Type = getData.Gpu_Type;

                // Power
                tempData.Power = getData.Power;

                // Clock
                tempData.Clock_Core = getData.Clock_Core;
                tempData.Clock_Memory = getData.Clock_Memory;

                // Temperature
                tempData.Temperature_Core = getData.Temperature_Core;
                tempData.Temperature_Hot_Spot = getData.Temperature_Hot_Spot;

                // Load
                tempData.Load_Core = getData.Load_Core;
                tempData.Load_Memory_Controller = getData.Load_Memory_Controller;
                tempData.Load_Video_Engine = getData.Load_Video_Engine;
                tempData.Load_Bus = getData.Load_Bus;
                tempData.Load_Memory = getData.Load_Memory;
                tempData.Load_D3D_3D = new List<float>(getData.Load_D3D_3D);
                tempData.Load_D3D_VideoDecode = new List<float>(getData.Load_D3D_VideoDecode);
                tempData.Load_D3D_Copy = new List<float>(getData.Load_D3D_Copy);
                tempData.Load_D3D_VideoProcessing = new List<float>(getData.Load_D3D_VideoProcessing);
                tempData.Load_D3D_GDIRender = new List<float>(getData.Load_D3D_GDIRender);
                tempData.Load_D3D_Overlay = new List<float>(getData.Load_D3D_Overlay);
                tempData.Load_Ohters = new List<float>(getData.Load_Ohters);

                // Data
                tempData.Data_Memory_Total = getData.Data_Memory_Total;
                tempData.Data_Memory_Free = getData.Data_Memory_Free;
                tempData.Data_Memory_Used = getData.Data_Memory_Used;
                tempData.Data_D3D_Shared_Memory_Total = getData.Data_D3D_Shared_Memory_Total;
                tempData.Data_D3D_Shared_Memory_Free = getData.Data_D3D_Shared_Memory_Free;
                tempData.Data_D3D_Shared_Memory_Used = getData.Data_D3D_Shared_Memory_Used;

                // Small Data
                tempData.SmallData_Memory_Total = getData.SmallData_Memory_Total;
                tempData.SmallData_Memory_Free = getData.SmallData_Memory_Free;
                tempData.SmallData_Memory_Used = getData.SmallData_Memory_Used;
                tempData.SmallData_D3D_Shared_Memory_Total = getData.SmallData_D3D_Shared_Memory_Total;
                tempData.SmallData_D3D_Shared_Memory_Free = getData.SmallData_D3D_Shared_Memory_Free;
                tempData.SmallData_D3D_Shared_Memory_Used = getData.SmallData_D3D_Shared_Memory_Used;

                // Throughput
                tempData.Throughput_PCIe_Rx = getData.Throughput_PCIe_Rx;
                tempData.Throughput_PCIe_Tx = getData.Throughput_PCIe_Tx;

                dataList.Add(item.Key, tempData);
            }
            return dataList;
        }

    }
}

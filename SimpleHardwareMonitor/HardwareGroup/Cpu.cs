using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Hardware.Cpu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Cpu : AHardwareGroup<Model.Cpu, HardwareNode.Cpu>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Cpu(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Cpu> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Cpu>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Cpu()
                {
                    /*---- [ Common ] ----------------------------------------*/
                    Name = node.Key,
                    //CoreCount = item.Value.Model.Core_Count,
                    //ProcessorCount = item.Value.Model.Processor_Count,

                    /*---- [ Use ] -------------------------------------------*/
                    //Use = item.Value.Model.Use,
                    //Use_ByThreads = new List<float>(item.Value.Model.Use_ByThreads),

                    /*---- [ Voltage ] ---------------------------------------*/
                    Voltage = node.Value.Model.Voltage,
                    Voltage_ByCore = new Dictionary<int, float>(node.Value.Model.Voltage_ByCore),

                    /*---- [ Current ] ---------------------------------------*/

                    /*---- [ Power ] -----------------------------------------*/
                    Power_Package = node.Value.Model.Power_Package,
                    Power_Cores = node.Value.Model.Power_Cores,
                    Power_Memory = node.Value.Model.Power_Memory,
                    Power_Platform = node.Value.Model.Power_Platform,

                    /*---- [ Clock ] -----------------------------------------*/
                    Clock_Bus_Speed = node.Value.Model.Clock_Bus_Speed,
                    Clock_ByCore = new Dictionary<int, float>(node.Value.Model.Clock_ByCore),

                    /*---- [ Temperature ] -----------------------------------*/
                    Temperature_Package = node.Value.Model.Temperature_Package,
                    Temperature_Max = node.Value.Model.Temperature_Max,
                    Temperature_Average = node.Value.Model.Temperature_Average,
                    Temperature_ByCore = new Dictionary<int, float>(node.Value.Model.Temperature_ByCore),
                    Temperature_Distanceto_Tj_Max_ByCore = new Dictionary<int, float>(node.Value.Model.Temperature_Distanceto_Tj_Max_ByCore),

                    /*---- [ Load ] ------------------------------------------*/
                    Load_Total = node.Value.Model.Load_Total,
                    Load_Core_Max = node.Value.Model.Load_Core_Max,
                    //Load_ByThreads = new Dictionary<int, Dictionary<int, float>(), // custom add setting

                    /*---- [ Frequency ] -------------------------------------*/
                    /*---- [ Fan ] -------------------------------------------*/
                    /*---- [ Flow ] ------------------------------------------*/
                    /*---- [ Control ] ---------------------------------------*/
                    /*---- [ Level ] -----------------------------------------*/
                    /*---- [ Data ] ------------------------------------------*/
                    /*---- [ Small Data ] ------------------------------------*/
                    /*---- [ Throughput ] ------------------------------------*/
                    /*---- [ Time Span ] -------------------------------------*/
                    /*---- [ Energy ] ----------------------------------------*/
                    /*---- [ Noise ] -----------------------------------------*/
                };

                // Add Load ByThreads
                var deepCopyLoad_ByThreads = new Dictionary<int, Dictionary<int, float>>();
                foreach (var core in node.Value.Model.Load_ByThreads)
                    deepCopyLoad_ByThreads[core.Key] = new Dictionary<int, float>(core.Value);
                tempModel.Load_ByThreads = deepCopyLoad_ByThreads;

                resultModelGroup.Add(node.Key, tempModel);
            }
            return resultModelGroup;
        }
    }
}

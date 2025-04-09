using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Memory : AHardwareGroup<Model.Memory, HardwareNode.Memory>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Memory(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Memory> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Memory>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Memory()
                {
                    /*---- [ Common ] ----------------------------------------*/
                    Name = node.Key,

                    /*---- [ Voltage ] ---------------------------------------*/
                    /*---- [ Current ] ---------------------------------------*/
                    /*---- [ Power ] -----------------------------------------*/
                    /*---- [ Clock ] -----------------------------------------*/
                    /*---- [ Temperature ] -----------------------------------*/

                    /*---- [ Load ] ------------------------------------------*/
                    Load_Memory = node.Value.Model.Load_Memory,
                    Load_Virtual_Memory = node.Value.Model.Load_Virtual_Memory,

                    /*---- [ Frequency ] -------------------------------------*/
                    /*---- [ Fan ] -------------------------------------------*/
                    /*---- [ Flow ] ------------------------------------------*/
                    /*---- [ Control ] ---------------------------------------*/
                    /*---- [ Level ] -----------------------------------------*/

                    /*---- [ Data ] ------------------------------------------*/
                    Data_Used = node.Value.Model.Data_Used,
                    Data_Available = node.Value.Model.Data_Available,
                    Data_Virtual_Used = node.Value.Model.Data_Virtual_Used,
                    Data_Virtual_Available = node.Value.Model.Data_Virtual_Available,

                    /*---- [ Small Data ] ------------------------------------*/
                    SmallData_Used = node.Value.Model.SmallData_Used,
                    SmallData_Available = node.Value.Model.SmallData_Available,
                    SmallData_Virtual_Used = node.Value.Model.SmallData_Virtual_Used,
                    SmallData_Virtual_Available = node.Value.Model.SmallData_Virtual_Available,

                    /*---- [ Throughput ] ------------------------------------*/
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

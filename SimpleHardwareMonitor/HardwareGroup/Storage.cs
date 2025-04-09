using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Storage : AHardwareGroup<Model.Storage, HardwareNode.Storage>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Storage(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Storage> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Storage>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Storage()
                {
                    /*---- [ Common ] ----------------------------------------*/
                    Name = node.Key,

                    /*---- [ Voltage ] ---------------------------------------*/
                    /*---- [ Current ] ---------------------------------------*/
                    /*---- [ Power ] -----------------------------------------*/
                    /*---- [ Clock ] -----------------------------------------*/

                    /*---- [ Temperature ] -----------------------------------*/
                    Temperature = new List<float>(node.Value.Model.Temperature),

                    /*---- [ Load ] ------------------------------------------*/
                    Load_Used_Space = node.Value.Model.Load_Used_Space,
                    Load_Read_Activity = node.Value.Model.Load_Read_Activity,
                    Load_Write_Activity = node.Value.Model.Load_Write_Activity,
                    Load_Total_Activity = node.Value.Model.Load_Total_Activity,

                    /*---- [ Frequency ] -------------------------------------*/
                    /*---- [ Fan ] -------------------------------------------*/
                    /*---- [ Flow ] ------------------------------------------*/
                    /*---- [ Control ] ---------------------------------------*/

                    /*---- [ Level ] -----------------------------------------*/
                    Level_Available_Spare = node.Value.Model.Level_Available_Spare,
                    Level_Available_Spare_Threshold = node.Value.Model.Level_Available_Spare_Threshold,
                    Level_Percentage_Used = node.Value.Model.Level_Percentage_Used,

                    /*---- [ Data ] ------------------------------------------*/
                    Data_Read = node.Value.Model.Data_Read,
                    Data_Written = node.Value.Model.Data_Written,

                    /*---- [ Small Data ] ------------------------------------*/
                    SmallData_Read = node.Value.Model.SmallData_Read,
                    SmallData_Written = node.Value.Model.SmallData_Written,

                    /*---- [ Throughput ] ------------------------------------*/
                    Throughput_Read_Rate = node.Value.Model.Throughput_Read_Rate,
                    Throughput_Write_Rate = node.Value.Model.Throughput_Write_Rate,

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

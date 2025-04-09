using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Battery : AHardwareGroup<Model.Battery, HardwareNode.Battery>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Battery(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Battery> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Battery>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Battery()
                {
                    /*---- [ Common ] ----------------------------------------*/
                    Name = node.Key,

                    /*---- [ Voltage ] ---------------------------------------*/
                    Voltage = node.Value.Model.Voltage,

                    /*---- [ Current ] ---------------------------------------*/
                    Current_Charge_Discharge = node.Value.Model.Current_Charge_Discharge,

                    /*---- [ Power ] -----------------------------------------*/
                    Power_Charge_Discharge_Rate = node.Value.Model.Power_Charge_Discharge_Rate,

                    /*---- [ Clock ] -----------------------------------------*/
                    /*---- [ Temperature ] -----------------------------------*/
                    /*---- [ Load ] ------------------------------------------*/
                    /*---- [ Frequency ] -------------------------------------*/
                    /*---- [ Fan ] -------------------------------------------*/
                    /*---- [ Flow ] ------------------------------------------*/
                    /*---- [ Control ] ---------------------------------------*/

                    /*---- [ Level ] -----------------------------------------*/
                    Level_Degradation = node.Value.Model.Level_Degradation,
                    Level_Charge = node.Value.Model.Level_Charge,

                    /*---- [ Data ] ------------------------------------------*/
                    /*---- [ Small Data ] ------------------------------------*/
                    /*---- [ Throughput ] ------------------------------------*/
                    /*---- [ Time Span ] -------------------------------------*/

                    /*---- [ Energy ] ----------------------------------------*/
                    Energy_Designed_Capacity = node.Value.Model.Energy_Designed_Capacity,
                    Energy_Fully_Charged_Capacity = node.Value.Model.Energy_Fully_Charged_Capacity,
                    Energy_Remaining_Capacity = node.Value.Model.Energy_Remaining_Capacity,

                    /*---- [ Noise ] -----------------------------------------*/
                };
                resultModelGroup.Add(node.Key, tempModel);
            }
            return resultModelGroup;
        }
    }
}

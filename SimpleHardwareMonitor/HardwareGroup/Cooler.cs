using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Cooler : AHardwareGroup<Model.Cooler, HardwareNode.Cooler>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Cooler(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Cooler> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Cooler>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Cooler()
                {
                    /*---- [ Common ] ----------------------------------------*/
                    Name = node.Key,

                    /*---- [ Voltage ] ---------------------------------------*/
                    /*---- [ Current ] ---------------------------------------*/
                    /*---- [ Power ] -----------------------------------------*/
                    /*---- [ Clock ] -----------------------------------------*/
                    /*---- [ Temperature ] -----------------------------------*/
                    /*---- [ Load ] ------------------------------------------*/
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
                resultModelGroup.Add(node.Key, tempModel);
            }
            return resultModelGroup;
        }
    }
}

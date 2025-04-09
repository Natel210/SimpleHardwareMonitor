using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Network : AHardwareGroup<Model.Network, HardwareNode.Network>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Network(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Network> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Network>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Network()
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

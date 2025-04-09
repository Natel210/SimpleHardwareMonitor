using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.ItemList
{
    internal class EmbeddedController : AHardwareGroup<Model.EmbeddedController, HardwareNode.EmbeddedController>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.EmbeddedController(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.EmbeddedController> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.EmbeddedController>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.EmbeddedController()
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

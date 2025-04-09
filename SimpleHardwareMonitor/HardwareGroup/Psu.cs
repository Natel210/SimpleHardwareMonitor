using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace SimpleHardwareMonitor.ItemList
{
    internal class Psu : AHardwareGroup<Model.Psu, HardwareNode.Psu>
    {
        protected sealed override void AddNodeGroupChild(IHardware hardware)
        {
            _nodeGroup.Add(hardware.Name, new HardwareNode.Psu(hardware.Name, hardware.HardwareType));
        }

        protected sealed override Dictionary<string, Model.Psu> GetNodeGroupChild()
        {
            var resultModelGroup = new Dictionary<string, Model.Psu>();
            foreach (var node in _nodeGroup)
            {
                var tempModel = new Model.Psu()
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

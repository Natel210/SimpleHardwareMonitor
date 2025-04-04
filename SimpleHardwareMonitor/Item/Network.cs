using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class Network : AItem<Data.Network>
    {
        protected sealed override void Init()
        {
            FillSensorMethods();
        }

        public Network(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

        private void FillSensorMethods()
        {
            _updateSensorMethods.Clear();

            /*---- [ Voltage ] -----------------------------------------------*/
            /*---- [ Current ] -----------------------------------------------*/
            /*---- [ Power ] -------------------------------------------------*/
            /*---- [ Clock ] -------------------------------------------------*/
            /*---- [ Load ] --------------------------------------------------*/
            /*---- [ Frequency ] ---------------------------------------------*/
            /*---- [ Fan ] ---------------------------------------------------*/
            /*---- [ Flow ] --------------------------------------------------*/
            /*---- [ Control ] -----------------------------------------------*/
            /*---- [ Level ] -------------------------------------------------*/
            /*---- [ Data ] --------------------------------------------------*/
            /*---- [ Small Data ] --------------------------------------------*/
            /*---- [ Throughput ] --------------------------------------------*/
            /*---- [ Time Span ] ---------------------------------------------*/
            /*---- [ Energy ] ------------------------------------------------*/
            /*---- [ Noise ] -------------------------------------------------*/
        }
    }
}

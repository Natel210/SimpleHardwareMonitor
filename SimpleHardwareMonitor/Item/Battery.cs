﻿using LibreHardwareMonitor.Hardware;
namespace SimpleHardwareMonitor.Item
{
    internal partial class Battery : AItem<Data.Battery>
    {
        protected sealed override void Init()
        {
            FillSensorMethods();
        }

        public Battery(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

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

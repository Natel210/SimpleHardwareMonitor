﻿using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Item
{
    internal class SuperIO : AItem<Data.SuperIO>
    {
        protected sealed override void Init()
        {
            FillSensorMethods();
        }

        public SuperIO(string hardWareName, HardwareType hardWareType) : base(hardWareName, hardWareType) { }

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

﻿using System.Collections.Generic;
using System.ComponentModel;

namespace SimpleHardwareMonitor.Data
{
    public struct Storage
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature

        /// <summary>
        /// Unit : °C
        /// </summary>
        public List<float> Temperature { get; internal set; }

        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Used_Space { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Read_Activity { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Write_Activity { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Total_Activity { get; internal set; }

        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level

        /// <summary>d
        /// Unit : %
        /// </summary>
        public float Level_Available_Spare { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Level_Available_Spare_Threshold { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Level_Percentage_Used { get; internal set; }

        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Read"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Read { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Written"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Written { get; internal set; }

        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Read"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Read { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Written"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Written { get; internal set; }

        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput

        /// <summary>
        /// Unit : KB/s
        /// </summary>
        public float Throughput_Read_Rate { get; internal set; }

        /// <summary>
        /// Unit : KB/s
        /// </summary>
        public float Throughput_Write_Rate { get; internal set; }

        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        #endregion

    }
}

using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents storage device sensor data model.<br/>
    /// Includes metrics for temperature, usage, throughput, and health levels.
    /// </summary>
    public struct Storage
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name or identifier of the storage device.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for supply voltage to the storage controller or NAND
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for current draw metrics
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for power usage data (read/write or idle)
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Not commonly applicable; reserved for interface clocks if needed
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature

        /// <summary>
        /// Temperature sensors for the storage device.<br/>
        /// Unit: °C
        /// </summary>
        public List<float> Temperature { get; internal set; }

        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load

        /// <summary>
        /// Storage usage as a percentage of total capacity.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Used_Space { get; internal set; }

        /// <summary>
        /// Current read activity load.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Read_Activity { get; internal set; }

        /// <summary>
        /// Current write activity load.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Write_Activity { get; internal set; }

        /// <summary>
        /// Combined read/write activity load.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Total_Activity { get; internal set; }

        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for bus or controller operation frequency
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Typically not applicable to storage
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for cooling systems with fluid sensors, if applicable
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for firmware or system-controlled throttling
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level

        /// <summary>
        /// Indicates remaining spare blocks percentage.<br/>
        /// Unit: %
        /// </summary>
        public float Level_Available_Spare { get; internal set; }

        /// <summary>
        /// Threshold for triggering spare warnings.<br/>
        /// Unit: %
        /// </summary>
        public float Level_Available_Spare_Threshold { get; internal set; }

        /// <summary>
        /// Estimated wear level of the storage device.<br/>
        /// Unit: %
        /// </summary>
        public float Level_Percentage_Used { get; internal set; }

        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data

        /// <summary>
        /// Total data read in gigabytes. If -1, fallback to <see cref="SmallData_Read"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Read { get; internal set; }

        /// <summary>
        /// Total data written in gigabytes. If -1, fallback to <see cref="SmallData_Written"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Written { get; internal set; }

        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data

        /// <summary>
        /// Total data read in megabytes. If -1, fallback to <see cref="Data_Read"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Read { get; internal set; }

        /// <summary>
        /// Total data written in megabytes. If -1, fallback to <see cref="Data_Written"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Written { get; internal set; }

        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput

        /// <summary>
        /// Real-time read rate.<br/>
        /// Unit: KB/s
        /// </summary>
        public float Throughput_Read_Rate { get; internal set; }

        /// <summary>
        /// Real-time write rate.<br/>
        /// Unit: KB/s
        /// </summary>
        public float Throughput_Write_Rate { get; internal set; }

        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for uptime or accumulated access time
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for energy consumed over time
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for acoustic/vibration noise (if applicable)
        #endregion
    }
}

using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents CPU-related sensor data model.
    /// </summary>
    public struct Cpu
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name of the CPU.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage

        /// <summary>
        /// Overall CPU voltage.<br/>
        /// Unit: V (Volts)
        /// </summary>
        public float Voltage { get; internal set; }

        /// <summary>
        /// Per-core voltage values.<br/>
        /// Key: Core index, Value: Voltage in V.
        /// </summary>
        public Dictionary<int, float> Voltage_ByCore { get; internal set; }

        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for CPU current draw information
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power

        /// <summary>
        /// Total power consumption of the CPU package.<br/>
        /// Unit: W (Watts)
        /// </summary>
        public float Power_Package { get; internal set; }

        /// <summary>
        /// Power consumption by CPU cores.<br/>
        /// Unit: W (Watts)
        /// </summary>
        public float Power_Cores { get; internal set; }

        /// <summary>
        /// Power consumption by memory controller.<br/>
        /// Unit: W (Watts)
        /// </summary>
        public float Power_Memory { get; internal set; }

        /// <summary>
        /// Power consumption by the platform (e.g., uncore, SoC).<br/>
        /// Unit: W (Watts)
        /// </summary>
        public float Power_Platform { get; internal set; }

        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock

        /// <summary>
        /// CPU bus speed.<br/>
        /// Unit: MHz
        /// </summary>
        public float Clock_Bus_Speed { get; internal set; }

        /// <summary>
        /// Per-core clock speeds.<br/>
        /// Key: Core index, Value: Clock speed in MHz.
        /// </summary>
        public Dictionary<int, float> Clock_ByCore { get; internal set; }

        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature

        /// <summary>
        /// Overall CPU package temperature.<br/>
        /// Unit: °C
        /// </summary>
        public float Temperature_Package { get; internal set; }

        /// <summary>
        /// Maximum core temperature.<br/>
        /// Unit: °C
        /// </summary>
        public float Temperature_Max { get; internal set; }

        /// <summary>
        /// Average temperature across all cores.<br/>
        /// Unit: °C
        /// </summary>
        public float Temperature_Average { get; internal set; }

        /// <summary>
        /// Per-core temperature readings.<br/>
        /// Key: Core index, Value: Temperature in °C.
        /// </summary>
        public Dictionary<int, float> Temperature_ByCore { get; internal set; }

        /// <summary>
        /// Distance to TjMax (thermal junction max) per core.<br/>
        /// Key: Core index, Value: Distance in °C.
        /// </summary>
        public Dictionary<int, float> Temperature_Distanceto_Tj_Max_ByCore { get; internal set; }

        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load

        /// <summary>
        /// Total CPU usage.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Total { get; internal set; }

        /// <summary>
        /// Maximum usage among cores or threads.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Core_Max { get; internal set; }

        /// <summary>
        /// Per-thread CPU usage.<br/>
        /// First key: Core index, Second key: Thread index, Value: Load %.
        /// </summary>
        public Dictionary<int, Dictionary<int, float>> Load_ByThreads { get; internal set; }

        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for CPU frequency-related metrics
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Not directly applicable to CPU
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for liquid-cooled CPU flow metrics
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for CPU fan/power control data
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for CPU operational levels or thresholds
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for bulk CPU data readings
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for smaller data points or summaries
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for performance throughput metrics
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for duration-based performance or load metrics
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for energy consumption tracking
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Not applicable to CPU directly
        #endregion

    }
}
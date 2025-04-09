using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents memory-related sensor data model including load and usage metrics.
    /// </summary>
    public struct Memory
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name or type of the memory module.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for memory voltage metrics (e.g., DIMM voltage)
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for current draw metrics
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for power consumption values
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Reserved for memory frequency or clock speed
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for memory temperature sensors (if available)
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load

        /// <summary>
        /// Current physical memory load.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Memory { get; internal set; }

        /// <summary>
        /// Current virtual memory load.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Virtual_Memory { get; internal set; }

        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for operational memory frequency
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Not applicable for memory modules
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Not applicable for memory modules
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for control settings or states
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for memory wear level or usage level (if measurable)
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data

        /// <summary>
        /// Used physical memory. If -1, fallback to <see cref="SmallData_Used"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Used { get; internal set; }

        /// <summary>
        /// Available physical memory. If -1, fallback to <see cref="SmallData_Available"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Available { get; internal set; }

        /// <summary>
        /// Used virtual memory. If -1, fallback to <see cref="SmallData_Virtual_Used"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Virtual_Used { get; internal set; }

        /// <summary>
        /// Available virtual memory. If -1, fallback to <see cref="SmallData_Virtual_Available"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Virtual_Available { get; internal set; }

        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data

        /// <summary>
        /// Used physical memory (small scale). If -1, fallback to <see cref="Data_Used"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Used { get; internal set; }

        /// <summary>
        /// Available physical memory (small scale). If -1, fallback to <see cref="Data_Available"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Available { get; internal set; }

        /// <summary>
        /// Used virtual memory (small scale). If -1, fallback to <see cref="Data_Virtual_Used"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Virtual_Used { get; internal set; }

        /// <summary>
        /// Available virtual memory (small scale). If -1, fallback to <see cref="Data_Virtual_Available"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Virtual_Available { get; internal set; }

        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for memory bandwidth or data throughput
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for operational time tracking
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for energy consumption metrics
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Not applicable to memory modules
        #endregion

    }
}

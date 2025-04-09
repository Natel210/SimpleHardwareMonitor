using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents Embedded Controller-related sensor data model.<br/>
    /// Typically used to access low-level system management sensors.
    /// </summary>
    public struct EmbeddedController
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name or identifier of the embedded controller.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for voltage metrics measured via embedded controller
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for current flow readings (e.g., system rails)
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for system power readings through the embedded controller
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Not typically applicable to embedded controllers
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for temperature sensors managed by the embedded controller
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load
        // Reserved for resource usage or system load stats
        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for frequency data (if applicable)
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Reserved for fan control/speed monitoring data
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for fluid or airflow monitoring (e.g., in thermal systems)
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for control signal values (e.g., fan duty cycle)
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for sensor level indicators or thresholds
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for large structured sensor data sets
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for compact or summarized data values
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for throughput metrics (e.g., data bus monitoring)
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for duration-based metrics (e.g., uptime)
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for accumulated energy metrics
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for acoustic or electrical noise data (if measurable)
        #endregion
    }
}
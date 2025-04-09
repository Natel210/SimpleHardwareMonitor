using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents power supply unit (PSU)-related sensor data model.<br/>
    /// Structured to include voltage, current, power, fan speed, and energy metrics.
    /// </summary>
    public struct Psu
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name or identifier of the power supply unit.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for PSU output voltage rails (e.g., 12V, 5V, 3.3V)
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for PSU current output readings per rail
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for total power delivery or per-rail power data
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Not typically applicable to PSU
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for PSU internal temperature sensors
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load
        // Reserved for PSU load percentages or wattage draw
        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for AC frequency or switching frequency (if measurable)
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Reserved for PSU fan speed monitoring
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Not applicable unless dealing with liquid-cooled PSUs
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for fan control or PSU power-saving mode control
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for power levels or regulation quality metrics
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for bulk data collection or vendor telemetry output
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for short-term summaries or alerts
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Typically not applicable; may represent power throughput in high-end PSUs
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for uptime, active hours, or protection events duration
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for cumulative energy delivered or consumed
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for electrical noise or ripple data (advanced PSUs)
        #endregion
    }
}
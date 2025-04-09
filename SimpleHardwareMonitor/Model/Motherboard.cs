using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents motherboard-related sensor data model.<br/>
    /// Structured to accommodate voltage, temperature, fan, and other board-level telemetry.
    /// </summary>
    public struct Motherboard
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name or identifier of the motherboard.
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for motherboard voltage readings (e.g., VCORE, 12V rail)
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for motherboard current draw measurements
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power
        // Reserved for board-level power consumption data
        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock
        // Reserved for clock generator or chipset clock data
        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature
        // Reserved for thermal sensor readings on the motherboard
        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load
        // Reserved for system load indicators managed via motherboard sensors
        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for frequency-specific values like BCLK or FSB
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Reserved for fan speed monitoring (e.g., SYS_FAN1, CPU_FAN)
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for fluid flow sensors on liquid-cooled motherboards
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for fan control signals or PWM percentages
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for level-based sensors (e.g., battery level, signal level)
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data
        // Reserved for structured data or firmware-level sensor outputs
        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data
        // Reserved for compact memory, flash data, or micro telemetry
        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput
        // Reserved for bus throughput (e.g., PCIe, memory bus)
        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for uptime or activity duration sensors
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for cumulative energy usage metrics
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for electrical or acoustic noise measurements
        #endregion
    }
}
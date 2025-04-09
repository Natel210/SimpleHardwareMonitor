using System.Collections.Generic;

namespace SimpleHardwareMonitor.Model
{
    /// <summary>
    /// Represents GPU (Graphics Processing Unit) sensor data model.<br/>
    /// Includes power usage, temperature, load, memory usage, and D3D engine metrics.
    /// </summary>
    public struct Gpu
    {
        /*---- [ Common ] ----------------------------------------------------*/
        #region Common

        /// <summary>
        /// Name of the GPU.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Type of the GPU (e.g., Nvidia, AMD, Intel).
        /// </summary>
        public string Gpu_Type { get; internal set; }

        #endregion

        /*---- [ Voltage ] ---------------------------------------------------*/
        #region Voltage
        // Reserved for GPU voltage metrics
        #endregion

        /*---- [ Current ] ---------------------------------------------------*/
        #region Current
        // Reserved for current consumption metrics
        #endregion

        /*---- [ Power ] -----------------------------------------------------*/
        #region Power

        /// <summary>
        /// GPU power consumption.<br/>
        /// Unit: W
        /// </summary>
        public float Power { get; internal set; }

        #endregion

        /*---- [ Clock ] -----------------------------------------------------*/
        #region Clock

        /// <summary>
        /// Core clock speed of the GPU.<br/>
        /// Unit: MHz
        /// </summary>
        public float Clock_Core { get; internal set; }

        /// <summary>
        /// Memory clock speed of the GPU.<br/>
        /// Unit: MHz
        /// </summary>
        public float Clock_Memory { get; internal set; }

        #endregion

        /*---- [ Temperature ] -----------------------------------------------*/
        #region Temperature

        /// <summary>
        /// Temperature of the GPU core.<br/>
        /// Unit: °C
        /// </summary>
        public float Temperature_Core { get; internal set; }

        /// <summary>
        /// Hot spot temperature of the GPU.<br/>
        /// Unit: °C
        /// </summary>
        public float Temperature_Hot_Spot { get; internal set; }

        #endregion

        /*---- [ Load ] ------------------------------------------------------*/
        #region Load

        /// <summary>
        /// Overall GPU core load.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Core { get; internal set; }

        /// <summary>
        /// Load on memory controller.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Memory_Controller { get; internal set; }

        /// <summary>
        /// Video engine usage.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Video_Engine { get; internal set; }

        /// <summary>
        /// Bus interface usage.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Bus { get; internal set; }

        /// <summary>
        /// Overall GPU memory load.<br/>
        /// Unit: %
        /// </summary>
        public float Load_Memory { get; internal set; }

        /// <summary>
        /// Load for D3D 3D engine.<br/>
        /// Unit: %
        /// </summary>
        public List<float> Load_D3D_3D { get; internal set; }

        /// <summary>
        /// Load for D3D video decoder.<br/>
        /// Unit: %
        /// </summary>
        public List<float> Load_D3D_VideoDecode { get; internal set; }

        /// <summary>
        /// Load for D3D copy engine.<br/>
        /// Unit: %
        /// </summary>
        public List<float> Load_D3D_Copy { get; internal set; }

        /// <summary>
        /// Load for D3D video processing engine.<br/>
        /// Unit: %
        /// </summary>
        public List<float> Load_D3D_VideoProcessing { get; internal set; }

        /// <summary>
        /// Load for D3D GDI rendering.<br/>
        /// Unit: %
        /// </summary>
        public List<float> Load_D3D_GDIRender { get; internal set; }

        /// <summary>
        /// Load for D3D overlay engine.<br/>
        /// Unit: %
        /// </summary>
        public List<float> Load_D3D_Overlay { get; internal set; }

        /// <summary>
        /// Load for uncategorized GPU usage.<br/>
        /// Unit: %
        /// </summary>
        public List<float> Load_Ohters { get; internal set; }

        #endregion

        /*---- [ Frequency ] -------------------------------------------------*/
        #region Frequency
        // Reserved for frequency-related GPU metrics
        #endregion

        /*---- [ Fan ] -------------------------------------------------------*/
        #region Fan
        // Reserved for fan speed or status
        #endregion

        /*---- [ Flow ] ------------------------------------------------------*/
        #region Flow
        // Reserved for liquid cooler flow rate (if applicable)
        #endregion

        /*---- [ Control ] ---------------------------------------------------*/
        #region Control
        // Reserved for fan/PWM control or dynamic tuning
        #endregion

        /*---- [ Level ] -----------------------------------------------------*/
        #region Level
        // Reserved for GPU performance or power level indicators
        #endregion

        /*---- [ Data ] ------------------------------------------------------*/
        #region Data

        /// <summary>
        /// Total GPU memory. If -1, fallback to <see cref="SmallData_Memory_Total"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Memory_Total { get; internal set; }

        /// <summary>
        /// Free GPU memory. If -1, fallback to <see cref="SmallData_Memory_Free"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Memory_Free { get; internal set; }

        /// <summary>
        /// Used GPU memory. If -1, fallback to <see cref="SmallData_Memory_Used"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_Memory_Used { get; internal set; }

        /// <summary>
        /// Total shared D3D memory. If -1, fallback to <see cref="SmallData_D3D_Shared_Memory_Total"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_D3D_Shared_Memory_Total { get; internal set; }

        /// <summary>
        /// Free shared D3D memory. If -1, fallback to <see cref="SmallData_D3D_Shared_Memory_Free"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_D3D_Shared_Memory_Free { get; internal set; }

        /// <summary>
        /// Used shared D3D memory. If -1, fallback to <see cref="SmallData_D3D_Shared_Memory_Used"/>.<br/>
        /// Unit: GB
        /// </summary>
        public float Data_D3D_Shared_Memory_Used { get; internal set; }

        #endregion

        /*---- [ Small Data ] ------------------------------------------------*/
        #region Small Data

        /// <summary>
        /// Total GPU memory (small scale). If -1, fallback to <see cref="Data_Memory_Total"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Memory_Total { get; internal set; }

        /// <summary>
        /// Free GPU memory (small scale). If -1, fallback to <see cref="Data_Memory_Free"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Memory_Free { get; internal set; }

        /// <summary>
        /// Used GPU memory (small scale). If -1, fallback to <see cref="Data_Memory_Used"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_Memory_Used { get; internal set; }

        /// <summary>
        /// Total shared D3D memory (small scale). If -1, fallback to <see cref="Data_D3D_Shared_Memory_Total"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_D3D_Shared_Memory_Total { get; internal set; }

        /// <summary>
        /// Free shared D3D memory (small scale). If -1, fallback to <see cref="Data_D3D_Shared_Memory_Free"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_D3D_Shared_Memory_Free { get; internal set; }

        /// <summary>
        /// Used shared D3D memory (small scale). If -1, fallback to <see cref="Data_D3D_Shared_Memory_Used"/>.<br/>
        /// Unit: MB
        /// </summary>
        public float SmallData_D3D_Shared_Memory_Used { get; internal set; }

        #endregion

        /*---- [ Throughput ] ------------------------------------------------*/
        #region Throughput

        /// <summary>
        /// PCIe receive throughput.<br/>
        /// Unit: KB/s
        /// </summary>
        public float Throughput_PCIe_Rx { get; internal set; }

        /// <summary>
        /// PCIe transmit throughput.<br/>
        /// Unit: KB/s
        /// </summary>
        public float Throughput_PCIe_Tx { get; internal set; }

        #endregion

        /*---- [ Time Span ] -------------------------------------------------*/
        #region Time Span
        // Reserved for tracking GPU uptime or operational time
        #endregion

        /*---- [ Energy ] ----------------------------------------------------*/
        #region Energy
        // Reserved for energy usage metrics
        #endregion

        /*---- [ Noise ] -----------------------------------------------------*/
        #region Noise
        // Reserved for acoustic noise level data
        #endregion

    }
}
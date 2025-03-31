using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleHardwareMonitor.Data
{
    public struct Gpu
    {

        public string Name { get; internal set; }

        public string Gpu_Type { get; internal set; }

        #region Power

        /// <summary>
        /// Unit : W
        /// </summary>
        public float Power { get; internal set; }

        #endregion

        #region Clock

        /// <summary>
        /// Unit : MHz
        /// </summary>
        public float Clock_Core { get; internal set; }

        /// <summary>
        /// Unit : MHz
        /// </summary>
        public float Clock_Memory { get; internal set; }

        #endregion

        #region Temperature

        /// <summary>
        /// Unit : °C
        /// </summary>
        public float Temperature_Core { get; internal set; }

        /// <summary>
        /// Unit : °C
        /// </summary>
        public float Temperature_Hot_Spot { get; internal set; }

        #endregion

        #region Load

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Core { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Memory_Controller { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Video_Engine { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Bus { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public float Load_Memory { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_D3D_3D { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_D3D_VideoDecode { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_D3D_Copy { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_D3D_VideoProcessing { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_D3D_GDIRender { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_D3D_Overlay { get; internal set; }

        /// <summary>
        /// Unit : %
        /// </summary>
        public List<float> Load_Ohters { get; internal set; }

        #endregion

        #region Data

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Memory_Total"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Memory_Total { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Memory_Free"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Memory_Free { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_Memory_Used"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_Memory_Used { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_D3D_Shared_Memory_Total"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_D3D_Shared_Memory_Total { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_D3D_Shared_Memory_Free"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_D3D_Shared_Memory_Free { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="SmallData_D3D_Shared_Memory_Used"/><br/>
        /// Unit : GB
        /// </summary>
        public float Data_D3D_Shared_Memory_Used { get; internal set; }

        #endregion

        #region SmallData

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Memory_Total"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Memory_Total { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Memory_Free"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Memory_Free { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_Memory_Used"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_Memory_Used { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_D3D_Shared_Memory_Total"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_D3D_Shared_Memory_Total { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_D3D_Shared_Memory_Free"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_D3D_Shared_Memory_Free { get; internal set; }

        /// <summary>
        /// If Value is 0, Find <see cref="Data_D3D_Shared_Memory_Used"/><br/>
        /// Unit : MB
        /// </summary>
        public float SmallData_D3D_Shared_Memory_Used { get; internal set; }

        #endregion

        #region Throughput

        /// <summary>
        /// Unit : KB/s
        /// </summary>
        public float Throughput_PCIe_Rx { get; internal set; }

        /// <summary>
        /// Unit : KB/s
        /// </summary>
        public float Throughput_PCIe_Tx { get; internal set; }

        #endregion

    }
}

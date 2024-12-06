using CsvHelper.Configuration.Attributes;
using SimpleFileIO.Log.Csv;

namespace SimpleHardWareDataParser.Rawdata
{
    public partial class RawdataItem
    {
        [Name("Primary")]
        public string Primary { get; set; } = "";
        [Name("Data Time")]
        public DateTime DateTime { get; set; } = new();
        #region Cpu
        [Name("CPU CoreCount")]
        public int CpuCoreCount { get; set; } = 0;
        [Name("CPU ProcessorCount")]
        public int CpuProcessorCount { get; set; } = 0;
        [Name("CPU Use")]
        public float CpuUse { get; set; } = -1;
        [Name("CPU UseByThreads")]
        [TypeConverter(typeof(CSVLogListConverter<float>))]
        public List<float> CpuUseByThreads { get; set; } = [];
        [Name("CPU Voltage")]
        public float CpuVoltage { get; set; } = -1.0f;
        [Name("CPU VoltageByCore")]
        [TypeConverter(typeof(CSVLogListConverter<float>))]
        public List<float> CpuVoltageByCore { get; set; } = [];
        [Name("CPU Power")]
        public float CpuPower { get; set; } = -1.0f;
        [Name("CPU PowerByCore")]
        [TypeConverter(typeof(CSVLogListConverter<float>))]
        public List<float> CpuPowerByCore { get; set; } = [];
        [Name("CPU Temperature")]
        public float CpuTemperature { get; set; } = -1.0f;
        [Name("CPU TemperatureByCore")]
        [TypeConverter(typeof(CSVLogListConverter<float>))]
        public List<float> CpuTemperatureByCore { get; set; } = [];
        #endregion

        /// <summary>
        /// Create a deep copy of the RawdataItem object.
        /// </summary>
        public RawdataItem Clone()
        {
            return new RawdataItem
            {
                Primary = this.Primary,
                DateTime = this.DateTime,
                CpuCoreCount = this.CpuCoreCount,
                CpuProcessorCount = this.CpuProcessorCount,
                CpuUse = this.CpuUse,
                CpuUseByThreads = new List<float>(this.CpuUseByThreads), // Clone the list
                CpuVoltage = this.CpuVoltage,
                CpuVoltageByCore = new List<float>(this.CpuVoltageByCore), // Clone the list
                CpuPower = this.CpuPower,
                CpuPowerByCore = new List<float>(this.CpuPowerByCore), // Clone the list
                CpuTemperature = this.CpuTemperature,
                CpuTemperatureByCore = new List<float>(this.CpuTemperatureByCore) // Clone the list
            };
        }
    }

}

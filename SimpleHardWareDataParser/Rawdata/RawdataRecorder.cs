using System.Collections.Generic;
using System.Numerics;

namespace SimpleHardWareDataParser.Rawdata
{
    /// <summary>
    /// Fix DateTime UTC+00:00
    /// </summary>
    public partial class RawdataRecorder
    {
        public event Action<string>? DataChanged = null;



        public string SplitName
        {
            get => _splitName;
            set
            {
                _splitName = value;
                DataChanged?.Invoke(nameof(SplitName));
            }
        }
        public DateTime StartDateTime
        {
            get => _startDateTime;
            set
            {
                _startDateTime = value;
                DataChanged?.Invoke(nameof(StartDateTime));
            }
        }
        public DateTime EndDateTime
        {
            get => _endDateTime;
            set
            {
                _endDateTime = value;
                DataChanged?.Invoke(nameof(EndDateTime));
            }
        }
        public RawdataItem Average { get => this._avg.Clone(); }
        public RawdataItem Min { get => this._min.Clone(); }
        public RawdataItem Max { get => this._max.Clone(); }
        public Dictionary<DateTime, RawdataItem> Data { get => new(this._data); }

        private string _splitName = "";
        private DateTime _startDateTime = new();
        private DateTime _endDateTime = new();

        private Dictionary<DateTime, RawdataItem> _data = new Dictionary<DateTime, RawdataItem>();
        private RawdataItem _sum = new();
        private RawdataItem _avg = new();
        private RawdataItem _min = new();
        private RawdataItem _max = new();

        public RawdataRecorder() { }

        public void SetData(DateTime startDateTime, DateTime endDateTime,Dictionary<DateTime, RawdataItem> data)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            SetData(data);
        }

        public void SetData(Dictionary<DateTime, RawdataItem> data)
        {
            _data = data.Where(x => x.Key >= StartDateTime && x.Key < EndDateTime).ToDictionary(x => x.Key, d => d.Value);

            if (_data.Count is 0)
                return;
            if (_data.Count is 1)
            {
                _sum = _data.First().Value;
                _avg = _data.First().Value;
                _min = _data.First().Value;
                _max = _data.First().Value;
                return;
            }

            int cpuCoreCount = _data.First().Value.CpuCoreCount;
            int cpuThreadCount = _data.First().Value.CpuProcessorCount;

            _sum = MakeRawdataResultItem(cpuCoreCount, cpuThreadCount);
            _avg = MakeRawdataResultItem(cpuCoreCount, cpuThreadCount);
            _min = MakeRawdataResultItem(cpuCoreCount, cpuThreadCount);
            _max = MakeRawdataResultItem(cpuCoreCount, cpuThreadCount);

            foreach (var item in _data.Values)
                CalculateMinMaxSum(item, cpuCoreCount, cpuThreadCount);

            CalculateAvg(_data.Count, cpuCoreCount, cpuThreadCount);

            DataChanged?.Invoke(nameof(Data));
        }

        
        private RawdataItem MakeRawdataResultItem(int cpuCoreCount, int cpuThreadCount)
        {
            var rawdata = new RawdataItem()
            {
                CpuCoreCount = cpuCoreCount,
                CpuProcessorCount = cpuThreadCount,
                CpuUseByThreads = new(cpuThreadCount),
                CpuVoltageByCore = new(cpuCoreCount),
                CpuPowerByCore = new(cpuCoreCount),
                CpuTemperatureByCore = new(cpuCoreCount),
            };
            rawdata.CpuUseByThreads.AddRange(Enumerable.Repeat(-1.0f, cpuThreadCount));
            rawdata.CpuVoltageByCore.AddRange(Enumerable.Repeat(-1.0f, cpuCoreCount));
            rawdata.CpuPowerByCore.AddRange(Enumerable.Repeat(-1.0f, cpuCoreCount));
            rawdata.CpuTemperatureByCore.AddRange(Enumerable.Repeat(-1.0f, cpuCoreCount));

            return rawdata;
        }

        private void CalculateMinMaxSum(RawdataItem item, int cpuCoreCount, int cpuThreadCount)
        {
            // If Check 0 or -1, add correction.
            // 1. dest 0 or -1 follows previous information
            // 2. If dest is not 0 or-1 and src is 0 or -1, the added information is followed.
            // 3. If not both, determine min and then decide
            var floatMinCheck = (float src, float dest) =>
            {
                if (dest is 0 || dest is -1)
                    return src;
                else if (src is 0 || src is -1)
                    return dest;
                else
                    return Math.Min(src, dest);
            };

            // CPU Use
            _min.CpuUse = floatMinCheck(_min.CpuUse, item.CpuUse);
            _sum.CpuUse += item.CpuUse;
            _max.CpuUse = Math.Max(_max.CpuUse, item.CpuUse);

            // CPU Use by threads
            for (int i = 0; i < cpuThreadCount; ++i)
            {
                _min.CpuUseByThreads[i] = floatMinCheck(_min.CpuUseByThreads[i], item.CpuUseByThreads[i]);
                _sum.CpuUseByThreads[i] += item.CpuUseByThreads[i];
                _max.CpuUseByThreads[i] = Math.Max(_max.CpuUseByThreads[i], item.CpuUseByThreads[i]);
            }

            // CPU Voltage
            _min.CpuVoltage = floatMinCheck(_min.CpuVoltage, item.CpuVoltage);
            _sum.CpuVoltage += item.CpuVoltage;
            _max.CpuVoltage = Math.Max(_max.CpuVoltage, item.CpuVoltage);

            // CPU Voltage by core
            for (int i = 0; i < cpuCoreCount; ++i)
            {
                _min.CpuVoltageByCore[i] = floatMinCheck(_min.CpuVoltageByCore[i], item.CpuVoltageByCore[i]);
                _sum.CpuVoltageByCore[i] += item.CpuVoltageByCore[i];
                _max.CpuVoltageByCore[i] = Math.Max(_max.CpuVoltageByCore[i], item.CpuVoltageByCore[i]);
            }

            // CPU Power
            _min.CpuPower = floatMinCheck(_min.CpuPower, item.CpuPower);
            _sum.CpuPower += item.CpuPower;
            _max.CpuPower = Math.Max(_max.CpuPower, item.CpuPower);

            // CPU Power by core
            for (int i = 0; i < cpuCoreCount; ++i)
            {
                _min.CpuPowerByCore[i] = floatMinCheck(_min.CpuPowerByCore[i], item.CpuPowerByCore[i]);
                _sum.CpuPowerByCore[i] += item.CpuPowerByCore[i];
                _max.CpuPowerByCore[i] = Math.Max(_max.CpuPowerByCore[i], item.CpuPowerByCore[i]);
            }

            // CPU Temperature
            _min.CpuTemperature = floatMinCheck(_min.CpuTemperature, item.CpuTemperature);
            _sum.CpuTemperature += item.CpuTemperature;
            _max.CpuTemperature = Math.Max(_max.CpuTemperature, item.CpuTemperature);

            // CPU Temperature by core
            for (int i = 0; i < cpuCoreCount; ++i)
            {
                _min.CpuTemperatureByCore[i] = floatMinCheck(_min.CpuTemperatureByCore[i], item.CpuTemperatureByCore[i]);
                _sum.CpuTemperatureByCore[i] += item.CpuTemperatureByCore[i];
                _max.CpuTemperatureByCore[i] = Math.Max(_max.CpuTemperatureByCore[i], item.CpuTemperatureByCore[i]);
            }
        }

        private void CalculateAvg(int itemCount, int cpuCoreCount, int cpuThreadCount)
        {
            

            _sum.CpuUse /= itemCount;
            for (int i = 0; i < cpuThreadCount; ++i)
                _sum.CpuUseByThreads[i] /= itemCount;

            _sum.CpuVoltage /= itemCount;
            for (int i = 0; i < cpuCoreCount; ++i)
                _sum.CpuVoltageByCore[i] /= itemCount;

            _sum.CpuPower /= itemCount;
            for (int i = 0; i < cpuCoreCount; ++i)
                _sum.CpuPowerByCore[i] /= itemCount;

            _sum.CpuTemperature /= itemCount;
            for (int i = 0; i < cpuCoreCount; ++i)
                _sum.CpuTemperatureByCore[i] /= itemCount;
        }
    }
}

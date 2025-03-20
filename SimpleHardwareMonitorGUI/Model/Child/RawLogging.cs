using System.ComponentModel;
using System.IO;
using SimpleFileIO.Log.Csv;
using SimpleFileIO.Utility;
using SimpleHardwareMonitorGUI.Common.SyncThread;

namespace SimpleHardwareMonitorGUI.Model.Child
{
    public partial class RawLogging : INotifyPropertyChanged
    {
        public RawLoggingItem.Interval LoggingInterval
        {
            get => _loggingInterval.Value;
            set
            {
                if (EqualityComparer<RawLoggingItem.Interval>.Default.Equals(_loggingInterval.Value, value))
                    return;
                _loggingInterval.Value = value;
                UpdatePathProperty();
                ResetLoggingInterval();
                OnPropertyChanged(nameof(LoggingInterval));
            }
        }
        public bool EnableAutoSave_ProgramStartup
        {
            get => _enableAutoSave_ProgramStartup.Value;
            set
            {
                if (EqualityComparer<bool>.Default.Equals(_enableAutoSave_ProgramStartup.Value, value))
                    return;
                _enableAutoSave_ProgramStartup.Value = value;
                OnPropertyChanged(nameof(EnableAutoSave_ProgramStartup));
            }
            
        }
        public string SaveRootDirectory
        {
            get => _saveRootDirectory.Value;
            set
            {
                if (EqualityComparer<string>.Default.Equals(_saveRootDirectory.Value, value))
                    return;
                _saveRootDirectory.Value = value;
                UpdatePathProperty();
                OnPropertyChanged(nameof(SaveRootDirectory));
            }
        }

        public bool IsLoggingRunning
        {
            get => _isLoggingRunning.Value;
            set
            {
                if (EqualityComparer<bool>.Default.Equals(_isLoggingRunning.Value, value))
                    return;
                _isLoggingRunning.Value = value;
                ResetLoggingInterval();
                OnPropertyChanged(nameof(IsLoggingRunning));
            }
        }

        // Member
        private ISync<RawLoggingItem.Interval> _loggingInterval = new SyncValue<RawLoggingItem.Interval>(loggingIntervalDefault);
        private ISync<bool> _enableAutoSave_ProgramStartup = new SyncValue<bool>(enableAutoSave_ProgramStartupDefault);
        private ISync<string> _saveRootDirectory = new SyncString(saveRootDirectoryDefault);
        private ISync<bool> _isLoggingRunning = new SyncValue<bool>(_isLoggingRunningDefault);

        // Default
        internal const RawLoggingItem.Interval loggingIntervalDefault = RawLoggingItem.Interval.Sec01;
        internal const bool enableAutoSave_ProgramStartupDefault = false;
        internal const string saveRootDirectoryDefault = "./RawData/";
        private const bool _isLoggingRunningDefault = false;

        private RawLoggingItem.LoggingItem FillContent()
        {
            var tempItem = new RawLoggingItem.LoggingItem();
            tempItem.DateTime = DateTime.UtcNow;
            if (SimpleHardwareMonitor.HardwareMonitor.Cpu is not null)
            {
                tempItem.CpuCoreCount = SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.CoreCount;
                tempItem.CpuProcessorCount = SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.ProcessorCount;
                tempItem.CpuUse = SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.Use;
                tempItem.CpuUseByThreads = new List<float>(SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.UseByThreads);
                tempItem.CpuVoltage = SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.Voltage;
                tempItem.CpuVoltageByCore = new List<float>(SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.VoltageByCore);
                tempItem.CpuPower = SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.Power;
                tempItem.CpuPowerByCore = new List<float>(SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.PowerByCore);
                tempItem.CpuTemperature = SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.Temperature;
                tempItem.CpuTemperatureByCore = new List<float>(SimpleHardwareMonitor.HardwareMonitor.Cpu.Value.TemperatureByCore);
            }
            return tempItem;
        }
    }

    //internal systems 
    public partial class RawLogging
    {
        private Timer _loggingTimer;
        private ICSVLog _csvLog;
        private const string _extension = "rawdata";

        public RawLogging()
        {
            // Creating CsvLog
            var pathProperty = new PathProperty()
            {
                RootDirectory = new("./RawData/"),
                FileName = "temp",
                Extension = _extension
            };

            _csvLog = SimpleFileIO.Manager.CreateCsvLog(nameof(RawLogging), pathProperty)
                ?? throw new InvalidOperationException("Failed to create CsvLog: " + pathProperty.FileName);

            // Start Timer
            _loggingTimer = new Timer(LoggingSystem, null, 0, (int)LoggingInterval);
            ResetLoggingInterval();
        }

        /// <summary>
        /// Update the path property in the internal code.
        /// </summary>
        /// <returns>Is Changed</returns>
        private bool UpdatePathProperty()
        {
            if (_csvLog is null)
                return false;
            if (GlobalModel.Instance is null)
                return false;
            if (GlobalModel.Instance.CommonData is null)
                return false;

            PathProperty tempPathProperty = new();
            var curNow = DateTime.UtcNow;
            DateTime fileTime = new DateTime();

            // Cal Date
            switch (LoggingInterval)
            {
                case RawLoggingItem.Interval.Sec01:
                case RawLoggingItem.Interval.Sec05:
                case RawLoggingItem.Interval.Sec10:
                case RawLoggingItem.Interval.Sec20:
                case RawLoggingItem.Interval.Sec30:
                    fileTime = new DateTime(curNow.Year, curNow.Month, curNow.Day, curNow.Hour, curNow.Minute, 0, 0);
                    break;
                case RawLoggingItem.Interval.Min01:
                case RawLoggingItem.Interval.Min10:
                case RawLoggingItem.Interval.Min20:
                case RawLoggingItem.Interval.Min30:
                    fileTime = new DateTime(curNow.Year, curNow.Month, curNow.Day, curNow.Hour, 0, 0, 0);
                    break;
                case RawLoggingItem.Interval.Hour01:
                    fileTime = new DateTime(curNow.Year, curNow.Month, curNow.Day, 0, 0, 0, 0);
                    break;
                default:
                    break;
            }

            // Make Path Property
            tempPathProperty.RootDirectory = new(Path.Combine(SaveRootDirectory, $"{GlobalModel.Instance.CommonData.MainWindowName}\\{fileTime:yyMMdd}\\"));
            tempPathProperty.FileName = $"{GlobalModel.Instance.CommonData.MainWindowName}_{fileTime:HHmmss}";
            tempPathProperty.Extension = _extension;

            // Check is Changed
            bool isChanged = false;
            if (_csvLog.PathProperty.RootDirectory == tempPathProperty.RootDirectory)
                isChanged = true;
            if (_csvLog.PathProperty.FileName == tempPathProperty.FileName)
                isChanged = true;
            if (_csvLog.PathProperty.Extension == tempPathProperty.Extension)
                isChanged = true;

            // Apply Path Property
            if (isChanged is true)
                _csvLog.PathProperty = tempPathProperty;

            // Result
            return isChanged;
        }

        private void LoggingSystem(object? state)
        {
            if (IsLoggingRunning is false)
                return;
            UpdatePathProperty();
            _csvLog.Add(FillContent());
            if (_csvLog.IsWriting is false)
                _csvLog.Write();
        }

        /// <summary>
        /// Resets the logging interval and calculates the next logging time to update the timer.
        /// </summary>
        private void ResetLoggingInterval()
        {
            TimeSpan currentInterval = TimeSpan.FromMilliseconds((double)LoggingInterval);
            DateTime currentDate = DateTime.UtcNow;
            DateTime nextTime = DateTime.UtcNow;

            //Cal
            // ( (CurTime / Unit) +1 ) * Unit
            // e.g
            // CurTime 12:30:48 Unit:10s
            // ((48 /10) + 1) * 10
            // (4 + 1) * 10
            // 50
            Func<int, int, int> CalNextTime = (currentValue, unit) => (currentValue / unit + 1) * unit;

            switch (LoggingInterval)
            {
                case RawLoggingItem.Interval.Sec01:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0, 0).AddSeconds(CalNextTime(currentDate.Second, 1));
                    break;
                case RawLoggingItem.Interval.Sec05:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0, 0).AddSeconds(CalNextTime(currentDate.Second, 5));
                    break;
                case RawLoggingItem.Interval.Sec10:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0, 0).AddSeconds(CalNextTime(currentDate.Second, 10));
                    break;
                case RawLoggingItem.Interval.Sec20:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0, 0).AddSeconds(CalNextTime(currentDate.Second, 20));
                    break;
                case RawLoggingItem.Interval.Sec30:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0, 0).AddSeconds(CalNextTime(currentDate.Second, 30));
                    break;
                case RawLoggingItem.Interval.Min01:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0, 0).AddMinutes(CalNextTime(currentDate.Minute, 1));
                    break;
                case RawLoggingItem.Interval.Min05:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0, 0).AddMinutes(CalNextTime(currentDate.Minute, 5));
                    break;
                case RawLoggingItem.Interval.Min10:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0, 0).AddMinutes(CalNextTime(currentDate.Minute, 10));
                    break;
                case RawLoggingItem.Interval.Min20:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0, 0).AddMinutes(CalNextTime(currentDate.Minute, 20));
                    break;
                case RawLoggingItem.Interval.Min30:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0, 0).AddMinutes(CalNextTime(currentDate.Minute, 30));
                    break;
                case RawLoggingItem.Interval.Hour01:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 1));
                    break;
                case RawLoggingItem.Interval.Hour02:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 2));
                    break;
                case RawLoggingItem.Interval.Hour03:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 3));
                    break;
                case RawLoggingItem.Interval.Hour04:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 4));
                    break;
                case RawLoggingItem.Interval.Hour06:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 6));
                    break;
                case RawLoggingItem.Interval.Hour08:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 8));
                    break;
                case RawLoggingItem.Interval.Hour12:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 12));
                    break;
                case RawLoggingItem.Interval.Day01:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 0, 0, 0, 0).AddHours(CalNextTime(currentDate.Hour, 24));
                    break;
            }

            TimeSpan waitTime;
            waitTime = nextTime - DateTime.UtcNow;
            _loggingTimer.Change(waitTime, currentInterval);
        }

        static internal string IntervalToString(RawLoggingItem.Interval interval)
        {
            switch (interval)
            {
                case RawLoggingItem.Interval.Sec01:
                    return nameof(RawLoggingItem.Interval.Sec01);
                case RawLoggingItem.Interval.Sec05:
                    return nameof(RawLoggingItem.Interval.Sec05);
                case RawLoggingItem.Interval.Sec10:
                    return nameof(RawLoggingItem.Interval.Sec10);
                case RawLoggingItem.Interval.Sec20:
                    return nameof(RawLoggingItem.Interval.Sec20);
                case RawLoggingItem.Interval.Sec30:
                    return nameof(RawLoggingItem.Interval.Sec30);
                case RawLoggingItem.Interval.Min01:
                    return nameof(RawLoggingItem.Interval.Min01);
                case RawLoggingItem.Interval.Min05:
                    return nameof(RawLoggingItem.Interval.Min05);
                case RawLoggingItem.Interval.Min10:
                    return nameof(RawLoggingItem.Interval.Min10);
                case RawLoggingItem.Interval.Min20:
                    return nameof(RawLoggingItem.Interval.Min20);
                case RawLoggingItem.Interval.Min30:
                    return nameof(RawLoggingItem.Interval.Min30);
                case RawLoggingItem.Interval.Hour01:
                    return nameof(RawLoggingItem.Interval.Hour01);
                case RawLoggingItem.Interval.Hour02:
                    return nameof(RawLoggingItem.Interval.Hour02);
                case RawLoggingItem.Interval.Hour03:
                    return nameof(RawLoggingItem.Interval.Hour03);
                case RawLoggingItem.Interval.Hour04:
                    return nameof(RawLoggingItem.Interval.Hour04);
                case RawLoggingItem.Interval.Hour06:
                    return nameof(RawLoggingItem.Interval.Hour06);
                case RawLoggingItem.Interval.Hour08:
                    return nameof(RawLoggingItem.Interval.Hour08);
                case RawLoggingItem.Interval.Hour12:
                    return nameof(RawLoggingItem.Interval.Hour12);
                case RawLoggingItem.Interval.Day01:
                    return nameof(RawLoggingItem.Interval.Day01);
                default:
                    throw new ArgumentNullException($"Invaild RawLoggingItem.Interval Type {interval.ToString()}");
            }
        }
        static internal RawLoggingItem.Interval StringToInterval(string interval)
        {
            switch (interval)
            {
                case nameof(RawLoggingItem.Interval.Sec01):
                    return RawLoggingItem.Interval.Sec01;
                case nameof(RawLoggingItem.Interval.Sec05):
                    return RawLoggingItem.Interval.Sec05;
                case nameof(RawLoggingItem.Interval.Sec10):
                    return RawLoggingItem.Interval.Sec10;
                case nameof(RawLoggingItem.Interval.Sec20):
                    return RawLoggingItem.Interval.Sec20;
                case nameof(RawLoggingItem.Interval.Sec30):
                    return RawLoggingItem.Interval.Sec30;
                case nameof(RawLoggingItem.Interval.Min01):
                    return RawLoggingItem.Interval.Min01;
                case nameof(RawLoggingItem.Interval.Min05):
                    return RawLoggingItem.Interval.Min05;
                case nameof(RawLoggingItem.Interval.Min10):
                    return RawLoggingItem.Interval.Min10;
                case nameof(RawLoggingItem.Interval.Min20):
                    return RawLoggingItem.Interval.Min20;
                case nameof(RawLoggingItem.Interval.Min30):
                    return RawLoggingItem.Interval.Min30;
                case nameof(RawLoggingItem.Interval.Hour01):
                    return RawLoggingItem.Interval.Hour01;
                case nameof(RawLoggingItem.Interval.Hour02):
                    return RawLoggingItem.Interval.Hour02;
                case nameof(RawLoggingItem.Interval.Hour03):
                    return RawLoggingItem.Interval.Hour03;
                case nameof(RawLoggingItem.Interval.Hour04):
                    return RawLoggingItem.Interval.Hour04;
                case nameof(RawLoggingItem.Interval.Hour06):
                    return RawLoggingItem.Interval.Hour06;
                case nameof(RawLoggingItem.Interval.Hour08):
                    return RawLoggingItem.Interval.Hour08;
                case nameof(RawLoggingItem.Interval.Hour12):
                    return RawLoggingItem.Interval.Hour12;
                case nameof(RawLoggingItem.Interval.Day01):
                    return RawLoggingItem.Interval.Day01;
                default:
                    throw new ArgumentNullException($"Invaild RawLoggingItem.Interval Type {interval}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

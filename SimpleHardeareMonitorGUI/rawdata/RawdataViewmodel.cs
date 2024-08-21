using SimpleHardwareMonitor;
using SimpleHardwareMonitor.data;
using SimpleHardwareMonitorGUI.common;
using SimpleLogger.Interface;
using SimpleLogger.UserProperties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace SimpleHardwareMonitorGUI.rawdata
{
    //static 
    public partial class RawdataViewmodel : AViewModelBase_None
    {
        private static readonly string _loggerName = "rawdataLogger";
        private static readonly string _extensionDefault = "rawdata";
        public static RawdataViewmodel instance = new RawdataViewmodel();
    }

    public partial class RawdataViewmodel : AViewModelBase_None
    {
        public DirectoryInfo RootDirectory
        {
            get => _rootDirectory;
            set
            {
                if(Set(ref _rootDirectory, value, nameof(RootDirectory)) is true)
                    ResettingLoggerProperties();
            }
        }
        public string Extension
        {
            get => _extension;
            set
            {
                if(Set(ref _extension, value, nameof(Extension)) is true)
                    ResettingLoggerProperties();
            }
        }
        public ERawDataInterval LoggingInterval
        {
            get => _loggingInterval;
            set
            {
                if(Set(ref _loggingInterval, value, nameof(LoggingInterval)) is true)
                    ResettingLoggerProperties();
            }
        }
        public bool LoggingEnabled
        {
            get => _loggingEnabled;
            set => Set(ref _loggingEnabled, value, nameof(LoggingEnabled));
        }



    }
    public partial class RawdataViewmodel : AViewModelBase_None
    {


        private void DataLogging()
        {
            var cpuData = HardwareMonitor.Cpu;
            _rawdataLogger.Add($"{DateTime.Now:yyMMdd_HH:mm:ss.fff},{cpuData.Use:000.0},{cpuData.Temperature:000.0},{cpuData.Power:000.0}");
        }

    }
    public partial class RawdataViewmodel : AViewModelBase_None
    {
        private Timer _loggingTimer;
        private ILogger _rawdataLogger;
        private DirectoryInfo _rootDirectory = new DirectoryInfo(".//");
        private string _extension = _extensionDefault;
        private bool _loggingEnabled = false;
        private ERawDataInterval _loggingInterval = ERawDataInterval.s1;
        private RawdataViewmodel()
        {
            _rawdataLogger = SimpleLogger.Main.Builder.Create(_loggerName) ?? throw new Exception("don't create logger...");
            RootDirectory = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".//");
            Extension = _extensionDefault;
            Load_INI();
            _loggingTimer = new Timer(SaveData, null, 0, (int)LoggingInterval);
            RestartLoggingTimer();
        }

        /// <summary>
        /// add ini load
        /// </summary>
        private void Load_INI()
        {
            //if is chatnged
            ResettingLoggerProperties();
        }
        private void SaveData(object? state)
        {
            if (LoggingEnabled is false)
                return;
            ResettingLoggerProperties();
            DataLogging();
            if (_rawdataLogger.IsWriting is false)
                _rawdataLogger.Write();
        }
        private void RestartLoggingTimer()
        {
            TimeSpan currentInterval = TimeSpan.FromMilliseconds((double)LoggingInterval);
            DateTime currentDate = DateTime.Now;
            TimeSpan waitTime;
            DateTime nextTime = DateTime.Now;
            switch (LoggingInterval)
            {
                case ERawDataInterval.ms250:
                case ERawDataInterval.ms500:
                case ERawDataInterval.s1:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, currentDate.Second, 0).AddSeconds(1);
                    break;
                case ERawDataInterval.s5:
                case ERawDataInterval.s10:
                case ERawDataInterval.s20:
                case ERawDataInterval.s30:
                case ERawDataInterval.m1:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0, 0).AddMinutes(1);
                    break;
                case ERawDataInterval.m10:
                case ERawDataInterval.m20:
                case ERawDataInterval.m30:
                case ERawDataInterval.h1:
                    nextTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0, 0).AddHours(1);
                    break;
                default:
                    break;
            }
            waitTime = nextTime - DateTime.Now;
            _loggingTimer.Change(waitTime, currentInterval);
        }
        private void ResettingLoggerProperties()
        {
            LoggerProperties tempProperties = _rawdataLogger.Properties;
            var curNow = DateTime.Now;
            switch (LoggingInterval)
            {
                case ERawDataInterval.ms250:
                case ERawDataInterval.ms500:
                case ERawDataInterval.s1:
                case ERawDataInterval.s5:
                case ERawDataInterval.s10:
                case ERawDataInterval.s20:
                case ERawDataInterval.s30:
                    tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(RootDirectory.FullName, $"{curNow:yyMMdd_HH}\\"));
                    tempProperties.FileName = $"{curNow.Minute:D2}";
                    break;
                case ERawDataInterval.m1:
                case ERawDataInterval.m10:
                case ERawDataInterval.m20:
                case ERawDataInterval.m30:
                    tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(RootDirectory.FullName, "Hour", $"{curNow:yyMMdd}\\"));
                    tempProperties.FileName = $"{curNow.Hour:D2}";
                    break;
                case ERawDataInterval.h1:
                    tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(RootDirectory.FullName,"Day", $"{curNow:yyMM}\\"));
                    tempProperties.FileName = $"{curNow.Day:D2}";
                    break;
                default:
                    break;
            }
            tempProperties.Extension = Extension;
            _rawdataLogger.Properties = tempProperties;
        }
    }
}

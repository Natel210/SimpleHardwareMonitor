using SimpleHardwareMonitor;
using SimpleHardwareMonitorGUI.Common;
using SimpleLogger.Interface;
using SimpleLogger.UserProperties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace SimpleHardwareMonitorGUI.Logger
{
    public partial class RawdataViewmodel : AViewModelBase_None
    {
        public DirectoryInfo RootDirectory
        {
            get => _rootDirectory;
            set => Set(ref _rootDirectory, value, nameof(RootDirectory));
        }
        public string Extension
        {
            get => _extension;
            set => Set(ref _extension, value, nameof(Extension));
        }
        public ELoggingInterval LoggingInterval
        {
            get => _loggingInterval;
            set
            {
                Set(ref _loggingInterval, value, nameof(LoggingInterval));
                RestartLoggingTimer();
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
        public static RawdataViewmodel instance = new RawdataViewmodel();



        private Timer _loggingTimer;
        private ILogger _rawdataLogger;
        private DirectoryInfo _rootDirectory = new DirectoryInfo(".//");
        private string _extension = "rawdata";
        private readonly string _extensionDefault = "rawdata";
        private bool _loggingEnabled = false;
        private ELoggingInterval _loggingInterval = ELoggingInterval.s1;
        private RawdataViewmodel()
        {
            _rawdataLogger = SimpleLogger.Main.Builder.Create("rawdataLogger") ?? throw new Exception("don't create logger...");
            RootDirectory = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".//");
            Extension = "rawdata";

            _loggingTimer = new Timer(SaveData, null, 0, (int)LoggingInterval);
            RestartLoggingTimer();
        }








        private void SaveData(object? state)
        {
            if (LoggingEnabled is false)
                return;
            ResettingLoggerProperties();
            if (_rawdataLogger.IsWriting is false)
                _rawdataLogger.Write();

        }
        private void RestartLoggingTimer()
        {
            TimeSpan currentInterval = TimeSpan.FromMilliseconds((double)LoggingInterval);
            DateTime calDate = DateTime.Now;
            TimeSpan waitTime;
            DateTime nextTime = DateTime.Now;
            switch (LoggingInterval)
            {
                case ELoggingInterval.ms250:
                case ELoggingInterval.ms500:
                case ELoggingInterval.s1:
                    nextTime = new DateTime(calDate.Year, calDate.Month, calDate.Day, calDate.Hour, calDate.Minute, calDate.Second, 0).AddSeconds(1);
                    break;
                case ELoggingInterval.s5:
                case ELoggingInterval.s10:
                case ELoggingInterval.s20:
                case ELoggingInterval.s30:
                case ELoggingInterval.m1:
                    nextTime = new DateTime(calDate.Year, calDate.Month, calDate.Day, calDate.Hour, calDate.Minute, 0, 0).AddMinutes(1);
                    break;
                case ELoggingInterval.m10:
                case ELoggingInterval.m20:
                case ELoggingInterval.m30:
                case ELoggingInterval.h1:
                    nextTime = new DateTime(calDate.Year, calDate.Month, calDate.Day, calDate.Hour, 0, 0, 0).AddHours(1);
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
                case ELoggingInterval.ms250:
                case ELoggingInterval.ms500:
                case ELoggingInterval.s1:
                case ELoggingInterval.s5:
                case ELoggingInterval.s10:
                case ELoggingInterval.s20:
                case ELoggingInterval.s30:
                    tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(RootDirectory.FullName, $"{curNow:yyMMdd_HH}\\"));
                    tempProperties.FileName = $"{curNow.Minute:D2}";
                    break;
                case ELoggingInterval.m1:
                case ELoggingInterval.m10:
                case ELoggingInterval.m20:
                case ELoggingInterval.m30:
                    tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(RootDirectory.FullName, "Hour", $"{curNow:yyMMdd}\\"));
                    tempProperties.FileName = $"{curNow.Hour:D2}";
                    break;
                case ELoggingInterval.h1:
                    tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(RootDirectory.FullName,"Day", $"{curNow:yyMM}\\"));
                    tempProperties.FileName = $"{curNow.Day:D2}";
                    break;
                default:
                    break;
            }
            _rawdataLogger.Properties = tempProperties;
        }
        private void Set
    }
}

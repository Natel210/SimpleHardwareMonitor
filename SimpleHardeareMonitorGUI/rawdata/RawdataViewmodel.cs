using LibreHardwareMonitor.Hardware.Motherboard.Lpc.EC;
using LibreHardwareMonitor.Hardware.Motherboard;
using SimpleHardwareMonitor;
using SimpleHardwareMonitorGUI.Common;
using SimpleFileIO.UserProperties;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.Json;
using Windows.Devices.Power;
using SimpleHardwareMonitor.data;
using System.Diagnostics;
using SimpleFileIO.Log.Text;
using SimpleFileIO.Log.Csv;

namespace SimpleHardwareMonitorGUI.Rawdata
{
    //static 
    public partial class RawdataViewmodel : AViewModelBase_None
    {
        private static readonly string _loggerName = "rawdataLogger";
        private static readonly string _titleNameDefault = "";
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
        public string TitleName
        {
            get => _titleName;
            set => Set(ref _titleName, value, nameof(TitleName));
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
                {
                    ResettingLoggerProperties();
                    RestartLoggingTimer();
                }
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

            var tempItem = new RawdataItem();
            tempItem.DateTime = DateTime.Now;
            if (HardwareMonitor.Cpu is not null)
            {
                tempItem.CpuCoreCount = HardwareMonitor.Cpu.Value.CoreCount;
                tempItem.CpuProcessorCount = HardwareMonitor.Cpu.Value.ProcessorCount;
                tempItem.CpuUse = HardwareMonitor.Cpu.Value.Use;
                tempItem.CpuUseByThreads = new List<float>(HardwareMonitor.Cpu.Value.UseByThreads);
                tempItem.CpuVoltage = HardwareMonitor.Cpu.Value.Voltage;
                tempItem.CpuVoltageByCore = new List<float>(HardwareMonitor.Cpu.Value.VoltageByCore);
                tempItem.CpuPower = HardwareMonitor.Cpu.Value.Power;
                tempItem.CpuPowerByCore = new List<float>(HardwareMonitor.Cpu.Value.PowerByCore);
                tempItem.CpuTemperature = HardwareMonitor.Cpu.Value.Temperature;
                tempItem.CpuTemperatureByCore = new List<float>(HardwareMonitor.Cpu.Value.TemperatureByCore);
            }

            _rawdataCSVLog.Add(tempItem);
        }

    }
    public partial class RawdataViewmodel : AViewModelBase_None
    {
        private Timer _loggingTimer;
        private ICSVLog _rawdataCSVLog;
        private DirectoryInfo _rootDirectory = new DirectoryInfo(".//");
        private string _titleName = _titleNameDefault;
        private string _extension = _extensionDefault;

        private bool _loggingEnabled = false;
        private ERawDataInterval _loggingInterval = ERawDataInterval.s1;
        private RawdataViewmodel()
        {
            PathProperties pathCSVLong = new();
            pathCSVLong.RootDirectory = new("./");
            pathCSVLong.FileName = "temp";
            pathCSVLong.Extension = ".rawdata";
            _rawdataCSVLog = SimpleFileIO.Manager.CreateCsvLog(_loggerName, pathCSVLong) ?? throw new Exception("don't create logger...");
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
            if (_rawdataCSVLog.IsWriting is false)
                _rawdataCSVLog.Write();
        }
        private void RestartLoggingTimer()
        {
            TimeSpan currentInterval = TimeSpan.FromMilliseconds((double)LoggingInterval);
            DateTime currentDate = DateTime.Now;
            TimeSpan waitTime;
            DateTime nextTime = DateTime.Now;
            switch (LoggingInterval)
            {
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
            PathProperties tempProperties = _rawdataCSVLog.Properties;
            var curNow = DateTime.Now;
            DateTime fileTime = new DateTime();
            switch (LoggingInterval)
            {
                case ERawDataInterval.s1:
                case ERawDataInterval.s5:
                case ERawDataInterval.s10:
                case ERawDataInterval.s20:
                case ERawDataInterval.s30:
                    fileTime = new DateTime(curNow.Year, curNow.Month, curNow.Day, curNow.Hour, curNow.Minute, 0, 0);
                    break;
                case ERawDataInterval.m1:
                case ERawDataInterval.m10:
                case ERawDataInterval.m20:
                case ERawDataInterval.m30:
                    fileTime = new DateTime(curNow.Year, curNow.Month, curNow.Day, curNow.Hour, 0, 0, 0);
                    break;
                case ERawDataInterval.h1:
                    fileTime = new DateTime(curNow.Year, curNow.Month, curNow.Day, 0, 0, 0, 0);
                    break;
                default:
                    break;
            }
            tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(RootDirectory.FullName, $"{TitleName}\\{fileTime:yyMMdd}\\"));
            tempProperties.FileName = $"{TitleName}_{fileTime:HHmmss}";
            tempProperties.Extension = Extension;
            _rawdataCSVLog.Properties = tempProperties;
        }
    }
}

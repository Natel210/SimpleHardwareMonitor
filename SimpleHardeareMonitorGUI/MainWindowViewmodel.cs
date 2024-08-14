using Microsoft.VisualBasic;
using SimpleHardwareMonitor;
using SimpleHardwareMonitorGUI.Common;
using SimpleHardwareMonitorGUI.Common.Enums;
using SimpleLogger.UserProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Media.Miracast;

namespace SimpleHardwareMonitorGUI
{
    public partial class MainWindowViewmodel : AViewModelBase_None
    {

        public MainWindowViewmodel()
        {
            _hardwareMonitorViewmodel = HardwareMonitorVM.instance ?? throw new ArgumentNullException(nameof(HardwareMonitorVM.instance));
            
            SimpleLogger.Main.Builder.Create("logData", new LoggerProperties());
            _loggingTimer = new Timer(SaveAsync, null, 0, (int)LoggingInterval);
            RestartLoggingTimer();
        }

        ~MainWindowViewmodel()
        {
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
                    nextTime = new DateTime(calDate.Year, calDate.Month, calDate.Day, calDate.Hour, calDate.Minute, 0,0).AddMinutes(1);
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

        public HardwareMonitorVM HW
        {
            get => _hardwareMonitorViewmodel;
            set => Set(ref _hardwareMonitorViewmodel, value, nameof(HW));
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
        public string TitleName
        {
            get => _titleName;
            set => Set(ref _titleName, value, nameof(TitleName));
        }
        public bool LoggingEnabled
        {
            get => _loggingEnabled;
            set => Set(ref _loggingEnabled, value, nameof(LoggingEnabled));
        }






        private void SaveAsync(object? state)
        {
            if (!LoggingEnabled || !HardwareMonitor.Runing)
                return;
            var logger = SimpleLogger.Main.Builder.Get("logData");
            if (logger is null)
                return;

            var cutDateTime = DateTime.Now;
            DirectoryInfo rootDir = new DirectoryInfo(".//");
            DirectoryInfo saveDir = new DirectoryInfo(Path.Combine(rootDir.FullName, $"{cutDateTime:yyMMdd}\\"));

            LoggerProperties tempProperties = logger.Properties;
            tempProperties.RootDirectory = new DirectoryInfo(Path.Combine(rootDir.FullName, $"{cutDateTime:yyMMdd}\\"));
            tempProperties.FileName = $"{cutDateTime.Hour:D2}H_Data";
            tempProperties.Extension = "rawdata";
            logger.Properties = tempProperties;

            logger.Add($"{cutDateTime:mm:ss:fff},{HW.Cpu.Use:000.0},{HW.Cpu.Temperature:000.0},{HW.Cpu.Power:000.0}");
            logger.Write();

        }
    }


    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        private HardwareMonitorVM _hardwareMonitorViewmodel;
        private string _titleName = "타이틀 !!!";
        private ELoggingInterval _loggingInterval = ELoggingInterval.s1;
        private bool _loggingEnabled = false;

        private Timer _loggingTimer;
    }
}

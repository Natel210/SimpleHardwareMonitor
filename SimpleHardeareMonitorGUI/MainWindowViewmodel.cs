using Microsoft.VisualBasic;
using SimpleHardwareMonitor;
using SimpleHardwareMonitorGUI.Common;
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

namespace SimpleHardwareMonitorGUI
{
    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        public MainWindowViewmodel()
        {
            _hardwareMonitorViewmodel = HardwareMonitorVM.instance ?? throw new ArgumentNullException(nameof(HardwareMonitorVM.instance));
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            ;


            DateTime now = DateTime.Now;
            DateTime nextMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(1);
            TimeSpan initialDelay = nextMinute - now;
            _loggingTimer = new Timer(async _ => await SaveAsync(cancellationToken), null, initialDelay.Milliseconds, LoggingInterval);
        }

        public HardwareMonitorVM HW
        {
            get => _hardwareMonitorViewmodel;
            set => Set(ref _hardwareMonitorViewmodel, value, nameof(HW));
        }
        public int LoggingInterval
        {
            get => _loggingInterval;
            set
            {
                Set(ref _loggingInterval, value, nameof(LoggingInterval));
            }
        }
        public bool LoggingEnabled
        {
            get => _loggingEnabled;
            set => Set(ref _loggingEnabled, value, nameof(LoggingEnabled));
        }






        private async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => {
                try
                {
                    if (!LoggingEnabled || !HardwareMonitor.Runing || cancellationToken.IsCancellationRequested)
                        return;

                    var cutDateTime = DateTime.Now;
                    DirectoryInfo rootDir = new DirectoryInfo(".//");
                    DirectoryInfo saveDir = new DirectoryInfo(Path.Combine(rootDir.FullName, $"{cutDateTime:yyMMdd}\\"));

                    if (!saveDir.Exists)
                        saveDir.Create();

                    FileInfo saveFile = new FileInfo(Path.Combine(saveDir.FullName, $"{cutDateTime.Hour:D2}H_Data.rawdata"));

                    using (var fileStream = new FileStream(saveFile.FullName, saveFile.Exists ? FileMode.Append : FileMode.Create))
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        if (!saveFile.Exists)
                        {
                            // Write the header if the file does not exist
                            var header = "Time,CPU USE,CPU TEMP,CPU POWER";
                            streamWriter.WriteLine(header);
                        }

                        string writeString = $"{cutDateTime:mm:ss:fff},{HW.Cpu.Use:000.0},{HW.Cpu.Temperature:000.0},{HW.Cpu.Power:000.0}";
                        streamWriter.WriteLine(writeString);
                    }
                }
                catch (Exception/* ex*/)
                {
                    // 적절한 예외 처리 로직을 추가하세요
                    //Console.WriteLine($"Error occurred: {ex.Message}");
                }
            }, cancellationToken);
        }
    }


    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        private HardwareMonitorVM _hardwareMonitorViewmodel;
        private int _loggingInterval = 60000;
        private bool _loggingEnabled = false;

        private Timer _loggingTimer;
        private CancellationTokenSource _cancellationTokenSource;
    }
}

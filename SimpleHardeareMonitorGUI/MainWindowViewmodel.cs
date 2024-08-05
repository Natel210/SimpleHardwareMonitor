using SimpleHardwareMonitor;
using SimpleHardwareMonitorGUI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace SimpleHardwareMonitorGUI
{
    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        public MainWindowViewmodel()
        {
            _hardwareMonitorViewmodel = HardwareMonitorVM.instance;
            loggingTime = new Timer(_ => { Save(); }, null, 0, LoggingInterval);
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
                loggingTime.Change(0, _loggingInterval);
            }
        }
        public bool LoggingEnabled
        {
            get => _loggingEnabled;
            set => Set(ref _loggingEnabled, value, nameof(LoggingEnabled));
        }




        

        private async Task Save()
        {
            while (true)
            {
                if (LoggingEnabled is false)
                    continew;

                string filePath = Path.Combine("..\\", "Test.csv");
                bool fileExists = File.Exists(filePath);

                using (var fileStream = new FileStream(filePath, fileExists ? FileMode.Append : FileMode.Create))
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    if (HardwareMonitor.Runing is false)
                        return;
                    var properties = HardwareMonitor.Cpu.GetType().GetProperties();
                    var values = string.Join(",", properties.Select(p => p.GetValue(HardwareMonitor.Cpu, null)?.ToString()));
                    streamWriter.WriteLine(values);

                    //var properties = typeof(T).GetProperties();

                    //if (!fileExists)
                    //{
                    //    // Write the header if the file does not exist
                    //    var header = string.Join(",", properties.Select(p => p.Name));
                    //    streamWriter.WriteLine(header);
                    //}

                    //// Write the values
                    //var values = string.Join(",", properties.Select(p => p.GetValue(item, null)?.ToString()));
                    //streamWriter.WriteLine(values);

                }
            }

            
        }
    }


    public partial class MainWindowViewmodel : AViewModelBase_None
    {
        private HardwareMonitorVM _hardwareMonitorViewmodel;
        private int _loggingInterval = 1000;
        private bool _loggingEnabled = false;

        private Timer loggingTime;
    }
}

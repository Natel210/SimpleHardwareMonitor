using HidSharp.Utility;
using SimpleFileIO.State.Ini;
using SimpleFileIO.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardwareMonitorGUI.Model
{
    internal class GlobalModel : INotifyPropertyChanged
    {
        static internal GlobalModel Instance { get; } = new();

        internal Child.Common CommonData { get; private set; }
        internal Child.HardwareMonitor HardwareMonitorData { get; private set; }
        internal Child.Theme ThemeData { get; private set; }
        internal Child.RawLogging RawLoggingData { get; private set; }

        private readonly IINIState _iniFile;
        internal PathProperty IniPathProperty
        {
            get => _iniFile.PathProperty;
            set => _iniFile.PathProperty = value;
        }

        private GlobalModel()
        {
            PathProperty properties = new() { RootDirectory = new("./Propertes/SettingData/"), FileName = "SettingProperty", Extension = "ini" };
            IINIState? iNIState = SimpleFileIO.Manager.CreateIniState("SettingData", properties);
            if (iNIState is null)
                throw new Exception("Not Create Setting Data...");
            _iniFile = iNIState;
            _iniFile.AddParser(typeof(Child.RawLoggingItem.Interval), new()
            {
                TargetType = typeof(Child.RawLoggingItem.Interval),
                ObjectToString = (obj) => Child.RawLogging.IntervalToString((Child.RawLoggingItem.Interval)obj),
                StringToObject = (str) => Child.RawLogging.StringToInterval(str)
            });

            if (_iniFile.Load() is false)
            {
                // Common
                _iniFile.SetValue_UseParser(nameof(Child.Common), nameof(Child.Common.MainWindowName), Child.Common.mainWindowNameDefault);
                // Theme
                _iniFile.SetValue_UseParser(nameof(Child.Theme), nameof(Child.Theme.CurrentTheme), Child.Theme.currentThemeDefault);
                // Hardware

                // RawLogging
                _iniFile.SetValue_UseParser(nameof(Child.RawLogging), nameof(Child.RawLogging.LoggingInterval), Child.RawLogging.loggingIntervalDefault);
                _iniFile.SetValue_UseParser(nameof(Child.RawLogging), nameof(Child.RawLogging.EnableAutoSave_ProgramStartup), Child.RawLogging.enableAutoSave_ProgramStartupDefault);
                _iniFile.Save();
            }

            CommonData = new() {

                MainWindowName = _iniFile.GetValue_UseParser(
                    nameof(Child.Common),
                    nameof(Child.Common.MainWindowName),
                    Child.Common.mainWindowNameDefault)

            };

            HardwareMonitorData = new() {

            };

            ThemeData = new() {

                CurrentTheme = _iniFile.GetValue_UseParser(
                    nameof(Child.Theme),
                    nameof(Child.Theme.CurrentTheme),
                    Child.Theme.currentThemeDefault)

            };

            RawLoggingData = new() {

                LoggingInterval = _iniFile.GetValue_UseParser(
                    nameof(Child.RawLogging),
                    nameof(Child.RawLogging.LoggingInterval),
                    Child.RawLogging.loggingIntervalDefault),

                EnableAutoSave_ProgramStartup = _iniFile.GetValue_UseParser(
                    nameof(Child.RawLogging),
                    nameof(Child.RawLogging.EnableAutoSave_ProgramStartup),
                    Child.RawLogging.enableAutoSave_ProgramStartupDefault)

            };
            if (RawLoggingData.EnableAutoSave_ProgramStartup is true)
                RawLoggingData.IsLoggingRunning = true;


        }

        internal bool Load()
        {
            if (_iniFile is null)
                throw new Exception("Not Create Setting Data...");
            if (_iniFile.Load() is false)
                return false;
            // Load
            _iniFile.Load();
            // Common
            CommonData.MainWindowName = _iniFile.GetValue_UseParser(nameof(Child.Common), nameof(Child.Common.MainWindowName), Child.Common.mainWindowNameDefault);
            // Theme
            ThemeData.CurrentTheme = _iniFile.GetValue_UseParser(nameof(Child.Theme), nameof(Child.Theme.CurrentTheme), Child.Theme.currentThemeDefault);
            // Hardware

            // RawLogging
            RawLoggingData.LoggingInterval = _iniFile.GetValue_UseParser(nameof(Child.RawLogging),
                nameof(Child.RawLogging.LoggingInterval), Child.RawLogging.loggingIntervalDefault);
            RawLoggingData.EnableAutoSave_ProgramStartup = _iniFile.GetValue_UseParser(nameof(Child.RawLogging),
                nameof(Child.RawLogging.EnableAutoSave_ProgramStartup), Child.RawLogging.enableAutoSave_ProgramStartupDefault);

            return true;
        }

        internal bool Save()
        {
            bool result = false;
            // Common
            result |= !_iniFile.SetValue_UseParser(nameof(Child.Common), nameof(Child.Common.MainWindowName), CommonData.MainWindowName);
            // Theme
            result |= !_iniFile.SetValue_UseParser(nameof(Child.Theme), nameof(Child.Theme.CurrentTheme), ThemeData.CurrentTheme);
            // Hardware

            // RawLogging
            result |= !_iniFile.SetValue_UseParser(nameof(Child.RawLogging), nameof(Child.RawLogging.LoggingInterval), RawLoggingData.LoggingInterval);
            result |= !_iniFile.SetValue_UseParser(nameof(Child.RawLogging), nameof(Child.RawLogging.EnableAutoSave_ProgramStartup), RawLoggingData.EnableAutoSave_ProgramStartup);
            // Save
            result |= !_iniFile.Save();
            return !result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

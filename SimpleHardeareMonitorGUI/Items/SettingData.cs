using SimpleHardwareMonitorGUI.Common;
using SimpleHardwareMonitor.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SimpleHardwareMonitorGUI.Items
{
    public enum EUpdateIntervalTime
    {
        ms100,
        s1,
        s5,
        m1,
    }


    class SettingData : INotifyPropertyChanged
    {
        private static SettingData _instance = new SettingData();
        public static SettingData Instance => _instance;

        private static ResourceDictionary? _resourceDictionary;

        static SettingData()
        {
            LoadResources();
            UpdateLogSaveFromResources();
        }

        private static void LoadResources()
        {
            _resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/SimpleHardwareMonitorGUI;component/Items/SettingDictionary.xaml")
            };
        }

        private static void UpdateLogSaveFromResources()
        {
            if (_resourceDictionary is null)
                return;

            if (_resourceDictionary.Contains("SettingData.LogSave"))
                Instance.LogSave = (bool)_resourceDictionary["SettingData.LogSave"];
            if (_resourceDictionary.Contains("SettingData.WindowTitleLocked"))
                Instance.WindowTitleLocked = (bool)_resourceDictionary["SettingData.WindowTitleLocked"];
        }

        private bool _logSave;
        public bool LogSave
        {
            get => _logSave;
            set
            {
                if (_logSave != value)
                {
                    _logSave = value;
                    OnPropertyChanged(nameof(LogSave));
                    if (_resourceDictionary != null)
                    {
                        _resourceDictionary["SettingData.LogSave"] = value;
                    }
                }
            }
        }

        private bool _windowTitleLocked;
        public bool WindowTitleLocked
        {
            get => _windowTitleLocked;
            set
            {
                if (_windowTitleLocked != value)
                {
                    _windowTitleLocked = value;
                    OnPropertyChanged(nameof(WindowTitleLocked));
                    if (_resourceDictionary != null)
                    {
                        _resourceDictionary["SettingData.WindowTitleLocked"] = value;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }






        public static EUpdateIntervalTime UpdateIntervalTime { get; set; } = EUpdateIntervalTime.ms100;
        public static string CurrentUpdateIntervalTimeToString()
        {
            string temp = "";
            switch (UpdateIntervalTime)
            {
                case EUpdateIntervalTime.ms100:
                    temp = "100ms";
                    break;
                case EUpdateIntervalTime.s1:
                    temp = "1s";
                    break;
                case EUpdateIntervalTime.s5:
                    temp = "5s";
                    break;
                case EUpdateIntervalTime.m1:
                    temp = "1m";
                    break;
                default:
                    break;
            }
            return temp;
        }
        public static int CurrentUpdateIntervalTimeToInteger()
        {
            int temp = -1;
            switch (UpdateIntervalTime)
            {
                case EUpdateIntervalTime.ms100:
                    temp = 100;
                    break;
                case EUpdateIntervalTime.s1:
                    temp = 1000;
                    break;
                case EUpdateIntervalTime.s5:
                    temp = 5000;
                    break;
                case EUpdateIntervalTime.m1:
                    temp = 60000;
                    break;
                default:
                    break;
            }
            return temp;
        }

    }
}

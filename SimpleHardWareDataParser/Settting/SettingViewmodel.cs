using SimpleHardWareDataParser.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace SimpleHardWareDataParser.Settting
{
    public partial class SettingViewmodel : AViewModelBase_None
    {
        public bool SameCurUTC
        {
            get => _sameCurUTC;
            set => Set(ref _sameCurUTC, value, nameof(SameCurUTC));
        }


        public TimeZoneInfo CurTimeZoneInfo
        {
            get => _curTimeZoneInfo;
            private set => Set(ref _curTimeZoneInfo, value, nameof(CurTimeZoneInfo));
        }

        public TimeZoneInfo SrcTimeZoneInfo
        {
            get => _srcTimeZoneInfo;
            set
            {
                if(Set(ref _srcTimeZoneInfo, value, nameof(SrcTimeZoneInfo)) is true)
                    CalTimeZoneDifference();
            }
        }

        public TimeZoneInfo DestTimeZoneInfo
        {
            get => _destTimeZoneInfo;
            set 
            {
                if(Set(ref _destTimeZoneInfo, value, nameof(DestTimeZoneInfo)) is true)
                    CalTimeZoneDifference();
            }
        }

        public TimeSpan TimeZoneDifference
        {
            get => _timeZoneDifference;
            private set => Set(ref _timeZoneDifference, value, nameof(TimeZoneDifference));
        }

        public SettingViewmodel()
        {
            UpdateLocalTimeZone();
            InitComponentDispatcher();
        }

    }

    public partial class SettingViewmodel
    {
        private TimeZoneInfo _curTimeZoneInfo = TimeZoneInfo.Local;
        private TimeZoneInfo _srcTimeZoneInfo = TimeZoneInfo.Utc;
        private TimeZoneInfo _destTimeZoneInfo = TimeZoneInfo.Local;
        private TimeSpan _timeZoneDifference = new(0L);
        private bool _sameCurUTC = false;

        private void InitComponentDispatcher()
        {
            ComponentDispatcher.ThreadPreprocessMessage += OnThreadPreprocessMessage;
        }

        private void OnThreadPreprocessMessage(ref MSG msg, ref bool handled)
        {
            if (msg.message == WM_TIMECHANGE)
            {
                UpdateLocalTimeZone();
            }

        }

        private void UpdateLocalTimeZone()
        {
            TimeZoneInfo.ClearCachedData();
            CurTimeZoneInfo = TimeZoneInfo.Local;

        }

        public void CalTimeZoneDifference()
        {
            TimeZoneDifference = _destTimeZoneInfo.GetUtcOffset(DateTime.UtcNow) - _srcTimeZoneInfo.GetUtcOffset(DateTime.UtcNow);
        }



    }

    public partial class SettingViewmodel
    {
        const int WM_TIMECHANGE = 0x001E;
    }
}

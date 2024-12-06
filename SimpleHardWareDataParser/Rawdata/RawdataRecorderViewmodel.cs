using SimpleHardWareDataParser.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHardWareDataParser.Rawdata
{
    public class RawdataRecorderViewmodel : AViewModelBase_None
    {
        private RawdataRecorder _model;

        public RawdataRecorderViewmodel(RawdataRecorder model)
        {
            _model = model;
            _model.DataChanged += OnPropertyChanged;
        }

        public string SplitName => _model.SplitName;
        public DateTime StartDateTime => _model.StartDateTime;
        public DateTime EndDateTime => _model.EndDateTime;
        public RawdataItem Average => _model.Average;  // 모델에서 계산된 평균 값 읽기 전용
        public RawdataItem Min => _model.Min;  // 모델에서 계산된 최소 값 읽기 전용
        public RawdataItem Max => _model.Max;  // 모델에서 계산된 최대 값 읽기 전용
        public Dictionary<DateTime, RawdataItem> Data => _model.Data;


    }
}

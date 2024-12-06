using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleHardWareDataParser.Rawdata
{
    static internal class RawdataRecordManager
    {
        static private Dictionary</* originData */string, /*all data*/Dictionary<DateTime, RawdataItem>> _originDataDic = [];
        static private Dictionary</* originData */string, Dictionary</* split name */string,RawdataRecorder>> _dataDic = [];
        static private Dictionary</* split */string, RawdataSplitInfo> _splitTemplate = [];
        static private Dictionary</* split */string, RawdataSplitInfo> _newSplitTemplate = [];
        static private readonly string _exception = @"*.rawdata";

        /// <summary>
        /// modifications are not reflected in <see cref="SplitTemplate"/> get.<br/>
        /// after updating with set, call <see cref="CalculateData"/>>.
        /// </summary>
        static public Dictionary<string, RawdataSplitInfo> SplitTemplate { get => new(_splitTemplate); set => _newSplitTemplate = value; }
        static public Dictionary<string, Dictionary</* split name */string, RawdataRecorder>> DataDic { get => new(_dataDic); }
        static public bool Load(DirectoryInfo rootDirectoryinfo)
        {
            bool result = false;
            if (Directory.Exists(rootDirectoryinfo.FullName) is false)
                return result;

            var files = Directory.EnumerateFiles(rootDirectoryinfo.FullName, _exception, SearchOption.AllDirectories);

#if DEBUG
            Debug.WriteLine("**** check file list ****");
            foreach (var file in files)
                Debug.WriteLine($"* {file}");
            Debug.WriteLine("****************\n");
#endif
            try
            {
                RawdataRecorder tempData = new();
                foreach (var file in files)
                {
                    using (var reader = new StreamReader(file))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<RawdataItem>();
                        foreach (var record in records)
                        {
                            if (_originDataDic.ContainsKey(record.Primary) is false)
                                _originDataDic[record.Primary] = new();
                            _originDataDic[record.Primary][record.DateTime] = record;
                        }
                    }
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
#if DEBUG
                throw;
#endif
            }
            return result;
        }

        static public void CalculateData()
        {
            _dataDic.Clear();
            _splitTemplate = new(_newSplitTemplate);
            foreach (var originData in _originDataDic)
            {
                Dictionary</* split name */string, RawdataRecorder> tempValue = [];
                foreach (var split in _splitTemplate)
                {
                    tempValue[split.Key] = new();
                    tempValue[split.Key].SetData(split.Value.SplitStart, split.Value.SplitEnd, originData.Value);
                }
                _dataDic.Add(originData.Key, tempValue);
            }
        }
    }
}

using CsvHelper;
using System;
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
        static public RawdataRecordDictionary _originData = new();

        static public Dictionary<string,RawdataSplitInfo> _rawdataSplitInfo = new();

        static internal Dictionary<string, RawdataRecordDictionary> Data { get; } = [];

        static internal bool Load(DirectoryInfo directoryinfo)
        {
            directoryinfo = new DirectoryInfo("./");

            if (Directory.Exists(directoryinfo.FullName) is false)
                return false;

            var files = Directory.EnumerateFiles(directoryinfo.FullName, "*.rawdata", SearchOption.AllDirectories);
            Debug.WriteLine("-------------파일들");
            foreach (var file in files)
            {
                Debug.WriteLine(file);
            }
            Debug.WriteLine("-------------\n");
            _originData.Clear();
            foreach (var file in files)
            {
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<RawdataItem>();
                    foreach (var record in records)
                        _originData.Add(record.DateTime, record);
                }
            }
            return true;
        }

        static internal void Split()
        {
            Data.Clear();
            foreach (var item in _rawdataSplitInfo)
            {
                var tempData = _originData
                    .Where(d => d.Key >= item.Value.SplitStart && d.Key <= item.Value.SplitEnd)
                    .ToDictionary(d => d.Key, d => d.Value);


                var splitData = new RawdataRecordDictionary();
                foreach (var data in tempData)
                    splitData.Add(data.Key, data.Value);

                Data.Add(item.Key, splitData);

            }
        }




    }
}

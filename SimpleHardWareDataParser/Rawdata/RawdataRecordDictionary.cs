namespace SimpleHardWareDataParser.Rawdata
{
    public class RawdataRecordDictionary : Dictionary<DateTime, RawdataItem>
    {



        public RawdataRecordDictionary() { }

        /// <summary>
        /// Clone
        /// </summary>
        public RawdataRecordDictionary(RawdataRecordDictionary dest)
        {
            if (dest != null)
            {
                foreach (var kvp in dest)
                {
                    // 각 키-값 쌍을 복사합니다.
                    this.Add(kvp.Key, kvp.Value);
                }
            }
        }

        /// <summary>
        /// Clone
        /// </summary>
        public RawdataRecordDictionary(Dictionary<DateTime, RawdataItem> source)
        {
            foreach (var kvp in source)
            {
                this.Add(kvp.Key, kvp.Value);
            }
        }
    }
}

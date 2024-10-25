using SimpleFileIO.Log.Csv;
using SimpleFileIO.Log.Text;
using SimpleFileIO.UserProperties;
using System.Linq;

namespace SimpleFileIO
{
    public static class Manager
    {
        #region TextLog
        
        public static ITextLog? CreateTextLog(string logName, PathProperties loggerProperties)
        {
            return TextLogManager.Create(logName, loggerProperties);
        }

        public static bool AddTextLog(string logName, ITextLog loggerInstance)
        {
            return TextLogManager.Add(logName, loggerInstance);
        }

        public static ITextLog? GetTextLog(string logName)
        {
            return TextLogManager.Get(logName);
        }

        public static List<string> GetTextLogListName()
        {
            return TextLogManager.GetLogListName();
        }

        #endregion

        #region CSVLog

        public static ICSVLog? CreateCsvLog(string logName, PathProperties loggerProperties)
        {
            return CSVLogManager.Create(logName, loggerProperties);
        }

        public static bool AddCsvLog(string logName, ICSVLog loggerInstance)
        {
            return CSVLogManager.Add(logName, loggerInstance);
        }

        public static ICSVLog? GetCsvLog(string logName)
        {
            return CSVLogManager.Get(logName);
        }

        public static List<string> GetCsvLogListName()
        {
            return CSVLogManager.GetLogListName();
        }

        #endregion

        static Manager()
        {
            //Create(_defaultLog);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLogger.Enum;
using SimpleLogger.UserProperties;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleLogger.Interface
{
    public interface ILogger
    {
        /// <summary>
        /// Logger properties.
        /// </summary>
        LoggerProperties Properties { get; set; }

        /// <summary>
        /// Indicates if writing is enabled.
        /// </summary>
        bool IsWriting { get; }

        /// <summary>
        /// Adds a log entry to the buffer.
        /// </summary>
        /// <param name="log">Log string to write</param>
        ELogAddErrorCode Add(string log);

        /// <summary>
        /// Writes the buffered log entries.
        /// </summary>
        ELogWriteErrorCode Write();

        /// <summary>
        /// Clears the buffered log entries.
        /// </summary>
        ELogClearErrorCode Clear();
    }
}

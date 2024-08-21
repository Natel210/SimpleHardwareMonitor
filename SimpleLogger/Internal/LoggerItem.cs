using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLogger.Enum;
using SimpleLogger.Interface;
using SimpleLogger.UserProperties;

namespace SimpleLogger.Internal
{
    internal class LoggerItem : ILogger
    {

        private List<string> _logItems = new List<string>();
        private readonly object _logItemsMutex = new object();
        private LoggerProperties _logProperties = new LoggerProperties();
        private readonly object _logPropertiesMutex = new object();

        /// <summary>
        /// Logger properties.
        /// </summary>
        public LoggerProperties Properties
        {
            get
            {
                LoggerProperties result = new LoggerProperties();
                try
                {
                    lock(_logItemsMutex)
                    {
                        result = new LoggerProperties();
                        if (_logProperties.RootDirectory is not null)
                            result.RootDirectory = new DirectoryInfo(_logProperties.RootDirectory.FullName);
                        result.FileName = _logProperties.FileName;
                        result.Extension = _logProperties.Extension;
                    }
                }
                catch (Exception)
                {
#if CHECK_THROW
                    throw;
#endif
                }
                return result;
            }

            set
            {
                try
                {
                    lock (_logItemsMutex)
                    {
                        _logProperties.RootDirectory = new DirectoryInfo(value.RootDirectory.FullName);
                        _logProperties.FileName = value.FileName;
                        _logProperties.Extension = value.Extension;
                    }
                }
                catch (Exception)
                {
#if CHECK_THROW
                    throw;
#endif
                }
            }
        }

        /// <summary>
        /// Indicates if writing is enabled.
        /// </summary>
        public bool IsWriting { get; private set; } = false;

        /// <summary>
        /// Adds a log entry to the buffer.
        /// </summary>
        /// <param name="log">Log string to write</param>
        public ELogAddErrorCode Add(string log)
        {
            ELogAddErrorCode result = ELogAddErrorCode.Unknown;
            try
            {
                lock (_logItemsMutex)
                {
                    _logItems.Add(log);
                    result = ELogAddErrorCode.OK;
                }
            }
            catch (Exception)
            {
                result = ELogAddErrorCode.WorkException;
#if CHECK_THROW
                throw;
#endif
            }
            return result;
        }

        /// <summary>
        /// Writes the buffered log entries.
        /// </summary>
        public ELogWriteErrorCode Write()
        {
            if (IsWriting is true)
                return ELogWriteErrorCode.AlreadyWriting;
            IsWriting = true;
            ELogWriteErrorCode result = ELogWriteErrorCode.Unknown;
            List<string>? tempItems = null;
            try
            {
                lock (_logItemsMutex)
                {
                    if (_logItems.Count is 0)
                        result = ELogWriteErrorCode.OK;
                    else
                    {
                        tempItems = new List<string>(_logItems);
                        _logItems.Clear();
                    }
                }
            }
            catch (Exception)
            {
                result = ELogWriteErrorCode.GetLogItemsException;
                IsWriting = false;
#if CHECK_THROW
                throw;
#endif
            }

            if (result is ELogWriteErrorCode.GetLogItemsException)
            {
                IsWriting = false;
                return result;
            }
            if (result is ELogWriteErrorCode.OK)
            {
                IsWriting = false;
                return result;
            }
            if (tempItems is null)
            {
                IsWriting = false;
                return ELogWriteErrorCode.InvalidNull;
            }
            if (tempItems.Count is 0)
            {
                IsWriting = false;
                return ELogWriteErrorCode.InvalidEmpty;
            }

            var tempProperties = Properties;

            try
            {
                var cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = cancellationTokenSource.Token;

                Task.Run(async () => {
                    try
                    {
                        if (tempProperties.RootDirectory.Exists is false)
                            tempProperties.RootDirectory.Create();
                        FileInfo fileInfo = new FileInfo(Path.Combine(tempProperties.RootDirectory.FullName, $"{tempProperties.FileName}.{tempProperties.Extension}"));
                        using (var fileStream = new FileStream(fileInfo.FullName, fileInfo.Exists ? FileMode.Append : FileMode.Create))
                        using (var streamWriter = new StreamWriter(fileStream))
                        {
                            foreach (var item in tempItems)
                                await streamWriter.WriteLineAsync(item);
                        }
                    }
                    catch (Exception)
                    {
                        result = ELogWriteErrorCode.TaskRunWritingException;
                        IsWriting = false;

#if CHECK_THROW
                throw;
#endif
                    }}, cancellationToken);
            }
            catch (Exception)
            {
                result = ELogWriteErrorCode.TaskRunCancellationTokenException;
#if CHECK_THROW
                throw;
#endif
            }
            finally { IsWriting = false; }
            return result;
        }

        /// <summary>
        /// Clears the buffered log entries.
        /// </summary>
        public ELogClearErrorCode Clear()
        {
            ELogClearErrorCode result = ELogClearErrorCode.Unknown;
            try
            {
                lock (_logItemsMutex)
                {
                    _logItems.Clear();
                    result = ELogClearErrorCode.OK;
                }
            }
            catch (Exception)
            {
                result = ELogClearErrorCode.WorkException;
#if CHECK_THROW
                throw;
#endif
            }
            return result;
        }
    }
}

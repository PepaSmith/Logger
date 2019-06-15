using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AppLogger
{
    public static class Logger
    {
        private static string _folderPath, _appName, _file, _fileName, _tempStr = "";
        private static int _linesNum, _maxNumOfLines = 0;
        private static bool _logOn;
        private static  StringBuilder _logString = new StringBuilder();
        private static StreamWriter logFile;
        private static StackFrame _callStack;

        /// <summary>
        /// Set logging ON/OFF
        /// </summary>
        public static bool LogOn
        {
            get { return _logOn; }
            set { _logOn = value; }
        }

        /// <summary>
        /// Initialize method
        /// </summary>
        public static void Initialize()
        {
            _appName = Utils.GetAttributeValue("AppName");
            _folderPath = @"" + Utils.GetAttributeValue("FolderPath");
            _fileName = CreateFileName();
            _file = _folderPath + _fileName;
            _maxNumOfLines = int.Parse(Utils.GetAttributeValue("MaxNumOfLines"));
            LogOn = bool.Parse(Utils.GetAttributeValue("LoggingEnabled"));

            if (LogOn)
            {
                Directory.CreateDirectory(_folderPath);
                using (logFile = new StreamWriter(_file, true))
                {
                    _logString.Append(Utils.GetTimeDate() + ";");
                    _logString.Append("  ");
                    _logString.Append("======================================== Logging Started! ========================================");
                    logFile.WriteLine(_logString);
                    _logString = new StringBuilder();
                }
            }
        }        

        /// <summary>
        /// Log general info
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Info, message);
            }        
        }

        /// <summary>
        /// Log general info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        [Obsolete]
        public static void Info(string message, string className, string methodName)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Info, message, className, methodName);
            }
        }

        /// <summary>
        /// Log info for debugging
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Debug, message);
            }    
        }

        /// <summary>
        /// Log info fo debugging
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        [Obsolete]
        public static void Debug(string message, string className, string methodName)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Debug, message, className, methodName);
            }
        }

        /// <summary>
        /// Log exceptions or other important info
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Warning, message);
            }
        }

        /// <summary>
        /// Log exceptions or other important info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        [Obsolete]
        public static void Warning(string message, string className, string methodName)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Warning, message, className, methodName);
            }
        }

        /// <summary>
        /// Log unhandled exceptions
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Error, message);
            }
        }

        /// <summary>
        /// Log unhandled exceptions
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        [Obsolete]
        public static void Error(string message, string className, string methodName)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Error, message, className, methodName);
            }
        }

        /// <summary>
        /// Log special exceptions
        /// </summary>
        /// <param name="exMsg">Exception message</param>
        /// <param name="methodName"></param>
        public static void Fatal(string message)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Fatal, message);
            }            
        }

        /// <summary>
        /// Log special exceptions
        /// </summary>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        [Obsolete]
        public static void Fatal(string message, string className, string methodName)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Fatal, message, className, methodName);
            }
        }

        /// <summary>
        /// Log exception in general
        /// </summary>
        /// <param name="ex"></param>
        public static void Exception(Exception ex)
        {
            if(_logOn)
            {
                BuildLog(LogLevels.Exception, ex.ToString()); 
            }
        }

        /// <summary>
        /// Log exception in general
        /// </summary>
        /// <param name="ex"></param>
        [Obsolete]
        public static void Exception(Exception ex, string className, string methodName)
        {
            if (_logOn)
            {
                BuildLog(LogLevels.Exception, ex.ToString(), className, methodName);
            }
        }

        /// <summary>
        /// ManageLogFile - If the log file reached the max number of lines then new log file is created
        /// </summary>
        public static void ManageLogFile()
        {            
            if (_linesNum == _maxNumOfLines)
            {
                using (logFile = new StreamWriter(_file, true))
                {                    
                    logFile.WriteLine(Utils.GetTimeDate() + ";  " + "============================== Closing log file, max number of lines reached! ==============================");
                }
                
                _linesNum = 0;
                _tempStr = _fileName.Replace(@"\", "");
                _fileName = CreateFileName();
                _file = _folderPath + _fileName; //create the new log file after that previous was closed     

                using (logFile = new StreamWriter(_file, true))
                {
                    logFile.WriteLine(Utils.GetTimeDate() + ";  " + "=================================== Continue of the: {0} ===================================", _tempStr);
                }
            }            
        }

        /// <summary>
        /// General method for logging
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        private static void BuildLog(string logType, string message)
        {            
            _callStack = new StackFrame(2, true);

            using (logFile = new StreamWriter(_file, true))
            {
                _logString.Append(Utils.GetTimeDate() + ";");
                _logString.Append("  ");
                _logString.Append(Utils.GetCurrentThreadName() + ";");
                _logString.Append("  ");
                _logString.Append(Utils.GetCurrentThreadID() + ";");
                _logString.Append("  ");
                _logString.Append(Utils.GetClassName() + ";");
                _logString.Append("  ");
                _logString.Append(Utils.GetMethodName() + ";");
                _logString.Append("  ");
                _logString.Append("Line: " + _callStack.GetFileLineNumber().ToString().PadRight(5) + ";");
                _logString.Append(logType.PadRight(9) + ";");
                _logString.Append("  ");
                _logString.Append(message);
                logFile.WriteLine(_logString);
                _logString = new StringBuilder();
                _linesNum++;
            }            

            ManageLogFile();
        }

        /// <summary>
        /// General method for logging
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        [Obsolete]
        private static void BuildLog(string logType, string message, string className, string methodName)
        {
            _callStack = new StackFrame(2, true);

            using (logFile = new StreamWriter(_file, true))
            {
                _logString.Append(Utils.GetTimeDate() + ";");
                _logString.Append("  ");
                _logString.Append(Utils.GetCurrentThreadName() + ";");
                _logString.Append("  ");
                _logString.Append(Utils.GetCurrentThreadID() + ";");
                _logString.Append("  ");
                _logString.Append(className.PadRight(20) + ";");
                _logString.Append("  ");
                _logString.Append(methodName.PadRight(20) + ";");
                _logString.Append("  ");
                _logString.Append("Line: " + _callStack.GetFileLineNumber().ToString().PadRight(5) + ";");
                _logString.Append(logType.PadRight(9) + ";");
                _logString.Append("  ");
                _logString.Append(message);
                logFile.WriteLine(_logString);
                _logString = new StringBuilder();
                _linesNum++;
            }

            ManageLogFile();
        }

        /// <summary>
        /// Method for creation a proper file name
        /// </summary>
        /// <returns></returns>
        private static string CreateFileName()
        {
            return @"\" + DateTime.Now.ToString("HHmmss") + "_" + DateTime.Now.ToString("ddMMyy") + "_" + _appName + ".log";
        }
    }
}

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml;

namespace AppLogger
{
    public class Utils
    {
        private static StackTrace _st = null;
        private static StackFrame _sf = null;
        private static MethodBase _mthb = null;
        private static string _threadName, _threadId = "";

        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss.fff");
        }

        public static string GetDate()
        {
            return DateTime.Now.ToString("dd.MM.yyyy");
        }

        public static string GetTimeDate()
        {
            return DateTime.Now.ToString("HH:mm:ss.fff  dd.MM.yyyy");
        }

        public static string GetCurrentThread()
        {
            _threadName = Thread.CurrentThread.Name;
            _threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            return _threadName + "  " + _threadId;
        }

        public static string GetMethodName()
        {
            string methodName;

            _st = new StackTrace();
            _sf = _st.GetFrame(3);

            if (_sf.GetMethod().Name.ToString().Length > 20)
            {
                methodName = _sf.GetMethod().Name.ToString().Substring(0, 20);
            }
            else
            {
                methodName = _sf.GetMethod().Name.ToString().PadRight(20);
            }
            
            return methodName;
        }

        public static string GetClassName()
        {
            string className;
            _st = new StackTrace();
            _mthb = _st.GetFrame(3).GetMethod();

            if (_mthb.ReflectedType.Name.Length > 20)
            {
                className = _mthb.ReflectedType.Name.Substring(0, 20);
            }
            else
            {
                className = _mthb.ReflectedType.Name.PadRight(20);
            }

            return className;
        }

        public static string GetAttributeValue(string node)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("LoggerConfig.config");
            string value = xmlDoc.SelectSingleNode("//Configuration/" + node + "/@value").InnerText;
            return value;
        }
    }
}

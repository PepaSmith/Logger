using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml;

namespace AppLogger
{
    public class Utils
    {
        private static StackTrace st = null;
        private static StackFrame sf = null;
        private static MethodBase mthb = null;
        private static string threadName, threadId = "";

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
            threadName = Thread.CurrentThread.Name;
            threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            return threadName + "  " + threadId;
        }

        public static string GetMethodName()
        {
            st = new StackTrace();
            sf = st.GetFrame(3);

            return sf.GetMethod().Name.ToString().PadRight(20);
        }

        public static string GetClassName()
        {
            st = new StackTrace();
            mthb = st.GetFrame(3).GetMethod();
            return (mthb.ReflectedType.Name + ".cs").PadRight(20);
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

namespace AppLogger
{
    public static class LogLevels
    {
        public static string Info = "Info"; // Generally useful information (service started, application started, ... )        
        public static string Debug = "Debug"; // Diagnostically important information
        public static string Warning = "Warning"; // Anything what can cause a fault in an application, but the application can recover from it
        public static string Error = "Error"; // Error which is fatal for the current operation but does not cause a crash of the application
        public static string Fatal = "Fatal"; // The most severe errors which cause crash of the application of service 
        public static string Exception = "Exception"; // Exceptions
    }
}

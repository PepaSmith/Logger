# Logger

Language: C# (.Net 4.5), Project type: Library, Author: Ing. Peter Kovac

My personal logging framework with standard log levels Info, Debug, Warning, Error, Fatal, Exception.
Logged is the following information: Time, Date, Thread Number, Class name, Method name, Line number, Log level and Message.

In the logger config file the following info is possible to set up: 
AppName - name of the application which uses the logger (for the log file name), FolderPath - path of the log files to save, MaxNumOfLines - max number of lines in one log file, LoggingEnabled - turning logging on or off

This is important to set up the Logger!
1. Properly set up the LoggerConfig.config file.
2. Call the Logger.Initialize() method at the initialization of your application.
3. Now you can call the other Logger methods and everything will work fine!

Enjoy :)

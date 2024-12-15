using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Anchridanex.Utilities.Logging
{
    public abstract class LoggerBase
    {
        private string logFilename = "";
        private static readonly object threadSafeLock = new object();

        public List<LogSeverity> Severities { get; set; } = new List<LogSeverity>();
        public bool WriteToDisk { get; set; } = false;
        public string LogFolder { get; } = "";

        public List<LogSeverity> AllSeverities
        {
            get
            {
                return new List<LogSeverity>() {
                    LogSeverity.Error, LogSeverity.Info, LogSeverity.Warning, LogSeverity.Debug
                };
            }
        }
        

        public List<LogSeverity> NoSeverities
        {
            get
            {
                return new List<LogSeverity>();
            }
        }

        public LoggerBase(string logFolder)
        {
            if (logFolder == "")
                return;

            LogFolder = logFolder;
            logFilename = Path.Combine(logFolder, $"Log_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.txt");
        }

        public void WriteToLog(LogSeverity sev, string message)
        {
            if (Severities == null)
                return;

            if (Severities.Contains(sev) == false)
                return;

            lock (threadSafeLock)
            {
                if (WriteToDisk == true && LogFolder != "")
                    WriteLogToDisk(FormatMessage(sev, message));

                WriteToLogWorker(sev, message);
            }
            
        }

        protected string FormatMessage(LogSeverity sev, string message)
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " +
                sev.ToString().ToUpper() + " - " + 
                message.Replace(Environment.NewLine, "|");
        }

        protected virtual void WriteToLogWorker(LogSeverity sev, string message)
        {
            throw new NotImplementedException("Derived class does not implement WriteToLogWorker()");
        }

        private bool WriteLogToDisk(string formattedMessage)
        {
            try
            {
                if (Directory.Exists(LogFolder) == false)
                    Directory.CreateDirectory(LogFolder);

                File.AppendAllText(logFilename, formattedMessage + Environment.NewLine);
                return true;
            }
            catch(Exception) when (Debugger.IsAttached == false)
            {
                return false;
            }
        }
    }
}

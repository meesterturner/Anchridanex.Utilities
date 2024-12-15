using System.Diagnostics;
using System.Reflection;

#nullable enable

namespace Anchridanex.Utilities.Logging
{
    public static class LogEngine
    {
        public static LogSeverity DefaultSeverity { get; set; } = LogSeverity.Debug;
        public static LoggerBase? Logger = null;

        public static void Log(string message)
        {
            Log(DefaultSeverity, message);
        }

        public static void Log(LogSeverity sev, string message)
        {
            if (Logger == null)
                return;

            Logger.WriteToLog(sev, message);
        }

        public static void LogInformation(string message)
        {
            Log(LogSeverity.Info, message);
        }

        public static void LogWarning(string message)
        {
            Log(LogSeverity.Warning, message);
        }

        public static void LogError(string message)
        {
            Log(LogSeverity.Error, message);
        }

        public static void LogDebug(string message)
        {
            Log(LogSeverity.Debug, message);
        }

        public static void LogFunctionCall()
        {
            if(Logger == null) 
                return;

            if (Logger.Severities.Contains(LogSeverity.Debug) == false)
                return;

            StackFrame? frame = new StackTrace().GetFrame(1);
            if(frame != null)
            {
                MethodBase? method = frame.GetMethod();
                if(method != null)
                {
                    Log(LogSeverity.Debug, $"Function Call: {method.ReflectedType?.Name}.{method.Name}()");
                }
                
            }
        }
    }
}

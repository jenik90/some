namespace QULIX.Domain.QULEX.Core.Utils
{
    using System;

    /// <summary>
    /// Logs formatted information.
    /// </summary>
    public static class Logger
    {
        private enum LogLevel
        {
            None,

            Info,

            Warning,

            Error
        }

        /// <summary>
        /// The trace method.
        /// </summary>
        public static Action<string> TraceMethod = Console.WriteLine;

        /// <summary>
        /// Logs a formatted message.
        /// </summary>
        /// <param name="message">A message or message template. A message template is considered as a message if no arguments.</param>
        /// <param name="args">Arguments to specify in the message template.</param>
        public static void Log(string message, params object[] args)
        {
            Logger.LogMessageP(null, LogLevel.None, message, args);
        }

        /// <summary>
        /// Logs a formatted message as an error.
        /// </summary>
        /// <param name="message">A message or message template. A message template is considered as a message if no arguments.</param>
        /// <param name="args">Arguments to specify in the message template.</param>
        public static void LogError(string message, params object[] args)
        {
            Logger.LogMessageP(TraceMethod, LogLevel.Error, message, args);
        }

        /// <summary>
        /// Logs a formatted message as an informational record.
        /// </summary>
        /// <param name="message">A message or message template. A message template is considered as a message if no arguments.</param>
        /// <param name="args">Arguments to specify in the message template.</param>
        public static void LogInfo(string message, params object[] args)
        {
            Logger.LogMessageP(TraceMethod, LogLevel.Info, message, args);
        }

        /// <summary>
        /// Logs a formatted message as a warning.
        /// </summary>
        /// <param name="message">A message or message template. A message template is considered as a message if no arguments.</param>
        /// <param name="args">Arguments to specify in the message template.</param>
        public static void LogWarning(string message, params object[] args)
        {
            Logger.LogMessageP(TraceMethod, LogLevel.Warning, message, args);
        }

        /// <summary>
        /// Writes a line to separate massages.
        /// </summary>
        /// <param name="delimiter">Form of the line.</param>
        /// <param name="length">Length of the line in chars.</param>
        public static void WriteSectionLine(char delimiter = '-', int length = 20)
        {
            Logger.Log(string.Empty.PadLeft(length < 1 ? 20 : length, delimiter));
        }

        private static void LogMessage(Action<string> traceMethod, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (traceMethod != null)
                {
                    traceMethod($"{DateTime.Now:u}\t{message}\n");
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
            else
            {
                traceMethod("\n");
            }
        }

        private static void LogMessageP(
            Action<string> traceMethod,
            LogLevel logLevel,
            string format,
            params object[] args)
        {
            string level = logLevel == LogLevel.None ? string.Empty : logLevel.ToString().ToUpper() + "\t";

            Logger.LogMessage(
                traceMethod,
                level
                + ((args == null || args.Length < 1 || string.IsNullOrEmpty(format))
                       ? format
                       : string.Format(format, args)));
        }
    }
}
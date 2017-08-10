namespace QULIX.WebUi.App_Start
{
    using System;
    using System.IO;

    using QULIX.Domain.QULEX.Core.Utils;

    public class LoggerConfig
    {
        private static readonly object lockObject = new object();

        public static void RegisterLogger(string logFileName)
        {
            LogStreamWriterPool.LogFileName = logFileName;
            LogStreamWriterPool.GetStreamWriter().Write("\nNEW DEBUG SESSION!!!\n");

            Logger.TraceMethod = msg =>
                {
                    lock (lockObject)
                    {
                        LogStreamWriterPool.GetStreamWriter().Write(msg);  // just for debugging to write database requests
                    }
                };
        }

        public static void DisposeLogger()
        {
            LogStreamWriterPool.DisposeOfStreamWriter();
        }

        #region Classes

        private static class LogStreamWriterPool
        {
            private static string logFileName;

            private static StreamWriter streamWriter;

            public static string LogFileName
            {
                set
                {
                    if (logFileName == null)
                    {
                        logFileName = value;
                    }
                }
            }

            public static StreamWriter GetStreamWriter()
            {
                if (streamWriter == null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            streamWriter = File.AppendText(logFileName);
                            break;
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                }

                return streamWriter;
            }

            public static void DisposeOfStreamWriter()
            {
                streamWriter?.Close();
                streamWriter?.Dispose();
            }
        }

        #endregion
    }
}
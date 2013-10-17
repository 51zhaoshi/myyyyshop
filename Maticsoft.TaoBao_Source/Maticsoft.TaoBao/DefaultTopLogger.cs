namespace Maticsoft.TaoBao
{
    using System;
    using System.Diagnostics;

    public class DefaultTopLogger : ITopLogger
    {
        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public const string LOG_FILE_NAME = "topsdk.log";

        static DefaultTopLogger()
        {
            try
            {
                Trace.Listeners.Add(new TextWriterTraceListener("topsdk.log"));
            }
            catch (Exception)
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            }
            Trace.AutoFlush = true;
        }

        public void Error(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ERROR");
        }

        public void Info(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " INFO");
        }

        public void Warn(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " WARN");
        }
    }
}


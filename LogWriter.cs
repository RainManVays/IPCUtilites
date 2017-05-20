using System.IO;

namespace IpcPmrep
{
    internal static class LogWriter
    {
        private static string logFile = "IPCPmrep.log";

        public static void SetLogFile(string logfile)
        {
           logFile = logfile;
        }
        public static  void Write(string text)
        {
            using (var streamWriter = File.AppendText(logFile))
            {
                streamWriter.WriteLine(text);
            }
        }


    }


}

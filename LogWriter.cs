using System.IO;

namespace IpcPmrep
{
    internal static class LogWriter
    {
        private static string logFile="IPCPmrep.log";

        public static void SetLogFile(string logfile)
        {
           logFile = logfile;
        }
        public static async void Write(string text)
        {
            if(logFile!=null)
                using (var streamWriter = File.AppendText(logFile))
                {
                    await streamWriter.WriteLineAsync(text);
                }
        }


    }


}

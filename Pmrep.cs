using System.Collections.Generic;
using System.IO;

namespace IpcPmrep
{
    /// <summary>
    /// Class for interacting with Informatica PowerCenter repository using the pmrep binary.
    /// You need Set envivorment variable Domain_info
    /// </summary>
    public class Pmrep
    {
        private string _pmrepFile;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pmrepfile"> full path to pmrep.exe</param>
        /// <param name="parameters">connection parameters</param>
        /// <param name="logFile">if you need write work log set full path to logfile</param>
        public Pmrep(string pmrepfile,Dictionary<char,string> parameters,string logFile=null)
        {
            _pmrepFile = pmrepfile;

            if (!File.Exists(pmrepfile))
                throw new FileNotFoundException("File not found!", pmrepfile);
            if (logFile != null)
                LogWriter.SetLogFile(logFile);


            char[] accetableArgs = { 'r', 'h', 'o', 'n', 's', 'x', 'u', 't' };
            char[] flags = null;


            var command = PmrepWorker.PrepareCommand("connect", parameters, accetableArgs, flags);
            var result= PmrepWorker.ExecuteCommand(_pmrepFile,command);
            LogWriter.Write(result.output);
            LogWriter.Write(result.errors);
           

        }

        /// <summary>
        /// List all connection objects in the repository and their respective connection types.
        /// Args:None
        /// </summary>
        public string[] ListConnections()
        {
            var result = PmrepWorker.ExecuteCommand(_pmrepFile,"list connections -t");
            LogWriter.Write(result.output);
            LogWriter.Write(result.errors);
            LogWriter.Write(PmrepWorker.FormattingResult(result.output, ",").Length.ToString());
            return PmrepWorker.FormattingResult(result.output, ",");
        }
    }
}

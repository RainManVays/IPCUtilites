using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpcPmrep
{

    internal  class PmrepOutput
    {
        internal string output { get; set; }
        internal string errors { get; set; }
    }


    internal static class PmrepWorker
    {

        internal static PmrepOutput ExecuteCommand(string pmrep, string command)
        {
            var pmrepProcess = new Process();
            pmrepProcess.StartInfo = new ProcessStartInfo(pmrep);
            pmrepProcess.StartInfo.Arguments = command;
            pmrepProcess.StartInfo.CreateNoWindow = true;
            pmrepProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pmrepProcess.StartInfo.RedirectStandardInput = true;
            pmrepProcess.StartInfo.RedirectStandardOutput = true;
            pmrepProcess.StartInfo.RedirectStandardError = true;
            pmrepProcess.StartInfo.UseShellExecute = false;
            LogWriter.Write(command);
            pmrepProcess.Start();
            
            var result = new PmrepOutput();

            result.output = pmrepProcess.StandardOutput.ReadToEnd();
            result.errors = pmrepProcess.StandardError.ReadToEnd();
            pmrepProcess.WaitForExit();
            return result;
        }

        private static string[] _ignoreLines = { "Invoked", "All Rights Reserved", "Informatica(r)", "Copyright (c)", "This Software","Completed at","completed successfully" };

        private static bool CheckingRowIsNotGarbage(string row)
        {
            foreach(var ignoreItem in _ignoreLines)
                if (row.ToLower().Contains(ignoreItem.ToLower()))
                    return true;
            return false;
        }
        internal static string[] FormattingResult(string result)
        {
            var resultarray = result.Trim().Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> resultCollection = new List<string>();
            foreach (var resultRow in resultarray)
            {
                if (!CheckingRowIsNotGarbage(resultRow))
                    resultCollection.Add(resultRow);
            }
            return resultCollection.ToArray();
        }

        internal static string[] FormattingResult(string result, char separator)
        {
            var resultarray = result.Trim().Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> resultCollection = new List<string>();
            foreach (var resultRow in resultarray)
            {
                if (!CheckingRowIsNotGarbage(resultRow))
                    resultCollection.Add(resultRow);
            }
            return resultCollection.ToArray();
        }
        internal static string[] FormattingResult(string result,string separator)
        {
            var resultarray = result.Trim().Split(new string[] {"\r","\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> resultCollection = new List<string>();
            foreach (var resultRow in resultarray)
            {
                if (!CheckingRowIsNotGarbage(resultRow))
                    resultCollection.Add(resultRow);
            }

            return resultCollection.ToArray();
        }
      
    }
}

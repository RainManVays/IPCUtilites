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
        private static string[] _ignoreLines = { "Informatica",
        "Copyright",
        "All Rights Reserved",
        "This Software is protected",
        "Invoked at",
        "Completed at",
        "completed successfully"};

        internal static string PrepareCommand(string commandName,Dictionary<char,string> parameters,char[] accetableParameters, char[] flags)
        {
            string command = commandName;
            foreach(var parameter in parameters)
            {
                if (accetableParameters.Contains<char>(parameter.Key))
                {
                    command += " -" + parameter.Key + " " + parameter.Value;
                }
                else if (flags.Contains<char>(parameter.Key))
                {
                    command += " -" + parameter.Key;
                }
                else
                {
                    throw new ApplicationException("key " + parameter.Key + " not found in accetable pmrep keys");
                }
            }
            return command;
        }

        internal static PmrepOutput ExecuteCommand(string pmrep,string command)
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
            pmrepProcess.Start();

            var result = new PmrepOutput();

            result.output = pmrepProcess.StandardOutput.ReadToEnd();
            result.errors = pmrepProcess.StandardError.ReadToEnd();
            pmrepProcess.WaitForExit();
            return result;
        }

        internal static string[] FormattingResult(string result, char separator)
        {
            var resultarray = result.Trim().Split(new string[] { "\r", "\n" }, StringSplitOptions.None);

            return resultarray;
        }
        internal static string[] FormattingResult(string result,string separator)
        {
            var resultarray = result.Trim().Split(new string[] {"\r","\n" }, StringSplitOptions.None);

            return resultarray;
        }
      
    }
}

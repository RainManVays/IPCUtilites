using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities.IpcPmcmd
{
    internal static class PmcmdWorker
    {

        internal static PmcmdOutput ExecuteCommand(string pmcmd, string command)
        {
            var pmcmdProcess = new Process();
            pmcmdProcess.StartInfo = new ProcessStartInfo(pmcmd);
            pmcmdProcess.StartInfo.Arguments = command;
            pmcmdProcess.StartInfo.CreateNoWindow = true;
            pmcmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pmcmdProcess.StartInfo.RedirectStandardInput = true;
            pmcmdProcess.StartInfo.RedirectStandardOutput = true;
            pmcmdProcess.StartInfo.RedirectStandardError = true;
            pmcmdProcess.StartInfo.UseShellExecute = false;
           // LogWriter.Write(command);
            pmcmdProcess.Start();

            var result = new PmcmdOutput();

            result.output = pmcmdProcess.StandardOutput.ReadToEnd();
            result.errors = pmcmdProcess.StandardError.ReadToEnd();
            pmcmdProcess.WaitForExit();
            return result;
        }


    }
}

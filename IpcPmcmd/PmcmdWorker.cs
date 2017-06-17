using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities.IpcPmcmd
{
    internal static class PmcmdWorker
    {

        private static StringBuilder pmcmdOutput = null;
        internal static string ExecuteCommand(string pmcmd,string connectionCmd, string command)
        {
            var pmcmdProcess = new Process();
            pmcmdProcess.StartInfo = new ProcessStartInfo(pmcmd);
            //pmcmdProcess.StartInfo.Arguments = connectionCmd;
            pmcmdProcess.StartInfo.CreateNoWindow = true;
            pmcmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pmcmdProcess.StartInfo.RedirectStandardInput = true;
            pmcmdProcess.StartInfo.RedirectStandardOutput = true;
            pmcmdOutput = new StringBuilder("");
            pmcmdProcess.OutputDataReceived += PmcmdProcess_OutputDataReceived;

            pmcmdProcess.StartInfo.RedirectStandardError = true;
            pmcmdProcess.StartInfo.UseShellExecute = false;
            pmcmdProcess.Start();
            pmcmdProcess.BeginOutputReadLine();
            using (StreamWriter processWriter = pmcmdProcess.StandardInput)
            {
                processWriter.WriteLine(connectionCmd);
                processWriter.WriteLine(command);
            }
            //processWriter.Close();
           // processWriter.Dispose();
            pmcmdProcess.WaitForExit();
            pmcmdProcess.Dispose();
            string result = pmcmdOutput.ToString();
            return result;

        }
        private static string _utilName = "pmcmd> ";
        private static string[] _ignoreLines = {"Connected to Integration service", "Invoked", "All Rights Reserved", "Informatica(r)", "Copyright (c)", "This Software", "Completed at", "completed successfully" };

        private static string ClearOutputData(string outputData)
        {
            if (!string.IsNullOrEmpty(outputData))
            {
                foreach (var ignoreItem in _ignoreLines)
                {
                    if (outputData.ToLower().Contains(ignoreItem.ToLower()))
                        return "";
                }

                if (outputData.Contains(_utilName))
                {
                    return outputData.Substring(outputData.IndexOf(_utilName) + _utilName.Length);
                }
            }
            return outputData;
        }
        private static void PmcmdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            var clearResult = ClearOutputData(e.Data);
            if(!string.IsNullOrWhiteSpace(clearResult))
                pmcmdOutput.AppendLine(clearResult);
        }

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

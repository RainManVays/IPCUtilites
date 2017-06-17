using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities
{
    namespace IpcPmrep
    {

        internal class PmrepOutput
        {
            internal string output { get; set; }
            internal string errors { get; set; }
        }


        internal class PmrepWorker : IDisposable
        {
            static StringBuilder _outputResult = new StringBuilder();
            static StreamWriter _imputCommand;
            static Process _pmrep;


            internal PmrepWorker(string pmrep, string command)
            {
                _pmrep = new Process()
                {
                    StartInfo = new ProcessStartInfo(pmrep)
                    {
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    }
                };
                _pmrep.OutputDataReceived += _pmrep_OutputDataReceived;
                _pmrep.Start();

                _imputCommand = _pmrep.StandardInput;
                _imputCommand.WriteLine(command);
                _pmrep.BeginOutputReadLine();
                _outputResult.Clear();
            }

            private static bool _workFlag = true;

            private static async Task<string> CheckFlag()
            {
                while (_workFlag)
                {
                    await Task.Delay(100);
                }
                return _outputResult.ToString();
            }

            internal string ExecuteCommand(string command)
            {
                _outputResult.Clear();
                _workFlag = true;
                _imputCommand.WriteLine(command);
                return CheckFlag().Result;
            }

            private readonly static string _applicationName = "pmrep>";
            private static void _pmrep_OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                
                if (!string.IsNullOrEmpty(e.Data))
                    if (e.Data.Contains(" completed successfully.") || e.Data.Contains("Failed to execute ") || e.Data.Contains("Repository connection failed.") || e.Data.Contains("Connected to repository "))
                        _workFlag = false;
                    else
                    {
                        if (e.Data.Contains(_applicationName))
                            _outputResult.AppendLine(e.Data.Substring(e.Data.IndexOf(_applicationName) + _applicationName.Length));
                        else
                            _outputResult.AppendLine(e.Data);
                        if (e.Data.Contains("Invoked at "))
                            _outputResult.Clear();
                    }
                else
                {
                    _workFlag = false;
                }

            }

            public void Dispose()
            {
                ExecuteCommand("cleanup");
                ExecuteCommand("exit");
                _pmrep.Close();
                _outputResult.Clear();
                _imputCommand.Dispose();
            }


            private static string[] _ignoreLines = { "Invoked", "All Rights Reserved", "Informatica(r)", "Copyright (c)", "This Software", "Completed at", "completed successfully" };

            private static bool CheckingRowIsNotGarbage(string row)
            {
                foreach (var ignoreItem in _ignoreLines)
                    if (row.ToLower().Contains(ignoreItem.ToLower()))
                        return true;
                return false;
            }

            internal static string RemoveResultHeader(string result)
            {
                string outResult = string.Empty;
                var resultarray = result.Trim().Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var resultRow in resultarray)
                {
                    if (!CheckingRowIsNotGarbage(resultRow))
                        outResult += resultRow + "\r\n";
                }
                return outResult;
            }

            internal string[] FormattingResult(string result)
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

            internal  bool CheckErrorInResult(string result)
            {

                if (result.Length > 0 || result.ToLower().Contains("failed"))
                {
                    LogWriter.Write(result);
                    return false;
                }
                return true;
            }

            internal void CreateControlImportFile(string sourceRepo, string sourceFolder, string targetFolder, string targetRepo, string dtdFile, string encoding)
            {
                encoding = string.IsNullOrEmpty(encoding) ? "windows-1251" : encoding;

                string controlFileTemplate = "<?xml version='1.0' encoding='" + encoding + "'?>" +

                "<!DOCTYPE IMPORTPARAMS SYSTEM '" + dtdFile + "'>\n" +
                "<IMPORTPARAMS CHECKIN_AFTER_IMPORT='NO'>\n" +
                "<FOLDERMAP SOURCEFOLDERNAME='" + sourceFolder + "' SOURCEREPOSITORYNAME='" + sourceRepo + "' TARGETFOLDERNAME='" + targetFolder + "' TARGETREPOSITORYNAME='" + targetRepo + "' />\n" +
                         "<RESOLVECONFLICT>\n" +
                         "<TYPEOBJECT OBJECTTYPENAME='ALL' RESOLUTION='REPLACE'/>\n" +
                          "</RESOLVECONFLICT >\n" +
                          "</IMPORTPARAMS>\n";
                File.WriteAllText("importXml.xml", controlFileTemplate);
            }






        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPCUtilities
{
    namespace IpcPmrep
    {
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
                //WA delay for remove connection header
                Thread.Sleep(1000);
                CheckConnectionsResult(_outputResult.ToString());
                _outputResult.Clear();
            }

            private static bool _workFlag = true;
            private bool CommandIsExit(string command)
            {
                if (command.Contains("exit"))
                    return false;
                return true;
            }

            private async Task<string> WaitForEnd(string command)
            {
                _workFlag=CommandIsExit(command);
                _imputCommand.WriteLine(command);
                while (_workFlag)
                {
                   await Task.Delay(300);
                }
                return _outputResult.ToString();
            }
            private void ThrowWorkError()
            {
                var outputResult = _outputResult.ToString();
                if (outputResult.Contains("Failed to execute "))
                {
                    throw new Exception(outputResult);
                }
            }
            internal string ExecuteCommand(string command)
            {
                _outputResult.Clear();
                var result = WaitForEnd(command).Result;
                _workFlag = false;
                ThrowWorkError();
                return result;
            }
            internal async Task<string> ExecuteCommandAsync(string command)
            {
                _outputResult.Clear();
                var result = await WaitForEnd(command);
                _workFlag = false;
                ThrowWorkError();
                return result;
            }
            private readonly  string _applicationName = "pmrep>";
            private  void _pmrep_OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                LogWriter.Write(e.Data);
                if (!string.IsNullOrEmpty(e.Data) && !e.Data.Contains("connect completed successfully."))
                {
                    if (e.Data.Contains(" completed successfully.") || e.Data.Contains("Failed to execute ") || e.Data.Contains("Repository connection failed.") )
                    {
                        
                        if (e.Data.Contains(_applicationName))
                            _outputResult.AppendLine(e.Data.Substring(e.Data.IndexOf(_applicationName,StringComparison.CurrentCulture) + _applicationName.Length));
                        else
                            _outputResult.AppendLine(e.Data);

                        _workFlag = false;
                    }
                    else 
                    {
                        if (e.Data.Contains(_applicationName))
                            _outputResult.AppendLine(e.Data.Substring(e.Data.IndexOf(_applicationName, StringComparison.CurrentCulture) + _applicationName.Length));
                        else
                            _outputResult.AppendLine(e.Data);
                    }
                }
            }
            private void CheckConnectionsResult(string row)
            {
                if(row.Contains("Repository connection failed."))
                {
                    Dispose();
                    throw new Exception(row);
                }
            }

            private static string[] _ignoreLines = { "Invoked", "All Rights Reserved", "Informatica(r)","Connected to repository " ,"Copyright (c)", "This Software", "Completed at", " completed successfully" };

            private static bool CheckingRowIsNotGarbage(string row)
            {
                foreach (var ignoreItem in _ignoreLines)
                    if (row.ToLower(CultureInfo.CurrentCulture).Contains(ignoreItem.ToLower(CultureInfo.CurrentCulture)))
                        return true;
                return false;
            }

            internal  string RemoveResultHeader(string result)
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

            internal string[] ConvertResultToArray(string result)
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

            internal bool CheckErrorInResult(string result)
            {
                if (result.Length == 0 || result.ToLower(CultureInfo.CurrentCulture).Contains("failed"))
                {
                    LogWriter.Write(result);
                    return false;
                }
                return true;
            }

            internal void CreateControlImportFile(string sourceRepo, string[] folders, string targetRepo, string dtdFile, string encoding)
            {
                encoding = string.IsNullOrEmpty(encoding) ? "windows-1251" : encoding;

                string controlFileTemplate = "<?xml version='1.0' encoding='" + encoding + "'?>" +

                "<!DOCTYPE IMPORTPARAMS SYSTEM '" + dtdFile + "'>\n" +
                "<IMPORTPARAMS CHECKIN_AFTER_IMPORT='NO'>\n";
                foreach(var folder in folders)
                {
                    controlFileTemplate += "<FOLDERMAP SOURCEFOLDERNAME='" + folder + "' SOURCEREPOSITORYNAME='" + sourceRepo + "' TARGETFOLDERNAME='" + folder + "' TARGETREPOSITORYNAME='" + targetRepo + "' />\n";
                }
                controlFileTemplate+=   "<RESOLVECONFLICT>\n" +
                         "<TYPEOBJECT OBJECTTYPENAME='ALL' RESOLUTION='REPLACE'/>\n" +
                          "</RESOLVECONFLICT >\n" +
                          "</IMPORTPARAMS>\n";
                File.WriteAllText("importXml.xml", controlFileTemplate);
            }

            #region IDisposable Support
            private bool disposedValue = false;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        ExecuteCommand("exit");
                        _pmrep.Close();
                        _outputResult.Clear();
                        _imputCommand.Dispose();
                        _pmrep.Dispose();
                    }

                    disposedValue = true;
                }
            }
            public void Dispose()
            {
                Dispose(true);
            }
            #endregion
        }

    }
}

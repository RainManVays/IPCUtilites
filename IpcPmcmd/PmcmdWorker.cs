using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities.IpcPmcmd
{
    internal class PmcmdWorker:IDisposable
    {

        private StringBuilder _outputResult = null;
        private Process _pmcmdProcess;
        private StreamWriter _imputCommand;

        public PmcmdWorker(string pmcmd, string connectionCmd)
        {
            _pmcmdProcess = new Process()
            {
                StartInfo = new ProcessStartInfo(pmcmd)
                {
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };
            _outputResult = new StringBuilder("");
            _pmcmdProcess.OutputDataReceived += PmcmdProcess_OutputDataReceived;
            _pmcmdProcess.ErrorDataReceived += _pmcmdProcess_ErrorDataReceived;
            _pmcmdProcess.Start();
            _pmcmdProcess.BeginErrorReadLine();
            _pmcmdProcess.BeginOutputReadLine();
            
            _imputCommand = _pmcmdProcess.StandardInput;
            _imputCommand.WriteLine(connectionCmd);
        }

        private void _pmcmdProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("err " + e.Data);
            _workLock = false;
            
        }

        private static bool _workLock = true;
        private bool IsNotReturnResult(string command)
        {
            if (command.Contains("exit")|| command.Contains("abortworkflow")|| command.Contains("startworkflow"))
                return false;
            return true;
        }
        int _counter = 0;
        private async Task<string> WaitForEnd(string command)
        {
            _counter = 0;
            _workLock = IsNotReturnResult(command);
            _imputCommand.WriteLine(command);
            while (_workLock)
            {
                //spike
                await Task.Delay(PmcmdSettings._delayInterval);
                if (_counter > PmcmdSettings._countDelay)
                    ExecuteCommand("exit");
                else
                    _counter++;
            }
            return _outputResult.ToString();
        }
        private void ThrowWorkError()
        {
            var outputResult = _outputResult.ToString();
            if (outputResult.Contains("ERROR") || outputResult.Contains("DOM_10033") || outputResult.Contains("PCSF_46007"))
            {
                throw new Exception(outputResult);
            }
        }
        internal string ExecuteCommand(string command)
        {
            _outputResult.Clear();
            var result = WaitForEnd(command).Result;
            _workLock = false;
            ThrowWorkError();
            return result;

        }
        private static string _utilName = "pmcmd> ";
        private static string[] _ignoreLines = { "Connected to Integration service", "Invoked", "All Rights Reserved", "Informatica(r)", "Copyright (c)", "This Software", "Completed at", "completed successfully" };

        private static string ClearOutputData(string outputData)
        {
            if (!string.IsNullOrEmpty(outputData))
            {
                foreach (var ignoreItem in _ignoreLines)
                    if (outputData.ToLower(CultureInfo.CurrentCulture).Contains(ignoreItem.ToLower(CultureInfo.CurrentCulture)))
                        return "";

                if (outputData.Contains(_utilName))
                {
                    _workLock = false;
                    return outputData.Substring(outputData.IndexOf(_utilName, StringComparison.CurrentCulture) + _utilName.Length);
                }
            }
            return outputData;
        }
        private void PmcmdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            var clearResult = ClearOutputData(e.Data);
            if (!string.IsNullOrWhiteSpace(clearResult))
                _outputResult.AppendLine(clearResult);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                    _outputResult.Clear();
                    _outputResult = null;
                    _imputCommand.Dispose();
                }

                _pmcmdProcess.Dispose();
                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~PmcmdWorker() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}

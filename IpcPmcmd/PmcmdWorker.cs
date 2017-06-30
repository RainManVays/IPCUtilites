using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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
            _workFlag = false;
            
        }

        private static bool _workFlag = true;
        private bool CommandIsExit(string command)
        {
            if (command.Contains("disconnect"))
                return false;
            return true;
        }
        int _counter = 0;
        private async Task<string> WaitForEnd(string command)
        {
            _workFlag = CommandIsExit(command);
            _imputCommand.WriteLine(command);
            while (_workFlag)
            {
                await Task.Delay(PmcmdSettings._countDelay);
                if (_counter > 60)
                {
                    ExecuteCommand("exit");
                }
                else
                {
                    _counter++;
                }
            }
            return _outputResult.ToString();
        }

        internal string ExecuteCommand(string command)
        {
            // pmcmdProcess.WaitForExit();
            // pmcmdProcess.Dispose();
            _outputResult.Clear();
            var result = WaitForEnd(command).Result;
            _workFlag = false;
            return result;

        }
        private static string _utilName = "pmcmd> ";
        private static string[] _ignoreLines = { "Connected to Integration service", "Invoked", "All Rights Reserved", "Informatica(r)", "Copyright (c)", "This Software", "Completed at", "completed successfully" };

        private static string ClearOutputData(string outputData)
        {
            if (!string.IsNullOrEmpty(outputData))
            {
                
                foreach (var ignoreItem in _ignoreLines)
                {
                    if (outputData.ToLower(CultureInfo.CurrentCulture).Contains(ignoreItem.ToLower(CultureInfo.CurrentCulture)))
                        return "";
                }

                if (outputData.Contains(_utilName))
                {
                    _workFlag = false;
                    return outputData.Substring(outputData.IndexOf(_utilName, StringComparison.CurrentCulture) + _utilName.Length);
                }
            }
            return outputData;
        }
        private  void PmcmdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
           // Console.WriteLine(" 456" + e.Data);
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
                }

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

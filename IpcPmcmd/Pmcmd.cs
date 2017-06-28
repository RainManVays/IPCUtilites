using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities
{
    namespace IpcPmcmd
    {
        /// <summary>
        /// Manage workflows. Use pmcmd to start, stop, schedule, and monitor workflows.
        /// </summary>
        public class Pmcmd:IDisposable
        {
            private string _pmcmdFile;
            private PmcmdConnection _connectionValue;
            private string _connectionCommand = "";
            PmcmdWorker _pmwork;
            public Pmcmd(string pmcmdfile, PmcmdConnection parameters, string logFile = null)
            {

                if (!File.Exists(pmcmdfile))
                    throw new FileNotFoundException("File not found!", pmcmdfile);
                _pmcmdFile = pmcmdfile;
                _connectionValue = parameters ?? throw new ArgumentNullException("parameters", "parameters is null");
                _connectionCommand = "connect " + parameters.Domain + parameters.Service + parameters.Password + parameters.UserName;
                _pmwork = new PmcmdWorker(pmcmdfile, _connectionCommand);
                // PmcmdWorker.ExecuteCommand(pmcmdfile, command);

            }

            public bool AbortTask()
            {
                return false;
            }
            public bool Abortworkflow()
            {
                return false;
            }
            public bool Disconnect()
            {
                return false;
            }
            public bool Exit()
            {
                return false;
            }
            public string GetRunningSessionsDetails()
            {
                var command = "getrunningsessionsdetails";
                var result = _pmwork.ExecuteCommand(command);
                return result;
            }
            public string GetServiceDetails(WorkflowsStatus type)
            {
                var command = "getservicedetails " + type.Value;
                var result = _pmwork.ExecuteCommand(command);
                ServiceDetailAdapter sdAdapter = new ServiceDetailAdapter();

                Console.WriteLine(sdAdapter.SetServiceDetailsData(result).ServiceStatus);
                Console.WriteLine(sdAdapter.SetServiceDetailsData(result).NumScheduledWorkflows);
                return result;
            }
           
            public string GetServiceProperties()
            {
                var command = "getserviceproperties";
                var result = _pmwork.ExecuteCommand(command);
                return result;
            }
            public bool GetSessionStatistics()
            {
                return false;
            }
            public bool GetTaskDetails()
            {
                return false;
            }
            public bool GetWorkflowDetails()
            {
                return false;
            }
            public bool PingService()
            {
                var command = "pingservice";
                var result = _pmwork.ExecuteCommand(command);
                if (result.ToLower(CultureInfo.CurrentCulture).Contains("integration service is alive"))
                {
                    return true;
                }
                return false;
            }
            public bool RecoverWorkflow()
            {
                return false;
            }
            public string ScheduleWorkflow(string folder, string workflow)
            {
                var command = "scheduleworkflow -folder " + folder + " " + workflow;
                var result = _pmwork.ExecuteCommand(command);
                return result;
            }
            public string ShowSettings()
            {
                var result = _pmwork.ExecuteCommand("showsettings");
                return result;
            }
            public bool StartTask()
            {
                return false;
            }
            public bool StartWorkflow()
            {
                return false;
            }
            public bool StopTask()
            {
                return false;
            }
            public bool StopWorkflow()
            {
                return false;
            }
            public string UnscheduleWorkflow(string folder, string workflow)
            {
                var command = "unscheduleworkflow -folder "+folder+" "+workflow;
                var result = _pmwork.ExecuteCommand(command);
                return result;
            }
            public string Version()
            {
                var result = _pmwork.ExecuteCommand("version");
                return result;
            }

            public bool WaitTask()
            {
                return false;
            }
            public bool WaitWorkflow()
            {
                return false;
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
            // ~Pmcmd() {
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
}

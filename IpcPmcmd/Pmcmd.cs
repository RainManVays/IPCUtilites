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
            PmcmdWorker _pmwork;
            public Pmcmd(string pmcmdfile, PmcmdConnection parameters, string logFile = null)
            {

                if (!File.Exists(pmcmdfile))
                    throw new FileNotFoundException("File not found!", pmcmdfile);
                var connectionValue = parameters ?? throw new ArgumentNullException("parameters", "parameters is null");
                var connectionCommand = "connect " + parameters.Domain + parameters.Service + parameters.Password + parameters.UserName;
                _pmwork = new PmcmdWorker(pmcmdfile, connectionCommand);
            }

            public string AbortTask(PmcmdAbortTask parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.Wait ? " -wait " : " -nowait ";

                var command = "aborttask " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + parameters.Workflow
                                                  + otherParams
                                                  +parameters.TaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
            }
            public string Abortworkflow(PmcmdAbortWorkflow parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.Wait ? " -wait " : " -nowait ";

                var command = "abortworkflow " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + otherParams
                                                  + parameters.Workflow;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
            }
            public void Disconnect()
            {
                _pmwork.ExecuteCommand("disconnect");
            }
            public void Exit()
            {
                _pmwork.ExecuteCommand("exit");
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
                ServiceDetailsAdapter sdAdapter = new ServiceDetailsAdapter();

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
            public string GetSessionStatistics(PmcmdGetSessionStatistics parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "getsessionstatistics " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + parameters.Workflow
                                                  + parameters.WorkflowTaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
            }
            public TaskDetails  GetTaskDetails(PmcmdGetTaskDetails parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "gettaskdetails " + parameters.Folder
                                                  + parameters.RunInsName
                                                  + parameters.Workflow
                                                  + parameters.TaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return TaskDetailsAdapter.GetConvertsResultToTaskDetails(result);
            }
            public WorkflowDetails GetWorkflowDetails(string folder, string workflow)
            {
                Guard.ThrowIsNull(folder,workflow);

                var command = "getworkflowdetails -folder " + folder+" "+workflow;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return WorkflowDetailsAdapter.GetConvertResultToWfDetails(result);
            }

            public WorkflowDetails GetWorkflowDetails(PmcmdGetWorkflowDetails parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "getworkflowdetails " + parameters.Folder
                                                  + parameters.RunInsName
                                                  + parameters.WorkflowRunId
                                                  + parameters.Workflow;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return WorkflowDetailsAdapter.GetConvertResultToWfDetails(result);
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
            public string RecoverWorkflow(PmcmdRecoverWorkflow parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "recoverworkflow " + parameters.Folder
                                                  + parameters.ParamFile
                                                  + parameters.LocalParamFile;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
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
            public string StartTask(PmcmdStartTask parameters)
            {
                Guard.ThrowIsNull(parameters);
                var otherParams = parameters.Wait ? " -wait " : " -nowait ";
                otherParams = parameters.Recovery ? " -recovery " : " -norecovery ";
                var command = "starttask " + parameters.Folder
                                                  + parameters.RunInsName
                                                  + parameters.ParamFile
                                                  + parameters.Workflow
                                                  + otherParams
                                                  + parameters.TaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
            }
            public string StartWorkflow(PmcmdStartWorkflow parameters)
            {
                Guard.ThrowIsNull(parameters);
                var otherParams = parameters.Wait ? " -wait " : " -nowait ";
                otherParams = parameters.Recovery ? " -recovery " : " -norecovery ";
                var command = "startworkflow " + parameters.Folder
                                                  + parameters.RunInsName
                                                  + parameters.ParamFile
                                                  + parameters.LocalParamFile
                                                  + parameters.StartFrom
                                                  + parameters.Osprofile
                                                  + otherParams
                                                  + parameters.Workflow;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
            }
            public string StopTask(PmcmdStopTask parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.Wait ? " -wait " : " -nowait ";

                var command = "stoptask " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + otherParams
                                                  + parameters.Workflow
                                                  + parameters.TaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
            }
            public string StopWorkflow(PmcmdStopWorkflow parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.Wait ? " -wait " : " -nowait ";

                var command = "stopworkflow " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + otherParams
                                                  + parameters.Workflow;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
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

            public string WaitTask(PmcmdWaitTask parameters)
            {
                Guard.ThrowIsNull(parameters);


                var command = "waittask " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + parameters.Workflow
                                                  + parameters.TaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
            }
            public string WaitWorkflow(PmcmdWaitWorkflow parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "waitworkflow " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + parameters.Workflow;

                var result = _pmwork.ExecuteCommand(command);
                //SetLastCommandResult(result);
                return result;
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

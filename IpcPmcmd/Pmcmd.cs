using IPCUtilities.IpcPmcmd.CommandObjects;
using System;
using System.Globalization;
using System.IO;

namespace IPCUtilities.IpcPmcmd
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
                return result;
            }
            /// <summary>
            /// Aborts a workflow
            /// </summary>
            /// <param name="parameters">command parameters</param>
            /// <returns></returns>
            public void Abortworkflow(PmcmdAbortWorkflow parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.Wait ? " -wait " : " -nowait ";

                var command = "abortworkflow " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + otherParams
                                                  + parameters.Workflow;

                _pmwork.ExecuteCommand(command);
            }

            /// <summary>
            /// Aborts a workflow
            /// </summary>
            /// <param name="folder">Name of the folder containing the workflow</param>
            /// <param name="workflow">Name of the workflow</param>
            /// <returns></returns>
            public void Abortworkflow(string folder, string workflow)
            {
                Guard.ThrowIsNull(folder, workflow);
                var command = "abortworkflow -folder " + folder + " " + workflow;

               _pmwork.ExecuteCommand(command);
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
                return result;
            }
           
            public string GetServiceProperties()
            {
                var command = "getserviceproperties";
                var result = _pmwork.ExecuteCommand(command);
                return result;
            }
            /// <summary>
            /// Information about a session
            /// </summary>
            /// <param name="folder">folder contains a workflow</param>
            /// <param name="workflow">workflow name contains a session</param>
            /// <param name="sessionName">session name</param>
            /// <returns>object session statistic</returns>
            public SessionStatistic GetSessionStatistics(string folder, string workflow, string sessionName)
            {
                Guard.ThrowIsNull(folder, workflow, sessionName);
                var command = "getsessionstatistics -folder " + folder
                                                  + " -workflow " + workflow + " " + sessionName;
                var result = _pmwork.ExecuteCommand(command);
                return SessionStatisticAdapter.GetConvertsResultToTaskDetails(result);
            }
            /// <summary>
            /// Information about a session
            /// </summary>
            /// <param name="parameters">command parameters</param>
            /// <returns>object session statistic</returns>
            public SessionStatistic GetSessionStatistics(PmcmdGetSessionStatistics parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "getsessionstatistics " + parameters.Folder
                                                  + parameters.WorkflowRunId
                                                  + parameters.RunInsName
                                                  + parameters.Workflow
                                                  + parameters.WorkflowTaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                return SessionStatisticAdapter.GetConvertsResultToTaskDetails(result);
            }
            /// <summary>
            /// Information about a task
            /// </summary>
            /// <param name="folder">folder contains a workflow</param>
            /// <param name="workflow">workflow name contains a task</param>
            /// <param name="taskName">task\session name</param>
            /// <returns>object task details</returns>
            public TaskDetails GetTaskDetails(string folder, string workflow, string taskName)
            {
                Guard.ThrowIsNull(folder, workflow, taskName);
                var command = "gettaskdetails -folder " + folder
                                                  +" -workflow "+ workflow+" "+ taskName;

                var result = _pmwork.ExecuteCommand(command);
                return TaskDetailsAdapter.GetConvertsResultToTaskDetails(result);
            }
            /// <summary>
            /// Information about a task
            /// </summary>
            /// <param name="parameters">command parameters</param>
            /// <returns>object task details</returns>
            public TaskDetails GetTaskDetails(PmcmdGetTaskDetails parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "gettaskdetails " + parameters.Folder
                                                  + parameters.RunInsName
                                                  + parameters.Workflow
                                                  + parameters.TaskInstancePath;

                var result = _pmwork.ExecuteCommand(command);
                return TaskDetailsAdapter.GetConvertsResultToTaskDetails(result);
            }
            /// <summary>
            /// Information about a workflow
            /// </summary>
            /// <param name="folder">folder contains a workflow</param>
            /// <param name="workflow">workflow name</param>
            /// <returns>object workflow details</returns>
            public WorkflowDetails GetWorkflowDetails(string folder, string workflow)
            {
                Guard.ThrowIsNull(folder,workflow);

                var command = "getworkflowdetails -folder " + folder+" "+workflow;
                var result = _pmwork.ExecuteCommand(command);

                return WorkflowDetailsAdapter.GetConvertResultToWfDetails(result);
            }

            /// <summary>
            /// Information about a workflow
            /// </summary>
            /// <param name="parameters">command parameters</param>
            /// <returns>object workflow details</returns>
            public WorkflowDetails GetWorkflowDetails(PmcmdGetWorkflowDetails parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "getworkflowdetails " + parameters.Folder
                                                  + parameters.RunInsName
                                                  + parameters.WorkflowRunId
                                                  + parameters.Workflow;

                var result = _pmwork.ExecuteCommand(command);

                return WorkflowDetailsAdapter.GetConvertResultToWfDetails(result);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
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
            /// <summary>
            /// 
            /// </summary>
            /// <param name="parameters"></param>
            /// <returns></returns>
            public string RecoverWorkflow(PmcmdRecoverWorkflow parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "recoverworkflow " + parameters.Folder
                                                  + parameters.ParamFile
                                                  + parameters.LocalParamFile;

                var result = _pmwork.ExecuteCommand(command);

                return result;
            }
            /// <summary>
            /// Schedule a workflow. Use this command to reschedule a workflow that has been removed from the schedule.
            /// </summary>
            /// <param name="folder"></param>
            /// <param name="workflow"></param>
            /// <returns>scheduling result row</returns>
            public string ScheduleWorkflow(string folder, string workflow)
            {
                var command = "scheduleworkflow -folder " + folder + " " + workflow;
                var result = _pmwork.ExecuteCommand(command);
                return result;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
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

                return result;
            }
            public string StartWorkflow(string folder, string workflow)
            {
                Guard.ThrowIsNull(folder, workflow);

                var command = "startworkflow -folder " + folder + " " + workflow;
                var result = _pmwork.ExecuteCommand(command);

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

                return result;
            }
            /// <summary>
            /// Removes workflow from schedule.
            /// </summary>
            /// <param name="folder"></param>
            /// <param name="workflow"></param>
            /// <returns>unscheduling result row</returns>
            public string UnscheduleWorkflow(string folder, string workflow)
            {
                Guard.ThrowIsNull(folder, workflow);
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

                return result;
            }

            #region IDisposable Support
            private bool disposedValue = false;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                    }

                    disposedValue = true;
                }
            }

            // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
            // ~Pmcmd() {
            //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            //   Dispose(false);
            // }
            public void Dispose()
            {
                Dispose(true);
            }
            #endregion

        }
 }

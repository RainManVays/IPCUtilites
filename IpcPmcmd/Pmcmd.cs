using System;
using System.Collections.Generic;
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
        public class Pmcmd
        {
            private string _pmcmdFile;
            private PmcmdConnection _connectionValue;
            private string _connectionCommand = "";
            public  Pmcmd(string pmcmdfile, PmcmdConnection parameters, string logFile = null)
            {
                
                if (!File.Exists(pmcmdfile))
                    throw new FileNotFoundException("File not found!", pmcmdfile);
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                _pmcmdFile = pmcmdfile;
                _connectionValue = parameters;
                _connectionCommand = "connect " + parameters.Domain + parameters.Service + parameters.Password + parameters.UserName;
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
                var result = PmcmdWorker.ExecuteCommand(_pmcmdFile, _connectionCommand, command);
                return result;
            }
            public string GetServiceDetails(WorkflowsStatus type)
            {
                var command = "getservicedetails "+ type.Value;
                var result = PmcmdWorker.ExecuteCommand(_pmcmdFile, _connectionCommand, command);
                return result;
            }
            public string GetServiceDetailsData(WorkflowsStatus type)
            {
                var command = "getservicedetails " + type.Value;
                var result = PmcmdWorker.ExecuteCommand(_pmcmdFile, _connectionCommand, command);
                return result;
            }
            public string GetServiceProperties()
            {
                var command = "getserviceproperties";
                var result=PmcmdWorker.ExecuteCommand(_pmcmdFile,_connectionCommand, command);
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
                var result = PmcmdWorker.ExecuteCommand(_pmcmdFile, _connectionCommand, command);
                if (result.ToLower().Contains("integration service is alive"))
                {
                    return true;
                }
                return false;
            }
            public bool RecoverWorkflow()
            {
                return false;
            }
            public bool ScheduleWorkflow()
            {
                return false;
            }
            public string ShowSettings()
            {
                var command = "showsettings";
                var result = PmcmdWorker.ExecuteCommand(_pmcmdFile, _connectionCommand, command);
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
            public bool UnscheduleWorkflow()
            {
                return false;
            }
            public string Version()
            {
                var command = "version";
                var result = PmcmdWorker.ExecuteCommand(_pmcmdFile, _connectionCommand, command);
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

        }
    }
}

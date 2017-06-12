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
            public  Pmcmd(string pmcmdfile, PmcmdConnection parameters, string logFile = null)
            {
                
                if (!File.Exists(pmcmdfile))
                    throw new FileNotFoundException("File not found!", pmcmdfile);
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                _pmcmdFile = pmcmdfile;
                _connectionValue = parameters;

            }

            public bool AbortTask()
            {
                return false;
            }
            public bool abortworkflow()
            {
                return false;
            }
            public bool Connect()
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
            public bool GetRunningSessionsDetails()
            {
                return false;
            }
            public bool GetServiceDetails()
            {
                return false;
            }
            public string GetServiceProperties()
            {
                var command= "getserviceproperties -t 60" + _connectionValue.Domain
                                                     + _connectionValue.Service;
                var result=PmcmdWorker.ExecuteCommand(_pmcmdFile, command);
                return result.output;
            }
            public bool getsessionstatistics()
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
            public bool Help()
            {
                return false;
            }
            public bool PingService()
            {
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
            public bool SetFolder()
            {
                return false;
            }
            public bool SetNoWait()
            {
                return false;
            }
            public bool SetWait()
            {
                return false;
            }
            public bool ShowSettings()
            {
                return false;
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
            public bool UnsetFolder()
            {
                return false;
            }
            public bool Version()
            {
                return false;
            }

            public bool WaitTask()
            {
                return false;
            }
            public bool WaitWorkflow()
            {
                return false;
            }
            public bool Using()
            {
                return false;
            }
        }
    }
}

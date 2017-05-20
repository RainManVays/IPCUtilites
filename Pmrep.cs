using System.Collections.Generic;
using System.IO;
using System;
namespace IpcPmrep
{
    /// <summary>
    /// Class for interacting with Informatica PowerCenter repository using the pmrep binary.
    /// You need Set envivorment variable Domain_info
    /// </summary>
    public class Pmrep
    {
        private string _pmrepFile;

        /// <summary>
        /// Connects to a repository. The first time you use pmrep in either command line or interactive mode, you must use     the Connect command. All commands require a connection to the repository except for the following commands
        /// </summary>
        /// <param name="pmrepfile"> full path to pmrep.exe</param>
        /// <param name="parameters">connection parameters</param>
        /// <param name="logFile">if you need write work log set full path to logfile</param>
        public Pmrep(string pmrepfile, PmrepConnection parameters, string logFile = null)
        {
            _pmrepFile = pmrepfile;

            if (!File.Exists(pmrepfile))
                throw new FileNotFoundException("File not found!", pmrepfile);
            if (logFile != null)
                LogWriter.SetLogFile(logFile);


            var command = "connect " + parameters.domain + parameters.hostName + parameters.password + parameters.port + parameters.repository + parameters.userName + parameters.timeout;
            var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
            LogWriter.Write(result.output);
            LogWriter.Write(result.errors);
            if (result.errors.Length > 0)
                throw new ApplicationException(result.errors);


        }
        public string[] AddToDeploymentGroup()
        {
            return null;
        }
        public string[] ApplyLabel()
        {
            return null;
        }
        public string[] AssignPermission()
        {
            return null;
        }
        public string[] BackUp()
        {
            return null;
        }
        public string[] ChangeOwner()
        {
            return null;
        }
        public string[] CheckIn()
        {
            return null;
        }
        public string[] CleanUp()
        {
            return null;
        }
        public string[] ClearDeploymentGroup()
        {
            return null;
        }
        public string[] Create()
        {
            return null;
        }
        public string[] CreateConnection()
        {
            return null;
        }
        public string[] CreateDeploymentGroup()
        {
            return null;
        }
        public string[] CreateFolder()
        {
            return null;
        }
        public string[] CreateLabel()
        {
            return null;
        }
        public string[] Delete()
        {
            return null;
        }
        public bool DeleteConnection(string connectionName, string connectionType = null)
        {
            string command = string.Empty;
            if (connectionType != null)
                command = "deleteconnection -f -n " + connectionName + " -s " + connectionType;
            else
                command = "deleteconnection -f -n " + connectionName;

            var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
            if (result.errors.Length > 0)
            {
                LogWriter.Write(result.errors);
                return false;
            }
            return true;
        }

        public string[] DeleteDeploymentGroup()
        {
            return null;
        }
        public string[] DeleteFolder()
        {
            return null;
        }
        public string[] DeleteLabel()
        {
            return null;
        }
        public string[] DeleteObject()
        {
            return null;
        }
        public string[] DeployDeploymentGroup()
        {
            return null;
        }
        public string[] DeployFolder()
        {
            return null;
        }
        public string[] ExecuteQuery()
        {
            return null;
        }
        public string[] Exit()
        {
            return null;
        }
        public string[] FindCheckout()
        {
            return null;
        }
        public string[] GetConnectionDetails()
        {
            return null;
        }
        public string[] GenerateAbapProgramToFile()
        {
            return null;
        }
        public string[] Help()
        {
            return null;
        }
        public string[] InstallAbapProgram()
        {
            return null;
        }
        /// <summary>
        /// Terminates user connections to the repository. You can terminate user connections based on the user name or 
        /// connection ID. You can also terminate all user connections to the repository.
        /// </summary>
        public bool KillUserConnection(string userName=null,string connectionID=null,bool terminateAll=false)
        {
            string command = string.Empty;
            if (terminateAll)
            {
                command = "killuserconnection -a";
            }
            else
            {
                if (userName != null)
                    command = "killuserconnection -n " + userName;
                else if(connectionID !=null)
                    command = "killuserconnection -i " + connectionID;
            }
            
            var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
            if (result.errors.Length > 0)
            {
                LogWriter.Write(result.errors);
                return false;
            }
            return true;
            
        }
        /// <summary>
        /// List all connection objects in the repository and their respective connection types.
        /// Args:None
        /// </summary>
        public string[] ListConnections()
        {
            var result = PmrepWorker.ExecuteCommand(_pmrepFile, "listconnections -t");
            LogWriter.Write(result.errors);
            return PmrepWorker.FormattingResult(result.output);
        }
        public string[] ListObjectDependencies()
        {
            return null;
        }
        public string[] ListObjects()
        {
            return null;
        }
        public string[] ListTablesBySess()
        {
            return null;
        }

        /// <summary>
        /// List all connection objects in the repository and their respective connection types.
        /// Args:None
        /// </summary>
        public string[] ListUserConnections()
        {
            var result = PmrepWorker.ExecuteCommand(_pmrepFile, "listuserconnections");
            LogWriter.Write(result.errors);
            return PmrepWorker.FormattingResult(result.output);
        }
        public string[] MassUpdate()
        {
            return null;
        }
        public string[] ModifyFolder()
        {
            return null;
        }
        public string[] Notify()
        {
            return null;
        }
        public string[] ObjectExport()
        {
            return null;
        }
        public string[] ObjectImport()
        {
            return null;
        }
        public string[] PurgeVersion()
        {
            return null;
        }
        public string[] Register()
        {
            return null;
        }
        public string[] RegisterPlugin()
        {
            return null;
        }
        public string[] Restore()
        {
            return null;
        }
        public string[] RollbackDeployment()
        {
            return null;
        }
        public string[] Run()
        {
            return null;
        }
        public string[] ShowConnectionInfo()
        {
            return null;
        }
        public string[] SwitchConnection()
        {
            return null;
        }
        public string[] TruncateLog()
        {
            return null;
        }
        public string[] UndoCheckout()
        {
            return null;
        }
        public string[] Unregister()
        {
            return null;
        }
        public string[] UnregisterPlugin()
        {
            return null;
        }
        public string[] UpdateConnection()
        {
            return null;
        }
        public string[] UpdateEmailAddr()
        {
            return null;
        }
        public string[] UpdateSeqGenVals()
        {
            return null;
        }
        public string[] UpdateSrcPrefix()
        {
            return null;
        }
        public string[] UpdateStatistics()
        {
            return null;
        }
        public string[] UpdateTargPrefix()
        {
            return null;
        }
        public string[] Upgrade()
        {
            return null;
        }
        public string[] UninstallAbapProgram()
        {
            return null;
        }
        public string[] Validate()
        {
            return null;
        }
        public string[] Version()
        {
            return null;
        }
        public string[] Using()
        {
            return null;
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System;

[assembly:CLSCompliant(true)]

namespace IPCUtilities
{
    namespace IpcPmrep
    {
        /// <summary>
        /// Performs repository administration tasks. Use pmrep to list repository objects, create and edit groups, and 
        /// restore and delete repositories.
        /// </summary>
        public class Pmrep
        {
            private string _pmrepFile;

            /// <summary>
            /// Connects to a repository. The first time you use pmrep in either command line or interactive mode, you must 
            /// use the Connect command. All commands require a connection to the repository except for the following 
            /// commands
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
            /// <summary>
            /// Adds objects to a deployment group. Use AddToDeploymentGroup to add source, target, transformation, mapping, 
            /// session, worklet, workflow, scheduler, session configuration, and task objects
            /// </summary>
            /// <param name="parameters"></param>
            /// <returns></returns>
            public bool AddToDeploymentGroup(PmrepAddDeploymentGroup parameters)
            {
                var command = "addtodeploymentgroup " + parameters.dbdSeparator + parameters.dependencyTypes + parameters.deploymentGroupName + parameters.folderName + parameters.objectName + parameters.objectSubType + parameters.objectType + parameters.persistentImputFile + parameters.versionNumber;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                if (result.errors.Length > 0)
                {
                    LogWriter.Write(result.errors);
                    return false;
                }
                return true;

            }
            public string[] ApplyLabel(PmrepApplyLabel parameters)
            {
                return null;
            }
            public string[] AssignPermission(PmrepPermissions parameters)
            {
                return null;
            }
            public string[] BackUp(PmrepBackup parameters)
            {
                return null;
            }
            public string[] ChangeOwner(PmrepChangeOwner parameters)
            {
                return null;
            }
            public string[] CheckIn(PmrepCheckIn parameters)
            {
                return null;
            }
            public string[] CleanUp()
            {
                return null;
            }
            public string[] ClearDeploymentGroup(string deploymentGroupName)
            {
                return null;
            }
            public string[] Create(PmrepCreate parameters)
            {
                return null;
            }
            public string[] CreateConnection(PmrepCreateConnection parameters)
            {
                return null;
            }
            public string[] CreateDeploymentGroup(PmrepNewDeploymentGroup parameters)
            {
                return null;
            }
            public string[] CreateFolder(PmrepNewFolder parameters)
            {
                return null;
            }
            public string[] CreateLabel(string labelName, string comments = null)
            {
                return null;
            }
            public string[] Delete(string repositoryPassword)
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

            public string[] DeleteDeploymentGroup(string groupName)
            {
                return null;
            }
            public string[] DeleteFolder(string folderName)
            {
                return null;
            }
            public string[] DeleteLabel(string labelName)
            {
                return null;
            }
            public string[] DeleteObject(string folderName, string objectName, string objectType)
            {
                return null;
            }
            public string[] DeployDeploymentGroup(PmrepRunDeploymentGroup parameters)
            {
                return null;
            }
            public string[] DeployFolder(PmrepDeployFolder parameters)
            {
                return null;
            }
            public string[] ExecuteQuery(PmrepQuery parameters)
            {
                return null;
            }
            /// <summary>
            /// Exits from the pmrep interactive mode.
            /// </summary>
            public void Exit()
            {
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "exit");
                if (result.errors.Length > 0)
                    LogWriter.Write(result.errors);
            }
            public string[] FindCheckout(PmrepCheckout parameters)
            {
                return null;
            }
            /// <summary>
            /// Lists the properties and attributes of a connection object as name-value pairs.
            /// </summary>
            /// <param name="connectionName">Required. Name of the connection to list details for.</param>
            /// <param name="connectionType">Required. Type of connection. A connection can be one of the following types:-
            /// Application
            /// FTP
            /// Loader
            /// Queue
            /// Relational</param>
            /// <returns></returns>
            public string[] GetConnectionDetails(string connectionName, string connectionType)
            {
                return null;
            }
            public string[] GenerateAbapProgramToFile(PmrepAbapProgram parameters)
            {
                return null;
            }
            public string[] InstallAbapProgram(PmrepAbapProgram parameters)
            {
                return null;
            }
            /// <summary>
            /// Terminates user connections to the repository. You can terminate user connections based on the user name or 
            /// connection ID. You can also terminate all user connections to the repository.
            /// </summary>
            public bool KillUserConnection(string userName = null, string connectionID = null, bool terminateAll = false)
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
                    else if (connectionID != null)
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
            public string[] ListObjectDependencies(PmrepObjectDependencies parameters)
            {
                return null;
            }
            public string[] ListObjects(PmrepObject parameters)
            {
                return null;
            }
            public string[] ListTablesBySess(string folderName, string sessionName, Enum sessionObjects)
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
            public void UpdateStatistics()
            {
            }
            public string[] UpdateTargPrefix()
            {
                return null;
            }
            public void Upgrade(string repositoryPassword)
            {

            }
            public string[] UninstallAbapProgram()
            {
                return null;
            }
            public string[] Validate()
            {
                return null;
            }
        }
    }
}
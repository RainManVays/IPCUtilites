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

                return PmrepWorker.CheckErrorInResult(result);

            }
            public bool ApplyLabel(PmrepApplyLabel parameters,bool acrossRepositories=false, bool moveLabel=false, bool comments=false)
            {
                //retest need
                var otherParams = acrossRepositories ? " -g ":"";
                otherParams += moveLabel ? " -m " : "";
                otherParams += comments ? " -c " : "";
                var command = "applylabel " + parameters.objectName
                                                  + parameters.objectType
                                                  + parameters.objectSubType
                                                  + parameters.folderName
                                                  + parameters.labelName
                                                  + parameters.dbdSeparator
                                                  + parameters.dependencyDirection
                                                  + parameters.dependencyObjectTypes
                                                  + parameters.persistentInputFile
                                                  + parameters.versionNumber
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool AssignPermission(PmrepPermissions parameters)
            {
                var command = "AssignPermission " + parameters.objectName
                                                  + parameters.objectType 
                                                  + parameters.objectSubType
                                                  + parameters.userName
                                                  + parameters.groupName
                                                  + parameters.permission
                                                  + parameters.securityDomain;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public string[] BackUp(PmrepBackup parameters)
            {
                return null;
            }
            public bool ChangeOwner(PmrepChangeOwner parameters)
            {
                var command = "ChangeOwner " + parameters.objectName
                                                   + parameters.objectType
                                                   + parameters.objectSubType
                                                   + parameters.newOwnerName
                                                   + parameters.securityDomain;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public string[] CheckIn(PmrepCheckIn parameters)
            {
                return null;
            }
            public bool CleanUp()
            {
                string command = "cleanup";

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool ClearDeploymentGroup(string deploymentGroupName)
            {
                string command = "cleardeploymentgroup -f -p "+deploymentGroupName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
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
            public bool CreateFolder(PmrepNewFolder parameters)
            {
                string command = "createfolder " + parameters.folderName +
                                                parameters.folderDescription +
                                                parameters.folderStatus +
                                                parameters.ownerName +
                                                parameters.ownerSecurityDomain +
                                                parameters.permissions +
                                                parameters.sharedFolder;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool CreateLabel(string labelName, string comments = null)
            {
                string command = "createlabel -a " + labelName + " -c " + comments;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
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
                return PmrepWorker.CheckErrorInResult(result);
            }

            public bool DeleteDeploymentGroup(string groupName)
            {
                string command = "deletedeploymentgroup -f -p " + groupName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool DeleteFolder(string folderName)
            {
                string command = "deletefolder -n " + folderName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool DeleteLabel(string labelName)
            {
                string command = "deletelabel -f -a " + labelName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool DeleteObject(string folderName, string objectName, string objectType)
            {
                string command = "deleteobject -f " + folderName + " -o " + objectName + " -n " + objectType;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
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
            public bool GetConnectionDetails(string connectionName, string connectionType)
            {
                string command = "getconnectiondetails -n " + connectionName + " -t " + connectionType;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
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
                return PmrepWorker.CheckErrorInResult(result);

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
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "listobjects "+parameters.columnSeparator
                                                                                  +parameters.dbdSeparator
                                                                                  +parameters.endOfListingIndicator+
                                                                                  parameters.endOfRecordIndicator+
                                                                                  parameters.folderName+
                                                                                  parameters.objectSubtype+
                                                                                  parameters.objectType+
                                                                                  parameters.printDatabaseType+
                                                                                  parameters.verbose);
                LogWriter.Write(result.errors);
                return PmrepWorker.FormattingResult(result.output);
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
            public bool Notify(string message)
            {
                string command = "notify -m " + message;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public string ObjectImport(PmrepObjectImport parameters,bool retainPersistentValue=true)
            {
                var otherParams = retainPersistentValue ? " -p " : "";
                string command = string.Empty;

              PmrepWorker.CreateControlImportFile(sourceFolder: parameters.sourceFolder,
                                                        sourceRepo: parameters.sourceRepo,
                                                        targetFolder: parameters.targetFolder,
                                                        targetRepo: parameters.targetRepo,
                                                        dtdFile: parameters.importDtdFile,
                                                        encoding: parameters.controlFileEncoding);
                    command = "objectimport " + parameters.importXml +
                                            " -c importXml.xml " +
                                            parameters.logFile +
                                            otherParams;
                

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return result.output;
            }
            public string ObjectImport(string importXml,string importControlFile,string logFileName=null, bool retainPersistentValue = true)
            {
                var otherParams = retainPersistentValue ? " -p " : "";
                otherParams += string.IsNullOrEmpty(logFileName) ? "" : " -l " + logFileName;

                string command = "objectimport " + "- i " + importXml +
                                                " -c "+importControlFile +
                                                otherParams;
  


                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return result.output;
            }

            public bool ObjectExport(PmrepObjectExport parameters,bool m=false,bool s = false, bool b = false, bool r = false)
            {
                var otherParams = m ? " -m " : "";
                    otherParams += s ? " -s " : "";
                    otherParams += b ? " -b " : "";
                    otherParams += r ? " -r " : "";
                var command = "objectexport " + parameters.folderName
                                                  + parameters.logFileName
                                                  + parameters.dbdSeparator
                                                  + parameters.persistentInputFile
                                                  + parameters.versionNumber
                                                  +parameters.xnlOutputFileName
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                Console.WriteLine(result.output);
                return PmrepWorker.CheckErrorInResult(result);
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
            public string ShowConnectionInfo()
            {
                return null;
            }
            public bool SwitchConnection(string oldConnectionName, string newConnectionName)
            {
                string command = "switchconnection -o " + oldConnectionName + " -n " + newConnectionName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
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
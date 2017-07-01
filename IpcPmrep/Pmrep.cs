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
        public class Pmrep: IDisposable
        {
            private string _lastCommandResult;
            private PmrepWorker _pmWork;
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
                

                if (!File.Exists(pmrepfile))
                    throw new FileNotFoundException("File not found!", pmrepfile);
                
                Guard.ThrowIsNull(parameters);
                if (logFile != null)
                    LogWriter.SetLogFile(logFile);
                var command = "connect " + parameters.Domain + parameters.HostName + parameters.Password + parameters.Port + parameters.Repository + parameters.UserName + parameters.Timeout;
                _pmWork = new PmrepWorker(pmrepfile, command);
            }
           
            private void SetLastCommandResult(string result)
            {
                _lastCommandResult = result;
            }
            public string GetLastCommandResult()
            {
                return _lastCommandResult;
            }
            /// <summary>
            /// Adds objects to a deployment group. Use AddToDeploymentGroup to add source, target, transformation, mapping, 
            /// session, worklet, workflow, scheduler, session configuration, and task objects
            /// </summary>
            /// <param name="parameters"></param>
            /// <returns>True or False</returns>
            public bool AddToDeploymentGroup(PmrepAddDeploymentGroup parameters)
            {
                Guard.ThrowIsNull(parameters);

                var command = "addtodeploymentgroup " + parameters.DbdSeparator 
                                                        + parameters.DependencyTypes 
                                                        + parameters.DeploymentGroupName 
                                                        + parameters.FolderName 
                                                        + parameters.ObjectName 
                                                        + parameters.ObjectSubType 
                                                        + parameters.ObjectType 
                                                        + parameters.PersistentImputFile 
                                                        + parameters.VersionNumber;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
               
                return _pmWork.CheckErrorInResult(result);

            }
            public bool ApplyLabel(PmrepApplyLabel parameters,bool acrossRepositories=false, bool moveLabel=false, bool comments=false)
            {
                
                Guard.ThrowIsNull(parameters);
                //retest need
                var otherParams = acrossRepositories ? " -g ":"";
                otherParams += moveLabel ? " -m " : "";
                otherParams += comments ? " -c " : "";
                var command = "applylabel " + parameters.ObjectName
                                                  + parameters.ObjectType
                                                  + parameters.ObjectSubType
                                                  + parameters.FolderName
                                                  + parameters.LabelName
                                                  + parameters.DbdSeparator
                                                  + parameters.DependencyDirection
                                                  + parameters.DependencyObjectTypes
                                                  + parameters.PersistentInputFile
                                                  + parameters.VersionNumber
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool AssignPermission(PmrepPermissions parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "AssignPermission " + parameters.ObjectName
                                                  + parameters.ObjectType 
                                                  + parameters.ObjectSubType
                                                  + parameters.UserName
                                                  + parameters.GroupName
                                                  + parameters.Permission
                                                  + parameters.SecurityDomain;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool BackUp(string outputFileName)
            {
                Guard.ThrowIsNull(outputFileName);
                var command = "backup -o " + outputFileName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool BackUp(string outputFileName,string description=null,bool overiteExistingFile=false,bool skipLog=false,bool skipDeployHistory=false,
                                    bool skipMxData=false,bool skipTaskStatistic=false)
            {
                var command = "backup -o " + outputFileName;
                var otherParams = string.IsNullOrEmpty(description) ? "" : " -d "+description;
                otherParams += overiteExistingFile ? " -f " : "";
                otherParams += skipLog ? " -b " : "";
                otherParams += skipDeployHistory ? " -j " : "";
                otherParams += skipMxData ? " -q " : "";
                otherParams += skipTaskStatistic ? " -v " : "";

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool ChangeOwner(PmrepChangeOwner parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "ChangeOwner " + parameters.ObjectName
                                                   + parameters.ObjectType
                                                   + parameters.ObjectSubType
                                                   + parameters.NewOwnerName
                                                   + parameters.SecurityDomain;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public string CheckIn(PmrepCheckIn parameters)
            {
                
                Guard.ThrowIsNull(parameters);
                var command = "checkin " + parameters.ObjectName
                                                    + parameters.ObjectType
                                                    + parameters.ObjectSubType
                                                    + parameters.FolderName
                                                    + parameters.DbdSeparator
                                                    + parameters.Comments;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            /// <summary>
            /// Cleans up any persistent resource created by pmrep.
            /// </summary>
            /// <returns>True or False</returns>
            public bool CleanUp()
            {
                string command = "cleanup";

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool ClearDeploymentGroup(string deploymentGroupName)
            {
                Guard.ThrowIsNull(deploymentGroupName);

                string command = "cleardeploymentgroup -f -p "+deploymentGroupName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public string Create(PmrepCreate parameters,bool createGlobalRepo=false,bool enableVersioning=false)
            {
                
                Guard.ThrowIsNull(parameters);
                var otherParams = createGlobalRepo ? " -g " : "";
                otherParams += enableVersioning ? " -v " : "";

                var command = "create " + parameters.DomainUserName
                                                    + parameters.DomainPassword
                                                    + parameters.DomainUserSecurity
                                                    + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            public bool CreateConnection(string connectionType, string connectionName,string codePage)
            {
                Guard.ThrowIsNull(connectionType, connectionName, codePage);

                var command = "createconnection -s " +connectionType 
                                                    + " -n "+connectionName
                                                    + " -l "+codePage;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateConnection(string connectionType, string connectionName, string codePage, string connectionString)
            {
                Guard.ThrowIsNull(connectionType, connectionName, codePage, connectionString);
                var command = "createconnection -s " + connectionType
                                                    + " -n " + connectionName
                                                    + " -l " + codePage
                                                    + " -c " + connectionString;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateConnection(PmrepCreateConnection parameters, bool t=false,bool x=false)
            {
                
                Guard.ThrowIsNull(parameters);
                var otherParams = t ? " -t " : "";
                otherParams += x ? " -x " : "";

                var command = "createconnection " + parameters.CodePage
                                                    + parameters.ConectionEnvironmentSQL
                                                    + parameters.ConnectionAttributes
                                                    + parameters.ConnectionName
                                                    +" -s "+ parameters.ConnectionType.Value
                                                    + parameters.ConnectString
                                                    + parameters.DatabaseName
                                                    + parameters.DataSourceName
                                                    + parameters.DomainName
                                                    + parameters.PacketSize
                                                    + parameters.Password
                                                    + parameters.RollbackSegment
                                                    + parameters.ServerName
                                                    + parameters.TransactionEnvironmentSQL
                                                    + parameters.UserName
                                                    + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateDeploymentGroup(string deploymentGroupName)
            {
                Guard.ThrowIsNull(deploymentGroupName);

                var command = "createdeploymentgroup -p " + deploymentGroupName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateDeploymentGroup(PmrepNewDeploymentGroup parameters)
            {
                
                Guard.ThrowIsNull(parameters);
                var command = "createdeploymentgroup " + parameters.DeploymentGroupName
                                                   + parameters.DeploymentGroupType
                                                   + parameters.QueryName
                                                   + parameters.QueryType
                                                   + parameters.Comments;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateFolder(string folderName)
            {
                Guard.ThrowIsNull(folderName);
                string command = "createfolder -n " + folderName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateFolder(string folderName,string permissions)
            {
                Guard.ThrowIsNull(folderName, permissions);

                string command = "createfolder -n " + folderName+ " -p " + permissions;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateFolder(PmrepNewFolder parameters)
            {
                
                Guard.ThrowIsNull(parameters);

                string command = "createfolder " + parameters.FolderName +
                                                parameters.FolderDescription +
                                                parameters.FolderStatus +
                                                parameters.OwnerName +
                                                parameters.OwnerSecurityDomain +
                                                parameters.Permissions +
                                                parameters.SharedFolder;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool CreateLabel(string labelName)
            {
                Guard.ThrowIsNull(labelName);

                string command = "createlabel -a " + labelName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }

            public bool CreateLabel(string labelName, string comments)
            {
                Guard.ThrowIsNull(labelName,comments);

                string command = "createlabel -a " + labelName + " -c " + comments;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool Delete(string repositoryPassword)
            {
                Guard.ThrowIsNull(repositoryPassword);

                string command = "delete -f  -x " + repositoryPassword;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }


            /// <summary>
            /// Delete connection from the repository.
            /// </summary>
            /// <param name="connectionName">Name of the connection to delete</param>
            /// <returns>True or False</returns>
            public bool DeleteConnection(string connectionName)
            {
                Guard.ThrowIsNull(connectionName);

                string  command = "deleteconnection -f -n " + connectionName;
                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            /// <summary>
            /// Delete connection from the repository.
            /// </summary>
            /// <param name="connectionName">Name of the connection to delete</param>
            /// <param name="connectionType">Type of connection</param>
            /// <returns>True or False</returns>
            public bool DeleteConnection(string connectionName, ConnectionType connectionType )
            {
                Guard.ThrowIsNull(connectionName);

                string command = "deleteconnection -f -n " + connectionName + " -s " + connectionType;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool DeleteDeploymentGroup(string groupName)
            {
                Guard.ThrowIsNull(groupName);

                string command = "deletedeploymentgroup -f -p " + groupName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            /// <summary>
            /// Delete a folder from the repository.
            /// </summary>
            /// <param name="folderName">Name of the folder</param>
            /// <returns>True or False</returns>
            public bool DeleteFolder(string folderName)
            {
                Guard.ThrowIsNull(folderName);

                string command = "deletefolder -n " + folderName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            /// <summary>
            /// Delete a label from the repository.
            /// </summary>
            /// <param name="labelName">Name of the label</param>
            /// <returns>True or False<</returns>
            public bool DeleteLabel(string labelName)
            {
                Guard.ThrowIsNull(labelName);

                string command = "deletelabel -f -a " + labelName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }

            /// <summary>
            /// Delete object. Use DeleteObject to delete a  target, source, user-def function,  mapping, mapplet, session, worklet or workflow.
            /// Don`t works in a versioned repository
            /// </summary>
            /// <param name="folderName">Name of the folder that contains the object</param>
            /// <param name="objectName">Name of the object you are deleting</param>
            /// <param name="objectType">Type of the object you are deleting: source, target, mapplet, session, mapping, user def func, worklet, workflow</param>
            /// <returns>True or False<</returns>
            public bool DeleteObject(string folderName, string objectName, string objectType)
            {
                Guard.ThrowIsNull(folderName,objectName,objectType);
                string command = "deleteobject -f " + folderName + " -o " + objectName + " -n " + objectType;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public string DeployDeploymentGroup(string deploymentGroupName,string controlFileName,string targetRepositoryName)
            {
                Guard.ThrowIsNull(deploymentGroupName,controlFileName,targetRepositoryName);

                string command = "deploydeploymentgroup -p " + deploymentGroupName + " -c " + controlFileName + " -r " + targetRepositoryName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            public string DeployDeploymentGroup(PmrepRunDeploymentGroup parameters)
            {
                
                Guard.ThrowIsNull(parameters);
                string command = "deploydeploymentgroup " + parameters.DeploymentGroupName +
                                                parameters.ControlFileName +
                                                parameters.TargetRepositoryName +
                                                parameters.TargetRepositoryUser +
                                                parameters.TargetRepositoryUserSecurityDomain +
                                                parameters.TargetRepositoryPassword +
                                                parameters.TargetRepositoryPasswordEnvVar +
                                                parameters.TargetDomainName +
                                                parameters.TargetPortalHostName +
                                                parameters.TargetPortalPortNumber +
                                                parameters.LogFileName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            public string DeployFolder(string folderName, string controlFileName, string targetRepositoryName)
            {
                Guard.ThrowIsNull(folderName, controlFileName, targetRepositoryName);

                string command = "deployfolder -f " + folderName + " -c " + controlFileName + " -r " + targetRepositoryName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            public string DeployFolder(PmrepDeployFolder parameters)
            {
                Guard.ThrowIsNull(parameters);
                string command = "deployfolder " + parameters.FolderName +
                                                parameters.ControlFileName +
                                                parameters.TargetRepositoryName +
                                                parameters.TargetRepositoryUser +
                                                parameters.TargetRepositoryUserSecurityDomain +
                                                parameters.TargetRepositoryPassword +
                                                parameters.TargetRepositoryPasswordEnvVar +
                                                parameters.TargetDomainName +
                                                parameters.TargetPortalHostName +
                                                parameters.TargetPortalPortNumber +
                                                parameters.LogFileName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            /// <summary>
            /// Runs a query and return result
            /// </summary>
            /// <param name="queryName">Name of the query to run</param>
            /// <returns></returns>
            public string ExecuteQuery(string queryName)
            {
                Guard.ThrowIsNull(queryName);

                string command = "executequery -q " + queryName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            /// <summary>
            /// Runs a query and return result
            /// </summary>
            /// <param name="parameters"></param>
            /// <param name="append">Appends the query results to output file</param>
            /// <param name="verbose">Displays more than the minimum information about the objects</param>
            /// <param name="printDBtype">Print the database type of sources and targets</param>
            /// <param name="dontIncludeParentPath">Don`t include the full parent path of non-reusable objects in the query result</param>
            /// <returns></returns>
            public string ExecuteQuery(PmrepQuery parameters, bool append=false,bool verbose = false, bool printDBtype = false, bool dontIncludeParentPath = false)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = append ? " -a " : "";
                otherParams += verbose ? " -b " : "";
                otherParams += printDBtype ? " -y " : "";
                otherParams += dontIncludeParentPath ? " -n " : "";

                var command = "executequery " + parameters.QueryName
                                                  + parameters.QueryType
                                                  + parameters.OutputPersistentFileName
                                                  + parameters.ColumnSeparator
                                                  + parameters.EndOfRecordSeparator
                                                  + parameters.EndOfListingIndicator
                                                  + parameters.DbdSeparator
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
            }
            /// <summary>
            /// Exits from the pmrep interactive mode.
            /// </summary>
            public bool Exit()
            {
                var result = _pmWork.ExecuteCommand("exit");
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public string FindCheckout(PmrepCheckout parameters, bool verbose = false, bool printDBtype = false, bool allUsers = false)
            {
                
                Guard.ThrowIsNull(parameters);
                var otherParams = allUsers ? " -u " : "";
                otherParams += verbose ? " -b " : "";
                otherParams += printDBtype ? " -y " : "";
                var command = "findcheckout " + parameters.ObjectType
                                                  + parameters.FolderName
                                                  + parameters.ColumnSeparator
                                                  + parameters.EndOfRecordSeparator
                                                  + parameters.EndOfRecordSeparator
                                                  + parameters.EndOfListingIndicator
                                                  + parameters.DbdSeparator
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return result;
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
            public ConnectionDetails GetConnectionDetails(string connectionName, ConnectionType connectionType)
            {
                Guard.ThrowIsNull(connectionName);
                string command = "getconnectiondetails -n " + connectionName + " -t " + connectionType;

                var result = _pmWork.ExecuteCommand(command);
                return ConnectionDetailsAdapter.ConvertResultToConnectDetails(result);
            }
            public string GenerateAbapProgramToFile(PmrepGenerateAbapProgramm parameters, bool enableOverride = false, bool authorityCheck = false, bool useNamespace = false)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = enableOverride ? " -e " : "";
                otherParams += authorityCheck ? " -a " : "";
                otherParams += useNamespace ? " -n " : "";

                var command = "generateabapprogramtofile " + parameters.FolderName
                                                  + parameters.MappingName
                                                  + parameters.VersionNumber
                                                  + parameters.LogFileName
                                                  + parameters.UserName
                                                  + parameters.Password
                                                  + parameters.ConnectString
                                                  + parameters.Client
                                                  + parameters.Language
                                                  + parameters.OutputFileLocation
                                                  + parameters.ProgramMode
                                                  + parameters.OverrideName
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }
            public bool InstallAbapProgram(PmrepInstallAbapProgram parameters, bool enableOverride = false, bool authorityCheck = false, bool useNamespace = false)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = enableOverride ? " -e " : "";
                otherParams += authorityCheck ? " -a " : "";
                otherParams += useNamespace ? " -n " : "";

                var command = "installabapprogram " + parameters.FolderName
                                                  + parameters.MappingName
                                                  + parameters.VersionNumber
                                                  + parameters.LogFileName
                                                  + parameters.UserName
                                                  + parameters.Password
                                                  + parameters.ConnectString
                                                  + parameters.Client
                                                  + parameters.Language
                                                  + parameters.InputFileName
                                                  + parameters.ProgramMode
                                                  + parameters.OverrideName
                                                  + parameters.DevelopmentClassName
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }

            /// <summary>
            /// Terminates all users connections.
            /// </summary>
            public bool KillUserConnection()
            {
                string command = "killuserconnection -a";

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            /// <summary>
            /// Terminates user connections to the repository. You can terminate user connections based on the user name
            /// </summary>
            /// <param name="userName">user name</param>
            /// <returns>True or False</returns>
            public bool KillUserConnection(string userName)
            {
                Guard.ThrowIsNull(userName);
                string command = "killuserconnection -n " + userName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);

            }

            /// <summary>
            /// Terminates user connections to the repository. You can terminate user connections based on connection ID.
            /// </summary>
            /// <param name="connectionID">user connection id</param>
            /// <returns>True or False</returns>
            public bool KillUserConnection(int connectionID)
            {
                string command =  "killuserconnection -i " + connectionID;
                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }


            /// <summary>
            /// List all connection objects in the repository and their respective connection types.
            /// </summary>
            /// <returns>Connections Array</returns>
            public string[] ListConnections()
            {
                var result = _pmWork.ExecuteCommand("listconnections -t");
                return _pmWork.ConvertResultToArray(result);
            }
            public string ListObjectDependencies(PmrepObjectDependencies parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.Append ? " -a " : "";
                otherParams += parameters.Verbose ? " -b " : "";
                otherParams += parameters.PrintDBtype ? " -y " : "";
                otherParams += parameters.AcrossRepositories ? " -g " : "";
                otherParams += parameters.IncludeFkPkDependency ? " -s " : "";
                var command = "listobjectdependencies " + parameters.ObjectName
                                                  + parameters.ObjectType
                                                  + parameters.ObjectSubType
                                                  + parameters.FolderName
                                                  + parameters.VersionNumber
                                                  + parameters.PersistentInputFile
                                                  + parameters.DependencyObjectTypes
                                                  + parameters.DependencyDirection
                                                  + parameters.PersistentOutputFileName
                                                  + parameters.ColumnSeparator
                                                  + parameters.DbdSeparator
                                                  + parameters.EndOfRecordSeparator
                                                  + parameters.EndOfListingIndicator
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }

            /// <summary>
            /// Get repository folder List
            /// </summary>
            /// <returns></returns>
            public string[] ListFolders()
            {
                var result = _pmWork.ExecuteCommand("listobjects -o folder");
                return _pmWork.ConvertResultToArray(result);
            }
            private string _workflowSubstring = "workflow ";
            /// <summary>
            /// Get folder workflows list
            /// </summary>
            /// <param name="folderName">name repository folder</param>
            /// <returns>workflows</returns>
            public string[] ListWorkflows(string folderName)
            {
                Guard.ThrowIsNull(folderName);
                var result = _pmWork.ExecuteCommand("listobjects -o workflow -f "+ folderName);
                var arrResult = _pmWork.ConvertResultToArray(result);
                string[] arrWorkflows = new string[arrResult.Length];

                for(int wfItem = 0; wfItem<arrResult.Length;wfItem++)
                    if(arrResult[wfItem].Contains(_workflowSubstring))
                        arrWorkflows[wfItem] 
                            = arrResult[wfItem].Substring(arrResult[wfItem].IndexOf(_workflowSubstring) + _workflowSubstring.Length);

                return arrWorkflows;
            }
            /// <summary>
            /// Get list session in workflow
            /// </summary>
            /// <param name="folderName">name repository folder</param>
            /// <param name="workflow">name workflow in folder</param>
            /// <returns>sessions </returns>
            public string[] ListWorkflowSessions(string folderName, string workflow)
            {
                Guard.ThrowIsNull(folderName,workflow);
                var result = _pmWork.ExecuteCommand("listobjects -o session -f " + folderName);
                var arrResult = _pmWork.ConvertResultToArray(result);
                List<string> sessions = new List<string>();
                foreach(var session in arrResult)
                    if (session.Contains(workflow))
                        sessions.Add(session.Substring(session.IndexOf('.') + 1));

                return sessions.ToArray();
            }
            /// <summary>
            /// Returns a list of objects in the repository.
            /// </summary>
            /// <param name="objectType">type object</param>
            /// <returns>objects array</returns>
            public string[] ListObjects(IpcObjectsTypes objectType)
            {
                var result = _pmWork.ExecuteCommand("listobjects -o " + objectType);
                return _pmWork.ConvertResultToArray(result);
            }

            /// <summary>
            /// Returns a list of objects in the repository.
            /// </summary>
            /// <param name="objectType">type object</param>
            /// <param name="folderName">folder name</param>
            /// <returns>objects array</returns>
            public string[] ListObjects(IpcObjectsTypes objectType, string folderName)
            {
                Guard.ThrowIsNull(folderName);

                var result = _pmWork.ExecuteCommand("listobjects -o " + objectType+" -f "+folderName);

                return _pmWork.ConvertResultToArray(result);
            }

            /// <summary>
            ///  Returns a list of objects in the repository.
            /// </summary>
            /// <param name="parameters">options and arguments</param>
            /// <returns>objects array</returns>
            public string[] ListObjects(PmrepObject parameters)
            {
                
                Guard.ThrowIsNull(parameters);

                var result = _pmWork.ExecuteCommand("listobjects "+parameters.ColumnSeparator
                                                                                  +parameters.DbdSeparator
                                                                                  +parameters.EndOfListingIndicator+
                                                                                  parameters.EndOfRecordIndicator+
                                                                                  parameters.FolderName+
                                                                                  parameters.ObjectSubtype+
                                                                                  parameters.ObjectType+
                                                                                  parameters.PrintDatabaseType+
                                                                                  parameters.Verbose);
                return _pmWork.ConvertResultToArray(result);
            }
            public string ListTablesBySess(string folderName, string sessionName, SessObjType sessobj)
            {
                Guard.ThrowIsNull(folderName, sessionName);
                string command = "listtablesbysess -f " + folderName + " -s "+ sessionName + " -t " + sessobj;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }


            /// <summary>
            /// List all connection objects in the repository and their respective connection types.
            /// </summary>
            /// <returns>object ConnectedUser contains connection data</returns>
            public List<ConnectedUser> ListUserConnections()
            {
                var result = _pmWork.ExecuteCommand("listuserconnections");
                SetLastCommandResult(result);
                result = _pmWork.RemoveResultHeader(result);
                List<ConnectedUser> userConnections = new List<ConnectedUser>(6);
                var usersArray = result.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                //slow.. change..
                for (int i = 0; i < usersArray.Length; i += 9)
                {
                    var userIdUserName = usersArray[i].Split(':');
                    userConnections.Add(new ConnectedUser
                    {
                        ConnectionID = userIdUserName[0].Trim(),
                        UserName = userIdUserName[1].Trim(),
                        Application = usersArray[i + 1].Trim(),
                        Service = usersArray[i + 2].Trim(),
                        HostName = usersArray[i + 3].Trim(),
                        HostAdress = usersArray[i + 4].Trim(),
                        LoginTime = usersArray[i + 5].Trim(),
                        LastActivityTime = usersArray[i + 6].Trim(),
                        ProcessID = usersArray[i + 7].Trim(),
                        Status = usersArray[i + 8].Trim()
                    });
                }
                return userConnections;
            }
            public bool MassUpdate(PmrepMassUpdate parameters)
            {
                Guard.ThrowIsNull(parameters);
                var result = _pmWork.ExecuteCommand("massupdate " + parameters.SessionPropertyType
                                                                                  + parameters.SessionPropertyName
                                                                                  + parameters.SessionPropertyValue +
                                                                                  parameters.TransformationType +
                                                                                  parameters.FolderName +
                                                                                  parameters.PersistentInputFile +
                                                                                  parameters.ConditionOperator +
                                                                                  parameters.ConditionValue +
                                                                                  parameters.UpdateSessionInstance+
                                                                                  parameters.TestMode +
                                                                                  parameters.OutputLogFileName);
                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }
            public bool ModifyFolder(PmrepModifyFolder parameters)
            {
                Guard.ThrowIsNull(parameters);
                var result = _pmWork.ExecuteCommand("modifyFolder " + parameters.FolderDescription
                                                                                  + parameters.FolderName
                                                                                  + parameters.FolderStatus +
                                                                                  parameters.NewFolderName +
                                                                                  parameters.OsProfile +
                                                                                  parameters.OwnerName +
                                                                                  parameters.OwnerSecurityDomain +
                                                                                  parameters.Permissions +
                                                                                  parameters.SharedFolder);
                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }
            /// <summary>
            /// Sends notification messages to users connected to all repositories
            /// </summary>
            /// <param name="message">notify message</param>
            /// <returns>True or False</returns>
            public bool Notify(string message)
            {
                Guard.ThrowIsNull(message);
                string command = "notify -m \"" + message+"\"";

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }
            public bool ObjectImport(string importXml, string importControlFile, string logFileName = null, bool retainPersistentValue = true)
            {
                var otherParams = retainPersistentValue ? " -p " : "";
                otherParams += string.IsNullOrEmpty(logFileName) ? "" : " -l " + logFileName;

                string command = "objectimport " + "- i " + importXml +
                                                " -c " + importControlFile +
                                                otherParams;



                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }
            public bool ObjectImport(PmrepObjectImport parameters,bool retainPersistentValue=true)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = retainPersistentValue ? " -p " : "";

              _pmWork.CreateControlImportFile(sourceFolder: parameters.SourceFolder,
                                                        sourceRepo: parameters.SourceRepo,
                                                        targetFolder: parameters.TargetFolder,
                                                        targetRepo: parameters.TargetRepo,
                                                        dtdFile: parameters.ImportDtdFile,
                                                        encoding: parameters.ControlFileEncoding);
                string command = "objectimport " + parameters.ImportXml +
                                            " -c importXml.xml " +
                                            parameters.LogFile +
                                            otherParams;
                

                var result = _pmWork.ExecuteCommand(command);

                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }

            /// <summary>
            /// Exports objects to an XML file defined by the powrmart.dtd file
            /// </summary>
            /// <param name="parameters"></param>
            /// <param name="m">export pk-fk dependency</param>
            /// <param name="s">export objects referred by shortcut</param>
            /// <param name="b">export non-reusable dependents</param>
            /// <param name="r">export reusable dependents</param>
            /// <returns>True or False</returns>
            public bool ObjectExport(PmrepObjectExport parameters,bool m=false,bool s = false, bool b = false, bool r = false)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = m ? " -m " : "";
                    otherParams += s ? " -s " : "";
                    otherParams += b ? " -b " : "";
                    otherParams += r ? " -r " : "";
                var command = "objectexport " + parameters.FolderName
                                                  + parameters.LogFileName
                                                  + parameters.DbdSeparator
                                                  + parameters.PersistentInputFile
                                                  + parameters.VersionNumber
                                                  +parameters.XnlOutputFileName
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }
            public bool PurgeVersion(PmrepPurgeVersion parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.PrewPurgedObjOnly ? " -p " : "";
                otherParams += parameters.Verbose ? " -b " : "";
                otherParams += parameters.CheckDeplGroupReference ? " -c " : "";
                otherParams += parameters.LogObjNotPurged ? " -k " : "";

                var command = "purgeversion " + parameters.Version
                                                  + parameters.LastNVersionsToKeep
                                                  + parameters.TimeDate
                                                  + parameters.FolderName
                                                  + parameters.QueryName
                                                  + parameters.OutputFileName
                                                  + parameters.DbdSeparator
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }
            public bool Register(string localRepoName, string localRepoUser, string localRepoPassw)
            {
                Guard.ThrowIsNull(localRepoName, localRepoUser, localRepoPassw);

                string command = "register -r " + localRepoName + " -n " + localRepoUser + " -x " + localRepoPassw;

                var result = _pmWork.ExecuteCommand(command);

                SetLastCommandResult(result);

                return _pmWork.CheckErrorInResult(result);
            }
            public bool Register(PmrepRepoObject parameters)
            {
                Guard.ThrowIsNull(parameters);

                var command = "register " + parameters.LocalRepoName
                                                  + parameters.LocalRepoUser
                                                  + parameters.LocalRepoUserSecurityDomain
                                                  + parameters.LocalRepoPassword
                                                  + parameters.LocalRepoPasswordEnvVar
                                                  + parameters.LocalRepoDomainName
                                                  + parameters.LocalRepoPortalHostName
                                                  + parameters.LocalRepoPortalPort;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool RegisterPlugin(string registrationFile)
            {
                Guard.ThrowIsNull(registrationFile);
                string command = "registerplugin -i "+ registrationFile;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool RegisterPlugin(PmrepRegisterPlugin parameters,bool updatePlugin=false,bool checkSecurLib = false,bool isNativePlugin=false)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = updatePlugin ? " -e " : "";
                otherParams += checkSecurLib ? " -k " : "";
                otherParams += isNativePlugin ? " -N " : "";
                var command = "registerplugin " + parameters.InputRegistrationFile
                                                  + parameters.NISLogin
                                                  + parameters.NISPassword
                                                  + parameters.NISPasswordEnviVar
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool Restore(PmrepRestore parameters)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = parameters.CreateGlobalRepository ? " -g " : "";
                otherParams += parameters.EnableObjVersioning ? " -y " : "";
                otherParams += parameters.SkipLogs ? " -b " : "";
                otherParams += parameters.SkipDeployHistory ? " -j " : "";
                otherParams += parameters.SkipMxData ? " -q " : "";
                otherParams += parameters.SkipTaskStatistic ? " -t " : "";
                otherParams += parameters.AsNewRepository ? " -a " : "";
                otherParams += parameters.ExitIfDomainDiffCurr ? " -e " : "";

                var command = "restore " + parameters.DomainUserName
                                                  + parameters.DomainUserSecurity
                                                  + parameters.DomainPassword
                                                  + parameters.DomainPasswordEnviVar
                                                  + parameters.InputFileName
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool RollbackDeployment(string deployGroupName, string latestDeployRun)
            {
                Guard.ThrowIsNull(deployGroupName, latestDeployRun);
                string command = "rollbackdeployment -p " + deployGroupName + " -t " + latestDeployRun;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool RollbackDeployment(string deployGroupName, string latestDeployRun,string repoName, string latestVersionDeployGroup)
            {
                Guard.ThrowIsNull(deployGroupName, latestDeployRun, repoName, latestVersionDeployGroup);
                var otherParams = !string.IsNullOrEmpty(repoName) ? " -r " : "";
                otherParams += !string.IsNullOrEmpty(latestVersionDeployGroup) ? " -v " : "";

                string command = "rollbackdeployment -p " + deployGroupName + " -t " + latestDeployRun + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool Run(string scriptFileName)
            {
                Guard.ThrowIsNull(scriptFileName);
                string command = "run -f " + scriptFileName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool Run(string scriptFileName,string outputFileName,bool echoCommand=false,bool stopAtFirstErr=false,bool encodeUTF8=false)
            {
                Guard.ThrowIsNull(scriptFileName, outputFileName);
                var otherParams = echoCommand ? " -e " : "";
                otherParams += stopAtFirstErr ? " -s " : "";
                otherParams += encodeUTF8 ? " -u " : "";
                string command = "run -f " + scriptFileName + " -o " + outputFileName+ otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool SwitchConnection(string oldConnectionName, string newConnectionName)
            {
                Guard.ThrowIsNull(oldConnectionName, newConnectionName);
                string command = "switchconnection -o " + oldConnectionName + " -n " + newConnectionName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool TruncateLog(string logsTruncated)
            {
                Guard.ThrowIsNull(logsTruncated);
                string command = "truncatelog -t " + logsTruncated;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool TruncateLog(string logsTruncated,string folderName, string workflowName)
            {
                Guard.ThrowIsNull(logsTruncated, folderName, workflowName);

                string command = "truncatelog -t " + logsTruncated
                                                   +" -f "+folderName
                                                   +" -w "+workflowName;
                                                                        

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UndoCheckout(PmrepUndoCheckout parameters)
            {
                Guard.ThrowIsNull(parameters);

                var command = "undocheckout " + parameters.DbdSeparator
                                                   + parameters.FolderName
                                                   + parameters.ObjectName
                                                   + parameters.ObjectSubType
                                                   + parameters.ObjectType;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool Unregister(PmrepRepoObject parameters)
            {
                Guard.ThrowIsNull(parameters);

                var command = "unregister " + parameters.LocalRepoName
                                                  + parameters.LocalRepoUser
                                                  + parameters.LocalRepoUserSecurityDomain
                                                  + parameters.LocalRepoPassword
                                                  + parameters.LocalRepoPasswordEnvVar
                                                  + parameters.LocalRepoDomainName
                                                  + parameters.LocalRepoPortalHostName
                                                  + parameters.LocalRepoPortalPort;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UnregisterPlugin(string vendorId, string pluginId)
            {
                Guard.ThrowIsNull(vendorId, pluginId);

                string command = "unregisterplugin -v " + vendorId + " -l " + vendorId;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UnregisterPlugin(PmrepUregisterPlugin parameters,bool isSecurityModule=false,bool removeUserNameLogin=false)
            {
                Guard.ThrowIsNull(parameters);

                var otherParams = isSecurityModule ? " -s " : "";
                otherParams += removeUserNameLogin ? " -g " : "";
                var command = "unregisterplugin " + parameters.VendorId
                                                  + parameters.PluginId
                                                  + parameters.NewPassword
                                                  + parameters.NewPasswordEnvVariable
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UpdateConnection(PmrepUpdateConnection parameters)
            {
                Guard.ThrowIsNull(parameters);

                var command = "updateconnection " + parameters.ConnectionSubtype
                                                   + parameters.ConnectionName
                                                   + parameters.NewUserName
                                                   + parameters.NewPassword
                                                   + parameters.NewPasswordEnvVariable
                                                   + parameters.NewConnectionString
                                                   + parameters.AttributeName
                                                   + parameters.NewAttributeValue
                                                   + parameters.ConnectionType
                                                   + parameters.CodePage;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UpdateEmailAddr(string folderName, string sessionName, string succesEmailAddres, string failureEmailAddress)
            {
                Guard.ThrowIsNull(folderName, sessionName, succesEmailAddres, failureEmailAddress);

                string command = "updateemailaddr -d " + folderName + " -s " + sessionName + " -u " + succesEmailAddres + " -f " + failureEmailAddress;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UpdateSeqGenVals(string folderName, string sequenceName)
            {
                Guard.ThrowIsNull(folderName, sequenceName);

                string command = "updateseqgenvals -f " + folderName + " -t " + sequenceName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UpdateSeqGenVals(PmrepSequence parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "updateseqgenvals " + parameters.FolderName
                                                   + parameters.MappingName
                                                   + parameters.SequenceGeneratorName
                                                   + parameters.StartValue
                                                   + parameters.EndValue
                                                   + parameters.IncrementBy
                                                   + parameters.CurrentValue;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UpdateSrcPrefix(string folderName,string prefixName,string sessionName)
            {
                Guard.ThrowIsNull(folderName, prefixName, sessionName);
                string command = "updatesrcprefix -f " + folderName + " -p " + prefixName + " -s " + sessionName;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public bool UpdateSrcPrefix(PmrepSrcTargetPrefix parameters, bool useTargetInstanceName = false)
            {
                Guard.ThrowIsNull(parameters);
                var otherParams = useTargetInstanceName ? " -n " : "";
                var command = "updatetargprefix " + parameters.FolderName
                                                  + parameters.SessionName
                                                  + parameters.SrcTrgName
                                                  + parameters.PrefixName
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            /// <summary>
            /// Updates statistics for repository tables and indexes.
            /// </summary>
            /// <returns>True or False</returns>
            public bool UpdateStatistics()
            {
                string command = "updatestatistics";

                var result = _pmWork.ExecuteCommand(command);
                SetLastCommandResult(result);
                return _pmWork.CheckErrorInResult(result);
            }
            public string UpdateTargPrefix(string folderName,string prefixName,string sessionName)
            {
                Guard.ThrowIsNull(folderName, prefixName, sessionName);
                string command = "updatetargprefix -f " + folderName+" -p "+prefixName+" -s "+sessionName;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }
            public string UpdateTargPrefix(PmrepSrcTargetPrefix parameters,bool useTargetInstanceName=false)
            {
                Guard.ThrowIsNull(parameters);
                var otherParams = useTargetInstanceName ? " -n " : "";
                var command = "updatetargprefix " + parameters.FolderName
                                                  + parameters.SessionName
                                                  + parameters.SrcTrgName
                                                  + parameters.PrefixName
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }
            /// <summary>
            /// Upgrades repository to the latest version
            /// </summary>
            /// <param name="repositoryPassword">administrator password</param>
            /// <returns>upgrades logs</returns>
            public string Upgrade(string repositoryPassword)
            {
                Guard.ThrowIsNull(repositoryPassword);
                string command = "upgrade -x " + repositoryPassword;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }
            public string UninstallAbapProgram(PmrepUnistallAbapProgram parameters)
            {
                Guard.ThrowIsNull(parameters);
                var command = "uninstallabapprogram " + parameters.FolderName
                                                  + parameters.MappingName
                                                  + parameters.VersionNumber
                                                  + parameters.LogFilename
                                                  + parameters.UserName
                                                  + parameters.Password
                                                  + parameters.ConnectString
                                                  + parameters.Client
                                                  + parameters.Language
                                                  + parameters.ProgramMode;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }
            public string Validate(PmrepValidate parameters, bool saveUponValid = false, bool checkUponValid = false, bool checkInComment = false, bool append = false, bool verbose = false, bool printDBType = false)
            {
                
                Guard.ThrowIsNull(parameters);
                var otherParams = saveUponValid ? " -s " : "";
                otherParams += checkUponValid ? " -k " : "";
                otherParams += checkInComment ? " -m " : "";
                otherParams += append ? " -a " : "";
                otherParams += verbose ? " -b " : "";
                otherParams += printDBType ? " -y " : "";
                var command = "validate " + parameters.ObjectName
                                                  + parameters.ObjectType
                                                  + parameters.VersionNumber
                                                  + parameters.FolderName
                                                  + parameters.OutputOptionTypes
                                                  + parameters.PersistentInputFile
                                                  + parameters.PersistentOutputFileName
                                                  + parameters.EndOfRecordSeparator
                                                  + parameters.EndOfListingIndicator
                                                  + otherParams;

                var result = _pmWork.ExecuteCommand(command);
                return result;
            }

            /// <summary>
            /// Clear all resources and closing pmrep
            /// </summary>
            public void Dispose()
            {
                _pmWork.Dispose();
            }
        }
    }
}
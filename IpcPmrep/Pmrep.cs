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
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                if (logFile != null)
                    LogWriter.SetLogFile(logFile);
                


                var command = "connect " + parameters.Domain + parameters.HostName + parameters.Password + parameters.Port + parameters.Repository + parameters.UserName + parameters.Timeout;
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
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var command = "addtodeploymentgroup " + parameters.DbdSeparator 
                                                        + parameters.DependencyTypes 
                                                        + parameters.DeploymentGroupName 
                                                        + parameters.FolderName 
                                                        + parameters.ObjectName 
                                                        + parameters.ObjectSubType 
                                                        + parameters.ObjectType 
                                                        + parameters.PersistentImputFile 
                                                        + parameters.VersionNumber;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);

            }
            public bool ApplyLabel(PmrepApplyLabel parameters,bool acrossRepositories=false, bool moveLabel=false, bool comments=false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool AssignPermission(PmrepPermissions parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var command = "AssignPermission " + parameters.ObjectName
                                                  + parameters.ObjectType 
                                                  + parameters.ObjectSubType
                                                  + parameters.UserName
                                                  + parameters.GroupName
                                                  + parameters.Permission
                                                  + parameters.SecurityDomain;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool BackUp(string outputFileName)
            {
                var command = "backup -o " + outputFileName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool ChangeOwner(PmrepChangeOwner parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var command = "ChangeOwner " + parameters.ObjectName
                                                   + parameters.ObjectType
                                                   + parameters.ObjectSubType
                                                   + parameters.NewOwnerName
                                                   + parameters.SecurityDomain;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public string CheckIn(PmrepCheckIn parameters)
            {
                var command = "checkin " + parameters.ObjectName
                                                    + parameters.ObjectType
                                                    + parameters.ObjectSubType
                                                    + parameters.FolderName
                                                    + parameters.DbdSeparator
                                                    + parameters.Comments;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return result.output;
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
            public string Create(PmrepCreate parameters,bool createGlobalRepo=false,bool enableVersioning=false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var otherParams = createGlobalRepo ? " -g " : "";
                otherParams += enableVersioning ? " -v " : "";

                var command = "create " + parameters.DomainUserName
                                                    + parameters.DomainPassword
                                                    + parameters.DomainUserSecurity
                                                    + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return result.output;
            }
            public string CreateConnection(string connectionType, string connectionName,string codePage)
            {
                var command = "createconnection -s " +connectionType 
                                                    + " -n "+connectionName
                                                    + " -l "+codePage;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return result.output;
            }
            public string CreateConnection(string connectionType, string connectionName, string codePage, string connectionString)
            {
                var command = "createconnection -s " + connectionType
                                                    + " -n " + connectionName
                                                    + " -l " + codePage
                                                    + " -c " + connectionString;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return result.output;
            }
            public string CreateConnection(PmrepCreateConnection parameters, bool t=false,bool x=false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.RemoveResultHeader(result.output);
            }
            public bool CreateDeploymentGroup(string deploymentGroupName)
            {
                var command = "createdeploymentgroup -p " + deploymentGroupName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool CreateDeploymentGroup(PmrepNewDeploymentGroup parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var command = "createdeploymentgroup " + parameters.DeploymentGroupName
                                                   + parameters.DeploymentGroupType
                                                   + parameters.QueryName
                                                   + parameters.QueryType
                                                   + parameters.Comments;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool CreateFolder(string folderName)
            {
                string command = "createfolder -n " + folderName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool CreateFolder(string folderName,string permissions)
            {
                string command = "createfolder -n " + folderName+ " -p " + permissions;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool CreateFolder(PmrepNewFolder parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                string command = "createfolder " + parameters.FolderName +
                                                parameters.FolderDescription +
                                                parameters.FolderStatus +
                                                parameters.OwnerName +
                                                parameters.OwnerSecurityDomain +
                                                parameters.Permissions +
                                                parameters.SharedFolder;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool CreateLabel(string labelName, string comments = null)
            {
                string command = "createlabel -a " + labelName + " -c " + comments;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool Delete(string repositoryPassword)
            {
                string command = "delete -f  -x " + repositoryPassword;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }


            /// <summary>
            /// Delete connection from the repository.
            /// </summary>
            /// <param name="connectionName">Name of the connection to delete</param>
            /// <returns>True or False</returns>
            public bool DeleteConnection(string connectionName)
            {
                
                string  command = "deleteconnection -f -n " + connectionName;
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            /// <summary>
            /// Delete connection from the repository.
            /// </summary>
            /// <param name="connectionName">Name of the connection to delete</param>
            /// <param name="connectionType">Type of connection</param>
            /// <returns>True or False</returns>
            public bool DeleteConnection(string connectionName, ConnectionType connectionType )
            {

                string command = "deleteconnection -f -n " + connectionName + " -s " + connectionType.Value;


                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool DeleteDeploymentGroup(string groupName)
            {
                string command = "deletedeploymentgroup -f -p " + groupName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            /// <summary>
            /// Delete a folder from the repository.
            /// </summary>
            /// <param name="folderName">Name of the folder</param>
            /// <returns>True or False</returns>
            public bool DeleteFolder(string folderName)
            {
                string command = "deletefolder -n " + folderName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            /// <summary>
            /// Delete a label from the repository.
            /// </summary>
            /// <param name="labelName">Name of the label</param>
            /// <returns>True or False<</returns>
            public bool DeleteLabel(string labelName)
            {
                string command = "deletelabel -f -a " + labelName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
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
                string command = "deleteobject -f " + folderName + " -o " + objectName + " -n " + objectType;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public string DeployDeploymentGroup(string deploymentGroupName,string controlFileName,string targetRepositoryName)
            {
                string command = "deploydeploymentgroup -p " + deploymentGroupName + " -c " + controlFileName + " -r " + targetRepositoryName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string DeployDeploymentGroup(PmrepRunDeploymentGroup parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string DeployFolder(string folderName, string controlFileName, string targetRepositoryName)
            {
                string command = "deployfolder -f " + folderName + " -c " + controlFileName + " -r " + targetRepositoryName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string DeployFolder(PmrepDeployFolder parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string ExecuteQuery(string queryName)
            {
                string command = "executequery -q " + queryName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string ExecuteQuery(PmrepQuery parameters, bool append=false,bool verbose = false, bool printDBtype = false, bool dontIncludeParentPath = false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            /// <summary>
            /// Exits from the pmrep interactive mode.
            /// </summary>
            public void Exit()
            {
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "exit");
                PmrepWorker.CheckErrorInResult(result);
            }
            public string FindCheckout(PmrepCheckout parameters, bool verbose = false, bool printDBtype = false, bool allUsers = false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
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
            public string GetConnectionDetails(string connectionName, ConnectionType connectionType)
            {
                string command = "getconnectiondetails -n " + connectionName + " -t " + connectionType.Value;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.RemoveResultHeader(result.output);
            }
            public string GenerateAbapProgramToFile(PmrepGenerateAbapProgramm parameters, bool enableOverride = false, bool authorityCheck = false, bool useNamespace = false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string InstallAbapProgram(PmrepInstallAbapProgram parameters, bool enableOverride = false, bool authorityCheck = false, bool useNamespace = false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
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
            public string ListObjectDependencies(PmrepObjectDependencies parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string[] ListObjects(PmrepObject parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "listobjects "+parameters.ColumnSeparator
                                                                                  +parameters.DbdSeparator
                                                                                  +parameters.EndOfListingIndicator+
                                                                                  parameters.EndOfRecordIndicator+
                                                                                  parameters.FolderName+
                                                                                  parameters.ObjectSubtype+
                                                                                  parameters.ObjectType+
                                                                                  parameters.PrintDatabaseType+
                                                                                  parameters.Verbose);
                LogWriter.Write(result.errors);
                return PmrepWorker.FormattingResult(result.output);
            }
            public string ListTablesBySess(string folderName, string sessionName, string objectType)
            {
                string command = "listtablesbysess -f " + folderName + " -s "+ sessionName + " -t " + objectType;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
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
            public string MassUpdate(PmrepMassUpdate parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "massupdate " + parameters.SessionPropertyType
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
                return result.output;
            }
            public string ModifyFolder(PmrepModifyFolder parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "modifyFolder " + parameters.FolderDescription
                                                                                  + parameters.FolderName
                                                                                  + parameters.FolderStatus +
                                                                                  parameters.NewFolderName +
                                                                                  parameters.OsProfile +
                                                                                  parameters.OwnerName +
                                                                                  parameters.OwnerSecurityDomain +
                                                                                  parameters.Permissions +
                                                                                  parameters.SharedFolder);
                return result.output;
            }
            public bool Notify(string message)
            {
                string command = "notify -m " + message;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public string ObjectImport(PmrepObjectImport parameters,bool retainPersistentValue=true)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var otherParams = retainPersistentValue ? " -p " : "";

              PmrepWorker.CreateControlImportFile(sourceFolder: parameters.SourceFolder,
                                                        sourceRepo: parameters.SourceRepo,
                                                        targetFolder: parameters.TargetFolder,
                                                        targetRepo: parameters.TargetRepo,
                                                        dtdFile: parameters.ImportDtdFile,
                                                        encoding: parameters.ControlFileEncoding);
                string command = "objectimport " + parameters.ImportXml +
                                            " -c importXml.xml " +
                                            parameters.LogFile +
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
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                Console.WriteLine(result.output);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public string PurgeVersion(PmrepPurgeVersion parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Register(string localRepoName, string localRepoUser, string localRepoPassw)
            {
                string command = "register -r " + localRepoName + " -n " + localRepoUser + " -x " + localRepoPassw;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Register(PmrepRepoObject parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var command = "register " + parameters.LocalRepoName
                                                  + parameters.LocalRepoUser
                                                  + parameters.LocalRepoUserSecurityDomain
                                                  + parameters.LocalRepoPassword
                                                  + parameters.LocalRepoPasswordEnvVar
                                                  + parameters.LocalRepoDomainName
                                                  + parameters.LocalRepoPortalHostName
                                                  + parameters.LocalRepoPortalPort;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string RegisterPlugin(string registrationFile)
            {
                string command = "registerplugin -i ";

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string RegisterPlugin(PmrepRegisterPlugin parameters,bool updatePlugin=false,bool checkSecurLib = false,bool isNativePlugin=false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var otherParams = updatePlugin ? " -e " : "";
                otherParams += checkSecurLib ? " -k " : "";
                otherParams += isNativePlugin ? " -N " : "";
                var command = "registerplugin " + parameters.InputRegistrationFile
                                                  + parameters.NISLogin
                                                  + parameters.NISPassword
                                                  + parameters.NISPasswordEnviVar
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Restore(PmrepRestore parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string RollbackDeployment(string deployGroupName, string latestDeployRun)
            {
                string command = "rollbackdeployment -p " + deployGroupName + " -t " + latestDeployRun;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string RollbackDeployment(string deployGroupName, string latestDeployRun,string repoName, string latestVersionDeployGroup)
            {
                var otherParams = !string.IsNullOrEmpty(repoName) ? " -r " : "";
                otherParams += !string.IsNullOrEmpty(latestVersionDeployGroup) ? " -v " : "";

                string command = "rollbackdeployment -p " + deployGroupName + " -t " + latestDeployRun + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Run(string scriptFileName)
            {
                string command = "run -f " + scriptFileName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Run(string scriptFileName,string outputFileName,bool echoCommand=false,bool stopAtFirstErr=false,bool encodeUTF8=false)
            {
                var otherParams = echoCommand ? " -e " : "";
                otherParams += stopAtFirstErr ? " -s " : "";
                otherParams += encodeUTF8 ? " -u " : "";
                string command = "run -f " + scriptFileName + " -o " + outputFileName+ otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public bool SwitchConnection(string oldConnectionName, string newConnectionName)
            {
                string command = "switchconnection -o " + oldConnectionName + " -n " + newConnectionName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool TruncateLog(string logsTruncated)
            {
                string command = "truncatelog -t " + logsTruncated;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public bool TruncateLog(string logsTruncated,string folderName, string workflowName)
            {
                string command = "truncatelog -t " + logsTruncated
                                                   +" -f "+folderName
                                                   +" -w "+workflowName;
                                                                        

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public string UndoCheckout(PmrepUndoCheckout parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var command = "undocheckout " + parameters.DbdSeparator
                                                   + parameters.FolderName
                                                   + parameters.ObjectName
                                                   + parameters.ObjectSubType
                                                   + parameters.ObjectType;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Unregister(PmrepRepoObject parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var command = "unregister " + parameters.LocalRepoName
                                                  + parameters.LocalRepoUser
                                                  + parameters.LocalRepoUserSecurityDomain
                                                  + parameters.LocalRepoPassword
                                                  + parameters.LocalRepoPasswordEnvVar
                                                  + parameters.LocalRepoDomainName
                                                  + parameters.LocalRepoPortalHostName
                                                  + parameters.LocalRepoPortalPort;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UnregisterPlugin(string vendorId, string pluginId)
            {
                string command = "unregisterplugin -v " + vendorId + " -l " + vendorId;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UnregisterPlugin(PmrepUregisterPlugin parameters,bool isSecurityModule=false,bool removeUserNameLogin=false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var otherParams = isSecurityModule ? " -s " : "";
                otherParams += removeUserNameLogin ? " -g " : "";
                var command = "unregisterplugin " + parameters.VendorId
                                                  + parameters.PluginId
                                                  + parameters.NewPassword
                                                  + parameters.NewPasswordEnvVariable
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateConnection(PmrepUpdateConnection parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateEmailAddr(string folderName, string sessionName, string succesEmailAddres, string failureEmailAddress)
            {
                string command = "updateemailaddr -d " + folderName + " -s " + sessionName + " -u " + succesEmailAddres + " -f " + failureEmailAddress;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateSeqGenVals(string folderName, string sequenceName)
            {
                string command = "updateseqgenvals -f " + folderName + " -t " + sequenceName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateSeqGenVals(PmrepSequence parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var command = "updateseqgenvals " + parameters.FolderName
                                                   + parameters.MappingName
                                                   + parameters.SequenceGeneratorName
                                                   + parameters.StartValue
                                                   + parameters.EndValue
                                                   + parameters.IncrementBy
                                                   + parameters.CurrentValue;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateSrcPrefix(string folderName,string prefixName,string sessionName)
            {
                string command = "updatesrcprefix -f " + folderName + " -p " + prefixName + " -s " + sessionName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateSrcPrefix(PmrepSrcTargetPrefix parameters, bool useTargetInstanceName = false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var otherParams = useTargetInstanceName ? " -n " : "";
                var command = "updatetargprefix " + parameters.FolderName
                                                  + parameters.SessionName
                                                  + parameters.SrcTrgName
                                                  + parameters.PrefixName
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public bool UpdateStatistics()
            {
                string command = "updatestatistics";

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return PmrepWorker.CheckErrorInResult(result);
            }
            public string UpdateTargPrefix(string folderName,string prefixName,string sessionName)
            {
                string command = "updatetargprefix -f " + folderName+" -p "+prefixName+" -s "+sessionName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateTargPrefix(PmrepSrcTargetPrefix parameters,bool useTargetInstanceName=false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var otherParams = useTargetInstanceName ? " -n " : "";
                var command = "updatetargprefix " + parameters.FolderName
                                                  + parameters.SessionName
                                                  + parameters.SrcTrgName
                                                  + parameters.PrefixName
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Upgrade(string repositoryPassword)
            {

                string command = "upgrade -x " + repositoryPassword;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UninstallAbapProgram(PmrepUnistallAbapProgram parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Validate(PmrepValidate parameters, bool saveUponValid = false, bool checkUponValid = false, bool checkInComment = false, bool append = false, bool verbose = false, bool printDBType = false)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
        }
    }
}
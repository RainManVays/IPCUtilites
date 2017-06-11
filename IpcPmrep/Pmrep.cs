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
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");

                var command = "addtodeploymentgroup " + parameters.dbdSeparator 
                                                        + parameters.dependencyTypes 
                                                        + parameters.deploymentGroupName 
                                                        + parameters.folderName 
                                                        + parameters.objectName 
                                                        + parameters.objectSubType 
                                                        + parameters.objectType 
                                                        + parameters.persistentImputFile 
                                                        + parameters.versionNumber;

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
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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
                var command = "ChangeOwner " + parameters.objectName
                                                   + parameters.objectType
                                                   + parameters.objectSubType
                                                   + parameters.newOwnerName
                                                   + parameters.securityDomain;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);

                return PmrepWorker.CheckErrorInResult(result);
            }
            public string CheckIn(PmrepCheckIn parameters)
            {
                var command = "checkin " + parameters.objectName
                                                    + parameters.objectType
                                                    + parameters.objectSubType
                                                    + parameters.folderName
                                                    + parameters.dbdSeparator
                                                    + parameters.comments;

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

                var command = "create " + parameters.domainUserName
                                                    + parameters.domainPassword
                                                    + parameters.domainUserSecurity
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

                var command = "createconnection " + parameters.codePage
                                                    + parameters.conectionEnvironmentSQL
                                                    + parameters.connectionAttributes
                                                    + parameters.connectionName
                                                    +" -s "+ parameters.connectionType.Value
                                                    + parameters.connectString
                                                    + parameters.databaseName
                                                    + parameters.dataSourceName
                                                    + parameters.domainName
                                                    + parameters.packetSize
                                                    + parameters.password
                                                    + parameters.rollbackSegment
                                                    + parameters.serverName
                                                    + parameters.transactionEnvironmentSQL
                                                    + parameters.userName
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
                var command = "createdeploymentgroup " + parameters.deploymentGroupName
                                                   + parameters.deploymentGroupType
                                                   + parameters.queryName
                                                   + parameters.queryType
                                                   + parameters.comments;

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
                string command = "deploydeploymentgroup " + parameters.deploymentGroupName +
                                                parameters.controlFileName +
                                                parameters.targetRepositoryName +
                                                parameters.targetRepositoryUser +
                                                parameters.targetRepositoryUserSecurityDomain +
                                                parameters.targetRepositoryPassword +
                                                parameters.targetRepositoryPasswordEnvVar +
                                                parameters.targetDomainName +
                                                parameters.targetPortalHostName +
                                                parameters.targetPortalPortNumber +
                                                parameters.logFileName;

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
                string command = "deployfolder " + parameters.folderName +
                                                parameters.controlFileName +
                                                parameters.targetRepositoryName +
                                                parameters.targetRepositoryUser +
                                                parameters.targetRepositoryUserSecurityDomain +
                                                parameters.targetRepositoryPassword +
                                                parameters.targetRepositoryPasswordEnvVar +
                                                parameters.targetDomainName +
                                                parameters.targetPortalHostName +
                                                parameters.targetPortalPortNumber +
                                                parameters.logFileName;

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
                var command = "executequery " + parameters.queryName
                                                  + parameters.queryType
                                                  + parameters.outputPersistentFileName
                                                  + parameters.columnSeparator
                                                  + parameters.endOfRecordSeparator
                                                  + parameters.endOfListingIndicator
                                                  + parameters.dbdSeparator
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
                var command = "findcheckout " + parameters.objectType
                                                  + parameters.folderName
                                                  + parameters.columnSeparator
                                                  + parameters.endOfRecordSeparator
                                                  + parameters.endOfRecordSeparator
                                                  + parameters.endOfListingIndicator
                                                  + parameters.dbdSeparator
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

                var command = "generateabapprogramtofile " + parameters.folderName
                                                  + parameters.mappingName
                                                  + parameters.versionNumber
                                                  + parameters.logFilename
                                                  + parameters.userName
                                                  + parameters.password
                                                  + parameters.connectString
                                                  + parameters.client
                                                  + parameters.language
                                                  + parameters.outputFileLocation
                                                  + parameters.programMode
                                                  + parameters.overrideName
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

                var command = "installabapprogram " + parameters.folderName
                                                  + parameters.mappingName
                                                  + parameters.versionNumber
                                                  + parameters.logFilename
                                                  + parameters.userName
                                                  + parameters.password
                                                  + parameters.connectString
                                                  + parameters.client
                                                  + parameters.language
                                                  + parameters.inputFileName
                                                  + parameters.programMode
                                                  + parameters.overrideName
                                                  + parameters.developmentClassName
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
                var otherParams = parameters.append ? " -a " : "";
                otherParams += parameters.verbose ? " -b " : "";
                otherParams += parameters.printDBtype ? " -y " : "";
                otherParams += parameters.acrossRepositories ? " -g " : "";
                otherParams += parameters.includeFkPkDependency ? " -s " : "";
                var command = "listobjectdependencies " + parameters.objectName
                                                  + parameters.objectType
                                                  + parameters.objectSubType
                                                  + parameters.folderName
                                                  + parameters.versionNumber
                                                  + parameters.persistentInputFile
                                                  + parameters.dependencyObjectTypes
                                                  + parameters.dependencyDirection
                                                  + parameters.persistentOutputFileName
                                                  + parameters.columnSeparator
                                                  + parameters.dbdSeparator
                                                  + parameters.endOfRecordSeparator
                                                  + parameters.endOfListingIndicator
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string[] ListObjects(PmrepObject parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
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
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "massupdate " + parameters.sessionPropertyType
                                                                                  + parameters.sessionPropertyName
                                                                                  + parameters.sessionPropertyValue +
                                                                                  parameters.transformationType +
                                                                                  parameters.folderName +
                                                                                  parameters.persistentInputFile +
                                                                                  parameters.conditionOperator +
                                                                                  parameters.conditionValue +
                                                                                  parameters.updateSessionInstanceFlag+
                                                                                  parameters.testMode +
                                                                                  parameters.outputLogFileName);
                return result.output;
            }
            public string ModifyFolder(PmrepModifyFolder parameters)
            {
                if (parameters == null)
                    throw new ArgumentNullException("parameters", "parameters is null");
                var result = PmrepWorker.ExecuteCommand(_pmrepFile, "modifyFolder " + parameters.folderDescription
                                                                                  + parameters.folderName
                                                                                  + parameters.folderStatus +
                                                                                  parameters.newFolderName +
                                                                                  parameters.osProfile +
                                                                                  parameters.ownerName +
                                                                                  parameters.ownerSecurityDomain +
                                                                                  parameters.permissions +
                                                                                  parameters.sharedFolder);
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
              var otherParams = retainPersistentValue ? " -p " : "";

              PmrepWorker.CreateControlImportFile(sourceFolder: parameters.sourceFolder,
                                                        sourceRepo: parameters.sourceRepo,
                                                        targetFolder: parameters.targetFolder,
                                                        targetRepo: parameters.targetRepo,
                                                        dtdFile: parameters.importDtdFile,
                                                        encoding: parameters.controlFileEncoding);
                string command = "objectimport " + parameters.importXml +
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
            public string PurgeVersion(PmrepPurgeVersion parameters)
            {
                var otherParams = parameters.prewPurgedObjOnly ? " -p " : "";
                otherParams += parameters.verbose ? " -b " : "";
                otherParams += parameters.checkDeplGroupReference ? " -c " : "";
                otherParams += parameters.logObjNotPurged ? " -k " : "";

                var command = "purgeversion " + parameters.version
                                                  + parameters.lastNVersionsToKeep
                                                  + parameters.timeDate
                                                  + parameters.folderName
                                                  + parameters.queryName
                                                  + parameters.outputFileName
                                                  + parameters.dbdSeparator
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
                var command = "register " + parameters.localRepoName
                                                  + parameters.localRepoUser
                                                  + parameters.localRepoUserSecurityDomain
                                                  + parameters.localRepoPassword
                                                  + parameters.localRepoPasswordEnvVar
                                                  + parameters.localRepoDomainName
                                                  + parameters.localRepoPortalHostName
                                                  + parameters.localRepoPortalPort;

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
                var otherParams = updatePlugin ? " -e " : "";
                otherParams += checkSecurLib ? " -k " : "";
                otherParams += isNativePlugin ? " -N " : "";
                var command = "registerplugin " + parameters.inputRegistrationFile
                                                  + parameters.NISLogin
                                                  + parameters.NISPassword
                                                  + parameters.NISPasswordEnviVar
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Restore(PmrepRestore parameters)
            {
                var otherParams = parameters.createGlobalRepository ? " -g " : "";
                otherParams += parameters.enableObjVersioning ? " -y " : "";
                otherParams += parameters.skipLogs ? " -b " : "";
                otherParams += parameters.skipDeployHistory ? " -j " : "";
                otherParams += parameters.skipMxData ? " -q " : "";
                otherParams += parameters.skipTaskStatistic ? " -t " : "";
                otherParams += parameters.asNewRepository ? " -a " : "";
                otherParams += parameters.exitIfDomainDiffCurr ? " -e " : "";

                var command = "restore " + parameters.domainUserName
                                                  + parameters.domainUserSecurity
                                                  + parameters.domainPassword
                                                  + parameters.domainPasswordEnviVar
                                                  + parameters.inputFileName
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
                var command = "undocheckout " + parameters.dbdSeparator
                                                   + parameters.folderName
                                                   + parameters.objectName
                                                   + parameters.objectSubType
                                                   + parameters.objectType;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Unregister(PmrepRepoObject parameters)
            {
                var command = "unregister " + parameters.localRepoName
                                                  + parameters.localRepoUser
                                                  + parameters.localRepoUserSecurityDomain
                                                  + parameters.localRepoPassword
                                                  + parameters.localRepoPasswordEnvVar
                                                  + parameters.localRepoDomainName
                                                  + parameters.localRepoPortalHostName
                                                  + parameters.localRepoPortalPort;

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

                var otherParams = isSecurityModule ? " -s " : "";
                otherParams += removeUserNameLogin ? " -g " : "";
                var command = "unregisterplugin " + parameters.vendorId
                                                  + parameters.pluginId
                                                  + parameters.newPassword
                                                  + parameters.newPasswordEnvVariable
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateConnection(PmrepUpdateConnection parameters)
            {
                var command = "updateconnection " + parameters.connectionSubtype
                                                   + parameters.connectionName
                                                   + parameters.newUserName
                                                   + parameters.newPassword
                                                   + parameters.newPasswordEnvVariable
                                                   + parameters.newConnectionString
                                                   + parameters.attributeName
                                                   + parameters.newAttributeValue
                                                   + parameters.connectionType
                                                   + parameters.codePage;

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
                var command = "updateseqgenvals " + parameters.folderName
                                                   + parameters.mappingName
                                                   + parameters.sequenceGeneratorName
                                                   + parameters.startValue
                                                   + parameters.endValue
                                                   + parameters.incrementBy
                                                   + parameters.currentValue;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateSrcPrefix(string folderName,string prefixName,string sessionName)
            {
                string command = "updatesrcprefix -f " + folderName + " -p " + prefixName + " -s " + sessionName;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string UpdateSrcPrefix(PmrepSrcPrefix parameters, bool useTargetInstanceName = false)
            {
                var otherParams = useTargetInstanceName ? " -n " : "";
                var command = "updatetargprefix " + parameters.folderName
                                                  + parameters.sessionName
                                                  + parameters.sourceName
                                                  + parameters.prefixName
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
            public string UpdateTargPrefix(PmrepTargPrefix parameters,bool useTargetInstanceName=false)
            {
                var otherParams = useTargetInstanceName ? " -n " : "";
                var command = "updatetargprefix " + parameters.folderName
                                                  + parameters.sessionName
                                                  + parameters.targetName
                                                  + parameters.prefixName
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

                var command = "uninstallabapprogram " + parameters.folderName
                                                  + parameters.mappingName
                                                  + parameters.versionNumber
                                                  + parameters.logFilename
                                                  + parameters.userName
                                                  + parameters.password
                                                  + parameters.connectString
                                                  + parameters.client
                                                  + parameters.language
                                                  + parameters.programMode;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
            public string Validate(PmrepValidate parameters, bool saveUponValid = false, bool checkUponValid = false, bool checkInComment = false, bool append = false, bool verbose = false, bool printDBType = false)
            {
                var otherParams = saveUponValid ? " -s " : "";
                otherParams += checkUponValid ? " -k " : "";
                otherParams += checkInComment ? " -m " : "";
                otherParams += append ? " -a " : "";
                otherParams += verbose ? " -b " : "";
                otherParams += printDBType ? " -y " : "";
                var command = "validate " + parameters.objectName
                                                  + parameters.objectType
                                                  + parameters.versionNumber
                                                  + parameters.folderName
                                                  + parameters.outputOptionTypes
                                                  + parameters.persistentInputFile
                                                  + parameters.persistentOutputFileName
                                                  + parameters.endOfRecordSeparator
                                                  + parameters.endOfListingIndicator
                                                  + otherParams;

                var result = PmrepWorker.ExecuteCommand(_pmrepFile, command);
                return result.output;
            }
        }
    }
}
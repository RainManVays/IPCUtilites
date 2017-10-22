using IPCUtilities.IpcPmrep.CommandObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities.IpcPmrep
{
    public partial class Pmrep : IDisposable
    {
        /// <summary>
        /// Get repository folder List
        /// </summary>
        /// <returns></returns>
        public async Task<IList<string>> ListFoldersAsync()
        {
            var result = await _pmWork.ExecuteCommandAsync("listobjects -o folder");
            return _pmWork.ConvertResultToArray(result);
        }
        /// <summary>
        /// Get folder workflows list
        /// </summary>
        /// <param name="folderName">name repository folder</param>
        /// <returns>workflows</returns>
        public async Task<IList<string>> ListWorkflowsAsync(string folderName)
        {
            Guard.ThrowIsNull(folderName);
            var result = await _pmWork.ExecuteCommandAsync("listobjects -o workflow -f " + folderName);
            var arrResult = _pmWork.ConvertResultToArray(result);
            string[] arrWorkflows = new string[arrResult.Length];

            for (int wfItem = 0; wfItem < arrResult.Length; wfItem++)
                if (arrResult[wfItem].Contains(_workflowSubstring))
                    arrWorkflows[wfItem]
                        = arrResult[wfItem].Substring(arrResult[wfItem].IndexOf(_workflowSubstring) + _workflowSubstring.Length);

            return arrWorkflows;
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
        public async Task<string> ObjectExportAsync(PmrepObjectExport parameters, bool m = false, bool s = false, bool b = false, bool r = false)
        {
            Guard.ThrowIsNull(parameters);

            var otherParams = m ? " -m " : "";
            otherParams += s ? " -s " : "";
            otherParams += b ? " -b " : "";
            otherParams += r ? " -r " : "";
            var command = "objectexport " + parameters.FolderName
                                              + parameters.ObjectName
                                              + parameters.ObjectType
                                              + parameters.ObjectSubType
                                              + parameters.LogFileName
                                              + parameters.DbdSeparator
                                              + parameters.PersistentInputFile
                                              + parameters.VersionNumber
                                              + parameters.XnlOutputFileName
                                              + otherParams;

            var result = await _pmWork.ExecuteCommandAsync(command);
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
        public async Task<ConnectionDetails> GetConnectionDetailsAsync(string connectionName, ConnectionType connectionType)
        {
            Guard.ThrowIsNull(connectionName);
            string command = "getconnectiondetails -n " + connectionName + " -t " + connectionType;

            var result = await _pmWork.ExecuteCommandAsync(command);
            return ConnectionDetailsAdapter.ConvertResultToConnectDetails(result);
        }

        public async Task<bool> UpdateConnectionAsync(PmrepUpdateConnection parameters)
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
            var result = await _pmWork.ExecuteCommandAsync(command);
            SetLastCommandResult(result);
            return _pmWork.CheckErrorInResult(result);
        }

        public async Task<bool> CreateFolderAsync(string folderName)
        {
            Guard.ThrowIsNull(folderName);
            string command = "createfolder -n " + folderName;

            var result = await _pmWork.ExecuteCommandAsync(command);
            SetLastCommandResult(result);
            return _pmWork.CheckErrorInResult(result);
        }

        public async Task<bool> ObjectImportAsync(PmrepObjectImport parameters, bool retainPersistentValue = true)
        {
            Guard.ThrowIsNull(parameters);

            var otherParams = retainPersistentValue ? " -p " : "";
            _pmWork.CreateControlImportFile(folders: parameters.SourceFolder,
                                                      sourceRepo: parameters.SourceRepo,
                                                      targetRepo: parameters.TargetRepo,
                                                      dtdFile: parameters.ImportDtdFile,
                                                      encoding: parameters.ControlFileEncoding);
            string command = "objectimport " + parameters.ImportXml +
                                        " -c importXml.xml " +
                                        parameters.LogFile +
                                        otherParams;


            var result = await _pmWork.ExecuteCommandAsync(command);

            SetLastCommandResult(result);

            return _pmWork.CheckErrorInResult(result);
        }

    }
}

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
    }
}

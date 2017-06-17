using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities.IpcPmcmd.CommandObjects
{
    public abstract class  AbstractTaskParams
    {
        private string _folder;
        private string _workflow;
        private string _runinstName;
        public virtual string Folder { get { return _folder; } set { _folder = " -folder " + value; } }
        public virtual string Workflow { get { return _workflow; } set { _workflow = " -workflow " + value; } }
        public virtual string RuninstName { get { return _runinstName; } set { _runinstName = " -runinsname " + value; } }
    }
}

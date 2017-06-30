using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities.IpcPmcmd
{
    class ServiceDetails
    {
        public string ServiceName { get; set; }
        public string ServiceStatus { get; set; }
        public string StartupTime { get; set; }
        public string CurrTime { get; set; }
        public int NumScheduledWorkflows { get; set; }
        public int NumActiveWorkflows { get; set; }
        public int NumActiveSessions { get; set; }
        public int NumWaitingSessions { get; set; }
       // public WorkfllowsDetails Workflows { get; set; }

    }
}

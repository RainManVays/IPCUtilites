using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCUtilities.IpcPmcmd
{
    class ServiceDetails
    {
        string NodeName { get; set; }
        string serviceName { get; set; }
        string serviceStatus { get; set; }
        string StartupTime { get; set; }
        string currTime { get; set; }
        int CountActiveWorkflow { get; set; }
        int CountActiveSessions { get; set; }
        int CountWaitingSessions { get; set; }
        WorkfllowsDetails workflows { get; set; }

    }
}

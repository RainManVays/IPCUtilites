using System;

namespace IPCUtilities.IpcPmcmd
{
    class ServiceDetailsAdapter
    {
        internal  ServiceDetails SetServiceDetailsData(string result)
        {
            ServiceDetails serviceDetails = new ServiceDetails();
            var arrayresult = ResultTreatment.ResultToArray(result);
            char[] trimChars = new char[] { ' ', '[', ']' };
            foreach (var row in arrayresult)
            {
                if (row.Contains("Integration Service status:"))
                {
                    serviceDetails.ServiceStatus = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Integration Service startup time:"))
                {
                    serviceDetails.StartupTime = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Integration Service current time:"))
                {
                    serviceDetails.CurrTime = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Number of scheduler workflows"))
                {
                    serviceDetails.NumScheduledWorkflows = Convert.ToInt32(ResultTreatment.GetRowValue(row, ":", trimChars));
                    continue;
                }
                if (row.Contains("Number of active workflows"))
                {
                    serviceDetails.NumActiveWorkflows = Convert.ToInt32(ResultTreatment.GetRowValue(row, ":", trimChars));
                    continue;
                }
                if (row.Contains("Number of waiting sessions"))
                {
                    serviceDetails.NumWaitingSessions = Convert.ToInt32(ResultTreatment.GetRowValue(row, ":", trimChars));
                    continue;
                }
                if (row.Contains("Number of active sessions"))
                {
                    serviceDetails.NumActiveSessions = Convert.ToInt32(ResultTreatment.GetRowValue(row, ":", trimChars));
                    continue;
                }

            }
            return serviceDetails;
        }
    }
}

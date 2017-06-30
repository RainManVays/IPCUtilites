namespace IPCUtilities.IpcPmcmd
{
   static class IntegrationServiceAdapter
    {
        private static IntegrationService GetServiceData(string[] arrayresult)
        {
            IntegrationService intservice = new IntegrationService();
            char[] trimChars = new char[] { ' ', '[', ']' };
            foreach (var row in arrayresult)
            {
                if (row.Contains("Integration Service status:"))
                {
                    intservice.ServiceStatus = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Integration Service startup time:"))
                {
                    intservice.ServiceStartupTime = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Integration Service current time:"))
                {
                    intservice.ServiceCurrTime = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Intergration Service"))
                {
                    intservice.ServiceName = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }

            }
            return intservice;
        }
        static internal IntegrationService GetConvertedResultIntSerivce(string result)
        {
            
            var arrayresult = ResultTreatment.ResultToArray(result);

            return GetServiceData(arrayresult);
          
        }
        static internal IntegrationService GetConvertedResultIntSerivce(string[] arrayresult)
        {
            return GetServiceData(arrayresult);
        }


    }
}

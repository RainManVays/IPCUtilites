namespace IPCUtilities.IpcPmcmd
{
   static class WorkflowDetailsAdapter
    {

       static internal WorkflowDetails GetConvertResultToWfDetails(string result)
        {
            WorkflowDetails workflowDetails = new WorkflowDetails();
            var arrayresult = ResultTreatment.ResultToArray(result);
            char[] trimChars = new char[] { ' ', '[', ']' };
            workflowDetails.Service = IntegrationServiceAdapter.GetConvertedResultIntSerivce(arrayresult);
            foreach (var row in arrayresult)
            {
                if (row.Contains("Folder"))
                {
                    workflowDetails.Folder = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Workflow: "))
                {
                    var baseResult = row.Substring(row.IndexOf('['));
                    var completeResult = baseResult.Split(']')[0];
                    workflowDetails.WorkflowName = completeResult.Trim(trimChars);
                    workflowDetails.Version = ResultTreatment.ResultToInt32(ResultTreatment.GetRowValue(row, "version", trimChars));

                    continue;
                }
                if (row.Contains("Workflow run status:"))
                {
                    workflowDetails.RunStatus = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Workflow run error code"))
                {

                    workflowDetails.ErrorCode = ResultTreatment.ResultToInt32(row);
                    continue;
                }
                if (row.Contains("Workflow run id"))
                {
                    workflowDetails.WorkflowRunId = ResultTreatment.ResultToInt32(row);
                    continue;
                }
                if (row.Contains("Start time:"))
                {
                    workflowDetails.StartTime = ResultTreatment.GetRowValue(row, "Start time:", trimChars);
                    continue;
                }
                if (row.Contains("End time:"))
                {
                    workflowDetails.EndTime = ResultTreatment.GetRowValue(row, "End time:", trimChars);
                    continue;
                }
                if (row.Contains("Workflow log file"))
                {
                    workflowDetails.LogFile = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Workflow run type"))
                {
                    workflowDetails.RunType = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Run workflow as user"))
                {
                    workflowDetails.RunUser = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Run workflow with "))
                {
                    workflowDetails.OsProfile = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
               
            }
            return workflowDetails;
        }

    }
}

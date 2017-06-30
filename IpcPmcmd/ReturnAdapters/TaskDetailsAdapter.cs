namespace IPCUtilities.IpcPmcmd
{
    class TaskDetailsAdapter
    {
        static internal TaskDetails GetConvertsResultToTaskDetails(string result)
        {
            TaskDetails taskDetails = new TaskDetails();
            var arrayresult = ResultTreatment.ResultToArray(result);
            char[] trimChars = new char[] { ' ', '[', ']' };
            taskDetails.Service = IntegrationServiceAdapter.GetConvertedResultIntSerivce(arrayresult);
            foreach (var row in arrayresult)
            {
                if (row.Contains("Folder"))
                {
                    taskDetails.Folder = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Workflow: "))
                {
                    var baseResult = row.Substring(row.IndexOf('['));
                    var completeResult = baseResult.Split(']')[0];
                    taskDetails.WorkflowName = completeResult.Trim(trimChars);
                    taskDetails.WorkflowVersion = ResultTreatment.ResultToInt32(ResultTreatment.GetRowValue(row, "version", trimChars));

                    continue;
                }
                if (row.Contains("Session Instance: "))
                {
                    var baseResult = row.Substring(row.IndexOf('['));
                    var completeResult = baseResult.Split(']')[0];
                    taskDetails.SessionName = completeResult.Trim(trimChars);
                    taskDetails.SessionVersion = ResultTreatment.ResultToInt32(ResultTreatment.GetRowValue(row, "version", trimChars));

                    continue;
                }
                if (row.Contains("Task type:"))
                {
                    taskDetails.RunStatus = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Start time:"))
                {
                    taskDetails.StartTime = ResultTreatment.GetRowValue(row, "Start time:", trimChars);
                    continue;
                }
                if (row.Contains("End time:"))
                {
                    taskDetails.EndTime = ResultTreatment.GetRowValue(row, "End time:", trimChars);
                    continue;
                }
                if (row.Contains("Task run status:"))
                {
                    taskDetails.RunStatus = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Task run error code"))
                {
                    taskDetails.ErrorCode = ResultTreatment.ResultToInt32(row);
                    continue;
                }
                if (row.Contains("Intergration Service Process:"))
                {
                    taskDetails.ServiceProcess = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Intergration Service Grid:"))
                {
                    taskDetails.ServiceGrid = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Task run mode:"))
                {
                    taskDetails.RunMode = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Mapping:"))
                {
                    taskDetails.MappingName = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Session log file"))
                {
                    taskDetails.LogFile = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("First error code"))
                {
                    taskDetails.FirstErrorCode = ResultTreatment.ResultToInt32(row);
                    continue;
                }
                if (row.Contains("First error message"))
                {
                    taskDetails.FirstErrorMessage = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Source success rows"))
                {
                    taskDetails.SourceSuccesRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Source failed rows"))
                {
                    taskDetails.SourceFailedRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Target success rows"))
                {
                    taskDetails.TargetSuccesRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Target failed rows"))
                {
                    taskDetails.TargetFailedRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Number of transformation errors"))
                {
                    taskDetails.NumberTransformError = ResultTreatment.ResultToInt32(row);
                    continue;
                }

            }
            return taskDetails;
        }
    }
}

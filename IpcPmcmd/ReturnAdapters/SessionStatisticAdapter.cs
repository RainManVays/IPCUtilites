using System.Collections.Generic;

namespace IPCUtilities.IpcPmcmd
{
    internal static class SessionStatisticAdapter
    {
        static private List<string> GetSessionHeaderData(string[] arrayresult)
        {
            List<string> sessionHeader = new List<string>();
            foreach (var row in arrayresult)
            {
                if (row.Contains("-----------"))
                    break;
                else
                    sessionHeader.Add(row);
            }
            return sessionHeader;
        }
        static private List<string> GetTransformationData(string[] arrayresult,int startIndex)
        {
            List<string> transformationData = new List<string>();
            for (int row = startIndex; row < arrayresult.Length; row++)
            {
                if(!arrayresult[row].Contains("-----------")&& !arrayresult[row].Contains("Node Name(s)") && !arrayresult[row].Contains("Preparation fragment"))
                    transformationData.Add(arrayresult[row]);
            }
            return transformationData;
        }
        static internal SessionStatistic GetConvertsResultToTaskDetails(string result)
        {
            SessionStatistic sessionStatistic = new SessionStatistic();
            var arrayresult = ResultTreatment.ResultToArray(result);
            
            
            var sessionHeader = GetSessionHeaderData(arrayresult);
            var transformationData = GetTransformationData(arrayresult, sessionHeader.Count);

            char[] trimChars = new char[] { ' ', '[', ']' };
            sessionStatistic.Transformations = TransformationStatAdapter.GetConvertsResultToTransformation(transformationData);

            foreach (var row in sessionHeader)
            {
                if (row.Contains("Folder"))
                {
                    sessionStatistic.Folder = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Workflow: "))
                {
                    sessionStatistic.WorkflowName = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Instance: "))
                {
                    sessionStatistic.SessionName = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Mapping:"))
                {
                    sessionStatistic.MappingName = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Session log file"))
                {
                    sessionStatistic.LogFile = ResultTreatment.GetRowValue(row, "Session log file:", trimChars);
                    continue;
                }
                if (row.Contains("Source success rows"))
                {
                    sessionStatistic.SourceSuccesRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Source failed rows"))
                {
                    sessionStatistic.SourceFailedRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Target success rows"))
                {
                    sessionStatistic.TargetSuccesRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Target failed rows"))
                {
                    sessionStatistic.TargetFailedRows = ResultTreatment.ResultToInt64(row);
                    continue;
                }
                if (row.Contains("Number of transformation errors"))
                {
                    sessionStatistic.NumberTransformError = ResultTreatment.ResultToInt32(row);
                    continue;
                }
                if (row.Contains("First error code"))
                {
                    sessionStatistic.FirstErrorCode = ResultTreatment.ResultToInt32(row);
                    continue;
                }
                if (row.Contains("First error message"))
                {
                    sessionStatistic.FirstErrorMessage = ResultTreatment.GetRowValue(row, "First error message:", trimChars);
                    continue;
                }
                if (row.Contains("Task run status:"))
                {
                    sessionStatistic.RunStatus = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Task run mode:"))
                {
                    sessionStatistic.ServiceName = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Intergration Service Process:"))
                {
                    sessionStatistic.ServiceProcess = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
                if (row.Contains("Intergration Service Grid:"))
                {
                    sessionStatistic.ServiceGrid = ResultTreatment.GetRowValue(row, ":", trimChars);
                    continue;
                }
            }
            return sessionStatistic;
        }
    }
}

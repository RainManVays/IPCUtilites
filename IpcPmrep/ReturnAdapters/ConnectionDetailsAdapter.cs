using System;
namespace IPCUtilities.IpcPmrep
{
    static class ConnectionDetailsAdapter
    {
        internal static ConnectionDetails ConvertResultToConnectDetails(string result)
        {
            ConnectionDetails connectionData = new ConnectionDetails();
            var arrayresult = ResultTreatment.ResultToArray(result);
            char[] trimChars = new char[] { ' ', '[', ']' };
            foreach (var row in arrayresult)
            {
                if (row.Contains("Connection Type"))
                {
                    connectionData.connectType = (ConnectionType)Enum.Parse(typeof(ConnectionType),ResultTreatment.GetRowValue(row, "="));
                    continue;
                }

                if (row.Contains("Name") && !row.Contains("User Name"))
                {
                    connectionData.Name = ResultTreatment.GetRowValue(row, "=");
                    continue;
                }
                if (row.Contains("Type")&&!row.Contains("Connection Type"))
                {
                    connectionData.DbType = ResultTreatment.GetRowValue(row, "=");
                    continue;
                }
                if (row.Contains("User Name"))
                {
                    connectionData.UserName = ResultTreatment.GetRowValue(row, "=");
                    continue;
                }
                if (row.Contains("Connect String"))
                {
                    connectionData.ConnectString = ResultTreatment.GetRowValue(row, "=");
                    continue;
                }
                if (row.Contains("Code page"))
                {
                    connectionData.CodePage = ResultTreatment.GetRowValue(row, "=");
                    continue;
                }
                if (row.Contains("Rollback Segment"))
                {
                    connectionData.RollbackSegment = ResultTreatment.GetRowValue(row, "=");
                    continue;
                }
                if (row.Contains("Connection Envi"))
                {
                    connectionData.ConnectEnvSql = ResultTreatment.GetRowValue(row, "=", trimChars);
                    continue;
                }
                if (row.Contains("Transaction Envi"))
                {
                    connectionData.TransactionEnvSql = ResultTreatment.GetRowValue(row, "=", trimChars);
                    continue;
                }
                if (row.Contains("Enable Parallel Mode"))
                {
                    connectionData.EnableParralelMode = ResultTreatment.ResultToInt32(ResultTreatment.GetRowValue(row, "=", trimChars));
                    continue;
                }
                if (row.Contains("Connection Retry Period"))
                {
                    connectionData.RetryPeriod = ResultTreatment.ResultToInt32(ResultTreatment.GetRowValue(row, "=", trimChars));
                    continue;
                }
            }
            return connectionData;
        }
    }
}

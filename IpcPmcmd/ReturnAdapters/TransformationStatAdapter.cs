using System.Collections.Generic;


namespace IPCUtilities.IpcPmcmd
{
    class TransformationStatAdapter
    {
        static internal List<Transformation> GetConvertsResultToTransformation(List<string> result)
        {
            List<Transformation> transformations = new List<Transformation>();
           
            char[] trimChars = new char[] { ' ', '[', ']' };
            for(int block = 0; block < result.Count; block += 11)
            {
                Transformation transform = new Transformation();
                if (result[block].Contains("Partition"))
                    transform.Partition = ResultTreatment.GetRowValue(result[block], ":", trimChars);
                if (result[block+1].Contains("Transformation instance"))
                    transform.TransformInstance = ResultTreatment.GetRowValue(result[block + 1], ":", trimChars);
                if (result[block + 2].Contains("Transformation") && !result[block + 2].Contains("Transformation instance"))
                    transform.Name = ResultTreatment.GetRowValue(result[block + 2], ":", trimChars);
                if (result[block + 3].Contains("Applied rows"))
                    transform.AppliedRows = ResultTreatment.ResultToInt64(result[block + 3]);
                if (result[block + 4].Contains("Affected rows"))
                    transform.AffectedRows = ResultTreatment.ResultToInt64(result[block + 4]);
                if (result[block + 5].Contains("Rejected rows"))
                    transform.RejectedRows = ResultTreatment.ResultToInt64(result[block + 5]);
                if (result[block + 6].Contains("Throughput(Rows/Sec)"))
                    transform.ThroughputRows = ResultTreatment.ResultToInt64(result[block + 6]);
                if (result[block + 7].Contains("Throughput(Bytes/Sec)"))
                    transform.ThroughputBytes = ResultTreatment.ResultToInt64(result[block + 7]);
                if (result[block + 8].Contains("Last error code"))
                {
                    transform.LastErrorCode = ResultTreatment.ResultToInt32(result[block + 8].Split(new char[]{','})[0]);
                    transform.LastErrorMessage= ResultTreatment.GetRowValue(result[block + 8], "message", trimChars);
                }
                if (result[block + 9].Contains("Start time"))
                    transform.StartTime = ResultTreatment.GetRowValue(result[block + 9], ":", trimChars);
                if (result[block + 10].Contains("End time"))
                    transform.EndTime = ResultTreatment.GetRowValue(result[block + 10], ":", trimChars);

                transformations.Add(transform);
            }

            return transformations;
        }
    }
}

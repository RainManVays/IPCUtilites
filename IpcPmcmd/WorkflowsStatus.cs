
namespace IPCUtilities.IpcPmcmd
{
    public class WorkflowsStatus
    {
        private WorkflowsStatus(string value) { Value = value; }
        public string Value { get; set; }
        public static WorkflowsStatus All { get { return new WorkflowsStatus(" -all "); } }
        public static WorkflowsStatus Running { get { return new WorkflowsStatus(" -running "); } }
        public static WorkflowsStatus Scheduled { get { return new WorkflowsStatus(" -scheduled "); } }
    }
}

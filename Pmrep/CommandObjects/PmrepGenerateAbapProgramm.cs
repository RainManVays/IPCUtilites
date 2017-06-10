namespace IPCUtilities.IpcPmrep
{
    public class PmrepGenerateAbapProgramm:AbstractAbapProgram
    {
        private string _outputFileLocation;

        public string outputFileLocation { get { return _outputFileLocation; } set { _outputFileLocation = " -f " + value; } }
    }
}
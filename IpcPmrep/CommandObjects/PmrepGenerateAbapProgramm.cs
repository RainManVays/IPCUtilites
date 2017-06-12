namespace IPCUtilities.IpcPmrep
{
    public class PmrepGenerateAbapProgramm:AbstractAbapProgram
    {
        private string _outputFileLocation;

        public string OutputFileLocation { get { return _outputFileLocation; } set { _outputFileLocation = " -f " + value; } }
    }
}
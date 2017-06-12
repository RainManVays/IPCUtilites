
namespace IPCUtilities.IpcPmrep
{
        public class PmrepInstallAbapProgram:AbstractAbapProgram
        {

            private string _inputFileName;
            private string _developmentClassName;

            public string InputFileName { get { return _inputFileName; } set { _inputFileName = " -f " + value; } }
            public string DevelopmentClassName { get { return _developmentClassName; } set { _developmentClassName = " -d " + value; } }

        }
}
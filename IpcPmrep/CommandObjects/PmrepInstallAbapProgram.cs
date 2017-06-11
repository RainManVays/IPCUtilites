
namespace IPCUtilities.IpcPmrep
{
        public class PmrepInstallAbapProgram:AbstractAbapProgram
        {

            private string _inputFileName;
            private string _developmentClassName;

            public string inputFileName { get { return _inputFileName; } set { _inputFileName = " -f " + value; } }
            public string developmentClassName { get { return _developmentClassName; } set { _developmentClassName = " -d " + value; } }

        }
}
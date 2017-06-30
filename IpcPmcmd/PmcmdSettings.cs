namespace IPCUtilities.IpcPmcmd
{
    public static class PmcmdSettings
    {

        internal static int _countDelay=60;
        internal static int _delayInterval=300;

        public static void SetDelayInterval(int interval)
        {
            _delayInterval = interval;
        }
        public static void SetCountDelay(int count)
        {
            _countDelay = count;
        }


    }
}

using System.IO;
namespace IPCUtilities
{
    static class CheckExists
    {

        public static string CheckAndModifyPath(string path,string fileName)
        {
            if (Directory.Exists(path))
            {
                return path + "\\" + fileName;
            }

            if (File.Exists(path))
            {
                return path;
            }
            return null;
        }


    }
}

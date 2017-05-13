using System.IO;

namespace PandyIT.Core.FileSystem
{
    public static class FileUtils
    {
        public static string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}

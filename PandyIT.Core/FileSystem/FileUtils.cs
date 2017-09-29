using System.Collections;
using System.IO;

namespace PandyIT.Core.FileSystem
{
    public static class FileUtils
    {
        public static string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        public static string[] GetFiles(string sourceFolder, string filter, SearchOption searchOption)
        {
            var allFiles = new ArrayList();

            var filters = filter.Split('|');

            foreach (string f in filters)
            {
                allFiles.AddRange(Directory.GetFiles(sourceFolder, f, searchOption));
            }

            // returns string array of relevant file names
            return (string[])allFiles.ToArray(typeof(string));
        }
    }
}

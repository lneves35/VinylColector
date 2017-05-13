using PandyIT.Core.FileSystem;

namespace PandyIT.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToSafeFilename(this string filename)
        {
            return FileUtils.GetSafeFilename(filename);
        }
    }
}

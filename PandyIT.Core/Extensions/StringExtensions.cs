namespace PandyIT.Core.Extensions
{
    using PandyIT.Core.FileSystem;
    using PandyIT.Core.Text;

    public static class StringExtensions
    {
        public static string ToSafeFilename(this string filename)
        {
            return FileUtils.GetSafeFilename(filename);
        }

        public static string RemoveBrackets(this string text)
        {
            return TextUtils.RemoveBrackets(text);
        }
    }
}

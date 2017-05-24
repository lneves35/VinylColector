namespace PandyIT.Core.Extensions
{
    using PandyIT.Core.Security;
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

        public static string MD5(this string text)
        {
            return Encryptor.MD5Hash(text);
        }
    }
}

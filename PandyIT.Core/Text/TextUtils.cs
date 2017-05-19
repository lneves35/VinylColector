namespace PandyIT.Core.Text
{
    using System.Text.RegularExpressions;

    public class TextUtils
    {
        public static string RemoveBrackets(string input)
        {
            var noBracket = Regex.Replace(input, @"\[[^]]*\]", string.Empty);
            return noBracket;
        }
    }
}

namespace Site.Services
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string StripHtmlTags(this string content)
        {
            const string pattern = @"<(.|\n)*?>";

            return Regex.Replace(content, pattern, string.Empty);;
        }
    }
}
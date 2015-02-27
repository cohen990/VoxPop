namespace Site.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class PollService
    {
        public static string EncodePollOption(this string option)
        {
            return HttpUtility.UrlEncode(option);
        }

        public static List<string> EncodePollOptions(this List<string> options)
        {
            return options.Select(HttpUtility.UrlEncode).ToList();
        }

        public static Dictionary<string, int> DecodePoll(this Dictionary<string, int> poll)
        {
            return poll.Keys.ToDictionary(HttpUtility.UrlDecode, key => poll[key]);
        }
    }
}
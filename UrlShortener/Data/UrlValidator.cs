using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UrlShortener.Data
{
    public interface IUrlValidator{
        public bool IsValid(string url);
        public string SanitizeUrl(string url);
    }

    public class UrlValidator
    {
        public List<string> UrlSuffixes { get; set; } = new List<string> { ".co.uk", ".com", ".org", ".edu",".int", "", ".gov" };

        public bool IsValidUrl(string url)
        {
            string urlPattern = "[(http(s)?):\\/\\/(www\\.)?a-zA-Z0-9@:%._\\+~#=]{2,256}\\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\\+.~#?&//=]*)";
            return Regex.IsMatch(urlPattern,url);
        }
    }
}

using System;
using System.Text.RegularExpressions;
using System.Web;

namespace R7.Dnn.UserHtml.Components
{
    public static class HtmlStripper
    {
        public static string StripTags (string html, params string [] tags)
        {
            foreach (var tag in tags) {
                var trimmedTag = tag.Trim ();
                html = Regex.Replace (html, $@"<{trimmedTag}.*>.*</{trimmedTag}>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                html = Regex.Replace (html, $@"<{trimmedTag}.*/>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase) ;
            }

            return html;
        }

        public static string StripTags (string html, bool htmlEncoded, string tags, string tagSeparators)
        {
            if (!string.IsNullOrEmpty (html) && !string.IsNullOrEmpty (tags)) {
                var tagParams = tags.Split (tagSeparators.ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
                if (tagParams.Length > 0) {
                    html = StripTags (htmlEncoded ? HttpUtility.HtmlDecode (html) : html, tagParams);
                }
           
                return htmlEncoded ? HttpUtility.HtmlEncode (html) : html;
            }

            return html;
        }
    }
}

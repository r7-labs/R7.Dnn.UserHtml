//
// HtmlStripper.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2018 Roman M. Yagodin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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

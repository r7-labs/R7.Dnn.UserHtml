//
// HtmlStripperTests.cs
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

using Xunit;
using R7.Dnn.UserHtml.Components;

namespace R7.Dnn.UserHtml.Tests.Components
{
    public class HtmlStripperTests
    {
        [Fact]
        public void StripTagsTest ()
        {
            Assert.Equal (
                "<p>Some content</p>",
                HtmlStripper.StripTags (
                    "<p>Some content</p><script type=\"text/javascript\">alert ('Hello!');</script>",
                    "script"
                )
            );

            Assert.Equal (
                "<div>Some content</div>",
                HtmlStripper.StripTags (
                    "<div>Some content<script>alert ('Hello!');</script></div>",
                    "script"
                )
            );

            Assert.Equal (
                "<p>Some content</p>",
                HtmlStripper.StripTags (
                    "<style type=\"text/css\">p { font-weight:bold; }</style><p>Some content</p><script type=\"text/javascript\">alert ('Hello!');</script>",
                                        "script", "style"
                )
            );

            Assert.Equal (
                "<p>Some content</p>",
                HtmlStripper.StripTags (
                    "<style type=\"text/css\">p { font-weight:bold; }</style><p>Some content</p><script type=\"text/javascript\">alert ('Hello!');</script>",
                    htmlEncoded: false,
                    tags: "script, style",
                    tagSeparators: ",;"
                )
            );
        }
    }
}

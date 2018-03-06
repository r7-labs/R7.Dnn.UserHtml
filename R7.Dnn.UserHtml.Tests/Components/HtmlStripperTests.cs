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

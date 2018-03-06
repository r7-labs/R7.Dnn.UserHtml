using System;
using R7.Dnn.UserHtml.Data;
using Xunit;

namespace R7.Dnn.UserHtml.Tests.Data
{
    public class CKEditorTemplateTokenDataSourceTests
    {
        [Fact]
        public void ReadTemplatesTest ()
        {
            var ds1 = new CKEditorTemplateTokenDataSource ();
            ds1.ReadTemplates ("../../Data/Templates/ValidTemplates.xml");
            Assert.Equal (2, ds1.Templates.Count);

            var ds2 = new CKEditorTemplateTokenDataSource ();
            Assert.ThrowsAny<Exception> (() => ds2.ReadTemplates ("../../Data/Templates/InvalidTemplates.xml"));

            var ds3 = new CKEditorTemplateTokenDataSource ();
            Assert.ThrowsAny<Exception> (() => ds3.ReadTemplates ("../../Data/Templates/BrokenTemplates.xml"));
        }
    }
}

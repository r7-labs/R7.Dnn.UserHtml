//
// CKEditorTemplateTokenDataSourceTests.cs
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

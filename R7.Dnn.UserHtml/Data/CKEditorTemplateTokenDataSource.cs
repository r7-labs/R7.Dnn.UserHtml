//
// CKEditorTemplateTokenDataSource.cs
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
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml.Data
{
    public class CKEditorTemplateTokenDataSource
    {
        public Dictionary<string, string> Templates { get; protected set; }

        public CKEditorTemplateTokenDataSource ()
        {
            Templates = new Dictionary<string, string> ();
        }

        public CKEditorTemplateTokenDataSource (int? templatesFileId): this ()
        {
            if (templatesFileId != null) {
                var templatesFile = FileManager.Instance.GetFile (templatesFileId.Value);
                if (templatesFile != null) {
                    ReadTemplates (templatesFile.PhysicalPath, Templates);
                }
                else {
                    Exceptions.LogException (
                        new Exception ($"Cannot find templates file with FileId={templatesFileId.Value}")
                    );
                }
            }
        }

        public CKEditorTemplateTokenDataSource (string templatesFile): this ()
        {
            ReadTemplates (templatesFile, Templates);
        }

        void ReadTemplates (string templatesFile, IDictionary<string, string> templates)
        {
            var sr = default (StreamReader);
            try {
                using (sr = new StreamReader (templatesFile)) {
                    var serializer = new XmlSerializer (typeof (CKEditorTemplatesRootInfo));
                    var templatesRoot = (CKEditorTemplatesRootInfo) serializer.Deserialize (sr);
                    foreach (var template in templatesRoot.Templates) {
                        templates.Add (template.Title, template.Html);
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.LogException (
                    new Exception ($"Error reading the {templatesFile} templates file.", ex)
                ); 
                templates.Clear ();
            }
            finally {
                if (sr != null)  {
                    sr.Close ();
                }
            }
        }
    }
}

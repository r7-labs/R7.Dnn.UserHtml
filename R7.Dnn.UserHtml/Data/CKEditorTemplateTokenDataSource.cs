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
            try {
                if (templatesFileId != null) {
                    var templatesFile = FileManager.Instance.GetFile (templatesFileId.Value);
                    if (templatesFile != null) {
                        ReadTemplates (templatesFile.PhysicalPath);
                    }
                    else {
                        throw new FileNotFoundException ($"Cannot find templates file with FileId={templatesFileId.Value}");
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
            }
        }

        public CKEditorTemplateTokenDataSource (string templatesFile): this ()
        {
            try {
                ReadTemplates (templatesFile);
            }
            catch (Exception ex) {
                Exceptions.LogException (ex);
            }
        }

        public void ReadTemplates (string templatesFile)
        {
            var sr = default (StreamReader);
            try {
                using (sr = new StreamReader (templatesFile)) {
                    var serializer = new XmlSerializer (typeof (CKEditorTemplatesRootInfo));
                    var templatesRoot = (CKEditorTemplatesRootInfo) serializer.Deserialize (sr);
                    foreach (var template in templatesRoot.Templates) {
                        Templates.Add (template.Title, template.Html);
                    }
                }
            }
            catch (Exception ex) {
                throw new Exception ($"Error reading the {templatesFile} templates file.", ex);
            }
            finally {
                if (sr != null)  {
                    sr.Close ();
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace R7.Dnn.UserHtml.Models
{
    [XmlRoot ("Templates")]
    public class CKEditorTemplatesRootInfo
    {
        [XmlAttribute ("templateName")]
        public string TemplateName { get; set; }

        [XmlElement ("Template")]
        public List<CKEditorTemplateInfo> Templates { get; set; } = new List<CKEditorTemplateInfo> ();
    }
}

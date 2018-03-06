using System.Xml.Serialization;

namespace R7.Dnn.UserHtml.Models
{
    public class CKEditorTemplateInfo
    {
        [XmlAttribute ("title")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Html { get; set; }
    }
}

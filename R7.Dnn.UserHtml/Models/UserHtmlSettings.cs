using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Entities.Modules.Settings;

namespace R7.Dnn.UserHtml.Models
{
    public class UserHtmlSettings
    {
        [ModuleSetting (Prefix = "UserHtml_")]
        public string EmptyHtml { get; set; }

        [ModuleSetting (Prefix = "UserHtml_")]
        public string DefaultHtml { get; set; }

        [ModuleSetting (Prefix = "UserHtml_")]
        public int? TemplatesFileId { get; set; }

        [ModuleSetting (Prefix = "UserHtml_")]
        public string StripTags { get; set; }

        [ModuleSetting (Prefix = "UserHtml_")]
        public string Roles { get; set; }

        public IEnumerable<int> RoleIds =>
            (Roles ?? string.Empty)
               .Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries)
               .Select (strRoleId => int.Parse (strRoleId));
    }
}

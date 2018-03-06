using System;
using DotNetNuke.ComponentModel.DataAnnotations;

namespace R7.Dnn.UserHtml.Models
{
    // TODO: Extract interface
    [TableName ("r7_UserHtmls")]
    [PrimaryKey (nameof (UserHtmlId), AutoIncrement = true)]
    public class UserHtmlInfo
    {
        public int UserHtmlId { get; set; }

        public int ModuleId { get; set; }

        public int UserId { get; set; }

        public string UserHtml { get; set; }

        public int CreatedByUserId { get; set; }

        public int LastModifiedByUserId { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public DateTime LastModifiedOnDate { get; set; }
    }
}

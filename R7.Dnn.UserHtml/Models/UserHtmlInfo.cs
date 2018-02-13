//
//  UserHtmlInfo.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Volgograd State Agricultural University
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Web;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace R7.Dnn.UserHtml.Models
{
    // http://www.dnnsoftware.com/wiki/dal-2

    [TableName ("r7_UserHtml_UserHtmls")]
    [PrimaryKey (nameof (UserHtmlId), AutoIncrement = true)]
    [Scope (nameof (ModuleId))]
    [Cacheable]
    public class UserHtmlInfo
    {
        #region Fields

        string createdByUserName;

        #endregion

        #region Properties

        public int UserHtmlId { get; set; }

        public int ModuleId { get; set; }

        public string Content { get; set; }

        public int CreatedByUserId { get; set; }

        [ReadOnlyColumn]
        public DateTime CreatedOnDate { get; set; }

        [IgnoreColumn]
        public string CreatedByUserName {
            get {
                if (createdByUserName == null) {
                    var portalId = PortalController.Instance.GetCurrentPortalSettings ().PortalId;
                    var user = UserController.GetUserById (portalId, CreatedByUserId);
                    createdByUserName = user.DisplayName;
                }

                return createdByUserName;
            }
        }

        public string FillTemplate (string template)
        {
            template = template.Replace ("[CREATEDBYUSER]", CreatedByUserId.ToString ());
            template = template.Replace ("[CREATEDBYUSERNAME]", CreatedByUserName);
            template = template.Replace ("[CREATEDONDATE]", CreatedOnDate.ToShortDateString ());
            template = template.Replace ("[CONTENT]", HttpUtility.HtmlDecode (Content));

            return template;
        }

        #endregion
    }
}

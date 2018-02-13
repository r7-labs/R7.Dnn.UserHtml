//
//  UserHtmlController.cs
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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search.Entities;
using R7.Dnn.Extensions.Data;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml.Components
{
    public class UserHtmlController : ModuleSearchBase, IPortable
    {
        #region ModuleSearchBase implementaion

        public override IList<SearchDocument> GetModifiedSearchDocuments (ModuleInfo moduleInfo, DateTime beginDateUtc)
        {
            var searchDocs = new List<SearchDocument> ();

            // TODO: Implement GetModifiedSearchDocuments () here
            // var sd = new SearchDocument ();
            // searchDocs.Add (searchDoc);

            return searchDocs;
        }

        #endregion

        #region IPortable members

        /// <summary>
        /// Exports a module to XML
        /// </summary>
        /// <param name="ModuleID">a module ID</param>
        /// <returns>XML string with module representation</returns>
        public string ExportModule (int moduleId)
        {
            var sb = new StringBuilder ();
            var dataProvider = new Dal2DataProvider ();
            var infos = dataProvider.GetObjects<UserHtmlInfo> (moduleId);

            if (infos != null) {
                sb.Append ("<UserHtmls>");
                foreach (var info in infos) {
                    sb.Append ("<UserHtml>");
                    sb.Append ("<content>");
                    sb.Append (XmlUtils.XMLEncode (info.Content));
                    sb.Append ("</content>");
                    sb.Append ("</UserHtml>");
                }
                sb.Append ("</UserHtmls>");
            }

            return sb.ToString ();
        }

        /// <summary>
        /// Imports a module from an XML
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule (int ModuleID, string Content, string Version, int UserID)
        {
            var infos = DotNetNuke.Common.Globals.GetContent (Content, "UserHtmls");
            var dataProvider = new Dal2DataProvider ();

            foreach (XmlNode info in infos.SelectNodes ("UserHtml")) {
                var item = new UserHtmlInfo ();
                item.ModuleId = ModuleID;
                item.Content = info.SelectSingleNode ("content").InnerText;
                item.CreatedByUserId = UserID;

                dataProvider.Add<UserHtmlInfo> (item);
            }
        }

        #endregion
    }
}

//
//  ViewUserHtml.ascx.cs
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
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Icons;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.Extensions.Data;
using R7.Dnn.Extensions.ModuleExtensions;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml
{
    public class ViewUserHtml : PortalModuleBase<UserHtmlSettings>, IActionable
    {
        #region Controls

        protected ListView lstContent;

        #endregion

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                if (!IsPostBack) {
                    var dataProvider = new Dal2DataProvider ();
                    var items = dataProvider.GetObjects<UserHtmlInfo> (ModuleId);

                    // check if we have some content to display
                    if (IsEditable && !items.Any ()) {
                        this.Message ("NothingToDisplay.Text", MessageType.Info, true);
                    } else {
                        // bind the data
                        lstContent.DataSource = items;
                        lstContent.DataBind ();
                    }
                }
            } catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #region IActionable implementation

        public ModuleActionCollection ModuleActions {
            get {
                var actions = new ModuleActionCollection ();
                actions.Add (
                    GetNextActionID (),
                    LocalizeString (ModuleActionType.AddContent),
                    ModuleActionType.AddContent,
                    "",
                    IconController.IconURL ("Add"),
                    EditUrl ("Edit"),
                    false,
                    DotNetNuke.Security.SecurityAccessLevel.Edit,
                    true,
                    false
                );

                return actions;
            }
        }

        #endregion
    }
}

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
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Icons;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.Extensions.ModuleExtensions;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.UserHtml.Data;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml
{
    public class ViewUserHtml : PortalModuleBase<UserHtmlSettings>, IActionable
    {
        #region Controls

        protected Literal litUserHtml;

        #endregion

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                if (!IsPostBack) {
                    if (Request.IsAuthenticated) {
                        var dataProvider = new UserHtmlDataProvider ();
                        // TODO: Add support for userid querystring parameter?
                        var userHtml = dataProvider.GetUserHtml (UserId, ModuleId);
                        if (userHtml != null && !string.IsNullOrEmpty (userHtml.UserHtml)) {
                            litUserHtml.Text = userHtml.UserHtml;
                        }
                        else if (IsEditable) {
                            this.Message ("NothingToDisplay.Text", MessageType.Info, true);
                        }
                        else {
                            // TODO: Display default content?
                            HideModule ();
                        }
                    }
                    else {
                        HideModule ();
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        void HideModule ()
        {
            ContainerControl.Visible = false;
        }

        #region IActionable implementation

        public ModuleActionCollection ModuleActions {
            get {
                var actions = new ModuleActionCollection ();
                actions.Add (
                    GetNextActionID (),
                    LocalizeString ("AddUserHtml.Action"),
                    ModuleActionType.AddContent,
                    "",
                    IconController.IconURL ("Add"),
                    EditUrl ("Edit"),
                    false,
                    SecurityAccessLevel.Edit,
                    true,
                    false
                );
                // TODO: Remove test data
                actions.Add (
                    GetNextActionID (),
                    "Edit",
                    ModuleActionType.AddContent,
                    "",
                    IconController.IconURL ("Edit"),
                    EditUrl ("user_id", "7", "Edit"),
                    false,
                    SecurityAccessLevel.Edit,
                    true,
                    false
                );
                return actions;
            }
        }

        #endregion
    }
}

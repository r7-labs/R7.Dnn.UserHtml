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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Icons;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.UserHtml.Data;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml
{
    public class ViewUserHtml : PortalModuleBase<UserHtmlSettings>, IActionable
    {
        #region Controls

        protected Literal litUserHtml;
        protected TextBox txtSearchUser;
        protected DropDownList selUser;
        protected HyperLink lnkEditUserHtml;
        protected Panel pnlUser;
        protected Panel pnlSearchUser;
        protected Panel pnlSelectUser;
        protected Panel pnlUserHtml;
        protected Label lblSearchResult;

        #endregion

        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            pnlUser.Visible = false;
            pnlSelectUser.Visible = false;
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                if (!IsPostBack) {
                    // TODO: Add support for userid querystring parameter?
                    // TODO: Don't show content on profile page of different user.
                    if (Request.IsAuthenticated) {
                        var dataProvider = new UserHtmlDataProvider ();
                        var userHtml = dataProvider.GetUserHtml (UserId, ModuleId);
                        if (userHtml != null && !string.IsNullOrEmpty (userHtml.UserHtml)) {
                            litUserHtml.Text = HttpUtility.HtmlDecode (userHtml.UserHtml);
                        }
                        else if (IsEditable) {
                            pnlUser.Visible = true;
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
                    LocalizeString ("AddUserHtml_Action.Text"),
                    ModuleActionType.AddContent,
                    "",
                    IconController.IconURL ("Add"),
                    EditUrl ("Edit"),
                    false,
                    SecurityAccessLevel.Edit,
                    true,
                    false
                );
                return actions;
            }
        }

        #endregion

        protected void btnSearchUser_Click (object sender, EventArgs e)
        {
            // TODO: Store search text and selected user in the session.
            var users = SearchUsers (txtSearchUser.Text.Trim ());
            if (users != null && users.Any ()) {
                pnlSelectUser.Visible = true;
                lblSearchResult.Text = string.Format (LocalizeString ("UsersFound_Format.Text"), users.Count ());
                selUser.DataSource = users;
                selUser.DataBind ();
                selUser_SelectedIndexChanged (selUser, new EventArgs ());
            }
            else {
                pnlSelectUser.Visible = false;
                lblSearchResult.Text = LocalizeString ("NoUsersFound.Text");
            }
        }

        // TODO: Move to BLL
        // TODO: Also search by FirstName/LastName combinations
        IEnumerable<UserInfo> SearchUsers (string searchText)
        {
            var searchTextLC = searchText.ToLower ();

            return UserController.GetUsers (false, false, PortalId)
                                 .Cast<UserInfo> ()
                                 .Where (u =>
                                         (u.Email != null && u.Email.ToLower ().Contains (searchTextLC)) ||
                                         (u.Username != null && u.Username.ToLower ().Contains (searchTextLC)) ||
                                         (u.DisplayName != null && u.DisplayName.ToLower ().Contains (searchTextLC)) ||
                                         (u.LastName != null && u.LastName.ToLower ().Contains (searchTextLC)) ||
                                         (u.FirstName != null && u.FirstName.ToLower ().Contains (searchTextLC))
                                        );
        }

        protected void selUser_SelectedIndexChanged (object sender, EventArgs e)
        {
            var userId = int.Parse (selUser.SelectedValue);
            lnkEditUserHtml.NavigateUrl = EditUrl ("user_id", userId.ToString (), "Edit");

            var dataProvider = new UserHtmlDataProvider ();
            var userHtml = dataProvider.GetUserHtml (userId, ModuleId);
            if (userHtml != null) {
                litUserHtml.Text = HttpUtility.HtmlDecode (userHtml.UserHtml);
            }
            else {
                litUserHtml.Text = string.Empty;
            }
        }
    }
}

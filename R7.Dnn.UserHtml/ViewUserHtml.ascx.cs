//
// ViewUserHtml.ascx.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2018 Roman M. Yagodin
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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
using R7.Dnn.UserHtml.ViewModels;
using R7.Dnn.Extensions.Utilities;

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

            txtSearchUser.Attributes.Add ("placeholder", LocalizeString ("txtSearchUser_Placeholder.Text"));
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                if (!IsPostBack) {
                    // TODO: Add support for userid querystring parameter?
                    // TODO: Don't show content on profile page of different user.
                    if (Request.IsAuthenticated) {
                        if (!IsOnProfilePage () || IsOnOwnProfilePage ()) {
                            ShowContent ();
                            pnlUser.Visible = IsEditable;
                        }
                        else {
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

        int? GetUserProfileId () => TypeUtils.ParseToNullable<int> (Request.QueryString ["userid"]);

        // TODO: Use profile page from portal settings?
        bool IsOnProfilePage () => GetUserProfileId () != null;

        bool IsOnOwnProfilePage () => IsOnProfilePage () && GetUserProfileId ().Value == UserId;

        void ShowContent ()
        {
            var dataProvider = new UserHtmlDataProvider ();
            var userHtml = dataProvider.GetUserHtml (UserId, ModuleId);
            if (userHtml != null && !string.IsNullOrEmpty (userHtml.UserHtml)) {
                litUserHtml.Text = HttpUtility.HtmlDecode (userHtml.UserHtml);
            }
            else {
                litUserHtml.Text = HttpUtility.HtmlDecode (Settings.EmptyHtml);
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
                selUser.DataSource = users.OrderBy (u => u.DisplayName)
                                          .Select (u => new UserViewModel (u));
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

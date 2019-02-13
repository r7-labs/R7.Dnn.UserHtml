//
// ViewUserHtml.ascx.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2018-2019 Roman M. Yagodin
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
using DotNetNuke.Security.Permissions;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.Extensions.Modules;
using R7.Dnn.Extensions.Text;
using R7.Dnn.UserHtml.Components;
using R7.Dnn.UserHtml.Data;
using R7.Dnn.UserHtml.Models;
using R7.Dnn.UserHtml.ViewModels;

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
        protected LinkButton btnSearchUser;

        #endregion

        #region Session-state properties

        int? _sessionUserId;
        protected int? SessionUserId {
            get { return _sessionUserId ?? (_sessionUserId = (int?) Session ["UserHtml_UserId_" + TabModuleId]); }
            set { Session ["UserHtml_UserId_" + TabModuleId] = _sessionUserId = value; }
        }

        #endregion

        void TryRestoreState ()
        {
            if (SessionUserId != null) {
                var user = UserController.Instance.GetUser (PortalId, SessionUserId.Value);
                if (user != null && user.IsInAnyRole (Settings.RoleIds, true)) {
                    pnlSelectUser.Visible = true;
                    selUser.DataSource = new List<UserViewModel> { new UserViewModel (user) };
                    selUser.DataBind ();
                    selUser_SelectedIndexChanged_Internal ();
                }
            }
        }

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
                    var showModule = false; 
                    // TODO: Add support for userid querystring parameter?
                    // TODO: Don't show content on profile page of different user.
                    if (Request.IsAuthenticated) {
                        if (ModulePermissionController.CanEditModuleContent (ModuleConfiguration)) {
                            ShowEditPanel ();
                            showModule = true;
                        }
                        else if (!IsOnProfilePage () || IsOnOwnProfilePage ()) {
                            ShowMyContent ();
                            showModule = true;
                        }
                    }

                    if (!showModule) {
                        HideModule ();
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        int? GetUserProfileId () => ParseHelper.ParseToNullable<int> (Request.QueryString ["userid"]);

        // TODO: Use profile page from portal settings?
        bool IsOnProfilePage () => GetUserProfileId () != null;

        bool IsOnOwnProfilePage () => IsOnProfilePage () && GetUserProfileId ().Value == UserId;

        void ShowMyContent ()
        {
            ShowContent (UserId);
        }

        void ShowContent (int userId)
        {
            var user = userId != UserId ? UserController.Instance.GetUser (PortalId, userId) : UserInfo;
            if (user.IsInAnyRole (Settings.RoleIds, true)) {
                var dataProvider = new UserHtmlDataProvider ();
                var userHtml = dataProvider.GetUserHtml (userId, ModuleId);
                litUserHtml.Text = ProcessUserHtml (user,
                    (userHtml != null && !string.IsNullOrEmpty (userHtml.UserHtml))
                        ? userHtml.UserHtml
                        : Settings.EmptyHtml
                );
            }
        }

        string ProcessUserHtml (UserInfo user, string html)
        {
            var tds = new CKEditorTemplateTokenDataSource (Settings.TemplatesFileId);
            var tokenReplace = new UserHtmlTokenReplace (PortalSettings, user, ModuleId);

            return HtmlStripper.StripTags (
                HttpUtility.HtmlDecode (tokenReplace.ReplaceEnvironmentTokens (tokenReplace.ReplaceCKEditorTemplateTokens (html, tds.Templates))),
                false, Settings.StripTags, ",;"
            );
        }

        void ShowEditPanel ()
        {
            pnlUser.Visible = true;
            TryRestoreState ();
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
            var searchText = txtSearchUser.Text.Trim ();
            btnSearchUser_Click_Internal (searchText);
        }

        void btnSearchUser_Click_Internal (string searchText)
        {
            var users = UserController.GetUsers (false, false, PortalId)
                                      .Cast<UserInfo> ()
                                      .WhereRoleIsAny (Settings.RoleIds, true)
                                      .Select (u => new UserInfoNameAdapter (u))
                                      .WhereNameContains (searchText)
                                      .Select (una => una.User);

            if (!users.IsNullOrEmpty ()) {
                pnlSelectUser.Visible = true;
                lblSearchResult.Text = string.Format (LocalizeString ("UsersFound_Format.Text"), users.Count (), searchText);
                selUser.DataSource = users.OrderBy (u => u.DisplayName)
                    .Select (u => new UserViewModel (u));
                selUser.DataBind ();
                selUser.SelectedIndex = 0;
                selUser_SelectedIndexChanged_Internal ();
            }
            else {
                pnlSelectUser.Visible = false;
                lblSearchResult.Text = LocalizeString ("NoUsersFound.Text");
            }
        }

        protected void selUser_SelectedIndexChanged (object sender, EventArgs e)
        {
            selUser_SelectedIndexChanged_Internal ();
            SessionUserId = ParseHelper.ParseToNullable<int> (selUser.SelectedValue, true);;
        }

        void selUser_SelectedIndexChanged_Internal ()
        {
            var userId = ParseHelper.ParseToNullable<int> (selUser.SelectedValue, true);
            if (userId != null) {
                lnkEditUserHtml.Visible = true;
                lnkEditUserHtml.NavigateUrl = EditUrl ("user_id", userId.ToString (), "Edit");

                ShowContent (userId.Value);
            }
            else {
                litUserHtml.Text = string.Empty;
                lnkEditUserHtml.Visible = false;
            }
        }
    }
}

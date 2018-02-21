//
// EditUserHtml.ascx.cs
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
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;
using R7.Dnn.Extensions.Utilities;
using R7.Dnn.UserHtml.Data;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml
{
    public class EditUserHtml : PortalModuleBase
    {
        #region Controls

        protected LinkButton buttonUpdate;
        protected LinkButton buttonDelete;
        protected HyperLink linkCancel;
        protected TextEditor textUserHtml;
        protected ModuleAuditControl ctlAudit;

        #endregion

        #region Handlers

        /// <summary>
        /// Handles Init event for a control.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            // set url for Cancel link
            linkCancel.NavigateUrl = Globals.NavigateURL ();

            // add confirmation dialog to delete button
            buttonDelete.Attributes.Add ("onClick", "javascript:return confirm('" + LocalizeString ("DeleteItem") + "');");
        }

        int? GetUserId ()
        {
            return TypeUtils.ParseToNullable<int> (Request.QueryString ["user_id"]);
        }

        /// <summary>
        /// Handles Load event for a control.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                var userId = GetUserId ();

                if (!IsPostBack) {
                    // load the data into the control the first time we hit this page

                    // check we have an item to lookup
                    // ALT: if (!Null.IsNull (itemId) 
                    if (userId != null) {
                        // load the item
                        var dataProvider = new UserHtmlDataProvider ();
                        var item = dataProvider.GetUserHtml (userId.Value, ModuleId);
                        if (item != null) {
                            
                            textUserHtml.Text = item.UserHtml;

                            // setup audit control
                            ctlAudit.CreatedByUser = UserController.Instance.GetUserById (PortalId, item.CreatedByUserId)?.DisplayName
                                ?? LocalizeString ("UnknownUser.Text");
                            ctlAudit.CreatedDate = item.CreatedOnDate.ToLongDateString ();
                        }
                        else {
                            buttonDelete.Visible = false;
                            ctlAudit.Visible = false;
                        }

                        var user = UserController.GetUserById (PortalId, userId.Value);
                        if (user != null) {
                            AppendToPageTitle (user.DisplayName);
                        }
                        else {
                            throw (new Exception ($"User with UserId={userId.Value} doesn't exists"));
                        }
                    }
                    else {
                        buttonDelete.Visible = false;
                        ctlAudit.Visible = false;
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        void AppendToPageTitle (string addition)
        {
            ((CDefault) Page).Title = ((CDefault) Page).Title.Append (addition, " &gt; ");
        }

        /// <summary>
        /// Handles Click event for Update button
        /// </summary>
        /// <param name='sender'>
        /// Sender.
        /// </param>
        /// <param name='e'>
        /// Event args.
        /// </param>
        protected void buttonUpdate_Click (object sender, EventArgs e)
        {
            try {
                var userId = GetUserId ();
                if (userId != null) {
                    var dataProvider = new UserHtmlDataProvider ();
                    var item = dataProvider.GetUserHtml (userId.Value, ModuleId);
                    var isNewItem = (item == null);

                    // determine if we are adding or updating
                    // ALT: if (Null.IsNull (itemId))
                    if (isNewItem) {
                        item = new UserHtmlInfo ();
                    }

                    // fill the object
                    item.UserHtml = textUserHtml.Text;
                    item.ModuleId = ModuleId;
                    item.UserId = userId.Value;

                    var now = DateTime.Now;
                    if (isNewItem) {
                        item.CreatedByUserId = UserId;
                        item.CreatedOnDate = now;
                    }
                    item.LastModifiedByUserId = UserId;
                    item.LastModifiedOnDate = now;

                    if (isNewItem) {
                        dataProvider.Add (item);
                    }
                    else {
                        dataProvider.Update (item);
                    }

                    ModuleController.SynchronizeModule (ModuleId);

                    // TODO: Add support for returnurl?
                    Response.Redirect (Globals.NavigateURL (), true);
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        /// <summary>
        /// Handles Click event for Delete button
        /// </summary>
        /// <param name='sender'>
        /// Sender.
        /// </param>
        /// <param name='e'>
        /// Event args.
        /// </param>
        protected void buttonDelete_Click (object sender, EventArgs e)
        {
            try {
                var userId = GetUserId ();
                if (userId != null) {
                    var dataProvider = new UserHtmlDataProvider ();
                    var item = dataProvider.GetUserHtml (userId.Value, ModuleId);
                    if (item != null) {
                        dataProvider.Delete (item);
                        Response.Redirect (Globals.NavigateURL (), true);
                    }
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #endregion
    }
}

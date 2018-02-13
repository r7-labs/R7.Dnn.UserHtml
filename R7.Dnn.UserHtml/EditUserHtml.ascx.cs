//
//  EditUserHtml.ascx.cs
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
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.UserControls;
using R7.Dnn.Extensions.Data;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml
{
    public class EditUserHtml : PortalModuleBase
    {
        #region Controls

        protected LinkButton buttonUpdate;
        protected LinkButton buttonDelete;
        protected HyperLink linkCancel;
        protected TextEditor txtContent;
        protected ModuleAuditControl ctlAudit;

        #endregion

        private int? itemId = null;

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
            buttonDelete.Attributes.Add ("onClick", "javascript:return confirm('" + Localization.GetString ("DeleteItem") + "');");
        }

        /// <summary>
        /// Handles Load event for a control.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                // parse querystring parameters
                int tmpItemId;
                if (int.TryParse (Request.QueryString ["UserHtmlId"], out tmpItemId))
                    itemId = tmpItemId;

                if (!IsPostBack) {
                    // load the data into the control the first time we hit this page

                    // check we have an item to lookup
                    // ALT: if (!Null.IsNull (itemId) 
                    if (itemId.HasValue) {
                        // load the item
                        var dataProvider = new Dal2DataProvider ();
                        var item = dataProvider.Get<UserHtmlInfo> (itemId.Value, ModuleId);

                        if (item != null) {
                            // TODO: Fill controls with data
                            txtContent.Text = item.Content;

                            // setup audit control
                            ctlAudit.CreatedByUser = item.CreatedByUserName;
                            ctlAudit.CreatedDate = item.CreatedOnDate.ToLongDateString ();
                        } else {
                            Response.Redirect (Globals.NavigateURL (), true);
                        }
                    } else {
                        buttonDelete.Visible = false;
                        ctlAudit.Visible = false;
                    }
                }
            } catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
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
                var dataProvider = new Dal2DataProvider ();
                UserHtmlInfo item;

                // determine if we are adding or updating
                // ALT: if (Null.IsNull (itemId))
                if (!itemId.HasValue) {
                    // TODO: populate new object properties with data from controls 
                    // to add new record
                    item = new UserHtmlInfo ();
                } else {
                    // TODO: update properties of existing object with data from controls 
                    // to update existing record
                    item = dataProvider.Get<UserHtmlInfo> (itemId.Value, ModuleId);
                }

                // fill the object
                item.Content = txtContent.Text;
                item.ModuleId = ModuleId;

                if (!itemId.HasValue) {
                    item.CreatedByUserId = UserId;
                    dataProvider.Add<UserHtmlInfo> (item);
                } else {
                    dataProvider.Update<UserHtmlInfo> (item);
                }

                ModuleController.SynchronizeModule (ModuleId);
                Response.Redirect (Globals.NavigateURL (), true);

            } catch (Exception ex) {
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
                if (itemId.HasValue) {
                    var dataProvider = new Dal2DataProvider ();
                    dataProvider.Delete<UserHtmlInfo> (itemId.Value);
                    Response.Redirect (Globals.NavigateURL (), true);
                }
            } catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #endregion
    }
}

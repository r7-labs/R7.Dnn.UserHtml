//
//  UserHtmlSettings.cs
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

using DotNetNuke.Entities.Modules.Settings;

namespace R7.Dnn.UserHtml.Models
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class UserHtmlSettings
    {
        /// <summary>
        /// Template used to render the module content
        /// </summary>
        [TabModuleSetting (Prefix = "UserHtml_")]
        public string Template { get; set; } = "<em>[CREATEDONDATE]</em> <strong>[CREATEDBYUSERNAME]:</strong><p>[CONTENT]</p>";
    }
}

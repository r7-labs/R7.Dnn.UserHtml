//
// UserFilters.cs
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

using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using R7.Dnn.Extensions.Utilities;

namespace R7.Dnn.UserHtml.Models
{
    public static class UserFilters
    {
        public static IEnumerable<UserInfo> WhereNameContains (this IEnumerable<UserInfo> users, string searchText)
        {
            var searchTextLC = searchText.ToLower ();

            return users.Where (u => Contains (u.Email, searchTextLC) ||
                                Contains (u.Username, searchTextLC) ||
                                Contains (u.DisplayName, searchTextLC) ||
                                Contains (u.LastName, searchTextLC) ||
                                Contains (u.FirstName, searchTextLC) ||
                                Contains (TextUtils.FormatList (" ", u.FirstName, u.LastName),
                                          searchTextLC) ||
                                Contains (TextUtils.FormatList (" ", u.LastName, u.FirstName),
                                      searchTextLC));
        }

        public static IEnumerable<UserInfo> WhereRoleIsAny (this IEnumerable<UserInfo> users, IEnumerable<int> roleIds, bool defaultAll)
        {
            return users.Where (u => u.IsInAnyRole (roleIds, defaultAll));
        }

        public static bool IsInAnyRole (this UserInfo user, IEnumerable<int> roleIds, bool defaultAll)
        {
            if (roleIds.IsNullOrEmpty ()) {
                return defaultAll;
            }

            return !RoleController.Instance.GetUserRoles (user, true)
                                  .Join (roleIds, ur => ur.RoleID, roleId => roleId, (ur, roleId) => roleId)
                                  .IsNullOrEmpty ();
        }

        static bool Contains (string text, string searchTextLC)
        {
            return text != null && text.ToLower ().Contains (searchTextLC);
        }
    }
}

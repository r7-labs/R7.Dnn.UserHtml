using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using R7.Dnn.Extensions.Utilities;

namespace R7.Dnn.UserHtml.Models
{
    public static class UserFilters
    {
        public static IEnumerable<UserInfoNameAdapter> WhereNameContains (this IEnumerable<UserInfoNameAdapter> users, string searchText)
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

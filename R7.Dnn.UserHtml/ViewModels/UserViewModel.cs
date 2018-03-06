using System;
using DotNetNuke.Entities.Users;

namespace R7.Dnn.UserHtml.ViewModels
{
    public class UserViewModel
    {
        protected readonly UserInfo User;

        public UserViewModel (UserInfo user)
        {
            User = user;
        }

        public int UserId => User.UserID;

        public string UserString =>
            User.Username.Equals (User.Email, StringComparison.CurrentCultureIgnoreCase)
                ? $"{User.DisplayName} / {User.Email}"
                    : $"{User.DisplayName} ({User.Username}) / {User.Email}";
    }
}

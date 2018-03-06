using DotNetNuke.Entities.Users;

namespace R7.Dnn.UserHtml.Models
{
    /// <summary>
    /// Provides subset of UserInfo properties to make <see cref="UserFilters.WhereNameContains" /> testable.
    /// </summary>
    public class UserInfoNameAdapter
    {
        public UserInfo User { get; protected set; }

        string _firstName;
        public string FirstName {
            get { return User != null ? User.FirstName : _firstName; }
            set { _firstName = value; }
        }

        string _lastName;
        public string LastName {
            get { return User != null ? User.LastName : _lastName; }
            set { _lastName = value; }
        }

        string _displayName;
        public string DisplayName {
            get { return User != null ? User.DisplayName : _displayName; }
            set { _displayName = value; }
        }

        string _username;
        public string Username {
            get { return User != null ? User.Username : _username; }
            set { _username = value; }
        }

        string _email;
        public string Email {
            get { return User != null ? User.Email : _email; }
            set { _email = value; }
        }

        public UserInfoNameAdapter ()
        {}

        public UserInfoNameAdapter (UserInfo user)
        {
            User = user;
        }
    }
}

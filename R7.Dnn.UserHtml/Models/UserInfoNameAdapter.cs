//
// UserInfoNameAdapter.cs
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

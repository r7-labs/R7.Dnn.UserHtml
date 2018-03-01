//
// UserFiltersTests.cs
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
using R7.Dnn.UserHtml.Models;
using Xunit;

namespace R7.Dnn.UserHtml.Tests.Models
{
    public class UserFiltersTests
    {
        [Fact]
        public void WhereNameContainsTest ()
        {
            var users = new List<UserInfoNameAdapter> {
                new UserInfoNameAdapter {
                    Username = "hj_simpson",
                    FirstName = "Homer",
                    LastName = "Simpson",
                    DisplayName = "Homer Jay Simpson",
                    Email = "homer@simpsons.net"
                },
                new UserInfoNameAdapter {
                    Username = "mj_simpson",
                    FirstName = "Marjorie",
                    LastName = "Simpson",
                    DisplayName = "Marjorie Jacqueline Simpson",
                    Email = "marge@simpsons.net"
                },
                new UserInfoNameAdapter {
                    Username = "bj_simpson",
                    FirstName = "Bartholomew",
                    LastName = "Simpson",
                    DisplayName = "Bartholomew Jo-Jo Simpson",
                    Email = "bart@simpsons.net"
                },
                new UserInfoNameAdapter {
                    Username = "lm_simpson",
                    FirstName = "Lisa",
                    LastName = "Simpson",
                    DisplayName = "Lisa Marie Simpson",
                    Email = "lisa@simpsons.net"
                },
                new UserInfoNameAdapter {
                    Username = "me_simpson",
                    FirstName = "Margaret",
                    LastName = "Simpson",
                    DisplayName = "Margaret Evelyn Simpson",
                    Email = "maggie@simpsons.net"
                }
            };

            Assert.Equal (1, users.WhereNameContains ("Homer").Count ());
            Assert.Equal (1, users.WhereNameContains ("Simpson Homer").Count ());
            Assert.Equal (1, users.WhereNameContains ("Homer Jay").Count ());
            Assert.Equal (1, users.WhereNameContains ("homer@").Count ());
            Assert.Equal (5, users.WhereNameContains ("Simpson").Count ());
            Assert.Equal (3, users.WhereNameContains ("j_s").Count ());
            Assert.Equal (3, users.WhereNameContains ("Mar").Count ());
            Assert.Equal (0, users.WhereNameContains ("Moe").Count ());
        }
    }
}

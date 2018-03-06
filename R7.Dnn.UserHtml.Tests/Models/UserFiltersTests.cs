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

using R7.Dnn.Extensions.Data;
using R7.Dnn.UserHtml.Models;

namespace R7.Dnn.UserHtml.Data
{
    public class UserHtmlDataProvider: Dal2DataProvider
    {
        public UserHtmlInfo GetUserHtml (int userId, int moduleId)
        {
            return Get<UserHtmlInfo> ("WHERE UserID=@0 AND ModuleID=@1", userId, moduleId);
        }
    }
}

using System.Collections;
using System.Globalization;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Tokens;

namespace R7.Dnn.UserHtml.Components
{
    public class UserHtmlTokenReplace: TokenReplace
    {
        public UserHtmlTokenReplace (PortalSettings portalSettings, UserInfo user, int moduleId):
            base (Scope.DefaultSettings, CultureInfo.CurrentCulture.IetfLanguageTag, portalSettings, user, moduleId)
        {
            #if DEBUG

            DebugMessages = true;

            #endif
        }

        public string ReplaceCKEditorTemplateTokens (string sourceText, IDictionary templateTokens, int times = 1, bool final = true)
        {
            var text = sourceText;

            if (final) {
                text = text.Replace ("[Final]", "[");
                text = text.Replace ("[/Final]", "]");
            }

            for (var i = 0; i < times; i++) {
                text = ReplaceEnvironmentTokens (text, templateTokens, "CKEditor");
            }

            return text;
        }
    }
}

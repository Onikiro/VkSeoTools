using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace VkInstruments.MVC.Auth
{
    public class AuthorizationVk
    {
        /// <summary>
        /// Менеджер версий VkApi
        /// </summary>
        private IVkApiVersionManager _versionManager { get; }

        public IApiAuthParams AuthParams { get; private set; }

        public AuthorizationVk(IVkApiVersionManager versionManager)
        {
            _versionManager = versionManager;
        }

        public void SetAuthParams(IApiAuthParams authParams)
        {
            AuthParams = authParams;
        }

        public Uri CreateAuthorizeUrl(ulong clientId, ulong scope, Display display, string state)
        {
            var builder = new StringBuilder("https://oauth.vk.com/authorize?");

            builder.Append($"client_id={clientId}&");
            builder.Append($"redirect_uri=YourDomain/Authorization/Complete&");
            builder.Append($"display={display}&");
            builder.Append($"scope={scope}&");
            builder.Append("response_type=token&");
            builder.Append($"v={_versionManager.Version}&");
            builder.Append($"state={state}&");
            builder.Append("revoke=1");

            return new Uri(builder.ToString());
        }

        public HttpCookie GetCookieByQueryTokenString(NameValueCollection queryString)
        {
            var token = queryString["access_token"];
            var expiresIn = queryString["expires_in"];
            var userId = queryString["user_id"];

            if (!int.TryParse(expiresIn, out var expiresIni) || !long.TryParse(userId, out var userIdl))
            {
                return null;
            }

            return new HttpCookie("token")
            {
                ["token"] = token,
                ["userId"] = userIdl.ToString(),
                Expires = DateTime.Now.AddSeconds(expiresIni)
            };
        }
    }
}

using System;
using System.Text;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace VkInstruments.WebApp.Auth
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
#if DEBUG
            builder.Append($"redirect_uri={AppProperties.TEST_DOMAIN}/Authorization/Complete&");
#else
            builder.Append($"redirect_uri={AppProperties.PROD_DOMAIN}/Authorization/Complete&");
#endif
            builder.Append($"display={display}&");
            builder.Append($"scope={scope}&");
            builder.Append("response_type=token&");
            builder.Append($"v={_versionManager.Version}&");
            builder.Append($"state={state}&");
            builder.Append("revoke=1");

            return new Uri(builder.ToString());
        }
    }
}

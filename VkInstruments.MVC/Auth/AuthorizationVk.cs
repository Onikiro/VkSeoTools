using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Utils;

namespace VkInstruments.MVC.Auth
{
    public class AuthorizationVk : IBrowser
    {
        /// <summary>
        /// Менеджер версий VkApi
        /// </summary>
        private readonly IVkApiVersionManager _versionManager;

        public IApiAuthParams AuthParams { get; private set; }

        public AuthorizationVk(IVkApiVersionManager versionManager)
        {
            _versionManager = versionManager;
        }

        public void SetAuthParams(IApiAuthParams authParams)
        {
            AuthParams = authParams;
        }

        public IWebProxy Proxy { get; set; }

        public Uri CreateAuthorizeUrl(ulong clientId, ulong scope, Display display, string state)
        {
            var builder = new StringBuilder("https://oauth.vk.com/authorize?");

            builder.Append($"client_id={clientId}&");
            builder.Append($"redirect_uri={HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/Authorization/Complete&");
            builder.Append($"display={display}&");
            builder.Append($"scope={scope}&");
            builder.Append("response_type=token&");
            builder.Append($"v={_versionManager.Version}&");
            builder.Append($"state={state}&");
            builder.Append("revoke=1");

            return new Uri(builder.ToString());
        }

        public string GetJson(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public AuthorizationResult Authorize()
        {
            throw new NotImplementedException();
        }

        public AuthorizationResult Validate(string validateUrl, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Utils;

namespace VkLikesParserMVC.Auth
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

        public string GetJson(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public void SetAuthParams(IApiAuthParams authParams)
        {
            AuthParams = authParams;
        }

        public IWebProxy Proxy { get; set; }

        public AuthorizationResult Authorize()
        {
            var authResult = new AuthorizationResult();
            var vk = new HttpClient();

            vk.DefaultRequestHeaders.Add("Connection", "close");

            var url = CreateAuthorizeUrl(AuthParams.ApplicationId, AuthParams.Settings.ToUInt64(), Display.Page, "123456");

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Host = "oauth.vk.com";
            request.UserAgent = "qwerty";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = false;

            using (var responsevk = (HttpWebResponse)request.GetResponse())
            {
                var result = VkAuthorization.From(responsevk.ResponseUri.AbsoluteUri);
                if (result.IsAuthorized)
                {
                    authResult = new AuthorizationResult
                    {
                        AccessToken = result.AccessToken,
                        ExpiresIn = result.ExpiresIn,
                        UserId = result.UserId,
                        State = result.State
                    };
                }
            }

            return authResult;
        }

        public Uri CreateAuthorizeUrl(ulong clientId, ulong scope, Display display, string state)
        {
            var builder = new StringBuilder("https://oauth.vk.com/authorize?");

            builder.Append($"client_id={clientId}&");
            builder.Append("redirect_uri=http://089828a0.ngrok.io/Authorization/Complete&");
            builder.Append($"display={display}&");
            builder.Append($"scope={scope}&");
            builder.Append("response_type=token&");
            builder.Append($"v={_versionManager.Version}&");
            builder.Append($"state={state}&");
            builder.Append("revoke=1");

            return new Uri(builder.ToString());
        }

        public AuthorizationResult Validate(string validateUrl, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}

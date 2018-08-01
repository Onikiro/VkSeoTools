using System;
using System.Web;
using System.Web.Mvc;
using VkLikesParserMVC.Auth;
using VkLikesParserMVC.Models;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;

namespace VkLikesParserMVC.Controllers
{
    public class AuthorizationController : Controller
    {

        private readonly VkSystem _vk = VkSystem.GetInstance();
        public bool IsAuthorized => _vk.Vk.IsAuthorized;

        public ActionResult Complete()
        {
            return View();
        }

        public ActionResult ReceiveToken()
        {
            var token = Request.QueryString["access_token"];
            var expiresIn = Request.QueryString["expires_in"];
            var userId = Request.QueryString["user_id"];

            if (!int.TryParse(expiresIn, out var expiresIni) || !long.TryParse(userId, out var userIdl))
            {
                return Redirect("~/Home/Parser");
            }

            SetTokenCookies(token, expiresIni, userIdl);
            _vk.Vk.Authorize(new ApiAuthParams
            {
                AccessToken = token,
                TokenExpireTime = expiresIni,
                UserId = userIdl
            });

            return Redirect("~/Home/Parser");
        }

        private void SetTokenCookies(string tokenValue, int expireTime, long userId)
        {
            var cookie = new HttpCookie("token", tokenValue)
            {
                ["userId"] = userId.ToString(),
                Expires = DateTime.Now.AddSeconds(expireTime)
            };

            Response.Cookies.Add(cookie);
        }

        public ActionResult Start()
        {
            var vkAuth = new AuthorizationVk(_vk.Vk.VkApiVersion);
            vkAuth.SetAuthParams(_vk.GetParams());
            var authUri = vkAuth.CreateAuthorizeUrl(vkAuth.AuthParams.ApplicationId, vkAuth.AuthParams.Settings.ToUInt64(), Display.Page, "123456");
            return Redirect(authUri.AbsoluteUri);
        }
    }
}
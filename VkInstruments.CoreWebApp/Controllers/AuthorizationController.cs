using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VkInstruments.Core.VkSystem;
using VkInstruments.CoreWebApp.Auth;
using VkNet.Enums.SafetyEnums;

namespace VkInstruments.CoreWebApp.Controllers
{
    //TODO: добавить identity для проверки авторизации
    //[AllowAnonymous]
    public class AuthorizationController : Controller
    {
        private readonly IVkSystem _vk;
        private readonly AuthorizationVk _vkAuth;
        private static string _returnUrl;

        public AuthorizationController(IVkSystem vk)
        {
            _vk = vk;
            _vkAuth = new AuthorizationVk(_vk.Vk.VkApiVersion);
            _vkAuth.SetAuthParams(_vk.GetParams(0123456));
        }

        public IActionResult Complete()
        {
            return View();
        }

        public IActionResult ReceiveToken()
        {
            var token = Request.Query["access_token"];
            var expireTime = Request.Query["expires_in"];
            var userId = Request.Query["user_id"];

            _vk.Auth(token, expireTime, userId);
            if (_vk.Vk.IsAuthorized)
            {
                if (!long.TryParse(expireTime, out var expireSeconds))
                {
                    return RedirectToAction("Parser", "Home");
                }

                var cookie = new CookieOptions
                {
                    Expires = DateTime.Now.AddSeconds(expireSeconds)
                };

                Response.Cookies.Append("token", token, cookie);
                Response.Cookies.Append("expireTime", expireTime, cookie);
                Response.Cookies.Append("userId", userId, cookie);

                //FormsAuthentication.SetAuthCookie(_vk.Vk.UserId.ToString(), false);

                if (string.IsNullOrEmpty(_returnUrl)) return RedirectToAction("Parser", "Home");

                return new RedirectResult(_returnUrl);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Start(string returnUrl)
        {
            _returnUrl = returnUrl;
            var authUri = _vkAuth.CreateAuthorizeUrl(_vkAuth.AuthParams.ApplicationId,
                _vkAuth.AuthParams.Settings.ToUInt64(), Display.Page, "123456");
            return Redirect(authUri.AbsoluteUri);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
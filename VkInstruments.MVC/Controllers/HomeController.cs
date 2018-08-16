using System;
using System.Web;
using System.Web.Mvc;
using VkLikesParserMVC.Models;
using VkNet.Model;

namespace VkLikesParserMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly VkSystem _vk = VkSystem.GetInstance();

        public ActionResult Parser()
        {
            ReauthorizeVkSystem();
            return View();
        }

        private void ReauthorizeVkSystem()
        {
            var token = ReadTokenCookies();

            if (token == null) return;
            if (!long.TryParse(token["userId"], out var userId)) return;

            var expireTime = (int)token.Expires.Subtract(DateTime.Now).TotalSeconds;

            _vk.Vk.Authorize(new ApiAuthParams
            {
                AccessToken = token["token"],
                TokenExpireTime = expireTime,
                UserId = userId
            });
        }

        private HttpCookie ReadTokenCookies()
        {
            return Request.Cookies["token"];
        }

        public ActionResult ParserResult()
        {
            ReauthorizeVkSystem();
            return View();
        }
    }
}
using System;
using System.Web;
using System.Web.Mvc;
using VkInstruments.MVC.Models;

namespace VkInstruments.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly VkSystem _vk = new VkSystem();

        public ActionResult Parser()
        {
            return View();
        }

        private void ReauthorizeVkSystem()
        {
            var token = ReadTokenCookies();

            if (token == null) return;

            if (!long.TryParse(token["userId"], out var userId)) return;

            var expireTime = (int)token.Expires.Subtract(DateTime.Now).TotalSeconds;
            _vk.Auth(token["token"], expireTime, userId);
        }

        private HttpCookie ReadTokenCookies()
        {
            return Request.Cookies["token"];
        }

        [HttpPost]
        public ActionResult ParserResult(string postLink)
        {
            ReauthorizeVkSystem();
            var likeIds = Core.Parser.GetLikes(_vk.Vk, postLink);
            @ViewBag.LikeIds = likeIds;
            return View();
        }
    }
}
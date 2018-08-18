using System;
using System.Web;
using System.Web.Mvc;
using VkSystem = VkInstruments.MVC.Models.VkSystem;

namespace VkInstruments.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly VkSystem _vk = Core.VkSystem.VkSystem.GetInstance() as VkSystem;

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
            _vk.Auth(token["token"], expireTime, userId);
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
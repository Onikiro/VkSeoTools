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
			var cookies = ReadTokenCookies();
			_vk.Auth(cookies);
		}

		private HttpCookie ReadTokenCookies()
		{
			return Request.Cookies["token"];
		}

		[HttpPost]
		public ActionResult ParserResult(string postLink)
		{
			ReauthorizeVkSystem();
			@ViewBag.LikeIds = Core.Parser.GetLikes(_vk.Vk, postLink);
			return View();
		}
	}
}
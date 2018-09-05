using System.Web;
using System.Web.Mvc;
using VkInstruments.Core;
using VkInstruments.Core.VkSystem;
using VkInstruments.MVC.Utils;
using VkNet.Model.RequestParams;

namespace VkInstruments.MVC.Controllers
{
    [Authorize]
	public class HomeController : Controller
	{
	    private readonly IVkService _vkService;

	    public HomeController(IVkSystem vk, IVkService vkService)
	    {
	        _vkService = vkService;
	    }

		private void ReauthorizeVkSystem()
		{
			var cookies = ReadTokenCookies();
		    _vkService.Auth(cookies);
		}

		private HttpCookie ReadTokenCookies()
		{
			return Request.Cookies["token"];
		}

        public ActionResult Parser()
        {
            ReauthorizeVkSystem();

            return View();
        }

        [HttpPost]
		public ActionResult ParserResult(string postLink)
		{
			ReauthorizeVkSystem();

            var model = _vkService.ParseLikesFromPost(postLink);

			return View(model);
		}

        public ActionResult Filter(string userIds)
        {
            ReauthorizeVkSystem();

            ViewBag.UserIds = userIds;
            ViewBag.Countries = _vkService.GetCountries().ToSelectList();

            return View();
        }

        [HttpPost]
        public ActionResult FilterResult(string userIds, UserSearchParams @params)
        {
            ReauthorizeVkSystem();

            var model = _vkService.FilterIds(userIds, @params);

            return View(model);
        }

        public ActionResult CityPartial(int countryId)
        {
            ReauthorizeVkSystem();

            var model = _vkService.GetCities(countryId);

            return PartialView(model);
        }
    }
}
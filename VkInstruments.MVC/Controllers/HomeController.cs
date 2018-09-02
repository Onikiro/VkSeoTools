using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkInstruments.Core;
using VkInstruments.MVC.Models;
using VkInstruments.MVC.Utils;
using VkNet.Model.RequestParams;

namespace VkInstruments.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly VkSystem _vk = new VkSystem();

		private void ReauthorizeVkSystem()
		{
			var cookies = ReadTokenCookies();
			_vk.Auth(cookies);
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
		    //TODO: IoC container, static just for test
            var model = ServiceFacade.ParseLikesFromPosts(_vk, postLink);

			return View(model);
		}

        public ActionResult Filter(string userIds)
        {
            ReauthorizeVkSystem();
            //TODO: Exclude to ServiceFacade or model if you can
            var model = !string.IsNullOrEmpty(userIds)
                ? userIds.Replace("vk.com/id", "")
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList()
                : new List<string>();

            ViewBag.Age = Enumerable.Range(1, 100).ToSelectList();
            ViewBag.Days = Enumerable.Range(1, 31).ToSelectList();
            ViewBag.Months = Enumerable.Range(1, 12)
                .ToDictionary(x => x, x => DateTimeFormatInfo.CurrentInfo?.GetMonthName(x))
                .ToSelectList();
            ViewBag.Countries = (_vk.Vk.IsAuthorized
                ? _vk.Vk.Database.GetCountries().ToDictionary(x => x.Id, x => x.Title)
                : new Dictionary<long?, string>())
                .ToSelectList();

            return View(model);
        }

        [HttpPost]
        public ActionResult FilterResult(string userIds, UserSearchParams @params)
        {
            ReauthorizeVkSystem();
            //TODO: IoC container, static just for test
            var model = ServiceFacade.FilterIds(_vk, userIds, @params);

            return View(model);
        }

        public ActionResult CityPartial(int countryId)
        {
            ReauthorizeVkSystem();
            //TODO: IoC container, static just for test
            var model = ServiceFacade.GetCities(_vk, countryId);

            return PartialView(model);
        }
    }
}
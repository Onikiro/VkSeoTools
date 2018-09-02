using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkInstruments.Core;
using VkInstruments.MVC.Models;
using VkNet.Model.RequestParams;
using VkNet.Model.RequestParams.Database;

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

			var model = Core.Parser.GetLikes(_vk.Vk, postLink);

			return View(model);
		}

        public ActionResult Filter(string userIds)
        {
            ReauthorizeVkSystem();

            var model = !string.IsNullOrEmpty(userIds)
                ? userIds.Replace("vk.com/id", "")
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList()
                : new List<string>();

            ViewBag.Age = Enumerable.Range(1, 100).ToSelectList();
            ViewBag.Days = Enumerable.Range(1, 31).ToSelectList();
            ViewBag.Months = Enumerable.Range(1, 12)
                .ToDictionary(x => x, x => DateTimeFormatInfo.CurrentInfo.GetMonthName(x))
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

            var userNames = userIds.Replace("vk.com/id", "")
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var users = _vk.Vk.Users.Get(userNames, UserFilter.GetProfileFields(@params));
            var model = UserFilter.ApplyFilter(users, @params);

            return View(model);
        }

        public ActionResult CityPartial(int countryId)
        {
            ReauthorizeVkSystem();

            var model = _vk.Vk.Database
                .GetCities(new GetCitiesParams { CountryId = countryId })
                .ToDictionary(x => x.Id, x => x.Title);

            return PartialView(model);
        }
    }
}
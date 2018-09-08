using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkInstruments.Core;
using VkInstruments.Core.VkSystem;
using VkInstruments.CoreWebApp.Utils;
using VkNet.Model.RequestParams;

namespace VkInstruments.CoreWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IVkSystem _vk;
        private readonly IVkService _vkService;

        public HomeController(IVkSystem vk, IVkService vkService)
        {
            _vk = vk;
            _vkService = vkService;
        }

        private void ReauthorizeVkSystem()
        {
            _vk.Auth(Request.Cookies["token"], Request.Cookies["userId"], Request.Cookies["expireTime"]);
        }

        public IActionResult Parser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ParserResult(string postLink)
        {
            ReauthorizeVkSystem();

            var model = _vkService.ParseLikesFromPost(postLink);

            return View(model);
        }

        public IActionResult Filter(string userIds)
        {
            ReauthorizeVkSystem();

            ViewBag.UserIds = userIds;
            ViewBag.Countries = _vkService.GetCountries().ToSelectList();

            return View();
        }

        [HttpPost]
        public IActionResult FilterResult(string userIds, UserSearchParams @params)
        {
            ReauthorizeVkSystem();

            var model = _vkService.FilterIds(userIds, @params);

            return View(model);
        }

        public IActionResult CityPartial(int countryId)
        {
            ReauthorizeVkSystem();

            var model = _vkService.GetCities(countryId);

            return PartialView(model);
        }
    }
}

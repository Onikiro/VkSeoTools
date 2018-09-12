using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkInstruments.Core;
using VkInstruments.CoreWebApp.Utils;
using VkNet.Model.RequestParams;

namespace VkInstruments.CoreWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IVkService _vkService;

        public HomeController(IVkService vkService)
        {
            _vkService = vkService;
        }

        public IActionResult Parser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ParserResult(string postLink)
        {
            var model = await _vkService.ParseLikesFromPost(postLink);

            return View(model);
        }

        public async Task<IActionResult> Filter(string userIds)
        {
            ViewBag.UserIds = userIds;
            ViewBag.Countries = (await _vkService.GetCountries()).ToSelectList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FilterResult(string userIds, UserSearchParams @params)
        {
            var model = await _vkService.FilterIds(userIds, @params);

            return View(model);
        }

        public async Task<IActionResult> CityPartial(int countryId)
        {
            var model = await Task.Run(() => _vkService.GetCities(countryId));

            return PartialView(model);
        }
    }
}

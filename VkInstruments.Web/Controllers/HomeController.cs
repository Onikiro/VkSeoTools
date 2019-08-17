using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VkInstruments.Core;
using VkInstruments.Web.Utils;
using VkInstruments.Web.ViewModels;
using VkNet.Model.RequestParams;

namespace VkInstruments.Web.Controllers
{
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
            var model = await _vkService.ParseLikedUserIdsFromPost(postLink);

            return View(model);
        }

        public async Task<IActionResult> Filter(string userIds)
        {
            var model = new UserFilterViewModel
            {
                UserIds = userIds,
                Countries = (await _vkService.GetCountries()).ToSelectList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FilterResult(string userIds, UserSearchParams searchParams)
        {
            var model = await _vkService.FilterIds(userIds, searchParams);

            return View(model);
        }

        public async Task<IActionResult> CityPartial(int countryId)
        {
            var model = (await _vkService.GetCities(countryId)).ToSelectList();

            return PartialView(model);
        }
    }
}

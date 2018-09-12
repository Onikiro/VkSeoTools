using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkInstruments.Core.VkSystem;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.RequestParams.Database;

namespace VkInstruments.Core
{
    public class VkService : IVkService
    {
        private readonly IVkSystem _vk;

        public VkService(IVkSystem vk)
        {
            _vk = vk;
        }

		public async Task<IEnumerable<long>> ParseLikesFromPost(string input)
		{
            var postLinks = input.Split('\n');
            var likeIds = new List<long>();

            if (postLinks.Length > 0)
            {
                foreach (var link in postLinks)
                {
                    likeIds.AddRange(await Parser.GetLikes(_vk.Vk, link));
                }
            }

            return likeIds;
        }

		public async Task<IEnumerable<User>> FilterIds(string userIds, UserSearchParams searchParams)
        {
            var userNames = userIds.Replace("vk.com/id", "")
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var users = await _vk.Vk.Users.GetAsync(userNames, UserFilter.GetProfileFields(searchParams));

            return UserFilter.ApplyFilter(users, searchParams);
        }

        public async Task<Dictionary<long?, string>> GetCountries(bool? needAll = null)
        {
            var result = await _vk.Vk.Database.GetCountriesAsync(needAll);

            return result.ToDictionary(x => x.Id, x => x.Title);
        }

        public async Task<Dictionary<long?, string>> GetCities(int countryId)
        {
            var result = await _vk.Vk.Database
                .GetCitiesAsync(new GetCitiesParams {CountryId = countryId});

            return result.ToDictionary(x => x.Id, x => x.Title);
        }
    }
}
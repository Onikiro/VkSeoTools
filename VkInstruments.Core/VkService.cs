using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.RequestParams.Database;

namespace VkInstruments.Core
{
    public class VkService : IVkService
    {
        private readonly IVkApi _vk;

        public VkService(IVkApi vk, string serviceToken)
        {
            _vk = vk;
            _vk.Authorize(new ApiAuthParams { AccessToken = serviceToken });
        }

        public async Task<List<long>> ParseLikedUserIdsFromPost(string input)
        {
            var parser = new Parser();

            var postLinks = input.Split('\n');
            var likeIds = new List<long>();

            if (postLinks.Length > 0)
            {
                foreach (var link in postLinks)
                {
                    likeIds.AddRange(await parser.GetLikes(_vk.Likes, link));
                }
            }

            return likeIds;
        }

        public async Task<List<User>> FilterIds(string userIds, UserSearchParams searchParams)
        {
            var userNames = userIds.Replace("vk.com/id", "")
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var users = await _vk.Users.GetAsync(userNames, UserFilter.GetProfileFields(searchParams));

            return UserFilter.ApplyFilter(users, searchParams).ToList();
        }

        public async Task<Dictionary<long, string>> GetCountries(bool? needAll = null)
        {
            var result = await _vk.Database.GetCountriesAsync(needAll);

            return result.Where(x => x.Id.HasValue).ToDictionary(x => x.Id.Value, x => x.Title);
        }

        public async Task<Dictionary<long, string>> GetCities(int countryId)
        {
            var result = await _vk.Database
                .GetCitiesAsync(new GetCitiesParams { CountryId = countryId });

            return result.Where(x => x.Id.HasValue).ToDictionary(x => x.Id.Value, x => x.Title);
        }
    }
}
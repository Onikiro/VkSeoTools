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

        public async Task<List<long>> ParseLikedUserIdsFromPost(string input)
        {
            var parser = new Parser();

            var postLinks = input.Split('\n');
            var likeIds = new List<long>();

            if (postLinks.Length > 0)
            {
                foreach (var link in postLinks)
                {
                    likeIds.AddRange(await parser.GetLikes(_vk.Vk.Likes, link));
                }
            }

            return likeIds;
        }

        public async Task<List<User>> FilterIds(string userIds, UserSearchParams searchParams)
        {
            var userNames = userIds.Replace("vk.com/id", "")
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var users = await _vk.Vk.Users.GetAsync(userNames, UserFilter.GetProfileFields(searchParams));

            return UserFilter.ApplyFilter(users, searchParams).ToList();
        }

        public async Task<Dictionary<long, string>> GetCountries(bool? needAll = null)
        {
            var result = await _vk.Vk.Database.GetCountriesAsync(needAll);

            return result.Where(x => x.Id.HasValue).ToDictionary(x => x.Id.Value, x => x.Title);
        }

        public async Task<Dictionary<long, string>> GetCities(int countryId)
        {
            var result = await _vk.Vk.Database
                .GetCitiesAsync(new GetCitiesParams { CountryId = countryId });

            return result.Where(x => x.Id.HasValue).ToDictionary(x => x.Id.Value, x => x.Title);
        }

        public async Task<List<User>> FilterIdsByPostLinkAsync(string input, UserSearchParams @params)
        {
            var ids = await ParseLikedUserIdsFromPost(input);
            var result = await _vk.Vk.Users.GetAsync(ids, UserFilter.GetProfileFields(@params));
            return UserFilter.ApplyFilter(result, @params).ToList();
        }
    }
}
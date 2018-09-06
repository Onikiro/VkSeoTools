using System;
using System.Collections.Generic;
using System.Linq;
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

		public IEnumerable<long> ParseLikesFromPost(string input)
		{
			var postLinks = input.Split('\n');
			var likeIds = new List<long>();

			if (postLinks.Count() > 0)
			{
				foreach (var link in postLinks)
				{
					likeIds.AddRange(Parser.GetLikes(_vk.Vk, link));
				}
			}

			return likeIds;
		}

		public IEnumerable<User> FilterIds(string userIds, UserSearchParams @params)
        {
            var userNames = userIds.Replace("vk.com/id", "")
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var users = _vk.Vk.Users.Get(userNames, UserFilter.GetProfileFields(@params));
            var filteredUsers = UserFilter.ApplyFilter(users, @params);
            return filteredUsers;
        }

        public Dictionary<long?, string> GetCountries(bool? needAll = null)
        {
            return _vk.Vk.Database
                .GetCountries(needAll)
                .ToDictionary(x => x.Id, x => x.Title);
        }

        public Dictionary<long?, string> GetCities(int countryId)
        {
            return _vk.Vk.Database
                .GetCities(new GetCitiesParams { CountryId = countryId })
                .ToDictionary(x => x.Id, x => x.Title);
        }
    }
}
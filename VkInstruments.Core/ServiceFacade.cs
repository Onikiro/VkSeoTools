using System;
using System.Collections.Generic;
using System.Linq;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.RequestParams.Database;

namespace VkInstruments.Core
{
    public class ServiceFacade
    {
        //TODO: IoC container, static just for test
        public static IEnumerable<long> ParseLikesFromPosts(VkSystem.VkSystem vk, string postLink)
        {
            return Parser.GetLikes(vk.Vk, postLink);
        }

        public static IEnumerable<long> ParseLikesFromPosts(VkSystem.VkSystem vk, ICollection<string> postLinkColletion)
        {
            var postLinks = postLinkColletion.ToList();
            var likeIds = Parser.GetLikes(vk.Vk, postLinks[0]);
            for (var i = 1; i < postLinks.Count; i++)
            {
                likeIds.AddRange(Parser.GetLikes(vk.Vk, postLinks[i]));
            }

            return likeIds;
        }

        public static IEnumerable<User> FilterIds(VkSystem.VkSystem vk, string userIds, UserSearchParams @params)
        {
            var userNames = userIds.Replace("vk.com/id", "")
                                   .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var users = vk.Vk.Users.Get(userNames, UserFilter.GetProfileFields(@params));
            var filteredUsers = UserFilter.ApplyFilter(users, @params);
            return filteredUsers;
        }

        public static Dictionary<long?, string> GetCities(VkSystem.VkSystem vk, int countryId)
        {
            return vk.Vk.Database
                .GetCities(new GetCitiesParams { CountryId = countryId })
                .ToDictionary(x => x.Id, x => x.Title);
        }
    }
}
using System.Collections.Generic;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkInstruments.Core
{
    public interface IVkService
    {
        IEnumerable<long> ParseLikesFromPost(string postLink);

        IEnumerable<long> ParseLikesFromPosts(ICollection<string> postLinkColletion);

        IEnumerable<User> FilterIds(string userIds, UserSearchParams @params);

        Dictionary<long?, string> GetCountries(bool? needAll = null);

        Dictionary<long?, string> GetCities(int countryId);
    }
}

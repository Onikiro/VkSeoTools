using System.Collections.Generic;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkInstruments.Core
{
    public interface IVkService
    {
        Task<List<long>> ParseLikesFromPost(string input);

        Task<List<User>> FilterIds(string userIds, UserSearchParams @params);

        Task<Dictionary<long, string>> GetCountries(bool? needAll = null);

        Task<Dictionary<long, string>> GetCities(int countryId);
    }
}
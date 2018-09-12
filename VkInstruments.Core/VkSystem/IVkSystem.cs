using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkInstruments.Core.VkSystem
{
    public interface IVkSystem
    {
        VkApi Vk { get; }

        Settings SettingFilters { get; }

        ApiAuthParams GetParams(ulong appId);

        Task AuthAsync(AccessToken token);
    }
}

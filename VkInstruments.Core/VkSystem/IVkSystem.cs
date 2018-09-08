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

        void Auth(string token, int expireTime, long userId);

        void Auth(string token, string expireTime, string userId);
    }
}

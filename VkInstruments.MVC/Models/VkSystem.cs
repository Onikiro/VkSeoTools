using NLog;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkLikesParserMVC.Models
{
    internal class VkSystem
    {
        public readonly VkApi Vk = new VkApi(LogManager.CreateNullLogger());

        private readonly Settings _settingFilters = Settings.Groups;

        private static VkSystem _instance;

        public static VkSystem GetInstance(Settings settingFilters = null)
        {
            return _instance ?? (_instance = new VkSystem(settingFilters));
        }

        private VkSystem()
        {
        }

        private VkSystem(Settings settingFilters)
        {
            if (settingFilters != null)
                _settingFilters = settingFilters;
        }

        public ApiAuthParams GetParams(ulong appId)
        {
            return new ApiAuthParams
            {
                ApplicationId = appId,
                Settings = _settingFilters
            };
        }
    }
}

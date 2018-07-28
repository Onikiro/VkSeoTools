using NLog;
using VkLikesParserMVC.Auth;
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

        public VkSystem()
        {
        }

        public VkSystem(Settings settingFilters)
        {
            if(settingFilters != null)
                _settingFilters = settingFilters;
        }

        public void Auth(Settings settingFilters = null)
        {
            var auth = new AuthorizationVk(Vk.VkApiVersion);
            auth.SetAuthParams(GetParams());
            var result = auth.Authorize();
            Vk.Authorize(new ApiAuthParams
            {
                AccessToken = result.AccessToken
            });
        }

        public ApiAuthParams GetParams()
        {
            return new ApiAuthParams
            {
                ApplicationId = 6634517,
                Settings = _settingFilters
            };
        }
    }
}

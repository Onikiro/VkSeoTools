using System;
using System.Web;
using NLog;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkInstruments.Core.VkSystem
{
    public class VkSystem : IVkSystem
    {
        public VkApi Vk { get; } = new VkApi(LogManager.CreateNullLogger());

        public Settings SettingFilters { get; } = Settings.Groups;

        public VkSystem() { }

        protected VkSystem(Settings settingFilters = null)
        {
            if (settingFilters != null)
                SettingFilters = settingFilters;
        }

        public ApiAuthParams GetParams(ulong appId)
        {
            return new ApiAuthParams
            {
                ApplicationId = appId,
                Settings = SettingFilters
            };
        }

        public void Auth()
        {
            throw new NotImplementedException();
        }

        public void Auth(string token, int expireTime, long userId)
        {
            Vk.Authorize(new ApiAuthParams
            {
                AccessToken = token,
                TokenExpireTime = expireTime,
                UserId = userId
            });
        }

        public void Auth(HttpCookie cookies)
        {
            if (cookies == null)
                return;

            if (!long.TryParse(cookies["userId"], out var userId))
                return;

            var expireTime = (int)cookies.Expires.Subtract(DateTime.Now).TotalSeconds;
            Auth(cookies["token"], expireTime, userId);
        }
    }
}

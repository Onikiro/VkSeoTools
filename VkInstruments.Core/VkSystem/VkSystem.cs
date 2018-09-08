using System;
using Microsoft.Extensions.Logging.Abstractions;
using NLog;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkInstruments.Core.VkSystem
{
    public class VkSystem : IVkSystem
    {
        public VkApi Vk { get; } = new VkApi(NullLogger<VkApi>.Instance);

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

        public void Auth(string token, string expireTime, string userId)
        {
            if (!long.TryParse(userId, out var userIdResult) || !int.TryParse(expireTime, out var expireTimeResult))
                return;

            Auth(token, expireTimeResult, userIdResult);
        }
    }
}

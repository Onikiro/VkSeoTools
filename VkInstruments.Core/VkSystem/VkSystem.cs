using System;
using NLog;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkInstruments.Core.VkSystem
{
    public class VkSystem
    {
        public readonly VkApi Vk = new VkApi(LogManager.CreateNullLogger());

        public readonly Settings SettingFilters = Settings.Groups;

        private static VkSystem _instance;

        public static VkSystem GetInstance(Settings settingFilters = null)
        {
            return _instance ?? (_instance = new VkSystem(settingFilters));
        }

        protected VkSystem()
        {
        }

        protected VkSystem(Settings settingFilters)
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

        public virtual void Auth()
        {
            throw new NotImplementedException();
        }

        public virtual void Auth(string token, int expireTime, long userId)
        {
            throw new NotImplementedException();
        }
    }
}

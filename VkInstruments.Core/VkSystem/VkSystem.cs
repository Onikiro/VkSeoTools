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

        public VkSystem(Settings settingFilters = null)
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

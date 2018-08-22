using System;
using NLog;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using System.Web;

namespace VkInstruments.Core.VkSystem
{
    public abstract class VkSystem
    {
		public VkApi Vk { get; } = new VkApi(LogManager.CreateNullLogger());

        public Settings SettingFilters { get; } = Settings.Groups;

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

        public virtual void Auth()
        {
            throw new NotImplementedException();
        }

        public virtual void Auth(string token, int expireTime, long userId)
        {
            throw new NotImplementedException();
        }

		public virtual void Auth(HttpCookie cookies)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
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

        public Task AuthAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AuthAsync(AccessToken token)
        {
            await Vk.AuthorizeAsync(token.ToApiAuthParams());
        }
    }
}

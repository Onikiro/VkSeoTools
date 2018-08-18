using VkNet.Model;

namespace VkInstruments.MVC.Models
{
    public class VkSystem : Core.VkSystem.VkSystem
    {
        public override void Auth(string token, int expireTime, long userId)
        {
            Vk.Authorize(new ApiAuthParams
            {
                AccessToken = token,
                TokenExpireTime = expireTime,
                UserId = userId
            });
        }
    }
}
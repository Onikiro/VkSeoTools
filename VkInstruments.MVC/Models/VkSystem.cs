using System;
using System.Web;
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

		public override void Auth(HttpCookie cookies)
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
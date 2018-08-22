using System;
using System.Web;
using System.Web.Mvc;
using VkInstruments.MVC.Auth;
using VkInstruments.MVC.Models;
using VkNet.Enums.SafetyEnums;

namespace VkInstruments.MVC.Controllers
{
	public class AuthorizationController : Controller
	{
		private readonly VkSystem _vk = new VkSystem();

		public ActionResult Complete()
		{
			return View();
		}

		public ActionResult ReceiveToken()
		{
			var token = Request.QueryString["access_token"];
			var expiresIn = Request.QueryString["expires_in"];
			var userId = Request.QueryString["user_id"];

			if (!int.TryParse(expiresIn, out var expiresIni) || !long.TryParse(userId, out var userIdl))
			{
				return Redirect("~/Home/Parser");
			}

			_vk.Auth(token, expiresIni, userIdl);
			if (_vk.Vk.IsAuthorized)
			{
				SetTokenCookies(token, expiresIni, userIdl);
			}

			return Redirect("~/Home/Parser");
		}

		private void SetTokenCookies(string tokenValue, int expireTime, long userId)
		{
			var cookie = new HttpCookie("token")
			{
				["token"] = tokenValue,
				["userId"] = userId.ToString(),
				Expires = DateTime.Now.AddSeconds(expireTime)
			};

			Response.Cookies.Add(cookie);
		}

		public ActionResult Start()
		{
			var vkAuth = new AuthorizationVk(_vk.Vk.VkApiVersion);
			vkAuth.SetAuthParams(_vk.GetParams(6634517)); //Enter your appId there
			var authUri = vkAuth.CreateAuthorizeUrl(vkAuth.AuthParams.ApplicationId, vkAuth.AuthParams.Settings.ToUInt64(), Display.Page, "123456");
			return Redirect(authUri.AbsoluteUri);
		}
	}
}
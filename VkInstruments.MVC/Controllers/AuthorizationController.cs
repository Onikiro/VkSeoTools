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
        private AuthorizationVk _vkAuth;

        public ActionResult Complete()
		{
			return View();
		}

		public ActionResult ReceiveToken()
		{
		    var cookie = _vkAuth.GetCookieByQueryTokenString(Request.QueryString);

            _vk.Auth(cookie);
			if (_vk.Vk.IsAuthorized)
			{
				SetTokenCookies(cookie);
			}

			return Redirect("~/Home/Parser");
		}

		private void SetTokenCookies(HttpCookie cookie)
		{
			Response.Cookies.Add(cookie);
		}

		public ActionResult Start()
		{
		    _vkAuth = new AuthorizationVk(_vk.Vk.VkApiVersion);
		    _vkAuth.SetAuthParams(_vk.GetParams(1234567)); //Enter your appId there
			var authUri = _vkAuth.CreateAuthorizeUrl(_vkAuth.AuthParams.ApplicationId, _vkAuth.AuthParams.Settings.ToUInt64(), Display.Page, "123456");
			return Redirect(authUri.AbsoluteUri);
		}
	}
}
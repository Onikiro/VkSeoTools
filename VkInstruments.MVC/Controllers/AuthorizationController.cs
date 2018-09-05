using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VkInstruments.Core.VkSystem;
using VkInstruments.MVC.Auth;
using VkNet.Enums.SafetyEnums;

namespace VkInstruments.MVC.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : Controller
    {
        private readonly IVkSystem _vk;
        private readonly AuthorizationVk _vkAuth;
        private static string _returnUrl;

        public AuthorizationController(IVkSystem vk)
        {
            _vk = vk;
            _vkAuth = new AuthorizationVk(_vk.Vk.VkApiVersion);
            _vkAuth.SetAuthParams(_vk.GetParams(6447383));
        }

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
                FormsAuthentication.SetAuthCookie(_vk.Vk.UserId.ToString(), false);

                if (string.IsNullOrEmpty(_returnUrl)) return RedirectToAction("Parser", "Home");

                return new RedirectResult(_returnUrl);
            }

            return RedirectToAction("Login");
        }

        private void SetTokenCookies(HttpCookie cookie)
        {
            Response.Cookies.Add(cookie);
        }

        public ActionResult Start(string returnUrl)
        {
            _returnUrl = returnUrl;
            var authUri = _vkAuth.CreateAuthorizeUrl(_vkAuth.AuthParams.ApplicationId,
                _vkAuth.AuthParams.Settings.ToUInt64(), Display.Page, "123456");
            return Redirect(authUri.AbsoluteUri);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
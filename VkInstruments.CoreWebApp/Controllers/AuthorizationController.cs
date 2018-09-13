using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkInstruments.Core;
using VkInstruments.Core.VkSystem;
using VkInstruments.CoreWebApp.Auth;
using VkNet.Enums.SafetyEnums;

namespace VkInstruments.CoreWebApp.Controllers
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
            _vkAuth.SetAuthParams(_vk.GetParams(AppProperties.APP_ID));         
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignInVk(string returnUrl = null)
        {
            _returnUrl = returnUrl;

            var authUri = _vkAuth.CreateAuthorizeUrl(
                _vkAuth.AuthParams.ApplicationId,
                _vkAuth.AuthParams.Settings.ToUInt64(), 
                Display.Page, "123456");

            return Redirect(authUri.AbsoluteUri);
        }

        [HttpGet]
        public IActionResult Complete()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReceiveToken(string token, string expiresIn, string userId)
        {
            var accessToken = AccessToken.FromString(token, expiresIn, userId);
            if (accessToken.Token != null)
            {
                await _vk.AuthAsync(accessToken);
                await SignInAsync();

                if (string.IsNullOrEmpty(_returnUrl)) return RedirectToAction("Parser", "Home");

                return new RedirectResult(_returnUrl);
            }            
            return RedirectToAction("SignIn");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("SignIn");
        }

        private async Task SignInAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.Name, _vk.Vk.UserId.ToString()) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties { IsPersistent = false };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);
        }
    }
}
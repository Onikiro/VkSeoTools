using System.Web.Mvc;
using VkLikesParserMVC.Auth;
using VkLikesParserMVC.Models;
using VkNet.Enums.SafetyEnums;

namespace VkLikesParserMVC.Controllers
{
    public class AuthorizationController : Controller
    {

        private readonly VkSystem _vk = VkSystem.GetInstance();
        public bool IsAuthorized => _vk.Vk.IsAuthorized;
        private string _token;

        public ActionResult Complete()
        {
            return Redirect("~/Home/Parser");
        }

        public ActionResult Start()
        {
            var vkAuth = new AuthorizationVk(_vk.Vk.VkApiVersion);
            vkAuth.SetAuthParams(_vk.GetParams());
            var authUri = vkAuth.CreateAuthorizeUrl(vkAuth.AuthParams.ApplicationId, vkAuth.AuthParams.Settings.ToUInt64(), Display.Page, "123456");
            return Redirect(authUri.AbsoluteUri);
        }
    }
}
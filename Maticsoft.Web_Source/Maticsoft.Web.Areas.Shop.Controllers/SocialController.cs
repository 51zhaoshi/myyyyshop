namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.Web.Controllers;
    using System.Web.Mvc;

    public class SocialController : SocialControllerBase
    {
        public override ActionResult RedirectToHome()
        {
            return base.RedirectToAction("Index", "UserCenter", new { area = "Shop" });
        }

        public override ActionResult RedirectToUserBind()
        {
            return base.RedirectToAction("UserBind", "UserCenter", new { area = "Shop" });
        }
    }
}


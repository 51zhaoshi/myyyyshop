namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.Web.Controllers;
    using System.Web.Mvc;

    public class SocialController : SocialControllerBase
    {
        public override ActionResult RedirectToHome()
        {
            return base.RedirectToAction("Index", "Home", new { area = "Mobile" });
        }

        public override ActionResult RedirectToUserBind()
        {
            return base.RedirectToAction("Index", "Home", new { area = "Mobile" });
        }
    }
}


namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.Web.Controllers;
    using System.Web.Mvc;

    public class SocialController : SocialControllerBase
    {
        public override ActionResult RedirectToHome()
        {
            return base.RedirectToAction("Index", "Home", new { area = "CMS" });
        }

        public override ActionResult RedirectToUserBind()
        {
            return base.RedirectToAction("Index", "Home", new { area = "CMS" });
        }
    }
}


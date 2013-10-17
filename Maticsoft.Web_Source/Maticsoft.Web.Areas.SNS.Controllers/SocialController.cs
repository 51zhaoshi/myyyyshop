namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.Web.Controllers;
    using System.Web.Mvc;

    public class SocialController : SocialControllerBase
    {
        public override ActionResult RedirectToHome()
        {
            return base.RedirectToAction("Posts", "Profile", new { area = "SNS" });
        }

        public override ActionResult RedirectToUserBind()
        {
            return base.RedirectToAction("UserBind", "UserCenter", new { area = "SNS" });
        }
    }
}


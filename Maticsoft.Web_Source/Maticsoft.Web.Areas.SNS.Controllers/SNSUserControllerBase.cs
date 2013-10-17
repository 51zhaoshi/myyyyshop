namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.Components;
    using Maticsoft.Web;
    using Maticsoft.Web.Controllers;
    using System.Web.Mvc;

    [SNSError]
    public class SNSUserControllerBase : ControllerBaseUser
    {
        public override ActionResult RedirectToLogin(ActionResult result)
        {
            if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
            {
                return this.Redirect("/Account/Login");
            }
            return this.Redirect("/SNS/Account/Login");
        }
    }
}


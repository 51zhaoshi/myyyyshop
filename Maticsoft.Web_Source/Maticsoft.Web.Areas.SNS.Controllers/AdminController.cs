namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Components.Filters;
    using System;
    using System.Web.Mvc;

    [TokenAuthorize(AccountType.Admin)]
    public class AdminController : SNSControllerBase
    {
        public ActionResult AjaxDeleteOperation(FormCollection fm)
        {
            if (base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_DeleteList)))
            {
                string text1 = fm["Type"];
                string str = fm["TargetType"];
                int num = Globals.SafeInt(fm["TargetId"], 0);
                if ((num > 0) && !string.IsNullOrEmpty(str))
                {
                    if (str == "product")
                    {
                        int num2;
                        new Products().DeleteListEx(num.ToString(), out num2, false, 1);
                        if (num2 == 1)
                        {
                            return base.Content("Yes");
                        }
                    }
                    else
                    {
                        int num3;
                        new Photos().DeleteListEx(num.ToString(), out num3, false, 1);
                        if (num3 == 1)
                        {
                            return base.Content("Yes");
                        }
                    }
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxRecommandOperation(FormCollection fm)
        {
            if (base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_ApproveList)))
            {
                string str = fm["Type"];
                string str2 = fm["TargetType"];
                int productId = Globals.SafeInt(fm["TargetId"], 0);
                if (((productId > 0) && !string.IsNullOrEmpty(str)) && !string.IsNullOrEmpty(str2))
                {
                    if (str2 == "product")
                    {
                        Products products = new Products();
                        if (products.UpdateRecomend(productId, (str == "recommand") ? 1 : 0))
                        {
                            return base.Content("Yes");
                        }
                    }
                    else
                    {
                        Photos photos = new Photos();
                        if (photos.UpdateRecomend(productId, (str == "recommand") ? 1 : 0))
                        {
                            return base.Content("Yes");
                        }
                    }
                }
            }
            return base.Content("No");
        }

        public ActionResult Index()
        {
            return base.View();
        }
    }
}


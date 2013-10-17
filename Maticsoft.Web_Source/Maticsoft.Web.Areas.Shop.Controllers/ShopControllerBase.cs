namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.Web.Controllers;
    using System;
    using System.Web.Mvc;

    [ShopError]
    public class ShopControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
        public int CommentDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS), 5);
        public int FallDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_FallDataSize", ApplicationKeyType.SNS), 20);
        public int FallInitDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS), 5);
        public int PostDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS), 15);

        protected ViewResult View(string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                return base.View();
            }
            return base.View(viewName);
        }

        protected ViewResult View(string viewName, object model)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                return base.View(model);
            }
            return base.View(viewName, model);
        }
    }
}


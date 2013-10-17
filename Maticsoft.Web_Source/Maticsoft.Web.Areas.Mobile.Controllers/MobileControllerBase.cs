namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.Web.Controllers;
    using System;
    using System.Web.Mvc;

    [MobileError]
    public class MobileControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
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


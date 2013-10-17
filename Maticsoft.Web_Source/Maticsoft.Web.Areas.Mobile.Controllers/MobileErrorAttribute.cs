namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using System;
    using System.Web.Mvc;

    public class MobileErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Error");
        }
    }
}


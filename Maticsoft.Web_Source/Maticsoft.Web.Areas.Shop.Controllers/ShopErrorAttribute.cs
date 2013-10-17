namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using System;
    using System.Web.Mvc;

    public class ShopErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Error");
        }
    }
}


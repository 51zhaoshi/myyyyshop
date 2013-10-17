namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using System;
    using System.Web.Mvc;

    public class SNSErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Error");
        }
    }
}


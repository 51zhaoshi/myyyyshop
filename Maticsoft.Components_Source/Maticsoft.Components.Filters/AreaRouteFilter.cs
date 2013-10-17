namespace Maticsoft.Components.Filters
{
    using System;
    using System.Web.Mvc;

    public class AreaRouteFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}


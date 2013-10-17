namespace Maticsoft.Components.Filters
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RemoveDuplicateContentAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RouteCollection routes = RouteTable.Routes;
            RequestContext requestContext = filterContext.RequestContext;
            RouteData routeData = requestContext.RouteData;
            RouteValueDictionary dataTokens = routeData.DataTokens;
            if (dataTokens["area"] == null)
            {
                dataTokens.Add("area", "");
            }
            if (routeData.Values.ContainsKey("area"))
            {
                dataTokens.Add("area", routeData.Values["area"]);
            }
            VirtualPathData data2 = routes.GetVirtualPathForArea(requestContext, routeData.Values);
            if (data2 != null)
            {
                string a = data2.VirtualPath.ToLower();
                HttpRequestBase request = requestContext.HttpContext.Request;
                if (!string.Equals(a, request.Path))
                {
                    filterContext.Result = new RedirectResult(a + request.Url.Query, true);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}


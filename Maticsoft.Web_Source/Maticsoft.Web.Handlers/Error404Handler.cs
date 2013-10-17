namespace Maticsoft.Web.Handlers
{
    using System;
    using System.Web;

    public class Error404Handler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string str = context.Request.QueryString["aspxerrorpath"];
            int startIndex = str.LastIndexOf("/") + 1;
            str.Substring(startIndex).Replace(".aspx", string.Empty).Replace("-", " ");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


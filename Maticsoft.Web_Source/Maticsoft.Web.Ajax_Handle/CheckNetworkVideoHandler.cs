namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.Common.Video;
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    public class CheckNetworkVideoHandler : IHttpHandler
    {
        private Regex regUrl = new Regex(@"(http:\/\/([\w.]+\/?)\S*)");

        public bool Check(string url)
        {
            if (!this.IsUrl(url))
            {
                return false;
            }
            if (!VideoHelper.IsYouKuVideoUrl(url) && !VideoHelper.IsKu6VideoUrl(url))
            {
                return false;
            }
            return true;
        }

        public bool IsUrl(string content)
        {
            return this.regUrl.Match(content).Success;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            string url = request.Params["videoUrl"];
            if (this.Check(url))
            {
                response.Write("true");
            }
            else
            {
                response.Write("false");
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}


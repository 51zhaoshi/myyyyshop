namespace Maticsoft.Web.Handlers
{
    using System;
    using System.Web;

    public abstract class HandlerBase : IHttpHandler
    {
        public const string KEY_DATA = "DATA";
        public const string KEY_STATUS = "STATUS";
        public const string STATUS_ERROR = "ERROR";
        public const string STATUS_FAILED = "FAILED";
        public const string STATUS_NODATA = "NODATA";
        public const string STATUS_NOLOGIN = "NOLOGIN";
        public const string STATUS_SUCCESS = "SUCCESS";
        public const string STATUS_UNAUTHORIZED = "UNAUTHORIZED";

        protected HandlerBase()
        {
        }

        public abstract void ProcessRequest(HttpContext context);

        public abstract bool IsReusable { get; }
    }
}


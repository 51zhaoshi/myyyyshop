namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.Common;
    using Maticsoft.Json;
    using System;
    using System.Web;

    public class CMSContentHandler : IHttpHandler
    {
        public const string CMS_KEY_DATA = "DATA";
        public const string CMS_KEY_STATUS = "STATUS";
        public const string CMS_STATUS_ERROR = "ERROR";
        public const string CMS_STATUS_FAILED = "FAILED";
        public const string CMS_STATUS_SUCCESS = "SUCCESS";

        private void DeleteFile(HttpContext context)
        {
            JsonObject obj2 = new JsonObject();
            string str = context.Request.Form["FilePath"];
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (FileManage.DeleteFile(context.Server.MapPath(str)))
                {
                    obj2.Accumulate("STATUS", "SUCCESS");
                }
                else
                {
                    obj2.Accumulate("STATUS", "FAILED");
                }
            }
            else
            {
                obj2.Accumulate("STATUS", "ERROR");
            }
            context.Response.Write(obj2.ToString());
        }

        public void ProcessRequest(HttpContext context)
        {
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                string str2;
                if (((str2 = str) != null) && (str2 == "DeleteFile"))
                {
                    this.DeleteFile(context);
                }
            }
            catch (Exception exception)
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("STATUS", "ERROR");
                obj2.Put("DATA", exception);
                context.Response.Write(obj2.ToString());
            }
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


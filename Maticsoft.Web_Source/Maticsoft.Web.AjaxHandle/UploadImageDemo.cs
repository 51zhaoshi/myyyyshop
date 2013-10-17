namespace Maticsoft.Web.AjaxHandle
{
    using System;
    using System.IO;
    using System.Web;

    public class UploadImageDemo : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files[0];
            string path = "/UploadFolder/";
            int contentLength = file.ContentLength;
            int num2 = 0x4e2000;
            string s = "-1";
            if (contentLength <= num2)
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }
                string str3 = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string filename = HttpContext.Current.Server.MapPath(path) + str3;
                file.SaveAs(filename);
                s = path + str3;
            }
            context.Response.Write(s);
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


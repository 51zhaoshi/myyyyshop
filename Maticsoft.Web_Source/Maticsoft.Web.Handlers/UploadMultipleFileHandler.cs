namespace Maticsoft.Web.Handlers
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Web;
    using System.Web.SessionState;

    public class UploadMultipleFileHandler : HandlerBase, IReadOnlySessionState, IRequiresSessionState
    {
        internal string ResponseContentType = "text/plain";

        protected virtual HttpFileCollection GetHttpPostedFile(HttpContext context)
        {
            if ((context.Request.Files.Count != 0) && (context.Request.Files[0].ContentLength >= 1))
            {
                return context.Request.Files;
            }
            return null;
        }

        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = this.ResponseContentType;
                HttpFileCollection httpPostedFile = this.GetHttpPostedFile(context);
                if (httpPostedFile == null)
                {
                    throw new FileNotFoundException("UpdateFile Not Found! HttpPostedFile Is NULL!");
                }
                string path = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string str2 = context.Server.MapPath(path);
                if (!Directory.Exists(str2))
                {
                    Directory.CreateDirectory(str2);
                }
                string str3 = string.Empty;
                for (int i = 0; i < httpPostedFile.Count; i++)
                {
                    HttpPostedFile file = httpPostedFile[i];
                    string str4 = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + Path.GetExtension(file.FileName);
                    file.SaveAs(str2 + str4);
                    string str5 = "T300X400_" + str4;
                    ImageTools.MakeThumbnail(str2 + str4, str2 + str5, 300, 400, MakeThumbnailMode.HW, InterpolationMode.High, SmoothingMode.HighQuality);
                    str3 = str3 + "|" + str5;
                }
                JsonObject obj2 = new JsonObject();
                obj2.Put("success", true);
                obj2.Put("path", path + "{0}");
                obj2.Put("names", str3.TrimStart(new char[] { '|' }));
                context.Response.Write(obj2.ToString());
            }
            catch (Exception exception)
            {
                Maticsoft.Model.SysManage.ErrorLog model = new Maticsoft.Model.SysManage.ErrorLog {
                    Loginfo = exception.Message,
                    StackTrace = exception.ToString(),
                    Url = context.Request.Url.AbsoluteUri
                };
                Maticsoft.BLL.SysManage.ErrorLog.Add(model);
                throw;
            }
        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


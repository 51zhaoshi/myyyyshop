namespace Maticsoft.Map.Handler
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.SessionState;

    public abstract class UploadMarkImgHandlerBase : IHttpHandler, IRequiresSessionState
    {
        protected readonly string MarkerImagesPath = "Images/MapMarkers";
        protected readonly string UploadFolder = ("/" + ConfigHelper.GetConfigString("UploadFolder") + "/");

        protected UploadMarkImgHandlerBase()
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            this.Upload(context);
        }

        protected virtual void Upload(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["Filedata"];
            string path = this.UploadFolder + this.MarkerImagesPath + "/";
            string str2 = HttpContext.Current.Server.MapPath(path);
            string filename = string.Empty;
            if (file == null)
            {
                context.Response.Write("0");
            }
            else
            {
                if (!Directory.Exists(str2))
                {
                    Directory.CreateDirectory(str2);
                }
                string fileName = file.FileName;
                if (fileName.Length < 1)
                {
                    context.Response.Write("0");
                }
                else
                {
                    string extension = Path.GetExtension(fileName);
                    string str6 = Guid.NewGuid() + extension;
                    filename = str2 + str6;
                    file.SaveAs(filename);
                    context.Response.Write("1|" + path + str6);
                }
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


namespace Maticsoft.Web.Ajax_Handle
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;

    public class UploadPictureHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            HttpPostedFile file = context.Request.Files["Filedata"];
            string path = HttpContext.Current.Server.MapPath(context.Request["folder"]) + @"\";
            if (file != null)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                StringBuilder builder = new StringBuilder();
                string fileName = file.FileName;
                if (fileName.Length > 0)
                {
                    string extension = Path.GetExtension(fileName);
                    string format = Guid.NewGuid() + extension;
                    string filename = path + format;
                    file.SaveAs(filename);
                    builder.AppendFormat(format, new object[0]);
                }
                context.Response.Write("1|" + builder);
            }
            else
            {
                context.Response.Write("0");
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


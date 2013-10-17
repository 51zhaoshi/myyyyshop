namespace Maticsoft.Web.Ajax_Handle
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class UploadLocalVideoHandler : IHttpHandler
    {
        private string ffmpegTools = ConfigSystem.GetValueByCache("FFmpeg");
        private string uploadFolder = ConfigSystem.GetValueByCache("UploadFolder");

        public string CutOutLocalvideoImages(string filename)
        {
            try
            {
                string path = this.uploadFolder + filename;
                string str2 = HttpContext.Current.Server.MapPath(this.ffmpegTools);
                if (File.Exists(str2) && File.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    Path.GetExtension(path);
                    Path.ChangeExtension(path, ".jpg");
                    string str3 = HttpContext.Current.Server.MapPath(path + ".jpg");
                    string str4 = "448*336";
                    ProcessStartInfo startInfo = new ProcessStartInfo(str2) {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = " -i " + HttpContext.Current.Server.MapPath(path) + " -y -f image2 -t 20 -s " + str4 + " " + str3
                    };
                    try
                    {
                        Process.Start(startInfo);
                    }
                    catch
                    {
                        return "";
                    }
                    Thread.Sleep(0xfa0);
                    if (File.Exists(str3))
                    {
                        return (filename + ".jpg");
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

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
                    this.CutOutLocalvideoImages(format);
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


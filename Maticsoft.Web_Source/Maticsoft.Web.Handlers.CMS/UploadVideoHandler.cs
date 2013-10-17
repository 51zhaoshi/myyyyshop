namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.Web.Handlers;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Web;

    public class UploadVideoHandler : UploadFileHandlerBase
    {
        private string ffmpegTools = ConfigSystem.GetValueByCache("FFmpeg");

        public string CutOutLocalvideoImages(string filename, string uploadPath)
        {
            try
            {
                string path = uploadPath + filename;
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

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            this.CutOutLocalvideoImages(fileName, uploadPath);
            context.Response.Write(uploadPath + fileName + "|" + uploadPath + fileName + ".jpg");
        }
    }
}


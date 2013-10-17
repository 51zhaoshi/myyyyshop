namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.Common;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Web;
    using System.Web.SessionState;

    public abstract class UploadImageHandlerBase : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        protected MakeThumbnailMode makeThumbnailMode = MakeThumbnailMode.Auto;

        protected UploadImageHandlerBase()
        {
        }

        protected virtual string GenerateFileName(HttpPostedFile file)
        {
            return (Guid.NewGuid() + Path.GetExtension(file.FileName));
        }

        protected abstract Size GetNormalImageSize();
        protected abstract Size GetThumbImageSize();
        protected virtual string GetUploadPath(HttpContext context)
        {
            return (HttpContext.Current.Server.MapPath(context.Request.Params["folder"]) + @"\");
        }

        protected virtual void MakeThumbnail(string uploadPath, string fileName, Size thumbImageSize, Size normalImageSize)
        {
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T_" + fileName, thumbImageSize.Width, thumbImageSize.Height, this.makeThumbnailMode, InterpolationMode.High, SmoothingMode.HighQuality);
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "N_" + fileName, normalImageSize.Width, normalImageSize.Height, this.makeThumbnailMode, InterpolationMode.High, SmoothingMode.HighQuality);
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["Filedata"];
            if ((file != null) && (file.FileName.Length >= 1))
            {
                string uploadPath = this.GetUploadPath(context);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = this.GenerateFileName(file);
                this.SaveAs(uploadPath, fileName, file);
                this.ProcessSub(context, fileName);
            }
        }

        protected abstract void ProcessSub(HttpContext context, string fileName);
        protected virtual void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            file.SaveAs(uploadPath + fileName);
            this.MakeThumbnail(uploadPath, fileName, this.GetThumbImageSize(), this.GetNormalImageSize());
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


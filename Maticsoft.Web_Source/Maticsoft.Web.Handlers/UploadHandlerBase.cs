namespace Maticsoft.Web.Handlers
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.SessionState;

    public abstract class UploadHandlerBase : HandlerBase, IReadOnlySessionState, IRequiresSessionState
    {
        internal Maticsoft.Model.SysManage.ApplicationKeyType ApplicationKeyType;
        internal bool IsLocalSave;
        internal string ResponseContentType = "text/plain";
        internal string UploadTempFolder = string.Format("/{0}/Temp/{1}/", MvcApplication.UploadFolder, DateTime.Now.ToString("yyyyMMdd"));

        public UploadHandlerBase(bool isLocalSave = true, Maticsoft.Model.SysManage.ApplicationKeyType applicationKeyType = -1)
        {
            this.IsLocalSave = isLocalSave;
            this.ApplicationKeyType = applicationKeyType;
        }

        protected virtual string GenerateFileName(HttpPostedFile file)
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + Path.GetExtension(file.FileName));
        }

        protected virtual HttpPostedFile GetHttpPostedFile(HttpContext context)
        {
            if (context.Request.Files.Count != 0)
            {
                return context.Request.Files[0];
            }
            return null;
        }

        protected virtual string GetUploadPath(HttpContext context)
        {
            return HttpContext.Current.Server.MapPath(this.UploadTempFolder);
        }

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = this.ResponseContentType;
            HttpPostedFile httpPostedFile = this.GetHttpPostedFile(context);
            if (httpPostedFile == null)
            {
                throw new FileNotFoundException("UpdateFile Not Found! HttpPostedFile Is NULL!");
            }
            if (httpPostedFile.FileName.Length >= 1)
            {
                string fileName = this.GenerateFileName(httpPostedFile);
                try
                {
                    if (this.IsLocalSave)
                    {
                        string uploadPath = this.GetUploadPath(context);
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }
                        this.SaveAs(uploadPath, fileName, httpPostedFile);
                    }
                    else
                    {
                        int contentLength = httpPostedFile.ContentLength;
                        byte[] buffer = new byte[contentLength];
                        httpPostedFile.InputStream.Read(buffer, 0, contentLength);
                        string imageUrl = "";
                        if (UpYunManager.UploadExecute(buffer, fileName, this.ApplicationKeyType, out imageUrl))
                        {
                            fileName = imageUrl;
                        }
                    }
                    this.ProcessSub(context, this.UploadTempFolder, fileName);
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
        }

        protected abstract void ProcessSub(HttpContext context, string uploadPath, string fileName);
        protected abstract void SaveAs(string uploadPath, string fileName, HttpPostedFile file);

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.Web.Handlers;
    using System;
    using System.Web;

    public class UploadFileHandler : UploadFileHandlerBase
    {
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            context.Response.Write("1|" + uploadPath + "{0}" + fileName);
        }
    }
}


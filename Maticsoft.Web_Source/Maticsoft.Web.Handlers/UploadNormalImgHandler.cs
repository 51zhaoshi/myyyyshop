namespace Maticsoft.Web.Handlers
{
    using System;
    using System.Web;

    public class UploadNormalImgHandler : UploadImageHandlerBase
    {
        protected UploadNormalImgHandler() : base(MakeThumbnailMode.Auto, true, ApplicationKeyType.None)
        {
        }

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            context.Response.Write("1|" + uploadPath + "{0}" + fileName);
        }

        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            file.SaveAs(uploadPath + fileName);
        }
    }
}


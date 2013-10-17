namespace Maticsoft.Web.Handlers
{
    using System;
    using System.Web;

    public abstract class UploadFileHandlerBase : UploadHandlerBase
    {
        protected UploadFileHandlerBase() : base(true, ApplicationKeyType.None)
        {
        }

        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            file.SaveAs(uploadPath + fileName);
        }
    }
}


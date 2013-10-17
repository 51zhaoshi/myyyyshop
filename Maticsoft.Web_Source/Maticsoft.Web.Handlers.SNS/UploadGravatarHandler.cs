namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Drawing.Drawing2D;
    using System.Web;

    public class UploadGravatarHandler : UploadImageHandlerBase
    {
        public const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";

        public UploadGravatarHandler() : base(MakeThumbnailMode.None, true, ApplicationKeyType.None)
        {
        }

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            JsonObject obj2 = new JsonObject();
            obj2.Put("data", uploadPath + "T_" + fileName);
            obj2.Put("success", true);
            context.Response.Write(obj2.ToString());
        }

        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            file.SaveAs(uploadPath + fileName);
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T_" + fileName, 420, 400, MakeThumbnailMode.Auto, InterpolationMode.High, SmoothingMode.HighQuality);
        }
    }
}


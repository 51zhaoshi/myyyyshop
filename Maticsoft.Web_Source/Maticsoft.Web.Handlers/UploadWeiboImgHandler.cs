namespace Maticsoft.Web.Handlers
{
    using Maticsoft.Json;
    using System;
    using System.Web;

    public class UploadWeiboImgHandler : UploadImageHandlerBase
    {
        public const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";

        public UploadWeiboImgHandler() : base(MakeThumbnailMode.None, true, ApplicationKeyType.None)
        {
        }

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            try
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("data", uploadPath + "{0}" + fileName);
                obj2.Put("success", true);
                context.Response.Write(obj2.ToString());
            }
            catch (Exception)
            {
            }
        }
    }
}


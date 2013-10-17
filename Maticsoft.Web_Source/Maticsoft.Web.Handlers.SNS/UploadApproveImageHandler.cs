namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class UploadApproveImageHandler : UploadImageHandlerBase
    {
        public const string POLL_KEY_DATA = "data";
        public const string POLL_KEY_SUCCESS = "success";

        public UploadApproveImageHandler() : base(MakeThumbnailMode.None, true, ApplicationKeyType.None)
        {
        }

        protected override List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(EnumHelper.AreaType.SNS, MvcApplication.ThemeName);
        }

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            try
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("success", true);
                obj2.Put("data", uploadPath + "{0}" + fileName);
                context.Response.Write(obj2.ToString());
            }
            catch (Exception)
            {
            }
        }
    }
}


namespace Maticsoft.Web.Handlers.Shop
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class ProductUploadImgHandler : UploadImageHandlerBase
    {
        public const string POLL_KEY_DATA = "data";
        public const string POLL_KEY_SUCCESS = "success";

        protected ProductUploadImgHandler() : base(MakeThumbnailMode.Auto, true, ApplicationKeyType.None)
        {
        }

        protected override List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(EnumHelper.AreaType.Shop, MvcApplication.ThemeName);
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


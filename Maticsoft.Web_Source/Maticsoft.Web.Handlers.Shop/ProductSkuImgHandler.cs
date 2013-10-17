namespace Maticsoft.Web.Handlers.Shop
{
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Drawing.Drawing2D;
    using System.Web;

    public class ProductSkuImgHandler : UploadImageHandlerBase
    {
        public const string POLL_KEY_DATA = "data";
        public const string POLL_KEY_SUCCESS = "success";

        protected ProductSkuImgHandler() : base(MakeThumbnailMode.Auto, true, ApplicationKeyType.None)
        {
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

        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            file.SaveAs(uploadPath + fileName);
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T32X32_" + fileName, 0x20, 0x20, MakeThumbnailMode.HW, InterpolationMode.High, SmoothingMode.HighQuality);
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T130X130_" + fileName, 130, 130, MakeThumbnailMode.HW, InterpolationMode.High, SmoothingMode.HighQuality);
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T300X390_" + fileName, 300, 390, MakeThumbnailMode.Cut, InterpolationMode.High, SmoothingMode.HighQuality);
        }
    }
}


namespace Maticsoft.Web.Handlers.CMS
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Components;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class UploadArticleImgHandler : UploadImageHandlerBase
    {
        protected UploadArticleImgHandler() : base(MakeThumbnailMode.Auto, true, ApplicationKeyType.None)
        {
        }

        protected override List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(EnumHelper.AreaType.CMS, MvcApplication.ThemeName);
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


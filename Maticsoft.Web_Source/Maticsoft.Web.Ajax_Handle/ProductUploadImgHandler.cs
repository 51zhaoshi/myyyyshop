namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using System;
    using System.Drawing;
    using System.Web;

    public class ProductUploadImgHandler : UploadImageHandlerBase
    {
        private string filePath = "/UploadFolder/Images/ProductImages/";

        protected ProductUploadImgHandler()
        {
            base.makeThumbnailMode = MakeThumbnailMode.Cut;
        }

        protected override Size GetNormalImageSize()
        {
            return StringPlus.SplitToSize(ConfigSystem.GetValueByCache("ProductNormalImageSize"), '|', SettingConstant.ProductNormalSize.Width, SettingConstant.ProductNormalSize.Height);
        }

        protected override Size GetThumbImageSize()
        {
            return StringPlus.SplitToSize(ConfigSystem.GetValueByCache("ProductNormalImageSize"), '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
        }

        protected override string GetUploadPath(HttpContext context)
        {
            return (HttpContext.Current.Server.MapPath(this.filePath) + @"\");
        }

        protected override void ProcessSub(HttpContext context, string fileName)
        {
            context.Response.Write("1|" + this.filePath + "{0}" + fileName);
        }
    }
}


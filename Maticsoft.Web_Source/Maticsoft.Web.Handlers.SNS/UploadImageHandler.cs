namespace Maticsoft.Web.Handlers.SNS
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class UploadImageHandler : UploadImageHandlerBase
    {
        public const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";

        public UploadImageHandler() : base(MakeThumbnailMode.None, true, ApplicationKeyType.None)
        {
            if (ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
            {
                base.IsLocalSave = false;
                base.ApplicationKeyType = ApplicationKeyType.SNS;
            }
        }

        protected override List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, MvcApplication.ThemeName);
        }

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            try
            {
                JsonObject obj2 = new JsonObject();
                if (ConfigSystem.GetValueByCache("SNS_ImageStoreWay") != "1")
                {
                    obj2.Put("data", uploadPath + "{0}" + fileName);
                }
                else
                {
                    obj2.Put("data", fileName);
                }
                obj2.Put("success", true);
                context.Response.Write(obj2.ToString());
            }
            catch (Exception)
            {
            }
        }
    }
}


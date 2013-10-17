namespace Maticsoft.Web.Handlers
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Web;

    public abstract class UploadImageHandlerBase : UploadHandlerBase
    {
        protected readonly MakeThumbnailMode ThumbnailMode;

        public UploadImageHandlerBase(MakeThumbnailMode mode = 0, bool isLocalSave = true, ApplicationKeyType applicationKeyType = -1) : base(isLocalSave, applicationKeyType)
        {
            this.ThumbnailMode = (mode == MakeThumbnailMode.None) ? MakeThumbnailMode.W : mode;
        }

        protected MakeThumbnailMode GetThumMode(int ThumMode)
        {
            switch (ThumMode)
            {
                case 0:
                    return MakeThumbnailMode.Auto;

                case 1:
                    return MakeThumbnailMode.Cut;

                case 2:
                    return MakeThumbnailMode.H;

                case 3:
                    return MakeThumbnailMode.HW;

                case 4:
                    return MakeThumbnailMode.W;
            }
            return MakeThumbnailMode.Auto;
        }

        protected virtual List<ThumbnailSize> GetThumSizeList()
        {
            return new List<ThumbnailSize>();
        }

        protected virtual void MakeThumbnail(string uploadPath, string fileName, string thumName, int thumWidth, int thumHeight, MakeThumbnailMode mode)
        {
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + thumName + fileName, thumWidth, thumHeight, mode, InterpolationMode.High, SmoothingMode.HighQuality);
        }

        protected virtual void MakeThumbnailList(string uploadPath, string fileName, List<ThumbnailSize> thumSizeList)
        {
            bool boolValueByCache = ConfigSystem.GetBoolValueByCache("System_ThumbImage_AddWater");
            string str = uploadPath;
            if (boolValueByCache)
            {
                str = uploadPath + "W_";
                FileHelper.MakeWater(uploadPath + fileName, str + fileName);
            }
            if ((thumSizeList != null) && (thumSizeList.Count > 0))
            {
                foreach (ThumbnailSize size in thumSizeList)
                {
                    ImageTools.MakeThumbnail(str + fileName, uploadPath + size.ThumName + fileName, size.ThumWidth, size.ThumHeight, this.GetThumMode(size.ThumMode), InterpolationMode.High, SmoothingMode.HighQuality);
                }
            }
        }

        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            file.SaveAs(uploadPath + fileName);
            this.MakeThumbnailList(uploadPath, fileName, this.GetThumSizeList());
        }
    }
}


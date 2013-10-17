namespace Maticsoft.Web.Ajax_Handle
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using System;
    using System.Drawing;
    using System.Web;

    public class UploadPhotoHandler : UploadImageHandlerBase
    {
        protected UploadPhotoHandler()
        {
            base.makeThumbnailMode = MakeThumbnailMode.Auto;
        }

        protected override Size GetNormalImageSize()
        {
            return new Size(Globals.SafeInt(ConfigSystem.GetValueByCache("NormalImageWidth"), 0), Globals.SafeInt(ConfigSystem.GetValueByCache("NormalImageHeight"), 0));
        }

        protected override Size GetThumbImageSize()
        {
            return new Size(Globals.SafeInt(ConfigSystem.GetValueByCache("ThumbImageWidth"), 0), Globals.SafeInt(ConfigSystem.GetValueByCache("ThumbImageHeight"), 0));
        }

        protected override void ProcessSub(HttpContext context, string fileName)
        {
            HttpRequest request = context.Request;
            if (!string.IsNullOrWhiteSpace(request.Params["album"]))
            {
                string s = request.Params["album"];
                if (!string.IsNullOrWhiteSpace(request.Params["userId"]))
                {
                    string str2 = request.Params["userId"];
                    string str3 = context.Request.Params["folder"];
                    Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
                    HttpPostedFile file = context.Request.Files["Filedata"];
                    Maticsoft.Model.CMS.Photo photo3 = new Maticsoft.Model.CMS.Photo {
                        PhotoName = file.FileName,
                        ImageUrl = str3 + "/" + fileName,
                        Description = "",
                        AlbumID = int.Parse(s),
                        State = 1,
                        CreatedUserID = int.Parse(str2),
                        CreatedDate = DateTime.Now,
                        PVCount = 0,
                        ClassID = 1,
                        ThumbImageUrl = str3 + "/T_" + fileName,
                        NormalImageUrl = str3 + "/N_" + fileName,
                        Sequence = new int?(photo.GetMaxSequence()),
                        IsRecomend = false,
                        CommentCount = 0,
                        Tags = ""
                    };
                    Maticsoft.Model.CMS.Photo model = photo3;
                    int num = photo.Add(model);
                    context.Response.Write(num.ToString());
                }
            }
        }
    }
}


namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Components.Setting.SNS;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class PhotoController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 0x20;
        private Maticsoft.BLL.SNS.Photos bllPhotos = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.SNS.Categories cateBll = new Maticsoft.BLL.SNS.Categories();
        private int commentPagesize = 5;

        public PhotoController()
        {
            this.commentPagesize = base.CommentDataSize;
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
        }

        public ActionResult Detail(int pid)
        {
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            if (!photos.Exists(pid))
            {
                return base.RedirectToAction("Index", "Error");
            }
            int albumId = Globals.SafeInt(base.Request.Params["AlbumId"], -1);
            TargetDetail model = new TargetDetail();
            Maticsoft.BLL.SNS.PhotoTags tags = new Maticsoft.BLL.SNS.PhotoTags();
            new Maticsoft.BLL.SNS.Comments();
            model = photos.GetPhotoAssistionInfo(pid, base.UserAlbumDetailType);
            Maticsoft.BLL.SNS.UserFavourite favourite = new Maticsoft.BLL.SNS.UserFavourite();
            model.RecommandPhoto = photos.GetRecommandByPid(pid);
            model.PhotoTagList = tags.GetHotTags(0x10);
            model.FavUserList = favourite.GetFavUserByTargetId(pid, 0, 8);
            model.CommentPageSize = this.commentPagesize;
            if (model.Photo == null)
            {
                return base.RedirectToAction("Index", "Home");
            }
            int prevID = photos.GetPrevID(pid, albumId);
            int nextID = photos.GetNextID(pid, albumId);
            IPageSetting pageSetting = PageSetting.GetPageSetting("PhotoDetail", ApplicationKeyType.SNS);
            string[][] values = new string[1][];
            values[0] = new string[] { "{cname}", model.Sharedes ?? (model.Nickname + "分享的图片") };
            pageSetting.Replace(values);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if (valueByCache != "true")
            {
                ((dynamic) base.ViewBag).PrevUrl = (prevID > 0) ? ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + prevID) : "#";
                ((dynamic) base.ViewBag).NextUrl = (nextID > 0) ? ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + nextID) : "#";
            }
            else
            {
                ((dynamic) base.ViewBag).PrevUrl = (prevID > 0) ? PageSetting.GetPhotoUrl(prevID) : "#";
                ((dynamic) base.ViewBag).NextUrl = (nextID > 0) ? PageSetting.GetPhotoUrl(nextID) : "#";
            }
            foreach (Maticsoft.Model.SNS.Photos photos2 in model.RecommandPhoto)
            {
                if (valueByCache != "true")
                {
                    photos2.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + photos2.PhotoID);
                    continue;
                }
                photos2.StaticUrl = string.IsNullOrWhiteSpace(photos2.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + photos2.PhotoID)) : photos2.StaticUrl;
            }
            return base.View(model);
        }

        public ActionResult GetCount(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["PhotoId"]))
            {
                return base.Content("No");
            }
            int targetId = Globals.SafeInt(Fm["PhotoId"], 0);
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            int favCountByTargetId = new Maticsoft.BLL.SNS.UserFavourite().GetFavCountByTargetId(targetId, 0);
            int commentCount = comments.GetCommentCount(1, targetId);
            photos.UpdatePvCount(targetId);
            TargetDetail photoAssistionInfo = photos.GetPhotoAssistionInfo(targetId, base.UserAlbumDetailType);
            return base.Content(string.Concat(new object[] { photoAssistionInfo.Favouritecount, "|", photoAssistionInfo.PvCount, "|", commentCount, "|", favCountByTargetId }));
        }

        public ActionResult GetListCounts(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["PhotoIds"]))
            {
                return base.Content("No");
            }
            string[] strArray = Fm["PhotoIds"].Split(new char[] { ',' });
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            string content = "";
            int num = 0;
            foreach (string str3 in strArray)
            {
                int pid = Globals.SafeInt(str3, 0);
                TargetDetail photoAssistionInfo = photos.GetPhotoAssistionInfo(pid, base.UserAlbumDetailType);
                int commentCount = comments.GetCommentCount(1, pid);
                if (num == 0)
                {
                    content = string.Concat(new object[] { pid, ",", photoAssistionInfo.Favouritecount, ",", commentCount });
                }
                else
                {
                    content = string.Concat(new object[] { content, "|", pid, ",", photoAssistionInfo.Favouritecount, ",", commentCount });
                }
                num++;
            }
            return base.Content(content);
        }

        public ActionResult Index(int type, int categoryId, string address, string orderby, int? pageIndex)
        {
            PhotoList model = new PhotoList {
                CategoryInfo = this.cateBll.GetModelByCache(categoryId)
            };
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            if (address == "all")
            {
                address = "";
            }
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int num4 = 0;
            num4 = this.bllPhotos.GetCountEx(type, categoryId, address);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > num4) ? num4 : num5;
            IPageSetting photoListSetting = PageSetting.GetPhotoListSetting(categoryId, ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = photoListSetting.Title;
            ((dynamic) base.ViewBag).Keywords = photoListSetting.Keywords;
            ((dynamic) base.ViewBag).Description = photoListSetting.Description;
            string str2 = orderby;
            if (str2 != null)
            {
                if (!(str2 == "hot"))
                {
                    if (str2 == "popular")
                    {
                        orderby = "FavouriteCount desc";
                        dynamic obj3 = base.ViewBag;
                        if (<Index>o__SiteContainer0.<>p__Sitec == null)
                        {
                            <Index>o__SiteContainer0.<>p__Sitec = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "Title", typeof(PhotoController)));
                        }
                        if (!<Index>o__SiteContainer0.<>p__Sitec.Target(<Index>o__SiteContainer0.<>p__Sitec, obj3))
                        {
                            if (<Index>o__SiteContainer0.<>p__Sitee == null)
                            {
                                <Index>o__SiteContainer0.<>p__Sitee = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(PhotoController), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                            }
                            obj3.Title = <Index>o__SiteContainer0.<>p__Sitee.Target(<Index>o__SiteContainer0.<>p__Sitee, obj3.Title, " 喜欢");
                        }
                        else
                        {
                            obj3.add_Title(" 喜欢");
                        }
                        goto Label_08BE;
                    }
                }
                else
                {
                    orderby = "CommentCount desc";
                    dynamic obj2 = base.ViewBag;
                    if (<Index>o__SiteContainer0.<>p__Site7 == null)
                    {
                        <Index>o__SiteContainer0.<>p__Site7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "Title", typeof(PhotoController)));
                    }
                    if (!<Index>o__SiteContainer0.<>p__Site7.Target(<Index>o__SiteContainer0.<>p__Site7, obj2))
                    {
                        if (<Index>o__SiteContainer0.<>p__Site9 == null)
                        {
                            <Index>o__SiteContainer0.<>p__Site9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(PhotoController), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                        }
                        obj2.Title = <Index>o__SiteContainer0.<>p__Site9.Target(<Index>o__SiteContainer0.<>p__Site9, obj2.Title, " 最热");
                    }
                    else
                    {
                        obj2.add_Title(" 最热");
                    }
                    goto Label_08BE;
                }
            }
            orderby = "";
            dynamic viewBag = base.ViewBag;
            if (<Index>o__SiteContainer0.<>p__Site11 == null)
            {
                <Index>o__SiteContainer0.<>p__Site11 = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "Title", typeof(PhotoController)));
            }
            if (!<Index>o__SiteContainer0.<>p__Site11.Target(<Index>o__SiteContainer0.<>p__Site11, viewBag))
            {
                if (<Index>o__SiteContainer0.<>p__Site13 == null)
                {
                    <Index>o__SiteContainer0.<>p__Site13 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(PhotoController), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                }
                viewBag.Title = <Index>o__SiteContainer0.<>p__Site13.Target(<Index>o__SiteContainer0.<>p__Site13, viewBag.Title, " 最新");
            }
            else
            {
                viewBag.add_Title(" 最新");
            }
        Label_08BE:
            if (num4 >= 1)
            {
                int? nullable = pageIndex;
                model.PhotoPagedList = this.bllPhotos.GetPhotoListByPageCache(type, categoryId, address, orderby, startIndex, endIndex).ToPagedList<PostContent>(nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, new int?(num4));
                if (base.Request.IsAjaxRequest())
                {
                    return this.PartialView("PhotoList", model);
                }
                string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                if ((model.PhotoPagedList != null) && (model.PhotoPagedList.Count > 0))
                {
                    foreach (PostContent content in model.PhotoPagedList)
                    {
                        if (valueByCache != "true")
                        {
                            content.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + content.TargetId);
                            continue;
                        }
                        Maticsoft.Model.SNS.Photos modelByCache = this.bllPhotos.GetModelByCache((int) content.TargetId);
                        if ((modelByCache == null) || string.IsNullOrWhiteSpace(modelByCache.StaticUrl))
                        {
                        }
                        content.StaticUrl = (<Index>o__SiteContainer0.<>p__Site1d != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <Index>o__SiteContainer0.<>p__Site1d.Target(<Index>o__SiteContainer0.<>p__Site1d, base.ViewBag)) + "Photo/Detail/") + content.TargetId));
                    }
                }
                if ((model.ZuiInList != null) && (model.ZuiInList.Count > 0))
                {
                    foreach (ZuiInPhoto photo in model.ZuiInList)
                    {
                        if (valueByCache != "true")
                        {
                            photo.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + photo.PhotoId);
                            continue;
                        }
                        Maticsoft.Model.SNS.Photos photos2 = this.bllPhotos.GetModelByCache(photo.PhotoId);
                        if ((photos2 == null) || string.IsNullOrWhiteSpace(photos2.StaticUrl))
                        {
                        }
                        photo.StaticUrl = (<Index>o__SiteContainer0.<>p__Site25 != null) ? photos2.StaticUrl : ((string) ((((dynamic) <Index>o__SiteContainer0.<>p__Site25.Target(<Index>o__SiteContainer0.<>p__Site25, base.ViewBag)) + "Photo/Detail/") + photo.PhotoId));
                    }
                }
            }
            return base.View(model);
        }

        [HttpPost]
        public ActionResult PhotosWaterfall(int type, int categoryId, string address, string orderby, int startIndex)
        {
            PhotoList model = new PhotoList();
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            if (address == "all")
            {
                address = "";
            }
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            if (this.bllPhotos.GetCountEx(type, categoryId, address) < 1)
            {
                return new EmptyResult();
            }
            string str2 = orderby;
            if (str2 != null)
            {
                if (!(str2 == "hot"))
                {
                    if (str2 == "popular")
                    {
                        orderby = "FavouriteCount desc";
                        goto Label_0101;
                    }
                }
                else
                {
                    orderby = "CommentCount desc";
                    goto Label_0101;
                }
            }
            orderby = "";
        Label_0101:
            model.PhotoListWaterfall = this.bllPhotos.GetPhotoListByPageCache(type, categoryId, address, orderby, startIndex, endIndex);
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (PostContent content in model.PhotoListWaterfall)
            {
                if (valueByCache != "true")
                {
                    content.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + content.TargetId);
                    continue;
                }
                Maticsoft.Model.SNS.Photos modelByCache = this.bllPhotos.GetModelByCache((int) content.TargetId);
                if ((modelByCache == null) || string.IsNullOrWhiteSpace(modelByCache.StaticUrl))
                {
                }
                content.StaticUrl = (<PhotosWaterfall>o__SiteContainer26.<>p__Site2f != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <PhotosWaterfall>o__SiteContainer26.<>p__Site2f.Target(<PhotosWaterfall>o__SiteContainer26.<>p__Site2f, base.ViewBag)) + "Photo/Detail/") + content.TargetId));
            }
            return base.View("PhotoListWaterfall", model);
        }

        public ActionResult ScrollPhotos()
        {
            List<Maticsoft.Model.SNS.Photos> topPhotoPostByType = this.bllPhotos.GetTopPhotoPostByType(9, 0);
            if ((topPhotoPostByType != null) && (topPhotoPostByType.Count > 0))
            {
                string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                foreach (Maticsoft.Model.SNS.Photos photos in topPhotoPostByType)
                {
                    if (valueByCache != "true")
                    {
                        photos.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + photos.PhotoID);
                        continue;
                    }
                    Maticsoft.Model.SNS.Photos modelByCache = this.bllPhotos.GetModelByCache(photos.PhotoID);
                    if ((modelByCache == null) || string.IsNullOrWhiteSpace(modelByCache.StaticUrl))
                    {
                    }
                    photos.StaticUrl = (<ScrollPhotos>o__SiteContainer30.<>p__Site38 != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <ScrollPhotos>o__SiteContainer30.<>p__Site38.Target(<ScrollPhotos>o__SiteContainer30.<>p__Site38, base.ViewBag)) + "Photo/Detail/") + photos.PhotoID));
                }
            }
            return base.View(topPhotoPostByType);
        }
    }
}


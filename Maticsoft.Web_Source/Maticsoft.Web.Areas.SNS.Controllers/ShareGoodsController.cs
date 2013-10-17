namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class ShareGoodsController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 0x20;
        private Maticsoft.BLL.SNS.Categories bllCategory = new Maticsoft.BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.Photos bllPhotos = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.SNS.Products productBll = new Maticsoft.BLL.SNS.Products();

        public ShareGoodsController()
        {
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
        }

        public ActionResult Index(string orderby, int? pageIndex)
        {
            int? nullable;
            PhotoList model = new PhotoList();
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int recordCount = 0;
            int intValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("SNS_ShareGood_Category");
            if (intValueByCache == -1)
            {
                intValueByCache = 0xa7;
            }
            recordCount = this.bllPhotos.GetRecordCount("Status=1 and  CategoryId=" + intValueByCache);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num6 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num6 > recordCount) ? recordCount : num6;
            IPageSetting pageSetting = PageSetting.GetPageSetting("PhotoList", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (recordCount < 1)
            {
                return base.View(model);
            }
            string str3 = orderby;
            if (str3 != null)
            {
                if (!(str3 == "hot"))
                {
                    if (str3 == "popular")
                    {
                        orderby = "FavouriteCount desc";
                        goto Label_0379;
                    }
                }
                else
                {
                    orderby = "CommentCount desc";
                    goto Label_0379;
                }
            }
            orderby = "";
        Label_0379:
            nullable = pageIndex;
            model.PhotoPagedList = this.bllPhotos.GetCachePhotoListByPage(intValueByCache, orderby, startIndex, endIndex).ToPagedList<PostContent>(nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, new int?(recordCount));
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("ShareGoodsList", model);
            }
            model.ZuiInList = this.bllPhotos.GetZuiInListByCache(intValueByCache, 3);
            model.PhotoCategory = this.bllCategory.GetPhotoMenuCategoryList();
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (PostContent content in model.PhotoPagedList)
            {
                if (valueByCache != "true")
                {
                    content.StaticUrl = (string) (((((dynamic) base.ViewBag).BasePath + ((content.Type == 1) ? "Product/" : "Photo/")) + "Detail/") + content.TargetId);
                    continue;
                }
                string str2 = "";
                if (content.Type == 1)
                {
                    Maticsoft.Model.SNS.Products modelByCache = this.productBll.GetModelByCache(content.TargetId);
                    if ((modelByCache == null) || string.IsNullOrWhiteSpace(modelByCache.StaticUrl))
                    {
                    }
                    str2 = (<Index>o__SiteContainer0.<>p__Sitef != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <Index>o__SiteContainer0.<>p__Sitef.Target(<Index>o__SiteContainer0.<>p__Sitef, base.ViewBag)) + "Product/Detail/") + content.TargetId));
                }
                else
                {
                    Maticsoft.Model.SNS.Photos photos = this.bllPhotos.GetModelByCache((int) content.TargetId);
                    if ((photos == null) || string.IsNullOrWhiteSpace(photos.StaticUrl))
                    {
                    }
                    str2 = (<Index>o__SiteContainer0.<>p__Site13 != null) ? photos.StaticUrl : ((string) ((((dynamic) <Index>o__SiteContainer0.<>p__Site13.Target(<Index>o__SiteContainer0.<>p__Site13, base.ViewBag)) + "Photo/Detail/") + content.TargetId));
                }
                content.StaticUrl = str2;
            }
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
                photo.StaticUrl = (<Index>o__SiteContainer0.<>p__Site1b != null) ? photos2.StaticUrl : ((string) ((((dynamic) <Index>o__SiteContainer0.<>p__Site1b.Target(<Index>o__SiteContainer0.<>p__Site1b, base.ViewBag)) + "Photo/Detail/") + photo.PhotoId));
            }
            return base.View(model);
        }

        public ActionResult ScrollShareGoods()
        {
            int intValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("SNS_ShareGood_Category");
            if (intValueByCache == -1)
            {
                intValueByCache = 0xa7;
            }
            List<Maticsoft.Model.SNS.Photos> topPhotoPostByType = this.bllPhotos.GetTopPhotoPostByType(9, intValueByCache);
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
                    photos.StaticUrl = (<ScrollShareGoods>o__SiteContainer2b.<>p__Site33 != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <ScrollShareGoods>o__SiteContainer2b.<>p__Site33.Target(<ScrollShareGoods>o__SiteContainer2b.<>p__Site33, base.ViewBag)) + "Photo/Detail/") + photos.PhotoID));
                }
            }
            return base.View(topPhotoPostByType);
        }

        [HttpPost]
        public ActionResult ShareGoodsWaterfall(string orderby, int startIndex)
        {
            PhotoList model = new PhotoList();
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            int intValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetIntValueByCache("SNS_ShareGood_Category");
            if (intValueByCache == -1)
            {
                intValueByCache = 0xa7;
            }
            if (this.bllPhotos.GetRecordCount("Status=1 and  CategoryId=" + intValueByCache) < 1)
            {
                return new EmptyResult();
            }
            string str3 = orderby;
            if (str3 != null)
            {
                if (!(str3 == "hot"))
                {
                    if (str3 == "popular")
                    {
                        orderby = "FavouriteCount desc";
                        goto Label_010A;
                    }
                }
                else
                {
                    orderby = "CommentCount desc";
                    goto Label_010A;
                }
            }
            orderby = "";
        Label_010A:
            model.PhotoListWaterfall = this.bllPhotos.GetCachePhotoListByPage(intValueByCache, orderby, startIndex, endIndex);
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (PostContent content in model.PhotoListWaterfall)
            {
                if (valueByCache != "true")
                {
                    content.StaticUrl = (string) (((((dynamic) base.ViewBag).BasePath + ((content.Type == 1) ? "Product/" : "Photo/")) + "Detail/") + content.TargetId);
                    continue;
                }
                string str2 = "";
                if (content.Type == 1)
                {
                    Maticsoft.Model.SNS.Products modelByCache = this.productBll.GetModelByCache(content.TargetId);
                    if ((modelByCache == null) || string.IsNullOrWhiteSpace(modelByCache.StaticUrl))
                    {
                    }
                    str2 = (<ShareGoodsWaterfall>o__SiteContainer1c.<>p__Site26 != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <ShareGoodsWaterfall>o__SiteContainer1c.<>p__Site26.Target(<ShareGoodsWaterfall>o__SiteContainer1c.<>p__Site26, base.ViewBag)) + "Product/Detail/") + content.TargetId));
                }
                else
                {
                    Maticsoft.Model.SNS.Photos photos = this.bllPhotos.GetModelByCache((int) content.TargetId);
                    if ((photos == null) || string.IsNullOrWhiteSpace(photos.StaticUrl))
                    {
                    }
                    str2 = (<ShareGoodsWaterfall>o__SiteContainer1c.<>p__Site2a != null) ? photos.StaticUrl : ((string) ((((dynamic) <ShareGoodsWaterfall>o__SiteContainer1c.<>p__Site2a.Target(<ShareGoodsWaterfall>o__SiteContainer1c.<>p__Site2a, base.ViewBag)) + "Photo/Detail/") + content.TargetId));
                }
                content.StaticUrl = str2;
            }
            return base.View(base.CurrentThemeViewPath + "Photo/PhotoListWaterfall.cshtml", model);
        }
    }
}


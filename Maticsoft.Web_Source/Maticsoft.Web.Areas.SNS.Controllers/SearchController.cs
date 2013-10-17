namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.ViewModel.SNS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class SearchController : SNSControllerBase
    {
        private int _baseAlbumPageSize = 0x10;
        private int _basePageSize = 6;
        private int _waterfallAlbumSize = 0x10;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 30;

        public SearchController()
        {
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
            this._baseAlbumPageSize = base.FallInitDataSize;
            this._waterfallAlbumSize = base.FallDataSize;
        }

        public ActionResult Albums(int pageIndex = 1)
        {
            ((dynamic) base.ViewBag).Title = "专辑搜索";
            string inputString = string.IsNullOrWhiteSpace(base.Request.Params["keyword"]) ? "" : base.Server.UrlDecode(base.Request.Params["keyword"]);
            string orderby = string.IsNullOrWhiteSpace(base.Request.Params["type"]) ? "hot" : base.Request.Params["type"];
            inputString = InjectionFilter.Filter(inputString);
            this.LogKeyWord(inputString);
            int pageSize = this._baseAlbumPageSize + this._waterfallAlbumSize;
            ((dynamic) base.ViewBag).BasePageSize = this._baseAlbumPageSize;
            ((dynamic) base.ViewBag).sequence = orderby;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex > 1) ? ((startIndex + this._baseAlbumPageSize) - 1) : this._baseAlbumPageSize;
            int countByKeyWard = 0;
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            countByKeyWard = albums.GetCountByKeyWard(inputString);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > countByKeyWard) ? countByKeyWard : num5;
            if (countByKeyWard < 1)
            {
                return base.View("Album");
            }
            PagedList<AlbumIndex> model = albums.GetListByKeyWord(inputString, orderby, startIndex, endIndex, base.UserAlbumDetailType).ToPagedList<AlbumIndex>(pageIndex, pageSize, new int?(countByKeyWard));
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("AlbumList", model);
            }
            return base.View("Album", model);
        }

        public ActionResult Groups(int? pageIndex)
        {
            ((dynamic) base.ViewBag).Title = "小组搜索";
            string inputString = string.IsNullOrWhiteSpace(base.Request.Params["keyword"]) ? "" : base.Server.UrlDecode(base.Request.Params["keyword"]);
            string sequence = string.IsNullOrWhiteSpace(base.Request.Params["type"]) ? "hot" : base.Request.Params["type"];
            int rec = string.IsNullOrWhiteSpace(base.Request.Params["Rec"]) ? -1 : Globals.SafeInt(base.Request.Params["Rec"], -1);
            inputString = InjectionFilter.Filter(inputString);
            Maticsoft.BLL.SNS.Groups groups = new Maticsoft.BLL.SNS.Groups();
            GroupSearch model = new GroupSearch();
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int pageSize = 10;
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex.Value * pageSize;
            int totalItemCount = 0;
            totalItemCount = groups.GetCountByKeyWord(inputString, rec);
            int? nullable = pageIndex;
            PagedList<Maticsoft.Model.SNS.Groups> list = new PagedList<Maticsoft.Model.SNS.Groups>(groups.GetGroupListByKeyWord(startIndex, endIndex, sequence, inputString, rec), nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, totalItemCount);
            model.SearchList = list;
            model.RecommandList = groups.GetGroupListByRecommendType(3, EnumHelper.GroupRecommend.Index);
            model.HotList = groups.GetHotGroupList(3);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/Search/GroupList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/Search/Groups.cshtml", model);
        }

        public void LogKeyWord(string q)
        {
            Maticsoft.BLL.SNS.SearchWordLog log = new Maticsoft.BLL.SNS.SearchWordLog();
            Maticsoft.Model.SNS.SearchWordLog model = new Maticsoft.Model.SNS.SearchWordLog();
            if (!string.IsNullOrWhiteSpace(q))
            {
                model.CreatedDate = DateTime.Now;
                if (base.currentUser != null)
                {
                    model.CreatedNickName = base.currentUser.NickName;
                    model.CreatedUserId = base.currentUser.UserID;
                }
                model.SearchWord = q;
                log.Add(model);
            }
        }

        public ActionResult Photo(int? pageIndex)
        {
            ((dynamic) base.ViewBag).Title = "图片搜索";
            string inputString = string.IsNullOrWhiteSpace(base.Request.Params["keyword"]) ? "" : base.Server.UrlDecode(base.Request.Params["keyword"]);
            string orderby = string.IsNullOrWhiteSpace(base.Request.Params["type"]) ? "hot" : base.Request.Params["type"];
            inputString = InjectionFilter.Filter(inputString);
            this.LogKeyWord(inputString);
            PhotoList model = new PhotoList();
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int searchCountByQ = 0;
            searchCountByQ = photos.GetSearchCountByQ(inputString);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > searchCountByQ) ? searchCountByQ : num5;
            if (searchCountByQ >= 1)
            {
                ((dynamic) base.ViewBag).sequence = orderby;
                int? nullable = pageIndex;
                model.PhotoPagedList = photos.GetListByKeyWord(inputString, orderby, startIndex, endIndex, "").ToPagedList<PostContent>(nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, new int?(searchCountByQ));
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
                        content.StaticUrl = string.IsNullOrWhiteSpace(content.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + content.TargetId)) : content.StaticUrl;
                    }
                }
                if (base.Request.IsAjaxRequest())
                {
                    return this.PartialView("PhotoList", model);
                }
            }
            return base.View(model);
        }

        public ActionResult Product(int pageIndex = 1)
        {
            ((dynamic) base.ViewBag).Title = "商品搜索";
            string inputString = string.IsNullOrWhiteSpace(base.Request.Params["keyword"]) ? "" : base.Server.UrlDecode(base.Request.Params["keyword"]);
            string str2 = string.IsNullOrWhiteSpace(base.Request.Params["type"]) ? "hot" : base.Request.Params["type"];
            inputString = InjectionFilter.Filter(inputString);
            this.LogKeyWord(inputString);
            ProductCategory model = new ProductCategory();
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            ProductQuery query = new ProductQuery {
                Order = str2
            };
            ((dynamic) base.ViewBag).sequence = str2;
            query.Keywords = inputString;
            query.CategoryID = -1;
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int productCount = 0;
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = pageIndex * pageSize;
            query.QueryType = 0;
            productCount = products.GetProductCount(query);
            if (productCount >= 1)
            {
                query.QueryType = 1;
                model.ProductPagedList = products.GetProductByPage(query, startIndex, endIndex).ToPagedList<Maticsoft.Model.SNS.Products>(pageIndex, pageSize, new int?(productCount));
                string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                if ((model.ProductPagedList != null) && (model.ProductPagedList.Count > 0))
                {
                    foreach (Maticsoft.Model.SNS.Products products2 in model.ProductPagedList)
                    {
                        if (valueByCache != "true")
                        {
                            products2.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products2.ProductID);
                            continue;
                        }
                        products2.StaticUrl = string.IsNullOrWhiteSpace(products2.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products2.ProductID)) : products2.StaticUrl;
                    }
                }
                if (base.Request.IsAjaxRequest())
                {
                    return this.PartialView("ProductList", model);
                }
            }
            return base.View(model);
        }

        public ActionResult Topics(string keyword, string type, int? pageIndex)
        {
            ((dynamic) base.ViewBag).Title = "帖子搜索";
            keyword = InjectionFilter.Filter(keyword);
            new Maticsoft.BLL.SNS.Groups();
            Maticsoft.BLL.SNS.GroupTopics topics = new Maticsoft.BLL.SNS.GroupTopics();
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int pageSize = 10;
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex.Value * pageSize;
            int totalItemCount = 0;
            totalItemCount = topics.GetCountByKeyWord(keyword, -1);
            int? nullable = pageIndex;
            PagedList<Maticsoft.Model.SNS.GroupTopics> model = new PagedList<Maticsoft.Model.SNS.GroupTopics>(topics.SearchTopicByKeyWord(startIndex, endIndex, keyword, 0, type), nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/Search/TopicList.cshtml", model);
            }
            ((dynamic) base.ViewBag).sequence = (type == "newreply") ? "newreply" : "newpost";
            return base.View(base.CurrentThemeViewPath + "/Search/Topic.cshtml", model);
        }

        public ActionResult User(int? page)
        {
            ((dynamic) base.ViewBag).Title = "用户搜索";
            string inputString = string.IsNullOrWhiteSpace(base.Request.Params["keyword"]) ? "" : base.Server.UrlDecode(base.Request.Params["keyword"]);
            inputString = InjectionFilter.Filter(inputString);
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            UsersExp exp = new UsersExp();
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int userCountByKeyWord = exp.GetUserCountByKeyWord(inputString);
            PagedList<UsersExpModel> model = null;
            List<UsersExpModel> items = exp.GetUserListByKeyWord(inputString, "", startIndex, endIndex);
            if (base.currentUser != null)
            {
                Action<UsersExpModel> action = null;
                List<Maticsoft.Model.SNS.UserShip> shipList = ship.GetModelList("ActiveUserID = " + base.currentUser.UserID);
                if ((shipList != null) && (shipList.Count > 0))
                {
                    if (action == null)
                    {
                        action = delegate (UsersExpModel item) {
                            item.IsFellow = this.UserIsFellow(item.UserID, shipList);
                        };
                    }
                    items.ForEach(action);
                }
            }
            if ((items != null) && (items.Count > 0))
            {
                int? nullable = page;
                model = new PagedList<UsersExpModel>(items, nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, userCountByKeyWord);
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/Search/UserList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/Search/User.cshtml", model);
        }

        public bool UserIsFellow(int UserId, List<Maticsoft.Model.SNS.UserShip> shipList)
        {
            return (shipList.Count<Maticsoft.Model.SNS.UserShip>(item => (item.PassiveUserID == UserId)) > 0);
        }

        [HttpPost]
        public ActionResult WaterfallAlbumListData(string keyword, string type, int startIndex)
        {
            keyword = InjectionFilter.Filter(keyword);
            ((dynamic) base.ViewBag).BasePageSize = this._baseAlbumPageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            if (albums.GetCountByKeyWard(keyword) < 1)
            {
                return new EmptyResult();
            }
            List<AlbumIndex> model = albums.GetListByKeyWord(keyword, type, startIndex, endIndex, base.UserAlbumDetailType);
            return base.View("AlbumListWaterfall", model);
        }

        [HttpPost]
        public ActionResult WaterfallPhotoListData(string keyword, string type, int startIndex)
        {
            keyword = InjectionFilter.Filter(keyword);
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            PhotoList model = new PhotoList();
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            if (photos.GetSearchCountByQ(keyword) < 1)
            {
                return new EmptyResult();
            }
            model.PhotoListWaterfall = photos.GetListByKeyWord(keyword, type, startIndex, endIndex, "");
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if ((model.PhotoListWaterfall != null) && (model.PhotoListWaterfall.Count > 0))
            {
                foreach (PostContent content in model.PhotoListWaterfall)
                {
                    if (valueByCache != "true")
                    {
                        content.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + content.TargetId);
                        continue;
                    }
                    content.StaticUrl = string.IsNullOrWhiteSpace(content.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Photo/Detail/") + content.TargetId)) : content.StaticUrl;
                }
            }
            return base.View(base.CurrentThemeViewPath + "/Photo/PhotoListWaterfall.cshtml", model);
        }

        [HttpPost]
        public ActionResult WaterfallProductListData(string keyword, string type, int startIndex)
        {
            keyword = InjectionFilter.Filter(keyword);
            ProductCategory model = new ProductCategory();
            ProductQuery query = new ProductQuery();
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            query.Order = type;
            query.Keywords = keyword;
            query.CategoryID = -1;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            if (products.GetProductCount(query) < 1)
            {
                return new EmptyResult();
            }
            model.ProductListWaterfall = products.GetProductByPage(query, startIndex, endIndex);
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if ((model.ProductListWaterfall != null) && (model.ProductListWaterfall.Count > 0))
            {
                foreach (Maticsoft.Model.SNS.Products products2 in model.ProductListWaterfall)
                {
                    if (valueByCache != "true")
                    {
                        products2.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products2.ProductID);
                        continue;
                    }
                    products2.StaticUrl = string.IsNullOrWhiteSpace(products2.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products2.ProductID)) : products2.StaticUrl;
                }
            }
            return base.View("ProductListWaterfall", model);
        }
    }
}


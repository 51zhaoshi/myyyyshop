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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Webdiyer.WebControls.Mvc;

    public class ProductController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDataCount = 1;
        private int _waterfallSize = 0x20;
        private Maticsoft.BLL.SNS.Categories CateBll = new Maticsoft.BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.Comments ComBll = new Maticsoft.BLL.SNS.Comments();
        private int commentPagesize = 3;
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        private Maticsoft.BLL.SNS.Photos PhotoBll = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.SNS.Products ProductBll = new Maticsoft.BLL.SNS.Products();
        private Maticsoft.BLL.SNS.SearchWordTop SearchBll = new Maticsoft.BLL.SNS.SearchWordTop();
        private Maticsoft.BLL.SNS.TagType TagTypeBll = new Maticsoft.BLL.SNS.TagType();

        public ProductController()
        {
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
            this.commentPagesize = base.CommentDataSize;
        }

        public ActionResult AjaxUpdateClickCount(FormCollection fc)
        {
            int prouductId = Globals.SafeInt(fc["ProductId"], 0);
            this.ProductBll.UpdateClickCount(prouductId);
            return new EmptyResult();
        }

        public ActionResult Detail(int pid)
        {
            if (!this.ProductBll.Exists((long) pid))
            {
                return base.RedirectToAction("Index", "Error");
            }
            TargetDetail model = new TargetDetail();
            ProductQuery query = new ProductQuery();
            Maticsoft.BLL.SNS.Tags tags = new Maticsoft.BLL.SNS.Tags();
            int type = 1;
            int num2 = 2;
            model = this.ProductBll.GetTargetAssiationInfo(pid);
            query.IsTopCategory = true;
            query.CategoryID = new int?(this.CateBll.GetTopCidByChildCid(model.Product.CategoryID.HasValue ? model.Product.CategoryID.Value : 0));
            query.Order = "hot";
            model.RecommandProduct = this.ProductBll.GetProductByPage(query, 1, 0x10);
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (Maticsoft.Model.SNS.Products products in model.RecommandProduct)
            {
                if (valueByCache != "true")
                {
                    products.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID);
                    continue;
                }
                products.StaticUrl = string.IsNullOrWhiteSpace(products.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID)) : products.StaticUrl;
            }
            model.ProductTagList = tags.GetHotTags(0x10);
            Maticsoft.BLL.SNS.UserFavourite favourite = new Maticsoft.BLL.SNS.UserFavourite();
            model.FavCount = favourite.GetFavCountByTargetId(pid, type);
            model.FavUserList = favourite.GetFavUserByTargetId(pid, type, 0x18);
            model.Commentcount = this.ComBll.GetCommentCount(num2, pid);
            model.CommentPageSize = this.commentPagesize;
            string[] strArray = (from item in model.RecommandProduct
                where (item.CommentCount > 0) && !string.IsNullOrEmpty(item.TopCommentsId)
                select item.TopCommentsId).Distinct<string>().ToArray<string>();
            string idStr = string.Join(",", strArray).TrimEnd(new char[] { ',' });
            model.CommentList = new Maticsoft.BLL.SNS.Comments().GetCommentByIds(idStr, 2);
            IPageSetting pageSetting = PageSetting.GetPageSetting("ProductDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(new string[][] { new string[] { "{cname}", model.Targetname }, new string[] { "{ctag}", model.Tags } });
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            ((dynamic) base.ViewBag).TaoCode = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_SNS_TaoBaoCode", ApplicationKeyType.OpenAPI);
            return base.View(model);
        }

        public ActionResult GetCount(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                return base.Content("No");
            }
            int targetId = Globals.SafeInt(Fm["ProductId"], 0);
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            int favCountByTargetId = new Maticsoft.BLL.SNS.UserFavourite().GetFavCountByTargetId(targetId, 1);
            int commentCount = comments.GetCommentCount(2, targetId);
            TargetDetail targetAssiationInfo = this.ProductBll.GetTargetAssiationInfo(targetId);
            return base.Content(string.Concat(new object[] { targetAssiationInfo.Favouritecount, "|", targetAssiationInfo.PvCount, "|", commentCount, "|", favCountByTargetId }));
        }

        public ActionResult GetListCounts(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ProductIds"]))
            {
                return base.Content("No");
            }
            string[] strArray = Fm["ProductIds"].Split(new char[] { ',' });
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            new Maticsoft.BLL.SNS.Photos();
            string content = "";
            int num = 0;
            foreach (string str3 in strArray)
            {
                int pid = Globals.SafeInt(str3, 0);
                TargetDetail targetAssiationInfo = this.ProductBll.GetTargetAssiationInfo(pid);
                int commentCount = comments.GetCommentCount(2, pid);
                if (num == 0)
                {
                    content = string.Concat(new object[] { pid, ",", targetAssiationInfo.Favouritecount, ",", commentCount });
                }
                else
                {
                    content = string.Concat(new object[] { content, "|", pid, ",", targetAssiationInfo.Favouritecount, ",", commentCount });
                }
                num++;
            }
            return base.Content(content);
        }

        public ActionResult GetProductIsR()
        {
            bool boolValueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Product_OpenRedirect");
            return base.Content(boolValueByCache.ToString());
        }

        public ActionResult Index(string cname, int topcid, int cid, int minprice, int maxprice, string sequence, string color, int? pageIndex)
        {
            ProductCategory cateListByParentId = this.CateBll.GetCateListByParentId(topcid);
            ProductQuery query = new ProductQuery();
            cateListByParentId.TagsList = this.TagTypeBll.GetTagListByCid(topcid, 0);
            cateListByParentId.CurrentSequence = sequence;
            cateListByParentId.CurrentMinPrice = minprice;
            cateListByParentId.CurrentMaxPrice = maxprice;
            cateListByParentId.CurrentCateName = cname;
            if ((topcid == 0) && (cid == 0))
            {
                cateListByParentId.KeyWordList = this.SearchBll.GetRecommadKeyWordList();
                Maticsoft.Model.SNS.Categories model = this.CateBll.GetModel(cname);
                cateListByParentId.CurrentCateName = "社区热搜";
                if (model != null)
                {
                    query.CategoryID = new int?(model.CategoryId);
                }
                else
                {
                    query.Keywords = (cname != "all") ? cname : null;
                }
            }
            else if (cid > 0)
            {
                query.CategoryID = new int?(cid);
            }
            else
            {
                query.CategoryID = new int?(topcid);
                query.Tags = cname;
            }
            query.IsTopCategory = cid == topcid;
            query.MaxPrice = new decimal?(maxprice);
            query.MinPrice = new decimal?(minprice);
            query.Order = sequence;
            query.Color = color;
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int productCount = 0;
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int? nullable = pageIndex;
            int num5 = pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() * num5) : null;
            string str = "{";
            foreach (KeyValuePair<string, object> pair in base.Request.RequestContext.RouteData.Values)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, pair.Key, ":'", pair.Value, "'," });
            }
            str = str.TrimEnd(new char[] { ',' }) + "}";
            ((dynamic) base.ViewBag).DataParam = str;
            query.QueryType = 0;
            productCount = this.ProductBll.GetProductCount(query);
            IPageSetting pageSetting = PageSetting.GetPageSetting("ProductList", ApplicationKeyType.SNS);
            pageSetting.Replace(new string[][] { new string[] { "{cname}", cateListByParentId.CurrentCateName } });
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (productCount >= 1)
            {
                query.QueryType = 1;
                int? nullable3 = pageIndex;
                cateListByParentId.ProductPagedList = this.ProductBll.GetProductByPage(query, startIndex, endIndex).ToPagedList<Maticsoft.Model.SNS.Products>(nullable3.HasValue ? nullable3.GetValueOrDefault() : 1, pageSize, new int?(productCount));
                string[] strArray = (from item in cateListByParentId.ProductPagedList
                    where (item.CommentCount > 0) && !string.IsNullOrEmpty(item.TopCommentsId)
                    select item.TopCommentsId).Distinct<string>().ToArray<string>();
                string idStr = string.Join(",", strArray).TrimEnd(new char[] { ',' });
                cateListByParentId.CommentList = new Maticsoft.BLL.SNS.Comments().GetCommentByIds(idStr, 2);
                string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                foreach (Maticsoft.Model.SNS.Products products in cateListByParentId.ProductPagedList)
                {
                    if (valueByCache != "true")
                    {
                        products.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID);
                        continue;
                    }
                    products.StaticUrl = string.IsNullOrWhiteSpace(products.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID)) : products.StaticUrl;
                }
                if (base.Request.IsAjaxRequest())
                {
                    return this.PartialView("ProductList", cateListByParentId);
                }
            }
            return base.View(cateListByParentId);
        }

        public ActionResult R(int id)
        {
            string productUrlByCache = this.ProductBll.GetProductUrlByCache((long) id);
            if (string.IsNullOrWhiteSpace(productUrlByCache))
            {
                return base.RedirectToAction("Detail", new { pid = id });
            }
            return this.Redirect(productUrlByCache);
        }

        [OutputCache(VaryByParam="*", Duration=300), HttpPost]
        public ActionResult WaterfallProductListData(string cname, int topcid, int cid, int minprice, int maxprice, string sequence, string color, int startIndex)
        {
            ProductCategory model = new ProductCategory();
            ProductQuery query = new ProductQuery();
            if ((topcid == 0) && (cid == 0))
            {
                model.KeyWordList = this.SearchBll.GetRecommadKeyWordList();
                Maticsoft.Model.SNS.Categories categories = this.CateBll.GetModel(cname);
                model.CurrentCateName = "社区热搜";
                if (categories != null)
                {
                    query.CategoryID = new int?(categories.CategoryId);
                }
                else
                {
                    query.Keywords = (cname != "all") ? cname : null;
                }
            }
            else if (cid > 0)
            {
                query.CategoryID = new int?(cid);
            }
            else
            {
                query.CategoryID = new int?(topcid);
                query.Tags = cname;
            }
            query.MaxPrice = new decimal?(maxprice);
            query.MinPrice = new decimal?(minprice);
            query.Order = sequence;
            query.IsTopCategory = cid == topcid;
            query.Color = color;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDataCount) - 1) : this._waterfallDataCount;
            if (this.ProductBll.GetProductCount(query) < 1)
            {
                return new EmptyResult();
            }
            model.ProductListWaterfall = this.ProductBll.GetProductByPage(query, startIndex, endIndex);
            string[] strArray = (from item in model.ProductListWaterfall
                where (item.CommentCount > 0) && !string.IsNullOrEmpty(item.TopCommentsId)
                select item.TopCommentsId).Distinct<string>().ToArray<string>();
            string idStr = string.Join(",", strArray).TrimEnd(new char[] { ',' });
            model.CommentList = new Maticsoft.BLL.SNS.Comments().GetCommentByIds(idStr, 2);
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (Maticsoft.Model.SNS.Products products in model.ProductListWaterfall)
            {
                if (valueByCache != "true")
                {
                    products.StaticUrl = (string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID);
                    continue;
                }
                products.StaticUrl = string.IsNullOrWhiteSpace(products.StaticUrl) ? ((string) ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + products.ProductID)) : products.StaticUrl;
            }
            return base.View("ProductListWaterfall", model);
        }
    }
}


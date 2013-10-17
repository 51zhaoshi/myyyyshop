namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.Components.Setting;
    using Maticsoft.Json;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.Shop;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class ProductController : ShopControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDataCount = 1;
        private int _waterfallSize = 0x20;
        private Maticsoft.BLL.Shop.Products.BrandInfo brandBll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryManage = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.Products.ProductConsults conBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        private OrderItems itemBll = new OrderItems();
        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();

        public ProductController()
        {
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
        }

        public PartialViewResult AttrList(int cid, int Top = -1, string viewName = "_AttrList")
        {
            List<Maticsoft.Model.Shop.Products.AttributeInfo> attributeListByCateID = new Maticsoft.BLL.Shop.Products.AttributeInfo().GetAttributeListByCateID(cid, true);
            return this.PartialView(viewName, attributeListByCateID);
        }

        public PartialViewResult AttrValues(int AttrId, int Top = -1, string viewName = "_AttrValues")
        {
            List<Maticsoft.Model.Shop.Products.AttributeValue> modelList = new Maticsoft.BLL.Shop.Products.AttributeValue().GetModelList(" AttributeId=" + AttrId);
            return this.PartialView(viewName, modelList);
        }

        public PartialViewResult BrandList(int Cid = 0, int top = 10, string viewName = "_BrandList")
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> model = new List<Maticsoft.Model.Shop.Products.BrandInfo>();
            ((dynamic) base.ViewBag).Cid = Cid;
            model = this.brandBll.GetBrandsByCateId(Cid, true, top);
            return this.PartialView(viewName, model);
        }

        public ActionResult Detail(int id, string viewName = "Detail")
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            ProductModel model = new ProductModel {
                ProductInfo = this.productManage.GetModel((long) id)
            };
            model.ProductImages = new Maticsoft.BLL.Shop.Products.ProductImage().ProductImagesList((long) id);
            model.ProductSkus = this.skuBll.GetProductSkuInfo((long) id);
            Maticsoft.BLL.Shop.Products.BrandInfo info = new Maticsoft.BLL.Shop.Products.BrandInfo();
            if (model.ProductInfo == null)
            {
                return base.RedirectToAction("Index", "Home");
            }
            Maticsoft.Model.Shop.Products.BrandInfo modelByCache = info.GetModelByCache(model.ProductInfo.BrandId);
            if (modelByCache != null)
            {
                ((dynamic) base.ViewBag).BrandName = modelByCache.BrandName;
            }
            Maticsoft.Model.Shop.Products.ProductCategories categoryModel = new Maticsoft.BLL.Shop.Products.ProductCategories().GetModel((long) id);
            if (categoryModel != null)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == categoryModel.CategoryId;
                }
                Maticsoft.Model.Shop.Products.CategoryInfo info3 = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                if (info3 != null)
                {
                    string[] cateIds = info3.Path.Split(new char[] { '|' });
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> list2 = (from c in allCateList
                        where cateIds.Contains<string>(c.CategoryId.ToString())
                        orderby c.Depth
                        select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
                    StringBuilder builder = new StringBuilder();
                    StringBuilder builder2 = new StringBuilder();
                    if ((list2 != null) && (list2.Count > 0))
                    {
                        foreach (Maticsoft.Model.Shop.Products.CategoryInfo info4 in list2)
                        {
                            builder2.AppendFormat("<a href='/Product/" + info4.CategoryId + "'>{0}</a> > ", info4.Name);
                        }
                    }
                    ((dynamic) base.ViewBag).Cid = categoryModel.CategoryId;
                    ((dynamic) base.ViewBag).PathInfo = builder.ToString();
                    ((dynamic) base.ViewBag).CategoryStr = builder2.ToString();
                }
            }
            PageSetting setting = PageSetting.GetProductSetting(model.ProductInfo, "Product", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            ((dynamic) base.ViewBag).ConsultCount = this.conBll.GetRecordCount("Status=1 and ProductId=" + id);
            ((dynamic) base.ViewBag).CommentCount = this.reviewsBll.GetRecordCount("Status=1 and ProductId=" + id);
            return base.View(viewName, model);
        }

        [HttpPost]
        public ActionResult GetSKUInfos(long productId)
        {
            if (productId < 1L)
            {
                return new EmptyResult();
            }
            ProductSKUModel productSKUInfoByProductId = this.skuBll.GetProductSKUInfoByProductId(productId);
            if (productSKUInfoByProductId == null)
            {
                return new EmptyResult();
            }
            if (((productSKUInfoByProductId.ListSKUInfos == null) || (productSKUInfoByProductId.ListSKUInfos.Count < 1)) || (productSKUInfoByProductId.ListSKUItems == null))
            {
                return new EmptyResult();
            }
            ((dynamic) base.ViewBag).HasSKU = true;
            if (productSKUInfoByProductId.ListSKUItems.Count == 0)
            {
                return new EmptyResult();
            }
            return base.Content(this.SKUInfoToJson(productSKUInfoByProductId.ListSKUInfos).ToString());
        }

        public ActionResult Index(int cid = 0, int brandid = 0, string attrvalues = "0", string mod = "default", string price = "", int? pageIndex = 1, string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            ProductListModel model = new ProductListModel();
            Maticsoft.Model.Shop.Products.CategoryInfo cateModel = null;
            if (cid > 0)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == cid;
                }
                cateModel = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                if (cateModel != null)
                {
                    string[] path_arr = cateModel.Path.Split(new char[] { '|' });
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> list2 = (from c in allCateList
                        where path_arr.Contains<string>(c.CategoryId.ToString())
                        orderby c.Depth
                        select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
                    model.CategoryPathList = list2;
                    model.CurrentCateName = cateModel.Name;
                }
            }
            model.CurrentCid = cid;
            model.CurrentMod = mod;
            string str = "{";
            foreach (KeyValuePair<string, object> pair in base.Request.RequestContext.RouteData.Values)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, pair.Key, ":'", pair.Value, "'," });
            }
            str = str.TrimEnd(new char[] { ',' }) + "}";
            ((dynamic) base.ViewBag).DataParam = str;
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int num4 = this.productManage.GetProductsCountEx(cid, brandid, attrvalues, price);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int? nullable = pageIndex;
            int num5 = pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() * num5) : null;
            List<Maticsoft.Model.Shop.Products.ProductInfo> list3 = this.productManage.GetProductsListEx(cid, brandid, attrvalues, price, mod, startIndex, endIndex);
            IPageSetting setting = PageSetting.GetCategorySetting(cateModel, "Category", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            if (num4 >= 1)
            {
                int? nullable3 = pageIndex;
                model.ProductPagedList = list3.ToPagedList<Maticsoft.Model.Shop.Products.ProductInfo>(nullable3.HasValue ? nullable3.GetValueOrDefault() : 1, pageSize, new int?(num4));
                if (base.Request.IsAjaxRequest())
                {
                    return this.PartialView(ajaxViewName, model);
                }
            }
            return base.View(viewName, model);
        }

        public ActionResult ListWaterfall(int cid, int brandid, string attrvalues, string mod, string price, int startIndex, string viewName = "_ListWaterfall")
        {
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDataCount) - 1) : this._waterfallDataCount;
            int num2 = this.productManage.GetProductsCountEx(cid, brandid, attrvalues, price);
            this.categoryManage.GetModelByCache(cid);
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productManage.GetProductsListEx(cid, brandid, attrvalues, price, mod, startIndex, endIndex);
            if (num2 < 1)
            {
                return new EmptyResult();
            }
            return base.View(viewName, model);
        }

        public ActionResult OptionAttr(long productId, string viewName = "_OptionAttr")
        {
            if (productId < 1L)
            {
                return new EmptyResult();
            }
            Maticsoft.BLL.Shop.Products.AttributeInfo info = new Maticsoft.BLL.Shop.Products.AttributeInfo();
            return base.View(viewName, info.GetAttributeInfoListByProductId(productId));
        }

        public ActionResult OptionSKU(long productId, string viewName = "_OptionSKU")
        {
            if (productId < 1L)
            {
                return new EmptyResult();
            }
            ProductSKUModel productSKUInfoByProductId = this.skuBll.GetProductSKUInfoByProductId(productId);
            if (productSKUInfoByProductId == null)
            {
                return new EmptyResult();
            }
            if (((productSKUInfoByProductId.ListSKUInfos == null) || (productSKUInfoByProductId.ListSKUInfos.Count < 1)) || (productSKUInfoByProductId.ListSKUItems == null))
            {
                return new EmptyResult();
            }
            ((dynamic) base.ViewBag).HasSKU = true;
            if (productSKUInfoByProductId.ListSKUItems.Count == 0)
            {
                ((dynamic) base.ViewBag).HasSKU = false;
                return base.View(viewName, productSKUInfoByProductId);
            }
            ((dynamic) base.ViewBag).SKUJson = this.SKUInfoToJson(productSKUInfoByProductId.ListSKUInfos).ToString();
            return base.View(viewName, productSKUInfoByProductId);
        }

        public PartialViewResult ProductCategory(int Cid, string viewName = "_ProductCategory", int Top = -1)
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> func2 = null;
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> func3 = null;
            List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(c => c.CategoryId == Cid);
            ((dynamic) base.ViewBag).CateName = (categoryInfo != null) ? categoryInfo.Name : "全部分类";
            ((dynamic) base.ViewBag).Cid = Cid;
            List<Maticsoft.Model.Shop.Products.CategoryInfo> model = new List<Maticsoft.Model.Shop.Products.CategoryInfo>();
            if ((categoryInfo != null) && !categoryInfo.HasChildren)
            {
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == categoryInfo.ParentCategoryId;
                }
                Maticsoft.Model.Shop.Products.CategoryInfo info = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                if (info != null)
                {
                    ((dynamic) base.ViewBag).CateName = info.Name;
                    ((dynamic) base.ViewBag).Cid = info.CategoryId;
                }
                if (func2 == null)
                {
                    func2 = c => c.ParentCategoryId == categoryInfo.ParentCategoryId;
                }
                model = allCateList.Where<Maticsoft.Model.Shop.Products.CategoryInfo>(func2).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            }
            else
            {
                if (func3 == null)
                {
                    func3 = c => c.ParentCategoryId == Cid;
                }
                model = allCateList.Where<Maticsoft.Model.Shop.Products.CategoryInfo>(func3).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            }
            return this.PartialView(viewName, model);
        }

        public PartialViewResult ProductComments(int id, int pageIndex = 1, string viewName = "_ProductComments")
        {
            int pageSize = 15;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            totalItemCount = this.reviewsBll.GetRecordCount("Status=1 and ProductId=" + id);
            ((dynamic) base.ViewBag).TotalCount = totalItemCount;
            if (totalItemCount == 0)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Shop.Products.ProductReviews> model = new PagedList<Maticsoft.Model.Shop.Products.ProductReviews>(this.reviewsBll.GetReviewsByPage((long) id, " CreatedDate desc", startIndex, endIndex), pageIndex, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        public PartialViewResult ProductConsult(int id, int pageIndex = 1, string viewName = "_ProductConsult")
        {
            int pageSize = 15;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            totalItemCount = this.conBll.GetRecordCount("Status=1 and ProductId=" + id);
            ((dynamic) base.ViewBag).TotalCount = totalItemCount;
            if (totalItemCount == 0)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Shop.Products.ProductConsults> model = new PagedList<Maticsoft.Model.Shop.Products.ProductConsults>(this.conBll.GetConsultationsByPage((long) id, " CreatedDate desc", startIndex, endIndex), pageIndex, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        public PartialViewResult ProductHotCom(string viewName = "_ProductHotCom", int top = 10)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productRanList = this.productManage.GetProductRanList(top);
            return this.PartialView(viewName, productRanList);
        }

        public PartialViewResult ProductLastView(string viewName = "_ProductLastView")
        {
            return base.PartialView(viewName);
        }

        public PartialViewResult ProductNew(string viewName = "_ProductNew", int top = 10)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productRanList = this.productManage.GetProductRanList(top);
            return this.PartialView(viewName, productRanList);
        }

        public PartialViewResult ProductRan(string viewName = "_ProductRan", int top = 10)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> productRanList = this.productManage.GetProductRanList(top);
            return this.PartialView(viewName, productRanList);
        }

        public PartialViewResult ProductRelation(int id, string viewName = "_ProductRelation", int top = 12)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productManage.RelatedProductsList((long) id, top);
            return this.PartialView(viewName, model);
        }

        public ActionResult ProSaleDetail(int id, string viewName = "ProSaleDetail")
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            ProductModel model = new ProductModel {
                ProductInfo = this.productManage.GetProSaleModel(id)
            };
            if (model.ProductInfo == null)
            {
                return base.RedirectToAction("Index", "Home");
            }
            model.ProductImages = new Maticsoft.BLL.Shop.Products.ProductImage().ProductImagesList((long) id);
            model.ProductSkus = this.skuBll.GetProductSkuInfo((long) id);
            Maticsoft.Model.Shop.Products.BrandInfo modelByCache = new Maticsoft.BLL.Shop.Products.BrandInfo().GetModelByCache(model.ProductInfo.BrandId);
            if (modelByCache != null)
            {
                ((dynamic) base.ViewBag).BrandName = modelByCache.BrandName;
            }
            Maticsoft.Model.Shop.Products.ProductCategories categoryModel = new Maticsoft.BLL.Shop.Products.ProductCategories().GetModel((long) id);
            if (categoryModel != null)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == categoryModel.CategoryId;
                }
                Maticsoft.Model.Shop.Products.CategoryInfo info3 = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                if (info3 != null)
                {
                    string[] cateIds = info3.Path.Split(new char[] { '|' });
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> list2 = (from c in allCateList
                        where cateIds.Contains<string>(c.CategoryId.ToString())
                        orderby c.Depth
                        select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
                    StringBuilder builder = new StringBuilder();
                    StringBuilder builder2 = new StringBuilder();
                    if ((list2 != null) && (list2.Count > 0))
                    {
                        foreach (Maticsoft.Model.Shop.Products.CategoryInfo info4 in list2)
                        {
                            builder2.AppendFormat("<a href='/Product/" + info4.CategoryId + "'>{0}</a> > ", info4.Name);
                        }
                    }
                    ((dynamic) base.ViewBag).Cid = categoryModel.CategoryId;
                    ((dynamic) base.ViewBag).PathInfo = builder.ToString();
                    ((dynamic) base.ViewBag).CategoryStr = builder2.ToString();
                }
            }
            PageSetting setting = PageSetting.GetProductSetting(model.ProductInfo, "Product", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            ((dynamic) base.ViewBag).ConsultCount = this.conBll.GetRecordCount("Status=1 and ProductId=" + id);
            ((dynamic) base.ViewBag).CommentCount = this.reviewsBll.GetRecordCount("Status=1 and ProductId=" + id);
            return base.View(viewName, model);
        }

        public PartialViewResult SaleRecord(int id, int pageIndex = 1, string viewName = "_SaleRecord")
        {
            int pageSize = 15;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            totalItemCount = this.itemBll.GetSaleRecordCount((long) id);
            if (totalItemCount == 0)
            {
                return base.PartialView(viewName);
            }
            PagedList<Maticsoft.ViewModel.Shop.SaleRecord> model = new PagedList<Maticsoft.ViewModel.Shop.SaleRecord>(this.itemBll.GetSaleRecordByPage((long) id, "", startIndex, endIndex), pageIndex, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(viewName, model);
            }
            return this.PartialView(viewName, model);
        }

        private JsonObject SKUInfoToJson(List<Maticsoft.Model.Shop.Products.SKUInfo> list)
        {
            Action<Maticsoft.Model.Shop.Products.SKUItem> action = null;
            long[] key;
            int index;
            if ((list == null) || (list.Count < 1))
            {
                return null;
            }
            JsonObject obj2 = new JsonObject();
            JsonObject obj3 = new JsonObject();
            foreach (Maticsoft.Model.Shop.Products.SKUInfo info in list)
            {
                if (((info.SkuItems != null) && (info.SkuItems.Count >= 1)) && (info.Stock >= 1))
                {
                    key = new long[info.SkuItems.Count];
                    index = 0;
                    if (action == null)
                    {
                        action = delegate (Maticsoft.Model.Shop.Products.SKUItem xx) {
                            key[index++] = xx.ValueId;
                        };
                    }
                    info.SkuItems.ForEach(action);
                    obj3.Accumulate(string.Join<long>(",", key), new { sku = info.SKU, count = info.Stock, price = info.SalePrice });
                }
            }
            list.Sort((Comparison<Maticsoft.Model.Shop.Products.SKUInfo>) ((x, y) => x.SalePrice.CompareTo(y.SalePrice)));
            obj2.Put("Default", new { minPrice = list[0].SalePrice, maxPrice = list[list.Count - 1].SalePrice });
            obj2.Put("SKUDATA", obj3);
            return obj2;
        }

        public PartialViewResult WholeSale(int ProductId, string viewName = "_WholeSale")
        {
            SalesModel salesRuleByCache = new SalesModel();
            if (base.currentUser != null)
            {
                salesRuleByCache = new SalesRuleProduct().GetSalesRuleByCache((long) ProductId, base.currentUser.UserID);
            }
            return this.PartialView(viewName, salesRuleByCache);
        }
    }
}


namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.Shop.Products;
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

    public class ProductController : MobileControllerBase
    {
        private Maticsoft.BLL.Shop.Products.BrandInfo brandBll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        private Maticsoft.BLL.Shop.Products.ProductConsults conBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
        private OrderItems itemBll = new OrderItems();
        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.ProductReviews reviewsBll = new Maticsoft.BLL.Shop.Products.ProductReviews();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();

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

        public ActionResult CategoryList(int parentId = 0, string viewName = "CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> model = (from c in allCateList
                where c.ParentCategoryId == parentId
                orderby c.DisplaySequence
                select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            Maticsoft.Model.Shop.Products.CategoryInfo info = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(c => c.CategoryId == parentId);
            ((dynamic) base.ViewBag).ParentId = -1;
            if (info != null)
            {
                ((dynamic) base.ViewBag).CurrentName = info.Name;
                ((dynamic) base.ViewBag).ParentId = info.ParentCategoryId;
            }
            return base.View(viewName, model);
        }

        public ActionResult Comments(int id, int pageIndex = 1, string viewName = "Comments")
        {
            int pageSize = 15;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            ((dynamic) base.ViewBag).ProductName = this.productManage.GetProductName((long) id);
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            totalItemCount = this.reviewsBll.GetRecordCount("Status=1 and ProductId=" + id);
            ((dynamic) base.ViewBag).TotalCount = totalItemCount;
            if (totalItemCount == 0)
            {
                return base.View(viewName);
            }
            PagedList<Maticsoft.Model.Shop.Products.ProductReviews> model = new PagedList<Maticsoft.Model.Shop.Products.ProductReviews>(this.reviewsBll.GetReviewsByPage((long) id, " CreatedDate desc", startIndex, endIndex), pageIndex, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return base.View(viewName, model);
            }
            return base.View(viewName, model);
        }

        public ActionResult Consults(int id, int pageIndex = 1, string viewName = "Consults")
        {
            int pageSize = 4;
            ((dynamic) base.ViewBag).ProductName = this.productManage.GetProductName((long) id);
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

        public ActionResult Detail(int ProductId)
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            ProductModel model = new ProductModel {
                ProductInfo = this.productManage.GetModel((long) ProductId)
            };
            model.ProductImages = new Maticsoft.BLL.Shop.Products.ProductImage().ProductImagesList((long) ProductId);
            model.ProductSkus = this.skuBll.GetProductSkuInfo((long) ProductId);
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
            Maticsoft.Model.Shop.Products.ProductCategories categoryModel = new Maticsoft.BLL.Shop.Products.ProductCategories().GetModel((long) ProductId);
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
            ((dynamic) base.ViewBag).ConsultCount = this.conBll.GetRecordCount("Status=1 and ProductId=" + ProductId);
            ((dynamic) base.ViewBag).CommentCount = this.reviewsBll.GetRecordCount("Status=1 and ProductId=" + ProductId);
            return base.View(model);
        }

        public ActionResult Filter(int id = 0)
        {
            return base.View();
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

        public ActionResult Index(int cid = 0, int brandid = 0, string attrvalues = "0", string mod = "hot", string price = "", int? pageIndex = 1, string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            ProductListModel model = new ProductListModel();
            Maticsoft.Model.Shop.Products.CategoryInfo cateModel = null;
            ((dynamic) base.ViewBag).ParentId = 0;
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
                ((dynamic) base.ViewBag).ParentId = cateModel.ParentCategoryId;
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
            int pageSize = 15;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + pageSize) - 1) : pageSize;
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

        public ActionResult ProductDesc(int id)
        {
            Maticsoft.Model.Shop.Products.ProductInfo model = this.productManage.GetModel((long) id);
            if (model == null)
            {
                return base.RedirectToAction("Index", "Home");
            }
            PageSetting setting = PageSetting.GetProductSetting(model, "Product", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            return base.View(model);
        }

        public PartialViewResult ProductRelation(int id, string viewName = "_ProductRelation", int top = 12)
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productManage.RelatedProductsList((long) id, top);
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
    }
}


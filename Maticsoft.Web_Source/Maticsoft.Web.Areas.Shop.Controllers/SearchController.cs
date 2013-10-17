namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.Shop;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class SearchController : ShopControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDataCount = 1;
        private int _waterfallSize = 0x20;
        private Maticsoft.BLL.Shop.Products.BrandInfo brandBll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();

        public SearchController()
        {
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
        }

        public PartialViewResult BrandList(int Cid = 0, int productType = 0, int top = 10, string viewName = "_BrandList")
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> model = new List<Maticsoft.Model.Shop.Products.BrandInfo>();
            if (Cid > 0)
            {
                model = this.brandBll.GetBrandsByCateId(Cid, true, top);
            }
            else
            {
                model = this.brandBll.GetModelListByProductTypeId(productType, top);
            }
            return this.PartialView(viewName, model);
        }

        public ActionResult Index(int cid = 0, int brandid = 0, string keyword = "", string mod = "default", string price = "0-0", int? pageIndex = 1, string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            ProductListModel model = new ProductListModel();
            keyword = InjectionFilter.SqlFilter(keyword);
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return base.RedirectToAction("Index", "Home");
            }
            if (cid > 0)
            {
                Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> func = null;
                List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == cid;
                }
                Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                if (categoryInfo != null)
                {
                    if (func == null)
                    {
                        func = delegate (Maticsoft.Model.Shop.Products.CategoryInfo c) {
                            if (!categoryInfo.Path.Contains(c.Path + "|"))
                            {
                                return c.Path == categoryInfo.Path;
                            }
                            return true;
                        };
                    }
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> list2 = allCateList.Where<Maticsoft.Model.Shop.Products.CategoryInfo>(func).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
                    model.CategoryPathList = list2;
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
            int num4 = this.productManage.GetSearchCountEx(cid, brandid, keyword, price);
            ((dynamic) base.ViewBag).TotalCount = num4;
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int? nullable = pageIndex;
            int num5 = pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() * num5) : null;
            List<Maticsoft.Model.Shop.Products.ProductInfo> list3 = this.productManage.GetSearchListEx(cid, brandid, keyword, price, mod, startIndex, endIndex);
            IPageSetting pageSetting = PageSetting.GetPageSetting("Category", ApplicationKeyType.Shop);
            pageSetting.Replace(new string[][] { new string[] { "{cname}", model.CurrentCateName } });
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
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

        public ActionResult ListWaterfall(int cid, int brandid, string keyword, string mod, string price, int startIndex, string viewName = "_ListWaterfall")
        {
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            keyword = InjectionFilter.SqlFilter(keyword);
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDataCount) - 1) : this._waterfallDataCount;
            int num2 = this.productManage.GetSearchCountEx(cid, brandid, keyword, price);
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productManage.GetSearchListEx(cid, brandid, keyword, price, mod, startIndex, endIndex);
            if (num2 < 1)
            {
                return new EmptyResult();
            }
            return base.View(viewName, model);
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
    }
}


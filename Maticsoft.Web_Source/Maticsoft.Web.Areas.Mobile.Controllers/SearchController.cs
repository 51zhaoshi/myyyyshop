namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.ViewModel.Shop;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class SearchController : MobileControllerBase
    {
        private Maticsoft.BLL.Shop.Products.BrandInfo brandBll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        private Content contBll = new Content();
        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();

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

        public ActionResult Filter(int cid = 0, string keyword = "")
        {
            return base.View();
        }

        public ActionResult Index(int cid = 0, int brandid = 0, string keyword = "", string mod = "hot", string price = "0-0", int? pageIndex = 1, string viewName = "Index", string ajaxViewName = "_ProductList")
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            ProductListModel model = new ProductListModel();
            keyword = InjectionFilter.SqlFilter(keyword);
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return base.RedirectToAction("Index", "Home", new { Area = "" });
            }
            if (cid > 0)
            {
                List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == cid;
                }
                Maticsoft.Model.Shop.Products.CategoryInfo info = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                if (info != null)
                {
                    string[] path_arr = info.Path.Split(new char[] { '|' });
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> list2 = (from c in allCateList
                        where path_arr.Contains<string>(c.CategoryId.ToString())
                        orderby c.Depth
                        select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
                    model.CategoryPathList = list2;
                    model.CurrentCateName = info.Name;
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
            int pageSize = 15;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + pageSize) - 1) : pageSize;
            int num4 = this.productManage.GetSearchCountEx(cid, brandid, keyword, price);
            ((dynamic) base.ViewBag).TotalCount = num4;
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int? nullable = pageIndex;
            int num5 = pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() * num5) : null;
            List<Maticsoft.Model.Shop.Products.ProductInfo> list3 = this.productManage.GetSearchListEx(cid, brandid, keyword, price, mod, startIndex, endIndex);
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

        public ActionResult Search(string viewName = "Index")
        {
            ((dynamic) base.ViewBag).tel = ConfigSystem.GetValueByCache("CompanyTelephone");
            return base.View(viewName);
        }

        public ActionResult SearchList(int menuid = -1, string kw = "", int pageIndex = 1, int topclass = 3, string viewName = "_SearchList")
        {
            kw = InjectionFilter.Filter(kw);
            if ((menuid <= 0) || string.IsNullOrWhiteSpace(kw))
            {
                return base.View(viewName);
            }
            int pageSize = 6;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            PagedList<Content> model = new PagedList<Content>(this.contBll.GetListByPageEx(menuid, startIndex, endIndex, kw, topclass, out totalItemCount), pageIndex, pageSize, totalItemCount);
            ((dynamic) base.ViewBag).pageIndex = pageIndex;
            ((dynamic) base.ViewBag).totalPage = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            ((dynamic) base.ViewBag).menuid = menuid;
            ((dynamic) base.ViewBag).keywords = kw;
            return base.View(viewName, model);
        }
    }
}


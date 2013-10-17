namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class HomeController : ShopControllerBase
    {
        private Maticsoft.BLL.Shop.Products.BrandInfo brandInfoBll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryBll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();

        public PartialViewResult CategoryList(string viewName = "_CategoryList")
        {
            return base.PartialView(viewName);
        }

        public PartialViewResult CommentPart(int Cid, int Top = 9, string viewName = "_CommentPart")
        {
            return base.PartialView(viewName);
        }

        public PartialViewResult HotBrands(int top = 10, string viewName = "_HotBrands")
        {
            List<Maticsoft.Model.Shop.Products.BrandInfo> brandList = this.brandInfoBll.GetBrandList("", top);
            return this.PartialView(viewName, brandList);
        }

        public PartialViewResult HotComments(int Top = 8, string viewName = "_HotComments")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysByParentId = this.categoryBll.GetCategorysByParentId(0, Top);
            return this.PartialView(viewName, categorysByParentId);
        }

        public ActionResult Index()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        public PartialViewResult NewListPart(int Cid, int Top = 5, string viewName = "_NewListPart")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productBll.GetProductRecList(ProductRecType.Latest, Cid, Top);
            return this.PartialView(viewName, model);
        }

        public PartialViewResult ProductList(int Cid, ProductRecType RecType = 4, int Top = 10, string viewName = "_ProductList")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productBll.GetProductRecList(RecType, Cid, Top);
            return this.PartialView(viewName, model);
        }

        public PartialViewResult ProductNewList(int Top = 7, string viewName = "_ProductNewList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categorysByParentId = this.categoryBll.GetCategorysByParentId(0, Top);
            return this.PartialView(viewName, categorysByParentId);
        }
    }
}


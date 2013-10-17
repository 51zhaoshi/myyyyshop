namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Components;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class HomeController : MobileControllerBase
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();

        public PartialViewResult CategoryList(int Cid = 0, int Top = 10, string ViewName = "_CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> model = (from c in Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList()
                where c.ParentCategoryId == Cid
                select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            return this.PartialView(ViewName, model);
        }

        public ActionResult Index()
        {
            ApplicationKeyType cMS = ApplicationKeyType.CMS;
            switch (Maticsoft.Components.MvcApplication.MainAreaRoute)
            {
                case AreaRoute.Shop:
                    cMS = ApplicationKeyType.Shop;
                    break;

                case AreaRoute.SNS:
                    cMS = ApplicationKeyType.SNS;
                    break;

                default:
                    cMS = ApplicationKeyType.CMS;
                    break;
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", cMS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        public PartialViewResult NewsList(string viewName, int ClassID, int Top)
        {
            List<Content> modelList = new Content().GetModelList(ClassID, Top);
            return this.PartialView(viewName, modelList);
        }

        public PartialViewResult ProductList(int Cid, ProductRecType RecType = 4, int Top = 10, string viewName = "_ProductList")
        {
            Maticsoft.Model.Shop.Products.CategoryInfo info = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList().FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(c => c.CategoryId == Cid);
            if (info != null)
            {
                ((dynamic) base.ViewBag).CategoryName = info.Name;
            }
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productBll.GetProductRecList(RecType, Cid, Top);
            return this.PartialView(viewName, model);
        }
    }
}


namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.Shop;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class ProSalesController : ShopControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDataCount = 1;
        private int _waterfallSize = 0x20;
        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();

        public ActionResult CountDown(int? pageIndex = 1, string viewName = "CountDown", string ajaxViewName = "_ProductList")
        {
            ProductListModel model = new ProductListModel();
            Maticsoft.Model.Shop.Products.CategoryInfo cateModel = null;
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int proSalesCount = this.productManage.GetProSalesCount();
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int? nullable = pageIndex;
            int num5 = pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() * num5) : null;
            List<Maticsoft.Model.Shop.Products.ProductInfo> proSalesList = this.productManage.GetProSalesList(startIndex, endIndex);
            IPageSetting setting = PageSetting.GetCategorySetting(cateModel, "Category", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            if (proSalesCount >= 1)
            {
                int? nullable3 = pageIndex;
                model.ProductPagedList = proSalesList.ToPagedList<Maticsoft.Model.Shop.Products.ProductInfo>(nullable3.HasValue ? nullable3.GetValueOrDefault() : 1, pageSize, new int?(proSalesCount));
                if (base.Request.IsAjaxRequest())
                {
                    return this.PartialView(ajaxViewName, model);
                }
            }
            return base.View(viewName, model);
        }

        public ActionResult ListWaterfall(int startIndex, string viewName = "_ListWaterfall")
        {
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDataCount) - 1) : this._waterfallDataCount;
            int proSalesCount = this.productManage.GetProSalesCount();
            List<Maticsoft.Model.Shop.Products.ProductInfo> proSalesList = this.productManage.GetProSalesList(startIndex, endIndex);
            if (proSalesCount < 1)
            {
                return new EmptyResult();
            }
            return base.View(viewName, proSalesList);
        }
    }
}


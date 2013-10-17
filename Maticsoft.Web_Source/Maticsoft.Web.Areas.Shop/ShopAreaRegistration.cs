namespace Maticsoft.Web.Areas.Shop
{
    using Maticsoft.Components;
    using Maticsoft.Web.Areas;
    using System;
    using System.Web.Mvc;

    public class ShopAreaRegistration : AreaRegistrationBase
    {
        public ShopAreaRegistration() : base(AreaRoute.Shop)
        {
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string name = this.AreaName + "_ProductList";
            string url = base.CurrentRoutePath + "Product/{cid}/{brandid}/{attrvalues}/{mod}/{price}";
            object defaults = new {
                controller = "Product",
                action = "Index",
                cid = 0,
                brandid = 0,
                attrvalues = "0",
                mod = "default",
                price = ""
            };
            object constraints = new {
                mod = "default|hot|new|price|pricedesc",
                cid = @"[\d]{0,11}"
            };
            string[] namespaces = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(name, url, defaults, constraints, namespaces);
            string str3 = this.AreaName + "_ProductSearch";
            string str4 = base.CurrentRoutePath + "Search/{cid}/{brandid}/{mod}/{price}/{keyword}";
            object obj4 = new {
                controller = "Search",
                action = "Index",
                cid = 0,
                brandid = 0,
                mod = "default",
                price = "0-0",
                keyword = ""
            };
            object obj5 = new {
                mod = "default|hot|new|price|pricedesc",
                cid = @"[\d]{0,11}"
            };
            string[] strArray4 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str3, str4, obj4, obj5, strArray4);
            if (MvcApplication.MainAreaRoute != base.CurrentArea)
            {
                string currentRouteName = base.CurrentRouteName;
                string str6 = this.AreaName + "/{controller}/{action}/{viewname}/{id}";
                object obj6 = new {
                    controller = "Home",
                    action = "Index",
                    viewname = UrlParameter.Optional,
                    id = UrlParameter.Optional
                };
                object obj7 = new {
                    viewname = @"[\w]{0,50}",
                    id = @"[\d]{0,11}"
                };
                string[] strArray6 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
                context.MapRoute(currentRouteName, str6, obj6, obj7, strArray6);
            }
        }
    }
}


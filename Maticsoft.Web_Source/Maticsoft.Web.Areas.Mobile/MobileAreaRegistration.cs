namespace Maticsoft.Web.Areas.Mobile
{
    using Maticsoft.Components;
    using Maticsoft.Web;
    using Maticsoft.Web.Areas;
    using System;
    using System.Web.Mvc;

    public class MobileAreaRegistration : AreaRegistrationBase
    {
        protected bool IsRegisterMArea;
        protected string RouteName;

        public MobileAreaRegistration() : base(AreaRoute.Mobile)
        {
            this.RouteName = AreaRoute.Mobile.ToString();
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string name = this.AreaName + "_" + this.RouteName + "_ProductList";
            string url = base.CurrentRoutePath + "p/{cid}/{brandid}/{attrvalues}/{mod}/{price}/{viewname}/{ajaxViewName}";
            object defaults = new {
                controller = "Product",
                action = "Index",
                cid = 0,
                brandid = 0,
                attrvalues = "0",
                mod = "hot",
                price = "",
                viewname = "Index",
                ajaxViewName = "_ProductList"
            };
            object constraints = new {
                mod = "default|hot|new|price|pricedesc",
                cid = @"[\d]{0,11}"
            };
            string[] namespaces = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(name, url, defaults, constraints, namespaces);
            string str3 = this.AreaName + "_" + this.RouteName + "_ProductSearch";
            string str4 = base.CurrentRoutePath + "s/{cid}/{brandid}/{mod}/{price}/{keyword}/{viewname}/{ajaxViewName}";
            object obj4 = new {
                controller = "Search",
                action = "Index",
                cid = 0,
                brandid = 0,
                mod = "default",
                price = "0-0",
                keyword = "",
                viewname = "Index",
                ajaxViewName = "_ProductList"
            };
            object obj5 = new {
                mod = "default|hot|new|price|pricedesc",
                cid = @"[\d]{0,11}"
            };
            string[] strArray4 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str3, str4, obj4, obj5, strArray4);
            string str5 = this.AreaName + "_" + this.RouteName + "_ProductSearchFilter";
            string str6 = base.CurrentRoutePath + "s/f/{cid}/{keyword}";
            object obj6 = new {
                controller = "Search",
                action = "Filter",
                cid = 0,
                keyword = ""
            };
            object obj7 = new {
                cid = @"[\d]{0,11}"
            };
            string[] strArray6 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str5, str6, obj6, obj7, strArray6);
            string str7 = this.AreaName + "_" + this.RouteName + "_CategoryList";
            string str8 = base.CurrentRoutePath + "p/c/{parentId}";
            object obj8 = new {
                controller = "Product",
                action = "CategoryList",
                parentId = 0
            };
            object obj9 = new {
                parentId = @"[\d]{0,11}"
            };
            string[] strArray8 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str7, str8, obj8, obj9, strArray8);
            string str9 = this.AreaName + "_" + this.RouteName + "_Login";
            string str10 = base.CurrentRoutePath + "a/l";
            object obj10 = new {
                controller = "Account",
                action = "Login"
            };
            string[] strArray10 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str9, str10, obj10, strArray10);
            string str11 = this.AreaName + "_" + this.RouteName + "_Register";
            string str12 = base.CurrentRoutePath + "a/r";
            object obj11 = new {
                controller = "Account",
                action = "Register"
            };
            string[] strArray12 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str11, str12, obj11, strArray12);
            string str13 = this.AreaName + "_" + this.RouteName + "_UserCenter";
            string str14 = base.CurrentRoutePath + "u/{action}/{id}";
            object obj12 = new {
                controller = "UserCenter",
                action = "Index",
                id = UrlParameter.Optional
            };
            string[] strArray14 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str13, str14, obj12, strArray14);
            string str15 = this.AreaName + "_" + this.RouteName + "_ProductDetail";
            string str16 = base.CurrentRoutePath + "p/d/{productId}";
            object obj13 = new {
                controller = "Product",
                action = "Detail",
                productId = 0
            };
            object obj14 = new {
                productId = @"[\d]{0,11}"
            };
            string[] strArray16 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str15, str16, obj13, obj14, strArray16);
            string str17 = this.AreaName + "_" + this.RouteName + "_ShoppingCart";
            string str18 = base.CurrentRoutePath + "sc/ci";
            object obj15 = new {
                controller = "ShoppingCart",
                action = "CartInfo"
            };
            string[] strArray18 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str17, str18, obj15, strArray18);
            string str19 = this.AreaName + "_" + this.RouteName + "_OderInfo";
            string str20 = base.CurrentRoutePath + "o/oi/{orderId}";
            object obj16 = new {
                controller = "Order",
                action = "OrderInfo",
                orderId = -1
            };
            string[] strArray20 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str19, str20, obj16, strArray20);
            string str21 = this.AreaName + "_" + this.RouteName + "_Attendance";
            string str22 = base.CurrentRoutePath + "w/a/{userId}";
            object obj17 = new {
                controller = "Home",
                action = "Attendance",
                userId = 0
            };
            string[] strArray22 = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
            context.MapRoute(str21, str22, obj17, strArray22);
            if ((Maticsoft.Components.MvcApplication.MainAreaRoute != base.CurrentArea) || this.IsRegisterMArea)
            {
                string str23 = base.CurrentRouteName + "Base";
                name = base.CurrentRoutePath + "{controller}/{action}/{id}";
                defaults = new {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                };
                constraints = new {
                    id = @"[\d]{0,11}"
                };
                string[] strArray = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
                strArray = strArray;
                context.MapRoute(str23, name, defaults, constraints, strArray);
                name = base.CurrentRouteName + this.RouteName;
                url = base.CurrentRoutePath + "{controller}/{action}/{viewname}/{id}";
                defaults = new {
                    controller = "Home",
                    action = "Index",
                    viewname = UrlParameter.Optional,
                    id = UrlParameter.Optional
                };
                constraints = new {
                    viewname = @"[\w]{0,50}",
                    id = @"[\d]{0,11}"
                };
                strArray = new string[] { string.Format("Maticsoft.Web.Areas.{0}.*", this.AreaName) };
                strArray = strArray;
                context.MapRoute(name, url, defaults, constraints, strArray);
            }
        }
    }
}


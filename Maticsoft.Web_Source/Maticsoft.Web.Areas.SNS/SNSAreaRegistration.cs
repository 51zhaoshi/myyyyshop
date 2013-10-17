namespace Maticsoft.Web.Areas.SNS
{
    using Maticsoft.Web;
    using Maticsoft.Web.Areas;
    using System;
    using System.Web.Mvc;

    public class SNSAreaRegistration : AreaRegistrationBase
    {
        public const int OutputCacheDuration = 300;

        public SNSAreaRegistration() : base(AreaRoute.SNS)
        {
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string name = this.AreaName + "_Album";
            string url = base.CurrentRoutePath + "Album/Details/{AlbumID}";
            object defaults = new {
                controller = "Album",
                action = "Details",
                AlbumID = UrlParameter.Optional
            };
            string[] namespaces = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(name, url, defaults, namespaces);
            string str3 = this.AreaName + "_SearchUserGroup";
            string str4 = base.CurrentRoutePath + "Search/{action}/{q}/{page}";
            object obj3 = new {
                controller = "Search",
                action = "User",
                q = UrlParameter.Optional,
                page = UrlParameter.Optional
            };
            object constraints = new {
                action = "User|Groups"
            };
            string[] strArray4 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str3, str4, obj3, constraints, strArray4);
            string str5 = this.AreaName + "_Search";
            string str6 = base.CurrentRoutePath + "Search/{action}/{sequence}/{q}/{pageIndex}";
            object obj5 = new {
                controller = "Search",
                action = "Albums",
                pageIndex = UrlParameter.Optional,
                sequence = "hot",
                q = UrlParameter.Optional
            };
            object obj6 = new {
                action = "Albums|Product|Topics|Photo"
            };
            string[] strArray6 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str5, str6, obj5, obj6, strArray6);
            string str7 = this.AreaName + "_AlbumEdit";
            string str8 = base.CurrentRoutePath + "Profile/AlbumEdit/{AlbumID}";
            object obj7 = new {
                controller = "Profile",
                action = "AlbumEdit",
                AlbumID = UrlParameter.Optional
            };
            string[] strArray8 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str7, str8, obj7, strArray8);
            string str9 = this.AreaName + "_User";
            string str10 = base.CurrentRoutePath + "User/Posts/{Uid}";
            object obj8 = new {
                controller = "User",
                action = "Posts",
                uid = UrlParameter.Optional
            };
            string[] strArray10 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str9, str10, obj8, strArray10);
            string str11 = this.AreaName + "_Video";
            string str12 = base.CurrentRoutePath + "Video/{action}";
            object obj9 = new {
                controller = "Video",
                action = "Index"
            };
            string[] strArray12 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str11, str12, obj9, strArray12);
            string str13 = this.AreaName + "_ShopCate";
            string str14 = base.CurrentRoutePath + "Product/{cname}/{cid}/{topcid}/{minprice}-{maxprice}-{sequence}-{color}";
            object obj10 = new {
                controller = "Product",
                action = "Index",
                cname = UrlParameter.Optional,
                topcid = UrlParameter.Optional,
                cid = 0,
                minprice = 0,
                maxprice = 0,
                sequence = "new",
                color = "all"
            };
            string[] strArray14 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str13, str14, obj10, strArray14);
            string str15 = this.AreaName + "_Collocation";
            string str16 = base.CurrentRoutePath + "Collocation/{orderby}";
            object obj11 = new {
                controller = "Collocation",
                action = "Index",
                orderby = "popular"
            };
            object obj12 = new {
                orderby = "popular|new|hot"
            };
            string[] strArray16 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str15, str16, obj11, obj12, strArray16);
            string str17 = this.AreaName + "_ProductDetail_Old";
            string str18 = base.CurrentRoutePath + "Detail/{controller}/{pid}";
            object obj13 = new {
                controller = "Product",
                action = "Detail",
                pid = 0
            };
            string[] strArray18 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str17, str18, obj13, strArray18);
            string str19 = this.AreaName + "_PhotoDetail_Old";
            string str20 = base.CurrentRoutePath + "Detail/Photo/{pid}";
            object obj14 = new {
                controller = "Photo",
                action = "Detail",
                pid = 0
            };
            string[] strArray20 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str19, str20, obj14, strArray20);
            string str21 = this.AreaName + "_ProductDetail";
            string str22 = base.CurrentRoutePath + "Product/Detail/{pid}";
            object obj15 = new {
                controller = "Product",
                action = "Detail",
                pid = 0
            };
            string[] strArray22 = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            context.MapRoute(str21, str22, obj15, strArray22);
            string str23 = this.AreaName + "_PhotoList";
            string str24 = base.CurrentRoutePath + "Photo/Index/{type}/{categoryId}/{address}/{orderby}";
            object obj16 = new {
                controller = "Photo",
                action = "Index",
                type = -1,
                categoryId = 0,
                address = "all",
                orderby = "hot"
            };
            string[] strArray = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            strArray = strArray;
            context.MapRoute(str23, str24, obj16, strArray);
            name = this.AreaName + "_PhotoDetail";
            url = base.CurrentRoutePath + "Photo/Detail/{pid}";
            defaults = new {
                controller = "Photo",
                action = "Detail",
                pid = 0
            };
            strArray = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            strArray = strArray;
            context.MapRoute(name, url, defaults, strArray);
            name = this.AreaName + "_ShareGoods";
            url = base.CurrentRoutePath + "ShareGoods/{orderby}";
            defaults = new {
                controller = "ShareGoods",
                action = "Index",
                orderby = "popular"
            };
            obj3 = new {
                orderby = "popular|new|hot"
            };
            strArray = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            strArray = strArray;
            context.MapRoute(name, url, defaults, obj3, strArray);
            name = this.AreaName + "_Star";
            url = base.CurrentRoutePath + "Star";
            defaults = new {
                controller = "Star",
                action = "Pioneer"
            };
            strArray = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            strArray = strArray;
            context.MapRoute(name, url, defaults, strArray);
            name = this.AreaName + "_ArticleList";
            url = base.CurrentRoutePath + "Article/Index/{classId}";
            defaults = new {
                controller = "Article",
                action = "Index",
                classId = 0
            };
            strArray = new string[] { "Maticsoft.Web.Areas.SNS.*" };
            strArray = strArray;
            context.MapRoute(name, url, defaults, strArray);
            base.RegisterArea(context);
        }

        private static string PathUploadFolderBase
        {
            get
            {
                return (AreaRegistrationBase.PathUploadFolderBase(AreaRoute.SNS) + "{0}/{1}/{2}/");
            }
        }

        public static string PathUploadImgGroup
        {
            get
            {
                return string.Format(PathUploadFolderBase, "Images", "Group", DateTime.Now.ToString("yyyyMMdd"));
            }
        }

        public static string PathUploadImgGroupThumb
        {
            get
            {
                return string.Format(PathUploadFolderBase, "Images", "GroupThumbs", DateTime.Now.ToString("yyyyMMdd"));
            }
        }
    }
}


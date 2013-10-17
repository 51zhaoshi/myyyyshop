namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Settings;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class PartialController : ShopControllerBase
    {
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryBll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private ContentClass contentclassBll = new ContentClass();
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();

        public PartialViewResult AD(int AdvPositionId, string viewName = "_AD")
        {
            Maticsoft.Model.Settings.Advertisement modelByAdvPositionId = new Maticsoft.BLL.Settings.Advertisement().GetModelByAdvPositionId(AdvPositionId);
            return this.PartialView(viewName, modelByAdvPositionId);
        }

        public PartialViewResult AdDetail(int id, string ViewName = "_IndexAd")
        {
            List<Maticsoft.Model.Settings.Advertisement> listByAidCache = new Maticsoft.BLL.Settings.Advertisement().GetListByAidCache(id);
            return this.PartialView(ViewName, listByAidCache);
        }

        public ActionResult BaiduShare()
        {
            ((dynamic) base.ViewBag).BaiduUid = ConfigSystem.GetValueByCache("BaiduShareUserId");
            return base.View("_BaiduShare");
        }

        public PartialViewResult CategoryList(int Cid = 0, int Top = 10, string ViewName = "_CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> model = (from c in Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList()
                where c.ParentCategoryId == Cid
                select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            return this.PartialView(ViewName, model);
        }

        public PartialViewResult ContentList(string viewName, int ClassID, int Top)
        {
            List<Content> modelList = new Content().GetModelList(ClassID, Top);
            ((dynamic) base.ViewBag).contentclassName = this.contentclassBll.GetClassnameById(ClassID);
            return this.PartialView(viewName, modelList);
        }

        public PartialViewResult Footer(string viewName = "_Footer")
        {
            return base.PartialView(viewName);
        }

        public PartialViewResult Header(string viewName = "_Header")
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Logo = set.LogoPath;
            ((dynamic) base.ViewBag).WebName = set.WebName;
            ((dynamic) base.ViewBag).Domain = set.WebSite_Domain;
            return base.PartialView(viewName);
        }

        public PartialViewResult HotKeyword(int Cid = 0, int Top = 6, string ViewName = "_HotKeyword")
        {
            List<Maticsoft.Model.Shop.Products.HotKeyword> keywordsList = new Maticsoft.BLL.Shop.Products.HotKeyword().GetKeywordsList(Cid, Top);
            ((dynamic) base.ViewBag).Cid = Cid;
            return this.PartialView(ViewName, keywordsList);
        }

        public PartialViewResult IndexSecondCategoryList(int Cid = 0, int Top = 10, string ViewName = "_SecondCateAll")
        {
            Func<Maticsoft.Model.Shop.Products.CategoryInfo, bool> predicate = null;
            List<Maticsoft.Model.Shop.Products.CategoryInfo> allCateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> model = null;
            if (Cid == 0)
            {
                model = (from c in allCateList
                    where c.ParentCategoryId == Cid
                    select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            }
            else
            {
                if (predicate == null)
                {
                    predicate = c => c.CategoryId == Cid;
                }
                Maticsoft.Model.Shop.Products.CategoryInfo xxx = allCateList.FirstOrDefault<Maticsoft.Model.Shop.Products.CategoryInfo>(predicate);
                if (xxx != null)
                {
                    model = (from c in allCateList
                        where c.Path.StartsWith(xxx.Path + "|")
                        select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
                }
            }
            return this.PartialView(ViewName, model);
        }

        public PartialViewResult Login(string notLoginView = "_NotLogin", string userLoginView = "_UserLogin")
        {
            if ((!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null)) || !(base.CurrentUser.UserType != "AA"))
            {
                return base.PartialView(notLoginView);
            }
            ((dynamic) base.ViewBag).loginnickname = base.CurrentUser.NickName;
            return base.PartialView(userLoginView);
        }

        public PartialViewResult MenuDetail(int Cid = 0, int Top = 4, string ViewName = "_MenuDetail")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> model = (from c in Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList()
                where c.ParentCategoryId == Cid
                select c).ToList<Maticsoft.Model.Shop.Products.CategoryInfo>();
            ((dynamic) base.ViewBag).Cid = Cid;
            int haschildren = 0;
            model.ForEach(delegate (Maticsoft.Model.Shop.Products.CategoryInfo x) {
                if (x.HasChildren)
                {
                    haschildren++;
                }
            });
            ((dynamic) base.ViewBag).haschildren = haschildren;
            return this.PartialView(ViewName, model);
        }

        public PartialViewResult Navigation(string viewName = "_Navigation", string Theme = "M1")
        {
            List<Maticsoft.Model.Settings.MainMenus> menusByAreaByCacle = new Maticsoft.BLL.Settings.MainMenus().GetMenusByAreaByCacle(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop, Theme);
            return this.PartialView(viewName, menusByAreaByCacle);
        }

        public PartialViewResult ProductRec(ProductRecType Type = 0, int Cid = 0, int Top = 5, string ViewName = "_ProductRec")
        {
            List<Maticsoft.Model.Shop.Products.ProductInfo> model = this.productBll.GetProductRecList(Type, Cid, Top);
            return this.PartialView(ViewName, model);
        }

        public PartialViewResult Search(string viewName = "_Search")
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Logo = set.LogoPath;
            ((dynamic) base.ViewBag).WebName = set.WebName;
            ((dynamic) base.ViewBag).Domain = set.WebSite_Domain;
            return base.PartialView(viewName);
        }

        public PartialViewResult SearchCart(string ViewName = "_SearchCart")
        {
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            ShoppingCartInfo shoppingCart = new ShoppingCartHelper(userId).GetShoppingCart();
            ((dynamic) base.ViewBag).CartCount = shoppingCart.Quantity;
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Logo = set.LogoPath;
            return base.PartialView(ViewName);
        }
    }
}


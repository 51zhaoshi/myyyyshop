namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.Shop.Supplier;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.Shop;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class ShoppingCartController : ShopControllerBase
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new Maticsoft.BLL.Shop.Products.SKUInfo();

        public ActionResult AddCart(string sku, int count = 1, string viewName = "AddCart")
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                return base.RedirectToAction("Index", "Home");
            }
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            ShoppingCartHelper helper = new ShoppingCartHelper(userId);
            ShoppingCartItem cartItem = new ShoppingCartItem();
            ProductModel model = new ProductModel();
            Maticsoft.Model.Shop.Products.SKUInfo modelBySKU = this.skuBll.GetModelBySKU(sku);
            if (modelBySKU == null)
            {
                return base.Content("NOSKU");
            }
            List<Maticsoft.Model.Shop.Products.SKUInfo> list2 = new List<Maticsoft.Model.Shop.Products.SKUInfo> {
                modelBySKU
            };
            model.ProductSkus = list2;
            model.ProductInfo = this.productBll.GetModelByCache(modelBySKU.ProductId);
            if ((model.ProductInfo != null) && (model.ProductSkus != null))
            {
                cartItem.Name = model.ProductInfo.ProductName;
                cartItem.Quantity = count;
                cartItem.SKU = model.ProductSkus[0].SKU;
                cartItem.ProductId = model.ProductInfo.ProductId;
                cartItem.UserId = userId;
                if (model.ProductInfo.SupplierId > 0)
                {
                    Maticsoft.Model.Shop.Supplier.SupplierInfo modelByCache = new Maticsoft.BLL.Shop.Supplier.SupplierInfo().GetModelByCache(model.ProductInfo.SupplierId);
                    if (modelByCache != null)
                    {
                        cartItem.SupplierId = new int?(modelByCache.SupplierId);
                        cartItem.SupplierName = modelByCache.Name;
                    }
                }
                List<Maticsoft.Model.Shop.Products.SKUItem> sKUItemsBySkuId = this.skuBll.GetSKUItemsBySkuId(modelBySKU.SkuId);
                if ((sKUItemsBySkuId != null) && (sKUItemsBySkuId.Count > 0))
                {
                    cartItem.SkuValues = new string[sKUItemsBySkuId.Count];
                    int index = 0;
                    sKUItemsBySkuId.ForEach(delegate (Maticsoft.Model.Shop.Products.SKUItem xx) {
                        cartItem.SkuValues[index++] = xx.ValueStr;
                        if (!string.IsNullOrWhiteSpace(xx.ImageUrl))
                        {
                            cartItem.SkuImageUrl = xx.ImageUrl;
                        }
                    });
                }
                cartItem.ThumbnailsUrl = model.ProductInfo.ThumbnailUrl1;
                cartItem.CostPrice = model.ProductSkus[0].CostPrice.HasValue ? model.ProductSkus[0].CostPrice.Value : 0M;
                cartItem.MarketPrice = model.ProductInfo.MarketPrice.HasValue ? model.ProductInfo.MarketPrice.Value : 0M;
                cartItem.SellPrice = cartItem.AdjustedPrice = model.ProductSkus[0].SalePrice;
                cartItem.Weight = model.ProductSkus[0].Weight.HasValue ? model.ProductSkus[0].Weight.Value : 0;
                helper.AddItem(cartItem);
                ShoppingCartInfo shoppingCart = helper.GetShoppingCart();
                ((dynamic) base.ViewBag).TotalPrice = shoppingCart.TotalSellPrice;
                ((dynamic) base.ViewBag).ItemCount = shoppingCart.Quantity;
            }
            ((dynamic) base.ViewBag).Title = "添加购物车";
            return base.RedirectToAction("CartInfo");
        }

        public ActionResult CartInfo()
        {
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "购物车信息" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View();
        }

        public ActionResult CartList(string viewName = "_CartList")
        {
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            ShoppingCartInfo shoppingCart = new ShoppingCartHelper(userId).GetShoppingCart();
            try
            {
                shoppingCart = new SalesRuleProduct().GetWholeSale(shoppingCart);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return base.View(viewName, shoppingCart);
        }

        public ActionResult ClearShopCart()
        {
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            new ShoppingCartHelper(userId).ClearShoppingCart();
            return base.Content("Yes");
        }

        public ActionResult GetCartCount()
        {
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            ShoppingCartHelper helper = new ShoppingCartHelper(userId);
            return base.Content(helper.GetShoppingCart().Quantity.ToString(CultureInfo.InvariantCulture));
        }

        public ActionResult RemoveItem(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ItemIds"]))
            {
                return base.Content("No");
            }
            string[] strArray = Fm["ItemIds"].Split(new char[] { ',' });
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            ShoppingCartHelper helper = new ShoppingCartHelper(userId);
            foreach (string str2 in strArray)
            {
                int itemId = Globals.SafeInt(str2, 0);
                helper.RemoveItem(itemId);
            }
            return base.Content("Yes");
        }

        public ActionResult UpdateItemCount(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ItemId"]) || string.IsNullOrWhiteSpace(Fm["Count"]))
            {
                return base.Content("No");
            }
            int itemId = Globals.SafeInt(Fm["ItemId"], 0);
            int quantity = Globals.SafeInt(Fm["Count"], 1);
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            new ShoppingCartHelper(userId).UpdateItemQuantity(itemId, quantity);
            return base.Content("Yes");
        }
    }
}


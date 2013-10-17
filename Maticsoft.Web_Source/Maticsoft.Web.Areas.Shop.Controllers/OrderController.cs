namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.Shop.Coupon;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.BLL.Shop.Shipping;
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Shop.Coupon;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.Model.Shop.Supplier;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Model;
    using Maticsoft.ViewModel.Shop;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.Shop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class OrderController : ShopControllerBaseUser
    {
        private readonly Maticsoft.BLL.Shop.Shipping.ShippingAddress _addressManage = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
        private readonly Orders _orderManage = new Orders();
        private readonly Maticsoft.BLL.Shop.Shipping.ShippingType _shippingTypeManage = new Maticsoft.BLL.Shop.Shipping.ShippingType();

        public ActionResult AddressInfo(int addressId = -1, bool isModify = false, string viewName = "_AddressInfo")
        {
            Predicate<Maticsoft.Model.Shop.Shipping.ShippingAddress> match = null;
            Maticsoft.BLL.Shop.Shipping.ShippingAddress address = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
            ShippingAddressModel model = new ShippingAddressModel {
                ListAddress = address.GetModelList(" UserId=" + base.currentUser.UserID)
            };
            ((dynamic) base.ViewBag).IsModify = isModify;
            if ((isModify && (addressId > 0)) && ((model.ListAddress != null) && (model.ListAddress.Count > 0)))
            {
                if (match == null)
                {
                    match = info => info.ShippingId == addressId;
                }
                model.CurrentAddress = model.ListAddress.Find(match);
            }
            if ((!isModify && (addressId < 0)) && (model.CurrentAddress == null))
            {
                Maticsoft.Model.Shop.Shipping.ShippingAddress address2 = new Maticsoft.Model.Shop.Shipping.ShippingAddress {
                    ShippingId = addressId,
                    ShipName = base.CurrentUser.TrueName,
                    UserId = base.CurrentUser.UserID,
                    EmailAddress = base.CurrentUser.Email,
                    CelPhone = base.CurrentUser.Phone
                };
                model.CurrentAddress = address2;
            }
            if (model.CurrentAddress == null)
            {
                Maticsoft.Model.Shop.Shipping.ShippingAddress address3 = new Maticsoft.Model.Shop.Shipping.ShippingAddress {
                    ShippingId = addressId
                };
                model.CurrentAddress = address3;
            }
            return base.View(viewName, model);
        }

        public ActionResult AjaxGetCoupon(FormCollection Fm)
        {
            if (string.IsNullOrWhiteSpace(Fm["ConponCode"]))
            {
                return base.Content("False");
            }
            string couponCode = Fm["ConponCode"];
            decimal num = Globals.SafeDecimal(Fm["TotalPrice"], (decimal) 0M);
            Maticsoft.Model.Shop.Coupon.CouponInfo couponInfo = new Maticsoft.BLL.Shop.Coupon.CouponInfo().GetCouponInfo(couponCode, false);
            if (couponInfo == null)
            {
                return base.Content("No");
            }
            if (couponInfo.Status == 2)
            {
                return base.Content("Used");
            }
            if (couponInfo.LimitPrice >= num)
            {
                return base.Content("Limit");
            }
            string str2 = (num - couponInfo.CouponPrice).ToString("F");
            return base.Content(couponInfo.CouponPrice.ToString("F") + "|" + str2);
        }

        private ShoppingCartInfo GetCartInfo4SKU(Maticsoft.Model.Shop.Products.ProductInfo productInfo, Maticsoft.Model.Shop.Products.SKUInfo skuInfo, int quantity, Maticsoft.Model.Shop.Products.ProductInfo proSaleInfo)
        {
            ShoppingCartInfo info = new ShoppingCartInfo();
            ShoppingCartItem cartItem = new ShoppingCartItem {
                MarketPrice = productInfo.MarketPrice.HasValue ? productInfo.MarketPrice.Value : 0M,
                Name = productInfo.ProductName,
                Quantity = (quantity < 1) ? 1 : quantity,
                SellPrice = skuInfo.SalePrice,
                AdjustedPrice = skuInfo.SalePrice,
                SKU = skuInfo.SKU,
                ProductId = skuInfo.ProductId,
                UserId = base.currentUser.UserID
            };
            if (proSaleInfo != null)
            {
                cartItem.AdjustedPrice = proSaleInfo.ProSalesPrice;
            }
            if (productInfo.SupplierId > 0)
            {
                Maticsoft.Model.Shop.Supplier.SupplierInfo modelByCache = new Maticsoft.BLL.Shop.Supplier.SupplierInfo().GetModelByCache(productInfo.SupplierId);
                if (modelByCache != null)
                {
                    cartItem.SupplierId = new int?(modelByCache.SupplierId);
                    cartItem.SupplierName = modelByCache.Name;
                }
            }
            List<Maticsoft.Model.Shop.Products.SKUItem> sKUItemsBySkuId = new Maticsoft.BLL.Shop.Products.SKUInfo().GetSKUItemsBySkuId(skuInfo.SkuId);
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
            cartItem.ThumbnailsUrl = productInfo.ThumbnailUrl1;
            cartItem.CostPrice = skuInfo.CostPrice.HasValue ? skuInfo.CostPrice.Value : 0M;
            cartItem.Weight = skuInfo.Weight.HasValue ? skuInfo.Weight.Value : 0;
            info.Items.Add(cartItem);
            return info;
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult OrderInfo(long OrderId = -1L, string viewName = "OrderInfo")
        {
            Maticsoft.Model.Shop.Order.OrderInfo modelInfoByCache = this._orderManage.GetModelInfoByCache(OrderId);
            if ((modelInfoByCache == null) || (modelInfoByCache.BuyerID != base.currentUser.UserID))
            {
                return (ActionResult) this.Redirect(((dynamic) base.ViewBag).BasePath + "UserCenter/Orders");
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", ApplicationKeyType.Shop);
            ((dynamic) base.ViewBag).Title = "查看订单详细信息" + pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(viewName, modelInfoByCache);
        }

        public ActionResult PayAndShipInfo(int payId = -1, int shipId = -1, string viewName = "_PayAndShipInfo")
        {
            PayAndShip model = new PayAndShip {
                ListPaymentMode = PaymentModeManage.GetPaymentModes()
            };
            if (payId > 0)
            {
                model.CurrentPaymentMode = PaymentModeManage.GetPaymentModeById(payId);
                model.ListShippingType = this._shippingTypeManage.GetListByPay(payId);
            }
            else if ((model.ListPaymentMode != null) && (model.ListPaymentMode.Count > 0))
            {
                model.CurrentPaymentMode = model.ListPaymentMode[0];
                model.ListShippingType = this._shippingTypeManage.GetListByPay(model.CurrentPaymentMode.ModeId);
            }
            else
            {
                PaymentModeInfo info = new PaymentModeInfo {
                    ModeId = -1,
                    Name = "当前网站未设置任何支付方式"
                };
                model.CurrentPaymentMode = info;
                model.ListPaymentMode = new List<PaymentModeInfo> { model.CurrentPaymentMode };
            }
            if (shipId > 0)
            {
                model.CurrentShippingType = this._shippingTypeManage.GetModelByCache(shipId);
            }
            else if ((model.ListShippingType != null) && (model.ListShippingType.Count > 0))
            {
                model.CurrentShippingType = model.ListShippingType[0];
            }
            else
            {
                Maticsoft.Model.Shop.Shipping.ShippingType type = new Maticsoft.Model.Shop.Shipping.ShippingType {
                    ModeId = -1,
                    Name = "当前支付方式未设置任何配送",
                    Description = "请选择其它支付方式"
                };
                model.CurrentShippingType = type;
                model.ListShippingType = new List<Maticsoft.Model.Shop.Shipping.ShippingType> { model.CurrentShippingType };
            }
            return base.View(viewName, model);
        }

        public ActionResult ShowAddress(int addressId = -1, string viewName = "_ShowAddress")
        {
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = null;
            if (addressId > 0)
            {
                model = this._addressManage.GetModel(addressId);
            }
            else
            {
                List<Maticsoft.Model.Shop.Shipping.ShippingAddress> modelList = this._addressManage.GetModelList(" UserId=" + base.currentUser.UserID);
                if ((modelList != null) && (modelList.Count > 0))
                {
                    model = modelList[0];
                }
            }
            if (model == null)
            {
                return base.View(viewName);
            }
            return base.View(viewName, model);
        }

        public ActionResult ShowPayAndShip(int payId = -1, int shipId = -1, string sku = new string(), int count = 1, int c = -1, string viewName = "_ShowPayAndShip")
        {
            ShoppingCartInfo shoppingCart;
            PayAndShip model = new PayAndShip {
                ListPaymentMode = PaymentModeManage.GetPaymentModes()
            };
            if (payId > 0)
            {
                model.CurrentPaymentMode = PaymentModeManage.GetPaymentModeById(payId);
            }
            else if ((model.ListPaymentMode != null) && (model.ListPaymentMode.Count > 0))
            {
                model.CurrentPaymentMode = model.ListPaymentMode[0];
                model.ListShippingType = this._shippingTypeManage.GetListByPay(model.CurrentPaymentMode.ModeId);
            }
            else
            {
                PaymentModeInfo info = new PaymentModeInfo {
                    ModeId = -1,
                    Name = "未选择支付方式"
                };
                model.CurrentPaymentMode = info;
            }
            if (shipId > 0)
            {
                model.CurrentShippingType = this._shippingTypeManage.GetModelByCache(shipId);
            }
            else if ((model.ListShippingType != null) && (model.ListShippingType.Count > 0))
            {
                model.CurrentShippingType = model.ListShippingType[0];
            }
            else
            {
                Maticsoft.Model.Shop.Shipping.ShippingType type = new Maticsoft.Model.Shop.Shipping.ShippingType {
                    ModeId = -1,
                    Name = "未选择配送方式"
                };
                model.CurrentShippingType = type;
            }
            if (string.IsNullOrWhiteSpace(sku))
            {
                int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
                shoppingCart = new ShoppingCartHelper(userId).GetShoppingCart();
            }
            else
            {
                Maticsoft.BLL.Shop.Products.SKUInfo info3 = new Maticsoft.BLL.Shop.Products.SKUInfo();
                Maticsoft.BLL.Shop.Products.ProductInfo info4 = new Maticsoft.BLL.Shop.Products.ProductInfo();
                Maticsoft.Model.Shop.Products.SKUInfo modelBySKU = info3.GetModelBySKU(sku);
                if (modelBySKU == null)
                {
                    ((dynamic) base.ViewBag).Freight = 0;
                    return base.View(viewName, model);
                }
                Maticsoft.Model.Shop.Products.ProductInfo productInfo = info4.GetModel(modelBySKU.ProductId);
                if (productInfo == null)
                {
                    ((dynamic) base.ViewBag).Freight = 0;
                    return base.View(viewName, model);
                }
                Maticsoft.Model.Shop.Products.ProductInfo proSaleInfo = null;
                if (c > 0)
                {
                    proSaleInfo = info4.GetProSaleModel(c);
                    if (proSaleInfo == null)
                    {
                        ((dynamic) base.ViewBag).Freight = 0;
                        return base.View(viewName, model);
                    }
                }
                shoppingCart = this.GetCartInfo4SKU(productInfo, modelBySKU, count, proSaleInfo);
            }
            ((dynamic) base.ViewBag).Freight = shoppingCart.CalcFreight(model.CurrentShippingType);
            return base.View(viewName, model);
        }

        [HttpPost]
        public ActionResult SubmitAddressInfo(FormCollection form)
        {
            bool flag = Globals.SafeBool(form["IsModify"], false);
            Maticsoft.Model.Shop.Shipping.ShippingAddress address2 = new Maticsoft.Model.Shop.Shipping.ShippingAddress {
                ShippingId = Globals.SafeInt(form["CurrentAddress.ShippingId"], -1),
                UserId = Globals.SafeInt(form["CurrentAddress.UserId"], -1),
                ShipName = form["CurrentAddress.ShipName"],
                RegionId = Globals.SafeInt(form["CurrentAddress.RegionId"], -1),
                Address = form["CurrentAddress.Address"],
                CelPhone = form["CurrentAddress.CelPhone"],
                Zipcode = form["CurrentAddress.Zipcode"]
            };
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = address2;
            if (model.ShippingId > 0)
            {
                if (flag && this._addressManage.Update(model))
                {
                    return base.RedirectToAction("ShowAddress", new { addressId = model.ShippingId });
                }
                return base.RedirectToAction("ShowAddress", new { addressId = model.ShippingId });
            }
            if (base.currentUser != null)
            {
                model.UserId = base.currentUser.UserID;
                model.ShippingId = this._addressManage.Add(model);
                if (model.ShippingId > 0)
                {
                    return base.RedirectToAction("ShowAddress", new { addressId = model.ShippingId });
                }
            }
            return base.RedirectToAction("AddressInfo");
        }

        [Obsolete]
        public ActionResult SubmitFail(string id, string viewName = "SubmitFail")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.Redirect("/");
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                long orderId = Globals.SafeLong(id, (long) (-1L));
                if (orderId < 1L)
                {
                    return base.Content("ERROR_NOTSAFEORDERID");
                }
                Maticsoft.Model.Shop.Order.OrderInfo model = this._orderManage.GetModel(orderId);
                if (model == null)
                {
                    return base.Content("ERROR_NOORDERINFO");
                }
                LogHelp.AddErrorLog(string.Concat(new object[] { "Shop >> SubmitFail >> OrderId[", orderId, "] Status[", model.OrderStatus, "]" }), "SubmitOrderFail", "Shop >> OrderController >> SubmitFail");
                ((dynamic) base.ViewBag).OrderId = model.OrderId;
            }
            return base.View(viewName);
        }

        public ActionResult SubmitOrder(string sku, int count = 1, int shippingTypeId = -1, int c = -1, string viewName = "SubmitOrder")
        {
            ShoppingCartInfo cartInfo = new ShoppingCartInfo();
            if (string.IsNullOrWhiteSpace(sku))
            {
                int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
                cartInfo = new ShoppingCartHelper(userId).GetShoppingCart();
            }
            else
            {
                Maticsoft.BLL.Shop.Products.SKUInfo info2 = new Maticsoft.BLL.Shop.Products.SKUInfo();
                Maticsoft.BLL.Shop.Products.ProductInfo info3 = new Maticsoft.BLL.Shop.Products.ProductInfo();
                Maticsoft.Model.Shop.Products.SKUInfo modelBySKU = info2.GetModelBySKU(sku);
                if (modelBySKU == null)
                {
                    return new RedirectResult("/Error");
                }
                Maticsoft.Model.Shop.Products.ProductInfo productInfo = info3.GetModel(modelBySKU.ProductId);
                if (productInfo == null)
                {
                    return new RedirectResult("/Error");
                }
                Maticsoft.Model.Shop.Products.ProductInfo proSaleInfo = null;
                if (c > 0)
                {
                    proSaleInfo = info3.GetProSaleModel(c);
                    if (proSaleInfo == null)
                    {
                        return new RedirectResult("/Error");
                    }
                    if (DateTime.Now > proSaleInfo.ProSalesEndDate)
                    {
                        return base.RedirectToAction("ProSaleDetail", "Product", new { area = "Shop", id = c });
                    }
                }
                cartInfo = this.GetCartInfo4SKU(productInfo, modelBySKU, count, proSaleInfo);
            }
            if (cartInfo.Items.Count < 1)
            {
                return (ActionResult) this.Redirect(((dynamic) base.ViewBag).BasePath + "ShoppingCart/CartInfo");
            }
            try
            {
                cartInfo = new SalesRuleProduct().GetWholeSale(cartInfo);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            Maticsoft.Model.Shop.Shipping.ShippingType model = this._shippingTypeManage.GetModel(shippingTypeId);
            ((dynamic) base.ViewBag).Freight = cartInfo.CalcFreight(model);
            ((dynamic) base.ViewBag).TotalQuantity = cartInfo.Quantity;
            ((dynamic) base.ViewBag).TotalAdjustedPrice = cartInfo.TotalAdjustedPrice;
            ((dynamic) base.ViewBag).ProductTotal = cartInfo.TotalSellPrice;
            ((dynamic) base.ViewBag).TotalPrice = cartInfo.TotalAdjustedPrice + ((dynamic) base.ViewBag).Freight;
            ((dynamic) base.ViewBag).TotalPromPrice = cartInfo.TotalSellPrice - cartInfo.TotalAdjustedPrice;
            ((dynamic) base.ViewBag).Title = "提交订单";
            return base.View(viewName, cartInfo);
        }

        public ActionResult SubmitSuccess(string id, string viewName = "SubmitSuccess")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return this.Redirect("/");
            }
            long orderId = Globals.SafeLong(id, (long) (-1L));
            if (orderId < 1L)
            {
                return base.Content("ERROR_NOTSAFEORDERID");
            }
            Maticsoft.Model.Shop.Order.OrderInfo model = this._orderManage.GetModel(orderId);
            if (model == null)
            {
                return base.Content("ERROR_NOORDERINFO");
            }
            ((dynamic) base.ViewBag).OrderId = model.OrderId;
            ((dynamic) base.ViewBag).OrderCode = model.OrderCode;
            ((dynamic) base.ViewBag).OrderAmount = model.Amount;
            return base.View(viewName);
        }
    }
}


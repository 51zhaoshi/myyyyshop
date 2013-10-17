namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.Ms;
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

    public class OrderController : MobileControllerBase
    {
        private readonly Maticsoft.BLL.Shop.Shipping.ShippingAddress _addressManage = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
        private readonly Orders _orderManage = new Orders();
        private readonly Maticsoft.BLL.Shop.Shipping.ShippingType _shippingTypeManage = new Maticsoft.BLL.Shop.Shipping.ShippingType();
        private Regions regionBll = new Regions();

        public ActionResult AddressInfo(int id = -1, string viewName = "AddressInfo")
        {
            Maticsoft.Model.Shop.Shipping.ShippingAddress model;
            if (id > 0)
            {
                model = this._addressManage.GetModel(id);
            }
            else
            {
                Maticsoft.Model.Shop.Shipping.ShippingAddress address2 = new Maticsoft.Model.Shop.Shipping.ShippingAddress {
                    ShipName = base.CurrentUser.TrueName,
                    UserId = base.CurrentUser.UserID,
                    EmailAddress = base.CurrentUser.Email,
                    CelPhone = base.CurrentUser.Phone
                };
                model = address2;
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
            int rID = modelInfoByCache.RegionId.HasValue ? modelInfoByCache.RegionId.Value : -1;
            modelInfoByCache.ShipAddress = this.regionBll.GetRegionNameByRID(rID) + "　" + modelInfoByCache.ShipAddress;
            return base.View(viewName, modelInfoByCache);
        }

        public ActionResult PayAndShipInfo(string viewName = "PayAndShipInfo")
        {
            PayAndShip model = new PayAndShip {
                ListPaymentMode = PaymentModeManage.GetPaymentModes()
            };
            int modeId = Globals.SafeInt(base.Request.Params["payId"], 0);
            int num2 = Globals.SafeInt(base.Request.QueryString["shipId"], 0);
            if (modeId > 0)
            {
                model.CurrentPaymentMode = PaymentModeManage.GetPaymentModeById(modeId);
                model.ListShippingType = this._shippingTypeManage.GetListByPay(modeId);
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
            if (num2 > 0)
            {
                model.CurrentShippingType = this._shippingTypeManage.GetModelByCache(num2);
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
            ((dynamic) base.ViewBag).PayId = modeId;
            ((dynamic) base.ViewBag).ShipId = num2;
            return base.View(viewName, model);
        }

        public ActionResult ShowAddress(string viewName = "_ShowAddress")
        {
            List<Maticsoft.Model.Shop.Shipping.ShippingAddress> modelList = this._addressManage.GetModelList(" UserId=" + base.currentUser.UserID);
            if ((modelList == null) || (modelList.Count < 1))
            {
                return base.View(viewName);
            }
            modelList[0].Address = this.regionBll.GetRegionNameByRID(modelList[0].RegionId) + "　" + modelList[0].Address;
            return base.View(viewName, modelList);
        }

        public ActionResult ShowPayAndShip(string viewName = "_ShowPayAndShip")
        {
            PayAndShip model = new PayAndShip();
            int modeId = Globals.SafeInt(base.Request.Params["payId"], 0);
            int num2 = Globals.SafeInt(base.Request.QueryString["shipId"], 0);
            model.ListPaymentMode = PaymentModeManage.GetPaymentModes();
            if (modeId > 0)
            {
                model.CurrentPaymentMode = PaymentModeManage.GetPaymentModeById(modeId);
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
            if (num2 > 0)
            {
                model.CurrentShippingType = this._shippingTypeManage.GetModelByCache(num2);
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
            ShoppingCartInfo shoppingCart = new ShoppingCartInfo();
            int userId = (base.currentUser == null) ? -1 : base.currentUser.UserID;
            shoppingCart = new ShoppingCartHelper(userId).GetShoppingCart();
            ((dynamic) base.ViewBag).Freight = shoppingCart.CalcFreight(model.CurrentShippingType);
            return base.View(viewName, model);
        }

        [HttpPost]
        public ActionResult SubmitAddressInfo(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            if (model.ShippingId > 0)
            {
                if (this._addressManage.Update(model))
                {
                    return base.Content("True");
                }
            }
            else if (base.currentUser != null)
            {
                model.UserId = base.currentUser.UserID;
                if (this._addressManage.Add(model) > 1)
                {
                    return base.Content("True");
                }
            }
            return base.Content("False");
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

        public ActionResult SubmitOrder(string sku, int count = 1, string viewName = "SubmitOrder")
        {
            if (!string.IsNullOrWhiteSpace(sku))
            {
                base.Session["SubmitOrder_SKU"] = sku;
                base.Session["SubmitOrder_COUNT"] = count;
            }
            else if (!string.IsNullOrWhiteSpace(base.Session["SubmitOrder_SKU"] as string))
            {
                sku = base.Session["SubmitOrder_SKU"] as string;
                count = Globals.SafeInt(base.Session["SubmitOrder_COUNT"], 1);
            }
            ((dynamic) base.ViewBag).SkuInfo = sku;
            ((dynamic) base.ViewBag).SkuCount = count;
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
                Maticsoft.Model.Shop.Products.ProductInfo info5 = info3.GetModel(modelBySKU.ProductId);
                if (info5 == null)
                {
                    return new RedirectResult("/Error");
                }
                ShoppingCartItem cartItem = new ShoppingCartItem {
                    MarketPrice = info5.MarketPrice.HasValue ? info5.MarketPrice.Value : 0M,
                    Name = info5.ProductName,
                    Quantity = count,
                    SellPrice = modelBySKU.SalePrice,
                    AdjustedPrice = modelBySKU.SalePrice,
                    SKU = modelBySKU.SKU,
                    ProductId = modelBySKU.ProductId,
                    UserId = base.currentUser.UserID
                };
                if (info5.SupplierId > 0)
                {
                    Maticsoft.Model.Shop.Supplier.SupplierInfo modelByCache = new Maticsoft.BLL.Shop.Supplier.SupplierInfo().GetModelByCache(info5.SupplierId);
                    if (modelByCache != null)
                    {
                        cartItem.SupplierId = new int?(modelByCache.SupplierId);
                        cartItem.SupplierName = modelByCache.Name;
                    }
                }
                List<Maticsoft.Model.Shop.Products.SKUItem> sKUItemsBySkuId = info2.GetSKUItemsBySkuId(modelBySKU.SkuId);
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
                cartItem.ThumbnailsUrl = info5.ThumbnailUrl1;
                cartItem.CostPrice = modelBySKU.CostPrice.HasValue ? modelBySKU.CostPrice.Value : 0M;
                cartItem.Weight = modelBySKU.Weight.HasValue ? modelBySKU.Weight.Value : 0;
                cartInfo.Items.Add(cartItem);
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
            int modeId = Globals.SafeInt(base.Request.QueryString["shipId"], 0);
            Maticsoft.Model.Shop.Shipping.ShippingType model = this._shippingTypeManage.GetModel(modeId);
            ((dynamic) base.ViewBag).Freight = cartInfo.CalcFreight(model);
            ((dynamic) base.ViewBag).TotalQuantity = cartInfo.Quantity;
            ((dynamic) base.ViewBag).TotalAdjustedPrice = cartInfo.TotalAdjustedPrice;
            ((dynamic) base.ViewBag).TotalPrice = cartInfo.TotalAdjustedPrice + ((dynamic) base.ViewBag).Freight;
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


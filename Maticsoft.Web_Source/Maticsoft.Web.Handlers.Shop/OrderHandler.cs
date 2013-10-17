namespace Maticsoft.Web.Handlers.Shop
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Coupon;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.BLL.Shop.Shipping;
    using Maticsoft.BLL.Shop.Supplier;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Shop.Coupon;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.Model.Shop.Supplier;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Model;
    using Maticsoft.Web;
    using Maticsoft.Web.Handlers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.SessionState;

    public class OrderHandler : HandlerBase, IRequiresSessionState
    {
        private Maticsoft.BLL.Shop.Products.ProductInfo _productInfoManage = new Maticsoft.BLL.Shop.Products.ProductInfo();
        private readonly Maticsoft.BLL.Ms.Regions _regionManage = new Maticsoft.BLL.Ms.Regions();
        private readonly Maticsoft.BLL.Shop.Shipping.ShippingAddress _shippingAddressManage = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
        private readonly Maticsoft.BLL.Shop.Shipping.ShippingType _shippingTypeManage = new Maticsoft.BLL.Shop.Shipping.ShippingType();
        private Maticsoft.BLL.Shop.Products.SKUInfo _skuInfoManage = new Maticsoft.BLL.Shop.Products.SKUInfo();
        private Maticsoft.BLL.Shop.Coupon.CouponInfo couponBll = new Maticsoft.BLL.Shop.Coupon.CouponInfo();

        private User GetBuyerUserInfo(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return null;
            }
            if (context.Session[Globals.SESSIONKEY_USER] == null)
            {
                User user = new User(new AccountsPrincipal(context.User.Identity.Name));
                context.Session[Globals.SESSIONKEY_USER] = user;
                return user;
            }
            return (User) context.Session[Globals.SESSIONKEY_USER];
        }

        private PaymentModeInfo GetPaymentModeInfo(HttpContext context)
        {
            int modeId = Globals.SafeInt(context.Request.Form["PaymentModeId"], -1);
            if (modeId < 1)
            {
                return null;
            }
            return PaymentModeManage.GetPaymentModeById(modeId);
        }

        private Maticsoft.Model.Shop.Shipping.ShippingAddress GetShippingAddress(HttpContext context)
        {
            int shippingId = Globals.SafeInt(context.Request.Form["ShippingAddressId"], -1);
            if (shippingId < 1)
            {
                return null;
            }
            return this._shippingAddressManage.GetModel(shippingId);
        }

        private Maticsoft.Model.Shop.Shipping.ShippingType GetShippingType(HttpContext context)
        {
            int modeId = Globals.SafeInt(context.Request.Form["ShippingTypeId"], -1);
            if (modeId < 1)
            {
                return null;
            }
            return this._shippingTypeManage.GetModel(modeId);
        }

        private ShoppingCartInfo GetShoppingCart(HttpContext context, User userBuyer, out ShoppingCartHelper shoppingCartHelper)
        {
            ShoppingCartInfo cartInfo = null;
            string str = context.Request.Form["SkuInfos"];
            if (string.IsNullOrWhiteSpace(str))
            {
                shoppingCartHelper = new ShoppingCartHelper(userBuyer.UserID);
                cartInfo = shoppingCartHelper.GetShoppingCart();
            }
            else
            {
                JsonArray array;
                shoppingCartHelper = null;
                try
                {
                    array = JsonConvert.Import<JsonArray>(str);
                }
                catch (Exception)
                {
                    throw;
                }
                if ((array == null) || (array.Length < 1))
                {
                    return null;
                }
                JsonObject obj2 = array.GetObject(0);
                string sku = obj2["SKU"].ToString();
                int num = Globals.SafeInt(obj2["Count"].ToString(), 1);
                int id = Globals.SafeInt(obj2["ProSales"].ToString(), -1);
                Maticsoft.Model.Shop.Products.SKUInfo modelBySKU = this._skuInfoManage.GetModelBySKU(sku);
                if (modelBySKU == null)
                {
                    return null;
                }
                Maticsoft.Model.Shop.Products.ProductInfo model = this._productInfoManage.GetModel(modelBySKU.ProductId);
                if (model == null)
                {
                    return null;
                }
                ShoppingCartItem itemInfo = new ShoppingCartItem {
                    MarketPrice = model.MarketPrice.HasValue ? model.MarketPrice.Value : 0M,
                    Name = model.ProductName,
                    Quantity = num,
                    SellPrice = modelBySKU.SalePrice,
                    AdjustedPrice = modelBySKU.SalePrice,
                    SKU = modelBySKU.SKU,
                    ProductId = modelBySKU.ProductId,
                    UserId = userBuyer.UserID
                };
                if (id > 0)
                {
                    Maticsoft.Model.Shop.Products.ProductInfo proSaleModel = this._productInfoManage.GetProSaleModel(id);
                    if (proSaleModel == null)
                    {
                        return null;
                    }
                    if (DateTime.Now > proSaleModel.ProSalesEndDate)
                    {
                        throw new ArgumentNullException("活动已过期");
                    }
                    itemInfo.AdjustedPrice = proSaleModel.ProSalesPrice;
                }
                List<Maticsoft.Model.Shop.Products.SKUItem> sKUItemsBySkuId = this._skuInfoManage.GetSKUItemsBySkuId(modelBySKU.SkuId);
                if ((sKUItemsBySkuId != null) && (sKUItemsBySkuId.Count > 0))
                {
                    itemInfo.SkuValues = new string[sKUItemsBySkuId.Count];
                    int index = 0;
                    sKUItemsBySkuId.ForEach(delegate (Maticsoft.Model.Shop.Products.SKUItem xx) {
                        itemInfo.SkuValues[index++] = xx.ValueStr;
                        if (!string.IsNullOrWhiteSpace(xx.ImageUrl))
                        {
                            itemInfo.SkuImageUrl = xx.ImageUrl;
                        }
                    });
                }
                itemInfo.ThumbnailsUrl = model.ThumbnailUrl1;
                itemInfo.CostPrice = modelBySKU.CostPrice.HasValue ? modelBySKU.CostPrice.Value : 0M;
                itemInfo.Weight = modelBySKU.Weight.HasValue ? modelBySKU.Weight.Value : 0;
                Maticsoft.Model.Shop.Supplier.SupplierInfo modelByCache = new Maticsoft.BLL.Shop.Supplier.SupplierInfo().GetModelByCache(model.SupplierId);
                if (modelByCache != null)
                {
                    itemInfo.SupplierId = new int?(modelByCache.SupplierId);
                    itemInfo.SupplierName = modelByCache.Name;
                }
                cartInfo = new ShoppingCartInfo();
                cartInfo.Items.Add(itemInfo);
            }
            try
            {
                cartInfo = new SalesRuleProduct().GetWholeSale(cartInfo);
            }
            catch (Exception)
            {
                return null;
            }
            return cartInfo;
        }

        public override void ProcessRequest(HttpContext context)
        {
            string str = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                string str2;
                if (((str2 = str) != null) && (str2 == "SubmitOrder"))
                {
                    context.Response.Write(this.SubmitOrder(context));
                }
            }
            catch (Exception exception)
            {
                JsonObject obj2 = new JsonObject();
                obj2.Put("STATUS", "ERROR");
                obj2.Put("DATA", exception);
                context.Response.Write(obj2.ToString());
            }
        }

        private string SubmitOrder(HttpContext context)
        {
            ShoppingCartInfo info2;
            Action<ShoppingCartItem> action = null;
            ShoppingCartHelper shoppingCartHelper;
            OrderInfo mainOrder;
            Maticsoft.Model.Shop.Order.OrderItems tmpOrderItem;
            JsonObject obj2 = new JsonObject();
            PaymentModeInfo paymentModeInfo = this.GetPaymentModeInfo(context);
            if (paymentModeInfo == null)
            {
                obj2.Accumulate("STATUS", "NOPAYMENTMODEINFO");
                return obj2.ToString();
            }
            User buyerUserInfo = this.GetBuyerUserInfo(context);
            if (buyerUserInfo == null)
            {
                obj2.Accumulate("STATUS", "NOLOGIN");
                return obj2.ToString();
            }
            if (buyerUserInfo.UserType == "AA")
            {
                obj2.Accumulate("STATUS", "UNAUTHORIZED");
                return obj2.ToString();
            }
            try
            {
                info2 = this.GetShoppingCart(context, buyerUserInfo, out shoppingCartHelper);
            }
            catch (ArgumentNullException)
            {
                obj2.Accumulate("STATUS", "PROSALEEXPIRED");
                return obj2.ToString();
            }
            if (((info2 == null) || (info2.Items == null)) || (info2.Items.Count < 1))
            {
                obj2.Accumulate("STATUS", "NOSHOPPINGCARTINFO");
                return obj2.ToString();
            }
            List<ShoppingCartItem> list = new List<ShoppingCartItem>();
            foreach (ShoppingCartItem item in info2.Items)
            {
                if (item.Quantity > this._skuInfoManage.GetStockBySKU(item.SKU))
                {
                    list.Add(item);
                }
            }
            if (list.Count > 0)
            {
                obj2.Accumulate("STATUS", "NOSTOCK");
                obj2.Accumulate("DATA", list);
                if (shoppingCartHelper != null)
                {
                    if (action == null)
                    {
                        action = delegate (ShoppingCartItem info) {
                            shoppingCartHelper.RemoveItem(info.ItemId);
                        };
                    }
                    list.ForEach(action);
                }
                return obj2.ToString();
            }
            Maticsoft.Model.Shop.Shipping.ShippingAddress shippingAddress = this.GetShippingAddress(context);
            if (shippingAddress == null)
            {
                obj2.Accumulate("STATUS", "NOSHIPPINGADDRESS");
                return obj2.ToString();
            }
            Maticsoft.Model.Ms.Regions modelByCache = this._regionManage.GetModelByCache(shippingAddress.RegionId);
            if (modelByCache == null)
            {
                obj2.Accumulate("STATUS", "NOREGIONINFO");
                return obj2.ToString();
            }
            Maticsoft.Model.Shop.Shipping.ShippingType shippingType = this.GetShippingType(context);
            if (shippingType == null)
            {
                obj2.Accumulate("STATUS", "NOSHIPPINGTYPE");
                return obj2.ToString();
            }
            mainOrder = new OrderInfo {
                CreatedDate = DateTime.Now,
                OrderCode = mainOrder.CreatedDate.ToString("yyyyMMddHHmmssfff"),
                PaymentTypeId = paymentModeInfo.ModeId,
                PaymentTypeName = paymentModeInfo.Name,
                PaymentGateway = paymentModeInfo.Gateway,
                Weight = new int?(info2.TotalWeight),
                FreightAdjusted = mainOrder.FreightActual = mainOrder.Freight = new decimal?(info2.CalcFreight(shippingType)),
                CouponAmount = 0
            };
            string couponCode = context.Request.Form["Coupon"];
            Maticsoft.Model.Shop.Coupon.CouponInfo couponInfo = this.couponBll.GetCouponInfo(couponCode, false);
            if (couponInfo != null)
            {
                mainOrder.CouponAmount = new decimal?(couponInfo.CouponPrice);
                mainOrder.CouponCode = couponInfo.CouponCode;
                mainOrder.CouponName = couponInfo.CouponName;
                mainOrder.CouponValue = new decimal?(couponInfo.CouponPrice);
                mainOrder.CouponValueType = 1;
            }
            mainOrder.ProductTotal = info2.TotalSellPrice;
            decimal totalCostPrice = info2.TotalCostPrice;
            mainOrder.OrderCostPrice = totalCostPrice + mainOrder.FreightActual;
            mainOrder.OrderTotal = info2.TotalSellPrice + mainOrder.Freight.Value;
            mainOrder.Amount = (info2.TotalAdjustedPrice + mainOrder.FreightAdjusted.Value) - mainOrder.CouponAmount.Value;
            mainOrder.OrderType = 1;
            mainOrder.OrderStatus = 0;
            mainOrder.BuyerID = buyerUserInfo.UserID;
            mainOrder.BuyerName = buyerUserInfo.UserName;
            mainOrder.BuyerEmail = string.IsNullOrWhiteSpace(buyerUserInfo.Email) ? "pay@maticsoft.com" : buyerUserInfo.Email;
            mainOrder.BuyerCellPhone = buyerUserInfo.Phone;
            Dictionary<int, List<Maticsoft.Model.Shop.Order.OrderItems>> dicSuppOrderItems = new Dictionary<int, List<Maticsoft.Model.Shop.Order.OrderItems>>();
            int orderPoint = 0;
            info2.Items.ForEach(delegate (ShoppingCartItem item) {
                Maticsoft.Model.Shop.Order.OrderItems items = new Maticsoft.Model.Shop.Order.OrderItems {
                    Name = item.Name,
                    SKU = item.SKU,
                    Quantity = item.Quantity,
                    ShipmentQuantity = item.Quantity,
                    ThumbnailsUrl = item.ThumbnailsUrl,
                    Points = item.Points,
                    Weight = item.Weight,
                    ProductId = item.ProductId,
                    Description = item.Description,
                    CostPrice = item.CostPrice,
                    SellPrice = item.SellPrice,
                    AdjustedPrice = item.AdjustedPrice,
                    Deduct = new decimal?(item.SellPrice - item.AdjustedPrice),
                    SupplierId = item.SupplierId,
                    SupplierName = item.SupplierName
                };
                tmpOrderItem = items;
                if ((item.SkuValues != null) && (item.SkuValues.Length > 0))
                {
                    tmpOrderItem.Attribute = string.Join(",", item.SkuValues);
                }
                mainOrder.OrderItems.Add(tmpOrderItem);
                if (tmpOrderItem.SupplierId.HasValue && (tmpOrderItem.SupplierId.Value > 0))
                {
                    if (dicSuppOrderItems.ContainsKey(tmpOrderItem.SupplierId.Value))
                    {
                        dicSuppOrderItems[tmpOrderItem.SupplierId.Value].Add(tmpOrderItem);
                    }
                    else
                    {
                        List<Maticsoft.Model.Shop.Order.OrderItems> list = new List<Maticsoft.Model.Shop.Order.OrderItems> {
                            tmpOrderItem
                        };
                        dicSuppOrderItems.Add(tmpOrderItem.SupplierId.Value, list);
                    }
                }
                orderPoint += tmpOrderItem.Points;
            });
            mainOrder.OrderPoint = orderPoint;
            mainOrder.RegionId = new int?(shippingAddress.RegionId);
            mainOrder.ShipRegion = modelByCache.RegionName;
            mainOrder.ShipName = shippingAddress.ShipName;
            mainOrder.ShipEmail = shippingAddress.EmailAddress;
            mainOrder.ShipCellPhone = shippingAddress.CelPhone;
            mainOrder.ShipTelPhone = shippingAddress.TelPhone;
            mainOrder.ShipAddress = shippingAddress.Address;
            mainOrder.ShipZipCode = shippingAddress.Zipcode;
            mainOrder.ShippingModeId = new int?(shippingType.ModeId);
            mainOrder.ShippingModeName = shippingType.Name;
            mainOrder.RealShippingModeId = new int?(shippingType.ModeId);
            mainOrder.RealShippingModeName = shippingType.Name;
            mainOrder.ShippingStatus = 0;
            mainOrder.ExpressCompanyName = shippingType.ExpressCompanyName;
            mainOrder.ExpressCompanyAbb = shippingType.ExpressCompanyEn;
            Maticsoft.BLL.Shop.Supplier.SupplierInfo info4 = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
            if (dicSuppOrderItems.Count > 1)
            {
                foreach (KeyValuePair<int, List<Maticsoft.Model.Shop.Order.OrderItems>> pair in dicSuppOrderItems)
                {
                    OrderInfo subOrder;
                    subOrder = new OrderInfo(mainOrder) {
                        Weight = 0,
                        FreightAdjusted = subOrder.FreightActual = subOrder.Freight = 0,
                        OrderPoint = 0,
                        ProductTotal = 0M,
                        OrderCostPrice = 0,
                        OrderOptionPrice = 0,
                        OrderProfit = 0,
                        Amount = 0M
                    };
                    pair.Value.ForEach(delegate (Maticsoft.Model.Shop.Order.OrderItems info) {
                        int? weight = subOrder.Weight;
                        int num = info.Weight;
                        subOrder.Weight = weight.HasValue ? new int?(weight.GetValueOrDefault() + num) : null;
                        subOrder.OrderPoint += info.Points;
                        subOrder.ProductTotal += info.SellPrice * info.Quantity;
                        decimal? orderCostPrice = subOrder.OrderCostPrice;
                        decimal num2 = info.CostPrice * info.Quantity;
                        subOrder.OrderCostPrice = orderCostPrice.HasValue ? new decimal?(orderCostPrice.GetValueOrDefault() + num2) : null;
                        subOrder.Amount += info.AdjustedPrice * info.Quantity;
                    });
                    decimal? freightAdjusted = mainOrder.FreightAdjusted;
                    decimal count = dicSuppOrderItems.Count;
                    subOrder.FreightAdjusted = subOrder.FreightActual = subOrder.Freight = freightAdjusted.HasValue ? new decimal?(freightAdjusted.GetValueOrDefault() / count) : null;
                    subOrder.OrderTotal = subOrder.ProductTotal + subOrder.Freight.Value;
                    subOrder.Amount += subOrder.FreightAdjusted.Value;
                    subOrder.OrderItems = pair.Value;
                    subOrder.OrderType = 2;
                    Maticsoft.Model.Shop.Supplier.SupplierInfo info5 = info4.GetModelByCache(pair.Key);
                    if (info5 == null)
                    {
                        obj2.Accumulate("STATUS", "NOSUPPLIERINFO");
                        return obj2.ToString();
                    }
                    subOrder.SupplierId = new int?(info5.SupplierId);
                    subOrder.SupplierName = info5.Name;
                    subOrder.CreatedDate = DateTime.Now;
                    subOrder.OrderCode = subOrder.CreatedDate.ToString("yyyyMMddHHmmssfff");
                    mainOrder.SubOrders.Add(subOrder);
                }
                mainOrder.HasChildren = true;
            }
            else
            {
                mainOrder.SupplierId = info2.Items[0].SupplierId;
                mainOrder.SupplierName = info2.Items[0].SupplierName;
                mainOrder.HasChildren = false;
            }
            try
            {
                mainOrder.OrderId = OrderManage.CreateOrder(mainOrder);
            }
            catch (Exception exception)
            {
                LogHelp.AddErrorLog("订单创建失败: " + exception.Message, exception.StackTrace, context.Request);
            }
            obj2.Accumulate("DATA", new { OrderId = mainOrder.OrderId, OrderCode = mainOrder.OrderCode, Amount = mainOrder.Amount, PaymentTypeId = mainOrder.PaymentTypeId, PaymentTypeName = mainOrder.PaymentTypeName });
            if (mainOrder.OrderId == -1L)
            {
                obj2.Accumulate("STATUS", "FAILED");
                return obj2.ToString();
            }
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                this.couponBll.UseCoupon(couponCode, mainOrder.BuyerID, mainOrder.BuyerEmail);
            }
            if (shoppingCartHelper != null)
            {
                shoppingCartHelper.ClearShoppingCart();
            }
            obj2.Accumulate("STATUS", "SUCCESS");
            return obj2.ToString();
        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


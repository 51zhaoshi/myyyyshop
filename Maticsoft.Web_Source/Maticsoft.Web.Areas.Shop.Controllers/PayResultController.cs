namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.Pay;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.Common;
    using Maticsoft.Model.Pay;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Web;
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class PayResultController : ShopControllerBase
    {
        private Maticsoft.BLL.Shop.Order.OrderItems orderItemManage = new Maticsoft.BLL.Shop.Order.OrderItems();
        private Orders orderManage = new Orders();
        private Maticsoft.BLL.Pay.RechargeRequest rechargeBll = new Maticsoft.BLL.Pay.RechargeRequest();

        public ActionResult Fail(string viewName = "Fail")
        {
            string str = base.Session["PaymentReturn_OrderId"] as string;
            string str2 = base.Session["PaymentReturn_Status"] as string;
            if (string.IsNullOrWhiteSpace(str2))
            {
                return this.Redirect("/");
            }
            base.Session.Remove("PaymentReturn_OrderId");
            base.Session.Remove("PaymentReturn_Status");
            if (!string.IsNullOrWhiteSpace(str))
            {
                long orderId = Globals.SafeLong(str, (long) (-1L));
                if (orderId < 1L)
                {
                    return base.Content("ERROR_NOTSAFEORDERID");
                }
                OrderInfo model = this.orderManage.GetModel(orderId);
                LogHelp.AddErrorLog(string.Concat(new object[] { "Shop >> PaymentFail >> OrderId[", orderId, "] Status[", str2, "]" }), str2, "Shop >> PaymentReturnHandler >> Redirect >> PayController");
                ((dynamic) base.ViewBag).OrderId = model.OrderId;
            }
            ((dynamic) base.ViewBag).PayStatus = str2;
            return base.View(viewName);
        }

        public ActionResult RechargeFail(string viewName = "RechargeFail")
        {
            string str = base.Session["RechargeReturn_RechargeId"] as string;
            string str2 = base.Session["RechargeReturn_Status"] as string;
            if (string.IsNullOrWhiteSpace(str2))
            {
                return this.Redirect("/");
            }
            base.Session.Remove("RechargeReturn_RechargeId");
            base.Session.Remove("RechargeReturn_Status");
            if (!string.IsNullOrWhiteSpace(str))
            {
                long num = Globals.SafeLong(str, (long) (-1L));
                if (num < 1L)
                {
                    return base.Content("ERROR_NOTSAFEORDERID");
                }
                LogHelp.AddErrorLog(string.Concat(new object[] { "Shop >> RechargeFail >> RechargeId[", num, "] Status[", str2, "]" }), str2, "Shop >> RechargeReturnHandler >> Redirect >> PayController");
                ((dynamic) base.ViewBag).RechargeId = num;
            }
            ((dynamic) base.ViewBag).PayStatus = str2;
            return base.View(viewName);
        }

        public ActionResult RechargeSuccess(string viewName = "RechargeSuccess")
        {
            string str = base.Session["RechargeReturn_RechargeId"] as string;
            string str2 = base.Session["RechargeReturn_Status"] as string;
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(str2))
            {
                return this.Redirect("/");
            }
            long rechargeId = Globals.SafeLong(str, (long) (-1L));
            if (str2 != "success")
            {
                return base.Content("ERROR_NOSUCCESS");
            }
            if (rechargeId < 1L)
            {
                return base.Content("ERROR_NOTSAFEORDERID");
            }
            base.Session.Remove("RechargeReturn_RechargeId");
            base.Session.Remove("RechargeReturn_Status");
            Maticsoft.Model.Pay.RechargeRequest model = this.rechargeBll.GetModel(rechargeId);
            ((dynamic) base.ViewBag).RechargeId = model.RechargeId;
            ((dynamic) base.ViewBag).RechargeBlance = model.RechargeBlance;
            return base.View(viewName);
        }

        public ActionResult Success(string viewName = "Success")
        {
            string str = base.Session["PaymentReturn_OrderId"] as string;
            string str2 = base.Session["PaymentReturn_Status"] as string;
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(str2))
            {
                return this.Redirect("/");
            }
            long orderId = Globals.SafeLong(str, (long) (-1L));
            if (str2 != "success")
            {
                return base.Content("ERROR_NOSUCCESS");
            }
            if (orderId < 1L)
            {
                return base.Content("ERROR_NOTSAFEORDERID");
            }
            base.Session.Remove("PaymentReturn_OrderId");
            base.Session.Remove("PaymentReturn_Status");
            OrderInfo model = this.orderManage.GetModel(orderId);
            ((dynamic) base.ViewBag).OrderId = model.OrderId;
            ((dynamic) base.ViewBag).OrderCode = model.OrderCode;
            ((dynamic) base.ViewBag).ShipName = model.ShipName;
            ((dynamic) base.ViewBag).ItemsCount = this.orderItemManage.GetOrderItemCountByOrderId(orderId);
            return base.View(viewName);
        }
    }
}


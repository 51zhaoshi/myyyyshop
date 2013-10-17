namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Payment;
    using Maticsoft.Payment.Handler;
    using Maticsoft.Payment.Model;
    using Maticsoft.Web;
    using System;
    using System.Web;

    public class SendPaymentHandler : SendPaymentHandlerBase<OrderInfo>
    {
        private readonly Orders _orderManage;
        private const string MSG_ERRORLOG = "SendPaymentHandler >> Verification[{0}] 操作用户[{1}] 已阻止非法方式支付订单!";

        public SendPaymentHandler() : base(new PaymentOption())
        {
            this._orderManage = new Orders();
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.System);
            base.HostName = set.WebName;
        }

        protected override TradeInfo GetTrade(string orderIdStr, decimal orderTotal, OrderInfo order)
        {
            return new TradeInfo { Body = base.HostName + "订单 - 订单号: [" + order.OrderCode + "]", BuyerEmailAddress = order.BuyerEmail, Date = order.OrderDate, OrderId = orderIdStr, Showurl = Globals.HostPath(HttpContext.Current.Request.Url), Subject = base.HostName + "订单 - 订单号: [" + order.OrderCode + "] - 在线支付 - 订单支付金额: " + orderTotal.ToString("0.00") + " 元", TotalMoney = orderTotal };
        }

        protected override bool VerifySendPayment(HttpContext context)
        {
            User user;
            string[] strArray = OrderProcessor.GetQueryString4OrderIds(context.Request);
            if ((strArray == null) || (strArray.Length < 1))
            {
                return false;
            }
            long orderId = Globals.SafeLong(strArray[0], (long) (-1L));
            if (orderId < -1L)
            {
                return false;
            }
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("/Account/Login");
                return false;
            }
            if (context.Session[Globals.SESSIONKEY_USER] == null)
            {
                user = new User(new AccountsPrincipal(context.User.Identity.Name));
                context.Session[Globals.SESSIONKEY_USER] = user;
            }
            else
            {
                user = (User) context.Session[Globals.SESSIONKEY_USER];
            }
            OrderInfo model = this._orderManage.GetModel(orderId);
            if (model == null)
            {
                LogHelp.AddErrorLog(string.Format("SendPaymentHandler >> Verification[{0}] 操作用户[{1}] 已阻止非法方式支付订单!", orderId, user.UserID), "非法操作订单", "Shop >> SendPaymentHandler >> Verificatio >> OrderInfo Is NULL");
                context.Response.Redirect("/");
                return false;
            }
            if (model.BuyerID != user.UserID)
            {
                LogHelp.AddErrorLog(string.Format("SendPaymentHandler >> Verification[{0}] 操作用户[{1}] 已阻止非法方式支付订单!", orderId, user.UserID), "非法操作订单", "Shop >> SendPaymentHandler >> Verificatio >> Check BuyerID");
                context.Response.Redirect("/");
                return false;
            }
            return true;
        }
    }
}


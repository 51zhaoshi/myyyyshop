namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Payment.Model;
    using System;

    public class PaymentOption : IPaymentOption<OrderInfo>
    {
        private const string _notifyUrl = "/pay/payment/{0}/notify_url.aspx";
        private readonly Orders _orderManage = new Orders();
        private const string _returnUrl = "/pay/payment/{0}/return_url.aspx";

        public OrderInfo GetOrderInfo(string orderIdStr)
        {
            long orderId = Globals.SafeLong(orderIdStr, (long) (-1L));
            if (orderId < 1L)
            {
                return null;
            }
            return this._orderManage.GetModel(orderId);
        }

        public bool PayForOrder(OrderInfo orderInfo)
        {
            return OrderManage.PayForOrder(orderInfo, null);
        }

        public string NotifyUrl
        {
            get
            {
                return "/pay/payment/{0}/notify_url.aspx";
            }
        }

        public string ReturnUrl
        {
            get
            {
                return "/pay/payment/{0}/return_url.aspx";
            }
        }
    }
}


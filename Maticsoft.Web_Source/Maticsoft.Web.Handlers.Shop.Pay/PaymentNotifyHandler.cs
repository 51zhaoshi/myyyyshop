namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.Payment.Handler;
    using System;

    public class PaymentNotifyHandler : PaymentReturnHandlerBase<OrderInfo>
    {
        public PaymentNotifyHandler() : base(new PaymentOption(), true)
        {
        }
    }
}


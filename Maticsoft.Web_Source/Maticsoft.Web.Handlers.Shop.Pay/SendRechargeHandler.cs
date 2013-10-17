namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.Payment.Handler;
    using System;

    public class SendRechargeHandler : SendRechargeHandlerBase
    {
        public SendRechargeHandler() : base(new RechargeOption())
        {
        }
    }
}


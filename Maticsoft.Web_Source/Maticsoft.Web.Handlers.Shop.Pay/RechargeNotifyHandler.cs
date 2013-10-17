namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.Payment.Handler;
    using System;

    public class RechargeNotifyHandler : RechargeReturnHandlerBase
    {
        public RechargeNotifyHandler() : base(new RechargeOption(), true)
        {
        }
    }
}


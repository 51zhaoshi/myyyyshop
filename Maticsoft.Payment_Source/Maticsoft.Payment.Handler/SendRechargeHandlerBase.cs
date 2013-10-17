namespace Maticsoft.Payment.Handler
{
    using Maticsoft.Payment.Model;
    using System;

    public abstract class SendRechargeHandlerBase : SendRechargeHandlerBase<RechargeRequestInfo, UserInfo>
    {
        protected SendRechargeHandlerBase(IRechargeOption<RechargeRequestInfo, UserInfo> option) : base(option)
        {
        }
    }
}


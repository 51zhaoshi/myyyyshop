namespace Maticsoft.Payment.Handler
{
    using Maticsoft.Payment.Model;
    using System;

    public abstract class RechargeReturnHandlerBase : RechargeReturnHandlerBase<RechargeRequestInfo, UserInfo>
    {
        protected RechargeReturnHandlerBase(IRechargeOption<RechargeRequestInfo, UserInfo> option, bool _isBackRequest) : base(option, _isBackRequest)
        {
        }
    }
}


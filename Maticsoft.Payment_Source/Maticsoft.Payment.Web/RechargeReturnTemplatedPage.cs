namespace Maticsoft.Payment.Web
{
    using System;

    [Obsolete]
    public abstract class RechargeReturnTemplatedPage : RechargeReturnTemplatedPage<RechargeRequestInfo>
    {
        protected RechargeReturnTemplatedPage(bool _isBackRequest) : base(_isBackRequest)
        {
        }
    }
}


namespace Maticsoft.Payment.Web
{
    using System;

    [Obsolete]
    public abstract class RechargeReturnTemplatedController : RechargeReturnTemplatedController<RechargeRequestInfo>
    {
        protected RechargeReturnTemplatedController(bool _isBackRequest) : base(_isBackRequest)
        {
        }
    }
}


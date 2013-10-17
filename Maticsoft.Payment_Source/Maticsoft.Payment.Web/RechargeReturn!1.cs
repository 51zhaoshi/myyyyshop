namespace Maticsoft.Payment.Web
{
    using System;

    public abstract class RechargeReturn<T> : RechargeReturnTemplatedPage<T> where T: class, IRechargeRequest, new()
    {
        public RechargeReturn() : base(false)
        {
        }

        protected override void DisplayMessage(string status)
        {
            throw new NotImplementedException();
        }
    }
}


namespace Maticsoft.Payment.Web
{
    using System;

    public abstract class RechargeNotify<T> : RechargeReturnTemplatedPage<T> where T: class, IRechargeRequest, new()
    {
        public RechargeNotify() : base(true)
        {
        }

        protected override void DisplayMessage(string status)
        {
        }
    }
}


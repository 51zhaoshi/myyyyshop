namespace Maticsoft.Payment.Model
{
    using System;

    public enum RefundStatus
    {
        All = 0x63,
        None = 0,
        Refund = 2,
        Reject = 3,
        Requested = 1
    }
}


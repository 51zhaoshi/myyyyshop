namespace Maticsoft.Payment.Model
{
    using System;

    public interface IRechargeRequest
    {
        string PaymentGateway { get; set; }

        int PaymentTypeId { get; set; }

        decimal RechargeBlance { get; set; }

        long RechargeId { get; set; }

        DateTime TradeDate { get; set; }

        int UserId { get; set; }
    }
}


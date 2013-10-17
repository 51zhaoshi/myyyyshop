namespace Maticsoft.Payment.Model
{
    using System;
    using System.Runtime.CompilerServices;

    public class RechargeRequestInfo : IRechargeRequest
    {
        public string PaymentGateway { get; set; }

        public int PaymentTypeId { get; set; }

        public decimal RechargeBlance { get; set; }

        public long RechargeId { get; set; }

        public DateTime TradeDate { get; set; }

        public int UserId { get; set; }
    }
}


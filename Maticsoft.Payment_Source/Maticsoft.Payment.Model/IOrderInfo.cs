namespace Maticsoft.Payment.Model
{
    using System;

    public interface IOrderInfo
    {
        decimal Amount { get; set; }

        string BuyerEmail { get; set; }

        string GatewayOrderId { get; set; }

        DateTime OrderDate { get; set; }

        Maticsoft.Payment.Model.OrderStatus OrderStatus { get; }

        Maticsoft.Payment.Model.PaymentStatus PaymentStatus { get; }

        int PaymentTypeId { get; set; }

        Maticsoft.Payment.Model.RefundStatus RefundStatus { get; }

        Maticsoft.Payment.Model.ShippingStatus ShippingStatus { get; }
    }
}


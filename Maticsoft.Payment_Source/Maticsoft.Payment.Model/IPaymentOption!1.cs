namespace Maticsoft.Payment.Model
{
    using System;

    public interface IPaymentOption<T> where T: class, IOrderInfo
    {
        T GetOrderInfo(string orderId);
        bool PayForOrder(T order);

        string NotifyUrl { get; }

        string ReturnUrl { get; }
    }
}


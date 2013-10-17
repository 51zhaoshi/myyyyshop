namespace Maticsoft.ViewModel.Shop
{
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PayAndShip
    {
        public PaymentModeInfo CurrentPaymentMode { get; set; }

        public ShippingType CurrentShippingType { get; set; }

        public List<PaymentModeInfo> ListPaymentMode { get; set; }

        public List<ShippingType> ListShippingType { get; set; }
    }
}


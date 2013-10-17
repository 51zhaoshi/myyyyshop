namespace Maticsoft.Model.Shop.Shipping
{
    using System;

    [Serializable]
    public class ShippingPayment
    {
        private int _paymentmodeid;
        private int _shippingmodeid;

        public int PaymentModeId
        {
            get
            {
                return this._paymentmodeid;
            }
            set
            {
                this._paymentmodeid = value;
            }
        }

        public int ShippingModeId
        {
            get
            {
                return this._shippingmodeid;
            }
            set
            {
                this._shippingmodeid = value;
            }
        }
    }
}


namespace Maticsoft.Model.Shop.Order
{
    using System;

    public static class EnumHelper
    {
        public enum OrderMainStatus
        {
            Cancel = 3,
            Complete = 9,
            Handling = 6,
            Locking = 4,
            None = -1,
            Paying = 1,
            PreConfirm = 5,
            PreHandle = 2,
            Shiped = 8,
            Shipping = 7
        }

        public enum OrderStatus
        {
            AdminLock = -3,
            Cancel = -1,
            Complete = 2,
            Handling = 1,
            SystemLock = -4,
            UnHandle = 0,
            UserLock = -2
        }

        public enum PaymentGateway
        {
            cod,
            bank,
            other
        }

        public enum PaymentStatus
        {
            Handling = 3,
            None = -1,
            Paid = 2,
            PayException = 4,
            PreConfirm = 1,
            Unpaid = 0
        }

        public enum ShippingStatus
        {
            ConfirmShip = 3,
            None = -1,
            Packing = 1,
            RejectedReturned = 5,
            RejectedReturning = 4,
            Shipped = 2,
            UnShipped = 0
        }
    }
}


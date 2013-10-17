namespace Maticsoft.Payment
{
    using Maticsoft.Payment.Model;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;

    public static class OrderProcessor
    {
        private static readonly string LockKey = "LOCK";
        public const char OrderIdsSplitChar = ';';

        public static bool CheckAction<T>(T order, OrderActions action) where T: IOrderInfo
        {
            if (order.OrderStatus == OrderStatus.InProgress)
            {
                switch (action)
                {
                    case OrderActions.BUYER_PAY:
                    case OrderActions.BUYER_MODIFY_DELIVER_ADDRESS:
                    case OrderActions.BUYER_MODIFY_PAYMENT_MODE:
                    case OrderActions.BUYER_MODIFY_SHIPPING_MODE:
                    case OrderActions.BUYER_CANCEL:
                    case OrderActions.SELLER_CONFIRM_PAY:
                    case OrderActions.SELLER_CLOSE_TRADE:
                    case OrderActions.SELLER_MODIFY_TRADE:
                        return (order.PaymentStatus == PaymentStatus.NotYet);

                    case OrderActions.BUYER_REFUND:
                        return ((order.PaymentStatus == PaymentStatus.Prepaid) && (order.RefundStatus == RefundStatus.None));

                    case OrderActions.BUYER_CANCEL_REFUND:
                    case OrderActions.SELLER_REJECT_REFUND:
                    case OrderActions.SELLER_ACCEPT_REFUND:
                        return ((order.PaymentStatus == PaymentStatus.Prepaid) && (order.RefundStatus == RefundStatus.Requested));

                    case OrderActions.BUYER_CONFIRM_GOODS:
                    case OrderActions.SELLER_FINISH_TRADE:
                        return ((order.ShippingStatus == ShippingStatus.Delivered) && (order.RefundStatus == RefundStatus.None));

                    case OrderActions.SELLER_SEND_GOODS:
                        if (order.RefundStatus == RefundStatus.None)
                        {
                            if (order.ShippingStatus != ShippingStatus.NotYet)
                            {
                                return (order.ShippingStatus == ShippingStatus.Packing);
                            }
                            return true;
                        }
                        return false;

                    case OrderActions.SELLER_PACK_GOODS:
                        return ((order.RefundStatus == RefundStatus.None) && (order.ShippingStatus == ShippingStatus.NotYet));
                }
            }
            return false;
        }

        public static string GenerateOrderId()
        {
            lock (LockKey)
            {
                StringBuilder builder = new StringBuilder(DateTime.Now.ToString("yyyyMMdd"));
                Random random = new Random();
                for (int i = 0; i < 7; i++)
                {
                    builder.Append((char) (0x30 + ((ushort) (random.Next() % 10))));
                }
                return builder.ToString();
            }
        }

        public static string[] GenerateOrderId(int maxNum)
        {
            if (maxNum < 2)
            {
                return new string[] { GenerateOrderId() };
            }
            string[] strArray = new string[maxNum];
            int index = 0;
            while (index < strArray.Length)
            {
                strArray[index] = GenerateOrderId() + "-" + ++index;
            }
            return strArray;
        }

        public static string[] GetQueryString4OrderIds(HttpRequest request)
        {
            string str;
            return GetQueryString4OrderIds(request, out str);
        }

        public static string[] GetQueryString4OrderIds(HttpRequest request, out string orderIdStr)
        {
            orderIdStr = request.QueryString["OrderIds"];
            if (string.IsNullOrEmpty(orderIdStr))
            {
                return null;
            }
            return orderIdStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}


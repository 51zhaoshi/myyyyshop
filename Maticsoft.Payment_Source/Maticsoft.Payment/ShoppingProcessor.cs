namespace Maticsoft.Payment
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;

    [Obsolete]
    public sealed class ShoppingProcessor
    {
        private static readonly string LockKey = "LOCK";
        public const char OrderIdsSplitChar = ';';

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

        public static string[] GetQueryStringForOrderIds(HttpRequest request)
        {
            string orderIdStr = string.Empty;
            return GetQueryStringForOrderIds(request, out orderIdStr);
        }

        public static string[] GetQueryStringForOrderIds(HttpRequest request, out string orderIdStr)
        {
            orderIdStr = request.QueryString["OrderIds"];
            if (string.IsNullOrEmpty(orderIdStr))
            {
                HttpContext.Current.Response.Redirect("~/");
                return null;
            }
            return orderIdStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}


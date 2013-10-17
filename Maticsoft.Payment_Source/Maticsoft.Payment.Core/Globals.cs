namespace Maticsoft.Payment.Core
{
    using Maticsoft.Payment.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    internal sealed class Globals
    {
        public static List<string> AlipayOtherParamKeys = new List<string>();
        public const string GATEWAY_KEY = "MATICSOFTGW";
        public const string PAYMENT_NOTIFY_URL = "Pay/Payment/Notify_url.aspx?MATICSOFTGW={0}";
        public const string PAYMENT_RETURN_URL = "Pay/Payment/Return_url.aspx?MATICSOFTGW={0}";
        public const string RECHARGE_NOTIFY_URL = "Pay/Recharge/Notify_url.aspx?MATICSOFTGW={0}";
        public const string RECHARGE_RETURN_URL = "Pay/Recharge/Return_url.aspx?MATICSOFTGW={0}";

        public static string FullPath(string local)
        {
            if (string.IsNullOrEmpty(local))
            {
                return local;
            }
            if (local.ToLower(CultureInfo.InvariantCulture).StartsWith("http://"))
            {
                return local;
            }
            if (HttpContext.Current == null)
            {
                return local;
            }
            return FullPath(HostPath(HttpContext.Current.Request.Url), local);
        }

        public static string FullPath(string hostPath, string local)
        {
            if (!hostPath.EndsWith("/") && !local.StartsWith("/"))
            {
                return (hostPath + "/" + local);
            }
            return (hostPath + local);
        }

        public static string GetMd5(Encoding encoding, string value)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(encoding.GetBytes(value));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public static string HostPath(Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }
            string str = (uri.Port == 80) ? string.Empty : (":" + uri.Port.ToString(CultureInfo.InvariantCulture));
            return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[] { uri.Scheme, uri.Host, str });
        }

        public static bool SafeBool(string text, bool defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        public static decimal SafeDecimal(string text, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static int SafeInt(string text, int defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static long SafeLong(string text, long defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static bool IsPaymentTestMode
        {
            get
            {
                return WebConfigHelper.GetConfigBool("PaymentTest");
            }
        }

        public static bool IsRechargeTestMode
        {
            get
            {
                return WebConfigHelper.GetConfigBool("RechargeTest");
            }
        }
    }
}


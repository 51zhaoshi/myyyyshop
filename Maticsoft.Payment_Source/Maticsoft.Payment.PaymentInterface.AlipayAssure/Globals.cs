namespace Maticsoft.Payment.PaymentInterface.AlipayAssure
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    internal sealed class Globals
    {
        private Globals()
        {
        }

        internal static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        internal static string CreatUrl(string gateway, string service, string partner, string sign_type, string out_trade_no, string subject, string body, string payment_type, string total_fee, string show_url, string seller_email, string key, string return_url, string _input_charset, string notify_url, string logistics_type, string logistics_fee, string logistics_payment, string quantity)
        {
            int num;
            string[] strArray = BubbleSort(new string[] { "service=" + service, "partner=" + partner, "subject=" + subject, "body=" + body, "out_trade_no=" + out_trade_no, "price=" + total_fee, "show_url=" + show_url, "payment_type=" + payment_type, "seller_email=" + seller_email, "notify_url=" + notify_url, "_input_charset=" + _input_charset, "return_url=" + return_url, "quantity=" + quantity, "logistics_type=" + logistics_type, "logistics_fee=" + logistics_fee, "logistics_payment=" + logistics_payment });
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < strArray.Length; num++)
            {
                if (num == (strArray.Length - 1))
                {
                    builder.Append(strArray[num]);
                }
                else
                {
                    builder.Append(strArray[num] + "&");
                }
            }
            builder.Append(key);
            string str = GetMD5(builder.ToString(), _input_charset);
            char[] separator = new char[] { '=' };
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(gateway);
            for (num = 0; num < strArray.Length; num++)
            {
                builder2.Append(strArray[num].Split(separator)[0] + "=" + HttpUtility.UrlEncode(strArray[num].Split(separator)[1]) + "&");
            }
            builder2.Append("sign=" + str + "&sign_type=" + sign_type);
            return builder2.ToString();
        }

        internal static string GetMD5(string s, string _input_charset)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }
    }
}


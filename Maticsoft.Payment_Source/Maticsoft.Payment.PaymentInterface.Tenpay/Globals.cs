namespace Maticsoft.Payment.PaymentInterface.Tenpay
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

        internal static string GetMD5(string encypStr, string charset)
        {
            byte[] bytes;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            try
            {
                bytes = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                bytes = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            return BitConverter.ToString(provider.ComputeHash(bytes)).Replace("-", "").ToUpper();
        }

        internal static string UrlEncode(string instr, string charset)
        {
            if ((instr == null) || (instr.Trim() == ""))
            {
                return "";
            }
            try
            {
                return HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));
            }
            catch (Exception)
            {
                return HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
            }
        }
    }
}


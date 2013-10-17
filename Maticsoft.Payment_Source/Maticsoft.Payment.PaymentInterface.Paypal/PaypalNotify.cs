namespace Maticsoft.Payment.PaymentInterface.Paypal
{
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;

    internal class PaypalNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public PaypalNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["mc_gross"]);
        }

        public override string GetOrderId()
        {
            return this.parameters["item_number"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://www.paypal.com/cgi-bin/webscr");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = HttpContext.Current.Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string str = Encoding.ASCII.GetString(bytes) + "&cmd=_notify-validate";
            request.ContentLength = str.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(str);
            writer.Close();
            StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream());
            string str2 = reader.ReadToEnd();
            reader.Close();
            string str3 = this.parameters["payment_status"];
            if ((str2 == "VERIFIED") && str3.Equals("Completed"))
            {
                this.OnPaidToMerchant();
            }
            else
            {
                this.OnNotifyVerifyFaild();
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
        }
    }
}


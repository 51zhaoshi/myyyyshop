namespace Maticsoft.Payment.Core
{
    using Maticsoft.Payment.Model;
    using System;
    using System.Globalization;
    using System.Web;

    public abstract class PaymentRequest
    {
        private const string FormFormat = "<form id=\"payform\" name=\"payform\" action=\"{0}\" method=\"POST\">{1}</form>";
        private const string InputFormat = "<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\">";

        protected PaymentRequest()
        {
        }

        protected virtual string CreateField(string name, string strValue)
        {
            return string.Format(CultureInfo.InvariantCulture, "<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\">", new object[] { name, strValue });
        }

        protected virtual string CreateForm(string content, string action)
        {
            content = content + "<input type=\"submit\" value=\"在线支付\" style=\"display:none;\">";
            return string.Format(CultureInfo.InvariantCulture, "<form id=\"payform\" name=\"payform\" action=\"{0}\" method=\"POST\">{1}</form>", new object[] { action, content });
        }

        public static PaymentRequest Instance(string requestType, PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            if (string.IsNullOrEmpty(requestType))
            {
                return null;
            }
            object[] args = new object[] { payee, gateway, trade };
            return (Activator.CreateInstance(Type.GetType(requestType), args) as PaymentRequest);
        }

        protected virtual void RedirectToGateway(string url)
        {
            HttpContext.Current.Response.Redirect(url, true);
        }

        public abstract void SendRequest();
        protected virtual void SubmitPaymentForm(string formContent)
        {
            string s = formContent + "<script>document.forms['payform'].submit();</script>";
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
    }
}


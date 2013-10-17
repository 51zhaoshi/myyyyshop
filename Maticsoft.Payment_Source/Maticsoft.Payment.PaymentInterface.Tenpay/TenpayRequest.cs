namespace Maticsoft.Payment.PaymentInterface.Tenpay
{
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Globalization;
    using System.Web;

    internal class TenpayRequest : PaymentRequest
    {
        private string attach = "Tenpay";
        private string bargainor_id = "";
        private string cmdno = "1";
        private string date = "";
        private string desc = "";
        private string fee_type = "1";
        private string gatewayUrl = "https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi";
        private string key = "";
        private string return_url = "";
        private string sp_billno = "";
        private string total_fee = "";
        private string transaction_id = "";

        public TenpayRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.key = payee.PrimaryKey;
            this.date = trade.Date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            this.desc = this.UrlEncode(trade.Subject);
            this.bargainor_id = payee.SellerAccount;
            this.transaction_id = this.bargainor_id + this.date + this.UnixStamp();
            this.sp_billno = trade.OrderId;
            this.return_url = gateway.ReturnUrl;
            this.total_fee = Convert.ToInt32((decimal) (trade.TotalMoney * 100M)).ToString(CultureInfo.InvariantCulture);
        }

        private static string getRealIp()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }

        public override void SendRequest()
        {
            string str = getRealIp();
            string str2 = Maticsoft.Payment.PaymentInterface.Tenpay.Globals.GetMD5("cmdno=" + this.cmdno + "&date=" + this.date + "&bargainor_id=" + this.bargainor_id + "&transaction_id=" + this.transaction_id + "&sp_billno=" + this.sp_billno + "&total_fee=" + this.total_fee + "&fee_type=" + this.fee_type + "&return_url=" + this.return_url + "&attach=" + this.attach + "&spbill_create_ip=" + str + "&key=" + this.key, "GB2312");
            string url = this.gatewayUrl + "?cmdno=" + this.cmdno + "&date=" + this.date + "&bank_type=0&desc=" + this.desc + "&purchaser_id=&bargainor_id=" + this.bargainor_id + "&transaction_id=" + this.transaction_id + "&sp_billno=" + this.sp_billno + "&total_fee=" + this.total_fee + "&fee_type=" + this.fee_type + "&return_url=" + Maticsoft.Payment.PaymentInterface.Tenpay.Globals.UrlEncode(this.return_url, "GB2312") + "&attach=" + this.attach + "&spbill_create_ip=" + str + "&sign=" + str2 + "&cs=gb2312";
            this.RedirectToGateway(url);
        }

        private uint UnixStamp()
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1)));
            return Convert.ToUInt32(span.TotalSeconds);
        }

        private string UrlEncode(string instr)
        {
            if ((instr != null) && !(instr.Trim() == ""))
            {
                return instr.Replace("%", "%25").Replace("=", "%3d").Replace("&", "%26").Replace("\"", "%22").Replace("?", "%3f").Replace("'", "%27").Replace(" ", "%20");
            }
            return "";
        }
    }
}


namespace Maticsoft.Payment.PaymentInterface.AlipayAssure
{
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;

    internal class AlipayAssureNotify : NotifyQuery
    {
        private string input_charset = "utf-8";
        private NameValueCollection parameters;

        public AlipayAssureNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        private string CreateUrl(PayeeInfo payee)
        {
            return string.Format(CultureInfo.InvariantCulture, "https://mapi.alipay.com/gateway.do?service=notify_verify&partner={0}&notify_id={1}", new object[] { payee.SellerAccount, this.parameters["notify_id"] });
        }

        public override string GetGatewayOrderId()
        {
            return this.parameters["trade_no"];
        }

        public override decimal GetOrderAmount()
        {
            return decimal.Parse(this.parameters["total_fee"]);
        }

        public override string GetOrderId()
        {
            return this.parameters["out_trade_no"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            bool flag;
            try
            {
                flag = bool.Parse(this.GetResponse(this.CreateUrl(payee), timeout));
            }
            catch
            {
                flag = false;
            }
            this.parameters.Remove("MATICSOFTGW");
            foreach (string str in Maticsoft.Payment.Core.Globals.AlipayOtherParamKeys)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    this.parameters.Remove(str);
                }
            }
            string[] strArray = Maticsoft.Payment.PaymentInterface.AlipayAssure.Globals.BubbleSort(this.parameters.AllKeys);
            string s = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((!string.IsNullOrEmpty(this.parameters[strArray[i]]) && (strArray[i] != "sign")) && (strArray[i] != "sign_type"))
                {
                    if (i == (strArray.Length - 1))
                    {
                        s = s + strArray[i] + "=" + this.parameters[strArray[i]];
                    }
                    else
                    {
                        s = s + strArray[i] + "=" + this.parameters[strArray[i]] + "&";
                    }
                }
            }
            s = s + payee.PrimaryKey;
            if (flag && this.parameters["sign"].Equals(Maticsoft.Payment.PaymentInterface.AlipayAssure.Globals.GetMD5(s, this.input_charset)))
            {
                string str3 = this.parameters["trade_status"];
                if (str3 != null)
                {
                    if (!(str3 == "WAIT_SELLER_SEND_GOODS"))
                    {
                        if (!(str3 == "TRADE_FINISHED"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        this.OnPaidToIntermediary();
                        return;
                    }
                    this.OnPaidToMerchant();
                }
            }
            else
            {
                this.OnNotifyVerifyFaild();
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
            if (context != null)
            {
                context.Response.Clear();
                context.Response.Write(success ? "success" : "fail");
                context.Response.End();
            }
        }
    }
}


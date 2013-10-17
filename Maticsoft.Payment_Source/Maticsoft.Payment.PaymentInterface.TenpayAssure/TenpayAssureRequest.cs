namespace Maticsoft.Payment.PaymentInterface.TenpayAssure
{
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Globalization;
    using System.Text;

    internal class TenpayAssureRequest : PaymentRequest
    {
        private string attach = "TenpayAssure";
        private string chnid = "";
        private string cmdno = "12";
        private string encode_type = "2";
        private string gatewayurl = "https://www.tenpay.com/cgi-bin/med/show_opentrans.cgi";
        private string key = "";
        private string mch_name = "";
        private string mch_price = "";
        private string mch_returl = "";
        private string mch_type = "1";
        private string mch_vno = "";
        private string need_buyerinfo = "2";
        private string seller = "";
        private string show_url = "";
        private string version = "2";

        public TenpayAssureRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            this.chnid = "";
            this.seller = payee.SellerAccount;
            this.mch_price = Convert.ToInt32((decimal) (trade.TotalMoney * 100M)).ToString(CultureInfo.InvariantCulture);
            this.mch_vno = trade.OrderId;
            this.key = payee.PrimaryKey;
            this.mch_returl = gateway.NotifyUrl;
            this.show_url = gateway.ReturnUrl;
            this.mch_name = trade.Subject;
        }

        public override void SendRequest()
        {
            StringBuilder buf = new StringBuilder();
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "attach", this.attach);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "chnid", this.chnid);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "cmdno", this.cmdno);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "encode_type", this.encode_type);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "mch_desc", "");
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "mch_name", this.mch_name);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "mch_price", this.mch_price);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "mch_returl", this.mch_returl);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "mch_type", this.mch_type);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "mch_vno", this.mch_vno);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "need_buyerinfo", this.need_buyerinfo);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "seller", this.seller);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "show_url", this.show_url);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "transport_desc", "");
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "transport_fee", "0");
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "version", this.version);
            Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "key", this.key);
            string str = Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.GetMD5(buf.ToString());
            string url = this.gatewayurl + "?attach=" + this.attach + "&chnid=" + this.chnid + "&cmdno=" + this.cmdno + "&encode_type=" + this.encode_type + "&mch_desc=&mch_name=" + this.mch_name + "&mch_price=" + this.mch_price + "&mch_returl=" + this.mch_returl + "&mch_type=" + this.mch_type + "&mch_vno=" + this.mch_vno + "&need_buyerinfo=" + this.need_buyerinfo + "&seller=" + this.seller + "&show_url=" + this.show_url + "&transport_desc=&transport_fee=0&version=" + this.version + "&sign=" + str;
            this.RedirectToGateway(url);
        }
    }
}


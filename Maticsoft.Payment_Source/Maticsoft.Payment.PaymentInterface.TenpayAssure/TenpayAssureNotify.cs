namespace Maticsoft.Payment.PaymentInterface.TenpayAssure
{
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;
    using System.Web;

    internal class TenpayAssureNotify : NotifyQuery
    {
        private NameValueCollection parameters;

        public TenpayAssureNotify(NameValueCollection parameters)
        {
            this.parameters = parameters;
        }

        public override decimal GetOrderAmount()
        {
            return (decimal.Parse(this.parameters["total_fee"], CultureInfo.InvariantCulture) / 100M);
        }

        public override string GetOrderId()
        {
            return this.parameters["mch_vno"];
        }

        public override void VerifyNotify(int timeout, PayeeInfo payee)
        {
            string parameterValue = this.parameters["version"];
            string str2 = this.parameters["cmdno"];
            string str3 = this.parameters["retcode"];
            string str4 = this.parameters["status"];
            string str5 = this.parameters["seller"];
            string str6 = this.parameters["total_fee"];
            string str7 = this.parameters["trade_price"];
            string str8 = this.parameters["transport_fee"];
            string str9 = this.parameters["buyer_id"];
            string str10 = this.parameters["chnid"];
            string str11 = this.parameters["cft_tid"];
            string str12 = this.parameters["mch_vno"];
            string str13 = this.parameters["attach"];
            string str14 = this.parameters["sign"];
            if (!str3.Equals("0"))
            {
                this.OnNotifyVerifyFaild();
            }
            else
            {
                StringBuilder buf = new StringBuilder();
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "attach", str13);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "buyer_id", str9);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "cft_tid", str11);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "chnid", str10);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "cmdno", str2);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "mch_vno", str12);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "retcode", str3);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "seller", str5);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "status", str4);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "total_fee", str6);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "trade_price", str7);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "transport_fee", str8);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "version", parameterValue);
                Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.AddParameter(buf, "key", payee.PrimaryKey);
                if (!str14.Equals(Maticsoft.Payment.PaymentInterface.TenpayAssure.Globals.GetMD5(buf.ToString())))
                {
                    this.OnNotifyVerifyFaild();
                }
                else
                {
                    string str15 = str4;
                    if (str15 != null)
                    {
                        if (!(str15 == "3"))
                        {
                            if (!(str15 == "5"))
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
            }
        }

        public override void WriteBack(HttpContext context, bool success)
        {
        }
    }
}


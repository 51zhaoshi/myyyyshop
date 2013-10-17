namespace Maticsoft.Payment.Web
{
    using Maticsoft.Payment;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Configuration;
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    public abstract class PaymentReturnTemplatedPage<T> : Page where T: class, IOrderInfo
    {
        protected decimal Amount;
        protected string GatewayName;
        private bool isBackRequest;
        protected NotifyQuery Notify;
        protected IPaymentOption<T> Option;
        protected T Order;
        protected string OrderId;

        protected PaymentReturnTemplatedPage(IPaymentOption<T> option, bool _isBackRequest)
        {
            this.Option = option;
            this.isBackRequest = _isBackRequest;
        }

        protected override void CreateChildControls()
        {
            this.DoValidate();
        }

        protected virtual void DisplayMessage(string status)
        {
        }

        protected virtual void DoValidate()
        {
            PayConfiguration config = PayConfiguration.GetConfig();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add(this.Page.Request.Form);
            parameters.Add(this.Page.Request.QueryString);
            string str = this.Page.Request.Params["MATICSOFTGW"];
            if (string.IsNullOrEmpty(str))
            {
                this.ResponseStatus(false, "gatewaynotfound");
            }
            else
            {
                this.GatewayName = str.ToLower();
                GatewayProvider provider = config.Providers[this.GatewayName] as GatewayProvider;
                if (provider == null)
                {
                    this.ResponseStatus(false, "gatewaynotfound");
                }
                else
                {
                    this.Notify = NotifyQuery.Instance(provider.NotifyType, parameters);
                    if (this.isBackRequest)
                    {
                        this.Notify.ReturnUrl = Globals.FullPath(string.Format(this.Option.ReturnUrl, this.GatewayName));
                    }
                    this.Amount = this.Notify.GetOrderAmount();
                    this.OrderId = this.Notify.GetOrderId();
                    this.Order = this.GetOrderInfo(this.OrderId);
                    if (this.Order == null)
                    {
                        this.ResponseStatus(false, "ordernotfound");
                    }
                    else if (this.Order.PaymentStatus == PaymentStatus.Prepaid)
                    {
                        this.ResponseStatus(true, "success");
                    }
                    else
                    {
                        this.Order.GatewayOrderId = this.Notify.GetGatewayOrderId();
                        PaymentModeInfo paymentMode = this.GetPaymentMode(this.Order.PaymentTypeId);
                        if (paymentMode == null)
                        {
                            this.ResponseStatus(false, "gatewaynotfound");
                        }
                        else if (Globals.IsPaymentTestMode)
                        {
                            string str2 = this.Page.Request.Params["sign"];
                            if (string.IsNullOrWhiteSpace(str2))
                            {
                                this.ResponseStatus(false, "<TestMode> no sign");
                            }
                            StringBuilder builder = new StringBuilder(Globals.FullPath(string.Format(this.Option.ReturnUrl, this.GatewayName)));
                            builder.AppendFormat("&out_trade_no={0}", this.OrderId);
                            builder.AppendFormat("&total_fee={0}", this.Amount);
                            if (str2 != Globals.GetMd5(Encoding.UTF8, builder.ToString()))
                            {
                                this.ResponseStatus(false, "<TestMode> Unauthorized sign");
                            }
                            this.PaidToSite();
                        }
                        else
                        {
                            PayeeInfo info3 = new PayeeInfo {
                                EmailAddress = paymentMode.EmailAddress,
                                Partner = paymentMode.Partner,
                                Password = paymentMode.Password,
                                PrimaryKey = paymentMode.SecretKey,
                                SecondKey = paymentMode.SecondKey,
                                SellerAccount = paymentMode.MerchantCode
                            };
                            PayeeInfo payee = info3;
                            this.Notify.NotifyVerifyFaild += new NotifyEventHandler(this.notify_NotifyVerifyFaild);
                            this.Notify.PaidToIntermediary += new NotifyEventHandler(this.notify_PaidToIntermediary);
                            this.Notify.PaidToMerchant += new NotifyEventHandler(this.notify_PaidToMerchant);
                            this.Notify.VerifyNotify(0x7530, payee);
                        }
                    }
                }
            }
        }

        protected abstract T GetOrderInfo(string orderId);
        protected virtual PaymentModeInfo GetPaymentMode(int paymentTypeId)
        {
            return PaymentModeManage.GetPaymentModeById(paymentTypeId);
        }

        private void notify_NotifyVerifyFaild(NotifyQuery sender)
        {
            this.ResponseStatus(false, "verifyfaild");
        }

        private void notify_PaidToIntermediary(NotifyQuery sender)
        {
            this.PaidToSite();
        }

        private void notify_PaidToMerchant(NotifyQuery sender)
        {
            this.PaidToSite();
        }

        private void PaidToSite()
        {
            if (this.Order.PaymentStatus == PaymentStatus.Prepaid)
            {
                this.ResponseStatus(true, "success");
            }
            else if (OrderProcessor.CheckAction<T>(this.Order, OrderActions.BUYER_PAY) && this.PayForOrder(this.Order))
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                this.ResponseStatus(false, "fail");
            }
        }

        protected abstract bool PayForOrder(T order);
        private void ResponseStatus(bool success, string status)
        {
            if (this.isBackRequest || !success)
            {
                this.Controls.Clear();
            }
            if (this.isBackRequest)
            {
                this.Notify.WriteBack(HttpContext.Current, success);
            }
            else
            {
                this.DisplayMessage(status);
            }
        }
    }
}


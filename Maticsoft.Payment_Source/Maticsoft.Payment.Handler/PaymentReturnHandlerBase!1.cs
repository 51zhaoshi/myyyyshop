namespace Maticsoft.Payment.Handler
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
    using System.Web.SessionState;

    public abstract class PaymentReturnHandlerBase<T> : IHttpHandler, IRequiresSessionState where T: class, IOrderInfo
    {
        private decimal _amount;
        private readonly bool _isBackRequest;
        private string _orderId;
        protected string GatewayName;
        protected NotifyQuery Notify;
        protected IPaymentOption<T> Option;
        protected T Order;

        protected PaymentReturnHandlerBase(IPaymentOption<T> option, bool isBackRequest)
        {
            this.Option = option;
            this._isBackRequest = isBackRequest;
        }

        protected virtual void DisplayMessage(string status)
        {
        }

        protected void DoValidate()
        {
            PayConfiguration config = PayConfiguration.GetConfig();
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add(HttpContext.Current.Request.Form);
            parameters.Add(HttpContext.Current.Request.QueryString);
            string str = HttpContext.Current.Request.Params["MATICSOFTGW"];
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
                    if (this._isBackRequest)
                    {
                        this.Notify.ReturnUrl = Globals.FullPath(string.Format(this.Option.ReturnUrl, this.GatewayName));
                    }
                    if (Globals.IsPaymentTestMode)
                    {
                        this._amount = Globals.SafeDecimal(parameters["total_fee"], -1M);
                        this._orderId = parameters["out_trade_no"];
                    }
                    else
                    {
                        this._amount = this.Notify.GetOrderAmount();
                        this._orderId = this.Notify.GetOrderId();
                    }
                    this.Order = this.Option.GetOrderInfo(this._orderId);
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
                            string str2 = HttpContext.Current.Request.QueryString["sign"];
                            if (string.IsNullOrWhiteSpace(str2))
                            {
                                this.ResponseStatus(false, "<TestMode> no sign");
                            }
                            StringBuilder builder = new StringBuilder(Globals.FullPath(string.Format(this.Option.ReturnUrl, this.GatewayName)));
                            builder.AppendFormat("&out_trade_no={0}", this._orderId);
                            builder.AppendFormat("&total_fee={0}", this._amount);
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

        protected virtual void ExecuteResult(bool success, string status)
        {
        }

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
            else if (OrderProcessor.CheckAction<T>(this.Order, OrderActions.BUYER_PAY) && this.Option.PayForOrder(this.Order))
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                this.ResponseStatus(false, "fail");
            }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            this.DoValidate();
        }

        private void ResponseStatus(bool success, string status)
        {
            this.ExecuteResult(success, status);
            if (this._isBackRequest || !success)
            {
                HttpContext.Current.Response.Clear();
            }
            if (this._isBackRequest)
            {
                this.Notify.WriteBack(HttpContext.Current, success);
            }
            else
            {
                this.DisplayMessage(status);
            }
        }

        public decimal Amount
        {
            get
            {
                return this._amount;
            }
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected string OrderId
        {
            get
            {
                return this._orderId;
            }
        }
    }
}


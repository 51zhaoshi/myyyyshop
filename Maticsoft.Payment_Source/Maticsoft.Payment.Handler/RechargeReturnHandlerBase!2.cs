namespace Maticsoft.Payment.Handler
{
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Configuration;
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;

    public abstract class RechargeReturnHandlerBase<T, U> : IHttpHandler, IRequiresSessionState where T: class, IRechargeRequest, new() where U: class, IUserInfo, new()
    {
        private decimal _amount;
        private long _rechargeId;
        protected string GatewayName;
        private bool isBackRequest;
        protected NotifyQuery Notify;
        protected IRechargeOption<T, U> Option;
        protected PaymentModeInfo paymode;
        protected T RechargeRequest;

        protected RechargeReturnHandlerBase(IRechargeOption<T, U> option, bool _isBackRequest)
        {
            this.Option = option;
            this.isBackRequest = _isBackRequest;
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
                    if (this.isBackRequest)
                    {
                        this.Notify.ReturnUrl = Globals.FullPath(string.Format(this.Option.ReturnUrl, this.GatewayName));
                    }
                    if (Globals.IsRechargeTestMode)
                    {
                        this._amount = Globals.SafeDecimal(parameters["total_fee"], -1M);
                        this.RechargeId = Globals.SafeLong(parameters["out_trade_no"], -1L);
                    }
                    else
                    {
                        this.RechargeId = Globals.SafeLong(this.Notify.GetOrderId(), -1L);
                        this.Amount = this.Notify.GetOrderAmount();
                    }
                    this.RechargeRequest = this.Option.GetRechargeRequest(this.RechargeId);
                    if (this.RechargeRequest == null)
                    {
                        this.ResponseStatus(true, "success");
                    }
                    else
                    {
                        this.Amount = this.RechargeRequest.RechargeBlance;
                        this.paymode = PaymentModeManage.GetPaymentModeByName(this.RechargeRequest.PaymentGateway);
                        if (this.paymode == null)
                        {
                            this.ResponseStatus(false, "gatewaynotfound");
                        }
                        else if (Globals.IsRechargeTestMode)
                        {
                            string str2 = HttpContext.Current.Request.QueryString["sign"];
                            if (string.IsNullOrWhiteSpace(str2))
                            {
                                this.ResponseStatus(false, "<TestMode> no sign");
                            }
                            StringBuilder builder = new StringBuilder(Globals.FullPath(string.Format(this.Option.ReturnUrl, this.GatewayName)));
                            builder.AppendFormat("&out_trade_no={0}", this._rechargeId);
                            builder.AppendFormat("&total_fee={0}", this._amount);
                            if (str2 != Globals.GetMd5(Encoding.UTF8, builder.ToString()))
                            {
                                this.ResponseStatus(false, "<TestMode> Unauthorized sign");
                            }
                            this.PaidToSite();
                        }
                        else
                        {
                            PayeeInfo info2 = new PayeeInfo {
                                EmailAddress = this.paymode.EmailAddress,
                                Partner = this.paymode.Partner,
                                Password = this.paymode.Password,
                                PrimaryKey = this.paymode.SecretKey,
                                SecondKey = this.paymode.SecondKey,
                                SellerAccount = this.paymode.MerchantCode
                            };
                            PayeeInfo payee = info2;
                            this.Notify.PaidToIntermediary += new NotifyEventHandler(this.notify_PaidToIntermediary);
                            this.Notify.PaidToMerchant += new NotifyEventHandler(this.notify_PaidToMerchant);
                            this.Notify.NotifyVerifyFaild += new NotifyEventHandler(this.notify_NotifyVerifyFaild);
                            this.Notify.VerifyNotify(0x7530, payee);
                        }
                    }
                }
            }
        }

        protected virtual void ExecuteResult(bool success, string status)
        {
        }

        private void notify_NotifyVerifyFaild(NotifyQuery sender)
        {
            this.ResponseStatus(false, "verifyfaild");
        }

        private void notify_PaidToIntermediary(NotifyQuery sender)
        {
            this.ResponseStatus(false, "waitconfirm");
        }

        private void notify_PaidToMerchant(NotifyQuery sender)
        {
            this.PaidToSite();
        }

        private void PaidToSite()
        {
            if (this.Option.PayForRechargeRequest(this.RechargeRequest))
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
            if (this.isBackRequest || !success)
            {
                HttpContext.Current.Response.Clear();
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

        public decimal Amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public long RechargeId
        {
            get
            {
                return this._rechargeId;
            }
            set
            {
                this._rechargeId = value;
            }
        }
    }
}


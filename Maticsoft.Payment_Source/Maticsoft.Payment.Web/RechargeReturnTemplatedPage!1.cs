namespace Maticsoft.Payment.Web
{
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Configuration;
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;

    [Obsolete]
    public abstract class RechargeReturnTemplatedPage<T> : Page where T: class, IRechargeRequest, new()
    {
        protected decimal Amount;
        protected string GatewayName;
        private bool isBackRequest;
        protected NotifyQuery Notify;
        protected PaymentModeInfo paymode;
        protected int RechargeId;
        protected T RechargeRequest;

        protected RechargeReturnTemplatedPage(bool _isBackRequest)
        {
            this.isBackRequest = _isBackRequest;
        }

        protected override void CreateChildControls()
        {
            this.DoValidate();
        }

        protected virtual void DisplayMessage(string status)
        {
        }

        protected void DoValidate()
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
                        this.Notify.ReturnUrl = Globals.FullPath(string.Format("Pay/Payment/Return_url.aspx?MATICSOFTGW={0}", this.GatewayName));
                    }
                    this.RechargeId = int.Parse(this.Notify.GetOrderId(), CultureInfo.InvariantCulture);
                    this.Amount = this.Notify.GetOrderAmount();
                    this.RechargeRequest = this.GetRechargeRequest((long) this.RechargeId);
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

        protected abstract T GetRechargeRequest(long rechargeId);
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
            if (this.PayForRechargeRequest(this.RechargeRequest))
            {
                this.ResponseStatus(true, "success");
            }
            else
            {
                this.ResponseStatus(false, "fail");
            }
        }

        protected abstract bool PayForRechargeRequest(T rechargeRequest);
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


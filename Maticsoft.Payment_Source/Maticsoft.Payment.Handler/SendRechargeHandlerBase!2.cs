namespace Maticsoft.Payment.Handler
{
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Configuration;
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;

    public abstract class SendRechargeHandlerBase<T, U> : IHttpHandler, IRequiresSessionState where T: class, IRechargeRequest, new() where U: class, IUserInfo, new()
    {
        protected string HostName;
        protected IRechargeOption<T, U> Option;

        protected SendRechargeHandlerBase(IRechargeOption<T, U> option)
        {
            this.HostName = "Maticsoft ";
            this.Option = option;
        }

        protected virtual GatewayInfo GetGateway(string gatewayName)
        {
            return new GatewayInfo { ReturnUrl = Globals.FullPath(string.Format(this.Option.ReturnUrl, gatewayName)), NotifyUrl = Globals.FullPath(string.Format(this.Option.NotifyUrl, gatewayName)) };
        }

        protected virtual PayeeInfo GetPayee(PaymentModeInfo paymode)
        {
            if (paymode == null)
            {
                return null;
            }
            return new PayeeInfo { EmailAddress = paymode.EmailAddress, Partner = paymode.Partner, Password = paymode.Password, PrimaryKey = paymode.SecretKey, SecondKey = paymode.SecondKey, SellerAccount = paymode.MerchantCode };
        }

        protected virtual TradeInfo GetTrade(T rechargeRequest, decimal payCharge, U user)
        {
            decimal num = rechargeRequest.RechargeBlance + payCharge;
            string str = rechargeRequest.RechargeId.ToString(CultureInfo.InvariantCulture);
            return new TradeInfo { BuyerEmailAddress = user.Email, Date = rechargeRequest.TradeDate, OrderId = str, Showurl = Globals.HostPath(HttpContext.Current.Request.Url), Subject = this.HostName + "在线充值: " + str, Body = this.HostName + "在线充值: " + str + " 金额: " + num.ToString(CultureInfo.InvariantCulture), TotalMoney = num };
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            long rechargeId = Globals.SafeLong(context.Request.QueryString["RechargeId"], 0L);
            if (rechargeId != 0L)
            {
                T rechargeRequest = this.Option.GetRechargeRequest(rechargeId);
                if ((rechargeRequest != null) && (rechargeRequest.PaymentTypeId >= 1))
                {
                    PaymentModeInfo paymentModeById = PaymentModeManage.GetPaymentModeById(rechargeRequest.PaymentTypeId);
                    if (paymentModeById != null)
                    {
                        U currentUser = this.Option.GetCurrentUser(context);
                        if (currentUser != null)
                        {
                            PayConfiguration config = PayConfiguration.GetConfig();
                            if (config != null)
                            {
                                GatewayProvider provider = config.Providers[paymentModeById.Gateway.ToLower()] as GatewayProvider;
                                if (provider != null)
                                {
                                    decimal payCharge = paymentModeById.CalcPayCharge(rechargeRequest.RechargeBlance);
                                    GatewayInfo gateway = this.GetGateway(paymentModeById.Gateway.ToLower());
                                    TradeInfo trade = this.GetTrade(rechargeRequest, payCharge, currentUser);
                                    if (Globals.IsRechargeTestMode)
                                    {
                                        StringBuilder builder = new StringBuilder(gateway.ReturnUrl);
                                        builder.AppendFormat("&out_trade_no={0}", trade.OrderId);
                                        builder.AppendFormat("&total_fee={0}", trade.TotalMoney);
                                        builder.AppendFormat("&sign={0}", Globals.GetMd5(Encoding.UTF8, builder.ToString()));
                                        HttpContext.Current.Response.Redirect(gateway.ReturnUrl.Contains("?") ? builder.ToString() : builder.ToString().Replace("&out_trade_no", "?out_trade_no"), true);
                                    }
                                    else
                                    {
                                        PaymentRequest.Instance(provider.RequestType, this.GetPayee(paymentModeById), gateway, trade).SendRequest();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}


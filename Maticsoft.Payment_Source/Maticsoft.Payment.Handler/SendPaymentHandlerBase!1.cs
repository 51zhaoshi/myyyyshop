namespace Maticsoft.Payment.Handler
{
    using Maticsoft.Payment;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Configuration;
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.Model;
    using System;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;

    public abstract class SendPaymentHandlerBase<T> : IHttpHandler, IRequiresSessionState where T: class, IOrderInfo, new()
    {
        protected string HostName;
        protected IPaymentOption<T> Option;

        protected SendPaymentHandlerBase(IPaymentOption<T> option)
        {
            this.HostName = "Maticsoft ";
            this.Option = option;
        }

        protected virtual GatewayInfo GetGateway(string gatewayName)
        {
            return new GatewayInfo { ReturnUrl = Globals.FullPath(string.Format(this.Option.ReturnUrl, gatewayName)), NotifyUrl = Globals.FullPath(string.Format(this.Option.NotifyUrl, gatewayName)) };
        }

        protected virtual decimal GetOrderTotalMoney(string[] orderIds, T orderInfo)
        {
            return orderInfo.Amount;
        }

        protected virtual PayeeInfo GetPayee(PaymentModeInfo paymode)
        {
            if (paymode == null)
            {
                return null;
            }
            return new PayeeInfo { EmailAddress = paymode.EmailAddress, Partner = paymode.Partner, Password = paymode.Password, PrimaryKey = paymode.SecretKey, SecondKey = paymode.SecondKey, SellerAccount = paymode.MerchantCode };
        }

        protected virtual PaymentModeInfo GetPaymentMode(int paymentTypeId)
        {
            return PaymentModeManage.GetPaymentModeById(paymentTypeId);
        }

        protected virtual TradeInfo GetTrade(string orderIdStr, decimal totalMoney, T order)
        {
            return new TradeInfo { Body = this.HostName + "订单 - 订单号: [" + orderIdStr + "]", BuyerEmailAddress = order.BuyerEmail, Date = order.OrderDate, OrderId = orderIdStr, Showurl = Globals.HostPath(HttpContext.Current.Request.Url), Subject = this.HostName + "订单 - 订单号: [" + orderIdStr + "] - 在线支付 - 订单支付金额: " + totalMoney.ToString("0.00") + " 元", TotalMoney = totalMoney };
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            if (this.VerifySendPayment(context))
            {
                string orderIdStr = string.Empty;
                string[] orderIds = OrderProcessor.GetQueryString4OrderIds(context.Request, out orderIdStr);
                if ((orderIds == null) || (orderIds.Length < 1))
                {
                    HttpContext.Current.Response.Redirect("~/");
                }
                else
                {
                    T orderInfo = this.Option.GetOrderInfo(orderIds[0]);
                    if (orderInfo != null)
                    {
                        decimal orderTotalMoney = this.GetOrderTotalMoney(orderIds, orderInfo);
                        if (orderTotalMoney >= 0M)
                        {
                            if (orderInfo.PaymentStatus != PaymentStatus.NotYet)
                            {
                                context.Response.Write(HttpContext.GetGlobalResourceObject("Resources", "IDS_ErrorMessage_SentPayment").ToString());
                            }
                            else
                            {
                                PaymentModeInfo paymentModeById = PaymentModeManage.GetPaymentModeById(orderInfo.PaymentTypeId);
                                if ((paymentModeById == null) || string.IsNullOrWhiteSpace(paymentModeById.Gateway))
                                {
                                    context.Response.Write(HttpContext.GetGlobalResourceObject("Resources", "IDS_ErrorMessage_NoPayment").ToString());
                                }
                                else
                                {
                                    GatewayProvider provider = PayConfiguration.GetConfig().Providers[paymentModeById.Gateway.ToLower()] as GatewayProvider;
                                    if (provider != null)
                                    {
                                        GatewayInfo gateway = this.GetGateway(paymentModeById.Gateway.ToLower());
                                        TradeInfo trade = this.GetTrade(orderIdStr, orderTotalMoney, orderInfo);
                                        if (Globals.IsPaymentTestMode)
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
        }

        protected virtual bool VerifySendPayment(HttpContext context)
        {
            return true;
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


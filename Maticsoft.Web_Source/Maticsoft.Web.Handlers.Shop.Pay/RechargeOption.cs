namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Pay;
    using Maticsoft.Common;
    using Maticsoft.Model.Pay;
    using Maticsoft.Payment.Model;
    using System;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Security;

    public class RechargeOption : IRechargeOption, IRechargeOption<RechargeRequestInfo, UserInfo>
    {
        private const string _notifyUrl = "/pay/recharge/{0}/notify_url.aspx";
        private const string _returnUrl = "/pay/recharge/{0}/return_url.aspx";

        public UserInfo GetCurrentUser(HttpContext context)
        {
            User user;
            AccountsPrincipal principal;
            if (!context.User.Identity.IsAuthenticated)
            {
                return null;
            }
            try
            {
                principal = new AccountsPrincipal(context.User.Identity.Name);
            }
            catch (IdentityNotMappedException)
            {
                FormsAuthentication.SignOut();
                context.Session.Remove(Globals.SESSIONKEY_USER);
                context.Session.Clear();
                context.Session.Abandon();
                return null;
            }
            if (context.Session[Globals.SESSIONKEY_USER] == null)
            {
                user = new User(principal);
                context.Session[Globals.SESSIONKEY_USER] = user;
            }
            else
            {
                user = (User) context.Session[Globals.SESSIONKEY_USER];
            }
            return new UserInfo { UserId = user.UserID, Email = user.Email };
        }

        public RechargeRequestInfo GetRechargeRequest(long rechargeId)
        {
            Maticsoft.Model.Pay.RechargeRequest model = new Maticsoft.BLL.Pay.RechargeRequest().GetModel(rechargeId);
            RechargeRequestInfo info = new RechargeRequestInfo();
            if (model != null)
            {
                info.PaymentGateway = model.PaymentGateway;
                info.PaymentTypeId = model.PaymentTypeId;
                info.RechargeBlance = model.RechargeBlance;
                info.RechargeId = model.RechargeId;
                info.TradeDate = model.TradeDate;
                info.UserId = model.UserId;
            }
            return info;
        }

        public bool PayForRechargeRequest(RechargeRequestInfo rechargeRequest)
        {
            Maticsoft.BLL.Pay.RechargeRequest request = new Maticsoft.BLL.Pay.RechargeRequest();
            Maticsoft.Model.Pay.RechargeRequest reModel = new Maticsoft.Model.Pay.RechargeRequest();
            if (rechargeRequest != null)
            {
                reModel.PaymentGateway = rechargeRequest.PaymentGateway;
                reModel.PaymentTypeId = rechargeRequest.PaymentTypeId;
                reModel.RechargeBlance = rechargeRequest.RechargeBlance;
                reModel.RechargeId = rechargeRequest.RechargeId;
                reModel.TradeDate = rechargeRequest.TradeDate;
                reModel.UserId = rechargeRequest.UserId;
            }
            reModel.Status = 1;
            return request.UpdateStatus(reModel);
        }

        public string NotifyUrl
        {
            get
            {
                return "/pay/recharge/{0}/notify_url.aspx";
            }
        }

        public string ReturnUrl
        {
            get
            {
                return "/pay/recharge/{0}/return_url.aspx";
            }
        }
    }
}


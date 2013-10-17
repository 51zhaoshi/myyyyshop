namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.Components;
    using Maticsoft.Payment.Handler;
    using Maticsoft.Web;
    using System;
    using System.Web;

    public class RechargeReturnHandler : RechargeReturnHandlerBase<RechargeRequestInfo, UserInfo>
    {
        public const string KEY_RECHARGEID = "RechargeReturn_RechargeId";
        public const string KEY_STATUS = "RechargeReturn_Status";

        public RechargeReturnHandler() : base(new RechargeOption(), false)
        {
        }

        protected override void DisplayMessage(string status)
        {
            string currentRoutePath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(AreaRoute.Shop);
            if (!string.IsNullOrWhiteSpace(base.RechargeId.ToString()))
            {
                HttpContext.Current.Session["RechargeReturn_RechargeId"] = base.RechargeId.ToString();
            }
            HttpContext.Current.Session["RechargeReturn_Status"] = status;
            switch (status)
            {
                case "success":
                    HttpContext.Current.Response.Redirect(currentRoutePath + "PayResult/RechargeSuccess");
                    return;
            }
            HttpContext.Current.Response.Redirect(currentRoutePath + "PayResult/RechargeFail");
        }
    }
}


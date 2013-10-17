namespace Maticsoft.Web.Handlers.Shop.Pay
{
    using Maticsoft.Components;
    using Maticsoft.Payment.Handler;
    using Maticsoft.Web;
    using System;
    using System.Web;

    public class PaymentReturnHandler : PaymentReturnHandlerBase<OrderInfo>
    {
        public const string KEY_ORDERID = "PaymentReturn_OrderId";
        public const string KEY_STATUS = "PaymentReturn_Status";

        public PaymentReturnHandler() : base(new PaymentOption(), false)
        {
        }

        protected override void DisplayMessage(string status)
        {
            string currentRoutePath = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(AreaRoute.Shop);
            if (!string.IsNullOrWhiteSpace(base.OrderId))
            {
                HttpContext.Current.Session["PaymentReturn_OrderId"] = base.OrderId;
            }
            HttpContext.Current.Session["PaymentReturn_Status"] = status;
            switch (status)
            {
                case "success":
                    HttpContext.Current.Response.Redirect(currentRoutePath + "PayResult/Success");
                    return;
            }
            HttpContext.Current.Response.Redirect(currentRoutePath + "PayResult/Fail");
        }
    }
}


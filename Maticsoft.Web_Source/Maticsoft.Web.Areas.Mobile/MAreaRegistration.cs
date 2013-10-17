namespace Maticsoft.Web.Areas.Mobile
{
    using Maticsoft.Components;
    using System;
    using System.Web.Mvc;

    public class MAreaRegistration : MobileAreaRegistration
    {
        public MAreaRegistration()
        {
            base.RouteName = "m";
            base.CurrentRouteName = string.Format("{0}_{1}_Default", this.AreaName, base.RouteName);
            base.CurrentRoutePath = base.RouteName + "/";
            base.IsRegisterMArea = MvcApplication.MainAreaRoute != base.CurrentArea;
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (base.IsRegisterMArea)
            {
                base.RegisterArea(context);
            }
        }
    }
}


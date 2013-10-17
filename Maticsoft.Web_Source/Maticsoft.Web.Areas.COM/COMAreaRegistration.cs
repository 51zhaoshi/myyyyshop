namespace Maticsoft.Web.Areas.COM
{
    using Maticsoft.Web.Areas;
    using System;
    using System.Web.Mvc;

    public class COMAreaRegistration : AreaRegistrationBase
    {
        public COMAreaRegistration() : base(AreaRoute.COM)
        {
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            base.RegisterArea(context);
        }
    }
}


namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Components;
    using Maticsoft.Web.Controllers;
    using System;
    using System.Web.Mvc;

    [SNSError]
    public class SNSControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
        private readonly TaoBaoConfig _taoBaoConfig = new TaoBaoConfig(ApplicationKeyType.OpenAPI);
        public int CommentDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS), 5);
        public int FallDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_FallDataSize", ApplicationKeyType.SNS), 20);
        public int FallInitDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS), 5);
        public int PostDataSize = Globals.SafeInt(ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS), 15);

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (MvcApplication.IsInstall && !filterContext.IsChildAction)
            {
                ((dynamic) base.ViewBag).TaoBaoAppkey = this._taoBaoConfig.TaoBaoAppkey;
            }
            base.OnResultExecuting(filterContext);
        }
    }
}


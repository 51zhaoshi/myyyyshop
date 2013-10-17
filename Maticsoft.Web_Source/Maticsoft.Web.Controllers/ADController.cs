namespace Maticsoft.Web.Controllers
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Web.Mvc;

    public class ADController : Maticsoft.Web.Controllers.ControllerBase
    {
        public ActionResult Index(int AdvPositionId)
        {
            Maticsoft.Model.Settings.AdvertisePosition modelByCache = new Maticsoft.BLL.Settings.AdvertisePosition().GetModelByCache(AdvPositionId);
            if ((modelByCache != null) && modelByCache.ShowType.HasValue)
            {
                return base.View();
            }
            return base.Content("AdvPositionId Not Find!");
        }
    }
}


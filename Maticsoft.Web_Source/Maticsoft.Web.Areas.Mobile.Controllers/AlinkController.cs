namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class AlinkController : MobileControllerBase
    {
        public ActionResult AlinkDetail(int aid = -1, string viewName = "AlinkDetail")
        {
            Maticsoft.Model.Settings.FriendlyLink modelByCache = new Maticsoft.BLL.Settings.FriendlyLink().GetModelByCache(aid);
            return base.View(viewName, modelByCache);
        }

        public ActionResult Alinks(int top, string viewName = "Alinks")
        {
            List<Maticsoft.Model.Settings.FriendlyLink> modelList = new Maticsoft.BLL.Settings.FriendlyLink().GetModelList(top, 0);
            return base.View(viewName, modelList);
        }
    }
}


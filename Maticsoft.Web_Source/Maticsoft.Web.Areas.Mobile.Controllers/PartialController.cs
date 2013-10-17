namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class PartialController : MobileControllerBase
    {
        public PartialViewResult AD(int AdvPositionId, string viewName = "_AD")
        {
            Maticsoft.Model.Settings.Advertisement modelByAdvPositionId = new Maticsoft.BLL.Settings.Advertisement().GetModelByAdvPositionId(AdvPositionId);
            return this.PartialView(viewName, modelByAdvPositionId);
        }

        public PartialViewResult AdDetail(int id, string ViewName = "_IndexAd")
        {
            List<Maticsoft.Model.Settings.Advertisement> listByAidCache = new Maticsoft.BLL.Settings.Advertisement().GetListByAidCache(id);
            return this.PartialView(ViewName, listByAidCache);
        }

        public PartialViewResult Footer(string viewName = "_Footer")
        {
            if (base.currentUser != null)
            {
                ((dynamic) base.ViewBag).usernickname = base.currentUser.NickName;
            }
            return base.PartialView(viewName);
        }

        public PartialViewResult FooterNav(string viewName = "_FooterNav")
        {
            return base.PartialView(viewName);
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public PartialViewResult NavBar(string viewName = "_NavBar")
        {
            ((dynamic) base.ViewBag).tel = ConfigSystem.GetValueByCache("CompanyTelephone");
            return base.PartialView(viewName);
        }

        public PartialViewResult Navigation(string viewName = "_Navigation")
        {
            return base.PartialView(viewName);
        }
    }
}


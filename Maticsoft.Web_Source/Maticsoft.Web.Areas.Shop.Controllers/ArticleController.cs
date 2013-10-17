namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.CMS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class ArticleController : ShopControllerBase
    {
        private Maticsoft.BLL.CMS.Content contentBll = new Maticsoft.BLL.CMS.Content();
        private Maticsoft.BLL.CMS.ContentClass contentclassBll = new Maticsoft.BLL.CMS.ContentClass();

        public PartialViewResult ContentTitleList(int classid, string viewName = "_ContentTitleList")
        {
            List<Maticsoft.Model.CMS.Content> modelList = this.contentBll.GetModelList(classid, 0);
            return this.PartialView(viewName, modelList);
        }

        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                int contentID = id.Value;
                Maticsoft.Model.CMS.Content modelByCache = this.contentBll.GetModelByCache(contentID);
                if (modelByCache != null)
                {
                    IPageSetting setting = PageSetting.GetArticleSetting(modelByCache, "CMS", ApplicationKeyType.CMS);
                    ((dynamic) base.ViewBag).Title = setting.Title;
                    ((dynamic) base.ViewBag).Keywords = setting.Keywords;
                    ((dynamic) base.ViewBag).Description = setting.Description;
                    this.contentBll.UpdatePV(contentID);
                    ((dynamic) base.ViewBag).AClassName = this.contentclassBll.GetAClassnameById(modelByCache.ClassID);
                    return base.View(modelByCache);
                }
            }
            return this.Redirect("/Home");
        }

        public PartialViewResult LeftMenu(int classid, string viewName = "_LeftMenu")
        {
            Maticsoft.Model.CMS.ContentClass class2;
            List<Maticsoft.Model.CMS.ContentClass> modelList = this.contentclassBll.GetModelList(classid, out class2);
            if (class2 != null)
            {
                ((dynamic) base.ViewBag).AclassName = class2.ClassName;
                ((dynamic) base.ViewBag).AclassId = class2.ClassID;
            }
            return this.PartialView(viewName, modelList);
        }
    }
}


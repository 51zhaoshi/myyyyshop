namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Settings;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.CMS;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class PartialController : CMSControllerBase
    {
        private Maticsoft.BLL.Settings.Advertisement bllAdvertisement = new Maticsoft.BLL.Settings.Advertisement();
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);

        public ActionResult AD(int AdvPositionId)
        {
            Maticsoft.Model.Settings.Advertisement modelByAdvPositionId = this.bllAdvertisement.GetModelByAdvPositionId(AdvPositionId);
            return base.View(modelByAdvPositionId);
        }

        public ActionResult ADDefindCode(int AdvPositionId)
        {
            return base.Content(this.bllAdvertisement.GetDefindCode(AdvPositionId));
        }

        public ActionResult ADRotator(int AdvPositionId)
        {
            List<Maticsoft.Model.Settings.Advertisement> modelList = this.bllAdvertisement.GetModelList(AdvPositionId);
            this.bllAdvertisement.GetDefindCode(AdvPositionId);
            return base.View(modelList);
        }

        public ActionResult Comments(int top)
        {
            List<Maticsoft.Model.CMS.Comment> model = new Maticsoft.BLL.CMS.Comment().GetModelList(top, "", " ID desc");
            if ((model != null) && (model.Count > 0))
            {
                foreach (Maticsoft.Model.CMS.Comment comment2 in model)
                {
                    comment2.CreatedNickName = string.IsNullOrWhiteSpace(comment2.CreatedNickName) ? "游客" : comment2.CreatedNickName;
                }
            }
            return base.View(model);
        }

        public ActionResult Footer()
        {
            Maticsoft.BLL.SysManage.WebSiteSet model = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);
            return base.View("Footer", model);
        }

        public ActionResult FriendLink()
        {
            List<Maticsoft.Model.Settings.FriendlyLink> modelList = new Maticsoft.BLL.Settings.FriendlyLink().GetModelList(10, 1);
            return base.View(modelList);
        }

        public ActionResult Header()
        {
            if (this.WebSiteSet != null)
            {
                ((dynamic) base.ViewBag).Logo = this.WebSiteSet.LogoPath;
                ((dynamic) base.ViewBag).WebName = this.WebSiteSet.WebName;
            }
            return base.View("Header");
        }

        public ActionResult HotArticles()
        {
            List<Maticsoft.Model.CMS.Content> modelList = new Maticsoft.BLL.CMS.Content().GetModelList();
            List<Maticsoft.Model.CMS.Content> model = new List<Maticsoft.Model.CMS.Content>();
            if (modelList != null)
            {
                string valueByCache = ConfigSystem.GetValueByCache("ArticleIsStatic");
                string str2 = ConfigSystem.GetValueByCache("MainArea");
                foreach (Maticsoft.Model.CMS.Content content2 in modelList)
                {
                    if (valueByCache == "true")
                    {
                        content2.SeoUrl = PageSetting.GetCMSUrl(content2.ContentID, "CMS", ApplicationKeyType.CMS);
                    }
                    else if (str2 == "CMS")
                    {
                        content2.SeoUrl = "/Article/Details/" + content2.ContentID;
                    }
                    else
                    {
                        content2.SeoUrl = "/CMS/Article/Details/" + content2.ContentID;
                    }
                    model.Add(content2);
                }
            }
            return base.View(model);
        }

        [ValidateInput(false)]
        public ActionResult MainMenu(int? id)
        {
            if (id.HasValue && (id > 0))
            {
                ((dynamic) base.ViewBag).MaticsoftId = id.Value;
            }
            List<Maticsoft.Model.Settings.MainMenus> menusByArea = new Maticsoft.BLL.Settings.MainMenus().GetMenusByArea(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, "");
            return base.View(menusByArea);
        }

        public ActionResult ShareScript()
        {
            return base.View("ShareScript");
        }
    }
}


namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.CMS;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class SearchController : CMSControllerBase
    {
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);

        [ValidateInput(false)]
        public ActionResult Article(int? id, string keywords, int? page)
        {
            ((dynamic) base.ViewBag).Keywords = Globals.HtmlDecode(keywords);
            if (this.WebSiteSet != null)
            {
                ((dynamic) base.ViewBag).Title = Globals.HtmlDecode(this.WebSiteSet.WebTitle) + "-" + Globals.HtmlDecode(this.WebSiteSet.WebName);
                ((dynamic) base.ViewBag).Description = Globals.HtmlDecode(this.WebSiteSet.Description);
            }
            ((dynamic) base.ViewBag).Domain = this.WebSiteSet.BaseHost;
            ((dynamic) base.ViewBag).WebName = this.WebSiteSet.WebName;
            ((dynamic) base.ViewBag).HotWordss = keywords;
            if (keywords == null)
            {
                return base.View();
            }
            if (keywords.Length > 0x19)
            {
                return base.View();
            }
            Maticsoft.BLL.CMS.Content content = new Maticsoft.BLL.CMS.Content();
            int pageSize = 10;
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int recordCount = content.GetRecordCount(id, keywords);
            PagedList<Maticsoft.Model.CMS.Content> model = null;
            List<Maticsoft.Model.CMS.Content> list2 = content.GetList(id, startIndex, endIndex, keywords);
            string valueByCache = ConfigSystem.GetValueByCache("ArticleIsStatic");
            List<Maticsoft.Model.CMS.Content> items = new List<Maticsoft.Model.CMS.Content>();
            string str2 = ConfigSystem.GetValueByCache("MainArea");
            if ((list2 != null) && (list2.Count > 0))
            {
                foreach (Maticsoft.Model.CMS.Content content2 in list2)
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
                    items.Add(content2);
                }
            }
            if ((items != null) && (items.Count > 0))
            {
                int? nullable = page;
                model = new PagedList<Maticsoft.Model.CMS.Content>(items, nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, recordCount);
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("~/Areas/CMS/Themes/Default/Views/Partial/UCjQuerySearchList.cshtml", model);
            }
            return base.View(model);
        }
    }
}


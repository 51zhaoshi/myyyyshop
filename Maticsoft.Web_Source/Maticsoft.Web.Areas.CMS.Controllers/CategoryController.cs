namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.CMS;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class CategoryController : CMSControllerBase
    {
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);

        public ActionResult List(int? cid, int? page)
        {
            Maticsoft.BLL.CMS.Content content = new Maticsoft.BLL.CMS.Content();
            ((dynamic) base.ViewBag).Domain = this.WebSiteSet.BaseHost;
            ((dynamic) base.ViewBag).WebName = this.WebSiteSet.WebName;
            Maticsoft.BLL.CMS.ContentClass class2 = new Maticsoft.BLL.CMS.ContentClass();
            if (cid.HasValue)
            {
                Maticsoft.Model.CMS.ContentClass modelByCache = class2.GetModelByCache(cid.Value);
                if (modelByCache != null)
                {
                    ((dynamic) base.ViewBag).Title = Globals.HtmlDecode(modelByCache.ClassName);
                    ((dynamic) base.ViewBag).Keywords = Globals.HtmlDecode(modelByCache.Keywords);
                    ((dynamic) base.ViewBag).Description = Globals.HtmlDecode(modelByCache.Description);
                    if (this.WebSiteSet != null)
                    {
                        dynamic viewBag = base.ViewBag;
                        string str3 = "-" + Globals.HtmlDecode(this.WebSiteSet.WebName);
                        if (<List>o__SiteContainer0.<>p__Site6 == null)
                        {
                            <List>o__SiteContainer0.<>p__Site6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "Title", typeof(CategoryController)));
                        }
                        if (!<List>o__SiteContainer0.<>p__Site6.Target(<List>o__SiteContainer0.<>p__Site6, viewBag))
                        {
                            if (<List>o__SiteContainer0.<>p__Site8 == null)
                            {
                                <List>o__SiteContainer0.<>p__Site8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(CategoryController), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                            }
                            viewBag.Title = <List>o__SiteContainer0.<>p__Site8.Target(<List>o__SiteContainer0.<>p__Site8, viewBag.Title, str3);
                        }
                        else
                        {
                            viewBag.add_Title(str3);
                        }
                    }
                }
            }
            int pageSize = 10;
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int recordCount = content.GetRecordCount(cid, "");
            PagedList<Maticsoft.Model.CMS.Content> model = null;
            List<Maticsoft.Model.CMS.Content> list2 = content.GetList(cid, startIndex, endIndex, "");
            if ((list2 != null) && (list2.Count > 0))
            {
                string valueByCache = ConfigSystem.GetValueByCache("ArticleIsStatic");
                List<Maticsoft.Model.CMS.Content> items = new List<Maticsoft.Model.CMS.Content>();
                string str2 = ConfigSystem.GetValueByCache("MainArea");
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
                if ((items != null) && (items.Count > 0))
                {
                    int? nullable = page;
                    model = new PagedList<Maticsoft.Model.CMS.Content>(items, nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, recordCount);
                }
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("~/Areas/CMS/Themes/Default/Views/Partial/UCjQueryArticleList.cshtml", model);
            }
            return base.View(model);
        }
    }
}


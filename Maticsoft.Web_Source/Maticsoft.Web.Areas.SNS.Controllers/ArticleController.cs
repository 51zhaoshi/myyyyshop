namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel;
    using Maticsoft.Web.Components.Setting.CMS;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class ArticleController : SNSControllerBase
    {
        private Maticsoft.BLL.CMS.Comment comBll = new Maticsoft.BLL.CMS.Comment();
        private int commentPagesize = 3;
        private Maticsoft.BLL.CMS.Content contentBll = new Maticsoft.BLL.CMS.Content();
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.SNS);

        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            if (base.currentUser == null)
            {
                return null;
            }
            Maticsoft.Model.CMS.Comment model = new Maticsoft.Model.CMS.Comment();
            int num = Globals.SafeInt(Fm["ContentId"], 0);
            int num2 = 0;
            string str = ViewModelBase.ReplaceFace(Globals.HtmlEncode(Fm["Des"]));
            model.CreatedDate = DateTime.Now;
            model.CreatedNickName = base.currentUser.NickName;
            model.CreatedUserID = base.currentUser.UserID;
            model.Description = str;
            model.IsRead = false;
            model.ReplyCount = 0;
            model.ContentId = new int?(num);
            model.TypeID = 3;
            model.State = true;
            num2 = this.comBll.AddEx(model);
            if (num2 <= 0)
            {
                return base.Content("No");
            }
            model.ID = num2;
            List<Maticsoft.Model.CMS.Comment> list = new List<Maticsoft.Model.CMS.Comment>();
            if (!FilterWords.ContainsModWords(model.Description))
            {
                list.Add(model);
            }
            return this.PartialView("_ArticleComment", list);
        }

        public ActionResult AjaxCount(int ContentId)
        {
            Maticsoft.Model.CMS.Content model = this.contentBll.GetModel(ContentId);
            return base.Content(model.TotalFav + "|" + model.TotalComment);
        }

        public ActionResult AjaxFavCount(int ContentId)
        {
            if ((base.Request.Cookies["ContentFav" + ContentId] != null) && (base.Request.Cookies["ContentFav" + ContentId].Value == ContentId.ToString()))
            {
                return base.Content("Repeat");
            }
            if (this.contentBll.UpdateFav(ContentId))
            {
                HttpCookie cookie = new HttpCookie("ContentFav" + ContentId) {
                    Value = ContentId.ToString(),
                    Expires = DateTime.MaxValue
                };
                base.Response.AppendCookie(cookie);
                return base.Content("Yes");
            }
            return base.Content("No");
        }

        public ActionResult AjaxGetComments(int ContentId, int? PageIndex)
        {
            int startPageIndex = ViewModelBase.GetStartPageIndex(this.commentPagesize, PageIndex.Value);
            int endPageIndex = ViewModelBase.GetEndPageIndex(this.commentPagesize, PageIndex.Value);
            List<Maticsoft.Model.CMS.Comment> model = this.comBll.GetComments(ContentId, startPageIndex, endPageIndex);
            return this.PartialView("_ArticleComment", model);
        }

        public ActionResult AjaxGetPvCount(int id)
        {
            int num = 0;
            num = this.contentBll.UpdatePV(id);
            return base.Content(num.ToString());
        }

        public ActionResult ArticleDetail(int id)
        {
            if (!this.contentBll.Exists(id))
            {
                return base.RedirectToAction("Index", "Error");
            }
            Maticsoft.Model.CMS.Content modelByCache = this.contentBll.GetModelByCache(id);
            IPageSetting setting = PageSetting.GetArticleSetting(modelByCache, "CMS", ApplicationKeyType.CMS);
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            return base.View(modelByCache);
        }

        public ActionResult ArticleLeft()
        {
            int classID = Globals.SafeInt(ConfigSystem.GetValueByCache("SNSHelpCenter"), 1);
            List<Maticsoft.Model.CMS.Content> modelList = new Maticsoft.BLL.CMS.Content().GetModelList(classID);
            return base.View(modelList);
        }

        public PartialViewResult ArticlePart(int ClassId, string viewName = "_ArticlePart", int top = 10, int ContentId = -1)
        {
            List<Maticsoft.Model.CMS.Content> model = this.contentBll.GetMoreList(ClassId, ContentId, top);
            return this.PartialView(viewName, model);
        }

        public ActionResult Column()
        {
            int classID = Globals.SafeInt(ConfigSystem.GetValueByCache("SNSHelpCenter"), 1);
            Maticsoft.Model.CMS.ContentClass modelByCache = new Maticsoft.BLL.CMS.ContentClass().GetModelByCache(classID);
            return base.View(modelByCache);
        }

        public ActionResult Details(int id)
        {
            Maticsoft.BLL.CMS.Content content = new Maticsoft.BLL.CMS.Content();
            Maticsoft.Model.CMS.Content modelExByCache = content.GetModelExByCache(id);
            if (modelExByCache != null)
            {
                ((dynamic) base.ViewBag).ArticleId = id;
                ((dynamic) base.ViewBag).Title = Globals.HtmlDecode(modelExByCache.Title);
                if (this.WebSiteSet != null)
                {
                    dynamic viewBag = base.ViewBag;
                    string str = "-" + Globals.HtmlDecode(this.WebSiteSet.WebName);
                    if (<Details>o__SiteContainer9.<>p__Sitec == null)
                    {
                        <Details>o__SiteContainer9.<>p__Sitec = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "Title", typeof(ArticleController)));
                    }
                    if (!<Details>o__SiteContainer9.<>p__Sitec.Target(<Details>o__SiteContainer9.<>p__Sitec, viewBag))
                    {
                        if (<Details>o__SiteContainer9.<>p__Sitee == null)
                        {
                            <Details>o__SiteContainer9.<>p__Sitee = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(ArticleController), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                        }
                        viewBag.Title = <Details>o__SiteContainer9.<>p__Sitee.Target(<Details>o__SiteContainer9.<>p__Sitee, viewBag.Title, str);
                    }
                    else
                    {
                        viewBag.add_Title(str);
                    }
                }
                content.UpdatePV(id);
                ((dynamic) base.ViewBag).Keywords = Globals.HtmlDecode(modelExByCache.Keywords);
                ((dynamic) base.ViewBag).Description = Globals.HtmlDecode(modelExByCache.Summary);
            }
            return base.View(modelExByCache);
        }

        public PartialViewResult HotArticle(int ClassId = 0, string viewName = "_HotArticle", int top = -1)
        {
            List<Maticsoft.Model.CMS.Content> model = this.contentBll.GetRecList(ClassId, Maticsoft.Model.CMS.EnumHelper.ContentRec.Hot, top, false);
            return this.PartialView(viewName, model);
        }

        public PartialViewResult HotComment(int ClassId = 0, string viewName = "_HotComment", int top = -1)
        {
            List<Maticsoft.Model.CMS.Content> hotComList = this.contentBll.GetHotComList(ClassId, top);
            return this.PartialView(viewName, hotComList);
        }

        public ActionResult Index(int classId, int? page)
        {
            Maticsoft.Model.CMS.ContentClass classModel = Maticsoft.BLL.CMS.ContentClass.GetAllClass().FirstOrDefault<Maticsoft.Model.CMS.ContentClass>(c => c.ClassID == classId);
            if (classModel != null)
            {
                ((dynamic) base.ViewBag).ClassName = classModel.ClassName;
            }
            IPageSetting setting = PageSetting.GetContentClassSetting(classModel, "CMSSelf", ApplicationKeyType.CMS);
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 20;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int totalItemCount = 0;
            totalItemCount = this.contentBll.GetRecordCount(" State=0  and ClassID=" + classId);
            ((dynamic) base.ViewBag).TotalCount = totalItemCount;
            if (totalItemCount == 0)
            {
                return base.View();
            }
            int? nullable = page;
            PagedList<Maticsoft.Model.CMS.Content> model = new PagedList<Maticsoft.Model.CMS.Content>(this.contentBll.GetList(new int?(classId), startIndex, endIndex, ""), nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/Article/ArticleList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/Article/Index.cshtml", model);
        }

        public string NoHTML(string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "\x00a1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "\x00a2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "\x00a3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "\x00a9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
    }
}


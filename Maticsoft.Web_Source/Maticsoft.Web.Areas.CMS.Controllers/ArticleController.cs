namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.CMS;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Mvc;

    public class ArticleController : CMSControllerBase
    {
        private readonly TimeSpan _commentTimeSpan = new TimeSpan(0, 0, 0, 30);
        private int Act_EditContent = 15;
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        private const string SESSIONKEY_COMMENTDATE = "CMS_CommentDate";
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);

        public ActionResult Comment(Maticsoft.Model.CMS.Comment model)
        {
            if ((base.Session["CMS_CommentDate"] != null) && !string.IsNullOrEmpty(model.Description))
            {
                DateTime? nullable = Globals.SafeDateTime(base.Session["CMS_CommentDate"].ToString(), (DateTime?) null);
                if (nullable.HasValue && ((DateTime.Now - nullable.Value) < this._commentTimeSpan))
                {
                    return base.Content("NOCOMMENT");
                }
            }
            model.CreatedDate = DateTime.Now;
            Maticsoft.BLL.CMS.Comment comment = new Maticsoft.BLL.CMS.Comment();
            List<Maticsoft.Model.CMS.Comment> modelList = new List<Maticsoft.Model.CMS.Comment>();
            if (string.IsNullOrEmpty(model.Description))
            {
                modelList = comment.GetModelList("ContentId=" + model.ContentId);
                if ((modelList != null) && (modelList.Count > 0))
                {
                    foreach (Maticsoft.Model.CMS.Comment comment2 in modelList)
                    {
                        comment2.CreatedNickName = string.IsNullOrWhiteSpace(comment2.CreatedNickName) ? "游客" : comment2.CreatedNickName;
                    }
                }
                return base.PartialView(modelList);
            }
            model.TypeID = 3;
            if (base.currentUser != null)
            {
                model.CreatedUserID = base.currentUser.UserID;
                model.CreatedNickName = base.currentUser.NickName;
            }
            if ((model.ContentId = new int?(comment.Add(model))) > 0)
            {
                model.CreatedNickName = string.IsNullOrWhiteSpace(model.CreatedNickName) ? "游客" : model.CreatedNickName;
                modelList.Add(model);
                base.Session["CMS_CommentDate"] = DateTime.Now;
                return base.PartialView(modelList);
            }
            return base.Content("False");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return base.View("Details");
            }
            int contentID = id.Value;
            Maticsoft.Model.CMS.Content modelExByCache = this.bll.GetModelExByCache(contentID);
            if (modelExByCache != null)
            {
                ((dynamic) base.ViewBag).Title = Globals.HtmlDecode(modelExByCache.Title);
                if (this.WebSiteSet != null)
                {
                    dynamic viewBag = base.ViewBag;
                    string str3 = "-" + Globals.HtmlDecode(this.WebSiteSet.WebName);
                    if (<Details>o__SiteContainer0.<>p__Site2 == null)
                    {
                        <Details>o__SiteContainer0.<>p__Site2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "Title", typeof(ArticleController)));
                    }
                    if (!<Details>o__SiteContainer0.<>p__Site2.Target(<Details>o__SiteContainer0.<>p__Site2, viewBag))
                    {
                        if (<Details>o__SiteContainer0.<>p__Site4 == null)
                        {
                            <Details>o__SiteContainer0.<>p__Site4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof(ArticleController), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                        }
                        viewBag.Title = <Details>o__SiteContainer0.<>p__Site4.Target(<Details>o__SiteContainer0.<>p__Site4, viewBag.Title, str3);
                    }
                    else
                    {
                        viewBag.add_Title(str3);
                    }
                }
                ((dynamic) base.ViewBag).Keywords = Globals.HtmlDecode(modelExByCache.Keywords);
                ((dynamic) base.ViewBag).Description = Globals.HtmlDecode(modelExByCache.Summary);
                ((dynamic) base.ViewBag).Domain = this.WebSiteSet.BaseHost;
                ((dynamic) base.ViewBag).WebName = this.WebSiteSet.WebName;
                string valueByCache = ConfigSystem.GetValueByCache("ArticleIsStatic");
                string str2 = ConfigSystem.GetValueByCache("MainArea");
                int prevID = this.bll.GetPrevID(contentID, -1);
                int nextID = this.bll.GetNextID(contentID, -1);
                if (valueByCache != "true")
                {
                    if (prevID > 0)
                    {
                        if (str2 == "CMS")
                        {
                            ((dynamic) base.ViewBag).PrevUrl = "/Article/Details/" + prevID;
                        }
                        else
                        {
                            ((dynamic) base.ViewBag).PrevUrl = "/CMS/Article/Details/" + prevID;
                        }
                    }
                    else
                    {
                        ((dynamic) base.ViewBag).PrevUrl = "";
                    }
                    if (nextID > 0)
                    {
                        if (str2 == "CMS")
                        {
                            ((dynamic) base.ViewBag).NextUrl = "/Article/Details/" + nextID;
                        }
                        else
                        {
                            ((dynamic) base.ViewBag).NextUrl = "/CMS/Article/Details/" + nextID;
                        }
                    }
                    else
                    {
                        ((dynamic) base.ViewBag).NextUrl = "";
                    }
                }
                else
                {
                    if (prevID > 0)
                    {
                        ((dynamic) base.ViewBag).PrevUrl = PageSetting.GetCMSUrl(prevID, "CMS", ApplicationKeyType.CMS);
                    }
                    else
                    {
                        ((dynamic) base.ViewBag).PrevUrl = "";
                    }
                    if (nextID > 0)
                    {
                        ((dynamic) base.ViewBag).NextUrl = PageSetting.GetCMSUrl(prevID, "CMS", ApplicationKeyType.CMS);
                    }
                    else
                    {
                        ((dynamic) base.ViewBag).NextUrl = "";
                    }
                }
                if (((base.UserPrincipal != null) && (base.currentUser != null)) && base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_EditContent)))
                {
                    ((dynamic) base.ViewBag).EditContent = "";
                }
                else
                {
                    ((dynamic) base.ViewBag).EditContent = "display:none;";
                }
            }
            return base.View(modelExByCache);
        }

        [HttpPost]
        public void GetPvCount(int id)
        {
            JsonObject obj2 = new JsonObject();
            int num = this.bll.UpdatePV(id);
            obj2.Accumulate("STATUS", "SUCC");
            obj2.Accumulate("DATA", num);
            base.Response.Write(obj2.ToString());
        }

        [HttpPost]
        public void Support(int id)
        {
            JsonObject obj2 = new JsonObject();
            if ((base.Request.Cookies["UsersSupports" + id] != null) && (base.Request.Cookies["UsersSupports" + id].Value == id.ToString()))
            {
                obj2.Accumulate("STATUS", "NOTALLOW");
            }
            else if (this.bll.UpdateTotalSupport(id))
            {
                Maticsoft.Model.CMS.Content model = this.bll.GetModel(id);
                Maticsoft.Model.CMS.Content modelExByCache = this.bll.GetModelExByCache(id);
                if (model != null)
                {
                    obj2.Accumulate("STATUS", "SUCC");
                    obj2.Accumulate("TotalSupport", model.TotalSupport);
                    modelExByCache.TotalSupport = model.TotalSupport;
                    HttpCookie cookie = new HttpCookie("UsersSupports" + id) {
                        Value = id.ToString(),
                        Expires = DateTime.MaxValue
                    };
                    base.Response.AppendCookie(cookie);
                }
                else
                {
                    obj2.Accumulate("STATUS", "FAIL");
                }
            }
            else
            {
                obj2.Accumulate("STATUS", "FAIL");
            }
            base.Response.Write(obj2.ToString());
        }
    }
}


namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class BlogController : SNSControllerBase
    {
        private Maticsoft.BLL.SNS.UserBlog blogBll = new Maticsoft.BLL.SNS.UserBlog();

        public PartialViewResult ActiveUser(string viewName = "_ActiveUser", int top = 15)
        {
            List<Maticsoft.Model.SNS.UserBlog> activeUser = this.blogBll.GetActiveUser(top);
            return this.PartialView(viewName, activeUser);
        }

        public ActionResult AjaxCount(int BlogId)
        {
            Maticsoft.Model.SNS.UserBlog model = this.blogBll.GetModel(BlogId);
            return base.Content(model.TotalFav + "|" + model.TotalComment);
        }

        public ActionResult AjaxFavCount(int BlogId)
        {
            if ((base.Request.Cookies["UsersBlogFav" + BlogId] != null) && (base.Request.Cookies["UsersBlogFav" + BlogId].Value == BlogId.ToString()))
            {
                return base.Content("Repeat");
            }
            if (this.blogBll.UpdateFavCount(BlogId))
            {
                HttpCookie cookie = new HttpCookie("UsersBlogFav" + BlogId) {
                    Value = BlogId.ToString(),
                    Expires = DateTime.MaxValue
                };
                base.Response.AppendCookie(cookie);
                return base.Content("Yes");
            }
            return base.Content("No");
        }

        public ActionResult AjaxGetPvCount(int id)
        {
            int pvCount = 0;
            if (this.blogBll.UpdatePvCount(id))
            {
                pvCount = this.blogBll.GetPvCount(id);
            }
            return base.Content(pvCount.ToString());
        }

        public ActionResult BlogDetail(int id)
        {
            if (!this.blogBll.Exists(id))
            {
                return base.RedirectToAction("Index", "Error");
            }
            Maticsoft.Model.SNS.UserBlog modelByCache = this.blogBll.GetModelByCache(id);
            IPageSetting setting = PageSetting.GetBlogDetailSetting(modelByCache, "BlogDetail", ApplicationKeyType.SNS);
            setting.Replace(new string[][] { new string[] { "{cateid}", modelByCache.Title.ToString() }, new string[] { "{cid}", modelByCache.BlogID.ToString() } });
            ((dynamic) base.ViewBag).Title = setting.Title;
            ((dynamic) base.ViewBag).Keywords = setting.Keywords;
            ((dynamic) base.ViewBag).Description = setting.Description;
            return base.View(modelByCache);
        }

        public PartialViewResult BlogPart(string viewName = "_BlogPart", int top = -1, int UserId = -1, int BlogId = -1)
        {
            List<Maticsoft.Model.SNS.UserBlog> model = this.blogBll.GetMoreList(UserId, BlogId, top);
            return this.PartialView(viewName, model);
        }

        public PartialViewResult HotBlog(string viewName = "_HotBlog", int top = -1)
        {
            List<Maticsoft.Model.SNS.UserBlog> hotBlogList = this.blogBll.GetHotBlogList(top);
            return this.PartialView(viewName, hotBlogList);
        }

        public ActionResult Index(int? page)
        {
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int totalItemCount = 0;
            IPageSetting pageSetting = PageSetting.GetPageSetting("BlogList", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            totalItemCount = this.blogBll.GetRecordCount("  Status=1 ");
            ((dynamic) base.ViewBag).TotalCount = totalItemCount;
            if (totalItemCount == 0)
            {
                return base.View();
            }
            int? nullable = page;
            PagedList<Maticsoft.Model.SNS.UserBlog> model = new PagedList<Maticsoft.Model.SNS.UserBlog>(this.blogBll.GetUserBlogPage(" Status=1", " CreatedDate desc", startIndex, endIndex), nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/Blog/BlogList.cshtml", model);
            }
            return base.View(base.CurrentThemeViewPath + "/Blog/Index.cshtml", model);
        }

        public PartialViewResult NewComment(string viewName = "_NewComment", int top = 15)
        {
            List<Maticsoft.Model.SNS.Comments> model = new Maticsoft.BLL.SNS.Comments().GetBlogComment(" type=4 and Status=1", " CreatedDate desc", top);
            if (model.Count > 0)
            {
                foreach (Maticsoft.Model.SNS.Comments comments2 in model)
                {
                    comments2.Description = this.NoHTML(comments2.Description);
                }
            }
            return this.PartialView(viewName, model);
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


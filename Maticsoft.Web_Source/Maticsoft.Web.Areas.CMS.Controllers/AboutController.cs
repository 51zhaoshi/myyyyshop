namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using System.Web.Mvc;

    public class AboutController : CMSControllerBase
    {
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);

        public ActionResult Index()
        {
            ((dynamic) base.ViewBag).Title = "关于";
            if (this.WebSiteSet != null)
            {
                ((dynamic) base.ViewBag).Keywords = Globals.HtmlDecode(this.WebSiteSet.KeyWords);
                ((dynamic) base.ViewBag).Description = Globals.HtmlDecode(this.WebSiteSet.Description);
            }
            return base.View("Index");
        }
    }
}


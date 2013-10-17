namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : ShopControllerBase
    {
        public ActionResult Index()
        {
            ((dynamic) base.ViewBag).Title = "出错啦 - " + ((dynamic) base.ViewBag).SiteName;
            return base.View();
        }

        public ActionResult TurnOff()
        {
            ((dynamic) base.ViewBag).Title = "该功能已关闭 - " + ((dynamic) base.ViewBag).SiteName;
            return base.View();
        }

        public ActionResult UserError()
        {
            ((dynamic) base.ViewBag).Title = "用户不存在 - " + ((dynamic) base.ViewBag).SiteName;
            return base.View();
        }
    }
}


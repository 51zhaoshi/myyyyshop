namespace Maticsoft.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult About()
        {
            return base.View();
        }

        public ActionResult Index()
        {
            ((dynamic) base.ViewBag).Message = "欢迎使用 ASP.NET MVC!";
            return base.View();
        }
    }
}


namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using System.Web.Mvc;

    public class DemoController : CMSControllerBase
    {
        public ActionResult Index()
        {
            return base.View("UploadImg");
        }
    }
}


namespace Maticsoft.Web.Areas.Shop.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    public class DownloadController : ShopControllerBase
    {
        private const string APP_FILEPATH = "~/Download/MaticsoftShop.apk";

        public ActionResult Android()
        {
            string path = base.Server.MapPath("~/Download/MaticsoftShop.apk");
            string fileName = Path.GetFileName(path);
            return this.File(path, "application/vnd.android.package-archive", base.Url.Encode(fileName));
        }
    }
}


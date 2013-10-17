namespace Maticsoft.Web.Areas.Mobile.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class VideosController : MobileControllerBase
    {
        public ActionResult VideoDetail(int vid = -1, string viewName = "VideoDetail")
        {
            Maticsoft.Model.CMS.Video modelByCache = new Maticsoft.BLL.CMS.Video().GetModelByCache(vid);
            return base.View(viewName, modelByCache);
        }

        public ActionResult VideosList(int pageIndex = 1, string viewName = "VideosList")
        {
            int pageSize = 6;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = pageIndex * pageSize;
            int totalItemCount = 0;
            Maticsoft.BLL.CMS.Video video = new Maticsoft.BLL.CMS.Video();
            PagedList<Maticsoft.Model.CMS.Video> model = new PagedList<Maticsoft.Model.CMS.Video>(video.GetListByPage(startIndex, endIndex, out totalItemCount), pageIndex, pageSize, totalItemCount);
            ((dynamic) base.ViewBag).pageIndex = pageIndex;
            ((dynamic) base.ViewBag).totalPage = (int) Math.Ceiling((double) (((double) totalItemCount) / ((double) pageSize)));
            return base.View(viewName, model);
        }
    }
}


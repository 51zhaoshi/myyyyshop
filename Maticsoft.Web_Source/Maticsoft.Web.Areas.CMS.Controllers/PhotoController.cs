namespace Maticsoft.Web.Areas.CMS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Model.CMS;
    using Maticsoft.ViewModel.CMS;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class PhotoController : CMSControllerBase
    {
        private int _basePageSize = 8;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 0x20;

        public ActionResult AjaxLikePhoto(int PhotoId)
        {
            Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
            Maticsoft.Model.CMS.Photo model = new Maticsoft.Model.CMS.Photo();
            if (photo.Exists(PhotoId))
            {
                model = photo.GetModel(PhotoId);
                model.FavouriteCount++;
                if (photo.Update(model))
                {
                    return base.Content("True");
                }
            }
            return base.Content("False");
        }

        public ActionResult Detail(int id)
        {
            Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
            Maticsoft.Model.CMS.Photo model = new Maticsoft.Model.CMS.Photo();
            model = photo.GetModel(id);
            if (model == null)
            {
                return new EmptyResult();
            }
            List<Maticsoft.Model.CMS.Photo> list = photo.GetListAroundPhotoId(10, id, model.ClassID);
            return base.View(list);
        }

        public ActionResult Index(int? pageIndex, int? photoClassId)
        {
            ((dynamic) base.ViewBag).Title = "图片";
            Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
            Maticsoft.ViewModel.CMS.Photo model = new Maticsoft.ViewModel.CMS.Photo();
            model.PhotoClassList = new Maticsoft.BLL.CMS.PhotoClass().GetTopList(10, "Depth=1", "Sequence");
            ((dynamic) base.ViewBag).PhotoClassId = photoClassId;
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int totalItemCount = 0;
            string strWhere = photoClassId.HasValue ? ("ClassID=" + photoClassId.Value) : "";
            totalItemCount = photo.GetRecordCount(strWhere);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > totalItemCount) ? totalItemCount : num5;
            List<Maticsoft.Model.CMS.Photo> items = photo.GetListModelByPage(strWhere, "PhotoID DESC", startIndex, endIndex);
            int? nullable = pageIndex;
            model.PhotoPagedList = new PagedList<Maticsoft.Model.CMS.Photo>(items, nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, totalItemCount);
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("PhotoList", model);
            }
            return base.View(model);
        }

        public ActionResult PhotoListWaterfall(int startIndex, int? photoClassId)
        {
            Maticsoft.BLL.CMS.Photo photo = new Maticsoft.BLL.CMS.Photo();
            Maticsoft.ViewModel.CMS.Photo model = new Maticsoft.ViewModel.CMS.Photo();
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            string strWhere = photoClassId.HasValue ? ("ClassID=" + photoClassId.Value) : "";
            if (photo.GetRecordCount(strWhere) < 1)
            {
                return new EmptyResult();
            }
            model.PhotoListWaterfall = photo.GetListModelByPage(strWhere, "PhotoID DESC", startIndex, endIndex);
            return base.View(model);
        }
    }
}


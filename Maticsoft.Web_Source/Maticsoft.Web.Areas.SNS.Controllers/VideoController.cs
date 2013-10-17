namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class VideoController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 0x20;
        private int commentPagesize = 5;
        private Maticsoft.BLL.SNS.Posts postBll = new Maticsoft.BLL.SNS.Posts();

        public VideoController()
        {
            this.commentPagesize = base.CommentDataSize;
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
        }

        public ActionResult Detail(int id)
        {
            Maticsoft.Model.SNS.Posts model = new Maticsoft.BLL.SNS.Posts().GetModel(id);
            if (model == null)
            {
                return base.RedirectToAction("Index", "Home");
            }
            ((dynamic) base.ViewBag).CommentPageSize = this.commentPagesize;
            ((dynamic) base.ViewBag).Commentcount = new Maticsoft.BLL.SNS.Comments().GetCommentCount(0, id);
            IPageSetting pageSetting = PageSetting.GetPageSetting("VideoDetail", ApplicationKeyType.SNS);
            string[][] values = new string[1][];
            values[0] = new string[] { "{cname}", model.Description ?? (model.CreatedNickName + "分享的视频") };
            pageSetting.Replace(values);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(model);
        }

        public ActionResult Index(int? pageIndex)
        {
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int recordCount = 0;
            recordCount = this.postBll.GetRecordCount(" type=3 and Status=1");
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > recordCount) ? recordCount : num5;
            IPageSetting pageSetting = PageSetting.GetPageSetting("VideoList", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (recordCount < 1)
            {
                return base.View();
            }
            int? nullable = pageIndex;
            PagedList<Maticsoft.Model.SNS.Posts> model = this.postBll.GetVideoListByPage(-1, startIndex, endIndex).ToPagedList<Maticsoft.Model.SNS.Posts>(nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, new int?(recordCount));
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("VideoList", model);
            }
            return base.View(model);
        }

        public PartialViewResult TopVideo(int id, int top = 5)
        {
            List<Maticsoft.Model.SNS.Posts> videoList = new Maticsoft.BLL.SNS.Posts().GetVideoList(id, top);
            return this.PartialView("_TopVideo", videoList);
        }

        public PartialViewResult UserInfo(int id)
        {
            UsersExp exp = new UsersExp();
            UsersExpModel usersModel = new UsersExpModel();
            usersModel = exp.GetUsersModel(id);
            if (usersModel == null)
            {
                return base.PartialView();
            }
            return this.PartialView("_UserInfo", usersModel);
        }

        [HttpPost]
        public ActionResult VideosWaterfall(int startIndex)
        {
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            if (this.postBll.GetRecordCount(" type=3 and Status=1") < 1)
            {
                return new EmptyResult();
            }
            List<Maticsoft.Model.SNS.Posts> model = this.postBll.GetVideoListByPage(-1, startIndex, endIndex);
            return base.View(base.CurrentThemeViewPath + "/Video/VideoListWaterfall.cshtml", model);
        }
    }
}


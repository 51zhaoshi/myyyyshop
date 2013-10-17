namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class AudioController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 0x20;
        private int commentPagesize = 5;
        private Maticsoft.BLL.SNS.Posts postBll = new Maticsoft.BLL.SNS.Posts();

        [HttpPost]
        public ActionResult AudiosWaterfall(int startIndex)
        {
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            if (this.postBll.GetRecordCount(" Status=1 and AudioUrl IS NOT NULL AND AudioUrl <>'' ") < 1)
            {
                return new EmptyResult();
            }
            List<Maticsoft.Model.SNS.Posts> model = this.postBll.GetAudioListByPage(-1, startIndex, endIndex);
            return base.View(base.CurrentThemeViewPath + "/Audio/AudioListWaterfall.cshtml", model);
        }

        public ActionResult Detail(int id)
        {
            Maticsoft.Model.SNS.Posts model = this.postBll.GetModel(id);
            if (model == null)
            {
                return base.RedirectToAction("Index", "Home");
            }
            ((dynamic) base.ViewBag).CommentPageSize = this.commentPagesize;
            ((dynamic) base.ViewBag).Commentcount = new Maticsoft.BLL.SNS.Comments().GetCommentCount(0, id);
            IPageSetting pageSetting = PageSetting.GetPageSetting("PhotoDetail", ApplicationKeyType.SNS);
            string[][] values = new string[1][];
            values[0] = new string[] { "{cname}", model.Description ?? (model.CreatedNickName + "分享的音乐") };
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
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int recordCount = 0;
            recordCount = this.postBll.GetRecordCount(" Status=1 and AudioUrl IS NOT NULL AND AudioUrl <>'' ");
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > recordCount) ? recordCount : num5;
            if (recordCount < 1)
            {
                return base.View();
            }
            int? nullable = pageIndex;
            PagedList<Maticsoft.Model.SNS.Posts> model = this.postBll.GetAudioListByPage(-1, startIndex, endIndex).ToPagedList<Maticsoft.Model.SNS.Posts>(nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, new int?(recordCount));
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("AudioList", model);
            }
            return base.View(model);
        }
    }
}


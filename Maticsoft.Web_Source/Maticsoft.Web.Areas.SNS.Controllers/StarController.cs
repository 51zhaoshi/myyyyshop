namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class StarController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 30;
        private Maticsoft.BLL.SNS.Star starBll = new Maticsoft.BLL.SNS.Star();
        private Maticsoft.BLL.SNS.StarRank starRankBll = new Maticsoft.BLL.SNS.StarRank();
        private Maticsoft.BLL.SNS.StarType starTypeBll = new Maticsoft.BLL.SNS.StarType();

        public ActionResult Apply()
        {
            ((dynamic) base.ViewBag).Title = "申请达人";
            if (base.CurrentUser == null)
            {
                return base.RedirectToAction("Pioneer", "Star");
            }
            Maticsoft.ViewModel.SNS.Star model = new Maticsoft.ViewModel.SNS.Star();
            List<Maticsoft.Model.SNS.StarType> modelList = this.starTypeBll.GetModelList("");
            List<SelectListItem> list2 = new List<SelectListItem>();
            SelectListItem item2 = new SelectListItem {
                Value = "0",
                Text = "请选择"
            };
            list2.Add(item2);
            if ((modelList != null) && (modelList.Count > 0))
            {
                foreach (Maticsoft.Model.SNS.StarType type in modelList)
                {
                    SelectListItem item = new SelectListItem {
                        Value = type.TypeID.ToString(),
                        Text = type.TypeName
                    };
                    list2.Add(item);
                }
            }
            model.DropList = list2;
            return base.View(model);
        }

        [HttpPost]
        public ActionResult Apply(Maticsoft.ViewModel.SNS.Star model)
        {
            ((dynamic) base.ViewBag).Title = "申请达人";
            if (base.CurrentUser != null)
            {
                if (this.starBll.Exists(base.currentUser.UserID, model.StarModel.TypeID))
                {
                    ((dynamic) base.ViewBag).Type = "Exists";
                    return base.View(model);
                }
                model.StarModel.UserID = base.CurrentUser.UserID;
                model.StarModel.Status = 0;
                model.StarModel.NickName = base.CurrentUser.NickName;
                model.StarModel.CreatedDate = DateTime.Now;
                if (!model.StarModel.UserGravatar.Contains("http://"))
                {
                    FileInfo info = new FileInfo(model.StarModel.UserGravatar);
                    string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("StarImagePath");
                    if (string.IsNullOrEmpty(valueByCache))
                    {
                        valueByCache = "/Upload/SNS/Images/Star/";
                    }
                    string path = valueByCache + info.Name;
                    if (!Directory.Exists(valueByCache))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(valueByCache));
                    }
                    if (File.Exists(HttpContext.Current.Server.MapPath(model.StarModel.UserGravatar)))
                    {
                        File.Move(HttpContext.Current.Server.MapPath(model.StarModel.UserGravatar), HttpContext.Current.Server.MapPath(path));
                    }
                    model.StarModel.UserGravatar = path;
                }
                if (this.starBll.Add(model.StarModel) > 1)
                {
                    ((dynamic) base.ViewBag).Type = "Success";
                    return base.View(model);
                }
            }
            return base.View(model);
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult Pioneer(int pageIndex = 1, int StarType = 0)
        {
            ((dynamic) base.ViewBag).Title = "达人排行";
            Maticsoft.BLL.SNS.StarType type = new Maticsoft.BLL.SNS.StarType();
            Stars model = new Stars();
            if (StarType == 0)
            {
                ((dynamic) base.ViewBag).Type = "Index";
                ((dynamic) base.ViewBag).TypeName = "综合推荐";
                model.HotStarList = this.starRankBll.HotStarList(4);
            }
            else
            {
                Maticsoft.Model.SNS.StarType type2 = type.GetModel(StarType);
                ((dynamic) base.ViewBag).TypeName = type2.TypeName;
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Star", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            model.StarNewList = this.starBll.GetStarNewList(StarType, 10);
            model.StarRankList = this.starRankBll.GetStarRankList(StarType, 10);
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            pageIndex = (pageIndex > 1) ? pageIndex : 1;
            int startIndex = (pageIndex > 1) ? (((pageIndex - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int countByType = 0;
            countByType = this.starBll.GetCountByType(StarType);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > countByType) ? countByType : num5;
            int currentUserId = (base.currentUser != null) ? base.currentUser.UserID : 0;
            if ((countByType < 1) && (StarType != 0))
            {
                return base.RedirectToAction("Pioneer");
            }
            model.StarPagedList = this.starBll.GetListForPage(StarType, "", startIndex, endIndex, currentUserId).ToPagedList<ViewStar>(pageIndex, pageSize, new int?(countByType));
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("StarList", model);
            }
            return base.View(model);
        }

        public ActionResult StarType()
        {
            Maticsoft.BLL.SNS.StarType type = new Maticsoft.BLL.SNS.StarType();
            ((dynamic) base.ViewBag).StarType = base.Request.Params["StarType"];
            List<Maticsoft.Model.SNS.StarType> modelList = type.GetModelList("");
            if ((modelList != null) && (modelList.Count > 0))
            {
                return base.View("_StarType", modelList);
            }
            return base.View("_StarType");
        }

        [HttpPost]
        public ActionResult WaterfallStarListData(int StartIndex, int StarType = 0)
        {
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            StartIndex = (StartIndex > 1) ? (StartIndex + 1) : 0;
            int endIndex = (StartIndex > 1) ? ((StartIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            int countByType = 0;
            countByType = this.starBll.GetCountByType(StarType);
            int currentUserId = (base.currentUser != null) ? base.currentUser.UserID : 0;
            if (countByType < 1)
            {
                return new EmptyResult();
            }
            Stars model = new Stars {
                StarList = this.starBll.GetListForPage(StarType, "", StartIndex, endIndex, currentUserId)
            };
            return base.View("StarListWaterfall", model);
        }
    }
}


namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Webdiyer.WebControls.Mvc;

    public class AlbumController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _commentPageSize = 8;
        private int _waterfallDetailCount = 1;
        private int _waterfallSize = 30;
        private Maticsoft.BLL.SNS.UserAlbums bllAlbums = new Maticsoft.BLL.SNS.UserAlbums();
        private UsersExp blluser = new UsersExp();
        private Maticsoft.BLL.SNS.Photos photoBll = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.SNS.Products productBll = new Maticsoft.BLL.SNS.Products();

        public AlbumController()
        {
            this._basePageSize = base.FallInitDataSize;
            this._waterfallSize = base.FallDataSize;
            this._commentPageSize = base.CommentDataSize;
        }

        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            Maticsoft.Model.SNS.Comments comModel = new Maticsoft.Model.SNS.Comments();
            int num = Globals.SafeInt(Fm["TargetId"], 0);
            int num2 = 3;
            int num3 = 0;
            string source = ViewModelBase.ReplaceFace(Fm["Des"]);
            comModel.CreatedDate = DateTime.Now;
            comModel.CreatedNickName = base.currentUser.NickName;
            comModel.CreatedUserID = base.currentUser.UserID;
            comModel.Description = source;
            comModel.HasReferUser = source.Contains<char>('@');
            comModel.IsRead = false;
            comModel.ReplyCount = 0;
            comModel.TargetId = num;
            comModel.Type = num2;
            comModel.UserIP = base.Request.UserHostAddress;
            num3 = comments.AddEx(comModel);
            if (num3 > 0)
            {
                comModel.CommentID = num3;
                comModel.Description = ViewModelBase.RegexNickName(comModel.Description);
                List<Maticsoft.Model.SNS.Comments> model = new List<Maticsoft.Model.SNS.Comments> {
                    comModel
                };
                return this.PartialView("CommentList", model);
            }
            return base.Content("No");
        }

        public ActionResult AjaxCheckIsFav(int AlbumId)
        {
            if (base.currentUser == null)
            {
                return new EmptyResult();
            }
            bool flag = new Maticsoft.BLL.SNS.UserFavAlbum().CheckIsFav(AlbumId, base.currentUser.UserID);
            return base.Content(flag.ToString());
        }

        public ActionResult AjaxFavAlbum(int AlbumId)
        {
            int num = new Maticsoft.BLL.SNS.UserFavAlbum().FavAlbum(AlbumId, base.currentUser.UserID);
            if (num > 0)
            {
                return base.Content(num.ToString());
            }
            return base.Content("Fail");
        }

        public ActionResult AjaxGetComments(FormCollection Fm)
        {
            int pageIndex = Globals.SafeInt(Fm["pageIndex"], 1);
            int targetId = Globals.SafeInt(Fm["AlbumId"], 1);
            List<Maticsoft.Model.SNS.Comments> model = new Maticsoft.BLL.SNS.Comments().GetCommentByPage(3, targetId, ViewModelBase.GetStartPageIndex(this._commentPageSize, pageIndex), ViewModelBase.GetEndPageIndex(this._commentPageSize, pageIndex));
            return this.PartialView("CommentList", model);
        }

        public ActionResult AjaxUnFavAlbum(int AlbumId)
        {
            int num = new Maticsoft.BLL.SNS.UserFavAlbum().UnFavAlbum(AlbumId, base.currentUser.UserID);
            if (num > 0)
            {
                return base.Content(num.ToString());
            }
            return base.Content("Fail");
        }

        [OutputCache(VaryByParam="*", Duration=300)]
        public PartialViewResult AlbumPart(int AlbumType)
        {
            List<AlbumIndex> model = new Maticsoft.BLL.SNS.UserAlbums().GetListForIndex(AlbumType, 8, 2, base.UserAlbumDetailType);
            return base.PartialView(model);
        }

        public ActionResult Create()
        {
            Action<Maticsoft.Model.SNS.AlbumType> action = null;
            ((dynamic) base.ViewBag).Title = "创建新专辑";
            CreateAlbum createAlbum = new CreateAlbum();
            List<Maticsoft.Model.SNS.AlbumType> modelListByCache = new Maticsoft.BLL.SNS.AlbumType().GetModelListByCache(Maticsoft.Model.SNS.EnumHelper.Status.Enabled);
            createAlbum.TypeList = new List<SelectListItem>();
            if (modelListByCache != null)
            {
                if (action == null)
                {
                    action = delegate (Maticsoft.Model.SNS.AlbumType xx) {
                        SelectListItem item = new SelectListItem {
                            Text = xx.TypeName,
                            Value = xx.ID.ToString(),
                            Selected = false
                        };
                        createAlbum.TypeList.Add(item);
                    };
                }
                modelListByCache.ForEach(action);
            }
            return base.View(createAlbum);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ((dynamic) base.ViewBag).Title = "创建新专辑";
            try
            {
                if (!base.HttpContext.User.Identity.IsAuthenticated)
                {
                    return base.RedirectToAction("Login", "Account");
                }
                if (base.currentUser.UserType == "AA")
                {
                    return base.View();
                }
                Maticsoft.Model.SNS.UserAlbums model = new Maticsoft.Model.SNS.UserAlbums {
                    AlbumName = collection["AlbumName"],
                    Description = collection["Description"],
                    CreatedNickName = base.CurrentUser.NickName,
                    CreatedUserID = base.CurrentUser.UserID
                };
                int typeId = Globals.SafeInt(collection["TypeRadio"], 0);
                int num2 = new Maticsoft.BLL.SNS.UserAlbums().AddEx(model, typeId);
                if (num2 > 0)
                {
                    Maticsoft.BLL.SNS.UserAlbumsType type = new Maticsoft.BLL.SNS.UserAlbumsType();
                    Maticsoft.Model.SNS.UserAlbumsType type2 = new Maticsoft.Model.SNS.UserAlbumsType {
                        AlbumsID = num2,
                        AlbumsUserID = new int?(model.CreatedUserID),
                        TypeID = typeId
                    };
                    type.Add(type2);
                }
                return base.RedirectToAction("Albums", "Profile");
            }
            catch
            {
                return base.View();
            }
        }

        public ActionResult Details(int AlbumID, int? pageIndex)
        {
            PhotoList model = new PhotoList {
                AlbumModel = this.bllAlbums.GetModel(AlbumID)
            };
            if ((model.AlbumModel == null) || !this.bllAlbums.UpdatePvCount(AlbumID))
            {
                return base.RedirectToAction("Index");
            }
            model.UserModel = this.blluser.GetUsersExpModelByCache(model.AlbumModel.CreatedUserID);
            if (model.UserModel == null)
            {
                return base.RedirectToAction("Index");
            }
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            IPageSetting pageSetting = PageSetting.GetPageSetting("AblumDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(new string[][] { new string[] { "{cname}", model.AlbumModel.AlbumName }, new string[] { "{ctname}", model.UserModel.NickName }, new string[] { "{cdes}", model.AlbumModel.Description } });
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int num4 = 0;
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            num4 = detail.GetRecordCount4AlbumImgByAlbumID(AlbumID, base.UserAlbumDetailType);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > num4) ? num4 : num5;
            model.AlbumsList = this.bllAlbums.GetUserAlbumsByUserId(10, (model.UserModel != null) ? model.UserModel.UserID : 0);
            if (num4 >= 1)
            {
                int? nullable = pageIndex;
                model.PhotoPagedList = detail.GetAlbumImgListByPage(AlbumID, startIndex, endIndex, base.UserAlbumDetailType).ToPagedList<PostContent>(nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, new int?(num4));
                string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                foreach (PostContent content in model.PhotoPagedList)
                {
                    if (valueByCache != "true")
                    {
                        content.StaticUrl = (string) (((((((dynamic) base.ViewBag).BasePath + ((content.Type == 1) ? "Product/" : "Photo/")) + "Detail/") + content.TargetId) + "?AlbumId=") + AlbumID);
                        continue;
                    }
                    string str2 = "";
                    if (content.Type == 1)
                    {
                        Maticsoft.Model.SNS.Products modelByCache = this.productBll.GetModelByCache(content.TargetId);
                        if ((modelByCache == null) || string.IsNullOrWhiteSpace(modelByCache.StaticUrl))
                        {
                        }
                        str2 = (<Details>o__SiteContainere.<>p__Site1f != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <Details>o__SiteContainere.<>p__Site1f.Target(<Details>o__SiteContainere.<>p__Site1f, base.ViewBag)) + "Product/Detail/") + content.TargetId));
                    }
                    else
                    {
                        Maticsoft.Model.SNS.Photos photos = this.photoBll.GetModelByCache((int) content.TargetId);
                        if ((photos == null) || string.IsNullOrWhiteSpace(photos.StaticUrl))
                        {
                        }
                        str2 = (<Details>o__SiteContainere.<>p__Site25 != null) ? photos.StaticUrl : ((string) ((((((dynamic) <Details>o__SiteContainere.<>p__Site25.Target(<Details>o__SiteContainere.<>p__Site25, base.ViewBag)) + "Photo/Detail/") + content.TargetId) + "?AlbumId=") + AlbumID));
                    }
                    content.StaticUrl = str2;
                }
                if (base.Request.IsAjaxRequest())
                {
                    return this.PartialView("PhotoList", model);
                }
                model.CommentPageSize = this._commentPageSize;
                model.CommentCount = new Maticsoft.BLL.SNS.Comments().GetCommentCount(3, AlbumID);
                if (((base.currentUser != null) && (model.AlbumModel != null)) && (model.AlbumModel.CreatedUserID == base.currentUser.UserID))
                {
                    ((dynamic) base.ViewBag).IsCurrentUser = true;
                }
            }
            return base.View(model);
        }

        public ActionResult Edit(int id)
        {
            return base.View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return base.RedirectToAction("Index");
            }
            catch
            {
                return base.View();
            }
        }

        public ActionResult GetUserAlbums()
        {
            List<Maticsoft.Model.SNS.UserAlbums> userAblumsByUserID = this.bllAlbums.GetUserAblumsByUserID(base.currentUser.UserID);
            return base.Json(userAblumsByUserID);
        }

        public ActionResult Index()
        {
            List<Maticsoft.Model.SNS.AlbumType> indexList = new Maticsoft.BLL.SNS.AlbumType().GetIndexList();
            IPageSetting pageSetting = PageSetting.GetPageSetting("Ablum", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(indexList);
        }

        [HttpPost]
        public ActionResult PhotoListWaterfall(int albumID, int startIndex)
        {
            PhotoList model = new PhotoList();
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            if (detail.GetRecordCount4AlbumImgByAlbumID(albumID, base.UserAlbumDetailType) < 1)
            {
                return new EmptyResult();
            }
            model.PhotoListWaterfall = detail.GetAlbumImgListByPage(albumID, startIndex, endIndex, base.UserAlbumDetailType);
            model.AlbumModel = this.bllAlbums.GetModel(albumID);
            if (((base.currentUser != null) && (model.AlbumModel != null)) && (model.AlbumModel.CreatedUserID == base.currentUser.UserID))
            {
                ((dynamic) base.ViewBag).IsCurrentUser = true;
            }
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (PostContent content in model.PhotoListWaterfall)
            {
                if (valueByCache != "true")
                {
                    content.StaticUrl = (string) (((((((dynamic) base.ViewBag).BasePath + ((content.Type == 1) ? "Product/" : "Photo/")) + "Detail/") + content.TargetId) + "?AlbumId=") + albumID);
                    continue;
                }
                string str2 = "";
                if (content.Type == 1)
                {
                    Maticsoft.Model.SNS.Products modelByCache = this.productBll.GetModelByCache(content.TargetId);
                    if ((modelByCache == null) || string.IsNullOrWhiteSpace(modelByCache.StaticUrl))
                    {
                    }
                    str2 = (<PhotoListWaterfall>o__SiteContainer27.<>p__Site34 != null) ? modelByCache.StaticUrl : ((string) ((((dynamic) <PhotoListWaterfall>o__SiteContainer27.<>p__Site34.Target(<PhotoListWaterfall>o__SiteContainer27.<>p__Site34, base.ViewBag)) + "Product/Detail/") + content.TargetId));
                }
                else
                {
                    Maticsoft.Model.SNS.Photos photos = this.photoBll.GetModelByCache((int) content.TargetId);
                    if ((photos == null) || string.IsNullOrWhiteSpace(photos.StaticUrl))
                    {
                    }
                    str2 = (<PhotoListWaterfall>o__SiteContainer27.<>p__Site3a != null) ? photos.StaticUrl : ((string) ((((((dynamic) <PhotoListWaterfall>o__SiteContainer27.<>p__Site3a.Target(<PhotoListWaterfall>o__SiteContainer27.<>p__Site3a, base.ViewBag)) + "Photo/Detail/") + content.TargetId) + "?AlbumId=") + albumID));
                }
                content.StaticUrl = str2;
            }
            return base.View(base.CurrentThemeViewPath + "/Photo/PhotoListWaterfall.cshtml", model);
        }

        public ActionResult TypeIndex(int AlbumType, int? pageIndex)
        {
            Maticsoft.Model.SNS.AlbumType model = new Maticsoft.BLL.SNS.AlbumType().GetModel(AlbumType);
            if (model == null)
            {
                return base.RedirectToAction("Index");
            }
            ((dynamic) base.ViewBag).TypeName = model.TypeName;
            int pageSize = this._basePageSize + this._waterfallSize;
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            IPageSetting pageSetting = PageSetting.GetPageSetting("AblumList", ApplicationKeyType.SNS);
            pageSetting.Replace(new string[][] { new string[] { "{cname}", model.TypeName } });
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            pageIndex = new int?((pageIndex.HasValue && (pageIndex.Value > 1)) ? pageIndex.Value : 1);
            int startIndex = (pageIndex.Value > 1) ? (((pageIndex.Value - 1) * pageSize) + 1) : 0;
            int endIndex = (pageIndex.Value > 1) ? ((startIndex + this._basePageSize) - 1) : this._basePageSize;
            int recordCount = 0;
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            recordCount = albums.GetRecordCount(AlbumType);
            ((dynamic) base.ViewBag).CurrentPageAjaxStartIndex = endIndex;
            int num5 = pageIndex.Value * pageSize;
            ((dynamic) base.ViewBag).CurrentPageAjaxEndIndex = (num5 > recordCount) ? recordCount : num5;
            if (recordCount < 1)
            {
                return base.View();
            }
            int? nullable = pageIndex;
            PagedList<AlbumIndex> list = albums.GetListForPage(AlbumType, "", startIndex, endIndex, base.UserAlbumDetailType).ToPagedList<AlbumIndex>(nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, new int?(recordCount));
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView("AlbumList", list);
            }
            return base.View(list);
        }

        [HttpPost]
        public ActionResult WaterfallAlbumListData(int AlbumType, int startIndex)
        {
            ((dynamic) base.ViewBag).BasePageSize = this._basePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            if (albums.GetRecordCount(AlbumType) < 1)
            {
                return new EmptyResult();
            }
            List<AlbumIndex> model = albums.GetListForPage(AlbumType, "", startIndex, endIndex, base.UserAlbumDetailType);
            return base.View("AlbumListWaterfall", model);
        }
    }
}


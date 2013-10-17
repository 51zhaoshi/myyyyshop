namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Json;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.ViewModel.UserCenter;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;

    public class PartialController : SNSControllerBase
    {
        protected Maticsoft.BLL.SNS.UserShip bllUserShip = new Maticsoft.BLL.SNS.UserShip();
        private Maticsoft.BLL.SNS.Categories cateBll = new Maticsoft.BLL.SNS.Categories();
        private Maticsoft.BLL.SNS.Comments ComBll = new Maticsoft.BLL.SNS.Comments();
        private int commentPagesize = 3;
        protected List<Maticsoft.ViewModel.SNS.Posts> list = new List<Maticsoft.ViewModel.SNS.Posts>();
        private Maticsoft.BLL.SNS.Posts PostsBll = new Maticsoft.BLL.SNS.Posts();

        public PartialController()
        {
            base.ValidateRequest = false;
        }

        [ValidateInput(false), HttpPost]
        public void AddAttention(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                base.RedirectToAction("Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                string str = collection["PassiveUserID"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (PageValidate.IsNumberSign(str) && this.bllUserShip.AddAttention(base.CurrentUser.UserID, int.Parse(str)))
                    {
                        obj2.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "FAIL");
                    }
                }
                else
                {
                    obj2.Accumulate("STATUS", "FAIL");
                }
                base.Response.Write(obj2.ToString());
            }
        }

        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            int num3;
            if (base.currentUser == null)
            {
                return null;
            }
            Maticsoft.Model.SNS.Comments comModel = new Maticsoft.Model.SNS.Comments();
            int num = Globals.SafeInt(Fm["TargetId"], 0);
            int num2 = 0;
            string str2 = Fm["Type"];
            if (str2 != null)
            {
                if (!(str2 == "Video"))
                {
                    if (str2 == "Product")
                    {
                        num2 = 2;
                        goto Label_0080;
                    }
                    if (str2 == "Photo")
                    {
                        num2 = 1;
                        goto Label_0080;
                    }
                    if (str2 == "Blog")
                    {
                        num2 = 4;
                        goto Label_0080;
                    }
                }
                else
                {
                    num2 = 0;
                    goto Label_0080;
                }
            }
            num2 = 0;
        Label_0080:
            num3 = 0;
            string str = ViewModelBase.ReplaceFace(Globals.HtmlEncode(Fm["Des"]));
            comModel.CreatedDate = DateTime.Now;
            comModel.CreatedNickName = base.currentUser.NickName;
            comModel.CreatedUserID = base.currentUser.UserID;
            comModel.Description = str;
            comModel.HasReferUser = str.Contains("@");
            comModel.IsRead = false;
            comModel.ReplyCount = 0;
            comModel.TargetId = num;
            comModel.Type = num2;
            comModel.UserIP = base.Request.UserHostAddress;
            num3 = this.ComBll.AddEx(comModel);
            if (num3 <= 0)
            {
                return base.Content("No");
            }
            comModel.CommentID = num3;
            List<Maticsoft.Model.SNS.Comments> model = new List<Maticsoft.Model.SNS.Comments>();
            if (!FilterWords.ContainsModWords(comModel.Description))
            {
                model.Add(comModel);
            }
            return this.PartialView("_TargetComment", model);
        }

        public ActionResult AjaxFellow(FormCollection Fm)
        {
            int fellowUserId = Globals.SafeInt(Fm["FellowUserId"], 0);
            if (((fellowUserId != 0) && (base.currentUser != null)) && this.bllUserShip.FellowUser(base.currentUser.UserID, fellowUserId))
            {
                return base.Content("Ok");
            }
            return base.Content("No");
        }

        public ActionResult AjaxGetCommentsByType(string type, int pid, int? PageIndex)
        {
            List<Maticsoft.Model.SNS.Comments> list;
            int startPageIndex = ViewModelBase.GetStartPageIndex(this.commentPagesize, PageIndex.Value);
            int endPageIndex = ViewModelBase.GetEndPageIndex(this.commentPagesize, PageIndex.Value);
            int num3 = 0;
            string str = type;
            if (str != null)
            {
                if (!(str == "Video"))
                {
                    if (str == "Product")
                    {
                        num3 = 2;
                        goto Label_007A;
                    }
                    if (str == "Photo")
                    {
                        num3 = 1;
                        goto Label_007A;
                    }
                    if (str == "Blog")
                    {
                        num3 = 4;
                        goto Label_007A;
                    }
                }
                else
                {
                    num3 = 0;
                    goto Label_007A;
                }
            }
            num3 = 0;
        Label_007A:
            list = this.ComBll.GetCommentByPage(num3, pid, startPageIndex, endPageIndex);
            return this.PartialView("_TargetComment", list);
        }

        public ActionResult AjaxGetCommentsCount(string type, int pid)
        {
            int num2;
            int num = 0;
            string str = type;
            if (str != null)
            {
                if (!(str == "Video"))
                {
                    if (str == "Product")
                    {
                        num = 2;
                        goto Label_004F;
                    }
                    if (str == "Photo")
                    {
                        num = 1;
                        goto Label_004F;
                    }
                    if (str == "Blog")
                    {
                        num = 4;
                        goto Label_004F;
                    }
                }
                else
                {
                    num = 0;
                    goto Label_004F;
                }
            }
            num = 0;
        Label_004F:
            num2 = this.ComBll.GetCommentCount(num, pid);
            return base.Content(num2.ToString());
        }

        public ActionResult AjaxGetPostByIndex(FormCollection Fm)
        {
            int num2;
            Maticsoft.Model.SNS.EnumHelper.PostType all = Maticsoft.Model.SNS.EnumHelper.PostType.All;
            string str = Fm["type"];
            int userId = Globals.SafeInt(Fm["UserID"], 0);
            if (!string.IsNullOrEmpty(str))
            {
                string str3 = str;
                if (str3 == null)
                {
                    goto Label_0066;
                }
                if (!(str3 == "user"))
                {
                    if (str3 == "fellow")
                    {
                        all = Maticsoft.Model.SNS.EnumHelper.PostType.Fellow;
                        goto Label_0068;
                    }
                    if (str3 == "referme")
                    {
                        all = Maticsoft.Model.SNS.EnumHelper.PostType.ReferMe;
                        goto Label_0068;
                    }
                    goto Label_0066;
                }
                all = Maticsoft.Model.SNS.EnumHelper.PostType.User;
            }
            goto Label_0068;
        Label_0066:
            all = Maticsoft.Model.SNS.EnumHelper.PostType.All;
        Label_0068:
            num2 = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PostPageSize"), 10);
            int pageIndex = Globals.SafeInt(Fm["pageIndex"], 0);
            this.list = this.PostsBll.GetPostByType(userId, ViewModelBase.GetStartPageIndex(num2, pageIndex), ViewModelBase.GetEndPageIndex(num2, pageIndex), all, base.IncludeProduct);
            return this.PartialView("LoadPostData", this.list);
        }

        public ActionResult AjaxLogin()
        {
            return base.View();
        }

        public ActionResult AjaxUnFellow(FormCollection Fm)
        {
            int fellowUserId = Globals.SafeInt(Fm["UnFellowUserId"], 0);
            if (((fellowUserId != 0) && (base.currentUser != null)) && this.bllUserShip.UnFellowUser(base.currentUser.UserID, fellowUserId))
            {
                return base.Content("Ok");
            }
            return base.Content("No");
        }

        public ActionResult AjaxUserInfo(int? UserID, string NickName)
        {
            Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
            UsersExp exp = new UsersExp();
            UsersExpModel usersModel = new UsersExpModel();
            if (!string.IsNullOrEmpty(NickName))
            {
                int userIdByNickName = users.GetUserIdByNickName(NickName);
                if (userIdByNickName <= 0)
                {
                    return base.View("_AjaxUserInfo", usersModel);
                }
                UserID = new int?(userIdByNickName);
            }
            if (UserID.HasValue)
            {
                usersModel = exp.GetUsersModel(UserID.Value);
                string regionNameByRID = new Maticsoft.BLL.Ms.Regions().GetRegionNameByRID(Globals.SafeInt(usersModel.Address, 0));
                if (regionNameByRID.Contains("北京北京"))
                {
                    regionNameByRID = regionNameByRID.Replace("北京北京", "北京");
                }
                else if (regionNameByRID.Contains("上海上海"))
                {
                    regionNameByRID = regionNameByRID.Replace("上海上海", "上海");
                }
                else if (regionNameByRID.Contains("重庆重庆"))
                {
                    regionNameByRID = regionNameByRID.Replace("重庆重庆", "重庆");
                }
                else if (regionNameByRID.Contains("天津天津"))
                {
                    regionNameByRID = regionNameByRID.Replace("天津天津", "天津");
                }
                usersModel.Address = string.IsNullOrEmpty(usersModel.Address) ? "暂未设置" : regionNameByRID;
            }
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            if (base.currentUser != null)
            {
                if (base.currentUser.UserID == UserID.Value)
                {
                    ((dynamic) base.ViewBag).IsSelf = true;
                }
                else if (ship.Exists(base.currentUser.UserID, UserID.Value))
                {
                    ((dynamic) base.ViewBag).IsFellow = true;
                }
            }
            return base.View("_AjaxUserInfo", usersModel);
        }

        public ActionResult ArticleList()
        {
            int classID = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSHelpCenter"), 1);
            List<Content> modelList = new Content().GetModelList(classID);
            return base.View(modelList);
        }

        public ActionResult BaiduShare()
        {
            ((dynamic) base.ViewBag).BaiduUid = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("BaiduShareUserId");
            return base.View("_BaiduShare");
        }

        [HttpPost, ValidateInput(false)]
        public void CancelAttention(FormCollection collection)
        {
            if (!base.HttpContext.User.Identity.IsAuthenticated || (base.CurrentUser == null))
            {
                base.RedirectToAction("Login");
            }
            else
            {
                JsonObject obj2 = new JsonObject();
                string str = collection["PassiveUserID"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (PageValidate.IsNumberSign(str) && this.bllUserShip.CancelAttention(base.CurrentUser.UserID, int.Parse(str)))
                    {
                        obj2.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "FAIL");
                    }
                }
                else
                {
                    obj2.Accumulate("STATUS", "FAIL");
                }
                base.Response.Write(obj2.ToString());
            }
        }

        public PartialViewResult CategoryList(int Cid = 0, int Top = 5, int Type = 0, string ViewName = "_SonList")
        {
            List<Maticsoft.Model.SNS.Categories> model = this.cateBll.GetChildList(Cid, Top, Type);
            return this.PartialView(ViewName, model);
        }

        public ActionResult ContainsDisWords()
        {
            string str = base.Request.Params["Desc"];
            if (!FilterWords.ContainsDisWords(str))
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

        public ViewResult FLink(int FLinkType = 1, int FLinkTop = 8, string ViewName = "_FLink")
        {
            List<FriendlyLink> modelList = new FriendlyLink().GetModelList(FLinkTop, FLinkType);
            return base.View(ViewName, modelList);
        }

        public PartialViewResult Footer()
        {
            return base.PartialView("_Footer");
        }

        public ActionResult GetCurrentUser()
        {
            if (base.currentUser == null)
            {
                return base.Content("No");
            }
            Maticsoft.BLL.Members.SiteMessage message = new Maticsoft.BLL.Members.SiteMessage();
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            int num = message.GetSystemMsgNotReadCount(base.currentUser.UserID, -1, base.currentUser.UserType);
            int receiveMsgNotReadCount = message.GetReceiveMsgNotReadCount(base.currentUser.UserID, -1);
            int referNotReadCountByType = users.GetReferNotReadCountByType(base.currentUser.UserID, 0);
            string str = string.IsNullOrWhiteSpace(base.currentUser.NickName) ? base.currentUser.UserName : base.currentUser.NickName;
            return base.Content(string.Concat(new object[] { str, "|", base.currentUser.UserID, "|", num, "|", receiveMsgNotReadCount, "|", referNotReadCountByType }));
        }

        public UsersExpModel GetUserModel(int UserID)
        {
            UsersExp exp = new UsersExp();
            UsersExpModel model = new UsersExpModel();
            return exp.GetUsersModel(UserID);
        }

        public ActionResult Header()
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Logo = set.LogoPath;
            ((dynamic) base.ViewBag).WebName = set.WebName;
            ((dynamic) base.ViewBag).Domain = set.WebSite_Domain;
            if (base.currentUser == null)
            {
                return base.View("_Header");
            }
            int num = 0;
            List<MsgTip> model = new List<MsgTip>();
            Maticsoft.BLL.Members.SiteMessage message = new Maticsoft.BLL.Members.SiteMessage();
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            num = message.GetSystemMsgNotReadCount(base.currentUser.UserID, -1, base.currentUser.UserType);
            if (num > 0)
            {
                MsgTip item = new MsgTip {
                    Count = num,
                    _MsgType = 0
                };
                model.Add(item);
            }
            int receiveMsgNotReadCount = 0;
            receiveMsgNotReadCount = message.GetReceiveMsgNotReadCount(base.currentUser.UserID, -1);
            if (receiveMsgNotReadCount > 0)
            {
                MsgTip tip2 = new MsgTip {
                    Count = receiveMsgNotReadCount,
                    _MsgType = 1
                };
                model.Add(tip2);
            }
            int referNotReadCountByType = 0;
            referNotReadCountByType = users.GetReferNotReadCountByType(base.currentUser.UserID, 0);
            if (referNotReadCountByType > 0)
            {
                MsgTip tip3 = new MsgTip {
                    Count = referNotReadCountByType,
                    _MsgType = 2
                };
                model.Add(tip3);
            }
            ((dynamic) base.ViewBag).Current = base.currentUser;
            ((dynamic) base.ViewBag).Pointer = Globals.SafeInt(base.Request.QueryString["pointer"], 0);
            return base.View("_UserHeader", model);
        }

        public ActionResult HotCity()
        {
            List<Maticsoft.Model.Ms.RegionRec> recCityList = new Maticsoft.BLL.Ms.RegionRec().GetRecCityList(0);
            return this.PartialView("_HotCity", recCityList);
        }

        public PartialViewResult ImageAd(int id = 0x17, string ViewName = "_ImageAd1")
        {
            List<Advertisement> listByAidCache = new Advertisement().GetListByAidCache(id);
            return this.PartialView(ViewName, listByAidCache);
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public PartialViewResult IndexAd(int id = 0x16, string ViewName = "_IndexAd")
        {
            List<Advertisement> listByAidCache = new Advertisement().GetListByAidCache(id);
            return this.PartialView(ViewName, listByAidCache);
        }

        public ActionResult LeftMenu()
        {
            if (base.CurrentUser == null)
            {
                return (ActionResult) this.RedirectToAction(((dynamic) base.ViewBag).BasePath + "Account/Login");
            }
            ((dynamic) base.ViewBag).UserInfo = base.CurrentUser;
            return base.View();
        }

        public PartialViewResult MediaLogin(string ViewName = "_MediaLogin")
        {
            return base.PartialView(ViewName);
        }

        public PartialViewResult Navigation()
        {
            List<MainMenus> menusByArea = new MainMenus().GetMenusByArea(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, MvcApplication.ThemeName);
            return this.PartialView("_Navigation", menusByArea);
        }

        public PartialViewResult PhotoPart(int Top = 5, string viewName = "_PhotoPart")
        {
            List<Maticsoft.Model.SNS.Photos> topPhotoList = new Maticsoft.BLL.SNS.Photos().GetTopPhotoList(Top, -1);
            return this.PartialView(viewName, topPhotoList);
        }

        public PartialViewResult ProfileLeft(int? uid)
        {
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            UsersExp exp = new UsersExp();
            UsersExpModel usersExpModel = new UsersExpModel();
            Maticsoft.BLL.SNS.Groups groups = new Maticsoft.BLL.SNS.Groups();
            int userid = uid.HasValue ? uid.Value : ((base.currentUser != null) ? base.currentUser.UserID : 0);
            List<Maticsoft.Model.SNS.UserShip> list = ship.GetToListByFansPage(userid, "", 0, 9);
            List<Maticsoft.Model.SNS.Groups> userJoinGroup = groups.GetUserJoinGroup(userid, 9);
            List<Maticsoft.Model.SNS.Groups> modelList = groups.GetModelList("CreatedUserId=" + userid);
            usersExpModel = exp.GetUsersExpModel(userid);
            ((dynamic) base.ViewBag).FansCount = (usersExpModel != null) ? usersExpModel.FansCount : 0;
            ((dynamic) base.ViewBag).IsCurrentUser = !uid.HasValue && (base.currentUser != null);
            ((dynamic) base.ViewBag).UserId = userid;
            Maticsoft.ViewModel.SNS.ProfileLeft model = new Maticsoft.ViewModel.SNS.ProfileLeft {
                joingroupList = userJoinGroup,
                shipList = list,
                creategroupList = modelList
            };
            return this.PartialView("_ProfileLeft", model);
        }

        public ActionResult Province()
        {
            List<Maticsoft.Model.Ms.Regions> provinceList = new Maticsoft.BLL.Ms.Regions().GetProvinceList();
            return this.PartialView("_Province", provinceList);
        }

        public ActionResult Search(string ViewName = "_Search")
        {
            WebSiteSet set = new WebSiteSet(ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Logo = set.LogoPath;
            return base.View(ViewName);
        }

        public PartialViewResult SelfRight()
        {
            new UsersExp();
            new UsersExpModel();
            Maticsoft.BLL.SNS.Groups groups = new Maticsoft.BLL.SNS.Groups();
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            Maticsoft.ViewModel.SNS.SelfRight model = new Maticsoft.ViewModel.SNS.SelfRight {
                MyGroups = groups.GetUserJoinGroup(base.currentUser.UserID, 9)
            };
            Maticsoft.BLL.Ms.Regions regions = new Maticsoft.BLL.Ms.Regions();
            new Maticsoft.BLL.SNS.Star();
            model.UserInfo = this.GetUserModel(base.currentUser.UserID);
            string regionNameByRID = regions.GetRegionNameByRID(Globals.SafeInt(model.UserInfo.Address, 0));
            if (regionNameByRID.Contains("北京北京"))
            {
                regionNameByRID = regionNameByRID.Replace("北京北京", "北京");
            }
            else if (regionNameByRID.Contains("上海上海"))
            {
                regionNameByRID = regionNameByRID.Replace("上海上海", "上海");
            }
            else if (regionNameByRID.Contains("重庆重庆"))
            {
                regionNameByRID = regionNameByRID.Replace("重庆重庆", "重庆");
            }
            else if (regionNameByRID.Contains("天津天津"))
            {
                regionNameByRID = regionNameByRID.Replace("天津天津", "天津");
            }
            model.UserInfo.Address = string.IsNullOrEmpty(model.UserInfo.Address) ? "暂未设置" : regionNameByRID;
            new Maticsoft.BLL.SNS.AlbumType();
            model.MyAlbum = albums.GetListByUserId(base.currentUser.UserID, base.UserAlbumDetailType);
            return this.PartialView("_SelfRight", model);
        }

        public ActionResult StarDPI(int? UserId)
        {
            if (UserId.HasValue)
            {
                UsersExpModel usersExpModelByCache = new UsersExp().GetUsersExpModelByCache(UserId.Value);
                if ((usersExpModelByCache != null) && usersExpModelByCache.IsUserDPI)
                {
                    ((dynamic) base.ViewBag).IsUserDPI = true;
                }
                else
                {
                    ((dynamic) base.ViewBag).IsUserDPI = false;
                }
            }
            else
            {
                ((dynamic) base.ViewBag).IsUserDPI = false;
            }
            return base.View("_StarDPI");
        }

        public PartialViewResult UserInfo(int uid = -1, string nickname = "")
        {
            int num;
            Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
            UsersExpModel userModel = new UsersExpModel();
            Maticsoft.BLL.Ms.Regions regions = new Maticsoft.BLL.Ms.Regions();
            Maticsoft.BLL.SNS.Star star = new Maticsoft.BLL.SNS.Star();
            if (!string.IsNullOrEmpty(nickname) && ((num = users.GetUserIdByNickName(nickname)) > 0))
            {
                uid = num;
            }
            ((dynamic) base.ViewBag).IsPost = uid == -1;
            ((dynamic) base.ViewBag).IsCurrentUser = false;
            uid = (uid > -1) ? uid : base.currentUser.UserID;
            if ((base.currentUser != null) && (uid == base.currentUser.UserID))
            {
                ((dynamic) base.ViewBag).IsCurrentUser = true;
            }
            userModel = this.GetUserModel(uid);
            string regionNameByRID = regions.GetRegionNameByRID(Globals.SafeInt(userModel.Address, 0));
            if (regionNameByRID.Contains("北京北京"))
            {
                regionNameByRID = regionNameByRID.Replace("北京北京", "北京");
            }
            else if (regionNameByRID.Contains("上海上海"))
            {
                regionNameByRID = regionNameByRID.Replace("上海上海", "上海");
            }
            else if (regionNameByRID.Contains("重庆重庆"))
            {
                regionNameByRID = regionNameByRID.Replace("重庆重庆", "重庆");
            }
            else if (regionNameByRID.Contains("天津天津"))
            {
                regionNameByRID = regionNameByRID.Replace("天津天津", "天津");
            }
            userModel.Address = string.IsNullOrEmpty(userModel.Address) ? "暂未设置" : regionNameByRID;
            ((dynamic) base.ViewBag).IsStar = star.IsStar(uid);
            ((dynamic) base.ViewBag).Level = new Maticsoft.BLL.SNS.GradeConfig().GetUserLevel(userModel.Points);
            return this.PartialView("_UserInfo", userModel);
        }

        public ActionResult UserRecommand()
        {
            return base.View();
        }

        public ActionResult WeiBoBind()
        {
            Maticsoft.BLL.Members.UserBind bind = new Maticsoft.BLL.Members.UserBind();
            if (base.currentUser != null)
            {
                UserBindList listEx = bind.GetListEx(base.currentUser.UserID);
                return base.View("_WeiBoBind", listEx);
            }
            return new EmptyResult();
        }
    }
}


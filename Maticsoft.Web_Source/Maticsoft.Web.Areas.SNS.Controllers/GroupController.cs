namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components.Filters;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Areas.SNS;
    using Maticsoft.Web.Components;
    using Maticsoft.Web.Components.Setting.SNS;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web.Mvc;
    using System.Web.UI;
    using Webdiyer.WebControls.Mvc;

    public class GroupController : SNSControllerBase
    {
        private Maticsoft.BLL.SNS.Groups bllGroups = new Maticsoft.BLL.SNS.Groups();
        private Maticsoft.BLL.SNS.GroupTags bllGroupTags = new Maticsoft.BLL.SNS.GroupTags();
        private Maticsoft.BLL.SNS.GroupUsers bllGroupUser = new Maticsoft.BLL.SNS.GroupUsers();
        private Maticsoft.BLL.SNS.GroupTopicReply bllReply = new Maticsoft.BLL.SNS.GroupTopicReply();
        private Maticsoft.BLL.SNS.GroupTopics bllTopic = new Maticsoft.BLL.SNS.GroupTopics();
        private const int HotGroupCount = 6;
        private const int TopGroupCount = 6;

        public PartialViewResult AdminRecTopic(int Top = -1, string ViewName = "_AdminRecTopic")
        {
            List<Maticsoft.Model.SNS.GroupTopics> model = this.bllTopic.GetList4Model(Top, "Status=1 and IsAdminRecommend=1", "TopicID desc");
            return this.PartialView(ViewName, model);
        }

        public ActionResult AjaxUserRoleUpdate(FormCollection fm)
        {
            int groupID = Globals.SafeInt(fm["GroupID"], 0);
            int userID = Globals.SafeInt(fm["UserId"], 0);
            int role = Globals.SafeInt(fm["Role"], 0);
            if (!this.bllGroupUser.UpdateRole(groupID, userID, role))
            {
                return base.Content("Fail");
            }
            return base.Content("Success");
        }

        [TokenAuthorize]
        public ActionResult Create()
        {
            RegisterGroup model = new RegisterGroup();
            List<Maticsoft.Model.SNS.GroupTags> modelList = this.bllGroupTags.GetModelList("Status = 1");
            if (modelList == null)
            {
            }
            model.TagList = (CS$<>9__CachedAnonymousMethodDelegate8 != null) ? "" : string.Join(",", modelList.Select<Maticsoft.Model.SNS.GroupTags, string>(CS$<>9__CachedAnonymousMethodDelegate8));
            ((dynamic) base.ViewBag).Title = "申请注册小组";
            return base.View(model);
        }

        [TokenAuthorize, HttpPost]
        public ActionResult Create(RegisterGroup model)
        {
            ((dynamic) base.ViewBag).Title = "申请注册小组";
            if (!base.ModelState.IsValid)
            {
                List<Maticsoft.Model.SNS.GroupTags> modelList = this.bllGroupTags.GetModelList("Status = 1");
                if (modelList == null)
                {
                }
                model.TagList = (CS$<>9__CachedAnonymousMethodDelegated != null) ? "" : string.Join(",", modelList.Select<Maticsoft.Model.SNS.GroupTags, string>(CS$<>9__CachedAnonymousMethodDelegated));
                return base.View(model);
            }
            Maticsoft.Model.SNS.Groups groups = new Maticsoft.Model.SNS.Groups {
                GroupName = model.GroupName,
                GroupDescription = model.GroupDescription,
                GroupUserCount = 1,
                CreatedUserId = base.CurrentUser.UserID,
                CreatedNickName = base.CurrentUser.NickName,
                CreatedDate = DateTime.Now,
                Tags = model.Tags
            };
            if (!string.IsNullOrWhiteSpace(model.GroupLogo))
            {
                string path = string.Format(model.GroupLogo, "");
                string str2 = base.HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroupThumb);
                string str3 = base.HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroup);
                try
                {
                    if (Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
                    {
                        groups.GroupLogoThumb = model.GroupLogo;
                        groups.GroupLogo = model.GroupLogo;
                    }
                    else
                    {
                        if (!Directory.Exists(str2))
                        {
                            Directory.CreateDirectory(str2);
                        }
                        if (!Directory.Exists(str3))
                        {
                            Directory.CreateDirectory(str3);
                        }
                        List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, "");
                        if ((thumSizeList != null) && (thumSizeList.Count > 0))
                        {
                            string str4 = "";
                            foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                            {
                                str4 = string.Format(model.GroupLogo, size.ThumName);
                                if (File.Exists(base.Server.MapPath(str4)))
                                {
                                    FileInfo info = new FileInfo(base.HttpContext.Server.MapPath(str4));
                                    info.MoveTo(str2 + info.Name);
                                }
                            }
                        }
                        FileInfo info2 = new FileInfo(base.HttpContext.Server.MapPath(path));
                        info2.MoveTo(str3 + info2.Name);
                        groups.GroupLogoThumb = SNSAreaRegistration.PathUploadImgGroupThumb + "{0}" + info2.Name;
                        groups.GroupLogo = SNSAreaRegistration.PathUploadImgGroup + info2.Name;
                    }
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("Message", "您上传的文件保存失败, 请重新上传!");
                    List<Maticsoft.Model.SNS.GroupTags> source = this.bllGroupTags.GetModelList("Status = 1");
                    if (source == null)
                    {
                    }
                    model.TagList = (CS$<>9__CachedAnonymousMethodDelegatee != null) ? "" : string.Join(",", source.Select<Maticsoft.Model.SNS.GroupTags, string>(CS$<>9__CachedAnonymousMethodDelegatee));
                    return base.View(model);
                }
            }
            groups.Status = 1;
            groups.IsRecommand = 0;
            groups.GroupID = this.bllGroups.Add(groups);
            if (groups.GroupID > 0)
            {
                Maticsoft.Model.SNS.GroupUsers users = new Maticsoft.Model.SNS.GroupUsers {
                    GroupID = groups.GroupID,
                    JoinTime = DateTime.Now,
                    UserID = groups.CreatedUserId,
                    NickName = groups.CreatedNickName,
                    Role = 2,
                    Status = 1
                };
                this.bllGroupUser.Add(users);
                return base.RedirectToAction("GroupInfo", new { GroupId = groups.GroupID });
            }
            return base.View(model);
        }

        public ActionResult GroupInfo(int GroupId, int? page, string type, string q)
        {
            Maticsoft.ViewModel.SNS.GroupInfo model = new Maticsoft.ViewModel.SNS.GroupInfo {
                Group = this.bllGroups.GetModel(GroupId)
            };
            ((dynamic) base.ViewBag).GetType = type;
            ((dynamic) base.ViewBag).IsCreator = (base.CurrentUser != null) && (model.Group.CreatedUserId == base.CurrentUser.UserID);
            ((dynamic) base.ViewBag).toalcount = model.Group.TopicCount;
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int totalItemCount = 0;
            switch (type)
            {
                case "Search":
                {
                    q = InjectionFilter.Filter(q);
                    totalItemCount = this.bllTopic.GetCountByKeyWord(q, GroupId);
                    ((dynamic) base.ViewBag).q = q;
                    ((dynamic) base.ViewBag).toalcount = totalItemCount;
                    int? nullable = page;
                    model.TopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(this.bllTopic.SearchTopicByKeyWord(startIndex, endIndex, q, GroupId, ""), nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, totalItemCount);
                    if (base.Request.IsAjaxRequest())
                    {
                        return this.PartialView(base.CurrentThemeViewPath + "/Group/TopicList.cshtml", model.TopicList);
                    }
                    break;
                }
                case "User":
                {
                    model.AdminUserList = this.bllGroupUser.GetAdminUserList(GroupId);
                    if ((model.AdminUserList.Count > 0) && (model.AdminUserList[0].Role == 2))
                    {
                        model.AdminUserList[0].User = new UsersExp().GetUsersExpModelByCache(model.AdminUserList[0].UserID);
                    }
                    totalItemCount = this.bllGroupUser.GetRecordCount("GroupId=" + GroupId + " AND Role = 0");
                    int? nullable2 = page;
                    model.UserList = new PagedList<Maticsoft.Model.SNS.GroupUsers>(this.bllGroupUser.GetUserList(GroupId, startIndex, endIndex), nullable2.HasValue ? nullable2.GetValueOrDefault() : 1, pageSize, totalItemCount);
                    if (base.Request.IsAjaxRequest())
                    {
                        return this.PartialView(base.CurrentThemeViewPath + "/Group/UserList.cshtml", model);
                    }
                    break;
                }
                case "Recommand":
                {
                    totalItemCount = this.bllTopic.GetRecommandCount(GroupId);
                    int? nullable3 = page;
                    model.TopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(this.bllTopic.GetTopicListPageByGroup(GroupId, startIndex, endIndex, true), nullable3.HasValue ? nullable3.GetValueOrDefault() : 1, pageSize, totalItemCount);
                    if (base.Request.IsAjaxRequest())
                    {
                        return this.PartialView(base.CurrentThemeViewPath + "/Group/TopicList.cshtml", model.TopicList);
                    }
                    break;
                }
                default:
                {
                    totalItemCount = this.bllTopic.GetRecordCount("  Status=1 and GroupID=" + GroupId);
                    int? nullable4 = page;
                    model.TopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(this.bllTopic.GetTopicListPageByGroup(GroupId, startIndex, endIndex, false), nullable4.HasValue ? nullable4.GetValueOrDefault() : 1, pageSize, totalItemCount);
                    if (base.Request.IsAjaxRequest())
                    {
                        return this.PartialView(base.CurrentThemeViewPath + "/Group/TopicList.cshtml", model.TopicList);
                    }
                    break;
                }
            }
            model.NewTopicList = this.bllTopic.GetNewTopListByGroup(GroupId, 10);
            model.NewUserList = this.bllGroupUser.GetNewUserListByGroup(GroupId, 9);
            ((dynamic) base.ViewBag).IsJoin = (base.currentUser != null) && this.bllGroupUser.Exists(GroupId, base.currentUser.UserID);
            IPageSetting pageSetting = PageSetting.GetPageSetting("GroupList", ApplicationKeyType.SNS);
            pageSetting.Replace(new string[][] { new string[] { "{cname}", model.Group.GroupName }, new string[] { "{ctag}", model.Group.Tags }, new string[] { "{cdes}", model.Group.GroupDescription } });
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(base.CurrentThemeViewPath + "/Group/GroupInfo.cshtml", model);
        }

        public PartialViewResult HotGroupTopic(int Top = -1, string ViewName = "_HotGroupTopic")
        {
            List<Maticsoft.Model.SNS.GroupTopics> hotListByGroup = this.bllTopic.GetHotListByGroup(-1, Top);
            return this.PartialView(ViewName, hotListByGroup);
        }

        public ActionResult Index()
        {
            GroupIndex model = new GroupIndex {
                TopGroupList = this.bllGroups.GetTopList(6, "IsRecommand=1", "TopicCount desc"),
                ProGroupList = this.bllGroups.GetTopList(6, "IsRecommand=2", "TopicCount desc"),
                HotGroupList = this.bllGroups.GetTopList(6, "", "TopicCount desc")
            };
            if (base.CurrentUser != null)
            {
                model.MyGroupList = this.bllGroups.GetUserJoinGroup(base.CurrentUser.UserID, 10);
            }
            ((dynamic) base.ViewBag).GroupCount = this.bllGroups.GetRecordCount("").ToString().ToCharArray();
            model.NewGroupTopicList = this.bllTopic.GetList4Model(10, "Status=1", "TopicID desc");
            IPageSetting pageSetting = PageSetting.GetPageSetting("Group", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(model);
        }

        [OutputCache(Location=OutputCacheLocation.None, NoStore=true)]
        public JsonResult IsExistGroupName(string groupName)
        {
            bool data = !this.bllGroups.Exists(groupName);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Location=OutputCacheLocation.None, NoStore=true)]
        public JsonResult IsExistGroupName4Ignore(string groupName, int groupId)
        {
            bool data = !this.bllGroups.Exists4Ignore(groupName, groupId);
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult NewGroup(int Top = -1, string ViewName = "_NewGroup")
        {
            List<Maticsoft.Model.SNS.Groups> model = this.bllGroups.GetTopList(Top, "", "CreatedDate desc");
            return this.PartialView(ViewName, model);
        }

        public PartialViewResult NewGroupTopic(int Top = -1, string ViewName = "_NewGroupTopic")
        {
            List<Maticsoft.Model.SNS.GroupTopics> model = this.bllTopic.GetList4Model(Top, "Status=1", "TopicID desc");
            return this.PartialView(ViewName, model);
        }

        [TokenAuthorize]
        public ActionResult NewTopic(int GroupId)
        {
            ((dynamic) base.ViewBag).Title = "发表主题";
            Maticsoft.Model.SNS.Groups model = new Maticsoft.Model.SNS.Groups();
            model = this.bllGroups.GetModel(GroupId);
            return base.View(model);
        }

        public ActionResult TopicReply(int Id, int? page)
        {
            if (!this.bllTopic.Exists(Id) || !this.bllTopic.UpdatePVCount(Id))
            {
                return base.RedirectToAction("Index", "Group");
            }
            Maticsoft.ViewModel.SNS.TopicReply model = new Maticsoft.ViewModel.SNS.TopicReply {
                Topic = this.bllTopic.GetModel(Id)
            };
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int recordCount = this.bllReply.GetRecordCount(" Status=1 and TopicID =" + Id);
            List<Maticsoft.Model.SNS.GroupTopicReply> items = this.bllReply.GetTopicReplyByTopic(Id, startIndex, endIndex);
            if ((items != null) && (items.Count > 0))
            {
                int? nullable = page;
                model.TopicsReply = new PagedList<Maticsoft.Model.SNS.GroupTopicReply>(items, nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, recordCount);
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/Group/TopicReplyList.cshtml", model);
            }
            model.UserJoinGroups = this.bllGroups.GetUserJoinGroup((model.Topic != null) ? model.Topic.CreatedUserID : 0, 9);
            model.HotTopic = this.bllTopic.GetHotListByGroup(model.Topic.GroupID, 9);
            model.Group = this.bllGroups.GetModel(model.Topic.GroupID);
            model.UserPostTopics = this.bllTopic.GetTopicByUserId(model.Topic.CreatedUserID, 9);
            IPageSetting pageSetting = PageSetting.GetPageSetting("GroupDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(new string[][] { new string[] { "{cname}", model.Group.GroupName }, new string[] { "{ctname}", model.Topic.Title }, new string[] { "{ctag}", model.Topic.Tags }, new string[] { "{cdes}", model.Topic.Description } });
            ((dynamic) base.ViewBag).Title = pageSetting.Title;
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            return base.View(base.CurrentThemeViewPath + "/Group/TopicReply.cshtml", model);
        }

        [TokenAuthorize, HttpPost]
        public ActionResult Update(UpdateGroup model)
        {
            ((dynamic) base.ViewBag).Title = "编辑小组信息";
            if (!base.ModelState.IsValid)
            {
                List<Maticsoft.Model.SNS.GroupTags> modelList = this.bllGroupTags.GetModelList("Status = 1");
                if (modelList == null)
                {
                }
                model.TagList = (CS$<>9__CachedAnonymousMethodDelegate18 != null) ? "" : string.Join(",", modelList.Select<Maticsoft.Model.SNS.GroupTags, string>(CS$<>9__CachedAnonymousMethodDelegate18));
                return base.View(model);
            }
            Maticsoft.Model.SNS.Groups groups = this.bllGroups.GetModel(model.GroupId);
            if ((model.GroupName != groups.GroupName) && this.bllGroups.Exists4Ignore(model.GroupName, model.GroupId))
            {
                base.ModelState.AddModelError("Message", "小组名称已经被Ta人抢注, 换个试试");
                List<Maticsoft.Model.SNS.GroupTags> source = this.bllGroupTags.GetModelList("Status = 1");
                if (source == null)
                {
                }
                model.TagList = (CS$<>9__CachedAnonymousMethodDelegate19 != null) ? "" : string.Join(",", source.Select<Maticsoft.Model.SNS.GroupTags, string>(CS$<>9__CachedAnonymousMethodDelegate19));
                return base.View(model);
            }
            groups.GroupName = model.GroupName;
            groups.GroupDescription = model.GroupDescription;
            groups.Tags = string.Join(",", new string[] { model.Tags });
            if (!string.IsNullOrWhiteSpace(model.GroupLogo) && (model.GroupLogo != groups.GroupLogo))
            {
                string path = string.Format(model.GroupLogo, "");
                string str2 = base.HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroupThumb);
                string str3 = base.HttpContext.Server.MapPath(SNSAreaRegistration.PathUploadImgGroup);
                try
                {
                    FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, groups.GroupLogoThumb);
                    FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, groups.GroupLogo);
                    if (Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
                    {
                        groups.GroupLogoThumb = model.GroupLogo;
                        groups.GroupLogo = model.GroupLogo;
                    }
                    else
                    {
                        if (!Directory.Exists(str2))
                        {
                            Directory.CreateDirectory(str2);
                        }
                        if (!Directory.Exists(str3))
                        {
                            Directory.CreateDirectory(str3);
                        }
                        List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, "");
                        if ((thumSizeList != null) && (thumSizeList.Count > 0))
                        {
                            string str4 = "";
                            foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                            {
                                str4 = string.Format(model.GroupLogo, size.ThumName);
                                if (File.Exists(base.Server.MapPath(str4)))
                                {
                                    FileInfo info = new FileInfo(base.HttpContext.Server.MapPath(str4));
                                    info.MoveTo(str2 + info.Name);
                                }
                            }
                        }
                        FileInfo info2 = new FileInfo(base.HttpContext.Server.MapPath(path));
                        info2.MoveTo(str3 + info2.Name);
                        groups.GroupLogoThumb = SNSAreaRegistration.PathUploadImgGroupThumb + "{0}" + info2.Name;
                        groups.GroupLogo = SNSAreaRegistration.PathUploadImgGroup + info2.Name;
                    }
                }
                catch (Exception)
                {
                    base.ModelState.AddModelError("Message", "您上传的文件保存失败, 请重新上传!");
                    List<Maticsoft.Model.SNS.GroupTags> list4 = this.bllGroupTags.GetModelList("Status = 1");
                    if (list4 == null)
                    {
                    }
                    model.TagList = (CS$<>9__CachedAnonymousMethodDelegate1a != null) ? "" : string.Join(",", list4.Select<Maticsoft.Model.SNS.GroupTags, string>(CS$<>9__CachedAnonymousMethodDelegate1a));
                    return base.View(model);
                }
            }
            if (this.bllGroups.Update(groups))
            {
                return base.RedirectToAction("GroupInfo", new { GroupId = groups.GroupID });
            }
            return base.View(model);
        }

        [TokenAuthorize]
        public ActionResult Update(int groupId)
        {
            ((dynamic) base.ViewBag).Title = "编辑小组信息";
            UpdateGroup model = new UpdateGroup();
            Maticsoft.Model.SNS.Groups groups = this.bllGroups.GetModel(groupId);
            if (groups.CreatedUserId != base.CurrentUser.UserID)
            {
                return base.RedirectToAction("GroupInfo", new { GroupId = groups.GroupID });
            }
            model.GroupId = groups.GroupID;
            model.GroupLogo = groups.GroupLogo;
            model.GroupName = groups.GroupName;
            model.Tags = groups.Tags;
            model.GroupDescription = groups.GroupDescription;
            List<Maticsoft.Model.SNS.GroupTags> modelList = this.bllGroupTags.GetModelList("Status = 1");
            if (modelList == null)
            {
            }
            model.TagList = (CS$<>9__CachedAnonymousMethodDelegate12 != null) ? "" : string.Join(",", modelList.Select<Maticsoft.Model.SNS.GroupTags, string>(CS$<>9__CachedAnonymousMethodDelegate12));
            return base.View(model);
        }
    }
}


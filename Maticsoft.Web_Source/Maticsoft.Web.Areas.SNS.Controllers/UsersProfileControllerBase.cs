namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Components.Setting;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.ViewModel;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.SNS;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Webdiyer.WebControls.Mvc;

    public abstract class UsersProfileControllerBase : SNSControllerBase
    {
        protected int _PostPageSize = 10;
        protected int _waterfallDetailCount = 1;
        protected int _waterfallSize = 30;
        protected bool Activity = true;
        private Maticsoft.BLL.SNS.UserBlog blogBll = new Maticsoft.BLL.SNS.UserBlog();
        protected Maticsoft.Model.SNS.EnumHelper.PostType DefaultPostType;
        protected int FavAllPageSize = 0x12;
        protected int FavBasePageSize = 6;
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        protected List<Maticsoft.ViewModel.SNS.Posts> list = new List<Maticsoft.ViewModel.SNS.Posts>();
        protected Maticsoft.BLL.SNS.Posts PostsBll = new Maticsoft.BLL.SNS.Posts();
        protected Maticsoft.Model.SNS.Posts PostsModel = new Maticsoft.Model.SNS.Posts();
        private Maticsoft.BLL.SNS.Products productBll = new Maticsoft.BLL.SNS.Products();
        protected UsersExpModel UserModel;

        public UsersProfileControllerBase()
        {
            base.ValidateRequest = false;
        }

        public ActionResult AjaxGetComment(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            int postID = Globals.SafeInt(Fm["PostId"], 0);
            if (postID > 0)
            {
                this.PostsModel = this.PostsBll.GetModel(postID);
                List<Maticsoft.Model.SNS.Comments> commentByPost = comments.GetCommentByPost(this.PostsModel);
                if (commentByPost.Count > 0)
                {
                    return base.Content(this.jss.Serialize(commentByPost));
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxGetCommentByPostId(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            int postID = Globals.SafeInt(Fm["PostId"], 0);
            if (postID > 0)
            {
                this.PostsModel = this.PostsBll.GetModel(postID);
                List<Maticsoft.Model.SNS.Comments> commentByPost = comments.GetCommentByPost(this.PostsModel);
                if (commentByPost.Count > 0)
                {
                    ((dynamic) base.ViewBag).PostId = postID;
                    return base.View(base.CurrentThemeViewPath + "/UserProfile/postCommentList.cshtml", commentByPost);
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxGetPostByIndex(FormCollection Fm)
        {
            string type = Fm["type"];
            int userId = Globals.SafeInt(Fm["UserID"], 0);
            this.DefaultPostType = this.GetDefaultPostType(type);
            int pageSize = this._PostPageSize;
            int pageIndex = Globals.SafeInt(Fm["pageIndex"], 0);
            this.list = this.PostsBll.GetPostByType(userId, ViewModelBase.GetStartPageIndex(pageSize, pageIndex), ViewModelBase.GetEndPageIndex(pageSize, pageIndex), this.DefaultPostType, base.IncludeProduct);
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if ((this.list != null) && (this.list.Count > 0))
            {
                foreach (Maticsoft.ViewModel.SNS.Posts posts in this.list)
                {
                    if ((posts.Post != null) && (posts.Post.Type == 2))
                    {
                        Maticsoft.Model.SNS.Products modelByCache = this.productBll.GetModelByCache((long) posts.Post.TargetId);
                        if (valueByCache != "true")
                        {
                            posts.Post.Description = (string) posts.Post.Description.Replace("{ProductUrl}", (((dynamic) base.ViewBag).BasePath + "Product/Detail/") + posts.Post.TargetId);
                        }
                        else if (modelByCache != null)
                        {
                            posts.Post.Description = (string) posts.Post.Description.Replace("{ProductUrl}", (dynamic) string.IsNullOrWhiteSpace(modelByCache.StaticUrl) ? ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + posts.Post.TargetId) : modelByCache.StaticUrl);
                        }
                    }
                    if ((posts.Post != null) && (posts.Post.Type == 4))
                    {
                        Maticsoft.Model.SNS.UserBlog blog = this.blogBll.GetModelByCache(posts.Post.TargetId);
                        if (valueByCache != "true")
                        {
                            posts.Post.Description = (string) posts.Post.Description.Replace("{BlogUrl}", (((dynamic) base.ViewBag).BasePath + "Blog/BlogDetail/") + posts.Post.TargetId);
                        }
                        else if (blog != null)
                        {
                            posts.Post.Description = (string) posts.Post.Description.Replace("{BlogUrl}", (dynamic) string.IsNullOrWhiteSpace(blog.StaticUrl) ? ((((dynamic) base.ViewBag).BasePath + "Blog/BlogDetail/") + posts.Post.TargetId) : blog.StaticUrl);
                        }
                    }
                    if ((posts.OrigPost != null) && (posts.OrigPost.Type == 2))
                    {
                        Maticsoft.Model.SNS.Products products2 = this.productBll.GetModelByCache((long) posts.OrigPost.TargetId);
                        if (valueByCache != "true")
                        {
                            posts.OrigPost.Description = (string) posts.OrigPost.Description.Replace("{ProductUrl}", (((dynamic) base.ViewBag).BasePath + "Product/Detail/") + posts.OrigPost.TargetId);
                        }
                        else if (products2 != null)
                        {
                            posts.OrigPost.Description = (string) posts.OrigPost.Description.Replace("{ProductUrl}", (dynamic) string.IsNullOrWhiteSpace(products2.StaticUrl) ? ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + posts.OrigPost.TargetId) : products2.StaticUrl);
                        }
                    }
                    if ((posts.OrigPost != null) && (posts.OrigPost.Type == 4))
                    {
                        Maticsoft.Model.SNS.UserBlog blog2 = this.blogBll.GetModelByCache(posts.OrigPost.TargetId);
                        if (valueByCache != "true")
                        {
                            posts.OrigPost.Description = (string) posts.OrigPost.Description.Replace("{BlogUrl}", (((dynamic) base.ViewBag).BasePath + "Blog/BlogDetail/") + posts.OrigPost.TargetId);
                            continue;
                        }
                        if (blog2 != null)
                        {
                            posts.OrigPost.Description = (string) posts.OrigPost.Description.Replace("{BlogUrl}", (dynamic) string.IsNullOrWhiteSpace(blog2.StaticUrl) ? ((((dynamic) base.ViewBag).BasePath + "Blog/BlogDetail/") + posts.OrigPost.TargetId) : blog2.StaticUrl);
                        }
                    }
                }
            }
            if (base.currentUser != null)
            {
                ((dynamic) base.ViewBag).CurrentUserID = base.currentUser.UserID;
            }
            return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", this.list);
        }

        public ActionResult Albums(int? uid, string IsFav)
        {
            if (!this.LoadUserInfo(!uid.HasValue ? 0 : uid.Value))
            {
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    return this.Redirect("/Account/Login");
                }
                return this.Redirect("/SNS/Account/Login");
            }
            Maticsoft.BLL.SNS.AlbumType type = new Maticsoft.BLL.SNS.AlbumType();
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            List<AlbumIndex> model = new List<AlbumIndex>();
            if (!string.IsNullOrEmpty(IsFav))
            {
                model = albums.GetUserFavAlbum(this.UserID, -1);
                ((dynamic) base.ViewBag).IsFav = true;
            }
            else
            {
                model = albums.GetListByUserId(this.UserID, base.UserAlbumDetailType);
            }
            ((dynamic) base.ViewBag).AlbumTypeList = type.GetModelListByCache(Maticsoft.Model.SNS.EnumHelper.Status.Enabled);
            ((dynamic) base.ViewBag).IsCurrentUser = this.IsCurrentUser;
            ((dynamic) base.ViewBag).NickName = this.NickName;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (<Albums>o__SiteContainer48.<>p__Site4f == null)
            {
                <Albums>o__SiteContainer48.<>p__Site4f = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(UsersProfileControllerBase), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            }
            if (<Albums>o__SiteContainer48.<>p__Site4f.Target(<Albums>o__SiteContainer48.<>p__Site4f, ((dynamic) base.ViewBag).IsCurrentUser == true))
            {
                ((dynamic) base.ViewBag).Title = "我的专辑 - " + pageSetting.Title;
            }
            else
            {
                ((dynamic) base.ViewBag).Title = this.NickName + "的专辑 - " + pageSetting.Title;
            }
            return base.View(base.CurrentThemeViewPath + "/UserProfile/Albums.cshtml", model);
        }

        public ActionResult CheckIsAdmin()
        {
            if (base.currentUser == null)
            {
                return base.Content("NoLogin");
            }
            if (base.currentUser.UserType != "AA")
            {
                return base.Content("False");
            }
            return base.Content("True");
        }

        public ActionResult CheckUserState()
        {
            if (base.currentUser != null)
            {
                return base.Content("Yes");
            }
            return base.Content("No");
        }

        public ActionResult CheckUserState4UserType()
        {
            if (base.currentUser != null)
            {
                return base.Content((base.currentUser.UserType == "AA") ? "Yes4AA" : "Yes");
            }
            return base.Content("No");
        }

        public ActionResult Fans(int? uid, int? page)
        {
            if (!this.LoadUserInfo(!uid.HasValue ? 0 : uid.Value))
            {
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    return this.Redirect("/Account/Login");
                }
                return this.Redirect("/SNS/Account/Login");
            }
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int totalItemCount = this.UserModel.FansCount.HasValue ? this.UserModel.FansCount.Value : 0;
            PagedList<Maticsoft.Model.SNS.UserShip> model = null;
            int currentUserId = (!this.IsCurrentUser && (base.currentUser != null)) ? base.currentUser.UserID : 0;
            List<Maticsoft.Model.SNS.UserShip> items = ship.GetUsersAllFansByPage(this.UserID, startIndex, endIndex, currentUserId);
            if ((items != null) && (items.Count > 0))
            {
                int? nullable3 = page;
                model = new PagedList<Maticsoft.Model.SNS.UserShip>(items, nullable3.HasValue ? nullable3.GetValueOrDefault() : 1, pageSize, totalItemCount);
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/FansList.cshtml", model);
            }
            ((dynamic) base.ViewBag).IsCurrentUser = this.IsCurrentUser;
            ((dynamic) base.ViewBag).UserId = this.UserID;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (<Fans>o__SiteContainer54.<>p__Site59 == null)
            {
                <Fans>o__SiteContainer54.<>p__Site59 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(UsersProfileControllerBase), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            }
            if (<Fans>o__SiteContainer54.<>p__Site59.Target(<Fans>o__SiteContainer54.<>p__Site59, ((dynamic) base.ViewBag).IsCurrentUser == true))
            {
                ((dynamic) base.ViewBag).Title = "我的粉丝 - " + pageSetting.Title;
            }
            else
            {
                ((dynamic) base.ViewBag).Title = this.NickName + "的粉丝 - " + pageSetting.Title;
            }
            return base.View(base.CurrentThemeViewPath + "/UserProfile/Fans.cshtml", model);
        }

        public ActionResult Fav(int? uid, int? pageIndex)
        {
            if (!this.LoadUserInfo(!uid.HasValue ? 0 : uid.Value))
            {
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    return this.Redirect("/Account/Login");
                }
                return this.Redirect("/SNS/Account/Login");
            }
            pageIndex = new int?(pageIndex.HasValue ? pageIndex.Value : 1);
            int startPageIndex = ViewModelBase.GetStartPageIndex(this.FavBasePageSize, pageIndex.Value);
            int endIndex = (startPageIndex + this.FavBasePageSize) - 1;
            int num3 = this.UserModel.FavouritesCount.Value;
            ((dynamic) base.ViewBag).WaterfallStartIndex = endIndex;
            ((dynamic) base.ViewBag).WaterfallEndIndex = ((pageIndex.Value * this.FavAllPageSize) > num3) ? num3 : (pageIndex.Value * this.FavAllPageSize);
            ((dynamic) base.ViewBag).UserId = this.UserID;
            ((dynamic) base.ViewBag).RequestPage = (base.GetType().Name == "UserController") ? "User" : "Profile";
            if (num3 < 0)
            {
                return new EmptyResult();
            }
            Maticsoft.BLL.SNS.UserFavourite favourite = new Maticsoft.BLL.SNS.UserFavourite();
            PhotoList model = new PhotoList();
            ((dynamic) base.ViewBag).IsCurrentUser = this.IsCurrentUser;
            ((dynamic) base.ViewBag).NickName = this.NickName;
            int? nullable2 = pageIndex;
            model.PhotoPagedList = favourite.GetFavListByPage(this.UserID, "", startPageIndex, endIndex).ToPagedList<PostContent>(nullable2.HasValue ? nullable2.GetValueOrDefault() : 1, this.FavBasePageSize, new int?(num3));
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/FavList.cshtml", model);
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (<Fav>o__SiteContainer37.<>p__Site40 == null)
            {
                <Fav>o__SiteContainer37.<>p__Site40 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(UsersProfileControllerBase), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            }
            if (<Fav>o__SiteContainer37.<>p__Site40.Target(<Fav>o__SiteContainer37.<>p__Site40, ((dynamic) base.ViewBag).IsCurrentUser == true))
            {
                ((dynamic) base.ViewBag).Title = "我的喜欢 - " + pageSetting.Title;
            }
            else
            {
                ((dynamic) base.ViewBag).Title = this.NickName + "的喜欢 - " + pageSetting.Title;
            }
            return base.View(base.CurrentThemeViewPath + "/UserProfile/FavIndex.cshtml", model);
        }

        public ActionResult Fellows(int? uid, int? page)
        {
            if (!this.LoadUserInfo(!uid.HasValue ? 0 : uid.Value))
            {
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    return this.Redirect("/Account/Login");
                }
                return this.Redirect("/SNS/Account/Login");
            }
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            int pageSize = 10;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int totalItemCount = this.UserModel.FellowCount.HasValue ? this.UserModel.FellowCount.Value : 0;
            int currentUserId = (!this.IsCurrentUser && (base.currentUser != null)) ? base.currentUser.UserID : 0;
            PagedList<Maticsoft.Model.SNS.UserShip> model = null;
            List<Maticsoft.Model.SNS.UserShip> items = ship.GetUsersAllFellowsByPage(this.UserID, startIndex, endIndex, currentUserId);
            if ((items != null) && (items.Count > 0))
            {
                int? nullable3 = page;
                model = new PagedList<Maticsoft.Model.SNS.UserShip>(items, nullable3.HasValue ? nullable3.GetValueOrDefault() : 1, pageSize, totalItemCount);
            }
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/FellowsList.cshtml", model);
            }
            ((dynamic) base.ViewBag).IsCurrentUser = this.IsCurrentUser;
            ((dynamic) base.ViewBag).UserId = this.UserID;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (<Fellows>o__SiteContainer5e.<>p__Site63 == null)
            {
                <Fellows>o__SiteContainer5e.<>p__Site63 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(UsersProfileControllerBase), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            }
            if (<Fellows>o__SiteContainer5e.<>p__Site63.Target(<Fellows>o__SiteContainer5e.<>p__Site63, ((dynamic) base.ViewBag).IsCurrentUser == true))
            {
                ((dynamic) base.ViewBag).Title = "我的关注 - " + pageSetting.Title;
            }
            else
            {
                ((dynamic) base.ViewBag).Title = this.NickName + "的关注 - " + pageSetting.Title;
            }
            return base.View(base.CurrentThemeViewPath + "/UserProfile/Fellows.cshtml", model);
        }

        public ActionResult GetDataCountByPageType(FormCollection fc)
        {
            this.UserID = Globals.SafeInt(fc["UserID"], 0);
            this.DefaultPostType = this.GetDefaultPostType(fc["PostType"]);
            int num = this.PostsBll.GetCountByPostType(this.UserID, this.DefaultPostType, base.IncludeProduct);
            return base.Content(num.ToString());
        }

        public Maticsoft.Model.SNS.EnumHelper.PostType GetDefaultPostType(string type)
        {
            Maticsoft.Model.SNS.EnumHelper.PostType user = Maticsoft.Model.SNS.EnumHelper.PostType.User;
            if (string.IsNullOrEmpty(type))
            {
                return user;
            }
            switch (type)
            {
                case "user":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.User;

                case "all":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.All;

                case "referme":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.ReferMe;

                case "eachother":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.EachOther;

                case "photo":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.Photo;

                case "product":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.Product;

                case "video":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.Video;

                case "fellow":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.Fellow;

                case "blog":
                    return Maticsoft.Model.SNS.EnumHelper.PostType.Blog;
            }
            return Maticsoft.Model.SNS.EnumHelper.PostType.All;
        }

        public ActionResult Group(int? uid, int? page, int? Type)
        {
            if (!this.LoadUserInfo(!uid.HasValue ? 0 : uid.Value))
            {
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    return this.Redirect("/Account/Login");
                }
                return this.Redirect("/SNS/Account/Login");
            }
            List<Maticsoft.Model.SNS.GroupTopics> list = new List<Maticsoft.Model.SNS.GroupTopics>();
            Maticsoft.BLL.SNS.GroupTopics topics = new Maticsoft.BLL.SNS.GroupTopics();
            page = new int?((page.HasValue && (page.Value > 1)) ? page.Value : 1);
            Type = new int?(Type.HasValue ? Type.Value : 0);
            int pageSize = 10;
            int startIndex = (page.Value > 1) ? (((page.Value - 1) * pageSize) + 1) : 0;
            int endIndex = page.Value * pageSize;
            int countByType = topics.GetCountByType(this.UserID, Type.Value);
            int? nullable = page;
            PagedList<Maticsoft.Model.SNS.GroupTopics> model = new PagedList<Maticsoft.Model.SNS.GroupTopics>(topics.GetUserRelativeTopicByType(this.UserID, Type.Value, startIndex, endIndex), nullable.HasValue ? nullable.GetValueOrDefault() : 1, pageSize, countByType);
            ((dynamic) base.ViewBag).IsCurrentUser = this.IsCurrentUser;
            ((dynamic) base.ViewBag).UserId = this.UserID;
            ((dynamic) base.ViewBag).Type = Type.Value;
            ((dynamic) base.ViewBag).NickName = this.NickName;
            if (base.Request.IsAjaxRequest())
            {
                return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/TopicList.cshtml", model);
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (<Group>o__SiteContainer68.<>p__Site6f == null)
            {
                <Group>o__SiteContainer68.<>p__Site6f = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(UsersProfileControllerBase), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            }
            if (<Group>o__SiteContainer68.<>p__Site6f.Target(<Group>o__SiteContainer68.<>p__Site6f, ((dynamic) base.ViewBag).IsCurrentUser == true))
            {
                ((dynamic) base.ViewBag).Title = "我的小组 - " + pageSetting.Title;
            }
            else
            {
                ((dynamic) base.ViewBag).Title = this.NickName + "的小组 - " + pageSetting.Title;
            }
            return base.View(base.CurrentThemeViewPath + "/UserProfile/Group.cshtml", model);
        }

        public abstract bool LoadUserInfo(int UserID);
        public ActionResult Posts(string type, int? uid, string nickname)
        {
            int num;
            Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
            if (!string.IsNullOrEmpty(nickname) && ((num = users.GetUserIdByNickName(nickname)) > 0))
            {
                uid = new int?(num);
            }
            ((dynamic) base.ViewBag).IsCurrentUser = !uid.HasValue && (base.currentUser != null);
            if (!this.LoadUserInfo(!uid.HasValue ? 0 : uid.Value) || !this.Activity)
            {
                if (Maticsoft.Components.MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    return this.Redirect("/Error/UserError");
                }
                return this.Redirect("/SNS/Error/UserError");
            }
            Maticsoft.BLL.SNS.AlbumType type2 = new Maticsoft.BLL.SNS.AlbumType();
            PostsPage model = new PostsPage();
            new Maticsoft.BLL.Members.PointsDetail();
            model.Type = type;
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "user":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.User;
                        goto Label_0235;

                    case "all":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.All;
                        goto Label_0235;

                    case "referme":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.ReferMe;
                        goto Label_0235;

                    case "eachother":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.EachOther;
                        goto Label_0235;

                    case "photo":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.Photo;
                        goto Label_0235;

                    case "product":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.Product;
                        goto Label_0235;

                    case "video":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.Video;
                        goto Label_0235;

                    case "fellow":
                        this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.Fellow;
                        goto Label_0235;
                }
            }
            else if (this.IsCurrentUser)
            {
                model.Type = "fellow";
                this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.Fellow;
            }
            else
            {
                model.Type = "user";
                this.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.User;
            }
        Label_0235:
            model.PageSize = this._PostPageSize;
            model.DataCount = this.PostsBll.GetCountByPostType(this.UserID, this.DefaultPostType, base.IncludeProduct);
            model.AlbumTypeList = type2.GetModelListByCache(Maticsoft.Model.SNS.EnumHelper.Status.Enabled);
            model.UserID = this.UserID;
            model.Setting = Maticsoft.BLL.SNS.ConfigSystem.GetPostSetByCache();
            ((dynamic) base.ViewBag).CurrentUserID = this.UserID;
            ((dynamic) base.ViewBag).NickName = this.NickName;
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ((dynamic) base.ViewBag).Keywords = pageSetting.Keywords;
            ((dynamic) base.ViewBag).Description = pageSetting.Description;
            if (<Posts>o__SiteContainer0.<>p__Site6 == null)
            {
                <Posts>o__SiteContainer0.<>p__Site6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(UsersProfileControllerBase), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
            }
            if (<Posts>o__SiteContainer0.<>p__Site6.Target(<Posts>o__SiteContainer0.<>p__Site6, ((dynamic) base.ViewBag).IsCurrentUser == true))
            {
                ((dynamic) base.ViewBag).Title = "我的首页 - " + pageSetting.Title;
            }
            else
            {
                ((dynamic) base.ViewBag).Title = this.NickName + "的首页 - " + pageSetting.Title;
            }
            return base.View("Posts", model);
        }

        [HttpPost]
        public ActionResult WaterfallFavListData(int UserID, int startIndex)
        {
            if (!this.LoadUserInfo(UserID))
            {
                return new EmptyResult();
            }
            ((dynamic) base.ViewBag).BasePageSize = this.FavBasePageSize;
            startIndex = (startIndex > 1) ? (startIndex + 1) : 0;
            int endIndex = (startIndex > 1) ? ((startIndex + this._waterfallDetailCount) - 1) : this._waterfallDetailCount;
            Maticsoft.BLL.SNS.UserFavourite favourite = new Maticsoft.BLL.SNS.UserFavourite();
            if (this.UserModel.FavouritesCount.Value < 1)
            {
                return new EmptyResult();
            }
            PhotoList model = new PhotoList {
                PhotoListWaterfall = favourite.GetFavListByPage(UserID, "", startIndex, endIndex)
            };
            ((dynamic) base.ViewBag).IsCurrentUser = this.IsCurrentUser;
            return base.View(base.CurrentThemeViewPath + "/UserProfile/FavListWaterfall.cshtml", model);
        }

        protected bool IsCurrentUser { get; set; }

        protected string NickName { get; set; }

        protected int UserID { get; set; }
    }
}


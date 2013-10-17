namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Settings;
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Components.Filters;
    using Maticsoft.Json;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.ViewModel;
    using Maticsoft.ViewModel.SNS;
    using Maticsoft.Web.Components;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    [TokenAuthorize]
    public class ProfileController : UsersProfileControllerBase
    {
        private UsersExp UserBll = new UsersExp();

        public ProfileController()
        {
            base._PostPageSize = base.PostDataSize;
            base.FavAllPageSize = base.FallDataSize;
            base.FavBasePageSize = base.FallInitDataSize;
        }

        public ActionResult AjaxAddAlbum(FormCollection Fm)
        {
            if (base.currentUser == null)
            {
                return base.Content("NoLogin");
            }
            if (base.CurrentUser.UserType == "AA")
            {
                return base.Content("AA");
            }
            string str = Fm["AlbumName"];
            int typeId = Globals.SafeInt(Fm["Type"], 0);
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            Maticsoft.Model.SNS.UserAlbums model = new Maticsoft.Model.SNS.UserAlbums {
                AlbumName = str,
                CreatedDate = DateTime.Now,
                CreatedNickName = base.currentUser.NickName,
                CreatedUserID = base.currentUser.UserID
            };
            if ((model.AlbumID = albums.AddEx(model, typeId)) > 0)
            {
                Maticsoft.BLL.SNS.UserAlbumsType type = new Maticsoft.BLL.SNS.UserAlbumsType();
                Maticsoft.Model.SNS.UserAlbumsType type2 = new Maticsoft.Model.SNS.UserAlbumsType {
                    AlbumsID = model.AlbumID,
                    AlbumsUserID = new int?(model.CreatedUserID),
                    TypeID = typeId
                };
                if (type.Add(type2))
                {
                    return base.Content(model.AlbumID.ToString());
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxAddBlog(FormCollection Fm)
        {
            if (base.currentUser == null)
            {
                return base.Content("NoLogin");
            }
            if (base.currentUser.UserType == "AA")
            {
                return base.Content("AA");
            }
            string str = InjectionFilter.Filter(Fm["Title"]);
            string str2 = Fm["Des"];
            base.PostsModel.Description = str;
            base.PostsModel.CreatedDate = DateTime.Now;
            base.PostsModel.CreatedNickName = base.currentUser.NickName;
            base.PostsModel.CreatedUserID = base.currentUser.UserID;
            base.PostsModel.UserIP = base.Request.UserHostAddress;
            Maticsoft.Model.SNS.UserBlog blogModel = new Maticsoft.Model.SNS.UserBlog {
                Title = str,
                Description = str2,
                UserID = base.currentUser.UserID,
                UserName = base.currentUser.NickName,
                CreatedDate = DateTime.Now
            };
            base.PostsModel.Status = 1;
            base.PostsModel = base.PostsBll.AddBlogPost(base.PostsModel, blogModel, true);
            base.PostsModel.Description = ViewModelBase.RegexNickName(base.PostsModel.Description);
            Maticsoft.ViewModel.SNS.Posts item = new Maticsoft.ViewModel.SNS.Posts();
            base.PostsModel.Description = (string) base.PostsModel.Description.Replace("{BlogUrl}", (((dynamic) base.ViewBag).BasePath + "Blog/BlogDetail/") + base.PostsModel.TargetId);
            item.Post = base.PostsModel;
            base.list.Add(item);
            return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", base.list);
        }

        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            Maticsoft.Model.SNS.Comments comModel = new Maticsoft.Model.SNS.Comments();
            int postID = Globals.SafeInt(Fm["PostId"], 0);
            List<Maticsoft.Model.SNS.Comments> model = new List<Maticsoft.Model.SNS.Comments>();
            int num2 = 0;
            string source = ViewModelBase.ReplaceFace(InjectionFilter.Filter(Fm["Des"]));
            if (postID > 0)
            {
                base.PostsModel = base.PostsBll.GetModel(postID);
                comModel.CreatedDate = DateTime.Now.AddMinutes(1.0);
                comModel.CreatedNickName = base.currentUser.NickName;
                comModel.CreatedUserID = base.currentUser.UserID;
                comModel.Description = source;
                comModel.HasReferUser = source.Contains<char>('@');
                comModel.IsRead = false;
                comModel.ReplyCount = 0;
                comModel.TargetId = (base.PostsModel.TargetId > 0) ? base.PostsModel.TargetId : base.PostsModel.PostID;
                if (base.PostsModel.Type.Value == 3)
                {
                    base.PostsModel.Type = 0;
                }
                comModel.Type = base.PostsModel.Type.Value;
                comModel.UserIP = base.Request.UserHostAddress;
                num2 = comments.AddEx(comModel);
                if (num2 > 0)
                {
                    comModel.CommentID = num2;
                    comModel.Description = ViewModelBase.RegexNickName(comModel.Description);
                    if (!FilterWords.ContainsModWords(comModel.Description))
                    {
                        model.Add(comModel);
                    }
                    ((dynamic) base.ViewBag).PostId = postID;
                    return base.View(base.CurrentThemeViewPath + "/UserProfile/postCommentList.cshtml", model);
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxAddFavourite(FormCollection Fm)
        {
            int postId = Globals.SafeInt(Fm["TargetId"], 0);
            if (Fm["Type"] == "Video")
            {
                Maticsoft.BLL.SNS.Posts posts = new Maticsoft.BLL.SNS.Posts();
                if (posts.UpdateFavCount(postId))
                {
                    return base.Content("Ok");
                }
                return base.Content("No");
            }
            Maticsoft.Model.SNS.UserFavourite favModel = new Maticsoft.Model.SNS.UserFavourite();
            Maticsoft.BLL.SNS.UserFavourite favourite2 = new Maticsoft.BLL.SNS.UserFavourite();
            int topicId = Globals.SafeInt(Fm["TopicId"], 0);
            int replyId = Globals.SafeInt(Fm["ReplyId"], 0);
            favModel.Type = (Fm["Type"] == "Product") ? 1 : 0;
            favModel.TargetID = Globals.SafeInt(Fm["TargetId"], 0);
            favModel.CreatedDate = DateTime.Now;
            favModel.CreatedUserID = base.currentUser.UserID;
            favModel.CreatedNickName = base.currentUser.NickName;
            if (favourite2.Exists(base.currentUser.UserID, favModel.Type, favModel.TargetID))
            {
                return base.Content("Repeat");
            }
            if (favourite2.AddEx(favModel, topicId, replyId))
            {
                return base.Content("Ok");
            }
            return base.Content("No");
        }

        public ActionResult AjaxAddPoint(string Type)
        {
            int num = 0;
            PointsDetail detail = new PointsDetail();
            if (Type == "Product")
            {
                num = detail.AddPoints("Product", base.currentUser.UserID, "发布商品", "");
            }
            else if (Type == "Photo")
            {
                num = detail.AddPoints("Photo", base.currentUser.UserID, "分享照片", "");
            }
            else
            {
                num = detail.AddPoints("Post", base.currentUser.UserID, "发布动态", "");
            }
            return base.Content(num.ToString());
        }

        public ActionResult AjaxAddProductComment(FormCollection Fm)
        {
            Maticsoft.Model.SNS.Comments comModel = new Maticsoft.Model.SNS.Comments();
            Maticsoft.BLL.SNS.Comments comments2 = new Maticsoft.BLL.SNS.Comments();
            int num = Globals.SafeInt(Fm["TargetId"], 0);
            int num2 = (Fm["Type"] == "Product") ? 2 : 1;
            int num3 = 0;
            string source = InjectionFilter.Filter(Fm["Des"]);
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
            num3 = comments2.AddEx(comModel);
            if (num3 <= 0)
            {
                return base.Content("No");
            }
            comModel.CommentID = num3;
            comModel.Description = ViewModelBase.RegexNickName(comModel.Description);
            List<Maticsoft.Model.SNS.Comments> list = new List<Maticsoft.Model.SNS.Comments>();
            if (!FilterWords.ContainsModWords(comModel.Description))
            {
                list.Add(comModel);
            }
            return this.PartialView(base.CurrentThemeViewPath + "/Partial/TargetListComment.cshtml", list.AsEnumerable<Maticsoft.Model.SNS.Comments>());
        }

        public ActionResult AjaxAddReport(FormCollection Fm)
        {
            string str = InjectionFilter.Filter(Fm["Description"]);
            int num = Globals.SafeInt(Fm["Type"], 0);
            int postID = Globals.SafeInt(Fm["TargetId"], 0);
            Maticsoft.BLL.SNS.Report report = new Maticsoft.BLL.SNS.Report();
            Maticsoft.Model.SNS.Report model = new Maticsoft.Model.SNS.Report();
            base.PostsModel = base.PostsBll.GetModel(postID);
            if (base.PostsModel != null)
            {
                model.Description = str;
                model.CreatedDate = DateTime.Now;
                model.CreatedNickName = base.currentUser.NickName;
                model.CreatedUserID = base.currentUser.UserID;
                model.ReportTypeID = num;
                model.TargetID = postID;
                if (base.PostsModel.TargetId > 0)
                {
                    model.TargetID = base.PostsModel.TargetId;
                }
                model.Status = 0;
                model.TargetType = base.PostsModel.Type.Value;
                if (report.Add(model) > 0)
                {
                    return base.Content("Ok");
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxAddToAlbum(FormCollection Fm)
        {
            Maticsoft.Model.SNS.UserAlbumDetail model = new Maticsoft.Model.SNS.UserAlbumDetail();
            Maticsoft.BLL.SNS.UserAlbumDetail detail2 = new Maticsoft.BLL.SNS.UserAlbumDetail();
            int targetID = Globals.SafeInt(Fm["TargetId"], 0);
            int type = (Fm["Type"] == "Product") ? 1 : 0;
            int albumID = Globals.SafeInt(Fm["AlbumId"], 0);
            if (albumID > 0)
            {
                string str = Fm["Des"];
                model.TargetID = targetID;
                model.Type = type;
                model.Description = str;
                model.AlbumUserId = base.currentUser.UserID;
                model.AlbumID = albumID;
                if (detail2.Exists(albumID, targetID, type))
                {
                    return base.Content("Repeat");
                }
                if (detail2.AddEx(model))
                {
                    return base.Content("Ok");
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxAddTopicToFav(int TopicId)
        {
            Maticsoft.Model.SNS.GroupTopicFav model = new Maticsoft.Model.SNS.GroupTopicFav();
            Maticsoft.BLL.SNS.GroupTopicFav fav2 = new Maticsoft.BLL.SNS.GroupTopicFav();
            if (fav2.Exists(TopicId, base.currentUser.UserID))
            {
                return base.Content("Repeate");
            }
            model.CreatedDate = DateTime.Now;
            model.CreatedUserID = base.currentUser.UserID;
            model.TopicID = TopicId;
            if (fav2.AddEx(model))
            {
                return base.Content("Yes");
            }
            return base.Content("No");
        }

        public ActionResult AjaxCheckIsFellow(int UserId)
        {
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            if (ship.Exists(base.currentUser.UserID, UserId))
            {
                return base.Content("True");
            }
            return base.Content("False");
        }

        public ActionResult AJaxCheckUserIsJoinGroup(FormCollection fm)
        {
            new Maticsoft.Model.SNS.GroupUsers();
            Maticsoft.BLL.SNS.GroupUsers users = new Maticsoft.BLL.SNS.GroupUsers();
            int groupID = Globals.SafeInt(fm["GroupId"], 0);
            if (users.Exists(groupID, base.currentUser.UserID))
            {
                return base.Content("joined");
            }
            return base.Content("No");
        }

        public ActionResult AjaxCreateAlbums()
        {
            List<Maticsoft.Model.SNS.AlbumType> modelList = new Maticsoft.BLL.SNS.AlbumType().GetModelList("");
            return base.View(base.CurrentThemeViewPath + "/UserProfile/AjaxCreateAlbums.cshtml", modelList);
        }

        public ActionResult AJaxCreateReply(FormCollection fm)
        {
            string content = InjectionFilter.Filter(fm["Des"]);
            int replyId = Globals.SafeInt(fm["ReplyId"], 0);
            int userID = base.currentUser.UserID;
            string nickName = base.currentUser.NickName;
            Maticsoft.BLL.SNS.GroupTopicReply reply = new Maticsoft.BLL.SNS.GroupTopicReply();
            content = ViewModelBase.ReplaceFace(content);
            int replyID = reply.ForwardReply(replyId, content, userID, nickName);
            List<Maticsoft.Model.SNS.GroupTopicReply> model = new List<Maticsoft.Model.SNS.GroupTopicReply>();
            Maticsoft.Model.SNS.GroupTopicReply item = new Maticsoft.Model.SNS.GroupTopicReply();
            item = reply.GetModel(replyID);
            model.Add(item);
            return this.PartialView(base.CurrentThemeViewPath + "/Group/TopicReplyResult.cshtml", model);
        }

        public ActionResult AjaxCreateReport()
        {
            List<Maticsoft.Model.SNS.ReportType> modelList = new Maticsoft.BLL.SNS.ReportType().GetModelList("");
            return base.View(base.CurrentThemeViewPath + "/UserProfile/AjaxCreateReport.cshtml", modelList);
        }

        public ActionResult AJaxCreateTopic(FormCollection fm)
        {
            string str = InjectionFilter.Filter((fm["Title"] != null) ? fm["Title"].ToString() : "");
            string str2 = (fm["Des"] != null) ? fm["Des"].ToString() : "";
            long pid = Globals.SafeLong(fm["Pid"], (long) 0L);
            string imageUrl = "";
            if (pid == 0L)
            {
                imageUrl = (fm["ImageUrl"] != null) ? fm["ImageUrl"].ToString() : "";
            }
            int num2 = Globals.SafeInt(fm["GroupId"], 0);
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
            {
                Maticsoft.BLL.SNS.GroupTopics topics = new Maticsoft.BLL.SNS.GroupTopics();
                Maticsoft.Model.SNS.GroupTopics tModel = new Maticsoft.Model.SNS.GroupTopics {
                    Title = str,
                    Description = str2
                };
                string savePath = "/Upload/SNS/Images/Group/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string saveThumbsPath = "/Upload/SNS/Images/GroupThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                tModel.ImageUrl = Maticsoft.BLL.SNS.Photos.MoveImage(imageUrl, savePath, saveThumbsPath);
                tModel.GroupID = num2;
                tModel.GroupName = "";
                tModel.CreatedDate = DateTime.Now;
                tModel.CreatedUserID = base.currentUser.UserID;
                tModel.CreatedNickName = base.currentUser.NickName;
                if ((tModel.TopicID = topics.AddEx(tModel, pid)) > 0)
                {
                    return base.Content(tModel.TopicID.ToString());
                }
                if (tModel.TopicID == -2)
                {
                    FormsAuthentication.SignOut();
                    base.Session.Remove(Globals.SESSIONKEY_USER);
                    base.Session.Clear();
                    base.Session.Abandon();
                }
            }
            return base.Content("No");
        }

        public ActionResult AJaxCreateTopicReply(FormCollection fm)
        {
            string str = InjectionFilter.Filter((fm["Des"] != null) ? fm["Des"].ToString() : "");
            long pid = Globals.SafeLong(fm["Pid"], (long) 0L);
            int num2 = Globals.SafeInt(fm["GroupId"], 0);
            int num3 = Globals.SafeInt(fm["TopicId"], 0);
            string imageUrl = "";
            if (pid == 0L)
            {
                imageUrl = (fm["ImageUrl"] != null) ? fm["ImageUrl"].ToString() : "";
            }
            if (!string.IsNullOrEmpty(str))
            {
                Maticsoft.BLL.SNS.GroupTopicReply reply = new Maticsoft.BLL.SNS.GroupTopicReply();
                Maticsoft.Model.SNS.GroupTopicReply tModel = new Maticsoft.Model.SNS.GroupTopicReply {
                    Description = str
                };
                string savePath = "/Upload/SNS/Images/Group/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string saveThumbsPath = "/Upload/SNS/Images/GroupThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                tModel.PhotoUrl = Maticsoft.BLL.SNS.Photos.MoveImage(imageUrl, savePath, saveThumbsPath);
                tModel.GroupID = num2;
                tModel.CreatedDate = DateTime.Now;
                tModel.ReplyUserID = base.currentUser.UserID;
                tModel.ReplyNickName = base.currentUser.NickName;
                tModel.Status = 1;
                tModel.TopicID = num3;
                if ((tModel.ReplyID = reply.AddEx(tModel, pid)) > 0)
                {
                    Maticsoft.BLL.SNS.GroupTopicReply reply3 = new Maticsoft.BLL.SNS.GroupTopicReply();
                    List<Maticsoft.Model.SNS.GroupTopicReply> model = new List<Maticsoft.Model.SNS.GroupTopicReply>();
                    tModel = reply3.GetModel(tModel.ReplyID);
                    model.Add(tModel);
                    return this.PartialView(base.CurrentThemeViewPath + "/Group/TopicReplyResult.cshtml", model);
                }
            }
            return base.Content("No");
        }

        public ActionResult AjaxDelAlbum(int AlbumId)
        {
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            if (albums.DeleteEx(AlbumId, base.currentUser.UserID))
            {
                return base.Content("True");
            }
            return base.Content("False");
        }

        public ActionResult AjaxDelAlbumDetail(int TargetId, string Type, int AlbumId)
        {
            Maticsoft.BLL.SNS.UserAlbumDetail detail = new Maticsoft.BLL.SNS.UserAlbumDetail();
            if (detail.DeleteEx(AlbumId, TargetId, (Type == "Product") ? 1 : 0))
            {
                return base.Content("True");
            }
            return base.Content("False");
        }

        public ActionResult AjaxDelFav(int TargetId, string Type)
        {
            Maticsoft.BLL.SNS.UserFavourite favourite = new Maticsoft.BLL.SNS.UserFavourite();
            if (favourite.DeleteEx(base.currentUser.UserID, TargetId, (Type == "Product") ? 1 : 0))
            {
                return base.Content("True");
            }
            return base.Content("False");
        }

        public ActionResult AjaxDelPost(FormCollection Fm)
        {
            int postID = Globals.SafeInt(Fm["PostId"], 0);
            Maticsoft.Model.SNS.Posts model = base.PostsBll.GetModel(postID);
            Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            int result = 0;
            DataSet set = new DataSet();
            int valueOrDefault = model.Type.GetValueOrDefault();
            if (model.Type.HasValue)
            {
                switch (valueOrDefault)
                {
                    case 0:
                        if ((base.PostsBll.DeleteEx(postID, false, 1) && !string.IsNullOrWhiteSpace(model.ImageUrl)) && !model.ImageUrl.StartsWith("http://"))
                        {
                            this.DeletePhysicalFile(model.ImageUrl);
                        }
                        goto Label_017A;

                    case 1:
                        set = photos.DeleteListEx(model.TargetId.ToString(), out result, false, 1);
                        if (set != null)
                        {
                            this.PhysicalFileInfo(set.Tables[0]);
                        }
                        goto Label_017A;

                    case 2:
                        set = products.DeleteListEx(model.TargetId.ToString(), out result, false, 1);
                        if (set != null)
                        {
                            this.PhysicalFileInfo(set.Tables[0]);
                        }
                        goto Label_017A;

                    case 4:
                        new Maticsoft.BLL.SNS.UserBlog().DeleteEx(model.TargetId);
                        goto Label_017A;
                }
            }
            if ((base.PostsBll.DeleteEx(postID, false, 1) && !string.IsNullOrWhiteSpace(model.ImageUrl)) && !model.ImageUrl.StartsWith("http://"))
            {
                this.DeletePhysicalFile(model.ImageUrl);
            }
        Label_017A:
            return base.Content(postID.ToString());
        }

        public ActionResult AjaxDelYulanTu(FormCollection Fm)
        {
            string str = Fm["ImageUrl"];
            if (string.IsNullOrEmpty(str))
            {
                return base.Content("No");
            }
            if (!str.StartsWith("http://"))
            {
                if (File.Exists(base.Server.MapPath(string.Format(str, ""))))
                {
                    File.Delete(base.Server.MapPath(string.Format(str, "")));
                }
                List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, "");
                if ((thumSizeList != null) && (thumSizeList.Count > 0))
                {
                    foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                    {
                        if (File.Exists(base.Server.MapPath(string.Format(str, size.ThumName))))
                        {
                            File.Delete(base.Server.MapPath(string.Format(str, size.ThumName)));
                        }
                    }
                }
            }
            return base.Content("True");
        }

        [HttpPost]
        public ActionResult AjaxFellowUser(int UserId)
        {
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            if (UserId == base.currentUser.UserID)
            {
                return base.Content("Self");
            }
            if (ship.AddAttention(base.CurrentUser.UserID, UserId))
            {
                return base.Content("True");
            }
            return base.Content("False");
        }

        public ActionResult AjaxGetAlbumsType()
        {
            List<Maticsoft.Model.SNS.AlbumType> modelListByCache = new Maticsoft.BLL.SNS.AlbumType().GetModelListByCache(Maticsoft.Model.SNS.EnumHelper.Status.Enabled);
            return base.Content(base.jss.Serialize(modelListByCache));
        }

        public ActionResult AjaxGetMyMyAblum()
        {
            List<Maticsoft.Model.SNS.UserAlbums> userAblumsByUserID = new Maticsoft.BLL.SNS.UserAlbums().GetUserAblumsByUserID(base.currentUser.UserID);
            return base.Content(base.jss.Serialize(userAblumsByUserID));
        }

        public ActionResult AjaxGetProductInfo(string ProductLink)
        {
            Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
            string text = ViewModelBase.RegexProductId(ProductLink);
            if (text == "No")
            {
                return base.Content("No");
            }
            string productImageUrl = products.GetProductImageUrl(Globals.SafeLong(text, (long) 0L));
            if (productImageUrl == "No")
            {
                return base.Content("No");
            }
            return base.Content(text + "|" + productImageUrl);
        }

        public ActionResult AjaxGetUploadPhotoResult()
        {
            List<Maticsoft.Model.SNS.UserAlbums> userAblumsByUserID = new Maticsoft.BLL.SNS.UserAlbums().GetUserAblumsByUserID(base.currentUser.UserID);
            PhotoAlbum model = new PhotoAlbum();
            List<Maticsoft.Model.SNS.Categories> allList = Maticsoft.BLL.SNS.Categories.GetAllList(1);
            model.UserAlbums = userAblumsByUserID;
            model.PhotoCateList = (from c in allList
                where c.Depth == 1
                select c).ToList<Maticsoft.Model.SNS.Categories>();
            ((dynamic) base.ViewBag).ImageUrl = base.Request["image"];
            ((dynamic) base.ViewBag).ImageData = base.Request["data"];
            return this.PartialView(base.CurrentThemeViewPath + "/Partial/_UploadPhotoResultLayOut.cshtml", model);
        }

        public ActionResult AjaxGetUserId()
        {
            if (base.currentUser != null)
            {
                return base.Content(base.currentUser.UserID.ToString());
            }
            return base.Content("0");
        }

        public ActionResult AjaxJoinGroup(FormCollection fm)
        {
            Maticsoft.Model.SNS.GroupUsers model = new Maticsoft.Model.SNS.GroupUsers();
            Maticsoft.BLL.SNS.GroupUsers users2 = new Maticsoft.BLL.SNS.GroupUsers();
            int groupID = Globals.SafeInt(fm["GroupId"], 0);
            if (users2.Exists(groupID, base.currentUser.UserID))
            {
                return base.Content("joined");
            }
            model.GroupID = groupID;
            model.JoinTime = DateTime.Now;
            model.NickName = base.currentUser.NickName;
            model.UserID = base.currentUser.UserID;
            model.Status = 1;
            if (users2.AddEx(model))
            {
                return base.Content("Yes");
            }
            return base.Content("No");
        }

        public ActionResult AjaxPostAdd(FormCollection Fm)
        {
            if (base.currentUser == null)
            {
                return base.Content("NoLogin");
            }
            if (base.currentUser.UserType == "AA")
            {
                return base.Content("AA");
            }
            bool flag = Globals.SafeBool(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic"), false);
            string str = Fm["Type"];
            string str2 = Fm["ImageUrl"];
            string str3 = InjectionFilter.Filter(Fm["ShareDes"]);
            string str4 = Fm["VideoUrl"];
            string str5 = Fm["PostExUrl"];
            string str6 = Fm["AudioUrl"];
            string productName = Fm["ProductName"];
            int photoCateId = Globals.SafeInt(Fm["PhotoCateId"], 0);
            int ablumId = Globals.SafeInt(Fm["AblumId"], 0);
            long pid = Globals.SafeLong(Fm["Pid"], (long) 0L);
            string photoAddress = Fm["Address"];
            string mapLng = Fm["MapLng"];
            string mapLat = Fm["MapLat"];
            base.PostsModel.Description = str3;
            base.PostsModel.CreatedDate = DateTime.Now;
            base.PostsModel.CreatedNickName = base.currentUser.NickName;
            base.PostsModel.CreatedUserID = base.currentUser.UserID;
            base.PostsModel.ImageUrl = str2;
            base.PostsModel.UserIP = base.Request.UserHostAddress;
            base.PostsModel.AudioUrl = str6;
            if (!string.IsNullOrWhiteSpace(base.PostsModel.ImageUrl) && (str != "Product"))
            {
                string savePath = "/Upload/SNS/Images/Photos/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string saveThumbsPath = "/Upload/SNS/Images/PhotosThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                base.PostsModel.ImageUrl = Maticsoft.BLL.SNS.Photos.MoveImage(base.PostsModel.ImageUrl, savePath, saveThumbsPath);
            }
            if (str == "Product")
            {
                base.PostsModel.Type = 2;
                Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
                if (products.Exsit(productName, base.currentUser.UserID))
                {
                    return base.Content("ProductRepeat");
                }
            }
            else if (str == "Photo")
            {
                base.PostsModel.Type = 1;
            }
            else
            {
                base.PostsModel.Type = 0;
                if (!string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5))
                {
                    base.PostsModel.VideoUrl = str4;
                    base.PostsModel.PostExUrl = str5;
                }
            }
            base.PostsModel.Status = 1;
            base.PostsModel = base.PostsBll.AddPost(base.PostsModel, ablumId, pid, photoCateId, photoAddress, mapLng, mapLat, true);
            base.PostsModel.Description = ViewModelBase.RegexNickName(base.PostsModel.Description);
            Maticsoft.ViewModel.SNS.Posts item = new Maticsoft.ViewModel.SNS.Posts();
            if (!string.IsNullOrEmpty(base.PostsModel.ImageUrl) && (str != "Product"))
            {
                base.PostsModel.ImageUrl = base.PostsModel.ImageUrl.Split(new char[] { '|' })[0];
            }
            if (base.PostsModel.Type == 2)
            {
                Maticsoft.Model.SNS.Products modelByCache = new Maticsoft.BLL.SNS.Products().GetModelByCache((long) base.PostsModel.TargetId);
                if (flag && (modelByCache != null))
                {
                    base.PostsModel.Description = (string) base.PostsModel.Description.Replace("{ProductUrl}", (dynamic) string.IsNullOrWhiteSpace(modelByCache.StaticUrl) ? ((((dynamic) base.ViewBag).BasePath + "Product/Detail/") + base.PostsModel.TargetId) : modelByCache.StaticUrl);
                }
                else
                {
                    base.PostsModel.Description = (string) base.PostsModel.Description.Replace("{ProductUrl}", (((dynamic) base.ViewBag).BasePath + "Product/Detail/") + base.PostsModel.TargetId);
                }
            }
            item.Post = base.PostsModel;
            base.list.Add(item);
            return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", base.list);
        }

        public ActionResult AjaxPostForward(FormCollection Fm)
        {
            int postId = 0;
            string str = ViewModelBase.ReplaceFace(InjectionFilter.Filter(Fm["content"]));
            int num2 = Globals.SafeInt(Fm["origid"], 0);
            int num3 = Globals.SafeInt(Fm["origuserid"], 0);
            string str2 = Fm["orignickname"];
            int num4 = Globals.SafeInt(Fm["forwardid"], 0);
            if ((num2 == 0) || (num3 == 0))
            {
                return base.Content("No");
            }
            base.PostsModel.CreatedDate = DateTime.Now;
            base.PostsModel.CreatedNickName = base.currentUser.NickName;
            base.PostsModel.CreatedUserID = base.currentUser.UserID;
            base.PostsModel.Description = str;
            base.PostsModel.ForwardedID = new int?(num4);
            base.PostsModel.HasReferUsers = str.Contains("@");
            base.PostsModel.OriginalID = num2;
            base.PostsModel.Type = 0;
            postId = base.PostsBll.AddForwardPost(base.PostsModel);
            base.list = base.PostsBll.GetForPostByPostId(postId, base.IncludeProduct);
            Maticsoft.BLL.SNS.ReferUsers users = new Maticsoft.BLL.SNS.ReferUsers();
            Maticsoft.Model.SNS.ReferUsers model = new Maticsoft.Model.SNS.ReferUsers {
                CreatedDate = DateTime.Now,
                IsRead = false,
                ReferUserID = num3,
                ReferNickName = str2,
                Type = 0,
                TagetID = postId
            };
            users.Add(model);
            return this.PartialView(base.CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", base.list);
        }

        public void AjaxPostWeibo(FormCollection Fm)
        {
            UserBind bind = new UserBind();
            Maticsoft.BLL.SNS.Posts posts = new Maticsoft.BLL.SNS.Posts();
            Maticsoft.BLL.SNS.GroupTopics topics = new Maticsoft.BLL.SNS.GroupTopics();
            Maticsoft.BLL.SNS.GroupTopicReply reply = new Maticsoft.BLL.SNS.GroupTopicReply();
            if ((base.currentUser.UserType != "AA") && !string.IsNullOrWhiteSpace(Fm["MediaIds"]))
            {
                string mediaIDs = Fm["MediaIds"];
                string productName = Fm["ShareDes"];
                string str3 = Fm["ImageUrl"];
                string text1 = Fm["VideoRawUrl"];
                string url = Fm["Url"];
                string str5 = Fm["Type"];
                int targetId = Globals.SafeInt(Fm["TargetID"], 0);
                int postID = Globals.SafeInt(Fm["PostID"], 0);
                int topicID = Globals.SafeInt(Fm["TopicID"], 0);
                int num4 = Globals.SafeInt(Fm["AlumbID"], 0);
                int replyID = Globals.SafeInt(Fm["ReplyId"], 0);
                if (num4 > 0)
                {
                    url = string.Concat(new object[] { "http://", Globals.DomainFullName, "/Album/Details?AlbumID=", num4 });
                }
                if (postID > 0)
                {
                    Maticsoft.Model.SNS.Posts model = posts.GetModel(postID);
                    if (model != null)
                    {
                        if (model.Type == 1)
                        {
                            str5 = "Photo";
                            targetId = model.TargetId;
                            if (string.IsNullOrEmpty(productName))
                            {
                                productName = "分享图片";
                            }
                        }
                        else if (model.Type == 2)
                        {
                            str5 = "Product";
                            targetId = model.TargetId;
                            if (string.IsNullOrEmpty(productName))
                            {
                                productName = model.ProductName;
                            }
                        }
                        else if (model.Type == 0)
                        {
                            str5 = "Weibo";
                            productName = string.IsNullOrEmpty(model.Description) ? "分享图片" : model.Description;
                        }
                        str3 = model.ImageUrl.Replace("{0}", "T116x170_");
                    }
                }
                if (topicID > 0)
                {
                    url = string.Concat(new object[] { "http://", Globals.DomainFullName, "/Group/TopicReply?id=", topicID });
                    Maticsoft.Model.SNS.GroupTopics topics2 = topics.GetModel(topicID);
                    str3 = (topics2 != null) ? topics2.ImageUrl : "";
                }
                if (replyID > 0)
                {
                    url = string.Concat(new object[] { "http://", Globals.DomainFullName, "/Group/TopicReply?id=", topicID });
                    Maticsoft.Model.SNS.GroupTopicReply reply2 = reply.GetModel(replyID);
                    str3 = (reply2 != null) ? reply2.PhotoUrl : "";
                }
                if ((!string.IsNullOrEmpty(str3) && (str3.Split(new char[] { '|' }).Length > 2)) && (str5 != "Product"))
                {
                    str3 = "http://" + Globals.DomainFullName + str3.Split(new char[] { '|' })[0];
                }
                if (str5 == "Weibo")
                {
                    url = string.Concat(new object[] { "http://", Globals.DomainFullName, "/User/Posts/", base.currentUser.UserID });
                }
                else if ((str5 == "Photo") || (str5 == "Product"))
                {
                    url = string.Concat(new object[] { "http://", Globals.DomainFullName, "/Detail/", str5, "/", targetId });
                }
                try
                {
                    bind.SendWeiBo(base.currentUser.UserID, mediaIDs, productName, url, str3);
                }
                catch (Exception exception)
                {
                    base.Response.Write(exception);
                }
            }
        }

        public ActionResult AjaxTopicOperation(FormCollection fm)
        {
            int topicID = Globals.SafeInt(fm["TopicId"], 0);
            int recommand = Globals.SafeInt(fm["Type"], 0);
            Maticsoft.BLL.SNS.GroupTopics topics = new Maticsoft.BLL.SNS.GroupTopics();
            Maticsoft.Model.SNS.GroupTopics model = new Maticsoft.Model.SNS.GroupTopics();
            model = topics.GetModel(topicID);
            if (recommand < 2)
            {
                if (topics.UpdateRecommand(topicID, recommand))
                {
                    return base.Content("Ok");
                }
                return base.Content("No");
            }
            if (recommand != 2)
            {
                return null;
            }
            model.IsRecomend = 0;
            if (topics.DeleteEx(topicID))
            {
                return base.Content("Ok");
            }
            return base.Content("No");
        }

        [HttpPost]
        public ActionResult AjaxUnFellowUser(int UserId)
        {
            Maticsoft.BLL.SNS.UserShip ship = new Maticsoft.BLL.SNS.UserShip();
            if (ship.CancelAttention(base.CurrentUser.UserID, UserId))
            {
                return base.Content("True");
            }
            return base.Content("False");
        }

        public ActionResult AlbumEdit(int AlbumId)
        {
            ((dynamic) base.ViewBag).Title = "编辑专辑";
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbumsType type = new Maticsoft.BLL.SNS.UserAlbumsType();
            Maticsoft.BLL.SNS.AlbumType type2 = new Maticsoft.BLL.SNS.AlbumType();
            Maticsoft.Model.SNS.UserAlbums model = albums.GetModel(AlbumId);
            List<Maticsoft.Model.SNS.AlbumType> modelList = type2.GetModelList("Status=1");
            List<SelectListItem> list2 = new List<SelectListItem>();
            SelectListItem item2 = new SelectListItem {
                Value = "0",
                Text = "请选择"
            };
            list2.Add(item2);
            if ((modelList != null) && (modelList.Count > 0))
            {
                foreach (Maticsoft.Model.SNS.AlbumType type3 in modelList)
                {
                    SelectListItem item = new SelectListItem {
                        Value = type3.ID.ToString(),
                        Text = type3.TypeName
                    };
                    list2.Add(item);
                }
                ((dynamic) base.ViewBag).TypeList = list2;
            }
            if (model == null)
            {
                return new EmptyResult();
            }
            Maticsoft.Model.SNS.UserAlbumsType modelByUserId = type.GetModelByUserId(model.AlbumID, base.currentUser.UserID);
            model.TypeId = 0;
            if (modelByUserId != null)
            {
                model.TypeId = modelByUserId.TypeID;
            }
            return base.View(model);
        }

        [HttpPost]
        public ActionResult AlbumEdit(FormCollection fm)
        {
            ((dynamic) base.ViewBag).Title = "编辑专辑";
            Maticsoft.Model.SNS.UserAlbums model = new Maticsoft.Model.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbums albums2 = new Maticsoft.BLL.SNS.UserAlbums();
            model.AlbumName = fm["AlbumName"];
            model.Description = fm["Description"];
            model.AlbumID = Globals.SafeInt(fm["AlbumID"], 0);
            if (!albums2.UpdateEx(model))
            {
                return base.RedirectToAction("AlbumEdit", new { AlbumId = model.AlbumID });
            }
            int num = Globals.SafeInt(fm["TypeId"], 0);
            Maticsoft.BLL.SNS.UserAlbumsType type = new Maticsoft.BLL.SNS.UserAlbumsType();
            Maticsoft.Model.SNS.UserAlbumsType modelByUserId = type.GetModelByUserId(model.AlbumID, base.currentUser.UserID);
            if (modelByUserId == null)
            {
                modelByUserId = new Maticsoft.Model.SNS.UserAlbumsType {
                    TypeID = num,
                    AlbumsID = model.AlbumID,
                    AlbumsUserID = new int?(base.currentUser.UserID)
                };
                type.AddEx(modelByUserId);
            }
            else
            {
                modelByUserId.TypeID = num;
                type.UpdateType(modelByUserId);
            }
            return base.RedirectToAction("Albums");
        }

        [HttpPost, ValidateInput(false)]
        public void CheckVideoUrl(string VideoUrl)
        {
            string str = VideoUrl;
            JsonObject obj2 = new JsonObject();
            if (string.IsNullOrWhiteSpace(str))
            {
                obj2.Accumulate("STATUS", "NotNull");
            }
            else
            {
                int type = this.GetType(str);
                switch (type)
                {
                    case 0:
                        obj2.Accumulate("STATUS", "Error");
                        goto Label_011A;

                    case 1:
                    {
                        YouKuInfo youKuInfo = VideoHelper.GetYouKuInfo(str);
                        if (youKuInfo != null)
                        {
                            obj2.Accumulate("STATUS", "Succ");
                            obj2.Accumulate("VideoUrl", string.Format("http://player.youku.com/player.php/sid/{0}/v.swf", youKuInfo.VidEncoded));
                            obj2.Accumulate("ImageUrl", youKuInfo.Logo);
                            obj2.Accumulate("VideoTitle", youKuInfo.Title);
                        }
                        else
                        {
                            obj2.Accumulate("STATUS", "Error");
                        }
                        break;
                    }
                }
                if (type == 2)
                {
                    Ku6Info info2 = VideoHelper.GetKu6Info(str);
                    if (info2 != null)
                    {
                        obj2.Accumulate("STATUS", "Succ");
                        obj2.Accumulate("VideoUrl", info2.flash);
                        obj2.Accumulate("ImageUrl", info2.coverurl);
                        obj2.Accumulate("VideoTitle", info2.title);
                    }
                    else
                    {
                        obj2.Accumulate("STATUS", "Error");
                    }
                }
            }
        Label_011A:
            base.Response.Write(obj2.ToString());
        }

        private void DeletePhysicalFile(string path)
        {
            FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, path);
        }

        public int GetType(string url)
        {
            int num = 0;
            if (VideoHelper.IsYouKuVideoUrl(url))
            {
                num = 1;
            }
            if (VideoHelper.IsKu6VideoUrl(url))
            {
                num = 2;
            }
            return num;
        }

        public ActionResult GetUserId()
        {
            if (base.currentUser == null)
            {
                return base.Content("0");
            }
            return base.Content(base.currentUser.UserID.ToString());
        }

        public string GetVideoUrl(string url)
        {
            switch (this.GetType(url))
            {
                case 1:
                {
                    YouKuInfo youKuInfo = VideoHelper.GetYouKuInfo(url);
                    if (youKuInfo != null)
                    {
                        string str = "http://player.youku.com/player.php/sid/" + youKuInfo.VidEncoded + "/v.swf";
                        string logo = youKuInfo.Logo;
                        return (str + "," + logo);
                    }
                    break;
                }
                case 2:
                {
                    Ku6Info info2 = VideoHelper.GetKu6Info(url);
                    if (info2 != null)
                    {
                        string flash = info2.flash;
                        string coverurl = info2.coverurl;
                        return (flash + "," + coverurl);
                    }
                    break;
                }
            }
            return "";
        }

        public override bool LoadUserInfo(int UserID)
        {
            if (base.currentUser != null)
            {
                base.UserID = base.currentUser.UserID;
                base.DefaultPostType = Maticsoft.Model.SNS.EnumHelper.PostType.Fellow;
                base.UserModel = this.UserBll.GetUsersExpModel(base.UserID);
                base.IsCurrentUser = true;
                base.NickName = (base.UserModel != null) ? base.UserModel.NickName : "";
                base.Activity = base.currentUser.Activity;
                return true;
            }
            return false;
        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if ((dt.Rows[i]["TargetImageURL"] != null) && (dt.Rows[i]["TargetImageURL"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["TargetImageURL"].ToString());
                    }
                    if ((dt.Rows[i]["ThumbImageUrl"] != null) && (dt.Rows[i]["ThumbImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["ThumbImageUrl"].ToString());
                    }
                    if ((dt.Rows[i]["NormalImageUrl"] != null) && (dt.Rows[i]["NormalImageUrl"].ToString() != ""))
                    {
                        this.DeletePhysicalFile(dt.Rows[i]["NormalImageUrl"].ToString());
                    }
                }
            }
        }
    }
}


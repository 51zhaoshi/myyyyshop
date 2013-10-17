namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SNS;
    using Maticsoft.ViewModel.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;

    public class Photos
    {
        private readonly Maticsoft.BLL.SNS.Comments commentBll = new Maticsoft.BLL.SNS.Comments();
        private readonly IPhotos dal = DASNS.CreatePhotos();

        public int Add(Maticsoft.Model.SNS.Photos model)
        {
            return this.dal.Add(model);
        }

        public static string CreateIDCode()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            DateTime time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = (TimeSpan) (time - time2);
            return span.TotalMilliseconds.ToString("0");
        }

        public List<Maticsoft.Model.SNS.Photos> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Photos> list = new List<Maticsoft.Model.SNS.Photos>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Photos item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int PhotoID)
        {
            return this.dal.Delete(PhotoID);
        }

        public bool DeleteEX(int PhotoID)
        {
            return this.dal.DeleteEX(PhotoID);
        }

        public bool DeleteList(string PhotoIDlist)
        {
            return this.dal.DeleteList(PhotoIDlist);
        }

        public DataSet DeleteListEx(string Ids, out int Result, bool IsSendMess = false, int SendUserID = 1)
        {
            List<int> photoUserIds = this.GetPhotoUserIds(Ids);
            DataSet set = this.dal.DeleteListEx(Ids, out Result);
            if ((Result > 0) && IsSendMess)
            {
                SiteMessage message = new SiteMessage();
                foreach (int num in photoUserIds)
                {
                    message.AddMessageByUser(SendUserID, num, "图片删除", "您的图片涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
                }
            }
            return set;
        }

        public bool DeleteListEX(string PhotoIds)
        {
            return this.dal.DeleteListEX(PhotoIds);
        }

        public bool Exists(int PhotoId)
        {
            return this.dal.Exists(PhotoId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<PostContent> GetCachePhotoListByPage(int categoryId, string orderby, int startIndex, int endIndex)
        {
            string cacheKey = string.Concat(new object[] { "CachePhotoListByPage", categoryId, orderby, startIndex, endIndex });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetPhotoListByPage(categoryId, orderby, startIndex, endIndex);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<PostContent>) cache;
        }

        public int GetCountEx(int type, int categoryId, string address)
        {
            return this.dal.GetCountEx(type, categoryId, address);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public List<PostContent> GetListByKeyWord(string q, string orderby, int startIndex, int endIndex, string area = "")
        {
            DataSet set;
            new List<Maticsoft.Model.SNS.Photos>();
            StringBuilder builder = new StringBuilder();
            builder.Append(" Status =1 ");
            if (!string.IsNullOrEmpty(q))
            {
                builder.Append(" and (Tags Like '%" + q + "%' or  Description like '%" + q + "%')");
            }
            if (!string.IsNullOrEmpty(area))
            {
                builder.Append(" and (PhotoAddress like '%" + area + "%' )");
            }
            string str = orderby;
            if (str != null)
            {
                if (!(str == "popular"))
                {
                    if (str == "new")
                    {
                        orderby = "CreatedDate";
                        goto Label_00D0;
                    }
                    if (str == "hot")
                    {
                        orderby = "CommentCount";
                        goto Label_00D0;
                    }
                }
                else
                {
                    orderby = "FavouriteCount";
                    goto Label_00D0;
                }
            }
            orderby = "FavouriteCount";
        Label_00D0:
            set = this.dal.GetListByPage(builder.ToString(), orderby, startIndex, endIndex);
            List<PostContent> list = new List<PostContent>();
            List<string> values = new List<string>();
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            if ((set != null) && (set.Tables.Count > 0))
            {
                Action<PostContent> action = null;
                DataTable table = set.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    PostContent item = new PostContent {
                        Type = 0,
                        TargetId = Convert.ToInt32(row["PhotoID"]),
                        TargetName = row["PhotoName"].ToString(),
                        TargetDescription = (row["Description"] != null) ? row["Description"].ToString() : "",
                        CommentCount = Convert.ToInt32(row["CommentCount"]),
                        FavouriteCount = Convert.ToInt32(row["FavouriteCount"]),
                        ThumbImageUrl = row["ThumbImageUrl"].ToString(),
                        TopCommentsId = (row["TopCommentsId"] != null) ? row["TopCommentsId"].ToString() : "",
                        StaticUrl = (row["StaticUrl"] != null) ? row["StaticUrl"].ToString() : ""
                    };
                    list.Add(item);
                    if (!string.IsNullOrEmpty(item.TopCommentsId))
                    {
                        values.Add(item.TopCommentsId);
                    }
                }
                List<Maticsoft.Model.SNS.Comments> commentList = comments.GetCommentByIds(string.Join(",", values).TrimEnd(new char[] { ',' }), 1);
                if (commentList == null)
                {
                    return list;
                }
                if (action == null)
                {
                    action = delegate (PostContent img) {
                        List<Maticsoft.Model.SNS.Comments> list = commentList.FindAll(xx => (xx.TargetId == img.TargetId) && (xx.Type == 1));
                        if (list != null)
                        {
                            img.CommentList = list;
                        }
                    };
                }
                list.ForEach(action);
            }
            return list;
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPageEx(strWhere, CateId, orderby, startIndex, endIndex);
        }

        public DataSet GetListEx(string strWhere, int CateId)
        {
            return this.dal.GetListEx(strWhere, CateId);
        }

        public List<int> GetListToReGen(string strWhere)
        {
            DataSet listToReGen = this.dal.GetListToReGen(strWhere);
            List<int> list = new List<int>();
            if ((listToReGen != null) && (listToReGen.Tables.Count > 0))
            {
                for (int i = 0; i < listToReGen.Tables[0].Rows.Count; i++)
                {
                    if ((listToReGen.Tables[0].Rows[i]["PhotoID"] != null) && (listToReGen.Tables[0].Rows[i]["PhotoID"].ToString() != ""))
                    {
                        list.Add(int.Parse(listToReGen.Tables[0].Rows[i]["PhotoID"].ToString()));
                    }
                }
            }
            return list;
        }

        public Maticsoft.Model.SNS.Photos GetModel(int PhotoID)
        {
            return this.dal.GetModel(PhotoID);
        }

        public Maticsoft.Model.SNS.Photos GetModelByCache(int PhotoID)
        {
            string cacheKey = "PhotosModel-" + PhotoID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(PhotoID);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.SNS.Photos) cache;
        }

        public List<Maticsoft.Model.SNS.Photos> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetNextID(int PhotoId, int albumId = -1)
        {
            return this.dal.GetNextID(PhotoId, albumId);
        }

        public TargetDetail GetPhotoAssistionInfo(int pid, int type = -1)
        {
            TargetDetail detail = new TargetDetail();
            UsersExp exp = new UsersExp();
            Maticsoft.BLL.SNS.UserAlbums albums = new Maticsoft.BLL.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbumDetail detail2 = new Maticsoft.BLL.SNS.UserAlbumDetail();
            detail.Photo = this.GetModel(pid);
            detail.UserModel = exp.GetUsersExpModel(detail.Userid);
            detail.UserAlums = albums.GetUserAlbum(0, detail.TargetId, detail.Userid);
            if (detail.UserAlums != null)
            {
                detail.CovorImageList = detail2.GetThumbImageByAlbum(detail.UserAlums.AlbumID, type);
            }
            return detail;
        }

        public List<PostContent> GetPhotoListByPage(int categoryId, string orderby, int startIndex, int endIndex)
        {
            List<PostContent> list = new List<PostContent>();
            DataSet set = this.dal.GetListByPage(string.Format(" Status=1 and CategoryId = {0} ", categoryId), orderby, startIndex, endIndex);
            List<string> values = new List<string>();
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            if ((set != null) && (set.Tables.Count > 0))
            {
                Action<PostContent> action = null;
                DataTable table = set.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    PostContent item = new PostContent {
                        Type = 0,
                        TargetId = Convert.ToInt32(row["PhotoID"]),
                        TargetName = row["PhotoName"].ToString(),
                        TargetDescription = (row["Description"] != null) ? row["Description"].ToString() : "",
                        CommentCount = Convert.ToInt32(row["CommentCount"]),
                        FavouriteCount = Convert.ToInt32(row["FavouriteCount"]),
                        ThumbImageUrl = row["ThumbImageUrl"].ToString(),
                        TopCommentsId = (row["TopCommentsId"] != null) ? row["TopCommentsId"].ToString() : ""
                    };
                    list.Add(item);
                    if (!string.IsNullOrEmpty(item.TopCommentsId))
                    {
                        values.Add(item.TopCommentsId);
                    }
                }
                List<Maticsoft.Model.SNS.Comments> commentList = comments.GetCommentByIds(string.Join(",", values).TrimEnd(new char[] { ',' }), 1);
                if (commentList == null)
                {
                    return list;
                }
                if (action == null)
                {
                    action = delegate (PostContent img) {
                        List<Maticsoft.Model.SNS.Comments> list = commentList.FindAll(xx => (xx.TargetId == img.TargetId) && (xx.Type == 1));
                        if (list != null)
                        {
                            img.CommentList = list;
                        }
                    };
                }
                list.ForEach(action);
            }
            return list;
        }

        public List<PostContent> GetPhotoListByPage(int type, int categoryId, string address, string orderby, int startIndex, int endIndex)
        {
            List<PostContent> list = new List<PostContent>();
            DataSet set = this.dal.GetListByPageEx(type, categoryId, address, orderby, startIndex, endIndex);
            List<string> values = new List<string>();
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            if ((set != null) && (set.Tables.Count > 0))
            {
                Action<PostContent> action = null;
                DataTable table = set.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    PostContent item = new PostContent {
                        Type = 0,
                        TargetId = Convert.ToInt32(row["PhotoID"]),
                        TargetName = row["PhotoName"].ToString(),
                        TargetDescription = (row["Description"] != null) ? row["Description"].ToString() : "",
                        CommentCount = Convert.ToInt32(row["CommentCount"]),
                        FavouriteCount = Convert.ToInt32(row["FavouriteCount"]),
                        ThumbImageUrl = row["ThumbImageUrl"].ToString(),
                        TopCommentsId = (row["TopCommentsId"] != null) ? row["TopCommentsId"].ToString() : ""
                    };
                    list.Add(item);
                    if (!string.IsNullOrEmpty(item.TopCommentsId))
                    {
                        values.Add(item.TopCommentsId);
                    }
                }
                List<Maticsoft.Model.SNS.Comments> commentList = comments.GetCommentByIds(string.Join(",", values).TrimEnd(new char[] { ',' }), 1);
                if (commentList == null)
                {
                    return list;
                }
                if (action == null)
                {
                    action = delegate (PostContent img) {
                        List<Maticsoft.Model.SNS.Comments> list = commentList.FindAll(xx => (xx.TargetId == img.TargetId) && (xx.Type == 1));
                        if (list != null)
                        {
                            img.CommentList = list;
                        }
                    };
                }
                list.ForEach(action);
            }
            return list;
        }

        public List<PostContent> GetPhotoListByPageCache(int type, int categoryId, string address, string orderby, int startIndex, int endIndex)
        {
            string cacheKey = string.Concat(new object[] { "GetPhotoListByPageCache", type, categoryId, address, orderby, startIndex, endIndex });
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetPhotoListByPage(type, categoryId, address, orderby, startIndex, endIndex);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<PostContent>) cache;
        }

        public List<int> GetPhotoUserIds(string ids)
        {
            DataSet photoUserIds = this.dal.GetPhotoUserIds(ids);
            List<int> list = new List<int>();
            if ((photoUserIds != null) && (photoUserIds.Tables.Count > 0))
            {
                for (int i = 0; i < photoUserIds.Tables[0].Rows.Count; i++)
                {
                    if ((photoUserIds.Tables[0].Rows[i]["CreatedUserID"] != null) && (photoUserIds.Tables[0].Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        list.Add(int.Parse(photoUserIds.Tables[0].Rows[i]["CreatedUserID"].ToString()));
                    }
                }
            }
            return list;
        }

        public int GetPrevID(int PhotoId, int albumId = -1)
        {
            return this.dal.GetPrevID(PhotoId, albumId);
        }

        public List<Maticsoft.Model.SNS.Photos> GetRecommandByPid(int pid)
        {
            Maticsoft.Model.SNS.Photos model = this.GetModel(pid);
            List<Maticsoft.Model.SNS.Photos> list = new List<Maticsoft.Model.SNS.Photos>();
            new List<string>();
            if (model == null)
            {
                return null;
            }
            Action<Maticsoft.Model.SNS.Photos> action = null;
            list = this.DataTableToList(this.GetListByPage("Type=" + model.Type, "CommentCount desc", 1, 0x18).Tables[0]);
            string[] strArray = (from item in list
                where !string.IsNullOrEmpty(item.TopCommentsId)
                select item.TopCommentsId).Distinct<string>().ToArray<string>();
            string idStr = string.Join(",", strArray);
            List<Maticsoft.Model.SNS.Comments> commentList = this.commentBll.GetCommentByIds(idStr, 1);
            if (commentList != null)
            {
                if (action == null)
                {
                    action = delegate (Maticsoft.Model.SNS.Photos img) {
                        List<Maticsoft.Model.SNS.Comments> list = commentList.FindAll(xx => (xx.TargetId == img.PhotoID) && (xx.Type == 1));
                        if (list != null)
                        {
                            img.commmentList = list;
                        }
                    };
                }
                list.ForEach(action);
            }
            return list;
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCountEx(string strWhere, int CateId)
        {
            return this.dal.GetRecordCountEx(strWhere, CateId);
        }

        public int GetSearchCountByQ(string q)
        {
            return this.GetRecordCount(" Status =1 and Tags Like '%" + q + "%' or  Description like '%" + q + "%'");
        }

        public string GetThumByPhotoID(int id)
        {
            Maticsoft.Model.SNS.Photos model = this.GetModel(id);
            if (model != null)
            {
                return model.ThumbImageUrl;
            }
            return "";
        }

        public List<Maticsoft.Model.SNS.Photos> GetTopPhotoList(int Top, int Type)
        {
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("HomeGetValueType");
            string str2 = "";
            if (valueByCache != null)
            {
                int num = 1;
                if (valueByCache == num.ToString())
                {
                    str2 = "IsRecomend=" + 1;
                }
            }
            if (Type != -1)
            {
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    str2 = str2 + " and ";
                }
                str2 = str2 + "  Type=" + Type;
            }
            return this.DataTableToList(this.GetList(Top, str2, "PhotoID desc").Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Photos> GetTopPhotoPostByType(int top, int CategoryId)
        {
            return this.DataTableToList(this.GetListByPage(" Status=1 and  CategoryId=" + CategoryId, "CreatedDate desc", 0, top).Tables[0]);
        }

        public List<ZuiInPhoto> GetZuiInList(int CategoryId, int Top)
        {
            List<ZuiInPhoto> list = new List<ZuiInPhoto>();
            DataSet zuiInList = this.dal.GetZuiInList(CategoryId, Top);
            if ((zuiInList != null) && (zuiInList.Tables.Count > 0))
            {
                DataTable table = zuiInList.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    ZuiInPhoto item = new ZuiInPhoto {
                        AlbumsCount = Convert.ToInt32(row["AlbumsCount"]),
                        FansCount = Convert.ToInt32(row["FansCount"]),
                        NickName = row["NickName"].ToString(),
                        PhotoUrl = row["PhotoUrl"].ToString(),
                        PhotoId = Convert.ToInt32(row["PhotoId"]),
                        UserId = Convert.ToInt32(row["UserId"])
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public List<ZuiInPhoto> GetZuiInListByCache(int CategoryId, int Top)
        {
            string cacheKey = "GetZuiInListByCache" + CategoryId + Top;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.GetZuiInList(CategoryId, Top);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<ZuiInPhoto>) cache;
        }

        public static string MoveImage(string ImageUrl, string savePath, string saveThumbsPath)
        {
            try
            {
                if (Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
                {
                    return (ImageUrl + "|" + ImageUrl);
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                    }
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(saveThumbsPath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(saveThumbsPath));
                    }
                    List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, "");
                    string str = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string path = "";
                    string str3 = "";
                    string format = saveThumbsPath + str;
                    if (File.Exists(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, ""))))
                    {
                        str3 = string.Format(savePath + str, "");
                        File.Move(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(str3));
                    }
                    if ((thumSizeList != null) && (thumSizeList.Count > 0))
                    {
                        foreach (Maticsoft.Model.Ms.ThumbnailSize size in thumSizeList)
                        {
                            if (File.Exists(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, size.ThumName))))
                            {
                                path = string.Format(format, size.ThumName);
                                File.Move(HttpContext.Current.Server.MapPath(string.Format(ImageUrl, size.ThumName)), HttpContext.Current.Server.MapPath(path));
                            }
                        }
                    }
                    return (str3 + "|" + format);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        public bool Update(Maticsoft.Model.SNS.Photos model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCateList(string PhotoIds, int CateId)
        {
            return this.dal.UpdateCateList(PhotoIds, CateId);
        }

        public bool UpdatePvCount(int pid)
        {
            return this.dal.UpdatePvCount(pid);
        }

        public bool UpdateRecomend(int PhotoID, int Recomend)
        {
            return this.dal.UpdateRecomend(PhotoID, Recomend);
        }

        public bool UpdateRecomendList(string PhotoIds, int Recomend)
        {
            return this.dal.UpdateRecomendList(PhotoIds, Recomend);
        }

        public bool UpdateRecommandState(int id, int State)
        {
            return this.dal.UpdateRecommandState(id, State);
        }

        public bool UpdateStaticUrl(int photoId, string staticUrl)
        {
            return this.dal.UpdateStaticUrl(photoId, staticUrl);
        }

        public bool UpdateStatus(int PhotoID, int Status)
        {
            return this.dal.UpdateStatus(PhotoID, Status);
        }

        public List<Maticsoft.Model.SNS.Photos> UserUploadPhotoList(int ablumId)
        {
            DataSet set = this.dal.UserUploadPhoto(ablumId);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                return this.DataTableToList(set.Tables[0]);
            }
            return null;
        }
    }
}


namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public class UserAlbumDetail
    {
        private readonly IUserAlbumDetail dal = DASNS.CreateUserAlbumDetail();

        public int Add(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            return this.dal.AddEx(model);
        }

        public List<Maticsoft.Model.SNS.UserAlbumDetail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserAlbumDetail> list = new List<Maticsoft.Model.SNS.UserAlbumDetail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserAlbumDetail item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool Delete(int AlbumID, int TargetID, int Type)
        {
            return this.dal.Delete(AlbumID, TargetID, Type);
        }

        public bool DeleteEx(int AlbumID, int TargetId, int Type)
        {
            return this.dal.DeleteEx(AlbumID, TargetId, Type);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(int AlbumID, int TargetID, int Type)
        {
            return this.dal.Exists(AlbumID, TargetID, Type);
        }

        public List<PostContent> GetAlbumImgListByPage(int albumID, int startIndex, int endIndex, int type = -1)
        {
            List<PostContent> list = new List<PostContent>();
            DataSet set = this.dal.GetAlbumImgListByPage(albumID, "", startIndex, endIndex, type);
            List<string> values = new List<string>();
            Maticsoft.BLL.SNS.Comments comments = new Maticsoft.BLL.SNS.Comments();
            if ((set != null) && (set.Tables.Count > 0))
            {
                Action<PostContent> action = null;
                DataTable table = set.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    PostContent item = new PostContent {
                        TargetId = Convert.ToInt32(row["TargetID"]),
                        TargetName = row["TargetName"].ToString(),
                        TargetDescription = (row["Description"] != null) ? row["Description"].ToString() : "",
                        CommentCount = Convert.ToInt32(row["CommentCount"]),
                        FavouriteCount = Convert.ToInt32(row["FavouriteCount"]),
                        ThumbImageUrl = row["ThumbImageUrl"].ToString(),
                        Price = Globals.SafeDecimal(row["Price"].ToString(), (decimal) -1M),
                        Type = (int) row["Type"],
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
                        List<Maticsoft.Model.SNS.Comments> list = commentList.FindAll(xx => (xx.TargetId == img.TargetId) && (xx.Type == ((img.Type == 0) ? 1 : 2)));
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

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.SNS.UserAlbumDetail GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.SNS.UserAlbumDetail GetModelByCache(int ID)
        {
            string cacheKey = "UserAlbumDetailModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
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
            return (Maticsoft.Model.SNS.UserAlbumDetail) cache;
        }

        public List<Maticsoft.Model.SNS.UserAlbumDetail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public int GetRecordCount4AlbumImgByAlbumID(int albumID, int type = -1)
        {
            return this.dal.GetRecordCount4AlbumImgByAlbumID(albumID, type);
        }

        public List<string> GetThumbImageByAlbum(int AlbumID, int type = -1)
        {
            return this.dal.GetThumbImageByAlbum(AlbumID, type);
        }

        public bool Update(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            return this.dal.Update(model);
        }
    }
}


namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserFavourite
    {
        private readonly Maticsoft.BLL.SNS.Comments commentBll = new Maticsoft.BLL.SNS.Comments();
        private readonly IUserFavourite dal = DASNS.CreateUserFavourite();

        public int Add(Maticsoft.Model.SNS.UserFavourite model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(Maticsoft.Model.SNS.UserFavourite FavModel, int TopicId, int ReplyId)
        {
            return this.dal.AddEx(FavModel, TopicId, ReplyId);
        }

        public List<Maticsoft.Model.SNS.UserFavourite> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserFavourite> list = new List<Maticsoft.Model.SNS.UserFavourite>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserFavourite item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int FavouriteID)
        {
            return this.dal.Delete(FavouriteID);
        }

        public bool DeleteEx(int UserId, int TargetId, int Type)
        {
            return this.dal.DeleteEx(UserId, TargetId, Type);
        }

        public bool DeleteList(string FavouriteIDlist)
        {
            return this.dal.DeleteList(FavouriteIDlist);
        }

        public bool Exists(int CreatedUserID, int Type, int TargetID)
        {
            return this.dal.Exists(CreatedUserID, Type, TargetID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetFavCountByTargetId(int TargetId, int Type)
        {
            return this.GetRecordCount(string.Concat(new object[] { "TargetID=", TargetId, " and Type=", Type }));
        }

        public List<PostContent> GetFavListByPage(int UserId, string orderby, int startIndex, int endIndex)
        {
            List<PostContent> list = new List<PostContent>();
            new List<Maticsoft.Model.SNS.Comments>();
            DataSet set = this.dal.GetFavListByPage(UserId, "", startIndex, endIndex);
            List<string> values = new List<string>();
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
                        Price = Convert.ToDecimal(row["Price"]),
                        Type = (((int) row["Type"]) == 0) ? 0 : 1,
                        TopCommentsId = (row["TopCommentsId"] != null) ? row["TopCommentsId"].ToString() : ""
                    };
                    if (!string.IsNullOrEmpty(item.TopCommentsId))
                    {
                        values.Add(item.TopCommentsId);
                    }
                    list.Add(item);
                }
                List<Maticsoft.Model.SNS.Comments> commentList = this.commentBll.GetCommentByIds(string.Join(",", values).TrimEnd(new char[] { ',' }), 1);
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

        public List<Maticsoft.Model.SNS.UserFavourite> GetFavUserByTargetId(int TargetId, int Type, int Top)
        {
            return this.DataTableToList(this.GetListByPage(string.Concat(new object[] { "TargetID=", TargetId, " and Type=", Type }), "", 1, Top).Tables[0]);
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

        public Maticsoft.Model.SNS.UserFavourite GetModel(int FavouriteID)
        {
            return this.dal.GetModel(FavouriteID);
        }

        public Maticsoft.Model.SNS.UserFavourite GetModelByCache(int FavouriteID)
        {
            string cacheKey = "UserFavouriteModel-" + FavouriteID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(FavouriteID);
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
            return (Maticsoft.Model.SNS.UserFavourite) cache;
        }

        public List<Maticsoft.Model.SNS.UserFavourite> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SNS.UserFavourite model)
        {
            return this.dal.Update(model);
        }
    }
}


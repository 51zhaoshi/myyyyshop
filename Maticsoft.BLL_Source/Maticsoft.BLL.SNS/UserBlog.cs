namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserBlog
    {
        private readonly IUserBlog dal = DASNS.CreateUserBlog();

        public int Add(Maticsoft.Model.SNS.UserBlog model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.UserBlog> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserBlog> list = new List<Maticsoft.Model.SNS.UserBlog>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.UserBlog item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int BlogID)
        {
            return this.dal.Delete(BlogID);
        }

        public bool DeleteEx(int BlogID)
        {
            return this.dal.DeleteEx(BlogID);
        }

        public bool DeleteList(string BlogIDlist)
        {
            return this.dal.DeleteList(BlogIDlist);
        }

        public bool Exists(int BlogID)
        {
            return this.dal.Exists(BlogID);
        }

        public List<Maticsoft.Model.SNS.UserBlog> GetActiveUser(int top)
        {
            string cacheKey = "GetActiveUser-" + top;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    DataSet activeUser = this.dal.GetActiveUser(top);
                    List<Maticsoft.Model.SNS.UserBlog> list = new List<Maticsoft.Model.SNS.UserBlog>();
                    int count = activeUser.Tables[0].Rows.Count;
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            Maticsoft.Model.SNS.UserBlog item = new Maticsoft.Model.SNS.UserBlog();
                            DataRow row = activeUser.Tables[0].Rows[i];
                            if (row != null)
                            {
                                if ((row["UserID"] != null) && (row["UserID"].ToString() != ""))
                                {
                                    item.UserID = int.Parse(row["UserID"].ToString());
                                }
                                if (row["UserName"] != null)
                                {
                                    item.UserName = row["UserName"].ToString();
                                }
                            }
                            if (item != null)
                            {
                                list.Add(item);
                            }
                        }
                    }
                    cache = list;
                    if (cache != null)
                    {
                        int num3 = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num3), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (List<Maticsoft.Model.SNS.UserBlog>) cache;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public List<Maticsoft.Model.SNS.UserBlog> GetHotBlogList(int top)
        {
            string strWhere = " Status=1";
            DataSet set = this.dal.GetList(top, strWhere, " PvCount desc");
            return this.DataTableToList(set.Tables[0]);
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.SNS.UserBlog GetModel(int BlogID)
        {
            return this.dal.GetModel(BlogID);
        }

        public Maticsoft.Model.SNS.UserBlog GetModelByCache(int BlogID)
        {
            string cacheKey = "UserBlogModel-" + BlogID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(BlogID);
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
            return (Maticsoft.Model.SNS.UserBlog) cache;
        }

        public List<Maticsoft.Model.SNS.UserBlog> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.UserBlog> GetMoreList(int userId, int blogId, int top)
        {
            string strWhere = " Status=1 ";
            if (userId > 0)
            {
                strWhere = strWhere + " and UserID=" + userId;
            }
            if (blogId > 0)
            {
                strWhere = strWhere + " and BlogID<> " + blogId;
            }
            DataSet set = this.dal.GetList(top, strWhere, " CreatedDate desc");
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetPvCount(int id)
        {
            return this.dal.GetPvCount(id);
        }

        public List<Maticsoft.Model.SNS.UserBlog> GetRecBlogList(int top)
        {
            string strWhere = " Status=1 and Recomend=1";
            DataSet set = this.dal.GetList(top, strWhere, " PvCount desc");
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.SNS.UserBlog> GetUserBlogPage(string strWhere, string orderby, int StartIndex, int EndIndex)
        {
            List<Maticsoft.Model.SNS.UserBlog> list = new List<Maticsoft.Model.SNS.UserBlog>();
            return this.DataTableToList(this.GetListByPage(strWhere, orderby, StartIndex, EndIndex).Tables[0]);
        }

        public bool Update(Maticsoft.Model.SNS.UserBlog model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateCommentCount(int id)
        {
            return this.dal.UpdateCommentCount(id);
        }

        public bool UpdateFavCount(int id)
        {
            return this.dal.UpdateFavCount(id);
        }

        public bool UpdatePvCount(int id)
        {
            return this.dal.UpdatePvCount(id);
        }

        public bool UpdateRec(int id, int Rec)
        {
            return this.dal.UpdateRec(id, Rec);
        }

        public bool UpdateRecList(string ids, int Rec)
        {
            return this.dal.UpdateRecList(ids, Rec);
        }

        public bool UpdateStatusList(string ids, int Status)
        {
            return this.dal.UpdateStatusList(ids, Status);
        }
    }
}


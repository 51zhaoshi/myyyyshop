namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserRank
    {
        private readonly IUserRank dal = DAMembers.CreateUserRank();

        public int Add(Maticsoft.Model.Members.UserRank model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.UserRank> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UserRank> list = new List<Maticsoft.Model.Members.UserRank>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.UserRank item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int RankId)
        {
            return this.dal.Delete(RankId);
        }

        public bool DeleteList(string RankIdlist)
        {
            return this.dal.DeleteList(RankIdlist);
        }

        public bool Exists(int RankId)
        {
            return this.dal.Exists(RankId);
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

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Members.UserRank GetModel(int RankId)
        {
            return this.dal.GetModel(RankId);
        }

        public Maticsoft.Model.Members.UserRank GetModelByCache(int RankId)
        {
            string cacheKey = "UserRankModel-" + RankId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RankId);
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(ConfigSystem.GetValueByCache("ModelCache"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Members.UserRank) cache;
        }

        public List<Maticsoft.Model.Members.UserRank> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public string GetUserLevel(int? grades)
        {
            if (grades.HasValue)
            {
                string userLevel = this.dal.GetUserLevel(grades.Value);
                if (!string.IsNullOrWhiteSpace(userLevel))
                {
                    return userLevel;
                }
            }
            return "0";
        }

        public bool Update(Maticsoft.Model.Members.UserRank model)
        {
            return this.dal.Update(model);
        }
    }
}


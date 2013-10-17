namespace Maticsoft.BLL.Members
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class UserInvite
    {
        private readonly IUserInvite dal = DAMembers.CreateUserInvite();

        public int Add(Maticsoft.Model.Members.UserInvite model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.UserInvite> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UserInvite> list = new List<Maticsoft.Model.Members.UserInvite>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.UserInvite item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int InviteId)
        {
            return this.dal.Delete(InviteId);
        }

        public bool DeleteList(string InviteIdlist)
        {
            return this.dal.DeleteList(InviteIdlist);
        }

        public bool Exists(int InviteId)
        {
            return this.dal.Exists(InviteId);
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

        public List<Maticsoft.Model.Members.UserInvite> GetListByPage(string strWhere, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage(strWhere, "  InviteId desc ", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public List<Maticsoft.Model.Members.UserInvite> GetListByUserId(int userId)
        {
            return this.GetModelList("InviteUserId=" + userId);
        }

        public int GetMaxId()
        {
            return this.dal.GetMaxId();
        }

        public Maticsoft.Model.Members.UserInvite GetModel(int InviteId)
        {
            return this.dal.GetModel(InviteId);
        }

        public Maticsoft.Model.Members.UserInvite GetModelByCache(int InviteId)
        {
            string cacheKey = "UserInviteModel-" + InviteId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(InviteId);
                    if (cache != null)
                    {
                        int configInt = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) configInt), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (Maticsoft.Model.Members.UserInvite) cache;
        }

        public List<Maticsoft.Model.Members.UserInvite> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Members.UserInvite model)
        {
            return this.dal.Update(model);
        }
    }
}


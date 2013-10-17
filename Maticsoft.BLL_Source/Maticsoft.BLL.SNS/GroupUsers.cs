namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class GroupUsers
    {
        private readonly IGroupUsers dal = DASNS.CreateGroupUsers();

        public bool Add(Maticsoft.Model.SNS.GroupUsers model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(Maticsoft.Model.SNS.GroupUsers model)
        {
            return this.dal.AddEx(model);
        }

        public List<Maticsoft.Model.SNS.GroupUsers> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GroupUsers> list = new List<Maticsoft.Model.SNS.GroupUsers>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.GroupUsers item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int GroupID, int UserID)
        {
            return this.dal.Delete(GroupID, UserID);
        }

        public bool DeleteEx(int GroupId, int UserID)
        {
            return this.dal.DeleteEx(GroupId, UserID);
        }

        public bool DeleteEx(int GroupId, string UserIDs)
        {
            return this.dal.DeleteEx(GroupId, UserIDs);
        }

        public bool Exists(int GroupID, int UserID)
        {
            return this.dal.Exists(GroupID, UserID);
        }

        public List<Maticsoft.Model.SNS.GroupUsers> GetAdminUserList(int GroupId)
        {
            return this.DataTableToList(this.GetList(-1, "GroupID=" + GroupId + "AND Role in (2,1)", "Role desc,JoinTime desc").Tables[0]);
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

        public Maticsoft.Model.SNS.GroupUsers GetModel(int GroupID, int UserID)
        {
            return this.dal.GetModel(GroupID, UserID);
        }

        public Maticsoft.Model.SNS.GroupUsers GetModelByCache(int GroupID, int UserID)
        {
            string cacheKey = "GroupUsersModel-" + GroupID + UserID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(GroupID, UserID);
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
            return (Maticsoft.Model.SNS.GroupUsers) cache;
        }

        public List<Maticsoft.Model.SNS.GroupUsers> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.GroupUsers> GetNewUserListByGroup(int GroupId, int Top)
        {
            return this.DataTableToList(this.GetListByPage("GroupID=" + GroupId, "JoinTime Desc", 0, Top).Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.SNS.GroupUsers> GetUserList(int GroupId, int startIndex, int endIndex)
        {
            return this.DataTableToList(this.GetListByPage("GroupID=" + GroupId + "AND Role = 0", "Role desc,JoinTime desc", startIndex, endIndex).Tables[0]);
        }

        public bool Update(Maticsoft.Model.SNS.GroupUsers model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateRecommand(int GroupID, int UserID, int Recommand)
        {
            return this.dal.UpdateRecommand(GroupID, UserID, Recommand);
        }

        public bool UpdateRole(int GroupID, int UserID, int Role)
        {
            return this.dal.UpdateRole(GroupID, UserID, Role);
        }

        public bool UpdateStatus(int GroupID, int UserID, int Status)
        {
            return this.dal.UpdateStatus(GroupID, UserID, Status);
        }

        public bool UpdateStatusByTopicIds(string Ids, int Status)
        {
            return this.dal.UpdateStatusByTopicIds(Ids, Status);
        }

        public bool UpdateStatusByTopicReplyIds(string Ids, int Status)
        {
            return this.dal.UpdateStatusByTopicReplyIds(Ids, Status);
        }
    }
}


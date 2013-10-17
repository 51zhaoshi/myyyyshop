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

    public class Groups
    {
        private readonly IGroups dal = DASNS.CreateGroups();

        public int Add(Maticsoft.Model.SNS.Groups model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SNS.Groups> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Groups> list = new List<Maticsoft.Model.SNS.Groups>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SNS.Groups item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(int GroupID)
        {
            return this.dal.Delete(GroupID);
        }

        public bool DeleteList(string GroupIDlist)
        {
            return this.dal.DeleteList(GroupIDlist);
        }

        public bool DeleteListEx(string GroupIDlist)
        {
            return this.dal.DeleteListEx(GroupIDlist);
        }

        public bool Exists(string GroupName)
        {
            return this.dal.Exists(GroupName);
        }

        public bool Exists4Ignore(string GroupName, int groupId)
        {
            return this.dal.Exists(GroupName, groupId);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public int GetCountByKeyWord(string q, int rec = -1)
        {
            string str = "";
            if (rec != -1)
            {
                str = " IsRecommand=" + rec;
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    str = str + " and ";
                }
                string str2 = str;
                str = str2 + " (GroupName like '%" + q + "%' or GroupDescription Like '%" + q + "%' or Tags like '%" + q + "%')";
            }
            return this.GetRecordCount(str);
        }

        public List<Maticsoft.Model.SNS.Groups> GetGroupListByKeyWord(string q)
        {
            return this.GetModelList("GroupName like '%" + q + "%'");
        }

        public List<Maticsoft.Model.SNS.Groups> GetGroupListByKeyWord(int startIndex, int endIndex, string Sequence, string q, int rec = -1)
        {
            string str = "";
            if (rec != -1)
            {
                str = " IsRecommand=" + rec;
            }
            if (!string.IsNullOrWhiteSpace(q))
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    str = str + " and ";
                }
                string str2 = str;
                str = str2 + " (GroupName like '%" + q + "%' or GroupDescription Like '%" + q + "%' or Tags like '%" + q + "%')";
            }
            return this.DataTableToList(this.GetListByPage(str, "", startIndex, endIndex).Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Groups> GetGroupListByRecommendType(int Top, EnumHelper.GroupRecommend Type)
        {
            return this.DataTableToList(this.GetListByPage("IsRecommand=" + ((int) Type), "", 0, Top).Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Groups> GetHotGroupList(int Top)
        {
            return this.DataTableToList(this.GetListByPage("", "TopicCount Desc", 0, Top).Tables[0]);
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

        public Maticsoft.Model.SNS.Groups GetModel(int GroupID)
        {
            return this.dal.GetModel(GroupID);
        }

        public Maticsoft.Model.SNS.Groups GetModelByCache(int GroupID)
        {
            string cacheKey = "GroupsModel-" + GroupID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(GroupID);
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
            return (Maticsoft.Model.SNS.Groups) cache;
        }

        public List<Maticsoft.Model.SNS.Groups> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public List<Maticsoft.Model.SNS.Groups> GetTopList(int Top, string strWhere, string filedOrder)
        {
            DataSet set = this.dal.GetList(Top, strWhere, filedOrder);
            return this.DataTableToList(set.Tables[0]);
        }

        public List<Maticsoft.Model.SNS.Groups> GetUserJoinGroup(int UserId, int Top)
        {
            return this.GetModelList(string.Concat(new object[] { " GroupID IN (SELECT TOP ", Top, " GroupID FROM SNS_GroupUsers WHERE UserID =", UserId, ")" }));
        }

        public bool Update(Maticsoft.Model.SNS.Groups model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateRecommand(int GroupId, int Recommand)
        {
            return this.dal.UpdateRecommand(GroupId, Recommand);
        }

        public bool UpdateStatusList(string IdsStr, EnumHelper.GroupStatus status)
        {
            return this.dal.UpdateStatusList(IdsStr, status);
        }
    }
}


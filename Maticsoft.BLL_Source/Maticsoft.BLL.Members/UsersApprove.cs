namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class UsersApprove
    {
        private readonly IUsersApprove dal = DAMembers.CreateUsersApprove();

        public int Add(Maticsoft.Model.Members.UsersApprove model)
        {
            return this.dal.Add(model);
        }

        public bool BatchUpdate(string ids, string status)
        {
            return this.dal.BatchUpdate(ids, status);
        }

        public List<Maticsoft.Model.Members.UsersApprove> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UsersApprove> list = new List<Maticsoft.Model.Members.UsersApprove>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.UsersApprove item = new Maticsoft.Model.Members.UsersApprove();
                    if ((dt.Rows[i]["ApproveID"] != null) && (dt.Rows[i]["ApproveID"].ToString() != ""))
                    {
                        item.ApproveID = int.Parse(dt.Rows[i]["ApproveID"].ToString());
                    }
                    if ((dt.Rows[i]["UserID"] != null) && (dt.Rows[i]["UserID"].ToString() != ""))
                    {
                        item.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                    }
                    if ((dt.Rows[i]["TrueName"] != null) && (dt.Rows[i]["TrueName"].ToString() != ""))
                    {
                        item.TrueName = dt.Rows[i]["TrueName"].ToString();
                    }
                    if ((dt.Rows[i]["IDCardNum"] != null) && (dt.Rows[i]["IDCardNum"].ToString() != ""))
                    {
                        item.IDCardNum = dt.Rows[i]["IDCardNum"].ToString();
                    }
                    if ((dt.Rows[i]["FrontView"] != null) && (dt.Rows[i]["FrontView"].ToString() != ""))
                    {
                        item.FrontView = dt.Rows[i]["FrontView"].ToString();
                    }
                    if ((dt.Rows[i]["RearView"] != null) && (dt.Rows[i]["RearView"].ToString() != ""))
                    {
                        item.RearView = dt.Rows[i]["RearView"].ToString();
                    }
                    if ((dt.Rows[i]["DueDate"] != null) && (dt.Rows[i]["DueDate"].ToString() != ""))
                    {
                        item.DueDate = new DateTime?(DateTime.Parse(dt.Rows[i]["DueDate"].ToString()));
                    }
                    if ((dt.Rows[i]["Status"] != null) && (dt.Rows[i]["Status"].ToString() != ""))
                    {
                        item.Status = int.Parse(dt.Rows[i]["Status"].ToString());
                    }
                    if ((dt.Rows[i]["ApproveUserID"] != null) && (dt.Rows[i]["ApproveUserID"].ToString() != ""))
                    {
                        item.ApproveUserID = int.Parse(dt.Rows[i]["ApproveUserID"].ToString());
                    }
                    if ((dt.Rows[i]["UserType"] != null) && (dt.Rows[i]["UserType"].ToString() != ""))
                    {
                        item.UserType = new int?(int.Parse(dt.Rows[i]["UserType"].ToString()));
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                    }
                    if ((dt.Rows[i]["ApproveDate"] != null) && (dt.Rows[i]["ApproveDate"].ToString() != ""))
                    {
                        item.ApproveDate = new DateTime?(DateTime.Parse(dt.Rows[i]["ApproveDate"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ApproveID)
        {
            return this.dal.Delete(ApproveID);
        }

        public bool DeleteByUserId(int userId)
        {
            return this.dal.DeleteByUserId(userId);
        }

        public bool DeleteList(string ApproveIDlist)
        {
            return this.dal.DeleteList(ApproveIDlist);
        }

        public bool Exists(int ApproveID)
        {
            return this.dal.Exists(ApproveID);
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataSet GetApproveList(string status, string trueName)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(status) && !string.IsNullOrWhiteSpace(trueName))
            {
                builder.Append(" WHERE ");
                builder.AppendFormat(" Status ={0} AND UA.TrueName  like '%{1}%'", status, trueName);
            }
            else if (!string.IsNullOrWhiteSpace(status))
            {
                builder.Append(" WHERE ");
                builder.AppendFormat(" Status ={0} ", status);
            }
            else if (!string.IsNullOrWhiteSpace(trueName))
            {
                builder.Append(" WHERE ");
                builder.AppendFormat(" UA.TrueName  like '%{0}%'", trueName);
            }
            return this.dal.GetApproveList(builder.ToString());
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

        public Maticsoft.Model.Members.UsersApprove GetModel(int ApproveID)
        {
            return this.dal.GetModel(ApproveID);
        }

        public Maticsoft.Model.Members.UsersApprove GetModelByCache(int ApproveID)
        {
            string cacheKey = "UsersApproveModel-" + ApproveID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ApproveID);
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
            return (Maticsoft.Model.Members.UsersApprove) cache;
        }

        public Maticsoft.Model.Members.UsersApprove GetModelByUserID(int UserID)
        {
            return this.dal.GetModelByUserID(UserID);
        }

        public List<Maticsoft.Model.Members.UsersApprove> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Members.UsersApprove model)
        {
            return this.dal.Update(model);
        }
    }
}


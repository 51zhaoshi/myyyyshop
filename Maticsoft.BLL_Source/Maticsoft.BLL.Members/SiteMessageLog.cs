namespace Maticsoft.BLL.Members
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class SiteMessageLog
    {
        private readonly ISiteMessageLog dal = DAMembers.CreateSiteMessageLog();

        public int Add(Maticsoft.Model.Members.SiteMessageLog model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Members.SiteMessageLog> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.SiteMessageLog> list = new List<Maticsoft.Model.Members.SiteMessageLog>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Members.SiteMessageLog item = new Maticsoft.Model.Members.SiteMessageLog();
                    if ((dt.Rows[i]["ID"] != null) && (dt.Rows[i]["ID"].ToString() != ""))
                    {
                        item.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    }
                    if ((dt.Rows[i]["MessageID"] != null) && (dt.Rows[i]["MessageID"].ToString() != ""))
                    {
                        item.MessageID = new int?(int.Parse(dt.Rows[i]["MessageID"].ToString()));
                    }
                    if ((dt.Rows[i]["MessageType"] != null) && (dt.Rows[i]["MessageType"].ToString() != ""))
                    {
                        item.MessageType = dt.Rows[i]["MessageType"].ToString();
                    }
                    if ((dt.Rows[i]["MessageState"] != null) && (dt.Rows[i]["MessageState"].ToString() != ""))
                    {
                        item.MessageState = dt.Rows[i]["MessageState"].ToString();
                    }
                    if ((dt.Rows[i]["ReceiverID"] != null) && (dt.Rows[i]["ReceiverID"].ToString() != ""))
                    {
                        item.ReceiverID = new int?(int.Parse(dt.Rows[i]["ReceiverID"].ToString()));
                    }
                    if ((dt.Rows[i]["Ext1"] != null) && (dt.Rows[i]["Ext1"].ToString() != ""))
                    {
                        item.Ext1 = dt.Rows[i]["Ext1"].ToString();
                    }
                    if ((dt.Rows[i]["Ext2"] != null) && (dt.Rows[i]["Ext2"].ToString() != ""))
                    {
                        item.Ext2 = dt.Rows[i]["Ext2"].ToString();
                    }
                    if ((dt.Rows[i]["ReceiverUserName"] != null) && (dt.Rows[i]["ReceiverUserName"].ToString() != ""))
                    {
                        item.ReceiverUserName = dt.Rows[i]["ReceiverUserName"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ID)
        {
            return this.dal.Delete(ID);
        }

        public bool DeleteList(string IDlist)
        {
            return this.dal.DeleteList(IDlist);
        }

        public bool Exists(int ID)
        {
            return this.dal.Exists(ID);
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

        public Maticsoft.Model.Members.SiteMessageLog GetModel(int ID)
        {
            return this.dal.GetModel(ID);
        }

        public Maticsoft.Model.Members.SiteMessageLog GetModelByCache(int ID)
        {
            string cacheKey = "SiteMessageLogModel-" + ID;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(ID);
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
            return (Maticsoft.Model.Members.SiteMessageLog) cache;
        }

        public List<Maticsoft.Model.Members.SiteMessageLog> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Members.SiteMessageLog model)
        {
            return this.dal.Update(model);
        }
    }
}


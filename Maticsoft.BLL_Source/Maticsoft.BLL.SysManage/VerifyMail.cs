namespace Maticsoft.BLL.SysManage
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class VerifyMail
    {
        private readonly IVerifyMail dal = DASysManage.CreateVerifyMail();

        public bool Add(Maticsoft.Model.SysManage.VerifyMail model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.SysManage.VerifyMail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SysManage.VerifyMail> list = new List<Maticsoft.Model.SysManage.VerifyMail>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.SysManage.VerifyMail item = new Maticsoft.Model.SysManage.VerifyMail();
                    if ((dt.Rows[i]["UserName"] != null) && (dt.Rows[i]["UserName"].ToString() != ""))
                    {
                        item.UserName = dt.Rows[i]["UserName"].ToString();
                    }
                    if ((dt.Rows[i]["KeyValue"] != null) && (dt.Rows[i]["KeyValue"].ToString() != ""))
                    {
                        item.KeyValue = dt.Rows[i]["KeyValue"].ToString();
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                    }
                    if ((dt.Rows[i]["Status"] != null) && (dt.Rows[i]["Status"].ToString() != ""))
                    {
                        item.Status = int.Parse(dt.Rows[i]["Status"].ToString());
                    }
                    if ((dt.Rows[i]["ValidityType"] != null) && (dt.Rows[i]["ValidityType"].ToString() != ""))
                    {
                        item.ValidityType = new int?(int.Parse(dt.Rows[i]["ValidityType"].ToString()));
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(string KeyValue)
        {
            return this.dal.Delete(KeyValue);
        }

        public bool DeleteList(string KeyValuelist)
        {
            return this.dal.DeleteList(KeyValuelist);
        }

        public bool Exists(string KeyValue)
        {
            return this.dal.Exists(KeyValue);
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

        public Maticsoft.Model.SysManage.VerifyMail GetModel(string KeyValue)
        {
            return this.dal.GetModel(KeyValue);
        }

        public Maticsoft.Model.SysManage.VerifyMail GetModelByCache(string KeyValue)
        {
            string cacheKey = "VerifyMailModel-" + KeyValue;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(KeyValue);
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
            return (Maticsoft.Model.SysManage.VerifyMail) cache;
        }

        public List<Maticsoft.Model.SysManage.VerifyMail> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.SysManage.VerifyMail model)
        {
            return this.dal.Update(model);
        }
    }
}


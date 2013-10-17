namespace Maticsoft.BLL.Pay
{
    using Maticsoft.Common;
    using Maticsoft.IDAL.Pay;
    using Maticsoft.Model.Pay;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BalanceDetails
    {
        private readonly IBalanceDetails dal = DAPay.CreateBalanceDetails();

        public long Add(Maticsoft.Model.Pay.BalanceDetails model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Pay.BalanceDetails> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Pay.BalanceDetails> list = new List<Maticsoft.Model.Pay.BalanceDetails>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Pay.BalanceDetails item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long JournalNumber)
        {
            return this.dal.Delete(JournalNumber);
        }

        public bool DeleteList(string JournalNumberlist)
        {
            return this.dal.DeleteList(JournalNumberlist);
        }

        public bool Exists(long JournalNumber)
        {
            return this.dal.Exists(JournalNumber);
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

        public List<Maticsoft.Model.Pay.BalanceDetails> GetListByPage(string strWhere, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage(strWhere, " JournalNumber desc  ", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public Maticsoft.Model.Pay.BalanceDetails GetModel(long JournalNumber)
        {
            return this.dal.GetModel(JournalNumber);
        }

        public Maticsoft.Model.Pay.BalanceDetails GetModelByCache(long JournalNumber)
        {
            string cacheKey = "BalanceDetailsModel-" + JournalNumber;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(JournalNumber);
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
            return (Maticsoft.Model.Pay.BalanceDetails) cache;
        }

        public List<Maticsoft.Model.Pay.BalanceDetails> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Pay.BalanceDetails model)
        {
            return this.dal.Update(model);
        }
    }
}


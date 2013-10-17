namespace Maticsoft.BLL.Pay
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Pay;
    using Maticsoft.Model.Pay;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BalanceDrawRequest
    {
        private readonly IBalanceDrawRequest dal = DAPay.CreateBalanceDrawRequest();

        public long Add(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            return this.dal.Add(model);
        }

        public bool AddEx(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            decimal userBalance = new UsersExp().GetUserBalance(model.UserID);
            return this.dal.AddEx(model, userBalance);
        }

        public List<Maticsoft.Model.Pay.BalanceDrawRequest> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Pay.BalanceDrawRequest> list = new List<Maticsoft.Model.Pay.BalanceDrawRequest>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Pay.BalanceDrawRequest item = this.dal.DataRowToModel(dt.Rows[i]);
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

        public decimal GetBalanceDraw(int userid)
        {
            return this.dal.GetBalanceDraw(userid, 1);
        }

        public DataSet GetList(string strWhere)
        {
            return this.dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return this.dal.GetList(Top, strWhere, filedOrder);
        }

        public List<Maticsoft.Model.Pay.BalanceDrawRequest> GetListByPage(string strWhere, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage(strWhere, " JournalNumber desc", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public DataSet GetListEx(string strWhere, string filedOrder)
        {
            return this.dal.GetListEx(strWhere, filedOrder);
        }

        public Maticsoft.Model.Pay.BalanceDrawRequest GetModel(long JournalNumber)
        {
            return this.dal.GetModel(JournalNumber);
        }

        public Maticsoft.Model.Pay.BalanceDrawRequest GetModelByCache(long JournalNumber)
        {
            string cacheKey = "BalanceDrawRequestModel-" + JournalNumber;
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
            return (Maticsoft.Model.Pay.BalanceDrawRequest) cache;
        }

        public List<Maticsoft.Model.Pay.BalanceDrawRequest> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Pay.BalanceDrawRequest model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStatus(string JournalNumberlist, int Status)
        {
            return this.dal.UpdateStatus(JournalNumberlist, Status);
        }
    }
}


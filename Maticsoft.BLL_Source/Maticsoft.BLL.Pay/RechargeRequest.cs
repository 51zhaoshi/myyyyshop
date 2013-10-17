namespace Maticsoft.BLL.Pay
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.IDAL.Pay;
    using Maticsoft.Model.Pay;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class RechargeRequest
    {
        private readonly IRechargeRequest dal = DAPay.CreateRechargeRequest();

        public long Add(Maticsoft.Model.Pay.RechargeRequest model)
        {
            return this.dal.Add(model);
        }

        public List<Maticsoft.Model.Pay.RechargeRequest> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Pay.RechargeRequest> list = new List<Maticsoft.Model.Pay.RechargeRequest>();
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.Pay.RechargeRequest item = this.dal.DataRowToModel(dt.Rows[i]);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public bool Delete(long RechargeId)
        {
            return this.dal.Delete(RechargeId);
        }

        public bool DeleteList(string RechargeIdlist)
        {
            return this.dal.DeleteList(RechargeIdlist);
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

        public Maticsoft.Model.Pay.RechargeRequest GetModel(long RechargeId)
        {
            return this.dal.GetModel(RechargeId);
        }

        public Maticsoft.Model.Pay.RechargeRequest GetModelByCache(long RechargeId)
        {
            string cacheKey = "RechargeRequestModel-" + RechargeId;
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    cache = this.dal.GetModel(RechargeId);
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
            return (Maticsoft.Model.Pay.RechargeRequest) cache;
        }

        public List<Maticsoft.Model.Pay.RechargeRequest> GetModelList(string strWhere)
        {
            DataSet list = this.dal.GetList(strWhere);
            return this.DataTableToList(list.Tables[0]);
        }

        public List<Maticsoft.Model.Pay.RechargeRequest> GetRechargeListByPage(string strWhere, int startIndex, int endIndex)
        {
            DataSet set = this.dal.GetListByPage(strWhere, " RechargeId desc ", startIndex, endIndex);
            return this.DataTableToList(set.Tables[0]);
        }

        public int GetRecordCount(string strWhere)
        {
            return this.dal.GetRecordCount(strWhere);
        }

        public bool Update(Maticsoft.Model.Pay.RechargeRequest model)
        {
            return this.dal.Update(model);
        }

        public bool UpdateStatus(Maticsoft.Model.Pay.RechargeRequest reModel)
        {
            decimal userBalance = new UsersExp().GetUserBalance(reModel.UserId);
            return this.dal.UpdateStatus(reModel, userBalance);
        }
    }
}


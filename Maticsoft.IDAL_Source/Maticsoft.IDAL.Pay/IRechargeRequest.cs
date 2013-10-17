namespace Maticsoft.IDAL.Pay
{
    using Maticsoft.Model.Pay;
    using System;
    using System.Data;

    public interface IRechargeRequest
    {
        long Add(RechargeRequest model);
        RechargeRequest DataRowToModel(DataRow row);
        bool Delete(long RechargeId);
        bool DeleteList(string RechargeIdlist);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        RechargeRequest GetModel(long RechargeId);
        int GetRecordCount(string strWhere);
        bool Update(RechargeRequest model);
        bool UpdateStatus(RechargeRequest reModel, decimal balance);
    }
}


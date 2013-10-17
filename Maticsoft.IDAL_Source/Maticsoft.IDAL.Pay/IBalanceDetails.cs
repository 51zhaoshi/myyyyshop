namespace Maticsoft.IDAL.Pay
{
    using Maticsoft.Model.Pay;
    using System;
    using System.Data;

    public interface IBalanceDetails
    {
        long Add(BalanceDetails model);
        BalanceDetails DataRowToModel(DataRow row);
        bool Delete(long JournalNumber);
        bool DeleteList(string JournalNumberlist);
        bool Exists(long JournalNumber);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        BalanceDetails GetModel(long JournalNumber);
        int GetRecordCount(string strWhere);
        bool Update(BalanceDetails model);
    }
}


namespace Maticsoft.IDAL.Pay
{
    using Maticsoft.Model.Pay;
    using System;
    using System.Data;

    public interface IBalanceDrawRequest
    {
        long Add(BalanceDrawRequest model);
        bool AddEx(BalanceDrawRequest model, decimal balance);
        BalanceDrawRequest DataRowToModel(DataRow row);
        bool Delete(long JournalNumber);
        bool DeleteList(string JournalNumberlist);
        bool Exists(long JournalNumber);
        decimal GetBalanceDraw(int userid, int Status);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere, string filedOrder);
        BalanceDrawRequest GetModel(long JournalNumber);
        int GetRecordCount(string strWhere);
        bool Update(BalanceDrawRequest model);
        bool UpdateStatus(string JournalNumberlist, int Status);
    }
}


namespace Maticsoft.IDAL.Shop.PromoteSales
{
    using Maticsoft.Model.Shop.PromoteSales;
    using System;
    using System.Data;

    public interface ICountDown
    {
        int Add(CountDown model);
        CountDown DataRowToModel(DataRow row);
        bool Delete(int CountDownId);
        bool DeleteList(string CountDownIdlist);
        bool Exists(int CountDownId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        CountDown GetModel(int CountDownId);
        int GetRecordCount(string strWhere);
        bool IsExists(long ProductId);
        int MaxSequence();
        bool Update(CountDown model);
        bool UpdateStatus(string ids, int status);
    }
}


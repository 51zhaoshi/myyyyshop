namespace Maticsoft.IDAL.Shop.Gift
{
    using Maticsoft.Model.Shop.Gift;
    using System;
    using System.Data;

    public interface IExchangeDetail
    {
        int Add(ExchangeDetail model);
        bool Delete(int ExchangeDetailID);
        bool DeleteList(string ExchangeDetailIDlist);
        bool Exists(int ExchangeDetailID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ExchangeDetail GetModel(int ExchangeDetailID);
        int GetRecordCount(string strWhere);
        bool SetStatus(int detailId, int status);
        bool SetStatusList(string detailIds, int status);
        bool Update(ExchangeDetail model);
    }
}


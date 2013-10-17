namespace Maticsoft.IDAL.Shop.Sales
{
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Data;

    public interface ISalesItem
    {
        int Add(SalesItem model);
        SalesItem DataRowToModel(DataRow row);
        bool Delete(int ItemId);
        bool DeleteByRuleId(int ruleId);
        bool DeleteList(string ItemIdlist);
        bool Exists(int ItemId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SalesItem GetModel(int ItemId);
        int GetRecordCount(string strWhere);
        bool Update(SalesItem model);
    }
}


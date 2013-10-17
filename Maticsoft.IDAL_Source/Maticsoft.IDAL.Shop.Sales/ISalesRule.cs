namespace Maticsoft.IDAL.Shop.Sales
{
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Data;

    public interface ISalesRule
    {
        int Add(SalesRule model);
        SalesRule DataRowToModel(DataRow row);
        bool Delete(int RuleId);
        bool DeleteEx(int RuleId);
        bool DeleteList(string RuleIdlist);
        bool DeleteListEx(string RuleIdlist);
        bool Exists(int RuleId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SalesRule GetModel(int RuleId);
        int GetRecordCount(string strWhere);
        bool Update(SalesRule model);
    }
}


namespace Maticsoft.IDAL.Shop.Sales
{
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Data;

    public interface ISalesRuleProduct
    {
        bool Add(SalesRuleProduct model);
        SalesRuleProduct DataRowToModel(DataRow row);
        bool Delete(int RuleId, long ProductId);
        bool DeleteByRule(int RuleId);
        bool Exists(int RuleId, long ProductId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SalesRuleProduct GetModel(int RuleId, long ProductId);
        int GetRecordCount(string strWhere);
        DataSet GetRuleProducts(int ruleId, string strWhere);
        bool Update(SalesRuleProduct model);
    }
}


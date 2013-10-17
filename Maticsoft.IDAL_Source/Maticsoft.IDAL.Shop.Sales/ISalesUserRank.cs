namespace Maticsoft.IDAL.Shop.Sales
{
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Data;

    public interface ISalesUserRank
    {
        bool Add(SalesUserRank model);
        SalesUserRank DataRowToModel(DataRow row);
        bool Delete(int RuleId, int RankId);
        bool DeleteByRuleId(int ruleId);
        bool Exists(int RuleId, int RankId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SalesUserRank GetModel(int RuleId, int RankId);
        int GetRecordCount(string strWhere);
        bool Update(SalesUserRank model);
    }
}


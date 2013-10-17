namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface IPointsRule
    {
        bool Add(PointsRule model);
        bool Delete(string Type);
        bool DeleteList(string Typelist);
        bool Exists(string Type);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        PointsRule GetModel(string Type);
        int GetRecordCount(string strWhere);
        string GetRuleName(string ruleaction);
        bool Update(PointsRule model);
    }
}


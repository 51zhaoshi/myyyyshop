namespace Maticsoft.IDAL.Settings
{
    using Maticsoft.Model.Settings;
    using System;
    using System.Data;

    public interface ISEORelation
    {
        int Add(SEORelation model);
        bool Delete(int RelationID);
        bool DeleteList(string RelationIDlist);
        bool Exists(int RelationID);
        bool Exists(string name);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SEORelation GetModel(int RelationID);
        int GetRecordCount(string strWhere);
        bool Update(SEORelation model);
    }
}


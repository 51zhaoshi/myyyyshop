namespace Maticsoft.IDAL.Members
{
    using Maticsoft.Model.Members;
    using System;
    using System.Data;

    public interface ISiteMessageLog
    {
        int Add(SiteMessageLog model);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SiteMessageLog GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(SiteMessageLog model);
    }
}


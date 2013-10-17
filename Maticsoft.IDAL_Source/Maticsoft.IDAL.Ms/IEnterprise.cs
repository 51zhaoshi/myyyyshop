namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IEnterprise
    {
        int Add(Enterprise model);
        bool Delete(int EnterpriseID);
        bool DeleteList(string EnterpriseIDlist);
        bool Exists(int EnterpriseID);
        bool Exists(string Name);
        bool Exists(string Name, int EnterpriseID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Enterprise GetModel(int EnterpriseID);
        int GetRecordCount(string strWhere);
        bool Update(Enterprise model);
        bool UpdateList(string IDlist, string strWhere);
    }
}


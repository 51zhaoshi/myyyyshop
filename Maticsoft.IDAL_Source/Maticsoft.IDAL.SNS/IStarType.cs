namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IStarType
    {
        int Add(StarType model);
        StarType DataRowToModel(DataRow row);
        bool Delete(int TypeID);
        bool DeleteList(string TypeIDlist);
        bool Exists(string TypeName);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        StarType GetModel(int TypeID);
        int GetRecordCount(string strWhere);
        bool Update(StarType model);
    }
}


namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IReferUsers
    {
        int Add(ReferUsers model);
        ReferUsers DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        ReferUsers GetModel(int ID);
        int GetRecordCount(string strWhere);
        int GetReferNotReadCountByType(int UserId, int Type);
        bool Update(ReferUsers model);
        bool UpdateReferStateToRead(int UserID, int Type);
    }
}


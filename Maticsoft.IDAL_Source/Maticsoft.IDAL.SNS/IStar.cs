namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IStar
    {
        int Add(Star model);
        Star DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool DeleteList(string IDlist, out DataSet ds);
        bool Exists(int UserID, int TypeID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        Star GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool IsExists(int userId, int typeId);
        bool IsStar(int userId);
        DataSet StarName(int userId);
        bool Update(Star model);
        bool UpdateStateList(string IDlist, int status);
    }
}


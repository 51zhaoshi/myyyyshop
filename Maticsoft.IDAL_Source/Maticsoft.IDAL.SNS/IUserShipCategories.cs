namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IUserShipCategories
    {
        int Add(UserShipCategories model);
        UserShipCategories DataRowToModel(DataRow row);
        bool Delete(int CategoryID);
        bool DeleteList(string CategoryIDlist);
        bool Exists(int CategoryID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        UserShipCategories GetModel(int CategoryID);
        int GetRecordCount(string strWhere);
        bool Update(UserShipCategories model);
    }
}


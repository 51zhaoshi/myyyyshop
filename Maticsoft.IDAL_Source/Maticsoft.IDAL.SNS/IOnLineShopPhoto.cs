namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IOnLineShopPhoto
    {
        bool Add(OnLineShopPhoto model);
        OnLineShopPhoto DataRowToModel(DataRow row);
        bool Delete(int PhotoID, int ProductID);
        bool Exists(int PhotoID, int ProductID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        OnLineShopPhoto GetModel(int PhotoID, int ProductID);
        int GetRecordCount(string strWhere);
        bool Update(OnLineShopPhoto model);
    }
}


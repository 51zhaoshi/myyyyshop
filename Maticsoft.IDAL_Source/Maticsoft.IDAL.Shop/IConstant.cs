namespace Maticsoft.IDAL.Shop
{
    using Maticsoft.Model.Shop;
    using System;
    using System.Data;

    public interface IConstant
    {
        bool Add(Constant model);
        Constant DataRowToModel(DataRow row);
        bool Delete();
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        Constant GetModel();
        int GetRecordCount(string strWhere);
        bool Update(Constant model);
    }
}


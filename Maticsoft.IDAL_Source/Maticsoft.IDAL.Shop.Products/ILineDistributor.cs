namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface ILineDistributor
    {
        bool Add(LineDistributor model);
        bool Delete(int LineId, int DistributorId);
        bool Exists(int LineId, int DistributorId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        LineDistributor GetModel(int LineId, int DistributorId);
        int GetRecordCount(string strWhere);
        bool Update(LineDistributor model);
    }
}


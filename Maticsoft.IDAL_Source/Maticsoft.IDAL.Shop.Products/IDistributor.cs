namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IDistributor
    {
        int Add(Distributor model);
        bool Delete(int DistributorId);
        bool DeleteList(string DistributorIdlist);
        bool Exists(int DistributorId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Distributor GetModel(int DistributorId);
        int GetRecordCount(string strWhere);
        bool Update(Distributor model);
    }
}


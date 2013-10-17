namespace Maticsoft.IDAL.Shop.Package
{
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Data;

    public interface IProductPackage
    {
        bool Add(ProductPackage model);
        ProductPackage DataRowToModel(DataRow row);
        bool Delete(long ProductId, int PackageId);
        bool Exists(long ProductId, int PackageId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductPackage GetModel(long ProductId, int PackageId);
        int GetRecordCount(string strWhere);
        bool Update(ProductPackage model);
    }
}


namespace Maticsoft.IDAL.Shop.Package
{
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Data;

    public interface IPackageCategory
    {
        int Add(PackageCategory model);
        PackageCategory DataRowToModel(DataRow row);
        bool Delete(int CategoryId);
        bool DeleteList(string CategoryIdlist);
        bool Exists(int CategoryId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        PackageCategory GetModel(int CategoryId);
        int GetRecordCount(string strWhere);
        bool Update(PackageCategory model);
    }
}


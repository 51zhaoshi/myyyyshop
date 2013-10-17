namespace Maticsoft.IDAL.Shop.Package
{
    using Maticsoft.Model.Shop.Package;
    using System;
    using System.Data;

    public interface IPackage
    {
        int Add(Maticsoft.Model.Shop.Package.Package model);
        Maticsoft.Model.Shop.Package.Package DataRowToModel(DataRow row);
        bool Delete(int PackageId);
        bool DeleteList(string PackageIdlist);
        bool Exists(int PackageId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere);
        int GetMaxId();
        Maticsoft.Model.Shop.Package.Package GetModel(int PackageId);
        int GetRecordCount(string strWhere);
        bool Update(Maticsoft.Model.Shop.Package.Package model);
    }
}


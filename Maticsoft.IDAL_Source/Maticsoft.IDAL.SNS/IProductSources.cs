namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IProductSources
    {
        int Add(ProductSources model);
        ProductSources DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(string WebSiteName, string WebSiteUrl);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        ProductSources GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(ProductSources model);
    }
}


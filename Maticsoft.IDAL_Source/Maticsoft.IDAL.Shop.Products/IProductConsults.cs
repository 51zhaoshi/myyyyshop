namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductConsults
    {
        int Add(ProductConsults model);
        ProductConsults DataRowToModel(DataRow row);
        bool Delete(int ConsultationId);
        bool DeleteList(string ConsultationIdlist);
        bool Exists(int ConsultationId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductConsults GetModel(int ConsultationId);
        int GetRecordCount(string strWhere);
        bool Update(ProductConsults model);
        bool UpdateStatusList(string ids, int status);
    }
}


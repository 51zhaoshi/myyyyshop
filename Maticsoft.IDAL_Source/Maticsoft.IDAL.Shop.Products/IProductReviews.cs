namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IProductReviews
    {
        int Add(ProductReviews model);
        bool AddEx(List<ProductReviews> modelList, long OrderId);
        bool AuditComment(string ids, int status);
        ProductReviews DataRowToModel(DataRow row);
        bool Delete(int ReviewId);
        bool DeleteList(string ReviewIdlist);
        bool Exists(int ReviewId);
        DataSet GetList(int? Status);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListsProdRev(int? Status);
        int GetMaxId();
        ProductReviews GetModel(int ReviewId);
        int GetRecordCount(string strWhere);
        bool Update(ProductReviews model);
    }
}


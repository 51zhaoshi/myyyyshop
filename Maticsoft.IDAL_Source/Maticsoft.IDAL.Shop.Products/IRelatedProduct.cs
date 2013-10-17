namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IRelatedProduct
    {
        bool Add(RelatedProduct model);
        bool Delete(int RelatedId, long ProductId);
        bool Exists(int RelatedId, long ProductId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        RelatedProduct GetModel(int RelatedId, long ProductId);
        int GetRecordCount(string strWhere);
        DataSet IsDoubleRelated(long productId);
        bool Update(RelatedProduct model);
    }
}


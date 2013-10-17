namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IProductAttribute
    {
        bool Add(ProductAttribute model);
        bool Delete(long ProductId, long AttributeId, int ValueId);
        bool Exists(long ProductId, long AttributeId, int ValueId);
        bool Exists(long? ProductId, long? AttributeId, long? ValueId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductAttribute GetModel(long ProductId, long AttributeId, int ValueId);
        int GetRecordCount(string strWhere);
        bool Update(ProductAttribute model);
    }
}


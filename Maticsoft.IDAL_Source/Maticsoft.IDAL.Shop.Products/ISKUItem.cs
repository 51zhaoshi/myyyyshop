namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface ISKUItem
    {
        bool Add(SKUItem model);
        DataSet AttributeValuesInfo(long productId);
        bool Delete(long SkuId, long AttributeId, long ValueId);
        bool Exists(long SkuId, long AttributeId, long ValueId);
        bool Exists(long? SkuId, long? AttributeId, long? ValueId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        SKUItem GetModel(long SkuId, long AttributeId, long ValueId);
        int GetRecordCount(string strWhere);
        DataSet GetSKUItem4AttrValByProductId(long productId);
        DataSet GetSKUItem4AttrValBySkuId(long skuId);
        bool Update(SKUItem model);
    }
}


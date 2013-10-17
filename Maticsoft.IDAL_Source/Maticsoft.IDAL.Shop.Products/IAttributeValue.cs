namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IAttributeValue
    {
        long Add(AttributeValue model);
        bool AttributeValueManage(AttributeValue model, DataProviderAction Action);
        bool Delete(long ValueId);
        bool DeleteImage(long valueId);
        bool DeleteList(string ValueIdlist);
        bool Exists(long ValueId);
        DataSet GetAttributeValue(int? cateID);
        DataSet GetList(long? AttributeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByAttribute(long AttributeId);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        AttributeValue GetModel(long ValueId);
        int GetRecordCount(string strWhere);
        bool Update(AttributeValue model);
    }
}


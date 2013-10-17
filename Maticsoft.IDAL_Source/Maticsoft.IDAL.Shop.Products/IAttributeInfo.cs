namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IAttributeInfo
    {
        long Add(AttributeInfo model);
        bool AttributeManage(AttributeInfo model, DataProviderAction Action);
        bool ChangeImageStatue(long AttributeId, ProductAttributeModel status);
        bool Delete(long AttributeId);
        bool DeleteList(string AttributeIdlist);
        bool Exists(long AttributeId);
        DataSet GetAttribute(int? cateID);
        List<AttributeInfo> GetAttributeInfoList(int? typeId, SearchType searchType);
        List<AttributeInfo> GetAttributeInfoListByProductId(long productId);
        DataSet GetAttributesByCate(int cateID, bool IsChild);
        DataSet GetList(string strWhere);
        DataSet GetList(long? Typeid, SearchType searchType);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        AttributeInfo GetModel(long AttributeId);
        DataSet GetProductAttributes(long productId);
        int GetRecordCount(string strWhere);
        bool IsExistDefinedAttribute(int typeId, long? attId);
        bool IsExistName(int typeId, string name);
        bool Update(AttributeInfo model);
    }
}


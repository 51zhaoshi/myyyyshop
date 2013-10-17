namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IProductType
    {
        int Add(ProductType model);
        bool Delete(int TypeId);
        bool DeleteList(string TypeIdlist);
        bool DeleteManage(int? TypeId, long? AttributeId, long? ValueId);
        bool Exists(int TypeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ProductType GetModel(int TypeId);
        List<ProductType> GetProductTypes();
        int GetRecordCount(string strWhere);
        bool ProductTypeManage(ProductType model, DataProviderAction Action, out int Typeid);
        bool SwapSeqManage(int? TypeId, long? AttributeId, long? ValueId, SwapSequenceIndex zIndex, bool UsageMode);
        bool Update(ProductType model);
    }
}


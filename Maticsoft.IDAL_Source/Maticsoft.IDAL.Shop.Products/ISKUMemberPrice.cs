namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface ISKUMemberPrice
    {
        bool Add(SKUMemberPrice model);
        bool Delete(long SkuId, int GradeId);
        bool Exists(long SkuId, int GradeId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        SKUMemberPrice GetModel(long SkuId, int GradeId);
        int GetRecordCount(string strWhere);
        bool Update(SKUMemberPrice model);
    }
}


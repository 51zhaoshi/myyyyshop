namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface ICategoryInfo
    {
        int Add(CategoryInfo model);
        CategoryInfo DataRowToModel(DataRow row);
        bool Delete(int CategoryId);
        DataSet DeleteCategory(int categoryId, out int Result);
        bool DeleteList(string CategoryIdlist);
        bool DisplaceCategory(int FromCategoryId, int ToCategoryId);
        bool Exists(int CategoryId);
        DataSet GetCategoryListByPath(string path);
        int GetDepthByCid(int Cid);
        DataSet GetList(string strWhere);
        DataSet GetList(string strWhere, bool IsOrder);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        int GetMaxSeqByCid(int parentId);
        CategoryInfo GetModel(int CategoryId);
        DataSet GetNameByPid(long productId);
        string GetNamePathByPath(string path);
        int GetRecordCount(string strWhere);
        bool IsExisted(int parentId, string name, int categoryId);
        bool IsExistedProduce(int category);
        bool SwapCategorySequence(int CategoryId, SwapSequenceIndex zIndex);
        bool Update(CategoryInfo model);
        bool UpdateCategory(CategoryInfo model);
        bool UpdateDepthAndPath(int Cid, int Depth, string Path);
        bool UpdateHasChild(int cid);
        bool UpdatePath(CategoryInfo model);
        bool UpdateSeqByCid(int Seq, int Cid);
    }
}


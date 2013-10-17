namespace Maticsoft.IDAL.Shop.Gift
{
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IGiftsCategory
    {
        int Add(GiftsCategory model);
        bool AddCategory(GiftsCategory model);
        bool Delete(int CategoryID);
        bool DeleteCategory(int categoryId);
        bool DeleteList(string CategoryIDlist);
        bool Exists(int CategoryID);
        DataSet GetCategoryList(string strWhere);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        GiftsCategory GetModel(int CategoryID);
        int GetRecordCount(string strWhere);
        bool SwapSequence(int CategoryId, SwapSequenceIndex zIndex);
        bool Update(GiftsCategory model);
        bool UpdateCategory(GiftsCategory model);
    }
}


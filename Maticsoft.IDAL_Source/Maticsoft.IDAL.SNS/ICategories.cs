namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface ICategories
    {
        int Add(Categories model);
        bool AddCategories(Categories model);
        bool AddCategory(Categories model);
        Categories DataRowToModel(DataRow row);
        bool Delete(int CategoryId);
        bool DeleteCategory(int categoryId);
        bool DeleteList(string CategoryIdlist);
        bool Exists(int CategoryId);
        DataSet GetCategoryList(string strWhere);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Categories GetModel(int CategoryId);
        int GetRecordCount(string strWhere);
        bool SwapCategorySequence(int CategoryId, EnumHelper.SwapSequenceIndex zIndex);
        bool Update(Categories model);
        bool UpdateCategory(Categories model);
    }
}


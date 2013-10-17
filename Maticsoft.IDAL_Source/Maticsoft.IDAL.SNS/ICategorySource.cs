namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface ICategorySource
    {
        int Add(CategorySource model);
        bool AddCategory(CategorySource model);
        CategorySource DataRowToModel(DataRow row);
        bool Delete(int SourceId, int CategoryId);
        bool DeleteCategory(int categoryId);
        bool Exists(int SourceId, int CategoryId);
        DataSet GetCategoryList(string strWhere);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        CategorySource GetModel(int SourceId, int CategoryId);
        int GetRecordCount(string strWhere);
        bool IsUpdate(long CategoryId, string name, int SourceId, int ParentID);
        bool Update(CategorySource model);
        bool UpdateCategory(CategorySource model);
        bool UpdateSNSCate(int CategoryId, int SNSCateId, bool IsLoop);
        bool UpdateSNSCateList(string ids, int SNSCateId, bool IsLoop);
    }
}


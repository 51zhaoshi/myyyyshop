namespace Maticsoft.IDAL.Shop.Tags
{
    using Maticsoft.Model.Shop.Tags;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface ITagCategories
    {
        int Add(TagCategories model);
        bool CreateCategory(TagCategories model);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        DataSet DeleteTagCategories(int ID, out int Result);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        TagCategories GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool TagCategoriesSequence(int ID, SequenceIndex Index);
        bool Update(TagCategories model);
    }
}


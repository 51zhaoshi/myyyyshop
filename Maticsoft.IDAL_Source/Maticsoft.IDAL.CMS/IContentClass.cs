namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Common.Video;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;

    public interface IContentClass
    {
        int Add(ContentClass model);
        bool AddExt(ContentClass model);
        ContentClass DataRowToModel(DataRow row);
        bool Delete(int ClassID);
        bool DeleteCategory(int categoryId);
        bool DeleteList(string ClassIDlist);
        bool Exists(int ClassID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListByView(string strWhere);
        DataSet GetListByView(int Top, string strWhere, string filedOrder);
        int GetMaxId();
        ContentClass GetModel(int ClassID);
        string GetNamePathByPath(string path);
        int GetRecordCount(string strWhere);
        DataSet GetTreeList(string strWhere);
        int SwapCategorySequence(int ContentClassId, SwapSequenceIndex zIndex);
        bool Update(ContentClass model);
        bool UpdateList(string IDlist, string strWhere);
    }
}


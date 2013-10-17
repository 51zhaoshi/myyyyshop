namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Common.Video;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;

    public interface IVideoClass
    {
        int Add(VideoClass model);
        int AddEx(VideoClass model);
        bool Delete(int VideoClassID);
        int DeleteEx(int VideoClassID);
        bool DeleteList(string VideoClassIDlist);
        bool Exists(int VideoClassID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere, string orderby);
        int GetMaxId();
        int GetMaxSequence();
        VideoClass GetModel(int VideoClassID);
        VideoClass GetModelByParentID(int ParentID);
        int GetRecordCount(string strWhere);
        int SwapCategorySequence(int VideoClassId, SwapSequenceIndex zIndex);
        bool Update(VideoClass model);
    }
}


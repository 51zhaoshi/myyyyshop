namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;

    public interface IVideo
    {
        int Add(Video model);
        bool Delete(int VideoID);
        bool DeleteList(string VideoIDlist);
        bool Exists(int VideoID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere, string orderby);
        int GetMaxId();
        int GetMaxSequence();
        Video GetModel(int VideoID);
        Video GetModelEx(int VideoID);
        int GetRecordCount(string strWhere);
        bool Update(Video model);
        bool UpdateList(string IDlist, string strWhere);
    }
}


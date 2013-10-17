namespace Maticsoft.IDAL.Ms
{
    using Maticsoft.Model.Ms;
    using System;
    using System.Data;

    public interface IThumbnailSize
    {
        bool Add(ThumbnailSize model);
        ThumbnailSize DataRowToModel(DataRow row);
        bool Delete(string ThumName);
        bool DeleteList(string ThumNamelist);
        bool Exists(string ThumName);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        ThumbnailSize GetModel(string ThumName);
        int GetRecordCount(string strWhere);
        bool Update(ThumbnailSize model);
    }
}


namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IVideos
    {
        int Add(Videos model);
        Videos DataRowToModel(DataRow row);
        bool Delete(int VideoID);
        bool DeleteList(string VideoIDlist);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        Videos GetModel(int VideoID);
        int GetRecordCount(string strWhere);
        bool Update(Videos model);
    }
}


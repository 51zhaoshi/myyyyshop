namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IPhotoTags
    {
        int Add(PhotoTags model);
        PhotoTags DataRowToModel(DataRow row);
        bool Delete(int TagID);
        bool DeleteList(string TagIDlist);
        bool Exists(int TagID);
        bool Exists(string TagName);
        bool Exists(int TagID, string TagName);
        DataSet GetHotTags(int top);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        PhotoTags GetModel(int TagID);
        int GetRecordCount(string strWhere);
        bool Update(PhotoTags model);
        bool UpdateStatus(int Status, string IdList);
    }
}


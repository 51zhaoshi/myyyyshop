namespace Maticsoft.IDAL.Settings
{
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IAdvertisement
    {
        bool Add(Advertisement model);
        List<Advertisement> DataTableToList(DataTable dt);
        bool Delete(int AdvertisementId);
        bool DeleteList(string AdvertisementIdlist);
        DataSet GetContentType(int AdvPositionId);
        DataSet GetDefindCode(int AdvPositionId);
        DataSet GetList(string strWhere);
        DataSet GetList(int? Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxSequence();
        Advertisement GetModel(int AdvertisementId);
        Advertisement GetModelByAdvPositionId(int AdvPositionId);
        int GetRecordCount(string strWhere);
        DataSet GetTransitionImg(int Aid, int ContentType, int? Num);
        int IsExist(int AdvPositionId, int contentType);
        DataSet SelectInfoByContentType(int ContentType);
        bool Update(Advertisement model);
    }
}


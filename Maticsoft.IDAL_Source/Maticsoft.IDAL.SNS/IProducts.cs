namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;

    public interface IProducts
    {
        long Add(Products model);
        Products DataRowToModel(DataRow row);
        bool Delete(long ProductID);
        bool DeleteEX(int ProductID);
        bool DeleteList(string ProductIDlist);
        DataSet DeleteListEx(string Ids, out int Result);
        bool DeleteListEX(string ProductIds);
        bool Exists(long ProductID);
        bool Exsit(long originalID, int type);
        bool Exsit(string ProductName, int Uid);
        bool ExsitUrl(string ProductUrl);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(string strWhere, int CateId);
        DataSet GetListToStatic(string strWhere);
        Products GetModel(long ProductID);
        DataSet GetProductByPage(string strWhere, string Order, int startIndex, int endIndex);
        string GetProductUrl(long productId);
        DataSet GetProductUserIds(string ids);
        int GetRecordCount(string strWhere);
        int GetRecordCountEx(string strWhere, int CateId);
        bool Update(Products model);
        bool UpdateCateList(string ProductIds, int CateId);
        bool UpdateClickCount(int ProuductId);
        bool UpdateEX(int ProductId, int CateId);
        bool UpdatePvCount(int ProductID);
        bool UpdateRecomend(int ProductId, int Recomend);
        bool UpdateRecomendList(string ProductIds, int Recomend);
        bool UpdateRecommandState(int id, int State);
        bool UpdateStaticUrl(int productId, string staticUrl);
        bool UpdateStatus(int ProductId, int Status);
        DataSet UserUploadProductsImage(int ablumId);
    }
}


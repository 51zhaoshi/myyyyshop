namespace Maticsoft.IDAL.CMS
{
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IContent
    {
        int Add(Content model);
        Content DataRowToModel(DataRow row);
        List<Content> DataTableToListEx(DataTable dt);
        bool Delete(int ContentID);
        bool DeleteList(string ContentIDlist);
        bool Exists(int ContentID);
        bool ExistsByClassID(int ClassID);
        bool ExistTitle(string Title);
        DataSet GetHotCom(int diffDate, int top);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByItem(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListByView(string strWhere);
        DataSet GetListByView(int Top, string strWhere, string filedOrder);
        DataSet GetListEx(int Top, string strWhere, string filedOrder);
        int GetMaxId();
        Content GetModel(int ContentID);
        Content GetModelByClassID(int ClassID);
        Content GetModelEx(int ContentID);
        int GetNextID(int ContentID, int ClassId);
        int GetPrevID(int ContentID, int ClassId);
        DataSet GetRanList(int ClassID, string keyword, int Top);
        int GetRecordCount(string strWhere);
        int GetRecordCount4Menu(string strWhere);
        int GetRecordCountEx(string strWhere);
        bool SetColor(int id, bool rec);
        bool SetColorList(string ids);
        bool SetHot(int id, bool rec);
        bool SetHotList(string ids);
        bool SetRec(int id, bool rec);
        bool SetRecList(string ids);
        bool SetTop(int id, bool rec);
        bool SetTopList(string ids);
        bool Update(Content model);
        bool UpdateFav(int ContentID);
        bool UpdateList(string IDlist, string strWhere);
        int UpdatePV(int ContentID);
        bool UpdateTotalSupport(int ContentID);
    }
}


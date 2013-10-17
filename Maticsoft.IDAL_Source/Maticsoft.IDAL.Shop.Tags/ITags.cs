namespace Maticsoft.IDAL.Shop.Tags
{
    using Maticsoft.Model.Shop.Tags;
    using System;
    using System.Data;

    public interface ITags
    {
        int Add(Maticsoft.Model.Shop.Tags.Tags model);
        bool Delete(int TagID);
        bool DeleteList(string TagIDlist);
        bool Exists(int TagID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        DataSet GetListEx(int Top, string strWhere, string filedOrder);
        Maticsoft.Model.Shop.Tags.Tags GetModel(int TagID);
        int GetRecordCount(string strWhere);
        bool Update(Maticsoft.Model.Shop.Tags.Tags model);
        bool UpdateIsRecommand(string IsRecommand, string IdList);
        bool UpdateStatus(int Status, string IdList);
    }
}


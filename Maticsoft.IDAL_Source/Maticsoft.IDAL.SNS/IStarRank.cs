namespace Maticsoft.IDAL.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;

    public interface IStarRank
    {
        int Add(StarRank model);
        bool AddCollocationRank();
        bool AddHotStarRank();
        bool AddShareProductRank();
        StarRank DataRowToModel(DataRow row);
        bool Delete(int ID);
        bool DeleteList(string IDlist);
        bool Exists(int ID);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        StarRank GetModel(int ID);
        int GetRecordCount(string strWhere);
        bool Update(StarRank model);
        bool UpdateStateList(string IDlist);
    }
}


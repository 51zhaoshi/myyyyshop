namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IScoreDetails
    {
        bool Add(ScoreDetails model);
        bool Delete(int ScoreId);
        bool DeleteList(string ScoreIdlist);
        bool Exists(int ScoreId);
        DataSet GetList();
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ScoreDetails GetModel(int ScoreId);
        int GetRecordCount(string strWhere);
        int GetScore(int? ReviewId);
        DataSet GetScoreDetailInfo(long productId);
        bool Update(ScoreDetails model);
    }
}


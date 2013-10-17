namespace Maticsoft.IDAL.Shop.Gift
{
    using Maticsoft.Model.Shop.Gift;
    using System;
    using System.Data;

    public interface IGifts
    {
        int Add(Gifts model);
        bool Delete(int GiftId);
        bool DeleteList(string GiftIdlist);
        bool Exists(int GiftId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        Gifts GetModel(int GiftId);
        int GetRecordCount(string strWhere);
        bool Update(Gifts model);
        bool UpdateStock(int giftid, int stock);
    }
}


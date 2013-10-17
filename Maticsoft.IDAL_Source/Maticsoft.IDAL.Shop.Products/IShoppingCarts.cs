namespace Maticsoft.IDAL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;

    public interface IShoppingCarts
    {
        bool Add(ShoppingCartItem model);
        ShoppingCartItem DataRowToModel(DataRow row);
        bool Delete(int ItemId, int UserId);
        bool Exists(int ItemId, int UserId);
        DataSet GetList(string strWhere);
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        int GetMaxId();
        ShoppingCartItem GetModel(int ItemId, int UserId);
        int GetRecordCount(string strWhere);
        bool Update(ShoppingCartItem model);
    }
}


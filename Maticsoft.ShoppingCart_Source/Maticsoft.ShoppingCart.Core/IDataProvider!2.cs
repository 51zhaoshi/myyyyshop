namespace Maticsoft.ShoppingCart.Core
{
    using System;
    using System.Data;

    public interface IDataProvider<TCartInfo, TCartItemInfo> : ICartProvider<TCartInfo, TCartItemInfo> where TCartInfo: CartInfo<TCartItemInfo>, new() where TCartItemInfo: CartItemInfo, new()
    {
        DataTable GetProductAndSku(int productId);
        DataTable GetProductAndSku(int productId, string skuOptions);
        DataTable GetProductListBySkus(string skus);
        void LoadCartProduct(TCartInfo cartInfo);
    }
}


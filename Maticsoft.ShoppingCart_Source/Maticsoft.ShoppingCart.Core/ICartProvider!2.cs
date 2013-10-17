namespace Maticsoft.ShoppingCart.Core
{
    using System;

    public interface ICartProvider<TCartInfo, TCartItemInfo> where TCartInfo: CartInfo<TCartItemInfo>, new() where TCartItemInfo: CartItemInfo, new()
    {
        void AddItem(TCartItemInfo itemInfo);
        void ClearShoppingCart();
        TCartInfo GetShoppingCart();
        void RemoveItem(int itemId);
        void UpdateItemQuantity(int itemId, int quantity);
    }
}


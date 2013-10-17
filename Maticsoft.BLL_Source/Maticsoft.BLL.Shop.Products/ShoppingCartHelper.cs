namespace Maticsoft.BLL.Shop.Products
{
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.ShoppingCart.Core;
    using System;

    public class ShoppingCartHelper
    {
        private readonly ICartProvider<ShoppingCartInfo, ShoppingCartItem> _cartProvider;

        public ShoppingCartHelper(int userId)
        {
            this._cartProvider = new CookieProvider<ShoppingCartInfo, ShoppingCartItem>(userId);
        }

        public void AddItem(ShoppingCartItem itemInfo)
        {
            this._cartProvider.AddItem(itemInfo);
        }

        public void ClearShoppingCart()
        {
            this._cartProvider.ClearShoppingCart();
        }

        public ShoppingCartInfo GetShoppingCart()
        {
            return this._cartProvider.GetShoppingCart();
        }

        public static void LoadShoppingCart(int userId)
        {
            CookieProvider<ShoppingCartInfo, ShoppingCartItem>.LoadShoppingCart(userId);
        }

        public void RemoveItem(int itemId)
        {
            this._cartProvider.RemoveItem(itemId);
        }

        public void UpdateItemQuantity(int itemId, int quantity)
        {
            this._cartProvider.UpdateItemQuantity(itemId, quantity);
        }
    }
}


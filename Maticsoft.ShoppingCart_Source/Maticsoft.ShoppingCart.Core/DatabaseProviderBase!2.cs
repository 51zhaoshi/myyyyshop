namespace Maticsoft.ShoppingCart.Core
{
    using System;

    public abstract class DatabaseProviderBase<TCartInfo, TCartItemInfo> : ICartProvider<TCartInfo, TCartItemInfo> where TCartInfo: CartInfo<TCartItemInfo>, new() where TCartItemInfo: CartItemInfo, new()
    {
        private readonly IDataProvider<TCartInfo, TCartItemInfo> _provider;

        protected DatabaseProviderBase(IDataProvider<TCartInfo, TCartItemInfo> provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            this._provider = provider;
        }

        public virtual void AddItem(TCartItemInfo itemInfo)
        {
            if (itemInfo.Quantity <= 0)
            {
                itemInfo.Quantity = 1;
            }
            this._provider.AddItem(itemInfo);
        }

        public virtual void ClearShoppingCart()
        {
            this._provider.ClearShoppingCart();
        }

        public virtual TCartInfo GetShoppingCart()
        {
            return this._provider.GetShoppingCart();
        }

        public virtual void RemoveItem(int itemId)
        {
            this._provider.RemoveItem(itemId);
        }

        public virtual void UpdateItemQuantity(int itemId, int quantity)
        {
            this._provider.UpdateItemQuantity(itemId, quantity);
        }
    }
}


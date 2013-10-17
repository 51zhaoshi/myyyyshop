namespace Maticsoft.ShoppingCart.Core
{
    using System;

    public abstract class DatabaseProviderBase : DatabaseProviderBase<CartInfo<CartItemInfo>, CartItemInfo>
    {
        protected DatabaseProviderBase(IDataProvider<CartInfo<CartItemInfo>, CartItemInfo> provider) : base(provider)
        {
        }
    }
}


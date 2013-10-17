namespace Maticsoft.ShoppingCart.Model
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class CartInfo<T> where T: CartItemInfo, new()
    {
        private List<T> _list;

        public CartInfo()
        {
            this._list = new List<T>();
        }

        public T this[int itemId]
        {
            get
            {
                return this.Items.Find(xx => xx.ItemId == itemId);
            }
        }

        public T this[string target]
        {
            get
            {
                return this.Items.Find(xx => xx.SKU == target);
            }
        }

        public List<T> Items
        {
            get
            {
                return this._list;
            }
            set
            {
                this._list = value;
            }
        }

        public int Quantity
        {
            get
            {
                if ((this.Items == null) || (this.Items.Count == 0))
                {
                    return 0;
                }
                int num = 0;
                this.Items.ForEach(delegate (T xx) {
                    num += xx.Quantity;
                });
                return num;
            }
        }
    }
}


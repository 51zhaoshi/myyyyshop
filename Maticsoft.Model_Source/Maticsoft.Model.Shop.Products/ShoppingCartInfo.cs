namespace Maticsoft.Model.Shop.Products
{
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.ShoppingCart.Model;
    using System;

    [Serializable]
    public class ShoppingCartInfo : CartInfo<ShoppingCartItem>
    {
        public decimal CalcFreight(ShippingType shippingType)
        {
            if (shippingType == null)
            {
                return 0M;
            }
            int totalWeight = this.TotalWeight;
            if (((totalWeight <= shippingType.Weight) || !shippingType.AddPrice.HasValue) || !shippingType.AddWeight.HasValue)
            {
                return 0M;
            }
            int num2 = 1;
            if ((totalWeight > shippingType.Weight) && (shippingType.AddWeight.Value > 0))
            {
                int num3 = totalWeight - shippingType.Weight;
                int num4 = num3;
                if ((num4 % shippingType.AddWeight) == 0)
                {
                    num2 = (totalWeight - shippingType.Weight) / shippingType.AddWeight.Value;
                }
                else
                {
                    num2 = ((totalWeight - shippingType.Weight) / shippingType.AddWeight.Value) + 1;
                }
            }
            if (totalWeight <= shippingType.Weight)
            {
                return shippingType.Price;
            }
            return ((num2 * shippingType.AddPrice.Value) + shippingType.Price);
        }

        public decimal TotalAdjustedPrice
        {
            get
            {
                if ((base.Items == null) || (base.Items.Count == 0))
                {
                    return 0M;
                }
                decimal totalPrice = 0M;
                base.Items.ForEach(delegate (ShoppingCartItem info) {
                    totalPrice += info.AdjustedPrice * info.Quantity;
                });
                return totalPrice;
            }
        }

        public decimal TotalCostPrice
        {
            get
            {
                if ((base.Items == null) || (base.Items.Count == 0))
                {
                    return 0M;
                }
                decimal totalPrice = 0M;
                base.Items.ForEach(delegate (ShoppingCartItem info) {
                    totalPrice += info.CostPrice * info.Quantity;
                });
                return totalPrice;
            }
        }

        public decimal TotalSellPrice
        {
            get
            {
                if ((base.Items == null) || (base.Items.Count == 0))
                {
                    return 0M;
                }
                decimal totalPrice = 0M;
                base.Items.ForEach(delegate (ShoppingCartItem info) {
                    totalPrice += info.SellPrice * info.Quantity;
                });
                return totalPrice;
            }
        }

        public int TotalWeight
        {
            get
            {
                if ((base.Items == null) || (base.Items.Count == 0))
                {
                    return 0;
                }
                int totalWeight = 0;
                base.Items.ForEach(delegate (ShoppingCartItem info) {
                    totalWeight += info.Weight * info.Quantity;
                });
                return totalWeight;
            }
        }
    }
}


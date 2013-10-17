namespace Maticsoft.ViewModel.Shop
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Runtime.CompilerServices;

    public class ProductComment
    {
        public ProductReviews Comment { get; set; }

        public Maticsoft.Model.Shop.Products.ProductInfo ProductInfo { get; set; }
    }
}


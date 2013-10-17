namespace Maticsoft.ViewModel.Shop
{
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ProductModel
    {
        public List<CategoryInfo> CategoryInfos { get; set; }

        public List<Maticsoft.Model.Shop.Package.Package> Package { get; set; }

        public List<ProductAttribute> ProductAttributes { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public Maticsoft.Model.Shop.Products.ProductInfo ProductInfo { get; set; }

        public List<SKUInfo> ProductSkus { get; set; }

        public List<Maticsoft.Model.Shop.Tags.Tags> TagList { get; set; }
    }
}


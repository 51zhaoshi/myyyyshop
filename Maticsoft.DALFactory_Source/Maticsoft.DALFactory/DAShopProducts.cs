namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.IDAL.Shop.Tags;
    using System;

    public sealed class DAShopProducts : DataAccessBase
    {
        public static IAccessoriesValue CreateAccessoriesValue()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.AccessoriesValue";
            return (IAccessoriesValue) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IAttributeInfo CreateAttributeInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.AttributeInfo";
            return (IAttributeInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IAttributeValue CreateAttributeValue()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.AttributeValue";
            return (IAttributeValue) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IBrandInfo CreateBrandInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.BrandInfo";
            return (IBrandInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ICategoryInfo CreateCategoryInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.CategoryInfo";
            return (ICategoryInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IDistributor CreateDistributor()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.Distributor";
            return (IDistributor) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IHotKeyword CreateHotKeyword()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.HotKeyword";
            return (IHotKeyword) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ILineDistributor CreateLineDistributor()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.LineDistributor";
            return (ILineDistributor) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductAccessorie CreateProductAccessorie()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductAccessorie";
            return (IProductAccessorie) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductAttribute CreateProductAttribute()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductAttribute";
            return (IProductAttribute) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductCategories CreateProductCategories()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductCategories";
            return (IProductCategories) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductConsults CreateProductConsults()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductConsults";
            return (IProductConsults) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductConsultsType CreateProductConsultsType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductConsultsType";
            return (IProductConsultsType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductImage CreateProductImage()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductImage";
            return (IProductImage) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductInfo CreateProductInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductInfo";
            return (IProductInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductLine CreateProductLine()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductLine";
            return (IProductLine) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductQA CreateProductQA()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductQA";
            return (IProductQA) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductReviews CreateProductReviews()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductReviews";
            return (IProductReviews) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductService CreateProductService()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductService";
            return (IProductService) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductStationMode CreateProductStationMode()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductStationMode";
            return (IProductStationMode) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductType CreateProductType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductType";
            return (IProductType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductTypeBrand CreateProductTypeBrand()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ProductTypeBrand";
            return (IProductTypeBrand) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IRelatedProduct CreateRelatedProduct()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.RelatedProduct";
            return (IRelatedProduct) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IScoreDetails CreateScoreDetails()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Admin.Shop.Products.ScoreDetails";
            return (IScoreDetails) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IShoppingCarts CreateShoppingCarts()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.ShoppingCarts";
            return (IShoppingCarts) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISKUInfo CreateSKUInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.SKUInfo";
            return (ISKUInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISKUItem CreateSKUItem()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.SKUItem";
            return (ISKUItem) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISKUMemberPrice CreateSKUMemberPrice()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Products.SKUMemberPrice";
            return (ISKUMemberPrice) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITagCategories CreateTagCategories()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Tags.TagCategories";
            return (ITagCategories) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ITags CreateTags()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Tags.Tags";
            return (ITags) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


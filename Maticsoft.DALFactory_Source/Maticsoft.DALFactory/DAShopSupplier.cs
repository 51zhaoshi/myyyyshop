namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Supplier;
    using System;

    public sealed class DAShopSupplier : DataAccessBase
    {
        public static ISupplierCategories CreateSupplierCategories()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierCategories";
            return (ISupplierCategories) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISupplierConfig CreateSupplierConfig()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierConfig";
            return (ISupplierConfig) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISupplierInfo CreateSupplierInfo()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierInfo";
            return (ISupplierInfo) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISupplierMenus CreateSupplierMenus()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierMenus";
            return (ISupplierMenus) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISupplierRank CreateSupplierRank()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierRank";
            return (ISupplierRank) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISupplierRankThemes CreateSupplierRankThemes()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierRankThemes";
            return (ISupplierRankThemes) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISupplierScoreDetails CreateSupplierScoreDetails()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierScoreDetails";
            return (ISupplierScoreDetails) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISupplierThemes CreateSupplierThemes()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SupplierThemes";
            return (ISupplierThemes) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISuppProductCategories CreateSuppProductCategories()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Supplier.SuppProductCategories";
            return (ISuppProductCategories) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


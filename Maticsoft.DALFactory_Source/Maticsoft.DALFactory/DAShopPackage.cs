namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Package;
    using System;

    public sealed class DAShopPackage : DataAccessBase
    {
        public static IPackage CreatePackage()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Package.Package";
            return (IPackage) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IPackageCategory CreatePackageCategory()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Package.PackageCategory";
            return (IPackageCategory) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IProductPackage CreateProductPackage()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Package.ProductPackage";
            return (IProductPackage) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


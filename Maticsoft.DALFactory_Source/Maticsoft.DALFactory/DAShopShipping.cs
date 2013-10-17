namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Shipping;
    using System;

    public sealed class DAShopShipping : DataAccessBase
    {
        public static IShippingAddress CreateShippingAddress()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Shipping.ShippingAddress";
            return (IShippingAddress) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IShippingGroup CreateShippingGroup()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Shipping.ShippingGroup";
            return (IShippingGroup) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IShippingPayment CreateShippingPayment()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Shipping.ShippingPayment";
            return (IShippingPayment) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IShippingRegions CreateShippingRegions()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Shipping.ShippingRegions";
            return (IShippingRegions) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IShippingType CreateShippingType()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Shipping.ShippingType";
            return (IShippingType) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


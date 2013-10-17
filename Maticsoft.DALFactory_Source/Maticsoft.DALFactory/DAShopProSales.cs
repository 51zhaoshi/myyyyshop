namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.PromoteSales;
    using System;

    public sealed class DAShopProSales : DataAccessBase
    {
        public static ICountDown CreateCountDown()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.PromoteSales.CountDown";
            return (ICountDown) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


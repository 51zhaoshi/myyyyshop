namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Gift;
    using System;

    public sealed class DAShopGifts : DataAccessBase
    {
        public static IExchangeDetail CreateExchangeDetail()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Gift.ExchangeDetail";
            return (IExchangeDetail) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGifts CreateGifts()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Gift.Gifts";
            return (IGifts) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IGiftsCategory CreateGiftsCategory()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Gift.GiftsCategory";
            return (IGiftsCategory) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


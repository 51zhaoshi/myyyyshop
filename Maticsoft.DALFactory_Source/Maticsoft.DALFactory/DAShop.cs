namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop;
    using System;

    public sealed class DAShop : DataAccessBase
    {
        public static IFavorite CreateFavorite()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Favorite";
            return (IFavorite) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


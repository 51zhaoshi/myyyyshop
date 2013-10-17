namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Sales;
    using System;

    public sealed class DAShopSales : DataAccessBase
    {
        public static ISalesItem CreateSalesItem()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Sales.SalesItem";
            return (ISalesItem) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISalesRule CreateSalesRule()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Sales.SalesRule";
            return (ISalesRule) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISalesRuleProduct CreateSalesRuleProduct()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Sales.SalesRuleProduct";
            return (ISalesRuleProduct) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISalesUserRank CreateSalesUserRank()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Sales.SalesUserRank";
            return (ISalesUserRank) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


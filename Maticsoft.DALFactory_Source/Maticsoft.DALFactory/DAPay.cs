namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Pay;
    using System;

    public sealed class DAPay : DataAccessBase
    {
        public static IBalanceDetails CreateBalanceDetails()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Pay.BalanceDetails";
            return (IBalanceDetails) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IBalanceDrawRequest CreateBalanceDrawRequest()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Pay.BalanceDrawRequest";
            return (IBalanceDrawRequest) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static IRechargeRequest CreateRechargeRequest()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Pay.RechargeRequest";
            return (IRechargeRequest) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


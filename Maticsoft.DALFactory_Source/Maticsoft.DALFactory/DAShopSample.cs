namespace Maticsoft.DALFactory
{
    using Maticsoft.IDAL.Shop.Sample;
    using System;

    public sealed class DAShopSample : DataAccessBase
    {
        public static ISample CreateSample()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Sample.Sample";
            return (ISample) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }

        public static ISampleDetail CreateSampleDetail()
        {
            string classNamespace = DataAccessBase.AssemblyPath + ".Shop.Sample.SampleDetail";
            return (ISampleDetail) DataAccessBase.CreateObject(DataAccessBase.AssemblyPath, classNamespace);
        }
    }
}


namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using System;

    public class DatabaseProviderFactory : NameTypeFactoryBase<Database>
    {
        protected DatabaseProviderFactory()
        {
        }

        public DatabaseProviderFactory(IConfigurationSource configurationSource) : base(configurationSource)
        {
        }
    }
}


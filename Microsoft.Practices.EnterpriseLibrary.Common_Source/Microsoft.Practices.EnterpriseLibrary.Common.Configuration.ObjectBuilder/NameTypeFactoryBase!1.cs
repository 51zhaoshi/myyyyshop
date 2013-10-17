namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using System;

    public class NameTypeFactoryBase<T>
    {
        private IConfigurationSource configurationSource;

        protected NameTypeFactoryBase() : this(ConfigurationSourceFactory.Create())
        {
        }

        protected NameTypeFactoryBase(IConfigurationSource configurationSource)
        {
            this.configurationSource = configurationSource;
        }

        public T Create(string name)
        {
            return EnterpriseLibraryFactory.BuildUp<T>(name, this.configurationSource);
        }

        public T CreateDefault()
        {
            return EnterpriseLibraryFactory.BuildUp<T>(this.configurationSource);
        }
    }
}


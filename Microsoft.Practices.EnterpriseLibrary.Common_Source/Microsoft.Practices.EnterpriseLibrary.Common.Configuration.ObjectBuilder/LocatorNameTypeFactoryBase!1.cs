namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public class LocatorNameTypeFactoryBase<T>
    {
        private IConfigurationSource configurationSource;
        private ILifetimeContainer lifetimeContainer;
        private IReadWriteLocator locator;

        protected LocatorNameTypeFactoryBase() : this(ConfigurationSourceFactory.Create())
        {
        }

        protected LocatorNameTypeFactoryBase(IConfigurationSource configurationSource)
        {
            this.configurationSource = configurationSource;
            this.locator = new Locator();
            this.lifetimeContainer = new LifetimeContainer();
            this.locator.Add(typeof(ILifetimeContainer), this.lifetimeContainer);
        }

        public T Create(string name)
        {
            return EnterpriseLibraryFactory.BuildUp<T>(this.locator, name, this.configurationSource);
        }

        public T CreateDefault()
        {
            return EnterpriseLibraryFactory.BuildUp<T>(this.locator, this.configurationSource);
        }
    }
}


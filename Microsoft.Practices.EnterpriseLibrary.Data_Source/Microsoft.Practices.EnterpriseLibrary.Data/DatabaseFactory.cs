namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;
    using System;
    using System.Configuration;

    public static class DatabaseFactory
    {
        public static Database CreateDatabase()
        {
            Database database;
            try
            {
                database = new DatabaseProviderFactory(ConfigurationSourceFactory.Create()).CreateDefault();
            }
            catch (ConfigurationErrorsException exception)
            {
                TryLogConfigurationError(exception, "default");
                throw;
            }
            return database;
        }

        public static Database CreateDatabase(string name)
        {
            Database database;
            try
            {
                database = new DatabaseProviderFactory(ConfigurationSourceFactory.Create()).Create(name);
            }
            catch (ConfigurationErrorsException exception)
            {
                TryLogConfigurationError(exception, name);
                throw;
            }
            return database;
        }

        private static void TryLogConfigurationError(ConfigurationErrorsException configurationException, string instanceName)
        {
            try
            {
                DefaultDataEventLogger logger = EnterpriseLibraryFactory.BuildUp<DefaultDataEventLogger>();
                if (logger != null)
                {
                    logger.LogConfigurationError(configurationException, instanceName);
                }
            }
            catch
            {
            }
        }
    }
}


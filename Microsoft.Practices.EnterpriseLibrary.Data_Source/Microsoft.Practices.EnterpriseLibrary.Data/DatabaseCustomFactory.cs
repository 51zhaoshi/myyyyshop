namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using Microsoft.Practices.ObjectBuilder;
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class DatabaseCustomFactory : ICustomFactory
    {
        private IDictionary<Type, IDatabaseAssembler> assemblersMapping = new Dictionary<Type, IDatabaseAssembler>(5);
        private object assemblersMappingLock = new object();

        public object CreateObject(IBuilderContext context, string name, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            DatabaseConfigurationView view = new DatabaseConfigurationView(configurationSource);
            ConnectionStringSettings connectionStringSettings = view.GetConnectionStringSettings(name);
            DbProviderMapping providerMapping = view.GetProviderMapping(name, connectionStringSettings.ProviderName);
            return this.GetAssembler(providerMapping.DatabaseType, name, reflectionCache).Assemble(name, connectionStringSettings, configurationSource);
        }

        public IDatabaseAssembler GetAssembler(Type type, string name, ConfigurationReflectionCache reflectionCache)
        {
            IDatabaseAssembler assembler;
            bool flag = false;
            lock (this.assemblersMappingLock)
            {
                flag = this.assemblersMapping.TryGetValue(type, out assembler);
            }
            if (!flag)
            {
                DatabaseAssemblerAttribute customAttribute = reflectionCache.GetCustomAttribute<DatabaseAssemblerAttribute>(type);
                if (customAttribute == null)
                {
                    throw new InvalidOperationException(string.Format(Resources.Culture, Resources.ExceptionDatabaseTypeDoesNotHaveAssemblerAttribute, new object[] { type.FullName, name }));
                }
                assembler = (IDatabaseAssembler) Activator.CreateInstance(customAttribute.AssemblerType);
                lock (this.assemblersMappingLock)
                {
                    this.assemblersMapping[type] = assembler;
                }
            }
            return assembler;
        }
    }
}


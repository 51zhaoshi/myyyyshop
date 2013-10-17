namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using Microsoft.Practices.ObjectBuilder;
    using System;
    using System.Collections.Generic;

    public abstract class AssemblerBasedObjectFactory<TObject, TConfiguration> where TObject: class where TConfiguration: class
    {
        private IDictionary<Type, IAssembler<TObject, TConfiguration>> assemblersMapping;
        private object assemblersMappingLock;
        private ConfigurationReflectionCache reflectionCache;

        protected AssemblerBasedObjectFactory()
        {
            this.assemblersMapping = new Dictionary<Type, IAssembler<TObject, TConfiguration>>();
            this.assemblersMappingLock = new object();
            this.reflectionCache = new ConfigurationReflectionCache();
        }

        public virtual TObject Create(IBuilderContext context, TConfiguration objectConfiguration, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            return this.GetAssembler(objectConfiguration).Assemble(context, objectConfiguration, configurationSource, reflectionCache);
        }

        private IAssembler<TObject, TConfiguration> GetAssembler(TConfiguration objectConfiguration)
        {
            IAssembler<TObject, TConfiguration> assembler;
            bool flag = false;
            Type key = objectConfiguration.GetType();
            lock (this.assemblersMappingLock)
            {
                flag = this.assemblersMapping.TryGetValue(key, out assembler);
            }
            if (!flag)
            {
                AssemblerAttribute assemblerAttribute = this.GetAssemblerAttribute(key);
                if (!typeof(IAssembler<TObject, TConfiguration>).IsAssignableFrom(assemblerAttribute.AssemblerType))
                {
                    throw new InvalidOperationException(string.Format(Resources.Culture, Resources.ExceptionAssemblerTypeNotCompatible, new object[] { objectConfiguration.GetType().FullName, typeof(IAssembler<TObject, TConfiguration>), assemblerAttribute.AssemblerType.FullName }));
                }
                assembler = (IAssembler<TObject, TConfiguration>) Activator.CreateInstance(assemblerAttribute.AssemblerType);
                lock (this.assemblersMappingLock)
                {
                    this.assemblersMapping[key] = assembler;
                }
            }
            return assembler;
        }

        private AssemblerAttribute GetAssemblerAttribute(Type type)
        {
            AssemblerAttribute customAttribute = Attribute.GetCustomAttribute(type, typeof(AssemblerAttribute)) as AssemblerAttribute;
            if (customAttribute == null)
            {
                throw new InvalidOperationException(string.Format(Resources.Culture, Resources.ExceptionAssemblerAttributeNotSet, new object[] { type.FullName }));
            }
            return customAttribute;
        }
    }
}


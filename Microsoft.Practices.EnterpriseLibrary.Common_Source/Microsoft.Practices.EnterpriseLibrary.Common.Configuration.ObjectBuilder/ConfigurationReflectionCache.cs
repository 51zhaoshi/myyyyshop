namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using System;
    using System.Collections.Generic;

    public sealed class ConfigurationReflectionCache
    {
        private Dictionary<PairKey<Type, Type>, Attribute> typeAttributes = new Dictionary<PairKey<Type, Type>, Attribute>();
        private object typeAttributesLock = new object();
        private Dictionary<Type, ICustomFactory> typeCustomFactories = new Dictionary<Type, ICustomFactory>();
        private object typeCustomFactoriesLock = new object();
        private Dictionary<PairKey<Type, Type>, Attribute> typeInheritedAttributes = new Dictionary<PairKey<Type, Type>, Attribute>();
        private object typeInheritedAttributesLock = new object();
        private Dictionary<Type, IConfigurationNameMapper> typeNameMappers = new Dictionary<Type, IConfigurationNameMapper>();
        private object typeNameMappersLock = new object();

        private IConfigurationNameMapper CreateConfigurationNameMapper(Type type)
        {
            ConfigurationNameMapperAttribute customAttribute = this.GetCustomAttribute<ConfigurationNameMapperAttribute>(type);
            if (customAttribute != null)
            {
                return (IConfigurationNameMapper) Activator.CreateInstance(customAttribute.NameMappingObjectType);
            }
            return null;
        }

        private ICustomFactory CreateCustomFactory(Type type)
        {
            CustomFactoryAttribute customAttribute = this.GetCustomAttribute<CustomFactoryAttribute>(type);
            if (customAttribute != null)
            {
                return (ICustomFactory) Activator.CreateInstance(customAttribute.FactoryType);
            }
            return null;
        }

        private TAttribute DoGetCustomAttribute<TAttribute>(Type type, Dictionary<PairKey<Type, Type>, Attribute> cache, object lockObject, bool inherit) where TAttribute: Attribute
        {
            Attribute attribute;
            PairKey<Type, Type> key = new PairKey<Type, Type>(type, typeof(TAttribute));
            bool flag = false;
            lock (lockObject)
            {
                flag = cache.TryGetValue(key, out attribute);
            }
            if (!flag)
            {
                attribute = this.RetrieveAttribute<TAttribute>(key, inherit);
                lock (lockObject)
                {
                    cache[key] = attribute;
                }
            }
            return (attribute as TAttribute);
        }

        public IConfigurationNameMapper GetConfigurationNameMapper(Type type)
        {
            IConfigurationNameMapper mapper;
            bool flag = false;
            lock (this.typeNameMappersLock)
            {
                flag = this.typeNameMappers.TryGetValue(type, out mapper);
            }
            if (!flag)
            {
                mapper = this.CreateConfigurationNameMapper(type);
                lock (this.typeNameMappersLock)
                {
                    this.typeNameMappers[type] = mapper;
                }
            }
            return mapper;
        }

        public TAttribute GetCustomAttribute<TAttribute>(Type type) where TAttribute: Attribute
        {
            return this.GetCustomAttribute<TAttribute>(type, false);
        }

        public TAttribute GetCustomAttribute<TAttribute>(Type type, bool inherit) where TAttribute: Attribute
        {
            return this.DoGetCustomAttribute<TAttribute>(type, inherit ? this.typeInheritedAttributes : this.typeAttributes, inherit ? this.typeInheritedAttributesLock : this.typeAttributesLock, inherit);
        }

        public ICustomFactory GetCustomFactory(Type type)
        {
            ICustomFactory factory;
            bool flag = false;
            lock (this.typeCustomFactoriesLock)
            {
                flag = this.typeCustomFactories.TryGetValue(type, out factory);
            }
            if (!flag)
            {
                factory = this.CreateCustomFactory(type);
                lock (this.typeCustomFactoriesLock)
                {
                    this.typeCustomFactories[type] = factory;
                }
            }
            return factory;
        }

        public bool HasCachedCustomAttribute<TAttribute>(Type type)
        {
            PairKey<Type, Type> key = new PairKey<Type, Type>(type, typeof(TAttribute));
            return this.typeAttributes.ContainsKey(key);
        }

        private TAttribute RetrieveAttribute<TAttribute>(PairKey<Type, Type> key, bool inherit) where TAttribute: Attribute
        {
            return (TAttribute) Attribute.GetCustomAttribute(key.Left, typeof(TAttribute), inherit);
        }
    }
}


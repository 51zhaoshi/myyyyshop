namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
    using System;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public sealed class CustomFactoryAttribute : Attribute
    {
        private Type factoryType;

        public CustomFactoryAttribute(Type factoryType)
        {
            if (factoryType == null)
            {
                throw new ArgumentNullException("factoryType");
            }
            if (!typeof(ICustomFactory).IsAssignableFrom(factoryType))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionTypeNotCustomFactory, new object[] { factoryType }), "factoryType");
            }
            this.factoryType = factoryType;
        }

        public Type FactoryType
        {
            get
            {
                return this.factoryType;
            }
        }
    }
}


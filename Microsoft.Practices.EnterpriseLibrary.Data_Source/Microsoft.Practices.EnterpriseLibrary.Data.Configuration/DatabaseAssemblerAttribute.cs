namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using System;

    public sealed class DatabaseAssemblerAttribute : Attribute
    {
        private Type assemblerType;

        public DatabaseAssemblerAttribute(Type assemblerType)
        {
            if (assemblerType == null)
            {
                throw new ArgumentNullException("assemblerType");
            }
            if (!typeof(IDatabaseAssembler).IsAssignableFrom(assemblerType))
            {
                throw new ArgumentException(string.Format(Resources.Culture, Resources.ExceptionTypeNotDatabaseAssembler, new object[] { assemblerType }), "assemblerType");
            }
            this.assemblerType = assemblerType;
        }

        public Type AssemblerType
        {
            get
            {
                return this.assemblerType;
            }
        }
    }
}


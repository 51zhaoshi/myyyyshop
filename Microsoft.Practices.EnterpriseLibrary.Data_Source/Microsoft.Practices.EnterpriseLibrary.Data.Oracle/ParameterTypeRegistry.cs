namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal sealed class ParameterTypeRegistry
    {
        private string commandText;
        private IDictionary<string, DbType> parameterTypes;

        internal ParameterTypeRegistry(string commandText)
        {
            this.commandText = commandText;
            this.parameterTypes = new Dictionary<string, DbType>();
        }

        internal DbType GetRegisteredParameterType(string parameterName)
        {
            return this.parameterTypes[parameterName];
        }

        internal bool HasRegisteredParameterType(string parameterName)
        {
            return this.parameterTypes.ContainsKey(parameterName);
        }

        internal void RegisterParameterType(string parameterName, DbType parameterType)
        {
            this.parameterTypes[parameterName] = parameterType;
        }
    }
}


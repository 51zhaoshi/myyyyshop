namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using System;
    using System.Collections;
    using System.Data;

    internal class CachingMechanism
    {
        private Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        public void AddParameterSetToCache(string connectionString, IDbCommand command, IDataParameter[] parameters)
        {
            string commandText = command.CommandText;
            string str2 = CreateHashKey(connectionString, commandText);
            this.paramCache[str2] = parameters;
        }

        public void Clear()
        {
            this.paramCache.Clear();
        }

        public static IDataParameter[] CloneParameters(IDataParameter[] originalParameters)
        {
            IDataParameter[] parameterArray = new IDataParameter[originalParameters.Length];
            int index = 0;
            int length = originalParameters.Length;
            while (index < length)
            {
                parameterArray[index] = (IDataParameter) ((ICloneable) originalParameters[index]).Clone();
                index++;
            }
            return parameterArray;
        }

        private static string CreateHashKey(string connectionString, string storedProcedure)
        {
            return (connectionString + ":" + storedProcedure);
        }

        public IDataParameter[] GetCachedParameterSet(string connectionString, IDbCommand command)
        {
            string commandText = command.CommandText;
            string str2 = CreateHashKey(connectionString, commandText);
            IDataParameter[] originalParameters = (IDataParameter[]) this.paramCache[str2];
            return CloneParameters(originalParameters);
        }

        public bool IsParameterSetCached(string connectionString, IDbCommand command)
        {
            string str = CreateHashKey(connectionString, command.CommandText);
            return (this.paramCache[str] != null);
        }
    }
}


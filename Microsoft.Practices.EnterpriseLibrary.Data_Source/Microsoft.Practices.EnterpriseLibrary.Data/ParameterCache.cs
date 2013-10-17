namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    using System;
    using System.Data;
    using System.Data.Common;

    public class ParameterCache
    {
        private CachingMechanism cache = new CachingMechanism();

        protected virtual void AddParametersFromCache(DbCommand command, Database database)
        {
            foreach (IDataParameter parameter in this.cache.GetCachedParameterSet(database.ConnectionString, command))
            {
                command.Parameters.Add(parameter);
            }
        }

        private bool AlreadyCached(IDbCommand command, Database database)
        {
            return this.cache.IsParameterSetCached(database.ConnectionString, command);
        }

        protected internal void Clear()
        {
            this.cache.Clear();
        }

        private static IDataParameter[] CreateParameterCopy(DbCommand command)
        {
            IDataParameterCollection parameters = command.Parameters;
            IDataParameter[] array = new IDataParameter[parameters.Count];
            parameters.CopyTo(array, 0);
            return CachingMechanism.CloneParameters(array);
        }

        public void SetParameters(DbCommand command, Database database)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }
            if (this.AlreadyCached(command, database))
            {
                this.AddParametersFromCache(command, database);
            }
            else
            {
                database.DiscoverParameters(command);
                IDataParameter[] parameters = CreateParameterCopy(command);
                this.cache.AddParameterSetToCache(database.ConnectionString, command, parameters);
            }
        }
    }
}


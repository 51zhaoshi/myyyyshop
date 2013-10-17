namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.OracleClient;
    using System.Globalization;
    using System.Security.Permissions;

    [DatabaseAssembler(typeof(OracleDatabaseAssembler)), OraclePermission(SecurityAction.Demand)]
    public class OracleDatabase : Database
    {
        private static readonly IList<IOraclePackage> emptyPackages = new List<IOraclePackage>(0);
        private IList<IOraclePackage> packages;
        private const string RefCursorName = "cur_OUT";
        private IDictionary<string, ParameterTypeRegistry> registeredParameterTypes;

        public OracleDatabase(string connectionString) : this(connectionString, emptyPackages)
        {
        }

        public OracleDatabase(string connectionString, IList<IOraclePackage> packages) : base(connectionString, OracleClientFactory.Instance)
        {
            this.registeredParameterTypes = new Dictionary<string, ParameterTypeRegistry>();
            if (packages == null)
            {
                throw new ArgumentNullException("packages");
            }
            this.packages = packages;
        }

        public override void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            if (DbType.Guid.Equals(dbType))
            {
                object obj2 = ConvertGuidToByteArray(value);
                this.AddParameter((OracleCommand) command, name, OracleType.Raw, 0x10, direction, nullable, precision, scale, sourceColumn, sourceVersion, obj2);
                this.RegisterParameterType(command, name, dbType);
            }
            else
            {
                base.AddParameter(command, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            }
        }

        public void AddParameter(OracleCommand command, string name, OracleType oracleType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            OracleParameter parameter = base.CreateParameter(name, DbType.AnsiString, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value) as OracleParameter;
            parameter.OracleType = oracleType;
            command.Parameters.Add(parameter);
        }

        private static object ConvertByteArrayToGuid(object value)
        {
            byte[] b = (byte[]) value;
            if (b.Length == 0)
            {
                return DBNull.Value;
            }
            return new Guid(b);
        }

        private static object ConvertGuidToByteArray(object value)
        {
            if (!(value is DBNull) && (value != null))
            {
                Guid guid = (Guid) value;
                return guid.ToByteArray();
            }
            return Convert.DBNull;
        }

        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            OracleCommandBuilder.DeriveParameters((OracleCommand) discoveryCommand);
        }

        public override DataSet ExecuteDataSet(DbCommand command)
        {
            this.PrepareCWRefCursor(command);
            return base.ExecuteDataSet(command);
        }

        public override DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction)
        {
            this.PrepareCWRefCursor(command);
            return base.ExecuteDataSet(command, transaction);
        }

        public override IDataReader ExecuteReader(DbCommand command)
        {
            this.PrepareCWRefCursor(command);
            return new OracleDataReaderWrapper((OracleDataReader) base.ExecuteReader(command));
        }

        public override IDataReader ExecuteReader(DbCommand command, DbTransaction transaction)
        {
            this.PrepareCWRefCursor(command);
            return new OracleDataReaderWrapper((OracleDataReader) base.ExecuteReader(command, transaction));
        }

        private ParameterTypeRegistry GetParameterTypeRegistry(string commandText)
        {
            ParameterTypeRegistry registry;
            this.registeredParameterTypes.TryGetValue(commandText, out registry);
            return registry;
        }

        public override object GetParameterValue(DbCommand command, string parameterName)
        {
            object parameterValue = base.GetParameterValue(command, parameterName);
            ParameterTypeRegistry parameterTypeRegistry = this.GetParameterTypeRegistry(command.CommandText);
            if ((parameterTypeRegistry != null) && parameterTypeRegistry.HasRegisteredParameterType(parameterName))
            {
                DbType registeredParameterType = parameterTypeRegistry.GetRegisteredParameterType(parameterName);
                if (DbType.Guid == registeredParameterType)
                {
                    return ConvertByteArrayToGuid(parameterValue);
                }
                if (DbType.Boolean == registeredParameterType)
                {
                    parameterValue = Convert.ToBoolean(parameterValue, CultureInfo.InvariantCulture);
                }
            }
            return parameterValue;
        }

        public override DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            string str = this.TranslatePackageSchema(storedProcedureName);
            return base.GetStoredProcCommand(str);
        }

        public override DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
        {
            string str = this.TranslatePackageSchema(storedProcedureName);
            return base.GetStoredProcCommand(str, parameterValues);
        }

        public override void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames)
        {
            this.PrepareCWRefCursor(command);
            base.LoadDataSet(command, dataSet, tableNames);
        }

        public override void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames, DbTransaction transaction)
        {
            this.PrepareCWRefCursor(command);
            base.LoadDataSet(command, dataSet, tableNames, transaction);
        }

        private void OnOracleRowUpdated(object sender, OracleRowUpdatedEventArgs args)
        {
            if ((args.RecordsAffected == 0) && (args.Errors != null))
            {
                args.Row.RowError = Resources.ExceptionMessageUpdateDataSetRowFailure;
                args.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        private void PrepareCWRefCursor(DbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if ((CommandType.StoredProcedure == command.CommandType) && QueryProcedureNeedsCursorParameter(command))
            {
                this.AddParameter(command as OracleCommand, "cur_OUT", OracleType.Cursor, 0, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, Convert.DBNull);
            }
        }

        private static bool QueryProcedureNeedsCursorParameter(DbCommand command)
        {
            foreach (OracleParameter parameter in command.Parameters)
            {
                if (parameter.OracleType == OracleType.Cursor)
                {
                    return false;
                }
            }
            return true;
        }

        private void RegisterParameterType(DbCommand command, string parameterName, DbType dbType)
        {
            ParameterTypeRegistry parameterTypeRegistry = this.GetParameterTypeRegistry(command.CommandText);
            if (parameterTypeRegistry == null)
            {
                parameterTypeRegistry = new ParameterTypeRegistry(command.CommandText);
                this.registeredParameterTypes.Add(command.CommandText, parameterTypeRegistry);
            }
            parameterTypeRegistry.RegisterParameterType(parameterName, dbType);
        }

        public override void SetParameterValue(DbCommand command, string parameterName, object value)
        {
            object obj2 = value;
            ParameterTypeRegistry parameterTypeRegistry = this.GetParameterTypeRegistry(command.CommandText);
            if ((parameterTypeRegistry != null) && parameterTypeRegistry.HasRegisteredParameterType(parameterName))
            {
                DbType registeredParameterType = parameterTypeRegistry.GetRegisteredParameterType(parameterName);
                if (DbType.Guid == registeredParameterType)
                {
                    obj2 = ConvertGuidToByteArray(value);
                }
            }
            base.SetParameterValue(command, parameterName, obj2);
        }

        protected override void SetUpRowUpdatedEvent(DbDataAdapter adapter)
        {
            ((OracleDataAdapter) adapter).RowUpdated += new OracleRowUpdatedEventHandler(this.OnOracleRowUpdated);
        }

        private string TranslatePackageSchema(string storedProcedureName)
        {
            string name = string.Empty;
            string str2 = storedProcedureName;
            if ((this.packages != null) && !string.IsNullOrEmpty(storedProcedureName))
            {
                foreach (IOraclePackage package in this.packages)
                {
                    if ((package.Prefix == "*") || storedProcedureName.StartsWith(package.Prefix))
                    {
                        name = package.Name;
                        break;
                    }
                }
            }
            if (name.Length != 0)
            {
                str2 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { name, storedProcedureName });
            }
            return str2;
        }
    }
}


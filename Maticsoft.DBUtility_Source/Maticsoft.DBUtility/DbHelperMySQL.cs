namespace Maticsoft.DBUtility
{
    using MySql.Data.MySqlClient;
    using MySql.Data.Types;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;

    public abstract class DbHelperMySQL
    {
        public static string connectionString = PubConstant.ConnectionString;

        public DbHelperMySQL()
        {
        }

        public DbHelperMySQL(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        private static MySqlCommand BuildQueryCommand(MySqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(storedProcName, connection) {
                CommandType = CommandType.StoredProcedure
            };
            foreach (MySqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        public static DbParameter CreateInParam(string ParamName, MySqlDbType DbType, int Size, object Value)
        {
            return CreateParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        public static DbParameter CreateInputOutParam(string ParamName, MySqlDbType DbType, int Size, object Value)
        {
            return CreateParam(ParamName, DbType, Size, ParameterDirection.InputOutput, Value);
        }

        public static DbParameter CreateOutParam(string ParamName, MySqlDbType DbType, int Size)
        {
            return CreateParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        private static DbParameter CreateParam(string ParamName, MySqlDbType DbType, int Size, ParameterDirection Direction, object Value)
        {
            DbParameter parameter;
            if (Size > 0)
            {
                parameter = new MySqlParameter(ParamName, DbType, Size);
            }
            else
            {
                parameter = new MySqlParameter(ParamName, DbType);
            }
            parameter.Direction = Direction;
            if ((Direction != ParameterDirection.Output) || (Value != null))
            {
                parameter.Value = Value;
            }
            return parameter;
        }

        public static DbParameter CreateReturnParam(string ParamName, MySqlDbType DbType, int Size)
        {
            return CreateParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null);
        }

        public static MySqlDataReader ExecuteReader(string strSQL)
        {
            MySqlDataReader reader2;
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (MySqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public static MySqlDataReader ExecuteReader(string SQLString, params DbParameter[] cmdParms)
        {
            MySqlDataReader reader2;
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch (MySqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public static int ExecuteSql(string SQLString)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (MySqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, params DbParameter[] cmdParms)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    num2 = num;
                }
                catch (MySqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, string content)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(SQLString, connection);
                MySqlParameter parameter = new MySqlParameter("?content", MySqlDbType.Text) {
                    Value = content
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (MySqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, string strConnectionString, params DbParameter[] cmdParms)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(strConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    num2 = num;
                }
                catch (DbException exception)
                {
                    throw exception;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
            return num2;
        }

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    command.CommandTimeout = Times;
                    num2 = command.ExecuteNonQuery();
                }
                catch (MySqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return num2;
        }

        public static object ExecuteSqlGet(string SQLString, string content)
        {
            object obj3;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(SQLString, connection);
                MySqlParameter parameter = new MySqlParameter("?content", MySqlDbType.LongText) {
                    Value = content
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    object objA = command.ExecuteScalar();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return null;
                    }
                    obj3 = objA;
                }
                catch (MySqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return obj3;
        }

        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            int num2;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(strSQL, connection);
                MySqlParameter parameter = new MySqlParameter("?fs", MySqlDbType.VarBinary) {
                    Value = fs
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (MySqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            int num3;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (CommandInfo info in cmdList)
                        {
                            string commandText = info.CommandText;
                            DbParameter[] parameters = info.Parameters;
                            PrepareCommand(cmd, connection, transaction, commandText, parameters);
                            if ((info.EffentNextType == EffentNextType.WhenHaveContine) || (info.EffentNextType == EffentNextType.WhenNoHaveContine))
                            {
                                if (info.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    transaction.Rollback();
                                    return 0;
                                }
                                object obj2 = cmd.ExecuteScalar();
                                bool flag = false;
                                if ((obj2 == null) && (obj2 == DBNull.Value))
                                {
                                    flag = false;
                                }
                                flag = Convert.ToInt32(obj2) > 0;
                                if ((info.EffentNextType == EffentNextType.WhenHaveContine) && !flag)
                                {
                                    transaction.Rollback();
                                    return 0;
                                }
                                if ((info.EffentNextType == EffentNextType.WhenNoHaveContine) && flag)
                                {
                                    transaction.Rollback();
                                    return 0;
                                }
                                continue;
                            }
                            int num2 = cmd.ExecuteNonQuery();
                            num += num2;
                            if ((info.EffentNextType == EffentNextType.ExcuteEffectRows) && (num2 == 0))
                            {
                                transaction.Rollback();
                                return 0;
                            }
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                        num3 = num;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return num3;
        }

        public static int ExecuteSqlTran(List<string> SQLStringList)
        {
            int num3;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand {
                    Connection = connection
                };
                MySqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    int num = 0;
                    for (int i = 0; i < SQLStringList.Count; i++)
                    {
                        string str = SQLStringList[i];
                        if (str.Trim().Length > 1)
                        {
                            command.CommandText = str;
                            num += command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    num3 = num;
                }
                catch
                {
                    transaction.Rollback();
                    num3 = 0;
                }
            }
            return num3;
        }

        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            DbParameter[] cmdParms = (DbParameter[]) entry.Value;
                            PrepareCommand(cmd, connection, transaction, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
        {
            return 0;
        }

        public static int ExecuteSqlTran4Indentity(List<CommandInfo> cmdList)
        {
            int num4;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int num = 0;
                        int num2 = 0;
                        foreach (CommandInfo info in cmdList)
                        {
                            string commandText = info.CommandText;
                            DbParameter[] parameters = info.Parameters;
                            foreach (DbParameter parameter in parameters)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = num2;
                                }
                            }
                            PrepareCommand(cmd, connection, transaction, commandText, parameters);
                            int num3 = cmd.ExecuteNonQuery();
                            num += num3;
                            if ((info.EffentNextType == EffentNextType.ExcuteEffectRows) && (num3 == 0))
                            {
                                transaction.Rollback();
                                return 0;
                            }
                            foreach (DbParameter parameter2 in parameters)
                            {
                                if (parameter2.Direction == ParameterDirection.Output)
                                {
                                    num2 = Convert.ToInt32(parameter2.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                        num4 = num;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return num4;
        }

        public static int ExecuteSqlTran4Indentity(List<CommandInfo> cmdList, MySqlTransaction trans)
        {
            MySqlCommand cmd = new MySqlCommand();
            int num = 0;
            int num2 = 0;
            foreach (CommandInfo info in cmdList)
            {
                string commandText = info.CommandText;
                DbParameter[] parameters = info.Parameters;
                foreach (DbParameter parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.InputOutput)
                    {
                        parameter.Value = num2;
                    }
                }
                PrepareCommand(cmd, trans.Connection, trans, commandText, parameters);
                int num3 = cmd.ExecuteNonQuery();
                num += num3;
                if ((info.EffentNextType == EffentNextType.ExcuteEffectRows) && (num3 == 0))
                {
                    throw new MySqlConversionException("DbHelperMySQL.ExecuteSqlTran4Indentity - [" + cmd.CommandText + "] 未执行成功!");
                }
                foreach (DbParameter parameter2 in parameters)
                {
                    if (parameter2.Direction == ParameterDirection.Output)
                    {
                        num2 = Convert.ToInt32(parameter2.Value);
                    }
                }
                cmd.Parameters.Clear();
            }
            return num;
        }

        [Obsolete]
        public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (CommandInfo info in SQLStringList)
                        {
                            string commandText = info.CommandText;
                            DbParameter[] parameters = info.Parameters;
                            foreach (DbParameter parameter in parameters)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = num;
                                }
                            }
                            PrepareCommand(cmd, connection, transaction, commandText, parameters);
                            cmd.ExecuteNonQuery();
                            foreach (DbParameter parameter2 in parameters)
                            {
                                if (parameter2.Direction == ParameterDirection.Output)
                                {
                                    num = Convert.ToInt32(parameter2.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            DbParameter[] cmdParms = (DbParameter[]) entry.Value;
                            foreach (DbParameter parameter in cmdParms)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = num;
                                }
                            }
                            PrepareCommand(cmd, connection, transaction, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            foreach (DbParameter parameter2 in cmdParms)
                            {
                                if (parameter2.Direction == ParameterDirection.Output)
                                {
                                    num = Convert.ToInt32(parameter2.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool Exists(string strSql)
        {
            int num;
            object single = GetSingle(strSql);
            if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(single.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public static bool Exists(string strSql, params DbParameter[] cmdParms)
        {
            int num;
            object single = GetSingle(strSql, cmdParms);
            if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(single.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public static int GetMaxID(string FieldName, string TableName)
        {
            object single = GetSingle("select max(" + FieldName + ")+1 from " + TableName);
            if (single == null)
            {
                return 1;
            }
            return int.Parse(single.ToString());
        }

        public static object GetSingle(string SQLString)
        {
            object obj3;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    object objA = command.ExecuteScalar();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return null;
                    }
                    obj3 = objA;
                }
                catch (MySqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return obj3;
        }

        public static object GetSingle(string SQLString, params DbParameter[] cmdParms)
        {
            object obj3;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object objA = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return null;
                    }
                    obj3 = objA;
                }
                catch (MySqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
            return obj3;
        }

        public static object GetSingle(string SQLString, int Times)
        {
            object obj3;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    command.CommandTimeout = Times;
                    object objA = command.ExecuteScalar();
                    if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
                    {
                        return null;
                    }
                    obj3 = objA;
                }
                catch (MySqlException exception)
                {
                    connection.Close();
                    throw exception;
                }
                finally
                {
                    if (command != null)
                    {
                        command.Dispose();
                    }
                }
            }
            return obj3;
        }

        public static object GetSingle4Trans(CommandInfo commandInfo, MySqlTransaction trans)
        {
            MySqlCommand cmd = new MySqlCommand();
            string commandText = commandInfo.CommandText;
            DbParameter[] parameters = commandInfo.Parameters;
            PrepareCommand(cmd, trans.Connection, trans, commandText, parameters);
            object objA = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if (!object.Equals(objA, null) && !object.Equals(objA, DBNull.Value))
            {
                return objA;
            }
            return null;
        }

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parameter in cmdParms)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static DataSet Query(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new MySqlDataAdapter(SQLString, connection).Fill(dataSet, "ds");
                }
                catch (MySqlException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }

        public static DataSet Query(string SQLString, params DbParameter[] cmdParms)
        {
            DataSet set2;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataSet dataSet = new DataSet();
                    try
                    {
                        adapter.Fill(dataSet, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (MySqlException exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    set2 = dataSet;
                }
            }
            return set2;
        }

        public static DataSet Query(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new MySqlDataAdapter(SQLString, connection) { SelectCommand = { CommandTimeout = Times } }.Fill(dataSet, "ds");
                }
                catch (MySqlException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }

        public static DataSet Query(string SQLString, string strConnectionString)
        {
            using (MySqlConnection connection = new MySqlConnection(strConnectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new MySqlDataAdapter(SQLString, connection).Fill(dataSet, "ds");
                }
                catch (DbException exception)
                {
                    throw exception;
                }
                return dataSet;
            }
        }

        public static MySqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return BuildQueryCommand(connection, storedProcName, parameters).ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                rowsAffected = BuildQueryCommand(connection, storedProcName, parameters).ExecuteNonQuery();
                return rowsAffected;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                new MySqlDataAdapter { SelectCommand = BuildQueryCommand(connection, storedProcName, parameters) }.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter {
                    SelectCommand = BuildQueryCommand(connection, storedProcName, parameters)
                };
                adapter.SelectCommand.CommandTimeout = Times;
                adapter.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, out int returnValue)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                adapter.SelectCommand = command;
                adapter.Fill(dataSet, tableName);
                if (command.Parameters.Contains("ReturnValue") && (command.Parameters["ReturnValue"].Value != null))
                {
                    returnValue = (int) command.Parameters["ReturnValue"].Value;
                }
                else
                {
                    returnValue = 1;
                }
                connection.Close();
                return dataSet;
            }
        }

        public static MySqlConnection GetConnection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }
    }
}


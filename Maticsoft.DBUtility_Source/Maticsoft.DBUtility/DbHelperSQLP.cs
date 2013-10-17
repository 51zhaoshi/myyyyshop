namespace Maticsoft.DBUtility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    public class DbHelperSQLP
    {
        public string connectionString;

        public DbHelperSQLP()
        {
            this.connectionString = PubConstant.ConnectionString;
        }

        public DbHelperSQLP(string ConnectionString)
        {
            this.connectionString = PubConstant.ConnectionString;
            this.connectionString = ConnectionString;
        }

        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection) {
                CommandType = CommandType.StoredProcedure
            };
            foreach (SqlParameter parameter in parameters)
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

        public bool ColumnExists(string tableName, string columnName)
        {
            string sQLString = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object single = this.GetSingle(sQLString);
            if (single == null)
            {
                return false;
            }
            return (Convert.ToInt32(single) > 0);
        }

        public SqlParameter CreateInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return this.CreateParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        public SqlParameter CreateInputOutParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return this.CreateParam(ParamName, DbType, Size, ParameterDirection.InputOutput, Value);
        }

        public SqlParameter CreateOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return this.CreateParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }

        private SqlParameter CreateParam(string ParamName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
        {
            SqlParameter parameter;
            if (Size > 0)
            {
                parameter = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                parameter = new SqlParameter(ParamName, DbType);
            }
            parameter.Direction = Direction;
            if ((Direction != ParameterDirection.Output) || (Value != null))
            {
                parameter.Value = Value;
            }
            return parameter;
        }

        public SqlParameter CreateReturnParam(string ParamName, SqlDbType DbType, int Size)
        {
            return this.CreateParam(ParamName, DbType, Size, ParameterDirection.ReturnValue, null);
        }

        public object[] ExecProcedures(SqlCommand cmd)
        {
            object[] objArray = new object[2];
            ArrayList list = new ArrayList();
            DataSet dataSet = new DataSet();
            cmd.Connection = new SqlConnection(this.connectionString);
            new SqlDataAdapter(cmd).Fill(dataSet);
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                if ((cmd.Parameters[i].Direction == ParameterDirection.Output) || (cmd.Parameters[i].Direction == ParameterDirection.InputOutput))
                {
                    list.Add(cmd.Parameters[i].Value);
                }
            }
            objArray[0] = dataSet;
            objArray[1] = list;
            cmd.Connection.Close();
            return objArray;
        }

        public SqlDataReader ExecuteReader(string strSQL)
        {
            SqlDataReader reader2;
            SqlConnection connection = new SqlConnection(this.connectionString);
            SqlCommand command = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlDataReader reader2;
            SqlConnection conn = new SqlConnection(this.connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return reader2;
        }

        public int ExecuteSql(string SQLString)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SqlException exception)
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

        public int ExecuteSql(string SQLString, string content)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                SqlParameter parameter = new SqlParameter("@content", SqlDbType.NText) {
                    Value = content
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SqlException exception)
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

        public int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    num2 = num;
                }
                catch (SqlException exception)
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

        public int ExecuteSqlByTime(string SQLString, int Times)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    command.CommandTimeout = Times;
                    num2 = command.ExecuteNonQuery();
                }
                catch (SqlException exception)
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

        public object ExecuteSqlGet(string SQLString, string content)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
                SqlParameter parameter = new SqlParameter("@content", SqlDbType.NText) {
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
                catch (SqlException exception)
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

        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(strSQL, connection);
                SqlParameter parameter = new SqlParameter("@fs", SqlDbType.Image) {
                    Value = fs
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (SqlException exception)
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

        public int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            int num3;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (CommandInfo info in cmdList)
                        {
                            string commandText = info.CommandText;
                            SqlParameter[] parameters = (SqlParameter[]) info.Parameters;
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

        public int ExecuteSqlTran(List<string> SQLStringList)
        {
            int num3;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand {
                    Connection = connection
                };
                SqlTransaction transaction = connection.BeginTransaction();
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

        public void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[]) entry.Value;
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

        public int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
        {
            return 0;
        }

        public void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (CommandInfo info in SQLStringList)
                        {
                            string commandText = info.CommandText;
                            SqlParameter[] parameters = (SqlParameter[]) info.Parameters;
                            foreach (SqlParameter parameter in parameters)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = num;
                                }
                            }
                            PrepareCommand(cmd, connection, transaction, commandText, parameters);
                            cmd.ExecuteNonQuery();
                            foreach (SqlParameter parameter2 in parameters)
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

        public void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int num = 0;
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[]) entry.Value;
                            foreach (SqlParameter parameter in cmdParms)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    parameter.Value = num;
                                }
                            }
                            PrepareCommand(cmd, connection, transaction, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            foreach (SqlParameter parameter2 in cmdParms)
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

        public bool Exists(string strSql)
        {
            int num;
            object single = DbHelperSQL.GetSingle(strSql);
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

        public bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            int num;
            object single = DbHelperSQL.GetSingle(strSql, cmdParms);
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

        public int GetMaxID(string FieldName, string TableName)
        {
            object single = DbHelperSQL.GetSingle("select max(" + FieldName + ")+1 from " + TableName);
            if (single == null)
            {
                return 1;
            }
            return int.Parse(single.ToString());
        }

        public object GetSingle(string SQLString)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
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
                catch (SqlException exception)
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

        public object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand();
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
                catch (SqlException exception)
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

        public object GetSingle(string SQLString, int Times)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(SQLString, connection);
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
                catch (SqlException exception)
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

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
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
                foreach (SqlParameter parameter in cmdParms)
                {
                    if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new SqlDataAdapter(SQLString, connection).Fill(dataSet, "ds");
                }
                catch (SqlException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }

        public DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            DataSet set2;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataSet dataSet = new DataSet();
                    try
                    {
                        adapter.Fill(dataSet, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    set2 = dataSet;
                }
            }
            return set2;
        }

        public DataSet Query(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new SqlDataAdapter(SQLString, connection) { SelectCommand = { CommandTimeout = Times } }.Fill(dataSet, "ds");
                }
                catch (SqlException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }

        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                return (int) command.Parameters["ReturnValue"].Value;
            }
        }

        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                new SqlDataAdapter { SelectCommand = BuildQueryCommand(connection, storedProcName, parameters) }.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter {
                    SelectCommand = BuildQueryCommand(connection, storedProcName, parameters)
                };
                adapter.SelectCommand.CommandTimeout = Times;
                adapter.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public bool TabExists(string TableName)
        {
            int num;
            object single = DbHelperSQL.GetSingle("select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1");
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
    }
}


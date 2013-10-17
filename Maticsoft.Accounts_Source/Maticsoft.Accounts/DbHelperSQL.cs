namespace Maticsoft.Accounts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;

    public static class DbHelperSQL
    {
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

        public static bool ColumnExists(string tableName, string columnName)
        {
            object single = GetSingle("select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'");
            if (single == null)
            {
                return false;
            }
            return (Convert.ToInt32(single) > 0);
        }

        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlDataReader reader2;
            SqlConnection connection = new SqlConnection(connectionString);
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

        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlDataReader reader2;
            SqlConnection conn = new SqlConnection(connectionString);
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

        public static int ExecuteSql(string SQLString)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int num2;
            try
            {
                connection = new SqlConnection(connectionString);
                command = new SqlCommand(SQLString, connection);
                connection.Open();
                num2 = command.ExecuteNonQuery();
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(command, connection, null, SQLString, cmdParms);
                        int num = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        num2 = num;
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
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, string content)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(SQLString, connection))
                {
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
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return num2;
        }

        public static object ExecuteSqlGet(string SQLString, string content)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public static int ExecuteSqlTran(List<string> SQLStringList)
        {
            int num3;
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num3;
        }

        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(SQLString, connection))
                {
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
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return obj3;
        }

        public static object GetSingle(string SQLString, int Times)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(SQLString, connection))
                {
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
                        command.Dispose();
                        connection.Close();
                    }
                }
            }
            return obj3;
        }

        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            object obj3;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(command, connection, null, SQLString, cmdParms);
                        object objA = command.ExecuteScalar();
                        command.Parameters.Clear();
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

        public static DataSet Query(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                finally
                {
                    connection.Close();
                }
                return dataSet;
            }
        }

        public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
        {
            DataSet set2;
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                    finally
                    {
                        cmd.Dispose();
                        connection.Close();
                    }
                    set2 = dataSet;
                }
            }
            return set2;
        }

        public static DataSet Query(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                finally
                {
                    connection.Close();
                }
                return dataSet;
            }
        }

        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                new SqlDataAdapter { SelectCommand = BuildQueryCommand(connection, storedProcName, parameters) }.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                int num = (int) command.Parameters["ReturnValue"].Value;
                connection.Close();
                return num;
            }
        }

        public static void RunProcedure(string storedProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                new SqlDataAdapter { SelectCommand = BuildIntCommand(connection, storedProcName, parameters) }.Fill(dataSet, tableName);
                connection.Close();
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public static bool TabExists(string TableName)
        {
            int num;
            object single = GetSingle("select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1");
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

        public static string connectionString
        {
            get
            {
                return PubConstant.ConnectionString;
            }
        }
    }
}


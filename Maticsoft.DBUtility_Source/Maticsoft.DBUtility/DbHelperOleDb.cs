namespace Maticsoft.DBUtility
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.OleDb;

    public abstract class DbHelperOleDb
    {
        public static string connectionString = PubConstant.ConnectionString;

        public static OleDbDataReader ExecuteReader(string strSQL)
        {
            OleDbDataReader reader2;
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = new OleDbCommand(strSQL, connection);
            try
            {
                connection.Open();
                reader2 = command.ExecuteReader();
            }
            catch (OleDbException exception)
            {
                throw new Exception(exception.Message);
            }
            return reader2;
        }

        public static OleDbDataReader ExecuteReader(string SQLString, params OleDbParameter[] cmdParms)
        {
            OleDbDataReader reader2;
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, conn, null, SQLString, cmdParms);
                OleDbDataReader reader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch (OleDbException exception)
            {
                throw new Exception(exception.Message);
            }
            return reader2;
        }

        public static int ExecuteSql(string SQLString)
        {
            int num2;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(SQLString, connection);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (OleDbException exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
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

        public static int ExecuteSql(string SQLString, string content)
        {
            int num2;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(SQLString, connection);
                OleDbParameter parameter = new OleDbParameter("@content", OleDbType.VarChar) {
                    Value = content
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (OleDbException exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static int ExecuteSql(string SQLString, params OleDbParameter[] cmdParms)
        {
            int num2;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    num2 = num;
                }
                catch (OleDbException exception)
                {
                    throw new Exception(exception.Message);
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

        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            int num2;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                OleDbParameter parameter = new OleDbParameter("@fs", OleDbType.Binary) {
                    Value = fs
                };
                command.Parameters.Add(parameter);
                try
                {
                    connection.Open();
                    num2 = command.ExecuteNonQuery();
                }
                catch (OleDbException exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return num2;
        }

        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand {
                    Connection = connection
                };
                OleDbTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    for (int i = 0; i < SQLStringList.Count; i++)
                    {
                        string str = SQLStringList[i].ToString();
                        if (str.Trim().Length > 1)
                        {
                            command.CommandText = str;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (OleDbException exception)
                {
                    transaction.Rollback();
                    throw new Exception(exception.Message);
                }
            }
        }

        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbTransaction transaction = connection.BeginTransaction())
                {
                    OleDbCommand cmd = new OleDbCommand();
                    try
                    {
                        foreach (DictionaryEntry entry in SQLStringList)
                        {
                            string cmdText = entry.Key.ToString();
                            OleDbParameter[] cmdParms = (OleDbParameter[]) entry.Value;
                            PrepareCommand(cmd, connection, transaction, cmdText, cmdParms);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static object GetSingle(string SQLString)
        {
            object obj3;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(SQLString, connection);
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
                catch (OleDbException exception)
                {
                    connection.Close();
                    throw new Exception(exception.Message);
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

        public static object GetSingle(string SQLString, params OleDbParameter[] cmdParms)
        {
            object obj3;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
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
                catch (OleDbException exception)
                {
                    throw new Exception(exception.Message);
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

        private static void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, OleDbParameter[] cmdParms)
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
                foreach (OleDbParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public static DataSet Query(string SQLString)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                try
                {
                    connection.Open();
                    new OleDbDataAdapter(SQLString, connection).Fill(dataSet, "ds");
                }
                catch (OleDbException exception)
                {
                    throw new Exception(exception.Message);
                }
                return dataSet;
            }
        }

        public static DataSet Query(string SQLString, params OleDbParameter[] cmdParms)
        {
            DataSet set2;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    DataSet dataSet = new DataSet();
                    try
                    {
                        adapter.Fill(dataSet, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (OleDbException exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    set2 = dataSet;
                }
            }
            return set2;
        }
    }
}


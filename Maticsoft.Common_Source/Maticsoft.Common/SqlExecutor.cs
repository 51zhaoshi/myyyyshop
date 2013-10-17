namespace Maticsoft.Common
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public static class SqlExecutor
    {
        public static void ExecuteScriptFile(string connectionString, string fileMapPath)
        {
            if (!File.Exists(fileMapPath))
            {
                throw new FileNotFoundException("ExecuteScriptFile: " + fileMapPath + " FileNotFound !");
            }
            using (StreamReader reader = new StreamReader(fileMapPath, Encoding.Default))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlTransaction transaction = null;
                SqlCommand command = new SqlCommand {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandTimeout = 120
                };
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command.Transaction = transaction;
                    while (!reader.EndOfStream)
                    {
                        string str = NextSqlFromStream(reader);
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            command.CommandText = str;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw;
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
        }

        public static int ExecuteSql(string connectionString, string safeSql)
        {
            int num;
            if (string.IsNullOrWhiteSpace(safeSql))
            {
                throw new ArgumentNullException("ExecuteSql: SafeSql IsNULL !");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandTimeout = 120,
                    CommandText = safeSql
                };
                try
                {
                    connection.Open();
                    num = command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        private static string NextSqlFromStream(StreamReader reader)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                string str = reader.ReadLine();
                if (str == null)
                {
                    return string.Empty;
                }
                string strA = str.Trim();
                while (!reader.EndOfStream && (string.Compare(strA, "GO", true, CultureInfo.InvariantCulture) != 0))
                {
                    builder.Append(strA + Environment.NewLine);
                    strA = str;
                }
                if (string.Compare(strA, "GO", true, CultureInfo.InvariantCulture) != 0)
                {
                    builder.Append(strA + Environment.NewLine);
                }
                return builder.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}


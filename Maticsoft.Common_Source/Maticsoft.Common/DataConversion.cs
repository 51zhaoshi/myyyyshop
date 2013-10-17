namespace Maticsoft.Common
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;

    public class DataConversion
    {
        private const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;";

        public string DataTableToExcel(DataTable dt, string excelPath)
        {
            if (dt == null)
            {
                return "DataTable不能为空";
            }
            dt.TableName = "Sheet1";
            int count = dt.Rows.Count;
            int num2 = dt.Columns.Count;
            if (count == 0)
            {
                return "没有数据";
            }
            StringBuilder builder = new StringBuilder();
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", excelPath);
            builder.Append("CREATE TABLE ");
            builder.Append(dt.TableName + " ( ");
            for (int i = 0; i < num2; i++)
            {
                if (i < (num2 - 1))
                {
                    builder.Append(string.Format("{0} nvarchar,", dt.Columns[i].ColumnName));
                }
                else
                {
                    builder.Append(string.Format("{0} nvarchar)", dt.Columns[i].ColumnName));
                }
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand {
                    Connection = connection,
                    CommandText = builder.ToString()
                };
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    return ("在Excel中创建表失败，错误信息：" + exception.Message);
                }
                builder.Remove(0, builder.Length);
                builder.Append("INSERT INTO ");
                builder.Append(dt.TableName + " ( ");
                for (int j = 0; j < num2; j++)
                {
                    if (j < (num2 - 1))
                    {
                        builder.Append(dt.Columns[j].ColumnName + ",");
                    }
                    else
                    {
                        builder.Append(dt.Columns[j].ColumnName + ") values (");
                    }
                }
                for (int k = 0; k < num2; k++)
                {
                    if (k < (num2 - 1))
                    {
                        builder.Append("@" + dt.Columns[k].ColumnName + ",");
                    }
                    else
                    {
                        builder.Append("@" + dt.Columns[k].ColumnName + ")");
                    }
                }
                command.CommandText = builder.ToString();
                OleDbParameterCollection parameters = command.Parameters;
                for (int m = 0; m < num2; m++)
                {
                    parameters.Add(new OleDbParameter("@" + dt.Columns[m].ColumnName, OleDbType.VarChar));
                }
                foreach (DataRow row in dt.Rows)
                {
                    for (int n = 0; n < parameters.Count; n++)
                    {
                        parameters[n].Value = row[n];
                    }
                    command.ExecuteNonQuery();
                }
                return "数据已成功导入Excel";
            }
        }

        public DataSet ExcelToDS(string Path)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string selectCommandText = "";
            OleDbDataAdapter adapter = null;
            DataSet dataSet = null;
            selectCommandText = "select * from [Sheet1$]";
            adapter = new OleDbDataAdapter(selectCommandText, connectionString);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "table1");
            connection.Close();
            return dataSet;
        }
    }
}


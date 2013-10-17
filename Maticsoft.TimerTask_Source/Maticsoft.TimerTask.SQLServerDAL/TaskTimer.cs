namespace Maticsoft.TimerTask.SQLServerDAL
{
    using Maticsoft.DBUtility;
    using Maticsoft.TimerTask.DAL;
    using Maticsoft.TimerTask.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TaskTimer : ITaskTimer
    {
        public int Add(Maticsoft.TimerTask.Model.TaskTimer model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Ms_TaskTimers(");
            builder.Append("ExecuteType,IsSingle,Interval,ExecuteTime,ExecuteNumber,Param1,Param2,Param3,Param4,Param5,Param6,Param7,Param8,Param9,Param10)");
            builder.Append(" values (");
            builder.Append("@ExecuteType,@IsSingle,@Interval,@ExecuteTime,@ExecuteNumber,@Param1,@Param2,@Param3,@Param4,@Param5,@Param6,@Param7,@Param8,@Param9,@Param10)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExecuteType", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@IsSingle", SqlDbType.Bit, 1), new SqlParameter("@Interval", SqlDbType.Decimal, 9), new SqlParameter("@ExecuteTime", SqlDbType.DateTime), new SqlParameter("@ExecuteNumber", SqlDbType.Int, 4), new SqlParameter("@Param1", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param2", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param3", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param4", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param5", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param6", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param7", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param8", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param9", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param10", SqlDbType.NVarChar, 0xfa0) };
            cmdParms[0].Value = model.ExecuteType;
            cmdParms[1].Value = model.IsSingle;
            cmdParms[2].Value = model.Interval;
            cmdParms[3].Value = model.ExecuteTime;
            cmdParms[4].Value = model.ExecuteNumber;
            cmdParms[5].Value = model.Param1;
            cmdParms[6].Value = model.Param2;
            cmdParms[7].Value = model.Param3;
            cmdParms[8].Value = model.Param4;
            cmdParms[9].Value = model.Param5;
            cmdParms[10].Value = model.Param6;
            cmdParms[11].Value = model.Param7;
            cmdParms[12].Value = model.Param8;
            cmdParms[13].Value = model.Param9;
            cmdParms[14].Value = model.Param10;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.TimerTask.Model.TaskTimer DataRowToModel(DataRow row)
        {
            Maticsoft.TimerTask.Model.TaskTimer timer = new Maticsoft.TimerTask.Model.TaskTimer();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    timer.ID = int.Parse(row["ID"].ToString());
                }
                if (row["ExecuteType"] != null)
                {
                    timer.ExecuteType = row["ExecuteType"].ToString();
                }
                if ((row["IsSingle"] != null) && (row["IsSingle"].ToString() != ""))
                {
                    if ((row["IsSingle"].ToString() == "1") || (row["IsSingle"].ToString().ToLower() == "true"))
                    {
                        timer.IsSingle = true;
                    }
                    else
                    {
                        timer.IsSingle = false;
                    }
                }
                if ((row["Interval"] != null) && (row["Interval"].ToString() != ""))
                {
                    timer.Interval = decimal.Parse(row["Interval"].ToString());
                }
                if ((row["ExecuteTime"] != null) && (row["ExecuteTime"].ToString() != ""))
                {
                    timer.ExecuteTime = DateTime.Parse(row["ExecuteTime"].ToString());
                }
                if ((row["ExecuteNumber"] != null) && (row["ExecuteNumber"].ToString() != ""))
                {
                    timer.ExecuteNumber = int.Parse(row["ExecuteNumber"].ToString());
                }
                if (row["Param1"] != null)
                {
                    timer.Param1 = row["Param1"].ToString();
                }
                if (row["Param2"] != null)
                {
                    timer.Param2 = row["Param2"].ToString();
                }
                if (row["Param3"] != null)
                {
                    timer.Param3 = row["Param3"].ToString();
                }
                if (row["Param4"] != null)
                {
                    timer.Param4 = row["Param4"].ToString();
                }
                if (row["Param5"] != null)
                {
                    timer.Param5 = row["Param5"].ToString();
                }
                if (row["Param6"] != null)
                {
                    timer.Param6 = row["Param6"].ToString();
                }
                if (row["Param7"] != null)
                {
                    timer.Param7 = row["Param7"].ToString();
                }
                if (row["Param8"] != null)
                {
                    timer.Param8 = row["Param8"].ToString();
                }
                if (row["Param9"] != null)
                {
                    timer.Param9 = row["Param9"].ToString();
                }
                if (row["Param10"] != null)
                {
                    timer.Param10 = row["Param10"].ToString();
                }
            }
            return timer;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_TaskTimers ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Ms_TaskTimers ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Ms_TaskTimers");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ExecuteType,IsSingle,Interval,ExecuteTime,ExecuteNumber,Param1,Param2,Param3,Param4,Param5,Param6,Param7,Param8,Param9,Param10 ");
            builder.Append(" FROM Ms_TaskTimers ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ID,ExecuteType,IsSingle,Interval,ExecuteTime,ExecuteNumber,Param1,Param2,Param3,Param4,Param5,Param6,Param7,Param8,Param9,Param10 ");
            builder.Append(" FROM Ms_TaskTimers ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from Ms_TaskTimers T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "Ms_TaskTimers");
        }

        public Maticsoft.TimerTask.Model.TaskTimer GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,ExecuteType,IsSingle,Interval,ExecuteTime,ExecuteNumber,Param1,Param2,Param3,Param4,Param5,Param6,Param7,Param8,Param9,Param10 from Ms_TaskTimers ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.TimerTask.Model.TaskTimer();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Ms_TaskTimers ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.TimerTask.Model.TaskTimer model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Ms_TaskTimers set ");
            builder.Append("ExecuteType=@ExecuteType,");
            builder.Append("IsSingle=@IsSingle,");
            builder.Append("Interval=@Interval,");
            builder.Append("ExecuteTime=@ExecuteTime,");
            builder.Append("ExecuteNumber=@ExecuteNumber,");
            builder.Append("Param1=@Param1,");
            builder.Append("Param2=@Param2,");
            builder.Append("Param3=@Param3,");
            builder.Append("Param4=@Param4,");
            builder.Append("Param5=@Param5,");
            builder.Append("Param6=@Param6,");
            builder.Append("Param7=@Param7,");
            builder.Append("Param8=@Param8,");
            builder.Append("Param9=@Param9,");
            builder.Append("Param10=@Param10");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ExecuteType", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@IsSingle", SqlDbType.Bit, 1), new SqlParameter("@Interval", SqlDbType.Decimal, 9), new SqlParameter("@ExecuteTime", SqlDbType.DateTime), new SqlParameter("@ExecuteNumber", SqlDbType.Int, 4), new SqlParameter("@Param1", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param2", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param3", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param4", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param5", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param6", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param7", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param8", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param9", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@Param10", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ExecuteType;
            cmdParms[1].Value = model.IsSingle;
            cmdParms[2].Value = model.Interval;
            cmdParms[3].Value = model.ExecuteTime;
            cmdParms[4].Value = model.ExecuteNumber;
            cmdParms[5].Value = model.Param1;
            cmdParms[6].Value = model.Param2;
            cmdParms[7].Value = model.Param3;
            cmdParms[8].Value = model.Param4;
            cmdParms[9].Value = model.Param5;
            cmdParms[10].Value = model.Param6;
            cmdParms[11].Value = model.Param7;
            cmdParms[12].Value = model.Param8;
            cmdParms[13].Value = model.Param9;
            cmdParms[14].Value = model.Param10;
            cmdParms[15].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


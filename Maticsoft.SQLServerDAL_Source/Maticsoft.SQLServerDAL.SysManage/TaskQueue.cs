namespace Maticsoft.SQLServerDAL.SysManage
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SysManage;
    using Maticsoft.Model.SysManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TaskQueue : ITaskQueue
    {
        public bool Add(Maticsoft.Model.SysManage.TaskQueue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SA_TaskQueue(");
            builder.Append("ID,Type,TaskId,Status,RunDate)");
            builder.Append(" values (");
            builder.Append("@ID,@Type,@TaskId,@Status,@RunDate)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TaskId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@RunDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ID;
            cmdParms[1].Value = model.Type;
            cmdParms[2].Value = model.TaskId;
            cmdParms[3].Value = model.Status;
            cmdParms[4].Value = model.RunDate;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.SysManage.TaskQueue DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SysManage.TaskQueue queue = new Maticsoft.Model.SysManage.TaskQueue();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    queue.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    queue.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["TaskId"] != null) && (row["TaskId"].ToString() != ""))
                {
                    queue.TaskId = int.Parse(row["TaskId"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    queue.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["RunDate"] != null) && (row["RunDate"].ToString() != ""))
                {
                    queue.RunDate = new DateTime?(DateTime.Parse(row["RunDate"].ToString()));
                }
            }
            return queue;
        }

        public bool Delete(int ID, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_TaskQueue ");
            builder.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            cmdParms[1].Value = Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteArticle()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_TaskQueue where type=0");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteTask(int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SA_TaskQueue where type=@Type");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int ID, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SA_TaskQueue");
            builder.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            cmdParms[1].Value = Type;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetContinueTask(int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Type,TaskId,Status,RunDate ");
            builder.Append(" FROM SA_TaskQueue ");
            builder.Append(" where  type=" + type + "  and Status=0 order by ID");
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SysManage.TaskQueue GetLastModel(int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,Type,TaskId,Status,RunDate from SA_TaskQueue ");
            builder.Append(" where type=" + type + " and Status=1 order by ID desc ");
            new Maticsoft.Model.SysManage.TaskQueue();
            DataSet set = DbHelperSQL.Query(builder.ToString());
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Type,TaskId,Status,RunDate ");
            builder.Append(" FROM SA_TaskQueue ");
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
            builder.Append(" ID,Type,TaskId,Status,RunDate ");
            builder.Append(" FROM SA_TaskQueue ");
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
                builder.Append("order by T.Type desc");
            }
            builder.Append(")AS Row, T.*  from SA_TaskQueue T ");
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
            return DbHelperSQL.GetMaxID("ID", "SA_TaskQueue");
        }

        public Maticsoft.Model.SysManage.TaskQueue GetModel(int ID, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,Type,TaskId,Status,RunDate from SA_TaskQueue ");
            builder.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            cmdParms[1].Value = Type;
            new Maticsoft.Model.SysManage.TaskQueue();
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
            builder.Append("select count(1) FROM SA_TaskQueue ");
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

        public bool Update(Maticsoft.Model.SysManage.TaskQueue model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SA_TaskQueue set ");
            builder.Append("TaskId=@TaskId,");
            builder.Append("Status=@Status,");
            builder.Append("RunDate=@RunDate");
            builder.Append(" where ID=@ID and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TaskId", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@RunDate", SqlDbType.DateTime), new SqlParameter("@ID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TaskId;
            cmdParms[1].Value = model.Status;
            cmdParms[2].Value = model.RunDate;
            cmdParms[3].Value = model.ID;
            cmdParms[4].Value = model.Type;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


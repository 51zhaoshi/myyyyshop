namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PointsLimit : IPointsLimit
    {
        public int Add(Maticsoft.Model.Members.PointsLimit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_PointsLimit(");
            builder.Append("Name,Cycle,CycleUnit,MaxTimes)");
            builder.Append(" values (");
            builder.Append("@Name,@Cycle,@CycleUnit,@MaxTimes)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Cycle", SqlDbType.Int, 4), new SqlParameter("@CycleUnit", SqlDbType.NVarChar, 50), new SqlParameter("@MaxTimes", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Cycle;
            cmdParms[2].Value = model.CycleUnit;
            cmdParms[3].Value = model.MaxTimes;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int PointsLimitID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_PointsLimit ");
            builder.Append(" where PointsLimitID=@PointsLimitID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PointsLimitID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PointsLimitID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEX(int PointsLimitID)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_PointsLimit ");
            builder.Append(" where PointsLimitID=@PointsLimitID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@PointsLimitID", SqlDbType.Int, 4) };
            para[0].Value = PointsLimitID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("UPDATE Accounts_PointsRule SET PointsLimitID=-1 WHERE PointsLimitID=@PointsLimitID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@PointsLimitID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = PointsLimitID;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool DeleteList(string PointsLimitIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_PointsLimit ");
            builder.Append(" where PointsLimitID in (" + PointsLimitIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int PointsLimitID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_PointsLimit");
            builder.Append(" where PointsLimitID=@PointsLimitID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PointsLimitID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PointsLimitID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PointsLimitID,Name,Cycle,CycleUnit,MaxTimes ");
            builder.Append(" FROM Accounts_PointsLimit ");
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
            builder.Append(" PointsLimitID,Name,Cycle,CycleUnit,MaxTimes ");
            builder.Append(" FROM Accounts_PointsLimit ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.PointsLimitID desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_PointsLimit T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PointsLimitID", "Accounts_PointsLimit");
        }

        public Maticsoft.Model.Members.PointsLimit GetModel(int PointsLimitID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 PointsLimitID,Name,Cycle,CycleUnit,MaxTimes from Accounts_PointsLimit ");
            builder.Append(" where PointsLimitID=@PointsLimitID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PointsLimitID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PointsLimitID;
            Maticsoft.Model.Members.PointsLimit limit = new Maticsoft.Model.Members.PointsLimit();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["PointsLimitID"] != null) && (set.Tables[0].Rows[0]["PointsLimitID"].ToString() != ""))
            {
                limit.PointsLimitID = int.Parse(set.Tables[0].Rows[0]["PointsLimitID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Name"] != null) && (set.Tables[0].Rows[0]["Name"].ToString() != ""))
            {
                limit.Name = set.Tables[0].Rows[0]["Name"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Cycle"] != null) && (set.Tables[0].Rows[0]["Cycle"].ToString() != ""))
            {
                limit.Cycle = int.Parse(set.Tables[0].Rows[0]["Cycle"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CycleUnit"] != null) && (set.Tables[0].Rows[0]["CycleUnit"].ToString() != ""))
            {
                limit.CycleUnit = set.Tables[0].Rows[0]["CycleUnit"].ToString();
            }
            if ((set.Tables[0].Rows[0]["MaxTimes"] != null) && (set.Tables[0].Rows[0]["MaxTimes"].ToString() != ""))
            {
                limit.MaxTimes = int.Parse(set.Tables[0].Rows[0]["MaxTimes"].ToString());
            }
            return limit;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Accounts_PointsLimit ");
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

        public bool Update(Maticsoft.Model.Members.PointsLimit model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_PointsLimit set ");
            builder.Append("Name=@Name,");
            builder.Append("Cycle=@Cycle,");
            builder.Append("CycleUnit=@CycleUnit,");
            builder.Append("MaxTimes=@MaxTimes");
            builder.Append(" where PointsLimitID=@PointsLimitID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Cycle", SqlDbType.Int, 4), new SqlParameter("@CycleUnit", SqlDbType.NVarChar, 50), new SqlParameter("@MaxTimes", SqlDbType.Int, 4), new SqlParameter("@PointsLimitID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Cycle;
            cmdParms[2].Value = model.CycleUnit;
            cmdParms[3].Value = model.MaxTimes;
            cmdParms[4].Value = model.PointsLimitID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


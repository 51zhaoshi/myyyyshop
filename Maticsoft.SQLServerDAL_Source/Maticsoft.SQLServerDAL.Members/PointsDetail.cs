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

    public class PointsDetail : IPointsDetail
    {
        public int Add(Maticsoft.Model.Members.PointsDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_PointsDetail(");
            builder.Append("RuleAction,UserID,Score,ExtData,CurrentPoints,Description,CreatedDate,Type)");
            builder.Append(" values (");
            builder.Append("@RuleAction,@UserID,@Score,@ExtData,@CurrentPoints,@Description,@CreatedDate,@Type)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Score", SqlDbType.Int, 4), new SqlParameter("@ExtData", SqlDbType.NVarChar), new SqlParameter("@CurrentPoints", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RuleAction;
            cmdParms[1].Value = model.UserID;
            cmdParms[2].Value = model.Score;
            cmdParms[3].Value = model.ExtData;
            cmdParms[4].Value = model.CurrentPoints;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Type;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddDetail(Maticsoft.Model.Members.PointsDetail model)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_PointsDetail(");
            builder.Append("RuleAction,UserID,Score,ExtData,CurrentPoints,Description,CreatedDate,Type)");
            builder.Append(" values (");
            builder.Append("@RuleAction,@UserID,@Score,@ExtData,0,@Description,@CreatedDate,@Type)");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Score", SqlDbType.Int, 4), new SqlParameter("@ExtData", SqlDbType.NVarChar), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4) };
            para[0].Value = model.RuleAction;
            para[1].Value = model.UserID;
            para[2].Value = model.Score;
            para[3].Value = model.ExtData;
            para[4].Value = model.Description;
            para[5].Value = model.CreatedDate;
            para[6].Value = model.Type;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("update Accounts_UsersExp set ");
            if (model.Type == 0)
            {
                builder2.Append("Points=Points+@Points");
            }
            if (model.Type == 1)
            {
                builder2.Append("Points=Points-@Points");
            }
            builder2.Append(" where UserID=@UserID ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.Score;
            parameterArray2[1].Value = model.UserID;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool Delete(int PointsDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_PointsDetail ");
            builder.Append(" where PointsDetailID=@PointsDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PointsDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PointsDetailID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string PointsDetailIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_PointsDetail ");
            builder.Append(" where PointsDetailID in (" + PointsDetailIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int PointsDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_PointsDetail");
            builder.Append(" where PointsDetailID=@PointsDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PointsDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PointsDetailID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PointsDetailID,RuleAction,UserID,Score,ExtData,CurrentPoints,Description,CreatedDate,Type ");
            builder.Append(" FROM Accounts_PointsDetail ");
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
            builder.Append(" PointsDetailID,RuleAction,UserID,Score,ExtData,CurrentPoints,Description,CreatedDate,Type ");
            builder.Append(" FROM Accounts_PointsDetail ");
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
                builder.Append("order by T.PointsDetailID desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_PointsDetail T ");
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
            return DbHelperSQL.GetMaxID("PointsDetailID", "Accounts_PointsDetail");
        }

        public Maticsoft.Model.Members.PointsDetail GetModel(int PointsDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 PointsDetailID,RuleAction,UserID,Score,ExtData,CurrentPoints,Description,CreatedDate,Type from Accounts_PointsDetail ");
            builder.Append(" where PointsDetailID=@PointsDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PointsDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PointsDetailID;
            Maticsoft.Model.Members.PointsDetail detail = new Maticsoft.Model.Members.PointsDetail();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["PointsDetailID"] != null) && (set.Tables[0].Rows[0]["PointsDetailID"].ToString() != ""))
            {
                detail.PointsDetailID = int.Parse(set.Tables[0].Rows[0]["PointsDetailID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["RuleAction"] != null) && (set.Tables[0].Rows[0]["RuleAction"].ToString() != ""))
            {
                detail.RuleAction = set.Tables[0].Rows[0]["RuleAction"].ToString();
            }
            if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
            {
                detail.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Score"] != null) && (set.Tables[0].Rows[0]["Score"].ToString() != ""))
            {
                detail.Score = int.Parse(set.Tables[0].Rows[0]["Score"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ExtData"] != null) && (set.Tables[0].Rows[0]["ExtData"].ToString() != ""))
            {
                detail.ExtData = set.Tables[0].Rows[0]["ExtData"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CurrentPoints"] != null) && (set.Tables[0].Rows[0]["CurrentPoints"].ToString() != ""))
            {
                detail.CurrentPoints = int.Parse(set.Tables[0].Rows[0]["CurrentPoints"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                detail.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                detail.CreatedDate = DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Type"] != null) && (set.Tables[0].Rows[0]["Type"].ToString() != ""))
            {
                detail.Type = int.Parse(set.Tables[0].Rows[0]["Type"].ToString());
            }
            return detail;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Accounts_PointsDetail ");
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

        public bool Update(Maticsoft.Model.Members.PointsDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_PointsDetail set ");
            builder.Append("RuleAction=@RuleAction,");
            builder.Append("UserID=@UserID,");
            builder.Append("Score=@Score,");
            builder.Append("ExtData=@ExtData,");
            builder.Append("CurrentPoints=@CurrentPoints,");
            builder.Append("Description=@Description,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Type=@Type");
            builder.Append(" where PointsDetailID=@PointsDetailID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleAction", SqlDbType.NVarChar, 200), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Score", SqlDbType.Int, 4), new SqlParameter("@ExtData", SqlDbType.NVarChar), new SqlParameter("@CurrentPoints", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@PointsDetailID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RuleAction;
            cmdParms[1].Value = model.UserID;
            cmdParms[2].Value = model.Score;
            cmdParms[3].Value = model.ExtData;
            cmdParms[4].Value = model.CurrentPoints;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Type;
            cmdParms[8].Value = model.PointsDetailID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdatePoints(int userId, int points, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UsersExp set ");
            if (type == 0)
            {
                builder.Append("Points=Points+@Points");
            }
            if (type == 1)
            {
                builder.Append("Points=Points-@Points");
            }
            builder.Append(" where UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Points", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = points;
            cmdParms[1].Value = userId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


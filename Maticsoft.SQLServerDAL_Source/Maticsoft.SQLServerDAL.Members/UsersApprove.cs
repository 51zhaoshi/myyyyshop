namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UsersApprove : IUsersApprove
    {
        public int Add(Maticsoft.Model.Members.UsersApprove model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Accounts_UsersApprove(");
            builder.Append("UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate)");
            builder.Append(" VALUES (");
            builder.Append("@UserID,@TrueName,@IDCardNum,@FrontView,@RearView,@DueDate,@Status,@ApproveUserID,@UserType,@CreatedDate,@ApproveDate)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@IDCardNum", SqlDbType.NVarChar, 20), new SqlParameter("@FrontView", SqlDbType.NVarChar, 500), new SqlParameter("@RearView", SqlDbType.NVarChar, 500), new SqlParameter("@DueDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ApproveUserID", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ApproveDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.TrueName;
            cmdParms[2].Value = model.IDCardNum;
            cmdParms[3].Value = model.FrontView;
            cmdParms[4].Value = model.RearView;
            cmdParms[5].Value = model.DueDate;
            cmdParms[6].Value = model.Status;
            cmdParms[7].Value = model.ApproveUserID;
            cmdParms[8].Value = model.UserType;
            cmdParms[9].Value = model.CreatedDate;
            cmdParms[10].Value = model.ApproveDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool BatchUpdate(string ids, string status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_UsersApprove ");
            builder.AppendFormat("SET Status={0} ,ApproveDate=GETDATE() ", status);
            builder.AppendFormat("WHERE ApproveID IN ({0}) ", ids);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Delete(int ApproveID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Accounts_UsersApprove ");
            builder.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ApproveID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ApproveID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteByUserId(int userId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Accounts_UsersApprove ");
            builder.Append(" WHERE UserID=@UserID AND Status=2 ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = userId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ApproveIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Accounts_UsersApprove ");
            builder.Append(" WHERE ApproveID in (" + ApproveIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ApproveID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Accounts_UsersApprove");
            builder.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ApproveID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ApproveID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetApproveList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ApproveID, UA.UserID,UserName, UA.TrueName, IDCardNum, FrontView, RearView, DueDate, Status, ApproveUserID, UA.UserType, CreatedDate, ApproveDate  ");
            builder.Append("FROM Accounts_UsersApprove UA  ");
            builder.Append("LEFT JOIN Accounts_Users AU ON UA.UserID = AU.UserID ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(strWhere);
            }
            builder.Append("ORDER BY UA.CreatedDate DESC ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate ");
            builder.Append(" FROM Accounts_UsersApprove ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate ");
            builder.Append(" FROM Accounts_UsersApprove ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.ApproveID desc");
            }
            builder.Append(")AS Row, T.*  FROM Accounts_UsersApprove T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ApproveID", "Accounts_UsersApprove");
        }

        public Maticsoft.Model.Members.UsersApprove GetModel(int ApproveID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate FROM Accounts_UsersApprove ");
            builder.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ApproveID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ApproveID;
            Maticsoft.Model.Members.UsersApprove approve = new Maticsoft.Model.Members.UsersApprove();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ApproveID"] != null) && (set.Tables[0].Rows[0]["ApproveID"].ToString() != ""))
            {
                approve.ApproveID = int.Parse(set.Tables[0].Rows[0]["ApproveID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
            {
                approve.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["TrueName"] != null) && (set.Tables[0].Rows[0]["TrueName"].ToString() != ""))
            {
                approve.TrueName = set.Tables[0].Rows[0]["TrueName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IDCardNum"] != null) && (set.Tables[0].Rows[0]["IDCardNum"].ToString() != ""))
            {
                approve.IDCardNum = set.Tables[0].Rows[0]["IDCardNum"].ToString();
            }
            if ((set.Tables[0].Rows[0]["FrontView"] != null) && (set.Tables[0].Rows[0]["FrontView"].ToString() != ""))
            {
                approve.FrontView = set.Tables[0].Rows[0]["FrontView"].ToString();
            }
            if ((set.Tables[0].Rows[0]["RearView"] != null) && (set.Tables[0].Rows[0]["RearView"].ToString() != ""))
            {
                approve.RearView = set.Tables[0].Rows[0]["RearView"].ToString();
            }
            if ((set.Tables[0].Rows[0]["DueDate"] != null) && (set.Tables[0].Rows[0]["DueDate"].ToString() != ""))
            {
                approve.DueDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["DueDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                approve.Status = int.Parse(set.Tables[0].Rows[0]["Status"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ApproveUserID"] != null) && (set.Tables[0].Rows[0]["ApproveUserID"].ToString() != ""))
            {
                approve.ApproveUserID = int.Parse(set.Tables[0].Rows[0]["ApproveUserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserType"] != null) && (set.Tables[0].Rows[0]["UserType"].ToString() != ""))
            {
                approve.UserType = new int?(int.Parse(set.Tables[0].Rows[0]["UserType"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                approve.CreatedDate = DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ApproveDate"] != null) && (set.Tables[0].Rows[0]["ApproveDate"].ToString() != ""))
            {
                approve.ApproveDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["ApproveDate"].ToString()));
            }
            return approve;
        }

        public Maticsoft.Model.Members.UsersApprove GetModelByUserID(int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ApproveID,UserID,TrueName,IDCardNum,FrontView,RearView,DueDate,Status,ApproveUserID,UserType,CreatedDate,ApproveDate FROM Accounts_UsersApprove ");
            builder.Append(" WHERE UserID=@UserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserID;
            Maticsoft.Model.Members.UsersApprove approve = new Maticsoft.Model.Members.UsersApprove();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ApproveID"] != null) && (set.Tables[0].Rows[0]["ApproveID"].ToString() != ""))
            {
                approve.ApproveID = int.Parse(set.Tables[0].Rows[0]["ApproveID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserID"] != null) && (set.Tables[0].Rows[0]["UserID"].ToString() != ""))
            {
                approve.UserID = int.Parse(set.Tables[0].Rows[0]["UserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["TrueName"] != null) && (set.Tables[0].Rows[0]["TrueName"].ToString() != ""))
            {
                approve.TrueName = set.Tables[0].Rows[0]["TrueName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["IDCardNum"] != null) && (set.Tables[0].Rows[0]["IDCardNum"].ToString() != ""))
            {
                approve.IDCardNum = set.Tables[0].Rows[0]["IDCardNum"].ToString();
            }
            if ((set.Tables[0].Rows[0]["FrontView"] != null) && (set.Tables[0].Rows[0]["FrontView"].ToString() != ""))
            {
                approve.FrontView = set.Tables[0].Rows[0]["FrontView"].ToString();
            }
            if ((set.Tables[0].Rows[0]["RearView"] != null) && (set.Tables[0].Rows[0]["RearView"].ToString() != ""))
            {
                approve.RearView = set.Tables[0].Rows[0]["RearView"].ToString();
            }
            if ((set.Tables[0].Rows[0]["DueDate"] != null) && (set.Tables[0].Rows[0]["DueDate"].ToString() != ""))
            {
                approve.DueDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["DueDate"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Status"] != null) && (set.Tables[0].Rows[0]["Status"].ToString() != ""))
            {
                approve.Status = int.Parse(set.Tables[0].Rows[0]["Status"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ApproveUserID"] != null) && (set.Tables[0].Rows[0]["ApproveUserID"].ToString() != ""))
            {
                approve.ApproveUserID = int.Parse(set.Tables[0].Rows[0]["ApproveUserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserType"] != null) && (set.Tables[0].Rows[0]["UserType"].ToString() != ""))
            {
                approve.UserType = new int?(int.Parse(set.Tables[0].Rows[0]["UserType"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                approve.CreatedDate = DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ApproveDate"] != null) && (set.Tables[0].Rows[0]["ApproveDate"].ToString() != ""))
            {
                approve.ApproveDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["ApproveDate"].ToString()));
            }
            return approve;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Accounts_UsersApprove ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Members.UsersApprove model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Accounts_UsersApprove SET ");
            builder.Append("UserID=@UserID,");
            builder.Append("TrueName=@TrueName,");
            builder.Append("IDCardNum=@IDCardNum,");
            builder.Append("FrontView=@FrontView,");
            builder.Append("RearView=@RearView,");
            builder.Append("DueDate=@DueDate,");
            builder.Append("Status=@Status,");
            builder.Append("ApproveUserID=@ApproveUserID,");
            builder.Append("UserType=@UserType,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("ApproveDate=@ApproveDate");
            builder.Append(" WHERE ApproveID=@ApproveID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@TrueName", SqlDbType.NVarChar, 50), new SqlParameter("@IDCardNum", SqlDbType.NVarChar, 20), new SqlParameter("@FrontView", SqlDbType.NVarChar, 500), new SqlParameter("@RearView", SqlDbType.NVarChar, 500), new SqlParameter("@DueDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ApproveUserID", SqlDbType.Int, 4), new SqlParameter("@UserType", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ApproveDate", SqlDbType.DateTime), new SqlParameter("@ApproveID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserID;
            cmdParms[1].Value = model.TrueName;
            cmdParms[2].Value = model.IDCardNum;
            cmdParms[3].Value = model.FrontView;
            cmdParms[4].Value = model.RearView;
            cmdParms[5].Value = model.DueDate;
            cmdParms[6].Value = model.Status;
            cmdParms[7].Value = model.ApproveUserID;
            cmdParms[8].Value = model.UserType;
            cmdParms[9].Value = model.CreatedDate;
            cmdParms[10].Value = model.ApproveDate;
            cmdParms[11].Value = model.ApproveID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


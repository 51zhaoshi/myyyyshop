namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserBind : IUserBind
    {
        public int Add(Maticsoft.Model.Members.UserBind model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UserBind(");
            builder.Append("UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status)");
            builder.Append(" values (");
            builder.Append("@UserId,@TokenAccess,@TokenExpireTime,@TokenRefresh,@MediaUserID,@MediaNickName,@MediaID,@iHome,@Comment,@GroupTopic,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@TokenAccess", SqlDbType.NVarChar, 200), new SqlParameter("@TokenExpireTime", SqlDbType.DateTime), new SqlParameter("@TokenRefresh", SqlDbType.NVarChar, 200), new SqlParameter("@MediaUserID", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@MediaNickName", SqlDbType.NVarChar, 200), new SqlParameter("@MediaID", SqlDbType.Int, 4), new SqlParameter("@iHome", SqlDbType.Bit, 1), new SqlParameter("@Comment", SqlDbType.Bit, 1), new SqlParameter("@GroupTopic", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.TokenAccess;
            cmdParms[2].Value = model.TokenExpireTime;
            cmdParms[3].Value = model.TokenRefresh;
            cmdParms[4].Value = model.MediaUserID;
            cmdParms[5].Value = model.MediaNickName;
            cmdParms[6].Value = model.MediaID;
            cmdParms[7].Value = model.iHome;
            cmdParms[8].Value = model.Comment;
            cmdParms[9].Value = model.GroupTopic;
            cmdParms[10].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Members.UserBind DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.UserBind bind = new Maticsoft.Model.Members.UserBind();
            if (row != null)
            {
                if ((row["BindId"] != null) && (row["BindId"].ToString() != ""))
                {
                    bind.BindId = int.Parse(row["BindId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    bind.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["TokenAccess"] != null)
                {
                    bind.TokenAccess = row["TokenAccess"].ToString();
                }
                if ((row["TokenExpireTime"] != null) && (row["TokenExpireTime"].ToString() != ""))
                {
                    bind.TokenExpireTime = new DateTime?(DateTime.Parse(row["TokenExpireTime"].ToString()));
                }
                if (row["TokenRefresh"] != null)
                {
                    bind.TokenRefresh = row["TokenRefresh"].ToString();
                }
                if (row["MediaUserID"] != null)
                {
                    bind.MediaUserID = row["MediaUserID"].ToString();
                }
                if (row["MediaNickName"] != null)
                {
                    bind.MediaNickName = row["MediaNickName"].ToString();
                }
                if ((row["MediaID"] != null) && (row["MediaID"].ToString() != ""))
                {
                    bind.MediaID = int.Parse(row["MediaID"].ToString());
                }
                if ((row["iHome"] != null) && (row["iHome"].ToString() != ""))
                {
                    if ((row["iHome"].ToString() == "1") || (row["iHome"].ToString().ToLower() == "true"))
                    {
                        bind.iHome = true;
                    }
                    else
                    {
                        bind.iHome = false;
                    }
                }
                if ((row["Comment"] != null) && (row["Comment"].ToString() != ""))
                {
                    if ((row["Comment"].ToString() == "1") || (row["Comment"].ToString().ToLower() == "true"))
                    {
                        bind.Comment = true;
                    }
                    else
                    {
                        bind.Comment = false;
                    }
                }
                if ((row["GroupTopic"] != null) && (row["GroupTopic"].ToString() != ""))
                {
                    if ((row["GroupTopic"].ToString() == "1") || (row["GroupTopic"].ToString().ToLower() == "true"))
                    {
                        bind.GroupTopic = true;
                    }
                    else
                    {
                        bind.GroupTopic = false;
                    }
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    bind.Status = new int?(int.Parse(row["Status"].ToString()));
                }
            }
            return bind;
        }

        public bool Delete(int BindId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_UserBind ");
            builder.Append(" where BindId=@BindId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BindId", SqlDbType.Int, 4) };
            cmdParms[0].Value = BindId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string BindIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_UserBind ");
            builder.Append(" where BindId in (" + BindIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int BindId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserBind");
            builder.Append(" where BindId=@BindId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BindId", SqlDbType.Int, 4) };
            cmdParms[0].Value = BindId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(int userId, string MediaUserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserBind");
            builder.Append(" where userId=@UserId and MediaUserID=@MediaUserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@MediaUserID", SqlDbType.NVarChar, 0x3e8) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = MediaUserID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select BindId,UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status ");
            builder.Append(" FROM Accounts_UserBind ");
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
            builder.Append(" BindId,UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status ");
            builder.Append(" FROM Accounts_UserBind ");
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
                builder.Append("order by T.BindId desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_UserBind T ");
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
            return DbHelperSQL.GetMaxID("BindId", "Accounts_UserBind");
        }

        public Maticsoft.Model.Members.UserBind GetModel(int BindId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 BindId,UserId,TokenAccess,TokenExpireTime,TokenRefresh,MediaUserID,MediaNickName,MediaID,iHome,Comment,GroupTopic,Status from Accounts_UserBind ");
            builder.Append(" where BindId=@BindId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@BindId", SqlDbType.Int, 4) };
            cmdParms[0].Value = BindId;
            new Maticsoft.Model.Members.UserBind();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Maticsoft.Model.Members.UserBind GetModel(int userId, int MediaID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 * from Accounts_UserBind ");
            builder.Append(" where userId=@UserId and MediaID=@MediaID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@MediaID", SqlDbType.Int, 4) };
            cmdParms[0].Value = userId;
            cmdParms[1].Value = MediaID;
            new Maticsoft.Model.Members.UserBind();
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
            builder.Append("select count(1) FROM Accounts_UserBind ");
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

        public bool Update(Maticsoft.Model.Members.UserBind model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UserBind set ");
            builder.Append("UserId=@UserId,");
            builder.Append("TokenAccess=@TokenAccess,");
            builder.Append("TokenExpireTime=@TokenExpireTime,");
            builder.Append("TokenRefresh=@TokenRefresh,");
            builder.Append("MediaUserID=@MediaUserID,");
            builder.Append("MediaNickName=@MediaNickName,");
            builder.Append("MediaID=@MediaID,");
            builder.Append("iHome=@iHome,");
            builder.Append("Comment=@Comment,");
            builder.Append("GroupTopic=@GroupTopic,");
            builder.Append("Status=@Status");
            builder.Append(" where BindId=@BindId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@TokenAccess", SqlDbType.NVarChar, 200), new SqlParameter("@TokenExpireTime", SqlDbType.DateTime), new SqlParameter("@TokenRefresh", SqlDbType.NVarChar, 200), new SqlParameter("@MediaUserID", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@MediaNickName", SqlDbType.NVarChar, 200), new SqlParameter("@MediaID", SqlDbType.Int, 4), new SqlParameter("@iHome", SqlDbType.Bit, 1), new SqlParameter("@Comment", SqlDbType.Bit, 1), new SqlParameter("@GroupTopic", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@BindId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.TokenAccess;
            cmdParms[2].Value = model.TokenExpireTime;
            cmdParms[3].Value = model.TokenRefresh;
            cmdParms[4].Value = model.MediaUserID;
            cmdParms[5].Value = model.MediaNickName;
            cmdParms[6].Value = model.MediaID;
            cmdParms[7].Value = model.iHome;
            cmdParms[8].Value = model.Comment;
            cmdParms[9].Value = model.GroupTopic;
            cmdParms[10].Value = model.Status;
            cmdParms[11].Value = model.BindId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateEx(Maticsoft.Model.Members.UserBind model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UserBind set ");
            builder.Append("TokenAccess=@TokenAccess,");
            builder.Append("TokenExpireTime=@TokenExpireTime,");
            builder.Append("TokenRefresh=@TokenRefresh,");
            builder.Append("MediaNickName=@MediaNickName,");
            builder.Append("MediaID=@MediaID,");
            builder.Append("iHome=@iHome,");
            builder.Append("Comment=@Comment,");
            builder.Append("GroupTopic=@GroupTopic,");
            builder.Append("Status=@Status");
            builder.Append(" where UserId=@UserId and MediaUserID=@MediaUserID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@TokenAccess", SqlDbType.NVarChar, 200), new SqlParameter("@TokenExpireTime", SqlDbType.DateTime), new SqlParameter("@TokenRefresh", SqlDbType.NVarChar, 200), new SqlParameter("@MediaUserID", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@MediaNickName", SqlDbType.NVarChar, 200), new SqlParameter("@MediaID", SqlDbType.Int, 4), new SqlParameter("@iHome", SqlDbType.Bit, 1), new SqlParameter("@Comment", SqlDbType.Bit, 1), new SqlParameter("@GroupTopic", SqlDbType.Bit, 1), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@BindId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.TokenAccess;
            cmdParms[2].Value = model.TokenExpireTime;
            cmdParms[3].Value = model.TokenRefresh;
            cmdParms[4].Value = model.MediaUserID;
            cmdParms[5].Value = model.MediaNickName;
            cmdParms[6].Value = model.MediaID;
            cmdParms[7].Value = model.iHome;
            cmdParms[8].Value = model.Comment;
            cmdParms[9].Value = model.GroupTopic;
            cmdParms[10].Value = model.Status;
            cmdParms[11].Value = model.BindId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


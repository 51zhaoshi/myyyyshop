namespace Maticsoft.SQLServerDAL.Members
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Members;
    using Maticsoft.Model.Members;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserInvite : IUserInvite
    {
        public int Add(Maticsoft.Model.Members.UserInvite model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Accounts_UserInvite(");
            builder.Append("UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc)");
            builder.Append(" values (");
            builder.Append("@UserId,@UserNick,@InviteUserId,@InviteNick,@IsRebate,@IsNew,@CreatedDate,@Remark,@RebateDesc)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserNick", SqlDbType.NVarChar, 200), new SqlParameter("@InviteUserId", SqlDbType.Int, 4), new SqlParameter("@InviteNick", SqlDbType.NVarChar, 200), new SqlParameter("@IsRebate", SqlDbType.Bit, 1), new SqlParameter("@IsNew", SqlDbType.Bit, 1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, -1), new SqlParameter("@RebateDesc", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.UserNick;
            cmdParms[2].Value = model.InviteUserId;
            cmdParms[3].Value = model.InviteNick;
            cmdParms[4].Value = model.IsRebate;
            cmdParms[5].Value = model.IsNew;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Remark;
            cmdParms[8].Value = model.RebateDesc;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Members.UserInvite DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Members.UserInvite invite = new Maticsoft.Model.Members.UserInvite();
            if (row != null)
            {
                if ((row["InviteId"] != null) && (row["InviteId"].ToString() != ""))
                {
                    invite.InviteId = int.Parse(row["InviteId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    invite.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["UserNick"] != null)
                {
                    invite.UserNick = row["UserNick"].ToString();
                }
                if ((row["InviteUserId"] != null) && (row["InviteUserId"].ToString() != ""))
                {
                    invite.InviteUserId = int.Parse(row["InviteUserId"].ToString());
                }
                if (row["InviteNick"] != null)
                {
                    invite.InviteNick = row["InviteNick"].ToString();
                }
                if ((row["IsRebate"] != null) && (row["IsRebate"].ToString() != ""))
                {
                    if ((row["IsRebate"].ToString() == "1") || (row["IsRebate"].ToString().ToLower() == "true"))
                    {
                        invite.IsRebate = true;
                    }
                    else
                    {
                        invite.IsRebate = false;
                    }
                }
                if ((row["IsNew"] != null) && (row["IsNew"].ToString() != ""))
                {
                    if ((row["IsNew"].ToString() == "1") || (row["IsNew"].ToString().ToLower() == "true"))
                    {
                        invite.IsNew = true;
                    }
                    else
                    {
                        invite.IsNew = false;
                    }
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    invite.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["Remark"] != null)
                {
                    invite.Remark = row["Remark"].ToString();
                }
                if (row["RebateDesc"] != null)
                {
                    invite.RebateDesc = row["RebateDesc"].ToString();
                }
            }
            return invite;
        }

        public bool Delete(int InviteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_UserInvite ");
            builder.Append(" where InviteId=@InviteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@InviteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = InviteId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string InviteIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Accounts_UserInvite ");
            builder.Append(" where InviteId in (" + InviteIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int InviteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Accounts_UserInvite");
            builder.Append(" where InviteId=@InviteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@InviteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = InviteId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select InviteId,UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc ");
            builder.Append(" FROM Accounts_UserInvite ");
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
            builder.Append(" InviteId,UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc ");
            builder.Append(" FROM Accounts_UserInvite ");
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
                builder.Append("order by T.InviteId desc");
            }
            builder.Append(")AS Row, T.*  from Accounts_UserInvite T ");
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
            return DbHelperSQL.GetMaxID("InviteId", "Accounts_UserInvite");
        }

        public Maticsoft.Model.Members.UserInvite GetModel(int InviteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 InviteId,UserId,UserNick,InviteUserId,InviteNick,IsRebate,IsNew,CreatedDate,Remark,RebateDesc from Accounts_UserInvite ");
            builder.Append(" where InviteId=@InviteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@InviteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = InviteId;
            new Maticsoft.Model.Members.UserInvite();
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
            builder.Append("select count(1) FROM Accounts_UserInvite ");
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

        public bool Update(Maticsoft.Model.Members.UserInvite model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Accounts_UserInvite set ");
            builder.Append("UserId=@UserId,");
            builder.Append("UserNick=@UserNick,");
            builder.Append("InviteUserId=@InviteUserId,");
            builder.Append("InviteNick=@InviteNick,");
            builder.Append("IsRebate=@IsRebate,");
            builder.Append("IsNew=@IsNew,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Remark=@Remark,");
            builder.Append("RebateDesc=@RebateDesc");
            builder.Append(" where InviteId=@InviteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@UserNick", SqlDbType.NVarChar, 200), new SqlParameter("@InviteUserId", SqlDbType.Int, 4), new SqlParameter("@InviteNick", SqlDbType.NVarChar, 200), new SqlParameter("@IsRebate", SqlDbType.Bit, 1), new SqlParameter("@IsNew", SqlDbType.Bit, 1), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, -1), new SqlParameter("@RebateDesc", SqlDbType.NVarChar, 200), new SqlParameter("@InviteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.UserId;
            cmdParms[1].Value = model.UserNick;
            cmdParms[2].Value = model.InviteUserId;
            cmdParms[3].Value = model.InviteNick;
            cmdParms[4].Value = model.IsRebate;
            cmdParms[5].Value = model.IsNew;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.Remark;
            cmdParms[8].Value = model.RebateDesc;
            cmdParms[9].Value = model.InviteId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class GroupUsers : IGroupUsers
    {
        public bool Add(Maticsoft.Model.SNS.GroupUsers model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupUsers(");
            builder.Append("GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status)");
            builder.Append(" values (");
            builder.Append("@GroupID,@UserID,@NickName,@JoinTime,@Role,@ApplyReason,@IsRecommend,@Sequence,@Status)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@JoinTime", SqlDbType.DateTime), new SqlParameter("@Role", SqlDbType.Int, 4), new SqlParameter("@ApplyReason", SqlDbType.NVarChar), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.GroupID;
            cmdParms[1].Value = model.UserID;
            cmdParms[2].Value = model.NickName;
            cmdParms[3].Value = model.JoinTime;
            cmdParms[4].Value = model.Role;
            cmdParms[5].Value = model.ApplyReason;
            cmdParms[6].Value = model.IsRecommend;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.Status;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool AddEx(Maticsoft.Model.SNS.GroupUsers model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_GroupUsers(");
            builder.Append("GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status)");
            builder.Append(" values (");
            builder.Append("@GroupID,@UserID,@NickName,@JoinTime,@Role,@ApplyReason,@IsRecommend,@Sequence,@Status)");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@JoinTime", SqlDbType.DateTime), new SqlParameter("@Role", SqlDbType.Int, 4), new SqlParameter("@ApplyReason", SqlDbType.NVarChar), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4) };
            para[0].Value = model.GroupID;
            para[1].Value = model.UserID;
            para[2].Value = model.NickName;
            para[3].Value = model.JoinTime;
            para[4].Value = model.Role;
            para[5].Value = model.ApplyReason;
            para[6].Value = model.IsRecommend;
            para[7].Value = model.Sequence;
            para[8].Value = model.Status;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update  SNS_Groups ");
            builder2.Append(" Set GroupUserCount=GroupUserCount+1 ");
            builder2.Append(" where GroupID=@GroupID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.GroupID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public Maticsoft.Model.SNS.GroupUsers DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.GroupUsers users = new Maticsoft.Model.SNS.GroupUsers();
            if (row != null)
            {
                if ((row["GroupID"] != null) && (row["GroupID"].ToString() != ""))
                {
                    users.GroupID = int.Parse(row["GroupID"].ToString());
                }
                if ((row["UserID"] != null) && (row["UserID"].ToString() != ""))
                {
                    users.UserID = int.Parse(row["UserID"].ToString());
                }
                if (row["NickName"] != null)
                {
                    users.NickName = row["NickName"].ToString();
                }
                if ((row["JoinTime"] != null) && (row["JoinTime"].ToString() != ""))
                {
                    users.JoinTime = DateTime.Parse(row["JoinTime"].ToString());
                }
                if ((row["Role"] != null) && (row["Role"].ToString() != ""))
                {
                    users.Role = int.Parse(row["Role"].ToString());
                }
                if (row["ApplyReason"] != null)
                {
                    users.ApplyReason = row["ApplyReason"].ToString();
                }
                if ((row["IsRecommend"] != null) && (row["IsRecommend"].ToString() != ""))
                {
                    if ((row["IsRecommend"].ToString() == "1") || (row["IsRecommend"].ToString().ToLower() == "true"))
                    {
                        users.IsRecommend = true;
                    }
                    else
                    {
                        users.IsRecommend = false;
                    }
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    users.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    users.Status = int.Parse(row["Status"].ToString());
                }
            }
            return users;
        }

        public bool Delete(int GroupID, int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupUsers ");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupID;
            cmdParms[1].Value = UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int GroupID, int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupUsers ");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            para[0].Value = GroupID;
            para[1].Value = UserID;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update  SNS_Groups ");
            builder2.Append(" Set GroupUserCount=GroupUserCount-1 ");
            builder2.Append(" where GroupID=@GroupID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = GroupID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteEx(int GroupID, string UserIDs)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupUsers ");
            builder.Append(string.Concat(new object[] { " where GroupID=", GroupID, " and UserID in (", UserIDs, ") " }));
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), null);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update  SNS_Groups ");
            builder2.Append(" Set GroupUserCount=GroupUserCount-1 ");
            builder2.Append(" where GroupID=@GroupID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            para[0].Value = GroupID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), para);
            cmdList.Add(info2);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Exists(int GroupID, int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_GroupUsers");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupID;
            cmdParms[1].Value = UserID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status ");
            builder.Append(" FROM SNS_GroupUsers ");
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
            builder.Append(" GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status ");
            builder.Append(" FROM SNS_GroupUsers ");
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
                builder.Append("order by T.UserID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_GroupUsers T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.GroupUsers GetModel(int GroupID, int UserID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 GroupID,UserID,NickName,JoinTime,Role,ApplyReason,IsRecommend,Sequence,Status from SNS_GroupUsers ");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupID;
            cmdParms[1].Value = UserID;
            new Maticsoft.Model.SNS.GroupUsers();
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
            builder.Append("select count(1) FROM SNS_GroupUsers ");
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

        public bool Update(Maticsoft.Model.SNS.GroupUsers model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupUsers set ");
            builder.Append("NickName=@NickName,");
            builder.Append("JoinTime=@JoinTime,");
            builder.Append("Role=@Role,");
            builder.Append("ApplyReason=@ApplyReason,");
            builder.Append("IsRecommend=@IsRecommend,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Status=@Status");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@JoinTime", SqlDbType.DateTime), new SqlParameter("@Role", SqlDbType.Int, 4), new SqlParameter("@ApplyReason", SqlDbType.NVarChar), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.NickName;
            cmdParms[1].Value = model.JoinTime;
            cmdParms[2].Value = model.Role;
            cmdParms[3].Value = model.ApplyReason;
            cmdParms[4].Value = model.IsRecommend;
            cmdParms[5].Value = model.Sequence;
            cmdParms[6].Value = model.Status;
            cmdParms[7].Value = model.GroupID;
            cmdParms[8].Value = model.UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecommand(int GroupID, int UserID, int Recommand)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupUsers set ");
            builder.Append("IsRecommend=@IsRecommend");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Recommand == 1;
            cmdParms[1].Value = GroupID;
            cmdParms[2].Value = UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRole(int GroupID, int UserID, int Role)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupUsers set ");
            builder.Append("Role=@Role");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Role", SqlDbType.Bit, 1), new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Role;
            cmdParms[1].Value = GroupID;
            cmdParms[2].Value = UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(int GroupID, int UserID, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_GroupUsers set ");
            builder.Append(" Status=@Status");
            builder.Append(" where GroupID=@GroupID and UserID=@UserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@GroupID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Status;
            cmdParms[1].Value = GroupID;
            cmdParms[2].Value = UserID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatusByTopicIds(string Ids, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE SNS_GroupUsers SET  Status =" + Status + " FROM  SNS_GroupUsers");
            builder.Append(" INNER JOIN SNS_GroupTopics ON SNS_GroupUsers.GroupID = SNS_GroupTopics.GroupID AND SNS_GroupTopics.CreatedUserID=SNS_GroupUsers.UserID WHERE (SNS_GroupTopics.TopicID IN(" + Ids + "))");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateStatusByTopicReplyIds(string Ids, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" UPDATE SNS_GroupUsers SET  Status =" + Status + " FROM  SNS_GroupUsers");
            builder.Append(" INNER JOIN SNS_GroupTopicReply ON SNS_GroupUsers.GroupID = SNS_GroupTopicReply.GroupID AND SNS_GroupTopicReply.ReplyUserID=SNS_GroupUsers.UserID WHERE (SNS_GroupTopicReply.ReplyID IN(" + Ids + "))");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}


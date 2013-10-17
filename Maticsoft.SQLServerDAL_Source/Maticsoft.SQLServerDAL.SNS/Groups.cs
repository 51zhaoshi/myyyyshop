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

    public class Groups : IGroups
    {
        public int Add(Maticsoft.Model.SNS.Groups model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Groups(");
            builder.Append("GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags)");
            builder.Append(" values (");
            builder.Append("@GroupName,@GroupDescription,@GroupUserCount,@CreatedUserId,@CreatedNickName,@CreatedDate,@GroupLogo,@GroupLogoThumb,@GroupBackground,@ApplyGroupReason,@IsRecommand,@TopicCount,@TopicReplyCount,@Status,@Sequence,@Privacy,@Tags)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@GroupName", SqlDbType.NVarChar, 50), new SqlParameter("@GroupDescription", SqlDbType.NVarChar), new SqlParameter("@GroupUserCount", SqlDbType.Int, 4), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@GroupLogo", SqlDbType.NVarChar, 200), new SqlParameter("@GroupLogoThumb", SqlDbType.NVarChar, 200), new SqlParameter("@GroupBackground", SqlDbType.NVarChar, 200), new SqlParameter("@ApplyGroupReason", SqlDbType.NVarChar), new SqlParameter("@IsRecommand", SqlDbType.Int, 4), new SqlParameter("@TopicCount", SqlDbType.Int, 4), new SqlParameter("@TopicReplyCount", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), 
                new SqlParameter("@Tags", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.GroupName;
            cmdParms[1].Value = model.GroupDescription;
            cmdParms[2].Value = model.GroupUserCount;
            cmdParms[3].Value = model.CreatedUserId;
            cmdParms[4].Value = model.CreatedNickName;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.GroupLogo;
            cmdParms[7].Value = model.GroupLogoThumb;
            cmdParms[8].Value = model.GroupBackground;
            cmdParms[9].Value = model.ApplyGroupReason;
            cmdParms[10].Value = model.IsRecommand;
            cmdParms[11].Value = model.TopicCount;
            cmdParms[12].Value = model.TopicReplyCount;
            cmdParms[13].Value = model.Status;
            cmdParms[14].Value = model.Sequence;
            cmdParms[15].Value = model.Privacy;
            cmdParms[0x10].Value = model.Tags;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.Groups DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Groups groups = new Maticsoft.Model.SNS.Groups();
            if (row != null)
            {
                if ((row["GroupID"] != null) && (row["GroupID"].ToString() != ""))
                {
                    groups.GroupID = int.Parse(row["GroupID"].ToString());
                }
                if (row["GroupName"] != null)
                {
                    groups.GroupName = row["GroupName"].ToString();
                }
                if (row["GroupDescription"] != null)
                {
                    groups.GroupDescription = row["GroupDescription"].ToString();
                }
                if ((row["GroupUserCount"] != null) && (row["GroupUserCount"].ToString() != ""))
                {
                    groups.GroupUserCount = int.Parse(row["GroupUserCount"].ToString());
                }
                if ((row["CreatedUserId"] != null) && (row["CreatedUserId"].ToString() != ""))
                {
                    groups.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    groups.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    groups.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["GroupLogo"] != null)
                {
                    groups.GroupLogo = row["GroupLogo"].ToString();
                }
                if (row["GroupLogoThumb"] != null)
                {
                    groups.GroupLogoThumb = row["GroupLogoThumb"].ToString();
                }
                if (row["GroupBackground"] != null)
                {
                    groups.GroupBackground = row["GroupBackground"].ToString();
                }
                if (row["ApplyGroupReason"] != null)
                {
                    groups.ApplyGroupReason = row["ApplyGroupReason"].ToString();
                }
                if ((row["IsRecommand"] != null) && (row["IsRecommand"].ToString() != ""))
                {
                    groups.IsRecommand = int.Parse(row["IsRecommand"].ToString());
                }
                if ((row["TopicCount"] != null) && (row["TopicCount"].ToString() != ""))
                {
                    groups.TopicCount = int.Parse(row["TopicCount"].ToString());
                }
                if ((row["TopicReplyCount"] != null) && (row["TopicReplyCount"].ToString() != ""))
                {
                    groups.TopicReplyCount = int.Parse(row["TopicReplyCount"].ToString());
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    groups.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    groups.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["Privacy"] != null) && (row["Privacy"].ToString() != ""))
                {
                    groups.Privacy = int.Parse(row["Privacy"].ToString());
                }
                if (row["Tags"] != null)
                {
                    groups.Tags = row["Tags"].ToString();
                }
            }
            return groups;
        }

        public bool Delete(int GroupID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Groups ");
            builder.Append(" where GroupID=@GroupID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string GroupIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Groups ");
            builder.Append(" where GroupID in (" + GroupIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool DeleteListEx(string GroupIDlist)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_GroupTopicFav ");
            builder.Append(" where TopicID in (select TopicID from SNS_GroupTopics where GroupID in (" + GroupIDlist + "))  ");
            CommandInfo item = new CommandInfo(builder.ToString(), null);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from SNS_GroupTopicReply ");
            builder2.Append(" where GroupID in (" + GroupIDlist + ")  ");
            CommandInfo info2 = new CommandInfo(builder2.ToString(), null);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from SNS_GroupTopics ");
            builder3.Append(" where GroupID in (" + GroupIDlist + ")  ");
            CommandInfo info3 = new CommandInfo(builder3.ToString(), null);
            cmdList.Add(info3);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete from SNS_GroupUsers ");
            builder4.Append(" where GroupID in (" + GroupIDlist + ")  ");
            CommandInfo info4 = new CommandInfo(builder4.ToString(), null);
            cmdList.Add(info4);
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete from SNS_Groups ");
            builder5.Append(" where GroupID in (" + GroupIDlist + ")  ");
            CommandInfo info5 = new CommandInfo(builder5.ToString(), null);
            cmdList.Add(info5);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Exists(string GroupName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Groups");
            builder.Append(" where GroupName=@GroupName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupName", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = GroupName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(string GroupName, int groupId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Groups");
            builder.Append(" where GroupName=@GroupName and groupId <>@GroupId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupName", SqlDbType.NVarChar, 50), new SqlParameter("@GroupId", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupName;
            cmdParms[1].Value = groupId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select GroupID,GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags ");
            builder.Append(" FROM SNS_Groups ");
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
            builder.Append(" GroupID,GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags ");
            builder.Append(" FROM SNS_Groups ");
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
                builder.Append("order by T.GroupID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Groups T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Groups GetModel(int GroupID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 GroupID,GroupName,GroupDescription,GroupUserCount,CreatedUserId,CreatedNickName,CreatedDate,GroupLogo,GroupLogoThumb,GroupBackground,ApplyGroupReason,IsRecommand,TopicCount,TopicReplyCount,Status,Sequence,Privacy,Tags from SNS_Groups ");
            builder.Append(" where GroupID=@GroupID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupID;
            new Maticsoft.Model.SNS.Groups();
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
            builder.Append("select count(1) FROM SNS_Groups ");
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

        public bool Update(Maticsoft.Model.SNS.Groups model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set ");
            builder.Append("GroupName=@GroupName,");
            builder.Append("GroupDescription=@GroupDescription,");
            builder.Append("GroupUserCount=@GroupUserCount,");
            builder.Append("CreatedUserId=@CreatedUserId,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("GroupLogo=@GroupLogo,");
            builder.Append("GroupLogoThumb=@GroupLogoThumb,");
            builder.Append("GroupBackground=@GroupBackground,");
            builder.Append("ApplyGroupReason=@ApplyGroupReason,");
            builder.Append("IsRecommand=@IsRecommand,");
            builder.Append("TopicCount=@TopicCount,");
            builder.Append("TopicReplyCount=@TopicReplyCount,");
            builder.Append("Status=@Status,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Privacy=@Privacy,");
            builder.Append("Tags=@Tags");
            builder.Append(" where GroupID=@GroupID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@GroupName", SqlDbType.NVarChar, 50), new SqlParameter("@GroupDescription", SqlDbType.NVarChar), new SqlParameter("@GroupUserCount", SqlDbType.Int, 4), new SqlParameter("@CreatedUserId", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@GroupLogo", SqlDbType.NVarChar, 200), new SqlParameter("@GroupLogoThumb", SqlDbType.NVarChar, 200), new SqlParameter("@GroupBackground", SqlDbType.NVarChar, 200), new SqlParameter("@ApplyGroupReason", SqlDbType.NVarChar), new SqlParameter("@IsRecommand", SqlDbType.Int, 4), new SqlParameter("@TopicCount", SqlDbType.Int, 4), new SqlParameter("@TopicReplyCount", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), 
                new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@GroupID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.GroupName;
            cmdParms[1].Value = model.GroupDescription;
            cmdParms[2].Value = model.GroupUserCount;
            cmdParms[3].Value = model.CreatedUserId;
            cmdParms[4].Value = model.CreatedNickName;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.GroupLogo;
            cmdParms[7].Value = model.GroupLogoThumb;
            cmdParms[8].Value = model.GroupBackground;
            cmdParms[9].Value = model.ApplyGroupReason;
            cmdParms[10].Value = model.IsRecommand;
            cmdParms[11].Value = model.TopicCount;
            cmdParms[12].Value = model.TopicReplyCount;
            cmdParms[13].Value = model.Status;
            cmdParms[14].Value = model.Sequence;
            cmdParms[15].Value = model.Privacy;
            cmdParms[0x10].Value = model.Tags;
            cmdParms[0x11].Value = model.GroupID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecommand(int GroupId, int Recommand)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set ");
            builder.Append("IsRecommand=@IsRecommand");
            builder.Append(" where GroupID=@GroupID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsRecommand", SqlDbType.Int, 4), new SqlParameter("@GroupID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Recommand;
            cmdParms[1].Value = GroupId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatusList(string IdsStr, EnumHelper.GroupStatus status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Groups set Status=" + ((int) status) + " ");
            builder.Append(" where GroupID in (" + IdsStr + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}


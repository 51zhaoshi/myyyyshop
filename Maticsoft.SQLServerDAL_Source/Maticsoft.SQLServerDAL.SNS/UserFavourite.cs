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

    public class UserFavourite : IUserFavourite
    {
        public int Add(Maticsoft.Model.SNS.UserFavourite model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserFavourite(");
            builder.Append("TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@TargetID,@Type,@CreatedUserID,@CreatedNickName,@OwnerUserID,@OwnerNickName,@Description,@Tags,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@OwnerUserID", SqlDbType.Int, 4), new SqlParameter("@OwnerNickName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.TargetID;
            cmdParms[1].Value = model.Type;
            cmdParms[2].Value = model.CreatedUserID;
            cmdParms[3].Value = model.CreatedNickName;
            cmdParms[4].Value = model.OwnerUserID;
            cmdParms[5].Value = model.OwnerNickName;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.Tags;
            cmdParms[8].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool AddEx(Maticsoft.Model.SNS.UserFavourite model, int TopicId, int ReplyId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserFavourite(");
            builder.Append("TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@TargetID,@Type,@CreatedUserID,@CreatedNickName,@OwnerUserID,@OwnerNickName,@Description,@Tags,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@OwnerUserID", SqlDbType.Int, 4), new SqlParameter("@OwnerNickName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            para[0].Value = model.TargetID;
            para[1].Value = model.Type;
            para[2].Value = model.CreatedUserID;
            para[3].Value = model.CreatedNickName;
            para[4].Value = model.OwnerUserID;
            para[5].Value = model.OwnerNickName;
            para[6].Value = model.Description;
            para[7].Value = model.Tags;
            para[8].Value = model.CreatedDate;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            if (model.Type == 0)
            {
                builder2.Append("Update  SNS_Photos ");
            }
            else
            {
                builder2.Append("Update  SNS_Products ");
            }
            builder2.Append(" Set FavouriteCount=FavouriteCount+1 ");
            if (model.Type == 0)
            {
                builder2.Append(" where PhotoID=@TargetId");
            }
            else
            {
                builder2.Append(" where ProductID=@TargetId");
            }
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = model.TargetID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("update Accounts_UsersExp Set FavouritesCount=FavouritesCount+1 ");
            builder3.Append(" where UserID=@UserID");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray3[0].Value = model.CreatedUserID;
            CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(info3);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("update Accounts_UsersExp Set FavoritedCount=FavoritedCount+1 ");
            builder4.Append(" where UserID=@UserID");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray4[0].Value = model.OwnerUserID;
            CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(info4);
            if (TopicId > 0)
            {
                StringBuilder builder5 = new StringBuilder();
                builder5.Append("update SNS_GroupTopics Set FavCount=FavCount+1 ");
                builder5.Append(" where TopicID=@TopicID");
                SqlParameter[] parameterArray5 = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4) };
                parameterArray5[0].Value = TopicId;
                CommandInfo info5 = new CommandInfo(builder5.ToString(), parameterArray5);
                cmdList.Add(info5);
            }
            else if (ReplyId > 0)
            {
                StringBuilder builder6 = new StringBuilder();
                builder6.Append("update SNS_GroupTopicReply Set FavCount=FavCount+1 ");
                builder6.Append(" where ReplyID=@ReplyID");
                SqlParameter[] parameterArray6 = new SqlParameter[] { new SqlParameter("@ReplyID", SqlDbType.Int, 4) };
                parameterArray6[0].Value = ReplyId;
                CommandInfo info6 = new CommandInfo(builder6.ToString(), parameterArray6);
                cmdList.Add(info6);
            }
            else
            {
                StringBuilder builder7 = new StringBuilder();
                builder7.Append("update SNS_Posts Set FavCount=FavCount+1 ");
                builder7.Append(" where TargetId=@TargetId and Type=@Type");
                SqlParameter[] parameterArray7 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
                parameterArray7[0].Value = model.TargetID;
                parameterArray7[1].Value = model.Type + 1;
                CommandInfo info7 = new CommandInfo(builder7.ToString(), parameterArray7);
                cmdList.Add(info7);
            }
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public Maticsoft.Model.SNS.UserFavourite DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserFavourite favourite = new Maticsoft.Model.SNS.UserFavourite();
            if (row != null)
            {
                if ((row["FavouriteID"] != null) && (row["FavouriteID"].ToString() != ""))
                {
                    favourite.FavouriteID = int.Parse(row["FavouriteID"].ToString());
                }
                if ((row["TargetID"] != null) && (row["TargetID"].ToString() != ""))
                {
                    favourite.TargetID = int.Parse(row["TargetID"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    favourite.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    favourite.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    favourite.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["OwnerUserID"] != null) && (row["OwnerUserID"].ToString() != ""))
                {
                    favourite.OwnerUserID = new int?(int.Parse(row["OwnerUserID"].ToString()));
                }
                if (row["OwnerNickName"] != null)
                {
                    favourite.OwnerNickName = row["OwnerNickName"].ToString();
                }
                if (row["Description"] != null)
                {
                    favourite.Description = row["Description"].ToString();
                }
                if (row["Tags"] != null)
                {
                    favourite.Tags = row["Tags"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    favourite.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return favourite;
        }

        public bool Delete(int FavouriteID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserFavourite ");
            builder.Append(" where FavouriteID=@FavouriteID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FavouriteID", SqlDbType.Int, 4) };
            cmdParms[0].Value = FavouriteID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEx(int UserId, int TargetId, int Type)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserFavourite ");
            builder.Append(" where TargetID=@TargetID and Type=@Type");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            para[0].Value = TargetId;
            para[1].Value = Type;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            if (Type == 1)
            {
                builder2.Append("update SNS_Products set FavouriteCount=FavouriteCount-1");
                builder2.Append(" where ProductID=@TargetID ");
            }
            else
            {
                builder2.Append("update SNS_Photos set FavouriteCount=FavouriteCount-1");
                builder2.Append(" where PhotoID=@TargetID  ");
            }
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = TargetId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("update Accounts_UsersExp set FavouritesCount=FavouritesCount-1");
            builder3.Append(" where UserID=@UserID  ");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray3[0].Value = UserId;
            CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(info3);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("update SNS_Posts Set FavCount=FavCount-1 ");
            builder4.Append(" where TargetId=@TargetId and Type=@Type");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            parameterArray4[0].Value = TargetId;
            parameterArray4[1].Value = Type + 1;
            CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(info4);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string FavouriteIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserFavourite ");
            builder.Append(" where FavouriteID in (" + FavouriteIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CreatedUserID, int Type, int TargetID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserFavourite");
            builder.Append(" where CreatedUserID=@CreatedUserID and Type=@Type and TargetID=@TargetID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CreatedUserID;
            cmdParms[1].Value = Type;
            cmdParms[2].Value = TargetID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetFavListByPage(int UserId, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.FavouriteID desc");
            }
            builder.Append(")AS Row, T.*  from ");
            builder.Append(" (select uad.FavouriteID,p.ProductID TargetID,p.ProductName TargetName,p.ShareDescription Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,p.Price,1 Type");
            builder.AppendFormat(" from (select FavouriteID,TargetID from SNS_UserFavourite where CreatedUserID={0} and Type=1) uad ", UserId);
            builder.Append(" inner join SNS_Products p on uad.TargetID=p.ProductID  ");
            builder.Append(" union");
            builder.Append(" select uad.FavouriteID,p.PhotoID TargetID,p.PhotoName TargetName,p.Description Description,p.TopCommentsId TopCommentsId,p.CommentCount,p.FavouriteCount,p.ThumbImageUrl,0 price,0 Type ");
            builder.AppendFormat(" from (select FavouriteID,TargetID from SNS_UserFavourite where CreatedUserID={0} and Type=0) uad ", UserId);
            builder.Append(" inner join SNS_Photos p on uad.TargetID=p.PhotoID) T ");
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FavouriteID,TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate ");
            builder.Append(" FROM SNS_UserFavourite ");
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
            builder.Append(" FavouriteID,TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate ");
            builder.Append(" FROM SNS_UserFavourite ");
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
                builder.Append("order by T.FavouriteID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserFavourite T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.UserFavourite GetModel(int FavouriteID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FavouriteID,TargetID,Type,CreatedUserID,CreatedNickName,OwnerUserID,OwnerNickName,Description,Tags,CreatedDate from SNS_UserFavourite ");
            builder.Append(" where FavouriteID=@FavouriteID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FavouriteID", SqlDbType.Int, 4) };
            cmdParms[0].Value = FavouriteID;
            new Maticsoft.Model.SNS.UserFavourite();
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
            builder.Append("select count(1) FROM SNS_UserFavourite ");
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

        public bool Update(Maticsoft.Model.SNS.UserFavourite model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserFavourite set ");
            builder.Append("TargetID=@TargetID,");
            builder.Append("Type=@Type,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("OwnerUserID=@OwnerUserID,");
            builder.Append("OwnerNickName=@OwnerNickName,");
            builder.Append("Description=@Description,");
            builder.Append("Tags=@Tags,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where FavouriteID=@FavouriteID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@OwnerUserID", SqlDbType.Int, 4), new SqlParameter("@OwnerNickName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@FavouriteID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.TargetID;
            cmdParms[1].Value = model.Type;
            cmdParms[2].Value = model.CreatedUserID;
            cmdParms[3].Value = model.CreatedNickName;
            cmdParms[4].Value = model.OwnerUserID;
            cmdParms[5].Value = model.OwnerNickName;
            cmdParms[6].Value = model.Description;
            cmdParms[7].Value = model.Tags;
            cmdParms[8].Value = model.CreatedDate;
            cmdParms[9].Value = model.FavouriteID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


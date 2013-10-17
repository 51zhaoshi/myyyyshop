namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class UserAlbums : IUserAlbums
    {
        public int Add(Maticsoft.Model.SNS.UserAlbums model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserAlbums(");
            builder.Append("AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags)");
            builder.Append(" values (");
            builder.Append("@AlbumName,@Description,@CoverTargetID,@CoverPhotoUrl,@CoverTargetType,@Status,@CreatedUserID,@CreatedNickName,@PhotoCount,@PVCount,@FavouriteCount,@CreatedDate,@CommentsCount,@IsRecommend,@ChannelSequence,@Privacy,@Sequence,@LastUpdatedDate,@Tags)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@AlbumName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CoverTargetID", SqlDbType.Int, 4), new SqlParameter("@CoverPhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@CoverTargetType", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@PhotoCount", SqlDbType.Int, 4), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CommentsCount", SqlDbType.Int, 4), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@ChannelSequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.Int, 4), 
                new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.AlbumName;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.CoverTargetID;
            cmdParms[3].Value = model.CoverPhotoUrl;
            cmdParms[4].Value = model.CoverTargetType;
            cmdParms[5].Value = model.Status;
            cmdParms[6].Value = model.CreatedUserID;
            cmdParms[7].Value = model.CreatedNickName;
            cmdParms[8].Value = model.PhotoCount;
            cmdParms[9].Value = model.PVCount;
            cmdParms[10].Value = model.FavouriteCount;
            cmdParms[11].Value = model.CreatedDate;
            cmdParms[12].Value = model.CommentsCount;
            cmdParms[13].Value = model.IsRecommend;
            cmdParms[14].Value = model.ChannelSequence;
            cmdParms[15].Value = model.Privacy;
            cmdParms[0x10].Value = model.Sequence;
            cmdParms[0x11].Value = model.LastUpdatedDate;
            cmdParms[0x12].Value = model.Tags;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int AddEx(Maticsoft.Model.SNS.UserAlbums model, int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_UserAlbums(");
            builder.Append("AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags)");
            builder.Append(" values (");
            builder.Append("@AlbumName,@Description,@CoverTargetID,@CoverPhotoUrl,@CoverTargetType,@Status,@CreatedUserID,@CreatedNickName,@PhotoCount,@PVCount,@FavouriteCount,@CreatedDate,@CommentsCount,@IsRecommend,@ChannelSequence,@Privacy,@Sequence,@LastUpdatedDate,@Tags)");
            builder.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@AlbumName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CoverTargetID", SqlDbType.Int, 4), new SqlParameter("@CoverPhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@CoverTargetType", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@PhotoCount", SqlDbType.Int, 4), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CommentsCount", SqlDbType.Int, 4), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@ChannelSequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.Int, 4), 
                new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@ReturnValue", SqlDbType.Int)
             };
            para[0].Value = model.AlbumName;
            para[1].Value = model.Description;
            para[2].Value = model.CoverTargetID;
            para[3].Value = model.CoverPhotoUrl;
            para[4].Value = model.CoverTargetType;
            para[5].Value = model.Status;
            para[6].Value = model.CreatedUserID;
            para[7].Value = model.CreatedNickName;
            para[8].Value = model.PhotoCount;
            para[9].Value = model.PVCount;
            para[10].Value = model.FavouriteCount;
            para[11].Value = model.CreatedDate;
            para[12].Value = model.CommentsCount;
            para[13].Value = model.IsRecommend;
            para[14].Value = model.ChannelSequence;
            para[15].Value = model.Privacy;
            para[0x10].Value = model.Sequence;
            para[0x11].Value = model.LastUpdatedDate;
            para[0x12].Value = model.Tags;
            para[0x13].Direction = ParameterDirection.Output;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update  SNS_AlbumType set AlbumsCount=AlbumsCount+1 ");
            builder2.Append(" where ID=@TypeId");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            parameterArray2[0].Value = TypeId;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("Update  Accounts_UsersExp set AblumsCount=AblumsCount+1 ");
            builder3.Append(" where UserID=@UserID");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray3[0].Value = model.CreatedUserID;
            CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(info3);
            DbHelperSQL.ExecuteSqlTran(cmdList);
            return (int) para[0x13].Value;
        }

        public Maticsoft.Model.SNS.UserAlbums DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.UserAlbums albums = new Maticsoft.Model.SNS.UserAlbums();
            if (row != null)
            {
                if ((row["AlbumID"] != null) && (row["AlbumID"].ToString() != ""))
                {
                    albums.AlbumID = int.Parse(row["AlbumID"].ToString());
                }
                if ((row["AlbumName"] != null) && (row["AlbumName"].ToString() != ""))
                {
                    albums.AlbumName = row["AlbumName"].ToString();
                }
                if ((row["Description"] != null) && (row["Description"].ToString() != ""))
                {
                    albums.Description = row["Description"].ToString();
                }
                if ((row["CoverTargetID"] != null) && (row["CoverTargetID"].ToString() != ""))
                {
                    albums.CoverTargetID = new int?(int.Parse(row["CoverTargetID"].ToString()));
                }
                if ((row["CoverPhotoUrl"] != null) && (row["CoverPhotoUrl"].ToString() != ""))
                {
                    albums.CoverPhotoUrl = row["CoverPhotoUrl"].ToString();
                }
                if ((row["CoverTargetType"] != null) && (row["CoverTargetType"].ToString() != ""))
                {
                    albums.CoverTargetType = new int?(int.Parse(row["CoverTargetType"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    albums.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    albums.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if ((row["CreatedNickName"] != null) && (row["CreatedNickName"].ToString() != ""))
                {
                    albums.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["PhotoCount"] != null) && (row["PhotoCount"].ToString() != ""))
                {
                    albums.PhotoCount = int.Parse(row["PhotoCount"].ToString());
                }
                if ((row["PVCount"] != null) && (row["PVCount"].ToString() != ""))
                {
                    albums.PVCount = int.Parse(row["PVCount"].ToString());
                }
                if ((row["FavouriteCount"] != null) && (row["FavouriteCount"].ToString() != ""))
                {
                    albums.FavouriteCount = int.Parse(row["FavouriteCount"].ToString());
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    albums.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CommentsCount"] != null) && (row["CommentsCount"].ToString() != ""))
                {
                    albums.CommentsCount = new int?(int.Parse(row["CommentsCount"].ToString()));
                }
                if ((row["IsRecommend"] != null) && (row["IsRecommend"].ToString() != ""))
                {
                    albums.IsRecommend = int.Parse(row["IsRecommend"].ToString());
                }
                if ((row["ChannelSequence"] != null) && (row["ChannelSequence"].ToString() != ""))
                {
                    albums.ChannelSequence = int.Parse(row["ChannelSequence"].ToString());
                }
                if ((row["Privacy"] != null) && (row["Privacy"].ToString() != ""))
                {
                    albums.Privacy = int.Parse(row["Privacy"].ToString());
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    albums.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["LastUpdatedDate"] != null) && (row["LastUpdatedDate"].ToString() != ""))
                {
                    albums.LastUpdatedDate = new DateTime?(DateTime.Parse(row["LastUpdatedDate"].ToString()));
                }
                if ((row["Tags"] != null) && (row["Tags"].ToString() != ""))
                {
                    albums.Tags = row["Tags"].ToString();
                }
            }
            return albums;
        }

        public bool Delete(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbums ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteAblumAction(int albumId)
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameters[0].Value = albumId;
            return (DbHelperSQL.RunProcedure("sp_SNS_AblumsDeleteAction", parameters, out rowsAffected) > 0);
        }

        public bool DeleteEx(int AlbumID, int TypeId, int UserId)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbumsType ");
            builder.Append(" where AlbumsID=@AlbumsID");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@AlbumsID", SqlDbType.Int, 4) };
            para[0].Value = AlbumID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from SNS_UserAlbumDetail ");
            builder2.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = AlbumID;
            CommandInfo info2 = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(info2);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete from SNS_UserAlbums ");
            builder3.Append(" where AlbumID=@AlbumID");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            parameterArray3[0].Value = AlbumID;
            CommandInfo info3 = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(info3);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("Update  SNS_AlbumType set AlbumsCount=AlbumsCount-1 ");
            builder4.Append(" where ID=@TypeId");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            parameterArray4[0].Value = TypeId;
            CommandInfo info4 = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(info4);
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("Update  Accounts_UsersExp set AblumsCount=AblumsCount-1 ");
            builder5.Append(" where UserID=@UserID");
            SqlParameter[] parameterArray5 = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            parameterArray5[0].Value = UserId;
            CommandInfo info5 = new CommandInfo(builder5.ToString(), parameterArray5);
            cmdList.Add(info5);
            if (DbHelperSQL.ExecuteSqlTran(cmdList) <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteList(string AlbumIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_UserAlbums ");
            builder.Append(" where AlbumID in (" + AlbumIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int CreatedUserID, string AlbumName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_UserAlbums");
            builder.Append(" where CreatedUserID=@CreatedUserID and AlbumName=@AlbumName");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@AlbumName", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = CreatedUserID;
            cmdParms[1].Value = AlbumName;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AlbumID,AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags ");
            builder.Append(" FROM SNS_UserAlbums ");
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
            builder.Append(" AlbumID,AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags ");
            builder.Append(" FROM SNS_UserAlbums ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            else
            {
                builder.Append(" order by AlbumID desc");
            }
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
                builder.Append("order by T.AlbumID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserAlbums T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListForIndex(int TypeID, int Top, string orderby, int RecommandType = -1)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" * from SNS_UserAlbums ua ");
            if (TypeID > 0)
            {
                builder.Append(" inner join SNS_UserAlbumsType uat on ua.AlbumID=uat.AlbumsID ");
                builder.AppendFormat(" and uat.TypeID={0} ", TypeID);
            }
            if ((TypeID > 0) && (RecommandType > -1))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("  ua.IsRecommend={0} ", RecommandType);
            }
            else if ((TypeID <= 0) && (RecommandType > -1))
            {
                builder.AppendFormat("where  ua.IsRecommend={0} ", RecommandType);
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                builder.Append("order by ua." + orderby);
            }
            else
            {
                builder.Append("order by ua.AlbumID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListForIndexEx(int TypeID, int Top, string orderby)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" * from SNS_UserAlbums ua");
            if (TypeID > 0)
            {
                builder.Append(" inner join SNS_UserAlbumsType uat on ua.AlbumID=uat.AlbumsID ");
                builder.AppendFormat(" where uat.TypeID={0} and ua.PhotoCount>8", TypeID);
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                builder.Append("order by ua." + orderby);
            }
            else
            {
                builder.Append("order by ua.AlbumID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListForPage(int TypeID, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.AlbumID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserAlbums T ");
            if (TypeID > 0)
            {
                builder.Append(" inner join SNS_UserAlbumsType uat on T.AlbumID=uat.AlbumsID ");
                builder.AppendFormat(" and uat.TypeID={0} ", TypeID);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListForPageEx(int TypeID, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.AlbumID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_UserAlbums T ");
            builder.Append(" left join SNS_UserAlbumsType uat on T.AlbumID=uat.AlbumsID ");
            builder.Append(" where T.PhotoCount>8 and T.IsRecommend=" + 1);
            if (TypeID > 0)
            {
                builder.Append("  and uat.TypeID={0} ");
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.UserAlbums GetModel(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AlbumID,AlbumName,Description,CoverTargetID,CoverPhotoUrl,CoverTargetType,Status,CreatedUserID,CreatedNickName,PhotoCount,PVCount,FavouriteCount,CreatedDate,CommentsCount,IsRecommend,ChannelSequence,Privacy,Sequence,LastUpdatedDate,Tags from SNS_UserAlbums ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            new Maticsoft.Model.SNS.UserAlbums();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetRecordCount(int TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_UserAlbums ua");
            if (TypeID > 0)
            {
                builder.Append(" inner join SNS_UserAlbumsType uat on ua.AlbumID=uat.AlbumsID ");
                builder.AppendFormat(" and uat.TypeID={0} ", TypeID);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_UserAlbums ");
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

        public Maticsoft.Model.SNS.UserAlbums GetUserAlbum(int type, int pid, int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AlbumID from SNS_UserAlbumDetail ");
            builder.Append(" where AlbumUserId=@AlbumUserId and TargetID=@TargetID and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumUserId", SqlDbType.Int, 4), new SqlParameter("@TargetID", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = UserId;
            cmdParms[1].Value = pid;
            cmdParms[2].Value = type;
            new Maticsoft.Model.SNS.UserAlbums();
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return null;
            }
            return this.GetModel(Convert.ToInt32(single));
        }

        public DataSet GetUserFavAlbum(int UserId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ua.* FROM SNS_UserFavAlbum ufa RIGHT JOIN SNS_UserAlbums ua ON ufa.AlbumID=ua.AlbumID ");
            if (UserId > 0)
            {
                builder.Append("WHERE ufa.UserID=" + UserId);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Maticsoft.Model.SNS.UserAlbums model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.Append("AlbumName=@AlbumName,");
            builder.Append("Description=@Description,");
            builder.Append("CoverTargetID=@CoverTargetID,");
            builder.Append("CoverPhotoUrl=@CoverPhotoUrl,");
            builder.Append("CoverTargetType=@CoverTargetType,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("PhotoCount=@PhotoCount,");
            builder.Append("PVCount=@PVCount,");
            builder.Append("FavouriteCount=@FavouriteCount,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CommentsCount=@CommentsCount,");
            builder.Append("IsRecommend=@IsRecommend,");
            builder.Append("ChannelSequence=@ChannelSequence,");
            builder.Append("Privacy=@Privacy,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("LastUpdatedDate=@LastUpdatedDate,");
            builder.Append("Tags=@Tags");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@AlbumName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@CoverTargetID", SqlDbType.Int, 4), new SqlParameter("@CoverPhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@CoverTargetType", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@PhotoCount", SqlDbType.Int, 4), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CommentsCount", SqlDbType.Int, 4), new SqlParameter("@IsRecommend", SqlDbType.Bit, 1), new SqlParameter("@ChannelSequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.Int, 4), 
                new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@AlbumID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.AlbumName;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.CoverTargetID;
            cmdParms[3].Value = model.CoverPhotoUrl;
            cmdParms[4].Value = model.CoverTargetType;
            cmdParms[5].Value = model.Status;
            cmdParms[6].Value = model.CreatedUserID;
            cmdParms[7].Value = model.CreatedNickName;
            cmdParms[8].Value = model.PhotoCount;
            cmdParms[9].Value = model.PVCount;
            cmdParms[10].Value = model.FavouriteCount;
            cmdParms[11].Value = model.CreatedDate;
            cmdParms[12].Value = model.CommentsCount;
            cmdParms[13].Value = model.IsRecommend;
            cmdParms[14].Value = model.ChannelSequence;
            cmdParms[15].Value = model.Privacy;
            cmdParms[0x10].Value = model.Sequence;
            cmdParms[0x11].Value = model.LastUpdatedDate;
            cmdParms[0x12].Value = model.Tags;
            cmdParms[0x13].Value = model.AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCommentCount(int ablumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.Append("CommentsCount=CommentsCount+1");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ablumId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateEx(Maticsoft.Model.SNS.UserAlbums model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.Append("AlbumName=@AlbumName,");
            builder.Append("Description=@Description ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumName", SqlDbType.NVarChar, 100), new SqlParameter("@Description", SqlDbType.NVarChar), new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumName;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.AppendFormat(" IsRecommend={0} ", IsRecommand);
            builder.AppendFormat(" where AlbumID IN({0})", IdList);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdatePhotoCount()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.Append("PhotoCount=(select COUNT(1) from SNS_UserAlbumDetail where AlbumID=SNS_UserAlbums.AlbumID)");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdatePvCount(int AlbumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.Append("PVCount=PvCount+1 Where AlbumID=" + AlbumId);
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool UpdateRecommand(int ablumId, EnumHelper.RecommendType recommendType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_UserAlbums set ");
            builder.Append("IsRecommend=@IsRecommend");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsRecommend", SqlDbType.Int), new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = (int) recommendType;
            cmdParms[1].Value = ablumId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


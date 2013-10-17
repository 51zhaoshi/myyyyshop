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

    public class Photos : IPhotos
    {
        public int Add(Maticsoft.Model.SNS.Photos model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Photos(");
            builder.Append("PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress)");
            builder.Append(" values (");
            builder.Append("@PhotoName,@PhotoUrl,@Type,@Description,@Status,@CreatedUserID,@CreatedNickName,@CreatedDate,@CategoryId,@PVCount,@ThumbImageUrl,@NormalImageUrl,@Sequence,@IsRecomend,@TopCommentsId,@ForwardedCount,@CommentCount,@FavouriteCount,@OwnerPhotoId,@Tags,@StaticUrl,@MapLng,@MapLat,@PhotoAddress)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@PhotoName", SqlDbType.NVarChar, 200), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@TopCommentsId", SqlDbType.NVarChar, 100), new SqlParameter("@ForwardedCount", SqlDbType.Int, 4), 
                new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@OwnerPhotoId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 300), new SqlParameter("@MapLng", SqlDbType.NVarChar, 200), new SqlParameter("@MapLat", SqlDbType.NVarChar, 200), new SqlParameter("@PhotoAddress", SqlDbType.NVarChar, 300)
             };
            cmdParms[0].Value = model.PhotoName;
            cmdParms[1].Value = model.PhotoUrl;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.CreatedUserID;
            cmdParms[6].Value = model.CreatedNickName;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.CategoryId;
            cmdParms[9].Value = model.PVCount;
            cmdParms[10].Value = model.ThumbImageUrl;
            cmdParms[11].Value = model.NormalImageUrl;
            cmdParms[12].Value = model.Sequence;
            cmdParms[13].Value = model.IsRecomend;
            cmdParms[14].Value = model.TopCommentsId;
            cmdParms[15].Value = model.ForwardedCount;
            cmdParms[0x10].Value = model.CommentCount;
            cmdParms[0x11].Value = model.FavouriteCount;
            cmdParms[0x12].Value = model.OwnerPhotoId;
            cmdParms[0x13].Value = model.Tags;
            cmdParms[20].Value = model.StaticUrl;
            cmdParms[0x15].Value = model.MapLng;
            cmdParms[0x16].Value = model.MapLat;
            cmdParms[0x17].Value = model.PhotoAddress;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.Photos DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Photos photos = new Maticsoft.Model.SNS.Photos();
            if (row != null)
            {
                if ((row["PhotoID"] != null) && (row["PhotoID"].ToString() != ""))
                {
                    photos.PhotoID = int.Parse(row["PhotoID"].ToString());
                }
                if (row["PhotoName"] != null)
                {
                    photos.PhotoName = row["PhotoName"].ToString();
                }
                if (row["PhotoUrl"] != null)
                {
                    photos.PhotoUrl = row["PhotoUrl"].ToString();
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    photos.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Description"] != null)
                {
                    photos.Description = row["Description"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    photos.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    photos.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    photos.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    photos.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    photos.CategoryId = int.Parse(row["CategoryId"].ToString());
                }
                if ((row["PVCount"] != null) && (row["PVCount"].ToString() != ""))
                {
                    photos.PVCount = int.Parse(row["PVCount"].ToString());
                }
                if (row["ThumbImageUrl"] != null)
                {
                    photos.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if (row["NormalImageUrl"] != null)
                {
                    photos.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    photos.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["IsRecomend"] != null) && (row["IsRecomend"].ToString() != ""))
                {
                    photos.IsRecomend = int.Parse(row["IsRecomend"].ToString());
                }
                if (row["TopCommentsId"] != null)
                {
                    photos.TopCommentsId = row["TopCommentsId"].ToString();
                }
                if ((row["ForwardedCount"] != null) && (row["ForwardedCount"].ToString() != ""))
                {
                    photos.ForwardedCount = int.Parse(row["ForwardedCount"].ToString());
                }
                if ((row["CommentCount"] != null) && (row["CommentCount"].ToString() != ""))
                {
                    photos.CommentCount = int.Parse(row["CommentCount"].ToString());
                }
                if ((row["FavouriteCount"] != null) && (row["FavouriteCount"].ToString() != ""))
                {
                    photos.FavouriteCount = int.Parse(row["FavouriteCount"].ToString());
                }
                if ((row["OwnerPhotoId"] != null) && (row["OwnerPhotoId"].ToString() != ""))
                {
                    photos.OwnerPhotoId = new int?(int.Parse(row["OwnerPhotoId"].ToString()));
                }
                if (row["Tags"] != null)
                {
                    photos.Tags = row["Tags"].ToString();
                }
                if (row["StaticUrl"] != null)
                {
                    photos.StaticUrl = row["StaticUrl"].ToString();
                }
                if (row["MapLng"] != null)
                {
                    photos.MapLng = row["MapLng"].ToString();
                }
                if (row["MapLat"] != null)
                {
                    photos.MapLat = row["MapLat"].ToString();
                }
                if (row["PhotoAddress"] != null)
                {
                    photos.PhotoAddress = row["PhotoAddress"].ToString();
                }
            }
            return photos;
        }

        public bool Delete(int PhotoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Photos ");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteEX(int PhotoID)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("delete SNS_UserFavourite ");
            builder.Append(" where type=0 and TargetID=@TargetId ");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            para[0].Value = PhotoID;
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update Accounts_UsersExp set  ShareCount=ShareCount-1");
            builder2.Append(" where UserID=( Select CreatedUserID  from SNS_Photos where PhotoID=@PhotoID) ");
            SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            parameterArray2[0].Value = PhotoID;
            item = new CommandInfo(builder2.ToString(), parameterArray2);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            builder3.Append("  where AlbumID=( Select AlbumID  from SNS_UserAlbumDetail where type=0 and TargetID=@TargetId)");
            SqlParameter[] parameterArray3 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray3[0].Value = PhotoID;
            item = new CommandInfo(builder3.ToString(), parameterArray3);
            cmdList.Add(item);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete SNS_Posts ");
            builder4.Append(" where Type=1 and TargetId=@TargetId ");
            SqlParameter[] parameterArray4 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray4[0].Value = PhotoID;
            item = new CommandInfo(builder4.ToString(), parameterArray4);
            cmdList.Add(item);
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete SNS_Comments ");
            builder5.Append(" where type=1 and TargetID=@TargetId ");
            SqlParameter[] parameterArray5 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray5[0].Value = PhotoID;
            item = new CommandInfo(builder5.ToString(), parameterArray5);
            cmdList.Add(item);
            StringBuilder builder6 = new StringBuilder();
            builder6.Append("delete SNS_UserAlbumDetail ");
            builder6.Append(" where type=0 and TargetID=@TargetId ");
            SqlParameter[] parameterArray6 = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.Int, 4) };
            parameterArray6[0].Value = PhotoID;
            item = new CommandInfo(builder6.ToString(), parameterArray6);
            cmdList.Add(item);
            StringBuilder builder7 = new StringBuilder();
            builder7.Append("delete SNS_Photos ");
            builder7.Append(" where PhotoID=@PhotoID");
            SqlParameter[] parameterArray7 = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            parameterArray7[0].Value = PhotoID;
            item = new CommandInfo(builder7.ToString(), parameterArray7);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool DeleteList(string PhotoIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Photos ");
            builder.Append(" where PhotoID in (" + PhotoIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet DeleteListEx(string Ids, out int Result)
        {
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@TargetIds ", SqlDbType.NVarChar), DbHelperSQL.CreateReturnParam("ReturnValue", SqlDbType.Int, 4) };
            parameters[0].Value = 1;
            parameters[1].Value = Ids;
            DataSet set = DbHelperSQL.RunProcedure("sp_SNS_ImageDeleteAction", parameters, "tb", out Result);
            if (Result == 1)
            {
                return set;
            }
            return null;
        }

        public bool DeleteListEX(string PhotoIDs)
        {
            int length = PhotoIDs.Split(new char[] { ',' }).Length;
            List<CommandInfo> cmdList = new List<CommandInfo>();
            StringBuilder builder = new StringBuilder();
            builder.Append("Update Accounts_UsersExp set  ShareCount=ShareCount-" + length);
            builder.Append(" where UserID in ( Select CreatedUserID  from SNS_Photos where PhotoID in (" + PhotoIDs + ")) ");
            SqlParameter[] para = new SqlParameter[0];
            CommandInfo item = new CommandInfo(builder.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("Update SNS_UserAlbums set  PhotoCount=PhotoCount-1 ");
            builder2.Append("  where AlbumID in ( Select AlbumID  from SNS_UserAlbumDetail where type=0 and TargetID in (" + PhotoIDs + "))");
            item = new CommandInfo(builder2.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder3 = new StringBuilder();
            builder3.Append("delete SNS_Photos ");
            builder3.Append(" where PhotoID in (" + PhotoIDs + ")");
            item = new CommandInfo(builder3.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder4 = new StringBuilder();
            builder4.Append("delete SNS_Posts ");
            builder4.Append(" where Type=1 and TargetId in (" + PhotoIDs + ") ");
            item = new CommandInfo(builder4.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder5 = new StringBuilder();
            builder5.Append("delete SNS_Comments ");
            builder5.Append(" where type=1 and TargetID in (" + PhotoIDs + ") ");
            item = new CommandInfo(builder5.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder6 = new StringBuilder();
            builder6.Append("delete SNS_UserFavourite ");
            builder6.Append(" where type=0 and TargetID in (" + PhotoIDs + ") ");
            item = new CommandInfo(builder6.ToString(), para);
            cmdList.Add(item);
            StringBuilder builder7 = new StringBuilder();
            builder7.Append("delete SNS_UserAlbumDetail ");
            builder7.Append(" where type=0 and TargetID in (" + PhotoIDs + ") ");
            item = new CommandInfo(builder7.ToString(), para);
            cmdList.Add(item);
            return (DbHelperSQL.ExecuteSqlTran(cmdList) > 0);
        }

        public bool Exists(int PhotoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_Photos");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public int GetCountEx(int type, int categoryId, string address)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM  SNS_Photos SP  where   SP.Status=1");
            if (type > 0)
            {
                builder.Append(" and  SP.Type=" + type);
            }
            if (categoryId > 0)
            {
                builder.Append(string.Concat(new object[] { "  AND SP.CategoryId in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=", categoryId, " or Path like '", categoryId, "|%'))" }));
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                builder.Append(" and SP.PhotoAddress like '%" + address + "%'");
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PhotoID,PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress ");
            builder.Append(" FROM SNS_Photos ");
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
            builder.Append(" PhotoID,PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress ");
            builder.Append(" FROM SNS_Photos ");
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
                builder.Append("order by T.PhotoID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Photos T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.PhotoID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Photos T  where");
            if (CateId == 0)
            {
                builder.Append("  CategoryID >0");
            }
            if (CateId > 0)
            {
                builder.Append(string.Concat(new object[] { "  CategoryID in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=", CateId, " or Path like '", CateId, "|%'))" }));
            }
            if (CateId == -1)
            {
                builder.Append("  CategoryID <=0");
            }
            if (strWhere.Length > 1)
            {
                builder.Append(" and ");
                builder.Append(strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageEx(int type, int categoryId, string address, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.PhotoID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Photos T ");
            builder.Append(" where  T.Status=1");
            if (type > 0)
            {
                builder.Append(" and  T.Type=" + type);
            }
            if (categoryId > 0)
            {
                builder.Append(string.Concat(new object[] { "  AND T.CategoryId in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=", categoryId, " or Path like '", categoryId, "|%'))" }));
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                builder.Append(" and T.PhotoAddress like '%" + address + "%'");
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere, int CateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM SNS_Photos  where");
            if (CateId == 0)
            {
                builder.Append("  CategoryID >0");
            }
            if (CateId > 0)
            {
                builder.Append(string.Concat(new object[] { "  CategoryID in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=", CateId, " or Path like '", CateId, "|%'))" }));
            }
            if (CateId == -1)
            {
                builder.Append("  CategoryID <=0");
            }
            if (strWhere.Trim() != "")
            {
                builder.Append(" and ");
                builder.Append(strWhere);
            }
            builder.Append(" order by CreatedDate desc");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListToReGen(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select PhotoID from SNS_Photos  ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append("WHERE  " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PhotoID", "SNS_Photos");
        }

        public Maticsoft.Model.SNS.Photos GetModel(int PhotoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 PhotoID,PhotoName,PhotoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,TopCommentsId,ForwardedCount,CommentCount,FavouriteCount,OwnerPhotoId,Tags,StaticUrl,MapLng,MapLat,PhotoAddress from SNS_Photos ");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            new Maticsoft.Model.SNS.Photos();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public int GetNextID(int photoID, int albumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MIN(TargetID) from SNS_UserAlbumDetail   where  Type=0  ");
            if (albumId == -1)
            {
                builder.Append(" and AlbumID=(select  top 1 AlbumID from SNS_UserAlbumDetail where  Type=0 and TargetID=@TargetID and AlbumUserId=(select CreatedUserID from SNS_Photos where PhotoID=@TargetID) order by ID) ");
            }
            else
            {
                builder.Append(" AND  AlbumID=" + albumId);
            }
            builder.Append(" AND TargetID>@TargetID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4) };
            cmdParms[0].Value = photoID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public DataSet GetPhotoUserIds(string ids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select CreatedUserID from SNS_Photos  where PhotoID IN (" + ids + ") ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetPrevID(int photoID, int albumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MAX(TargetID) from SNS_UserAlbumDetail   where  Type=0  ");
            if (albumId == -1)
            {
                builder.Append(" and AlbumID=(select  top 1 AlbumID from SNS_UserAlbumDetail where  Type=0 and TargetID=@TargetID and AlbumUserId=(select CreatedUserID from SNS_Photos where PhotoID=@TargetID) order by ID) ");
            }
            else
            {
                builder.Append(" AND  AlbumID=" + albumId);
            }
            builder.Append(" AND TargetID<@TargetID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TargetID", SqlDbType.Int, 4) };
            cmdParms[0].Value = photoID;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_Photos ");
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

        public int GetRecordCountEx(string strWhere, int CateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM SNS_Photos where");
            if (CateId == 0)
            {
                builder.Append("  CategoryID >0");
            }
            if (CateId > 0)
            {
                builder.Append(string.Concat(new object[] { "  CategoryID in ( select CategoryID from SNS_Categories where Type=1 and (CategoryID=", CateId, " or Path like '", CateId, "|%'))" }));
            }
            if (CateId == -1)
            {
                builder.Append("  CategoryID<=0 ");
            }
            if (strWhere.Trim() != "")
            {
                builder.Append(" and ");
                builder.Append(strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public DataSet GetZuiInList(int CategoryId, int Top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT p.CreatedNickName NickName,p.PhotoID PhotoId,ue.AblumsCount AlbumsCount,ue.FansCount FansCount,p.ThumbImageUrl PhotoUrl,p.CreatedUserID UserId");
            if (Top > 0)
            {
                builder.Append("  FROM ( SELECT TOP " + Top + " * FROM SNS_Photos ");
            }
            builder.Append("WHERE CategoryId=" + CategoryId + " AND IsRecomend=2 ) AS p INNER JOIN Accounts_UsersExp ue ON ue.UserID=p.CreatedUserID");
            return DbHelperSQL.Query(builder.ToString());
        }

        public bool Update(Maticsoft.Model.SNS.Photos model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Photos set ");
            builder.Append("PhotoName=@PhotoName,");
            builder.Append("PhotoUrl=@PhotoUrl,");
            builder.Append("Type=@Type,");
            builder.Append("Description=@Description,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedNickName=@CreatedNickName,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CategoryId=@CategoryId,");
            builder.Append("PVCount=@PVCount,");
            builder.Append("ThumbImageUrl=@ThumbImageUrl,");
            builder.Append("NormalImageUrl=@NormalImageUrl,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("IsRecomend=@IsRecomend,");
            builder.Append("TopCommentsId=@TopCommentsId,");
            builder.Append("ForwardedCount=@ForwardedCount,");
            builder.Append("CommentCount=@CommentCount,");
            builder.Append("FavouriteCount=@FavouriteCount,");
            builder.Append("OwnerPhotoId=@OwnerPhotoId,");
            builder.Append("Tags=@Tags,");
            builder.Append("StaticUrl=@StaticUrl,");
            builder.Append("MapLng=@MapLng,");
            builder.Append("MapLat=@MapLat,");
            builder.Append("PhotoAddress=@PhotoAddress");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@PhotoName", SqlDbType.NVarChar, 200), new SqlParameter("@PhotoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@TopCommentsId", SqlDbType.NVarChar, 100), new SqlParameter("@ForwardedCount", SqlDbType.Int, 4), 
                new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@OwnerPhotoId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 300), new SqlParameter("@MapLng", SqlDbType.NVarChar, 200), new SqlParameter("@MapLat", SqlDbType.NVarChar, 200), new SqlParameter("@PhotoAddress", SqlDbType.NVarChar, 300), new SqlParameter("@PhotoID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.PhotoName;
            cmdParms[1].Value = model.PhotoUrl;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.Status;
            cmdParms[5].Value = model.CreatedUserID;
            cmdParms[6].Value = model.CreatedNickName;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.CategoryId;
            cmdParms[9].Value = model.PVCount;
            cmdParms[10].Value = model.ThumbImageUrl;
            cmdParms[11].Value = model.NormalImageUrl;
            cmdParms[12].Value = model.Sequence;
            cmdParms[13].Value = model.IsRecomend;
            cmdParms[14].Value = model.TopCommentsId;
            cmdParms[15].Value = model.ForwardedCount;
            cmdParms[0x10].Value = model.CommentCount;
            cmdParms[0x11].Value = model.FavouriteCount;
            cmdParms[0x12].Value = model.OwnerPhotoId;
            cmdParms[0x13].Value = model.Tags;
            cmdParms[20].Value = model.StaticUrl;
            cmdParms[0x15].Value = model.MapLng;
            cmdParms[0x16].Value = model.MapLat;
            cmdParms[0x17].Value = model.PhotoAddress;
            cmdParms[0x18].Value = model.PhotoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateCateList(string PhotoIDs, int CateId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Photos set ");
            builder.Append("CategoryID=@CategoryID");
            builder.Append(" where PhotoID in (" + PhotoIDs + ")");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@CategoryID", SqlDbType.Int, 4) };
            cmdParms[0].Value = CateId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdatePvCount(int PhotoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Photos set ");
            builder.Append("PVCount=PVCount+1");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecomend(int PhotoID, int Recomend)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Photos set ");
            builder.Append("IsRecomend=@Recomend,Status=1");
            builder.Append(" where PhotoID =@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Recomend", SqlDbType.Int, 4), new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Recomend;
            cmdParms[1].Value = PhotoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecomendList(string PhotoIds, int Recomend)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Photos set ");
            builder.Append("IsRecomend=@Recomend,Status=1");
            builder.Append(" where PhotoID in (" + PhotoIds + ")");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Recomend", SqlDbType.Int, 4) };
            cmdParms[0].Value = Recomend;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateRecommandState(int id, int Recomend)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Products set ");
            builder.Append("IsRecomend=@IsRecomend,Status=1");
            builder.Append(" where ProductID=@ProductID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@ProductID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Recomend;
            cmdParms[1].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStaticUrl(int photoId, string staticUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Photos set ");
            builder.Append("StaticUrl=@StaticUrl");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 300), new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = staticUrl;
            cmdParms[1].Value = photoId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateStatus(int PhotoID, int Status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Photos set ");
            builder.Append("Status=@Status");
            builder.Append(" where PhotoID =@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = Status;
            cmdParms[1].Value = PhotoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet UserUploadPhoto(int ablumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT P.*  ");
            builder.Append("FROM  SNS_Photos P , ");
            builder.Append("( SELECT TargetID ");
            builder.Append("FROM SNS_UserAlbumDetail ");
            builder.Append("WHERE AlbumID = @AlbumID ");
            builder.Append("AND Type = 0 ");
            builder.Append(") UAD , ");
            builder.Append("( SELECT CreatedUserID ");
            builder.Append("FROM SNS_UserAlbums ");
            builder.Append("WHERE AlbumID = @AlbumID ");
            builder.Append(") U ");
            builder.Append("WHERE P.PhotoID = UAD.TargetID ");
            builder.Append("AND P.CreatedUserID = U.CreatedUserID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ablumId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }
    }
}


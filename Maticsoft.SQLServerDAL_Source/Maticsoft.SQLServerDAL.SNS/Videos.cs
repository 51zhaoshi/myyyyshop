namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Videos : IVideos
    {
        public int Add(Maticsoft.Model.SNS.Videos model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_Videos(");
            builder.Append("VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags)");
            builder.Append(" values (");
            builder.Append("@VideoName,@VideoUrl,@Type,@Description,@Status,@CreatedUserID,@CreatedNickName,@CreatedDate,@CategoryId,@PVCount,@ThumbImageUrl,@NormalImageUrl,@Sequence,@IsRecomend,@ForwardedCount,@CommentCount,@FavouriteCount,@OwnerVideoId,@Tags)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@VideoName", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@ForwardedCount", SqlDbType.Int, 4), new SqlParameter("@CommentCount", SqlDbType.Int, 4), 
                new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@OwnerVideoId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100)
             };
            cmdParms[0].Value = model.VideoName;
            cmdParms[1].Value = model.VideoUrl;
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
            cmdParms[14].Value = model.ForwardedCount;
            cmdParms[15].Value = model.CommentCount;
            cmdParms[0x10].Value = model.FavouriteCount;
            cmdParms[0x11].Value = model.OwnerVideoId;
            cmdParms[0x12].Value = model.Tags;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.Videos DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.Videos videos = new Maticsoft.Model.SNS.Videos();
            if (row != null)
            {
                if ((row["VideoID"] != null) && (row["VideoID"].ToString() != ""))
                {
                    videos.VideoID = int.Parse(row["VideoID"].ToString());
                }
                if (row["VideoName"] != null)
                {
                    videos.VideoName = row["VideoName"].ToString();
                }
                if (row["VideoUrl"] != null)
                {
                    videos.VideoUrl = row["VideoUrl"].ToString();
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    videos.Type = int.Parse(row["Type"].ToString());
                }
                if (row["Description"] != null)
                {
                    videos.Description = row["Description"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    videos.Status = int.Parse(row["Status"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    videos.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if (row["CreatedNickName"] != null)
                {
                    videos.CreatedNickName = row["CreatedNickName"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    videos.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CategoryId"] != null) && (row["CategoryId"].ToString() != ""))
                {
                    videos.CategoryId = new int?(int.Parse(row["CategoryId"].ToString()));
                }
                if ((row["PVCount"] != null) && (row["PVCount"].ToString() != ""))
                {
                    videos.PVCount = int.Parse(row["PVCount"].ToString());
                }
                if (row["ThumbImageUrl"] != null)
                {
                    videos.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if (row["NormalImageUrl"] != null)
                {
                    videos.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    videos.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["IsRecomend"] != null) && (row["IsRecomend"].ToString() != ""))
                {
                    videos.IsRecomend = int.Parse(row["IsRecomend"].ToString());
                }
                if ((row["ForwardedCount"] != null) && (row["ForwardedCount"].ToString() != ""))
                {
                    videos.ForwardedCount = int.Parse(row["ForwardedCount"].ToString());
                }
                if ((row["CommentCount"] != null) && (row["CommentCount"].ToString() != ""))
                {
                    videos.CommentCount = int.Parse(row["CommentCount"].ToString());
                }
                if ((row["FavouriteCount"] != null) && (row["FavouriteCount"].ToString() != ""))
                {
                    videos.FavouriteCount = int.Parse(row["FavouriteCount"].ToString());
                }
                if ((row["OwnerVideoId"] != null) && (row["OwnerVideoId"].ToString() != ""))
                {
                    videos.OwnerVideoId = new int?(int.Parse(row["OwnerVideoId"].ToString()));
                }
                if (row["Tags"] != null)
                {
                    videos.Tags = row["Tags"].ToString();
                }
            }
            return videos;
        }

        public bool Delete(int VideoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Videos ");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string VideoIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_Videos ");
            builder.Append(" where VideoID in (" + VideoIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select VideoID,VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags ");
            builder.Append(" FROM SNS_Videos ");
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
            builder.Append(" VideoID,VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags ");
            builder.Append(" FROM SNS_Videos ");
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
                builder.Append("order by T.VideoID desc");
            }
            builder.Append(")AS Row, T.*  from SNS_Videos T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.Videos GetModel(int VideoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 VideoID,VideoName,VideoUrl,Type,Description,Status,CreatedUserID,CreatedNickName,CreatedDate,CategoryId,PVCount,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,ForwardedCount,CommentCount,FavouriteCount,OwnerVideoId,Tags from SNS_Videos ");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoID;
            new Maticsoft.Model.SNS.Videos();
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
            builder.Append("select count(1) FROM SNS_Videos ");
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

        public bool Update(Maticsoft.Model.SNS.Videos model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_Videos set ");
            builder.Append("VideoName=@VideoName,");
            builder.Append("VideoUrl=@VideoUrl,");
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
            builder.Append("ForwardedCount=@ForwardedCount,");
            builder.Append("CommentCount=@CommentCount,");
            builder.Append("FavouriteCount=@FavouriteCount,");
            builder.Append("OwnerVideoId=@OwnerVideoId,");
            builder.Append("Tags=@Tags");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@VideoName", SqlDbType.NVarChar, 200), new SqlParameter("@VideoUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedNickName", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Int, 4), new SqlParameter("@ForwardedCount", SqlDbType.Int, 4), new SqlParameter("@CommentCount", SqlDbType.Int, 4), 
                new SqlParameter("@FavouriteCount", SqlDbType.Int, 4), new SqlParameter("@OwnerVideoId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@VideoID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.VideoName;
            cmdParms[1].Value = model.VideoUrl;
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
            cmdParms[14].Value = model.ForwardedCount;
            cmdParms[15].Value = model.CommentCount;
            cmdParms[0x10].Value = model.FavouriteCount;
            cmdParms[0x11].Value = model.OwnerVideoId;
            cmdParms[0x12].Value = model.Tags;
            cmdParms[0x13].Value = model.VideoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


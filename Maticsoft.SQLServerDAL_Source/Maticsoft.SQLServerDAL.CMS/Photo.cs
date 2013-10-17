namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Photo : IPhoto
    {
        public int Add(Maticsoft.Model.CMS.Photo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_Photo(");
            builder.Append("PhotoName,ImageUrl,Description,AlbumID,State,CreatedUserID,CreatedDate,PVCount,ClassID,ThumbImageUrl,NormalImageUrl,Sequence,IsRecomend,CommentCount,Tags,FavouriteCount)");
            builder.Append(" values (");
            builder.Append("@PhotoName,@ImageUrl,@Description,@AlbumID,@State,@CreatedUserID,@CreatedDate,@PVCount,@ClassID,@ThumbImageUrl,@NormalImageUrl,@Sequence,@IsRecomend,@CommentCount,@Tags,@FavouriteCount)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoName", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@ClassID", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Bit, 1), new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 200), new SqlParameter("@FavouriteCount", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.PhotoName;
            cmdParms[1].Value = model.ImageUrl;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.AlbumID;
            cmdParms[4].Value = model.State;
            cmdParms[5].Value = model.CreatedUserID;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.PVCount;
            cmdParms[8].Value = model.ClassID;
            cmdParms[9].Value = model.ThumbImageUrl;
            cmdParms[10].Value = model.NormalImageUrl;
            cmdParms[11].Value = model.Sequence;
            cmdParms[12].Value = model.IsRecomend;
            cmdParms[13].Value = model.CommentCount;
            cmdParms[14].Value = model.Tags;
            cmdParms[15].Value = model.FavouriteCount;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public bool Delete(int PhotoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_Photo ");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string PhotoIDlist, out DataSet imageList)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT ImageUrl ,ThumbImageUrl,NormalImageUrl FROM CMS_Photo");
            builder.AppendFormat(" WHERE PhotoID IN ({0})  ", PhotoIDlist);
            imageList = DbHelperSQL.Query(builder.ToString());
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("delete from CMS_Photo ");
            builder2.Append(" where PhotoID in (" + PhotoIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder2.ToString()) > 0);
        }

        public bool Exists(int PhotoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_Photo");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select T.*,PA.CoverPhoto FROM CMS_Photo  T LEFT JOIN CMS_PhotoAlbum PA ON T.AlbumID = PA.AlbumID");
            builder.Append(" ");
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
                builder.Append(" top " + Top);
            }
            builder.Append(" *  FROM CMS_Photo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListAroundPhotoId(int Top, int PhotoId, int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  * ");
            builder.Append("FROM    CMS_Photo ");
            builder.Append("WHERE   PhotoID IN ( SELECT PhotoID ");
            builder.Append("                     FROM   ( SELECT TOP " + Top + " ");
            builder.Append("                                        PhotoID , ");
            builder.Append("                                        ABS(PhotoID - " + PhotoId + ") AS seq ");
            builder.Append("                              FROM      ( SELECT    * ");
            builder.Append("                                          FROM      CMS_Photo ");
            builder.Append("                                          WHERE     ClassID = " + ClassId + " ");
            builder.Append("                                        ) temp ");
            builder.Append("                              ORDER BY  seq ");
            builder.Append("                            ) temp1 ) ");
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
                builder.Append("order by T.PhotoID desc");
            }
            builder.Append(")AS Row, T.*,PA.CoverPhoto  from CMS_Photo T LEFT JOIN CMS_PhotoAlbum PA ON T.AlbumID = PA.AlbumID");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListToReGen(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select PhotoID from CMS_Photo  ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                builder.Append("WHERE  " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("PhotoID", "CMS_Photo");
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_Photo");
        }

        public Maticsoft.Model.CMS.Photo GetModel(int PhotoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CMSP.*,AU.UserName AS CreatedUserName from CMS_Photo CMSP ");
            builder.Append(" LEFT JOIN Accounts_Users AU ON CMSP.CreatedUserID=AU.UserID ");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@PhotoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = PhotoID;
            Maticsoft.Model.CMS.Photo photo = new Maticsoft.Model.CMS.Photo();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["PhotoID"] != null) && (set.Tables[0].Rows[0]["PhotoID"].ToString() != ""))
            {
                photo.PhotoID = int.Parse(set.Tables[0].Rows[0]["PhotoID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["PhotoName"] != null) && (set.Tables[0].Rows[0]["PhotoName"].ToString() != ""))
            {
                photo.PhotoName = set.Tables[0].Rows[0]["PhotoName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["ImageUrl"] != null) && (set.Tables[0].Rows[0]["ImageUrl"].ToString() != ""))
            {
                photo.ImageUrl = set.Tables[0].Rows[0]["ImageUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                photo.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["AlbumID"] != null) && (set.Tables[0].Rows[0]["AlbumID"].ToString() != ""))
            {
                photo.AlbumID = int.Parse(set.Tables[0].Rows[0]["AlbumID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["State"] != null) && (set.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                photo.State = int.Parse(set.Tables[0].Rows[0]["State"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CreatedUserID"] != null) && (set.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                photo.CreatedUserID = int.Parse(set.Tables[0].Rows[0]["CreatedUserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                photo.CreatedDate = DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["PVCount"] != null) && (set.Tables[0].Rows[0]["PVCount"].ToString() != ""))
            {
                photo.PVCount = int.Parse(set.Tables[0].Rows[0]["PVCount"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ClassID"] != null) && (set.Tables[0].Rows[0]["ClassID"].ToString() != ""))
            {
                photo.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ThumbImageUrl"] != null) && (set.Tables[0].Rows[0]["ThumbImageUrl"].ToString() != ""))
            {
                photo.ThumbImageUrl = set.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["NormalImageUrl"] != null) && (set.Tables[0].Rows[0]["NormalImageUrl"].ToString() != ""))
            {
                photo.NormalImageUrl = set.Tables[0].Rows[0]["NormalImageUrl"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Sequence"] != null) && (set.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                photo.Sequence = new int?(int.Parse(set.Tables[0].Rows[0]["Sequence"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["IsRecomend"] != null) && (set.Tables[0].Rows[0]["IsRecomend"].ToString() != ""))
            {
                photo.IsRecomend = new bool?((set.Tables[0].Rows[0]["IsRecomend"].ToString() == "1") || (set.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower() == "true"));
            }
            if ((set.Tables[0].Rows[0]["CommentCount"] != null) && (set.Tables[0].Rows[0]["CommentCount"].ToString() != ""))
            {
                photo.CommentCount = new int?(int.Parse(set.Tables[0].Rows[0]["CommentCount"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Tags"] != null) && (set.Tables[0].Rows[0]["Tags"].ToString() != ""))
            {
                photo.Tags = set.Tables[0].Rows[0]["Tags"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CreatedUserName"] != null) && (set.Tables[0].Rows[0]["CreatedUserName"].ToString() != ""))
            {
                photo.CreatedUserName = set.Tables[0].Rows[0]["CreatedUserName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["FavouriteCount"] != null) && (set.Tables[0].Rows[0]["FavouriteCount"].ToString() != ""))
            {
                photo.FavouriteCount = int.Parse(set.Tables[0].Rows[0]["FavouriteCount"].ToString());
            }
            return photo;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_Photo T");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public bool Update(Maticsoft.Model.CMS.Photo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Photo set ");
            builder.Append("PhotoName=@PhotoName,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("Description=@Description,");
            builder.Append("AlbumID=@AlbumID,");
            builder.Append("State=@State,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("PVCount=@PVCount,");
            builder.Append("ClassID=@ClassID,");
            builder.Append("ThumbImageUrl=@ThumbImageUrl,");
            builder.Append("NormalImageUrl=@NormalImageUrl,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("IsRecomend=@IsRecomend,");
            builder.Append("CommentCount=@CommentCount,");
            builder.Append("Tags=@Tags,");
            builder.Append("FavouriteCount=@FavouriteCount ");
            builder.Append(" where PhotoID=@PhotoID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@PhotoName", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NVarChar, 500), new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@ClassID", SqlDbType.Int, 4), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Bit, 1), new SqlParameter("@CommentCount", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 200), new SqlParameter("@PhotoID", SqlDbType.Int, 4), 
                new SqlParameter("@FavouriteCount", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.PhotoName;
            cmdParms[1].Value = model.ImageUrl;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.AlbumID;
            cmdParms[4].Value = model.State;
            cmdParms[5].Value = model.CreatedUserID;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.PVCount;
            cmdParms[8].Value = model.ClassID;
            cmdParms[9].Value = model.ThumbImageUrl;
            cmdParms[10].Value = model.NormalImageUrl;
            cmdParms[11].Value = model.Sequence;
            cmdParms[12].Value = model.IsRecomend;
            cmdParms[13].Value = model.CommentCount;
            cmdParms[14].Value = model.Tags;
            cmdParms[15].Value = model.PhotoID;
            cmdParms[0x10].Value = model.FavouriteCount;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdatePhotoAlbum(int AlbumID, int newAlbumId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update CMS_Photo set AlbumID = @newAlbumId where AlbumID = @AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@newAlbumId", SqlDbType.Int), new SqlParameter("@AlbumID", SqlDbType.Int) };
            cmdParms[0].Value = newAlbumId;
            cmdParms[1].Value = AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


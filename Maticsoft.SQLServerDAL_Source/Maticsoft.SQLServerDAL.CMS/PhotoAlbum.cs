namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PhotoAlbum : IPhotoAlbum
    {
        public int Add(Maticsoft.Model.CMS.PhotoAlbum model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_PhotoAlbum(");
            builder.Append("AlbumName,Description,CoverPhoto,State,CreatedUserID,CreatedDate,PVCount,Sequence,Privacy,LastUpdatedDate)");
            builder.Append(" values (");
            builder.Append("@AlbumName,@Description,@CoverPhoto,@State,@CreatedUserID,@CreatedDate,@PVCount,@Sequence,@Privacy,@LastUpdatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumName", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CoverPhoto", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.AlbumName;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.CoverPhoto;
            cmdParms[3].Value = model.State;
            cmdParms[4].Value = model.CreatedUserID;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.PVCount;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.Privacy;
            cmdParms[9].Value = model.LastUpdatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public bool Delete(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_PhotoAlbum ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AlbumIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_PhotoAlbum ");
            builder.Append(" where AlbumID in (" + AlbumIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_PhotoAlbum");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT PL.*,p.ThumbImageUrl FROM CMS_PhotoAlbum PL LEFT JOIN CMS_Photo P ON p.PhotoID = PL.CoverPhoto");
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
            builder.Append(" * FROM CMS_PhotoAlbum ");
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
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by T." + orderby);
            }
            else
            {
                builder.Append("order by T.AlbumID ASC");
            }
            builder.Append(")AS Row, T.*,P.ThumbImageUrl from CMS_PhotoAlbum T LEFT JOIN CMS_Photo P ON p.PhotoID = T.CoverPhoto");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AlbumID", "CMS_PhotoAlbum");
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_PhotoAlbum");
        }

        public Maticsoft.Model.CMS.PhotoAlbum GetModel(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AlbumID,AlbumName,Description,CoverPhoto,State,CreatedUserID,CreatedDate,PVCount,Sequence,Privacy,LastUpdatedDate from CMS_PhotoAlbum ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            Maticsoft.Model.CMS.PhotoAlbum album = new Maticsoft.Model.CMS.PhotoAlbum();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["AlbumID"] != null) && (set.Tables[0].Rows[0]["AlbumID"].ToString() != ""))
            {
                album.AlbumID = int.Parse(set.Tables[0].Rows[0]["AlbumID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["AlbumName"] != null) && (set.Tables[0].Rows[0]["AlbumName"].ToString() != ""))
            {
                album.AlbumName = set.Tables[0].Rows[0]["AlbumName"].ToString();
            }
            if ((set.Tables[0].Rows[0]["Description"] != null) && (set.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                album.Description = set.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CoverPhoto"] != null) && (set.Tables[0].Rows[0]["CoverPhoto"].ToString() != ""))
            {
                album.CoverPhoto = new int?(int.Parse(set.Tables[0].Rows[0]["CoverPhoto"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["State"] != null) && (set.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                album.State = int.Parse(set.Tables[0].Rows[0]["State"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CreatedUserID"] != null) && (set.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                album.CreatedUserID = int.Parse(set.Tables[0].Rows[0]["CreatedUserID"].ToString());
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                album.CreatedDate = DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((set.Tables[0].Rows[0]["PVCount"] != null) && (set.Tables[0].Rows[0]["PVCount"].ToString() != ""))
            {
                album.PVCount = int.Parse(set.Tables[0].Rows[0]["PVCount"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Sequence"] != null) && (set.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                album.Sequence = int.Parse(set.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Privacy"] != null) && (set.Tables[0].Rows[0]["Privacy"].ToString() != ""))
            {
                album.Privacy = int.Parse(set.Tables[0].Rows[0]["Privacy"].ToString());
            }
            if ((set.Tables[0].Rows[0]["LastUpdatedDate"] != null) && (set.Tables[0].Rows[0]["LastUpdatedDate"].ToString() != ""))
            {
                album.LastUpdatedDate = DateTime.Parse(set.Tables[0].Rows[0]["LastUpdatedDate"].ToString());
            }
            return album;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_PhotoAlbum ");
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

        public bool Update(Maticsoft.Model.CMS.PhotoAlbum model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_PhotoAlbum set ");
            builder.Append("AlbumName=@AlbumName,");
            builder.Append("Description=@Description,");
            builder.Append("CoverPhoto=@CoverPhoto,");
            builder.Append("State=@State,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("PVCount=@PVCount,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Privacy=@Privacy,");
            builder.Append("LastUpdatedDate=@LastUpdatedDate");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumName", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CoverPhoto", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@PVCount", SqlDbType.Int, 4), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumName;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.CoverPhoto;
            cmdParms[3].Value = model.State;
            cmdParms[4].Value = model.CreatedUserID;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.PVCount;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.Privacy;
            cmdParms[9].Value = model.LastUpdatedDate;
            cmdParms[10].Value = model.AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


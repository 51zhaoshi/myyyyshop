namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class VideoAlbum : IVideoAlbum
    {
        public int Add(Maticsoft.Model.CMS.VideoAlbum model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_VideoAlbum(");
            builder.Append("AlbumName,CoverVideo,Description,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdatedDate,State,Sequence,Privacy,PvCount)");
            builder.Append(" values (");
            builder.Append("@AlbumName,@CoverVideo,@Description,@CreatedUserID,@CreatedDate,@LastUpdateUserID,@LastUpdatedDate,@State,@Sequence,@Privacy,@PvCount)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumName", SqlDbType.NVarChar, 100), new SqlParameter("@CoverVideo", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@LastUpdateUserID", SqlDbType.Int, 4), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@PvCount", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumName;
            cmdParms[1].Value = model.CoverVideo;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.LastUpdateUserID;
            cmdParms[6].Value = model.LastUpdatedDate;
            cmdParms[7].Value = model.State;
            cmdParms[8].Value = model.Sequence;
            cmdParms[9].Value = model.Privacy;
            cmdParms[10].Value = model.PvCount;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_VideoAlbum ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string AlbumIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_VideoAlbum ");
            builder.Append(" where AlbumID in (" + AlbumIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_VideoAlbum");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AlbumID,AlbumName,CoverVideo,Description,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdatedDate,State,Sequence,Privacy,PvCount ");
            builder.Append(" FROM CMS_VideoAlbum ");
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
            builder.Append(" AlbumID,AlbumName,CoverVideo,Description,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdatedDate,State,Sequence,Privacy,PvCount ");
            builder.Append(" FROM CMS_VideoAlbum ");
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
                builder.Append("order by T.AlbumID desc");
            }
            builder.Append(")AS Row, T.*  from CMS_VideoAlbum T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(string strWhere, string orderby)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM View_VideoAlbum ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by AlbumID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("AlbumID", "CMS_VideoAlbum");
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_VideoAlbum");
        }

        public Maticsoft.Model.CMS.VideoAlbum GetModel(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AlbumID,AlbumName,CoverVideo,Description,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdatedDate,State,Sequence,Privacy,PvCount from CMS_VideoAlbum ");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            Maticsoft.Model.CMS.VideoAlbum album = new Maticsoft.Model.CMS.VideoAlbum();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((ds.Tables[0].Rows[0]["AlbumID"] != null) && (ds.Tables[0].Rows[0]["AlbumID"].ToString() != ""))
            {
                album.AlbumID = int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["AlbumName"] != null) && (ds.Tables[0].Rows[0]["AlbumName"].ToString() != ""))
            {
                album.AlbumName = ds.Tables[0].Rows[0]["AlbumName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CoverVideo"] != null) && (ds.Tables[0].Rows[0]["CoverVideo"].ToString() != ""))
            {
                album.CoverVideo = ds.Tables[0].Rows[0]["CoverVideo"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Description"] != null) && (ds.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                album.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserID"] != null) && (ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                album.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["CreatedDate"] != null) && (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                album.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateUserID"] != null) && (ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString() != ""))
            {
                album.LastUpdateUserID = new int?(int.Parse(ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["LastUpdatedDate"] != null) && (ds.Tables[0].Rows[0]["LastUpdatedDate"].ToString() != ""))
            {
                album.LastUpdatedDate = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdatedDate"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["State"] != null) && (ds.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                album.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Sequence"] != null) && (ds.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                album.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Privacy"] != null) && (ds.Tables[0].Rows[0]["Privacy"].ToString() != ""))
            {
                album.Privacy = new int?(int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["PvCount"] != null) && (ds.Tables[0].Rows[0]["PvCount"].ToString() != ""))
            {
                album.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
            }
            return album;
        }

        public Maticsoft.Model.CMS.VideoAlbum GetModelEx(int AlbumID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT top 1 * FROM View_VideoAlbum ");
            builder.Append(" where AlbumID=@AlbumID ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = AlbumID;
            Maticsoft.Model.CMS.VideoAlbum album = new Maticsoft.Model.CMS.VideoAlbum();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((ds.Tables[0].Rows[0]["AlbumID"] != null) && (ds.Tables[0].Rows[0]["AlbumID"].ToString() != ""))
            {
                album.AlbumID = int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["AlbumName"] != null) && (ds.Tables[0].Rows[0]["AlbumName"].ToString() != ""))
            {
                album.AlbumName = ds.Tables[0].Rows[0]["AlbumName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CoverVideo"] != null) && (ds.Tables[0].Rows[0]["CoverVideo"].ToString() != ""))
            {
                album.CoverVideo = ds.Tables[0].Rows[0]["CoverVideo"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Description"] != null) && (ds.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                album.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserID"] != null) && (ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                album.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserName"] != null) && (ds.Tables[0].Rows[0]["CreatedUserName"].ToString() != ""))
            {
                album.CreatedUserName = ds.Tables[0].Rows[0]["CreatedUserName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CreatedDate"] != null) && (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                album.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateUserID"] != null) && (ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString() != ""))
            {
                album.LastUpdateUserID = new int?(int.Parse(ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateUserName"] != null) && (ds.Tables[0].Rows[0]["CreatedUserName"].ToString() != ""))
            {
                album.LastUpdateUserName = ds.Tables[0].Rows[0]["LastUpdateUserName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["LastUpdatedDate"] != null) && (ds.Tables[0].Rows[0]["LastUpdatedDate"].ToString() != ""))
            {
                album.LastUpdatedDate = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdatedDate"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["State"] != null) && (ds.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                album.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Sequence"] != null) && (ds.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                album.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Privacy"] != null) && (ds.Tables[0].Rows[0]["Privacy"].ToString() != ""))
            {
                album.Privacy = new int?(int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["PvCount"] != null) && (ds.Tables[0].Rows[0]["PvCount"].ToString() != ""))
            {
                album.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
            }
            return album;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_VideoAlbum ");
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

        public bool Update(Maticsoft.Model.CMS.VideoAlbum model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_VideoAlbum set ");
            builder.Append("AlbumName=@AlbumName,");
            builder.Append("CoverVideo=@CoverVideo,");
            builder.Append("Description=@Description,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("LastUpdateUserID=@LastUpdateUserID,");
            builder.Append("LastUpdatedDate=@LastUpdatedDate,");
            builder.Append("State=@State,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Privacy=@Privacy,");
            builder.Append("PvCount=@PvCount");
            builder.Append(" where AlbumID=@AlbumID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@AlbumName", SqlDbType.NVarChar, 100), new SqlParameter("@CoverVideo", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@LastUpdateUserID", SqlDbType.Int, 4), new SqlParameter("@LastUpdatedDate", SqlDbType.DateTime), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@AlbumID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.AlbumName;
            cmdParms[1].Value = model.CoverVideo;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.LastUpdateUserID;
            cmdParms[6].Value = model.LastUpdatedDate;
            cmdParms[7].Value = model.State;
            cmdParms[8].Value = model.Sequence;
            cmdParms[9].Value = model.Privacy;
            cmdParms[10].Value = model.PvCount;
            cmdParms[11].Value = model.AlbumID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_VideoAlbum set " + strWhere);
            builder.Append(" where AlbumID in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}


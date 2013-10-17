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

    public class Video : IVideo
    {
        public int Add(Maticsoft.Model.CMS.Video model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_Video(");
            builder.Append("Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount)");
            builder.Append(" values (");
            builder.Append("@Title,@Description,@AlbumID,@CreatedUserID,@CreatedDate,@LastUpdateUserID,@LastUpdateDate,@Sequence,@VideoClassID,@IsRecomend,@ImageUrl,@ThumbImageUrl,@NormalImageUrl,@TotalTime,@TotalComment,@TotalFav,@TotalUp,@Reference,@Tags,@VideoUrl,@UrlType,@VideoFormat,@Domain,@Grade,@Attachment,@Privacy,@State,@Remark,@PvCount)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Title", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@LastUpdateUserID", SqlDbType.Int, 4), new SqlParameter("@LastUpdateDate", SqlDbType.DateTime), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@VideoClassID", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Bit, 1), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TotalTime", SqlDbType.Int, 4), new SqlParameter("@TotalComment", SqlDbType.Int, 4), new SqlParameter("@TotalFav", SqlDbType.Int, 4), 
                new SqlParameter("@TotalUp", SqlDbType.Int, 4), new SqlParameter("@Reference", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@VideoUrl", SqlDbType.NVarChar), new SqlParameter("@UrlType", SqlDbType.NVarChar), new SqlParameter("@VideoFormat", SqlDbType.NVarChar, 50), new SqlParameter("@Domain", SqlDbType.NVarChar, 50), new SqlParameter("@Grade", SqlDbType.Int, 4), new SqlParameter("@Attachment", SqlDbType.NVarChar, 100), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@PvCount", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.AlbumID;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.LastUpdateUserID;
            cmdParms[6].Value = model.LastUpdateDate;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.VideoClassID;
            cmdParms[9].Value = model.IsRecomend;
            cmdParms[10].Value = model.ImageUrl;
            cmdParms[11].Value = model.ThumbImageUrl;
            cmdParms[12].Value = model.NormalImageUrl;
            cmdParms[13].Value = model.TotalTime;
            cmdParms[14].Value = model.TotalComment;
            cmdParms[15].Value = model.TotalFav;
            cmdParms[0x10].Value = model.TotalUp;
            cmdParms[0x11].Value = model.Reference;
            cmdParms[0x12].Value = model.Tags;
            cmdParms[0x13].Value = model.VideoUrl;
            cmdParms[20].Value = model.UrlType;
            cmdParms[0x15].Value = model.VideoFormat;
            cmdParms[0x16].Value = model.Domain;
            cmdParms[0x17].Value = model.Grade;
            cmdParms[0x18].Value = model.Attachment;
            cmdParms[0x19].Value = model.Privacy;
            cmdParms[0x1a].Value = model.State;
            cmdParms[0x1b].Value = model.Remark;
            cmdParms[0x1c].Value = model.PvCount;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int VideoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_Video ");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string VideoIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_Video ");
            builder.Append(" where VideoID in (" + VideoIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int VideoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_Video");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select VideoID,Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount ");
            builder.Append(" FROM CMS_Video ");
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
            builder.Append(" VideoID,Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount ");
            builder.Append(" FROM CMS_Video ");
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
                builder.Append("order by T.VideoID desc");
            }
            builder.Append(")AS Row, T.*  from CMS_Video T ");
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
            builder.Append(" SELECT * FROM View_Video ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("order by " + orderby);
            }
            else
            {
                builder.Append("order by VideoID desc");
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("VideoID", "CMS_Video");
        }

        public int GetMaxSequence()
        {
            return DbHelperSQL.GetMaxID("Sequence", "CMS_Video");
        }

        public Maticsoft.Model.CMS.Video GetModel(int VideoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 VideoID,Title,Description,AlbumID,CreatedUserID,CreatedDate,LastUpdateUserID,LastUpdateDate,Sequence,VideoClassID,IsRecomend,ImageUrl,ThumbImageUrl,NormalImageUrl,TotalTime,TotalComment,TotalFav,TotalUp,Reference,Tags,VideoUrl,UrlType,VideoFormat,Domain,Grade,Attachment,Privacy,State,Remark,PvCount from CMS_Video ");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoID;
            Maticsoft.Model.CMS.Video video = new Maticsoft.Model.CMS.Video();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((ds.Tables[0].Rows[0]["VideoID"] != null) && (ds.Tables[0].Rows[0]["VideoID"].ToString() != ""))
            {
                video.VideoID = int.Parse(ds.Tables[0].Rows[0]["VideoID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Title"] != null) && (ds.Tables[0].Rows[0]["Title"].ToString() != ""))
            {
                video.Title = ds.Tables[0].Rows[0]["Title"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Description"] != null) && (ds.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                video.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["AlbumID"] != null) && (ds.Tables[0].Rows[0]["AlbumID"].ToString() != ""))
            {
                video.AlbumID = new int?(int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserID"] != null) && (ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                video.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["CreatedDate"] != null) && (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                video.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateUserID"] != null) && (ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString() != ""))
            {
                video.LastUpdateUserID = new int?(int.Parse(ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateDate"] != null) && (ds.Tables[0].Rows[0]["LastUpdateDate"].ToString() != ""))
            {
                video.LastUpdateDate = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateDate"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["Sequence"] != null) && (ds.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                video.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["VideoClassID"] != null) && (ds.Tables[0].Rows[0]["VideoClassID"].ToString() != ""))
            {
                video.VideoClassID = new int?(int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["IsRecomend"] != null) && (ds.Tables[0].Rows[0]["IsRecomend"].ToString() != ""))
            {
                if ((ds.Tables[0].Rows[0]["IsRecomend"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower() == "true"))
                {
                    video.IsRecomend = true;
                }
                else
                {
                    video.IsRecomend = false;
                }
            }
            if ((ds.Tables[0].Rows[0]["ImageUrl"] != null) && (ds.Tables[0].Rows[0]["ImageUrl"].ToString() != ""))
            {
                video.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["ThumbImageUrl"] != null) && (ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString() != ""))
            {
                video.ThumbImageUrl = ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["NormalImageUrl"] != null) && (ds.Tables[0].Rows[0]["NormalImageUrl"].ToString() != ""))
            {
                video.NormalImageUrl = ds.Tables[0].Rows[0]["NormalImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["TotalTime"] != null) && (ds.Tables[0].Rows[0]["TotalTime"].ToString() != ""))
            {
                video.TotalTime = new int?(int.Parse(ds.Tables[0].Rows[0]["TotalTime"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["TotalComment"] != null) && (ds.Tables[0].Rows[0]["TotalComment"].ToString() != ""))
            {
                video.TotalComment = int.Parse(ds.Tables[0].Rows[0]["TotalComment"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["TotalFav"] != null) && (ds.Tables[0].Rows[0]["TotalFav"].ToString() != ""))
            {
                video.TotalFav = int.Parse(ds.Tables[0].Rows[0]["TotalFav"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["TotalUp"] != null) && (ds.Tables[0].Rows[0]["TotalUp"].ToString() != ""))
            {
                video.TotalUp = int.Parse(ds.Tables[0].Rows[0]["TotalUp"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Reference"] != null) && (ds.Tables[0].Rows[0]["Reference"].ToString() != ""))
            {
                video.Reference = int.Parse(ds.Tables[0].Rows[0]["Reference"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Tags"] != null) && (ds.Tables[0].Rows[0]["Tags"].ToString() != ""))
            {
                video.Tags = ds.Tables[0].Rows[0]["Tags"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["VideoUrl"] != null) && (ds.Tables[0].Rows[0]["VideoUrl"].ToString() != ""))
            {
                video.VideoUrl = ds.Tables[0].Rows[0]["VideoUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["UrlType"] != null) && (ds.Tables[0].Rows[0]["UrlType"].ToString() != ""))
            {
                video.UrlType = int.Parse(ds.Tables[0].Rows[0]["UrlType"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["VideoFormat"] != null) && (ds.Tables[0].Rows[0]["VideoFormat"].ToString() != ""))
            {
                video.VideoFormat = ds.Tables[0].Rows[0]["VideoFormat"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Domain"] != null) && (ds.Tables[0].Rows[0]["Domain"].ToString() != ""))
            {
                video.Domain = ds.Tables[0].Rows[0]["Domain"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Grade"] != null) && (ds.Tables[0].Rows[0]["Grade"].ToString() != ""))
            {
                video.Grade = int.Parse(ds.Tables[0].Rows[0]["Grade"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Attachment"] != null) && (ds.Tables[0].Rows[0]["Attachment"].ToString() != ""))
            {
                video.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Privacy"] != null) && (ds.Tables[0].Rows[0]["Privacy"].ToString() != ""))
            {
                video.Privacy = int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["State"] != null) && (ds.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                video.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Remark"] != null) && (ds.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                video.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["PvCount"] != null) && (ds.Tables[0].Rows[0]["PvCount"].ToString() != ""))
            {
                video.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
            }
            return video;
        }

        public Maticsoft.Model.CMS.Video GetModelEx(int VideoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT Top 1 * FROM View_Video ");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@VideoID", SqlDbType.Int, 4) };
            cmdParms[0].Value = VideoID;
            Maticsoft.Model.CMS.Video video = new Maticsoft.Model.CMS.Video();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((ds.Tables[0].Rows[0]["VideoID"] != null) && (ds.Tables[0].Rows[0]["VideoID"].ToString() != ""))
            {
                video.VideoID = int.Parse(ds.Tables[0].Rows[0]["VideoID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Title"] != null) && (ds.Tables[0].Rows[0]["Title"].ToString() != ""))
            {
                video.Title = ds.Tables[0].Rows[0]["Title"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Description"] != null) && (ds.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                video.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["AlbumID"] != null) && (ds.Tables[0].Rows[0]["AlbumID"].ToString() != ""))
            {
                video.AlbumID = new int?(int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserID"] != null) && (ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                video.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserName"] != null) && (ds.Tables[0].Rows[0]["CreatedUserName"].ToString() != ""))
            {
                video.CreatedUserName = ds.Tables[0].Rows[0]["CreatedUserName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CreatedDate"] != null) && (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                video.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateUserID"] != null) && (ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString() != ""))
            {
                video.LastUpdateUserID = new int?(int.Parse(ds.Tables[0].Rows[0]["LastUpdateUserID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateUserName"] != null) && (ds.Tables[0].Rows[0]["LastUpdateUserName"].ToString() != ""))
            {
                video.LastUpdateUserName = ds.Tables[0].Rows[0]["LastUpdateUserName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["LastUpdateDate"] != null) && (ds.Tables[0].Rows[0]["LastUpdateDate"].ToString() != ""))
            {
                video.LastUpdateDate = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["LastUpdateDate"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["Sequence"] != null) && (ds.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                video.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["VideoClassID"] != null) && (ds.Tables[0].Rows[0]["VideoClassID"].ToString() != ""))
            {
                video.VideoClassID = new int?(int.Parse(ds.Tables[0].Rows[0]["VideoClassID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["IsRecomend"] != null) && (ds.Tables[0].Rows[0]["IsRecomend"].ToString() != ""))
            {
                if ((ds.Tables[0].Rows[0]["IsRecomend"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower() == "true"))
                {
                    video.IsRecomend = true;
                }
                else
                {
                    video.IsRecomend = false;
                }
            }
            if ((ds.Tables[0].Rows[0]["ImageUrl"] != null) && (ds.Tables[0].Rows[0]["ImageUrl"].ToString() != ""))
            {
                video.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["ThumbImageUrl"] != null) && (ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString() != ""))
            {
                video.ThumbImageUrl = ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["NormalImageUrl"] != null) && (ds.Tables[0].Rows[0]["NormalImageUrl"].ToString() != ""))
            {
                video.NormalImageUrl = ds.Tables[0].Rows[0]["NormalImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["TotalTime"] != null) && (ds.Tables[0].Rows[0]["TotalTime"].ToString() != ""))
            {
                video.TotalTime = new int?(int.Parse(ds.Tables[0].Rows[0]["TotalTime"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["TotalComment"] != null) && (ds.Tables[0].Rows[0]["TotalComment"].ToString() != ""))
            {
                video.TotalComment = int.Parse(ds.Tables[0].Rows[0]["TotalComment"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["TotalFav"] != null) && (ds.Tables[0].Rows[0]["TotalFav"].ToString() != ""))
            {
                video.TotalFav = int.Parse(ds.Tables[0].Rows[0]["TotalFav"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["TotalUp"] != null) && (ds.Tables[0].Rows[0]["TotalUp"].ToString() != ""))
            {
                video.TotalUp = int.Parse(ds.Tables[0].Rows[0]["TotalUp"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Reference"] != null) && (ds.Tables[0].Rows[0]["Reference"].ToString() != ""))
            {
                video.Reference = int.Parse(ds.Tables[0].Rows[0]["Reference"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Tags"] != null) && (ds.Tables[0].Rows[0]["Tags"].ToString() != ""))
            {
                video.Tags = ds.Tables[0].Rows[0]["Tags"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["VideoUrl"] != null) && (ds.Tables[0].Rows[0]["VideoUrl"].ToString() != ""))
            {
                video.VideoUrl = ds.Tables[0].Rows[0]["VideoUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["UrlType"] != null) && (ds.Tables[0].Rows[0]["UrlType"].ToString() != ""))
            {
                video.UrlType = int.Parse(ds.Tables[0].Rows[0]["UrlType"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["VideoFormat"] != null) && (ds.Tables[0].Rows[0]["VideoFormat"].ToString() != ""))
            {
                video.VideoFormat = ds.Tables[0].Rows[0]["VideoFormat"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Domain"] != null) && (ds.Tables[0].Rows[0]["Domain"].ToString() != ""))
            {
                video.Domain = ds.Tables[0].Rows[0]["Domain"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Grade"] != null) && (ds.Tables[0].Rows[0]["Grade"].ToString() != ""))
            {
                video.Grade = int.Parse(ds.Tables[0].Rows[0]["Grade"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Attachment"] != null) && (ds.Tables[0].Rows[0]["Attachment"].ToString() != ""))
            {
                video.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Privacy"] != null) && (ds.Tables[0].Rows[0]["Privacy"].ToString() != ""))
            {
                video.Privacy = int.Parse(ds.Tables[0].Rows[0]["Privacy"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["State"] != null) && (ds.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                video.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Remark"] != null) && (ds.Tables[0].Rows[0]["Remark"].ToString() != ""))
            {
                video.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["PvCount"] != null) && (ds.Tables[0].Rows[0]["PvCount"].ToString() != ""))
            {
                video.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
            }
            return video;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_Video ");
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

        public bool Update(Maticsoft.Model.CMS.Video model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Video set ");
            builder.Append("Title=@Title,");
            builder.Append("Description=@Description,");
            builder.Append("AlbumID=@AlbumID,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("LastUpdateUserID=@LastUpdateUserID,");
            builder.Append("LastUpdateDate=@LastUpdateDate,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("VideoClassID=@VideoClassID,");
            builder.Append("IsRecomend=@IsRecomend,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("ThumbImageUrl=@ThumbImageUrl,");
            builder.Append("NormalImageUrl=@NormalImageUrl,");
            builder.Append("TotalTime=@TotalTime,");
            builder.Append("TotalComment=@TotalComment,");
            builder.Append("TotalFav=@TotalFav,");
            builder.Append("TotalUp=@TotalUp,");
            builder.Append("Reference=@Reference,");
            builder.Append("Tags=@Tags,");
            builder.Append("VideoUrl=@VideoUrl,");
            builder.Append("UrlType=@UrlType,");
            builder.Append("VideoFormat=@VideoFormat,");
            builder.Append("Domain=@Domain,");
            builder.Append("Grade=@Grade,");
            builder.Append("Attachment=@Attachment,");
            builder.Append("Privacy=@Privacy,");
            builder.Append("State=@State,");
            builder.Append("Remark=@Remark,");
            builder.Append("PvCount=@PvCount");
            builder.Append(" where VideoID=@VideoID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Title", SqlDbType.NVarChar, 200), new SqlParameter("@Description", SqlDbType.NText), new SqlParameter("@AlbumID", SqlDbType.Int, 4), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@LastUpdateUserID", SqlDbType.Int, 4), new SqlParameter("@LastUpdateDate", SqlDbType.DateTime), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@VideoClassID", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Bit, 1), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@TotalTime", SqlDbType.Int, 4), new SqlParameter("@TotalComment", SqlDbType.Int, 4), new SqlParameter("@TotalFav", SqlDbType.Int, 4), 
                new SqlParameter("@TotalUp", SqlDbType.Int, 4), new SqlParameter("@Reference", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@VideoUrl", SqlDbType.NVarChar), new SqlParameter("@UrlType", SqlDbType.NVarChar), new SqlParameter("@VideoFormat", SqlDbType.NVarChar, 50), new SqlParameter("@Domain", SqlDbType.NVarChar, 50), new SqlParameter("@Grade", SqlDbType.Int, 4), new SqlParameter("@Attachment", SqlDbType.NVarChar, 100), new SqlParameter("@Privacy", SqlDbType.SmallInt, 2), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@VideoID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.AlbumID;
            cmdParms[3].Value = model.CreatedUserID;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.LastUpdateUserID;
            cmdParms[6].Value = model.LastUpdateDate;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.VideoClassID;
            cmdParms[9].Value = model.IsRecomend;
            cmdParms[10].Value = model.ImageUrl;
            cmdParms[11].Value = model.ThumbImageUrl;
            cmdParms[12].Value = model.NormalImageUrl;
            cmdParms[13].Value = model.TotalTime;
            cmdParms[14].Value = model.TotalComment;
            cmdParms[15].Value = model.TotalFav;
            cmdParms[0x10].Value = model.TotalUp;
            cmdParms[0x11].Value = model.Reference;
            cmdParms[0x12].Value = model.Tags;
            cmdParms[0x13].Value = model.VideoUrl;
            cmdParms[20].Value = model.UrlType;
            cmdParms[0x15].Value = model.VideoFormat;
            cmdParms[0x16].Value = model.Domain;
            cmdParms[0x17].Value = model.Grade;
            cmdParms[0x18].Value = model.Attachment;
            cmdParms[0x19].Value = model.Privacy;
            cmdParms[0x1a].Value = model.State;
            cmdParms[0x1b].Value = model.Remark;
            cmdParms[0x1c].Value = model.PvCount;
            cmdParms[0x1d].Value = model.VideoID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Video set " + strWhere);
            builder.Append(" where VideoID in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }
    }
}


namespace Maticsoft.SQLServerDAL.CMS
{
    using Maticsoft.Common;
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.CMS;
    using Maticsoft.Model.CMS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Content : IContent
    {
        public int Add(Maticsoft.Model.CMS.Content model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into CMS_Content(");
            builder.Append("Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl)");
            builder.Append(" values (");
            builder.Append("@Title,@SubTitle,@Summary,@Description,@ImageUrl,@ThumbImageUrl,@NormalImageUrl,@CreatedDate,@CreatedUserID,@LastEditUserID,@LastEditDate,@LinkUrl,@PvCount,@State,@ClassID,@Keywords,@Sequence,@IsRecomend,@IsHot,@IsColor,@IsTop,@Attachment,@Remary,@TotalComment,@TotalSupport,@TotalFav,@TotalShare,@BeFrom,@FileName,@Meta_Title,@Meta_Description,@Meta_Keywords,@SeoUrl,@SeoImageAlt,@SeoImageTitle,@StaticUrl)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Title", SqlDbType.NVarChar, 50), new SqlParameter("@SubTitle", SqlDbType.NVarChar, 0xff), new SqlParameter("@Summary", SqlDbType.NText), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@LastEditUserID", SqlDbType.Int, 4), new SqlParameter("@LastEditDate", SqlDbType.DateTime), new SqlParameter("@LinkUrl", SqlDbType.NVarChar, 200), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@ClassID", SqlDbType.Int, 4), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), 
                new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Bit, 1), new SqlParameter("@IsHot", SqlDbType.Bit, 1), new SqlParameter("@IsColor", SqlDbType.Bit, 1), new SqlParameter("@IsTop", SqlDbType.Bit, 1), new SqlParameter("@Attachment", SqlDbType.NVarChar, 200), new SqlParameter("@Remary", SqlDbType.NVarChar, 200), new SqlParameter("@TotalComment", SqlDbType.Int, 4), new SqlParameter("@TotalSupport", SqlDbType.Int, 4), new SqlParameter("@TotalFav", SqlDbType.Int, 4), new SqlParameter("@TotalShare", SqlDbType.Int, 4), new SqlParameter("@BeFrom", SqlDbType.NVarChar, 50), new SqlParameter("@FileName", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 500)
             };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.SubTitle;
            cmdParms[2].Value = model.Summary;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.ImageUrl;
            cmdParms[5].Value = model.ThumbImageUrl;
            cmdParms[6].Value = model.NormalImageUrl;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.CreatedUserID;
            cmdParms[9].Value = model.LastEditUserID;
            cmdParms[10].Value = model.LastEditDate;
            cmdParms[11].Value = model.LinkUrl;
            cmdParms[12].Value = model.PvCount;
            cmdParms[13].Value = model.State;
            cmdParms[14].Value = model.ClassID;
            cmdParms[15].Value = model.Keywords;
            cmdParms[0x10].Value = model.Sequence;
            cmdParms[0x11].Value = model.IsRecomend;
            cmdParms[0x12].Value = model.IsHot;
            cmdParms[0x13].Value = model.IsColor;
            cmdParms[20].Value = model.IsTop;
            cmdParms[0x15].Value = model.Attachment;
            cmdParms[0x16].Value = model.Remary;
            cmdParms[0x17].Value = model.TotalComment;
            cmdParms[0x18].Value = model.TotalSupport;
            cmdParms[0x19].Value = model.TotalFav;
            cmdParms[0x1a].Value = model.TotalShare;
            cmdParms[0x1b].Value = model.BeFrom;
            cmdParms[0x1c].Value = model.FileName;
            cmdParms[0x1d].Value = model.Meta_Title;
            cmdParms[30].Value = model.Meta_Description;
            cmdParms[0x1f].Value = model.Meta_Keywords;
            cmdParms[0x20].Value = model.SeoUrl;
            cmdParms[0x21].Value = model.SeoImageAlt;
            cmdParms[0x22].Value = model.SeoImageTitle;
            cmdParms[0x23].Value = model.StaticUrl;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.CMS.Content DataRowToModel(DataRow row)
        {
            Maticsoft.Model.CMS.Content content = new Maticsoft.Model.CMS.Content();
            if (row != null)
            {
                if ((row["ContentID"] != null) && (row["ContentID"].ToString() != ""))
                {
                    content.ContentID = int.Parse(row["ContentID"].ToString());
                }
                if (row["Title"] != null)
                {
                    content.Title = row["Title"].ToString();
                }
                if (row["SubTitle"] != null)
                {
                    content.SubTitle = row["SubTitle"].ToString();
                }
                if (row["Summary"] != null)
                {
                    content.Summary = row["Summary"].ToString();
                }
                if (row["Description"] != null)
                {
                    content.Description = row["Description"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    content.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["ThumbImageUrl"] != null)
                {
                    content.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if (row["NormalImageUrl"] != null)
                {
                    content.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    content.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["CreatedUserID"] != null) && (row["CreatedUserID"].ToString() != ""))
                {
                    content.CreatedUserID = int.Parse(row["CreatedUserID"].ToString());
                }
                if ((row["LastEditUserID"] != null) && (row["LastEditUserID"].ToString() != ""))
                {
                    content.LastEditUserID = new int?(int.Parse(row["LastEditUserID"].ToString()));
                }
                if ((row["LastEditDate"] != null) && (row["LastEditDate"].ToString() != ""))
                {
                    content.LastEditDate = new DateTime?(DateTime.Parse(row["LastEditDate"].ToString()));
                }
                if (row["LinkUrl"] != null)
                {
                    content.LinkUrl = row["LinkUrl"].ToString();
                }
                if ((row["PvCount"] != null) && (row["PvCount"].ToString() != ""))
                {
                    content.PvCount = int.Parse(row["PvCount"].ToString());
                }
                if ((row["State"] != null) && (row["State"].ToString() != ""))
                {
                    content.State = int.Parse(row["State"].ToString());
                }
                if ((row["ClassID"] != null) && (row["ClassID"].ToString() != ""))
                {
                    content.ClassID = int.Parse(row["ClassID"].ToString());
                }
                if (row["Keywords"] != null)
                {
                    content.Keywords = row["Keywords"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    content.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if ((row["IsRecomend"] != null) && (row["IsRecomend"].ToString() != ""))
                {
                    if ((row["IsRecomend"].ToString() == "1") || (row["IsRecomend"].ToString().ToLower() == "true"))
                    {
                        content.IsRecomend = true;
                    }
                    else
                    {
                        content.IsRecomend = false;
                    }
                }
                if ((row["IsHot"] != null) && (row["IsHot"].ToString() != ""))
                {
                    if ((row["IsHot"].ToString() == "1") || (row["IsHot"].ToString().ToLower() == "true"))
                    {
                        content.IsHot = true;
                    }
                    else
                    {
                        content.IsHot = false;
                    }
                }
                if ((row["IsColor"] != null) && (row["IsColor"].ToString() != ""))
                {
                    if ((row["IsColor"].ToString() == "1") || (row["IsColor"].ToString().ToLower() == "true"))
                    {
                        content.IsColor = true;
                    }
                    else
                    {
                        content.IsColor = false;
                    }
                }
                if ((row["IsTop"] != null) && (row["IsTop"].ToString() != ""))
                {
                    if ((row["IsTop"].ToString() == "1") || (row["IsTop"].ToString().ToLower() == "true"))
                    {
                        content.IsTop = true;
                    }
                    else
                    {
                        content.IsTop = false;
                    }
                }
                if (row["Attachment"] != null)
                {
                    content.Attachment = row["Attachment"].ToString();
                }
                if (row["Remary"] != null)
                {
                    content.Remary = row["Remary"].ToString();
                }
                if ((row["TotalComment"] != null) && (row["TotalComment"].ToString() != ""))
                {
                    content.TotalComment = int.Parse(row["TotalComment"].ToString());
                }
                if ((row["TotalSupport"] != null) && (row["TotalSupport"].ToString() != ""))
                {
                    content.TotalSupport = int.Parse(row["TotalSupport"].ToString());
                }
                if ((row["TotalFav"] != null) && (row["TotalFav"].ToString() != ""))
                {
                    content.TotalFav = int.Parse(row["TotalFav"].ToString());
                }
                if ((row["TotalShare"] != null) && (row["TotalShare"].ToString() != ""))
                {
                    content.TotalShare = int.Parse(row["TotalShare"].ToString());
                }
                if (row["BeFrom"] != null)
                {
                    content.BeFrom = row["BeFrom"].ToString();
                }
                if (row["FileName"] != null)
                {
                    content.FileName = row["FileName"].ToString();
                }
                if (row["Meta_Title"] != null)
                {
                    content.Meta_Title = row["Meta_Title"].ToString();
                }
                if (row["Meta_Description"] != null)
                {
                    content.Meta_Description = row["Meta_Description"].ToString();
                }
                if (row["Meta_Keywords"] != null)
                {
                    content.Meta_Keywords = row["Meta_Keywords"].ToString();
                }
                if (row["SeoUrl"] != null)
                {
                    content.SeoUrl = row["SeoUrl"].ToString();
                }
                if (row["SeoImageAlt"] != null)
                {
                    content.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if (row["SeoImageTitle"] != null)
                {
                    content.SeoImageTitle = row["SeoImageTitle"].ToString();
                }
                if (row["StaticUrl"] != null)
                {
                    content.StaticUrl = row["StaticUrl"].ToString();
                }
            }
            return content;
        }

        public List<Maticsoft.Model.CMS.Content> DataTableToListEx(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Content> list = new List<Maticsoft.Model.CMS.Content>();
            if (DataTableTools.DataTableIsNull(dt))
            {
                return null;
            }
            int count = dt.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Maticsoft.Model.CMS.Content item = new Maticsoft.Model.CMS.Content();
                    if ((dt.Rows[i]["ContentID"] != null) && (dt.Rows[i]["ContentID"].ToString() != ""))
                    {
                        item.ContentID = int.Parse(dt.Rows[i]["ContentID"].ToString());
                    }
                    if ((dt.Rows[i]["Title"] != null) && (dt.Rows[i]["Title"].ToString() != ""))
                    {
                        item.Title = dt.Rows[i]["Title"].ToString();
                    }
                    if ((dt.Rows[i]["SubTitle"] != null) && (dt.Rows[i]["SubTitle"].ToString() != ""))
                    {
                        item.SubTitle = dt.Rows[i]["SubTitle"].ToString();
                    }
                    if ((dt.Rows[i]["Summary"] != null) && (dt.Rows[i]["Summary"].ToString() != ""))
                    {
                        item.Summary = dt.Rows[i]["Summary"].ToString();
                    }
                    if ((dt.Rows[i]["Description"] != null) && (dt.Rows[i]["Description"].ToString() != ""))
                    {
                        item.Description = dt.Rows[i]["Description"].ToString();
                    }
                    if ((dt.Rows[i]["ImageUrl"] != null) && (dt.Rows[i]["ImageUrl"].ToString() != ""))
                    {
                        item.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["ThumbImageUrl"] != null) && (dt.Rows[i]["ThumbImageUrl"].ToString() != ""))
                    {
                        item.ThumbImageUrl = dt.Rows[i]["ThumbImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["NormalImageUrl"] != null) && (dt.Rows[i]["NormalImageUrl"].ToString() != ""))
                    {
                        item.NormalImageUrl = dt.Rows[i]["NormalImageUrl"].ToString();
                    }
                    if ((dt.Rows[i]["CreatedDate"] != null) && (dt.Rows[i]["CreatedDate"].ToString() != ""))
                    {
                        item.CreatedDate = DateTime.Parse(dt.Rows[i]["CreatedDate"].ToString());
                    }
                    if ((dt.Rows[i]["CreatedUserID"] != null) && (dt.Rows[i]["CreatedUserID"].ToString() != ""))
                    {
                        item.CreatedUserID = int.Parse(dt.Rows[i]["CreatedUserID"].ToString());
                    }
                    if ((dt.Rows[i]["CreatedUserName"] != null) && (dt.Rows[i]["CreatedUserName"].ToString() != ""))
                    {
                        item.CreatedUserName = dt.Rows[i]["CreatedUserName"].ToString();
                    }
                    if ((dt.Rows[i]["LastEditUserID"] != null) && (dt.Rows[i]["LastEditUserID"].ToString() != ""))
                    {
                        item.LastEditUserID = new int?(int.Parse(dt.Rows[i]["LastEditUserID"].ToString()));
                    }
                    if ((dt.Rows[i]["LastEditDate"] != null) && (dt.Rows[i]["LastEditDate"].ToString() != ""))
                    {
                        item.LastEditDate = new DateTime?(DateTime.Parse(dt.Rows[i]["LastEditDate"].ToString()));
                    }
                    if ((dt.Rows[i]["LinkUrl"] != null) && (dt.Rows[i]["LinkUrl"].ToString() != ""))
                    {
                        item.LinkUrl = dt.Rows[i]["LinkUrl"].ToString();
                    }
                    if ((dt.Rows[i]["PvCount"] != null) && (dt.Rows[i]["PvCount"].ToString() != ""))
                    {
                        item.PvCount = int.Parse(dt.Rows[i]["PvCount"].ToString());
                    }
                    if ((dt.Rows[i]["State"] != null) && (dt.Rows[i]["State"].ToString() != ""))
                    {
                        item.State = int.Parse(dt.Rows[i]["State"].ToString());
                    }
                    if ((dt.Rows[i]["ClassID"] != null) && (dt.Rows[i]["ClassID"].ToString() != ""))
                    {
                        item.ClassID = int.Parse(dt.Rows[i]["ClassID"].ToString());
                    }
                    if ((dt.Rows[i]["ClassName"] != null) && (dt.Rows[i]["ClassName"].ToString() != ""))
                    {
                        item.ClassName = dt.Rows[i]["ClassName"].ToString();
                    }
                    if ((dt.Rows[i]["Keywords"] != null) && (dt.Rows[i]["Keywords"].ToString() != ""))
                    {
                        item.Keywords = dt.Rows[i]["Keywords"].ToString();
                    }
                    if ((dt.Rows[i]["Sequence"] != null) && (dt.Rows[i]["Sequence"].ToString() != ""))
                    {
                        item.Sequence = int.Parse(dt.Rows[i]["Sequence"].ToString());
                    }
                    if ((dt.Rows[i]["IsRecomend"] != null) && (dt.Rows[i]["IsRecomend"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsRecomend"].ToString() == "1") || (dt.Rows[i]["IsRecomend"].ToString().ToLower() == "true"))
                        {
                            item.IsRecomend = true;
                        }
                        else
                        {
                            item.IsRecomend = false;
                        }
                    }
                    if ((dt.Rows[i]["IsHot"] != null) && (dt.Rows[i]["IsHot"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsHot"].ToString() == "1") || (dt.Rows[i]["IsHot"].ToString().ToLower() == "true"))
                        {
                            item.IsHot = true;
                        }
                        else
                        {
                            item.IsHot = false;
                        }
                    }
                    if ((dt.Rows[i]["IsColor"] != null) && (dt.Rows[i]["IsColor"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsColor"].ToString() == "1") || (dt.Rows[i]["IsColor"].ToString().ToLower() == "true"))
                        {
                            item.IsColor = true;
                        }
                        else
                        {
                            item.IsColor = false;
                        }
                    }
                    if ((dt.Rows[i]["IsTop"] != null) && (dt.Rows[i]["IsTop"].ToString() != ""))
                    {
                        if ((dt.Rows[i]["IsTop"].ToString() == "1") || (dt.Rows[i]["IsTop"].ToString().ToLower() == "true"))
                        {
                            item.IsTop = true;
                        }
                        else
                        {
                            item.IsTop = false;
                        }
                    }
                    if ((dt.Rows[i]["Attachment"] != null) && (dt.Rows[i]["Attachment"].ToString() != ""))
                    {
                        item.Attachment = dt.Rows[i]["Attachment"].ToString();
                    }
                    if ((dt.Rows[i]["Remary"] != null) && (dt.Rows[i]["Remary"].ToString() != ""))
                    {
                        item.Remary = dt.Rows[i]["Remary"].ToString();
                    }
                    if ((dt.Rows[i]["TotalComment"] != null) && (dt.Rows[i]["TotalComment"].ToString() != ""))
                    {
                        item.TotalComment = int.Parse(dt.Rows[i]["TotalComment"].ToString());
                    }
                    if ((dt.Rows[i]["TotalSupport"] != null) && (dt.Rows[i]["TotalSupport"].ToString() != ""))
                    {
                        item.TotalSupport = int.Parse(dt.Rows[i]["TotalSupport"].ToString());
                    }
                    if ((dt.Rows[i]["TotalFav"] != null) && (dt.Rows[i]["TotalFav"].ToString() != ""))
                    {
                        item.TotalFav = int.Parse(dt.Rows[i]["TotalFav"].ToString());
                    }
                    if ((dt.Rows[i]["TotalShare"] != null) && (dt.Rows[i]["TotalShare"].ToString() != ""))
                    {
                        item.TotalShare = int.Parse(dt.Rows[i]["TotalShare"].ToString());
                    }
                    if (dt.Rows[i]["BeFrom"] != null)
                    {
                        item.BeFrom = dt.Rows[i]["BeFrom"].ToString();
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public bool Delete(int ContentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_Content ");
            builder.Append(" where ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ContentIDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from CMS_Content ");
            builder.Append(" where ContentID in (" + ContentIDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ContentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_Content");
            builder.Append(" where ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistsByClassID(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_Content");
            builder.Append(" where ClassID=@ClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool ExistTitle(string Title)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from CMS_Content");
            builder.Append(" where Title=@Title");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Title", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = Title;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetHotCom(int diffDate, int top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  * , TT.ComCount  FROM    CMS_Content  C ");
            builder.Append("  JOIN ( SELECT  ContentId, COUNT(1) AS ComCount FROM     CMS_Comment ");
            builder.AppendFormat(" WHERE    DATEDIFF(day, CreatedDate, GETDATE()) < {0} ", diffDate);
            builder.Append("  GROUP BY ContentId  ) TT ON TT.ContentId=C.ContentID ");
            builder.AppendFormat(" WHERE  State=0 and   EXISTS ( SELECT TOP {0} ContentId , COUNT(1) AS ComCount ", top);
            builder.Append(" FROM   CMS_Comment  WHERE  ContentId = C.ContentID ");
            builder.AppendFormat(" AND DATEDIFF(day, CreatedDate, GETDATE()) < {0} ", diffDate);
            builder.Append("  GROUP BY ContentId  ORDER BY ComCount DESC )");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl ");
            builder.Append(" FROM CMS_Content ");
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
            builder.Append(" ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl ");
            builder.Append(" FROM CMS_Content ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByItem(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" * FROM CMS_Content ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" ORDER BY " + filedOrder);
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
                builder.Append("order by T.ContentID desc");
            }
            builder.Append(")AS Row, T.*  from CMS_Content T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex)
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
                builder.Append("order by T.ContentID desc");
            }
            builder.Append(")AS Row, T.*,UserName as CreatedUserName,ClassName from CMS_Content T ");
            builder.Append(" LEFT JOIN Accounts_Users AS AU ON AU.UserID = T.CreatedUserID ");
            builder.Append(" LEFT JOIN CMS_ContentClass CMCC ON CMCC.ClassID = T.ClassID ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByView(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM View_Content ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY ContentID DESC");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByView(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" * FROM View_Content ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" WHERE " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" ORDER BY " + filedOrder);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl ");
            builder.Append(" FROM CMS_Content as cont ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ContentID", "CMS_Content");
        }

        public Maticsoft.Model.CMS.Content GetModel(int ContentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl from CMS_Content ");
            builder.Append(" where ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            new Maticsoft.Model.CMS.Content();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Maticsoft.Model.CMS.Content GetModelByClassID(int ClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ContentID,Title,SubTitle,Summary,Description,ImageUrl,ThumbImageUrl,NormalImageUrl,CreatedDate,CreatedUserID,LastEditUserID,LastEditDate,LinkUrl,PvCount,State,ClassID,Keywords,Sequence,IsRecomend,IsHot,IsColor,IsTop,Attachment,Remary,TotalComment,TotalSupport,TotalFav,TotalShare,BeFrom,FileName,Meta_Title,Meta_Description,Meta_Keywords,SeoUrl,SeoImageAlt,SeoImageTitle,StaticUrl from CMS_Content ");
            builder.Append(" where ClassID=@ClassID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ClassID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ClassID;
            new Maticsoft.Model.CMS.Content();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public Maticsoft.Model.CMS.Content GetModelEx(int ContentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 CMSS.*,UserName as CreatedUserName from CMS_Content CMSS LEFT JOIN Accounts_Users AS AU ON AU.UserID = CMSS.CreatedUserID ");
            builder.Append(" where State=0  AND ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            Maticsoft.Model.CMS.Content content = new Maticsoft.Model.CMS.Content();
            DataSet ds = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (DataSetTools.DataSetIsNull(ds))
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((ds.Tables[0].Rows[0]["ContentID"] != null) && (ds.Tables[0].Rows[0]["ContentID"].ToString() != ""))
            {
                content.ContentID = int.Parse(ds.Tables[0].Rows[0]["ContentID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Title"] != null) && (ds.Tables[0].Rows[0]["Title"].ToString() != ""))
            {
                content.Title = ds.Tables[0].Rows[0]["Title"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["SubTitle"] != null) && (ds.Tables[0].Rows[0]["SubTitle"].ToString() != ""))
            {
                content.SubTitle = ds.Tables[0].Rows[0]["SubTitle"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Summary"] != null) && (ds.Tables[0].Rows[0]["Summary"].ToString() != ""))
            {
                content.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Description"] != null) && (ds.Tables[0].Rows[0]["Description"].ToString() != ""))
            {
                content.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["ImageUrl"] != null) && (ds.Tables[0].Rows[0]["ImageUrl"].ToString() != ""))
            {
                content.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["ThumbImageUrl"] != null) && (ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString() != ""))
            {
                content.ThumbImageUrl = ds.Tables[0].Rows[0]["ThumbImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["NormalImageUrl"] != null) && (ds.Tables[0].Rows[0]["NormalImageUrl"].ToString() != ""))
            {
                content.NormalImageUrl = ds.Tables[0].Rows[0]["NormalImageUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CreatedDate"] != null) && (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                content.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserName"] != null) && (ds.Tables[0].Rows[0]["CreatedUserName"].ToString() != ""))
            {
                content.CreatedUserName = ds.Tables[0].Rows[0]["CreatedUserName"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["CreatedUserID"] != null) && (ds.Tables[0].Rows[0]["CreatedUserID"].ToString() != ""))
            {
                content.CreatedUserID = int.Parse(ds.Tables[0].Rows[0]["CreatedUserID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["LastEditUserID"] != null) && (ds.Tables[0].Rows[0]["LastEditUserID"].ToString() != ""))
            {
                content.LastEditUserID = new int?(int.Parse(ds.Tables[0].Rows[0]["LastEditUserID"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["LastEditDate"] != null) && (ds.Tables[0].Rows[0]["LastEditDate"].ToString() != ""))
            {
                content.LastEditDate = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["LastEditDate"].ToString()));
            }
            if ((ds.Tables[0].Rows[0]["LinkUrl"] != null) && (ds.Tables[0].Rows[0]["LinkUrl"].ToString() != ""))
            {
                content.LinkUrl = ds.Tables[0].Rows[0]["LinkUrl"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["PvCount"] != null) && (ds.Tables[0].Rows[0]["PvCount"].ToString() != ""))
            {
                content.PvCount = int.Parse(ds.Tables[0].Rows[0]["PvCount"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["State"] != null) && (ds.Tables[0].Rows[0]["State"].ToString() != ""))
            {
                content.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["ClassID"] != null) && (ds.Tables[0].Rows[0]["ClassID"].ToString() != ""))
            {
                content.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["Keywords"] != null) && (ds.Tables[0].Rows[0]["Keywords"].ToString() != ""))
            {
                content.Keywords = ds.Tables[0].Rows[0]["Keywords"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Sequence"] != null) && (ds.Tables[0].Rows[0]["Sequence"].ToString() != ""))
            {
                content.Sequence = int.Parse(ds.Tables[0].Rows[0]["Sequence"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["IsRecomend"] != null) && (ds.Tables[0].Rows[0]["IsRecomend"].ToString() != ""))
            {
                if ((ds.Tables[0].Rows[0]["IsRecomend"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsRecomend"].ToString().ToLower() == "true"))
                {
                    content.IsRecomend = true;
                }
                else
                {
                    content.IsRecomend = false;
                }
            }
            if ((ds.Tables[0].Rows[0]["IsHot"] != null) && (ds.Tables[0].Rows[0]["IsHot"].ToString() != ""))
            {
                if ((ds.Tables[0].Rows[0]["IsHot"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsHot"].ToString().ToLower() == "true"))
                {
                    content.IsHot = true;
                }
                else
                {
                    content.IsHot = false;
                }
            }
            if ((ds.Tables[0].Rows[0]["IsColor"] != null) && (ds.Tables[0].Rows[0]["IsColor"].ToString() != ""))
            {
                if ((ds.Tables[0].Rows[0]["IsColor"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsColor"].ToString().ToLower() == "true"))
                {
                    content.IsColor = true;
                }
                else
                {
                    content.IsColor = false;
                }
            }
            if ((ds.Tables[0].Rows[0]["IsTop"] != null) && (ds.Tables[0].Rows[0]["IsTop"].ToString() != ""))
            {
                if ((ds.Tables[0].Rows[0]["IsTop"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsTop"].ToString().ToLower() == "true"))
                {
                    content.IsTop = true;
                }
                else
                {
                    content.IsTop = false;
                }
            }
            if ((ds.Tables[0].Rows[0]["Attachment"] != null) && (ds.Tables[0].Rows[0]["Attachment"].ToString() != ""))
            {
                content.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["Remary"] != null) && (ds.Tables[0].Rows[0]["Remary"].ToString() != ""))
            {
                content.Remary = ds.Tables[0].Rows[0]["Remary"].ToString();
            }
            if ((ds.Tables[0].Rows[0]["TotalComment"] != null) && (ds.Tables[0].Rows[0]["TotalComment"].ToString() != ""))
            {
                content.TotalComment = int.Parse(ds.Tables[0].Rows[0]["TotalComment"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["TotalSupport"] != null) && (ds.Tables[0].Rows[0]["TotalSupport"].ToString() != ""))
            {
                content.TotalSupport = int.Parse(ds.Tables[0].Rows[0]["TotalSupport"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["TotalFav"] != null) && (ds.Tables[0].Rows[0]["TotalFav"].ToString() != ""))
            {
                content.TotalFav = int.Parse(ds.Tables[0].Rows[0]["TotalFav"].ToString());
            }
            if ((ds.Tables[0].Rows[0]["TotalShare"] != null) && (ds.Tables[0].Rows[0]["TotalShare"].ToString() != ""))
            {
                content.TotalShare = int.Parse(ds.Tables[0].Rows[0]["TotalShare"].ToString());
            }
            if (ds.Tables[0].Rows[0]["BeFrom"] != null)
            {
                content.BeFrom = ds.Tables[0].Rows[0]["BeFrom"].ToString();
            }
            return content;
        }

        public int GetNextID(int ContentID, int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MIN(ContentID) from CMS_Content ");
            builder.Append(" where State=0 ");
            if (ClassId > 0)
            {
                builder.Append("  and ClassID=@ClassId");
            }
            builder.Append("  AND ContentID>@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            cmdParms[1].Value = ClassId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public int GetPrevID(int ContentID, int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MAX(ContentID) from CMS_Content ");
            builder.Append(" where State=0 ");
            if (ClassId > 0)
            {
                builder.Append("  and ClassID=@ClassId");
            }
            builder.Append(" AND ContentID<@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4), new SqlParameter("@ClassId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            cmdParms[1].Value = ClassId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public DataSet GetRanList(int ClassID, string keyword, int Top)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" *  FROM CMS_Content  where  State=0 ");
            if (ClassID > 0)
            {
                builder.Append(" and  EXISTS ( SELECT ClassID FROM   CMS_ContentClass WHERE  dbo.CMS_ContentClass.ClassID = CMS_Content.ClassID ");
                builder.AppendFormat("  AND ( ClassID = {0} OR Path LIKE ( SELECT Path  FROM   CMS_ContentClass  WHERE  ClassID = {0} ) + '|%'  ) ) ", ClassID);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                builder.AppendFormat(" AND Title LIKE '%{0}%' ", keyword);
            }
            builder.Append(" order by  NEWID()");
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_Content ");
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

        public int GetRecordCount4Menu(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_Content T");
            builder.Append(" LEFT JOIN CMS_ContentClass CMCC ON CMCC.ClassID = T.ClassID ");
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

        public int GetRecordCountEx(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM CMS_Content as T ");
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

        public bool SetColor(int id, bool rec)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsColor=@IsColor ");
            builder.Append(" where ContentID=@ContentID  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsColor", SqlDbType.Bit, 1), new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = rec;
            cmdParms[1].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool SetColorList(string ids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsColor=1 ");
            builder.Append(" where ContentID in(" + ids + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool SetHot(int id, bool rec)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsHot=@IsHot ");
            builder.Append(" where ContentID=@ContentID  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsHot", SqlDbType.Bit, 1), new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = rec;
            cmdParms[1].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool SetHotList(string ids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsHot=1 ");
            builder.Append(" where ContentID in(" + ids + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool SetRec(int id, bool rec)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsRecomend=@IsRecomend ");
            builder.Append(" where ContentID=@ContentID  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsRecomend", SqlDbType.Bit, 1), new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = rec;
            cmdParms[1].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool SetRecList(string ids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsRecomend=1 ");
            builder.Append(" where ContentID in(" + ids + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool SetTop(int id, bool rec)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsTop=@IsTop ");
            builder.Append(" where ContentID=@ContentID  ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@IsTop", SqlDbType.Bit, 1), new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = rec;
            cmdParms[1].Value = id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool SetTopList(string ids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set IsTop=1 ");
            builder.Append(" where ContentID in(" + ids + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Update(Maticsoft.Model.CMS.Content model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set ");
            builder.Append("Title=@Title,");
            builder.Append("SubTitle=@SubTitle,");
            builder.Append("Summary=@Summary,");
            builder.Append("Description=@Description,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("ThumbImageUrl=@ThumbImageUrl,");
            builder.Append("NormalImageUrl=@NormalImageUrl,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("CreatedUserID=@CreatedUserID,");
            builder.Append("LastEditUserID=@LastEditUserID,");
            builder.Append("LastEditDate=@LastEditDate,");
            builder.Append("LinkUrl=@LinkUrl,");
            builder.Append("PvCount=@PvCount,");
            builder.Append("State=@State,");
            builder.Append("ClassID=@ClassID,");
            builder.Append("Keywords=@Keywords,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("IsRecomend=@IsRecomend,");
            builder.Append("IsHot=@IsHot,");
            builder.Append("IsColor=@IsColor,");
            builder.Append("IsTop=@IsTop,");
            builder.Append("Attachment=@Attachment,");
            builder.Append("Remary=@Remary,");
            builder.Append("TotalComment=@TotalComment,");
            builder.Append("TotalSupport=@TotalSupport,");
            builder.Append("TotalFav=@TotalFav,");
            builder.Append("TotalShare=@TotalShare,");
            builder.Append("BeFrom=@BeFrom,");
            builder.Append("FileName=@FileName,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_Keywords=@Meta_Keywords,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("SeoImageAlt=@SeoImageAlt,");
            builder.Append("SeoImageTitle=@SeoImageTitle,");
            builder.Append("StaticUrl=@StaticUrl");
            builder.Append(" where ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Title", SqlDbType.NVarChar, 50), new SqlParameter("@SubTitle", SqlDbType.NVarChar, 0xff), new SqlParameter("@Summary", SqlDbType.NText), new SqlParameter("@Description", SqlDbType.Text), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@CreatedUserID", SqlDbType.Int, 4), new SqlParameter("@LastEditUserID", SqlDbType.Int, 4), new SqlParameter("@LastEditDate", SqlDbType.DateTime), new SqlParameter("@LinkUrl", SqlDbType.NVarChar, 200), new SqlParameter("@PvCount", SqlDbType.Int, 4), new SqlParameter("@State", SqlDbType.SmallInt, 2), new SqlParameter("@ClassID", SqlDbType.Int, 4), new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), 
                new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@IsRecomend", SqlDbType.Bit, 1), new SqlParameter("@IsHot", SqlDbType.Bit, 1), new SqlParameter("@IsColor", SqlDbType.Bit, 1), new SqlParameter("@IsTop", SqlDbType.Bit, 1), new SqlParameter("@Attachment", SqlDbType.NVarChar, 200), new SqlParameter("@Remary", SqlDbType.NVarChar, 200), new SqlParameter("@TotalComment", SqlDbType.Int, 4), new SqlParameter("@TotalSupport", SqlDbType.Int, 4), new SqlParameter("@TotalFav", SqlDbType.Int, 4), new SqlParameter("@TotalShare", SqlDbType.Int, 4), new SqlParameter("@BeFrom", SqlDbType.NVarChar, 50), new SqlParameter("@FileName", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Keywords", SqlDbType.NVarChar, 0x3e8), 
                new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@StaticUrl", SqlDbType.NVarChar, 500), new SqlParameter("@ContentID", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Title;
            cmdParms[1].Value = model.SubTitle;
            cmdParms[2].Value = model.Summary;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.ImageUrl;
            cmdParms[5].Value = model.ThumbImageUrl;
            cmdParms[6].Value = model.NormalImageUrl;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.CreatedUserID;
            cmdParms[9].Value = model.LastEditUserID;
            cmdParms[10].Value = model.LastEditDate;
            cmdParms[11].Value = model.LinkUrl;
            cmdParms[12].Value = model.PvCount;
            cmdParms[13].Value = model.State;
            cmdParms[14].Value = model.ClassID;
            cmdParms[15].Value = model.Keywords;
            cmdParms[0x10].Value = model.Sequence;
            cmdParms[0x11].Value = model.IsRecomend;
            cmdParms[0x12].Value = model.IsHot;
            cmdParms[0x13].Value = model.IsColor;
            cmdParms[20].Value = model.IsTop;
            cmdParms[0x15].Value = model.Attachment;
            cmdParms[0x16].Value = model.Remary;
            cmdParms[0x17].Value = model.TotalComment;
            cmdParms[0x18].Value = model.TotalSupport;
            cmdParms[0x19].Value = model.TotalFav;
            cmdParms[0x1a].Value = model.TotalShare;
            cmdParms[0x1b].Value = model.BeFrom;
            cmdParms[0x1c].Value = model.FileName;
            cmdParms[0x1d].Value = model.Meta_Title;
            cmdParms[30].Value = model.Meta_Description;
            cmdParms[0x1f].Value = model.Meta_Keywords;
            cmdParms[0x20].Value = model.SeoUrl;
            cmdParms[0x21].Value = model.SeoImageAlt;
            cmdParms[0x22].Value = model.SeoImageTitle;
            cmdParms[0x23].Value = model.StaticUrl;
            cmdParms[0x24].Value = model.ContentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateFav(int ContentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set ");
            builder.Append(" TotalFav=TotalFav+1");
            builder.Append(" where ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool UpdateList(string IDlist, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set " + strWhere);
            builder.Append(" where ContentID in(" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public int UpdatePV(int ContentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set ");
            builder.Append(" PvCount=PvCount+1");
            builder.Append(" where  ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            if (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0)
            {
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("select PvCount from  CMS_Content  ");
                builder2.Append(" where  ContentID=@ContentID");
                SqlParameter[] parameterArray2 = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
                parameterArray2[0].Value = ContentID;
                return Convert.ToInt32(DbHelperSQL.GetSingle(builder2.ToString(), parameterArray2));
            }
            return 0;
        }

        public bool UpdateTotalSupport(int ContentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update CMS_Content set ");
            builder.Append(" TotalSupport=TotalSupport+1");
            builder.Append(" where ContentID=@ContentID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ContentID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ContentID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


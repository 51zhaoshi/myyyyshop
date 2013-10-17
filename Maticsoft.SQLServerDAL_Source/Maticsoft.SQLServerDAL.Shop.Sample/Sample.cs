namespace Maticsoft.SQLServerDAL.Shop.Sample
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Sample;
    using Maticsoft.Model.Shop.Sample;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Sample : ISample
    {
        public int Add(Maticsoft.Model.Shop.Sample.Sample model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Sample(");
            builder.Append("Tiltle,ElecCoverImageUrl,NormalElecCoverImageUrl,ThumblElecCoverImageUrl,PdfCoverImageUrl,NormalPdfCoverImageUrl,ThumbPdfCoverImageUrl,Sequence,Status,CreatedDate,Remark,Meta_Title,Meta_Description,Meta_KeyWords,SeoUrl,SeoImageAlt,SeoImageTitle)");
            builder.Append(" values (");
            builder.Append("@Tiltle,@ElecCoverImageUrl,@NormalElecCoverImageUrl,@ThumblElecCoverImageUrl,@PdfCoverImageUrl,@NormalPdfCoverImageUrl,@ThumbPdfCoverImageUrl,@Sequence,@Status,@CreatedDate,@Remark,@Meta_Title,@Meta_Description,@Meta_KeyWords,@SeoUrl,@SeoImageAlt,@SeoImageTitle)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Tiltle", SqlDbType.NVarChar, 200), new SqlParameter("@ElecCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalElecCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumblElecCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@PdfCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalPdfCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbPdfCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_KeyWords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), 
                new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300)
             };
            cmdParms[0].Value = model.Tiltle;
            cmdParms[1].Value = model.ElecCoverImageUrl;
            cmdParms[2].Value = model.NormalElecCoverImageUrl;
            cmdParms[3].Value = model.ThumblElecCoverImageUrl;
            cmdParms[4].Value = model.PdfCoverImageUrl;
            cmdParms[5].Value = model.NormalPdfCoverImageUrl;
            cmdParms[6].Value = model.ThumbPdfCoverImageUrl;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.Status;
            cmdParms[9].Value = model.CreatedDate;
            cmdParms[10].Value = model.Remark;
            cmdParms[11].Value = model.Meta_Title;
            cmdParms[12].Value = model.Meta_Description;
            cmdParms[13].Value = model.Meta_KeyWords;
            cmdParms[14].Value = model.SeoUrl;
            cmdParms[15].Value = model.SeoImageAlt;
            cmdParms[0x10].Value = model.SeoImageTitle;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Sample.Sample DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Sample.Sample sample = new Maticsoft.Model.Shop.Sample.Sample();
            if (row != null)
            {
                if ((row["SampleId"] != null) && (row["SampleId"].ToString() != ""))
                {
                    sample.SampleId = int.Parse(row["SampleId"].ToString());
                }
                if ((row["Tiltle"] != null) && (row["Tiltle"].ToString() != ""))
                {
                    sample.Tiltle = row["Tiltle"].ToString();
                }
                if ((row["ElecCoverImageUrl"] != null) && (row["ElecCoverImageUrl"].ToString() != ""))
                {
                    sample.ElecCoverImageUrl = row["ElecCoverImageUrl"].ToString();
                }
                if ((row["NormalElecCoverImageUrl"] != null) && (row["NormalElecCoverImageUrl"].ToString() != ""))
                {
                    sample.NormalElecCoverImageUrl = row["NormalElecCoverImageUrl"].ToString();
                }
                if ((row["ThumblElecCoverImageUrl"] != null) && (row["ThumblElecCoverImageUrl"].ToString() != ""))
                {
                    sample.ThumblElecCoverImageUrl = row["ThumblElecCoverImageUrl"].ToString();
                }
                if ((row["PdfCoverImageUrl"] != null) && (row["PdfCoverImageUrl"].ToString() != ""))
                {
                    sample.PdfCoverImageUrl = row["PdfCoverImageUrl"].ToString();
                }
                if ((row["NormalPdfCoverImageUrl"] != null) && (row["NormalPdfCoverImageUrl"].ToString() != ""))
                {
                    sample.NormalPdfCoverImageUrl = row["NormalPdfCoverImageUrl"].ToString();
                }
                if ((row["ThumbPdfCoverImageUrl"] != null) && (row["ThumbPdfCoverImageUrl"].ToString() != ""))
                {
                    sample.ThumbPdfCoverImageUrl = row["ThumbPdfCoverImageUrl"].ToString();
                }
                if ((row["Sequence"] != null) && (row["Sequence"].ToString() != ""))
                {
                    sample.Sequence = new int?(int.Parse(row["Sequence"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    sample.Status = new int?(int.Parse(row["Status"].ToString()));
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    sample.CreatedDate = new DateTime?(DateTime.Parse(row["CreatedDate"].ToString()));
                }
                if ((row["Remark"] != null) && (row["Remark"].ToString() != ""))
                {
                    sample.Remark = row["Remark"].ToString();
                }
                if ((row["Meta_Title"] != null) && (row["Meta_Title"].ToString() != ""))
                {
                    sample.Meta_Title = row["Meta_Title"].ToString();
                }
                if ((row["Meta_Description"] != null) && (row["Meta_Description"].ToString() != ""))
                {
                    sample.Meta_Description = row["Meta_Description"].ToString();
                }
                if ((row["Meta_KeyWords"] != null) && (row["Meta_KeyWords"].ToString() != ""))
                {
                    sample.Meta_KeyWords = row["Meta_KeyWords"].ToString();
                }
                if ((row["SeoUrl"] != null) && (row["SeoUrl"].ToString() != ""))
                {
                    sample.SeoUrl = row["SeoUrl"].ToString();
                }
                if ((row["SeoImageAlt"] != null) && (row["SeoImageAlt"].ToString() != ""))
                {
                    sample.SeoImageAlt = row["SeoImageAlt"].ToString();
                }
                if ((row["SeoImageTitle"] != null) && (row["SeoImageTitle"].ToString() != ""))
                {
                    sample.SeoImageTitle = row["SeoImageTitle"].ToString();
                }
            }
            return sample;
        }

        public bool Delete(int SampleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Sample ");
            builder.Append(" where SampleId=@SampleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SampleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SampleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string SampleIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Sample ");
            builder.Append(" where SampleId in (" + SampleIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int SampleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Sample");
            builder.Append(" where SampleId=@SampleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SampleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SampleId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select SampleId,Tiltle,ElecCoverImageUrl,NormalElecCoverImageUrl,ThumblElecCoverImageUrl,PdfCoverImageUrl,NormalPdfCoverImageUrl,ThumbPdfCoverImageUrl,Sequence,Status,CreatedDate,Remark,Meta_Title,Meta_Description,Meta_KeyWords,SeoUrl,SeoImageAlt,SeoImageTitle ");
            builder.Append(" FROM Shop_Sample ");
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
            builder.Append(" SampleId,Tiltle,ElecCoverImageUrl,NormalElecCoverImageUrl,ThumblElecCoverImageUrl,PdfCoverImageUrl,NormalPdfCoverImageUrl,ThumbPdfCoverImageUrl,Sequence,Status,CreatedDate,Remark,Meta_Title,Meta_Description,Meta_KeyWords,SeoUrl,SeoImageAlt,SeoImageTitle ");
            builder.Append(" FROM Shop_Sample ");
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
                builder.Append("order by T.SampleId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Sample T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SampleId", "Shop_Sample");
        }

        public Maticsoft.Model.Shop.Sample.Sample GetModel(int SampleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 SampleId,Tiltle,ElecCoverImageUrl,NormalElecCoverImageUrl,ThumblElecCoverImageUrl,PdfCoverImageUrl,NormalPdfCoverImageUrl,ThumbPdfCoverImageUrl,Sequence,Status,CreatedDate,Remark,Meta_Title,Meta_Description,Meta_KeyWords,SeoUrl,SeoImageAlt,SeoImageTitle from Shop_Sample ");
            builder.Append(" where SampleId=@SampleId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SampleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = SampleId;
            new Maticsoft.Model.Shop.Sample.Sample();
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
            builder.Append("select count(1) FROM Shop_Sample ");
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

        public bool Update(Maticsoft.Model.Shop.Sample.Sample model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Sample set ");
            builder.Append("Tiltle=@Tiltle,");
            builder.Append("ElecCoverImageUrl=@ElecCoverImageUrl,");
            builder.Append("NormalElecCoverImageUrl=@NormalElecCoverImageUrl,");
            builder.Append("ThumblElecCoverImageUrl=@ThumblElecCoverImageUrl,");
            builder.Append("PdfCoverImageUrl=@PdfCoverImageUrl,");
            builder.Append("NormalPdfCoverImageUrl=@NormalPdfCoverImageUrl,");
            builder.Append("ThumbPdfCoverImageUrl=@ThumbPdfCoverImageUrl,");
            builder.Append("Sequence=@Sequence,");
            builder.Append("Status=@Status,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Remark=@Remark,");
            builder.Append("Meta_Title=@Meta_Title,");
            builder.Append("Meta_Description=@Meta_Description,");
            builder.Append("Meta_KeyWords=@Meta_KeyWords,");
            builder.Append("SeoUrl=@SeoUrl,");
            builder.Append("SeoImageAlt=@SeoImageAlt,");
            builder.Append("SeoImageTitle=@SeoImageTitle");
            builder.Append(" where SampleId=@SampleId");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Tiltle", SqlDbType.NVarChar, 200), new SqlParameter("@ElecCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalElecCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumblElecCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@PdfCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@NormalPdfCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@ThumbPdfCoverImageUrl", SqlDbType.NVarChar, 200), new SqlParameter("@Sequence", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 200), new SqlParameter("@Meta_Title", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@Meta_KeyWords", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@SeoUrl", SqlDbType.NVarChar, 300), new SqlParameter("@SeoImageAlt", SqlDbType.NVarChar, 300), 
                new SqlParameter("@SeoImageTitle", SqlDbType.NVarChar, 300), new SqlParameter("@SampleId", SqlDbType.Int, 4)
             };
            cmdParms[0].Value = model.Tiltle;
            cmdParms[1].Value = model.ElecCoverImageUrl;
            cmdParms[2].Value = model.NormalElecCoverImageUrl;
            cmdParms[3].Value = model.ThumblElecCoverImageUrl;
            cmdParms[4].Value = model.PdfCoverImageUrl;
            cmdParms[5].Value = model.NormalPdfCoverImageUrl;
            cmdParms[6].Value = model.ThumbPdfCoverImageUrl;
            cmdParms[7].Value = model.Sequence;
            cmdParms[8].Value = model.Status;
            cmdParms[9].Value = model.CreatedDate;
            cmdParms[10].Value = model.Remark;
            cmdParms[11].Value = model.Meta_Title;
            cmdParms[12].Value = model.Meta_Description;
            cmdParms[13].Value = model.Meta_KeyWords;
            cmdParms[14].Value = model.SeoUrl;
            cmdParms[15].Value = model.SeoImageAlt;
            cmdParms[0x10].Value = model.SeoImageTitle;
            cmdParms[0x11].Value = model.SampleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


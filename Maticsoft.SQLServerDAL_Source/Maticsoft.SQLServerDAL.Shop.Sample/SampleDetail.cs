namespace Maticsoft.SQLServerDAL.Shop.Sample
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Sample;
    using Maticsoft.Model.Shop.Sample;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SampleDetail : ISampleDetail
    {
        public int Add(Maticsoft.Model.Shop.Sample.SampleDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SampleDetail(");
            builder.Append("SampleId,Title,Type,ImageUrl,NormalImageUrl,ThumbImageUrl,PdfUrl,CreatedDate,Status,Remark)");
            builder.Append(" values (");
            builder.Append("@SampleId,@Title,@Type,@ImageUrl,@NormalImageUrl,@ThumbImageUrl,@PdfUrl,@CreatedDate,@Status,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SampleId", SqlDbType.Int, 4), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@PdfUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 300) };
            cmdParms[0].Value = model.SampleId;
            cmdParms[1].Value = model.Title;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.ImageUrl;
            cmdParms[4].Value = model.NormalImageUrl;
            cmdParms[5].Value = model.ThumbImageUrl;
            cmdParms[6].Value = model.PdfUrl;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.Status;
            cmdParms[9].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Sample.SampleDetail DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Sample.SampleDetail detail = new Maticsoft.Model.Shop.Sample.SampleDetail();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    detail.ID = int.Parse(row["ID"].ToString());
                }
                if ((row["SampleId"] != null) && (row["SampleId"].ToString() != ""))
                {
                    detail.SampleId = int.Parse(row["SampleId"].ToString());
                }
                if ((row["Title"] != null) && (row["Title"].ToString() != ""))
                {
                    detail.Title = row["Title"].ToString();
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    detail.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["ImageUrl"] != null) && (row["ImageUrl"].ToString() != ""))
                {
                    detail.ImageUrl = row["ImageUrl"].ToString();
                }
                if ((row["NormalImageUrl"] != null) && (row["NormalImageUrl"].ToString() != ""))
                {
                    detail.NormalImageUrl = row["NormalImageUrl"].ToString();
                }
                if ((row["ThumbImageUrl"] != null) && (row["ThumbImageUrl"].ToString() != ""))
                {
                    detail.ThumbImageUrl = row["ThumbImageUrl"].ToString();
                }
                if ((row["PdfUrl"] != null) && (row["PdfUrl"].ToString() != ""))
                {
                    detail.PdfUrl = row["PdfUrl"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    detail.CreatedDate = new DateTime?(DateTime.Parse(row["CreatedDate"].ToString()));
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    detail.Status = new int?(int.Parse(row["Status"].ToString()));
                }
                if ((row["Remark"] != null) && (row["Remark"].ToString() != ""))
                {
                    detail.Remark = row["Remark"].ToString();
                }
            }
            return detail;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SampleDetail ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SampleDetail ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SampleDetail");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,SampleId,Title,Type,ImageUrl,NormalImageUrl,ThumbImageUrl,PdfUrl,CreatedDate,Status,Remark ");
            builder.Append(" FROM Shop_SampleDetail ");
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
            builder.Append(" ID,SampleId,Title,Type,ImageUrl,NormalImageUrl,ThumbImageUrl,PdfUrl,CreatedDate,Status,Remark ");
            builder.Append(" FROM Shop_SampleDetail ");
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
                builder.Append("order by T.ID desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SampleDetail T ");
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
            return DbHelperSQL.GetMaxID("ID", "Shop_SampleDetail");
        }

        public Maticsoft.Model.Shop.Sample.SampleDetail GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,SampleId,Title,Type,ImageUrl,NormalImageUrl,ThumbImageUrl,PdfUrl,CreatedDate,Status,Remark from Shop_SampleDetail ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.Shop.Sample.SampleDetail();
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
            builder.Append("select count(1) FROM Shop_SampleDetail ");
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

        public bool Update(Maticsoft.Model.Shop.Sample.SampleDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SampleDetail set ");
            builder.Append("SampleId=@SampleId,");
            builder.Append("Title=@Title,");
            builder.Append("Type=@Type,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("NormalImageUrl=@NormalImageUrl,");
            builder.Append("ThumbImageUrl=@ThumbImageUrl,");
            builder.Append("PdfUrl=@PdfUrl,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("Status=@Status,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@SampleId", SqlDbType.Int, 4), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@NormalImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@ThumbImageUrl", SqlDbType.NVarChar, 300), new SqlParameter("@PdfUrl", SqlDbType.NVarChar, 300), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@Status", SqlDbType.SmallInt, 2), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.SampleId;
            cmdParms[1].Value = model.Title;
            cmdParms[2].Value = model.Type;
            cmdParms[3].Value = model.ImageUrl;
            cmdParms[4].Value = model.NormalImageUrl;
            cmdParms[5].Value = model.ThumbImageUrl;
            cmdParms[6].Value = model.PdfUrl;
            cmdParms[7].Value = model.CreatedDate;
            cmdParms[8].Value = model.Status;
            cmdParms[9].Value = model.Remark;
            cmdParms[10].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


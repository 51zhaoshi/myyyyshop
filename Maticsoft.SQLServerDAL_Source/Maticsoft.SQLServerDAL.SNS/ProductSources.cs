namespace Maticsoft.SQLServerDAL.SNS
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.SNS;
    using Maticsoft.Model.SNS;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ProductSources : IProductSources
    {
        public int Add(Maticsoft.Model.SNS.ProductSources model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SNS_ProductSources(");
            builder.Append("WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status)");
            builder.Append(" values (");
            builder.Append("@WebSiteName,@WebSiteUrl,@WebSiteLogo,@CategoryTags,@PriceTags,@ImagesTag,@Status)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WebSiteName", SqlDbType.NVarChar, 100), new SqlParameter("@WebSiteUrl", SqlDbType.NVarChar, 200), new SqlParameter("@WebSiteLogo", SqlDbType.NVarChar, 200), new SqlParameter("@CategoryTags", SqlDbType.NVarChar), new SqlParameter("@PriceTags", SqlDbType.NVarChar), new SqlParameter("@ImagesTag", SqlDbType.NVarChar), new SqlParameter("@Status", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.WebSiteName;
            cmdParms[1].Value = model.WebSiteUrl;
            cmdParms[2].Value = model.WebSiteLogo;
            cmdParms[3].Value = model.CategoryTags;
            cmdParms[4].Value = model.PriceTags;
            cmdParms[5].Value = model.ImagesTag;
            cmdParms[6].Value = model.Status;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.SNS.ProductSources DataRowToModel(DataRow row)
        {
            Maticsoft.Model.SNS.ProductSources sources = new Maticsoft.Model.SNS.ProductSources();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    sources.ID = int.Parse(row["ID"].ToString());
                }
                if (row["WebSiteName"] != null)
                {
                    sources.WebSiteName = row["WebSiteName"].ToString();
                }
                if (row["WebSiteUrl"] != null)
                {
                    sources.WebSiteUrl = row["WebSiteUrl"].ToString();
                }
                if (row["WebSiteLogo"] != null)
                {
                    sources.WebSiteLogo = row["WebSiteLogo"].ToString();
                }
                if (row["CategoryTags"] != null)
                {
                    sources.CategoryTags = row["CategoryTags"].ToString();
                }
                if (row["PriceTags"] != null)
                {
                    sources.PriceTags = row["PriceTags"].ToString();
                }
                if (row["ImagesTag"] != null)
                {
                    sources.ImagesTag = row["ImagesTag"].ToString();
                }
                if ((row["Status"] != null) && (row["Status"].ToString() != ""))
                {
                    sources.Status = int.Parse(row["Status"].ToString());
                }
            }
            return sources;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_ProductSources ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SNS_ProductSources ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(string WebSiteName, string WebSiteUrl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SNS_ProductSources");
            builder.Append(" where WebSiteName=@WebSiteName or WebSiteUrl=@WebSiteUrl");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WebSiteName", SqlDbType.NVarChar, 100), new SqlParameter("@WebSiteUrl", SqlDbType.NVarChar, 200) };
            cmdParms[0].Value = WebSiteName;
            cmdParms[1].Value = WebSiteUrl;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status ");
            builder.Append(" FROM SNS_ProductSources ");
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
            builder.Append(" ID,WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status ");
            builder.Append(" FROM SNS_ProductSources ");
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
            builder.Append(")AS Row, T.*  from SNS_ProductSources T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.SNS.ProductSources GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,WebSiteName,WebSiteUrl,WebSiteLogo,CategoryTags,PriceTags,ImagesTag,Status from SNS_ProductSources ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.SNS.ProductSources();
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
            builder.Append("select count(1) FROM SNS_ProductSources ");
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

        public bool Update(Maticsoft.Model.SNS.ProductSources model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SNS_ProductSources set ");
            builder.Append("WebSiteName=@WebSiteName,");
            builder.Append("WebSiteUrl=@WebSiteUrl,");
            builder.Append("WebSiteLogo=@WebSiteLogo,");
            builder.Append("CategoryTags=@CategoryTags,");
            builder.Append("PriceTags=@PriceTags,");
            builder.Append("ImagesTag=@ImagesTag,");
            builder.Append("Status=@Status");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@WebSiteName", SqlDbType.NVarChar, 100), new SqlParameter("@WebSiteUrl", SqlDbType.NVarChar, 200), new SqlParameter("@WebSiteLogo", SqlDbType.NVarChar, 200), new SqlParameter("@CategoryTags", SqlDbType.NVarChar), new SqlParameter("@PriceTags", SqlDbType.NVarChar), new SqlParameter("@ImagesTag", SqlDbType.NVarChar), new SqlParameter("@Status", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.WebSiteName;
            cmdParms[1].Value = model.WebSiteUrl;
            cmdParms[2].Value = model.WebSiteLogo;
            cmdParms[3].Value = model.CategoryTags;
            cmdParms[4].Value = model.PriceTags;
            cmdParms[5].Value = model.ImagesTag;
            cmdParms[6].Value = model.Status;
            cmdParms[7].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


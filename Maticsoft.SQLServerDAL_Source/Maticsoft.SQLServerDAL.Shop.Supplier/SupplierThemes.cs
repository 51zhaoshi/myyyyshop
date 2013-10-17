namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SupplierThemes : ISupplierThemes
    {
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierThemes model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SupplierThemes(");
            builder.Append("Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark)");
            builder.Append(" values (");
            builder.Append("@Name,@Description,@ImageUrl,@Author,@WebSite,@Language,@CreatedDate,@UpdatedDate,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 100), new SqlParameter("@Author", SqlDbType.NVarChar, 100), new SqlParameter("@WebSite", SqlDbType.NVarChar, 200), new SqlParameter("@Language", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.ImageUrl;
            cmdParms[3].Value = model.Author;
            cmdParms[4].Value = model.WebSite;
            cmdParms[5].Value = model.Language;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.UpdatedDate;
            cmdParms[8].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierThemes DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierThemes themes = new Maticsoft.Model.Shop.Supplier.SupplierThemes();
            if (row != null)
            {
                if ((row["ThemeId"] != null) && (row["ThemeId"].ToString() != ""))
                {
                    themes.ThemeId = int.Parse(row["ThemeId"].ToString());
                }
                if (row["Name"] != null)
                {
                    themes.Name = row["Name"].ToString();
                }
                if (row["Description"] != null)
                {
                    themes.Description = row["Description"].ToString();
                }
                if (row["ImageUrl"] != null)
                {
                    themes.ImageUrl = row["ImageUrl"].ToString();
                }
                if (row["Author"] != null)
                {
                    themes.Author = row["Author"].ToString();
                }
                if (row["WebSite"] != null)
                {
                    themes.WebSite = row["WebSite"].ToString();
                }
                if (row["Language"] != null)
                {
                    themes.Language = row["Language"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    themes.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if ((row["UpdatedDate"] != null) && (row["UpdatedDate"].ToString() != ""))
                {
                    themes.UpdatedDate = new DateTime?(DateTime.Parse(row["UpdatedDate"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    themes.Remark = row["Remark"].ToString();
                }
            }
            return themes;
        }

        public bool Delete(int ThemeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierThemes ");
            builder.Append(" where ThemeId=@ThemeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ThemeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ThemeIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierThemes ");
            builder.Append(" where ThemeId in (" + ThemeIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ThemeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SupplierThemes");
            builder.Append(" where ThemeId=@ThemeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ThemeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ThemeId,Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark ");
            builder.Append(" FROM Shop_SupplierThemes ");
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
            builder.Append(" ThemeId,Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark ");
            builder.Append(" FROM Shop_SupplierThemes ");
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
                builder.Append("order by T.ThemeId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SupplierThemes T ");
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
            return DbHelperSQL.GetMaxID("ThemeId", "Shop_SupplierThemes");
        }

        public Maticsoft.Model.Shop.Supplier.SupplierThemes GetModel(int ThemeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ThemeId,Name,Description,ImageUrl,Author,WebSite,Language,CreatedDate,UpdatedDate,Remark from Shop_SupplierThemes ");
            builder.Append(" where ThemeId=@ThemeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ThemeId;
            new Maticsoft.Model.Shop.Supplier.SupplierThemes();
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
            builder.Append("select count(1) FROM Shop_SupplierThemes ");
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

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierThemes model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SupplierThemes set ");
            builder.Append("Name=@Name,");
            builder.Append("Description=@Description,");
            builder.Append("ImageUrl=@ImageUrl,");
            builder.Append("Author=@Author,");
            builder.Append("WebSite=@WebSite,");
            builder.Append("Language=@Language,");
            builder.Append("CreatedDate=@CreatedDate,");
            builder.Append("UpdatedDate=@UpdatedDate,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ThemeId=@ThemeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 50), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 100), new SqlParameter("@Author", SqlDbType.NVarChar, 100), new SqlParameter("@WebSite", SqlDbType.NVarChar, 200), new SqlParameter("@Language", SqlDbType.NVarChar, 50), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@UpdatedDate", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.NVarChar, 100), new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Description;
            cmdParms[2].Value = model.ImageUrl;
            cmdParms[3].Value = model.Author;
            cmdParms[4].Value = model.WebSite;
            cmdParms[5].Value = model.Language;
            cmdParms[6].Value = model.CreatedDate;
            cmdParms[7].Value = model.UpdatedDate;
            cmdParms[8].Value = model.Remark;
            cmdParms[9].Value = model.ThemeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


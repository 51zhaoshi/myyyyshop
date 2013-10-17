namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SupplierRankThemes : ISupplierRankThemes
    {
        public bool Add(Maticsoft.Model.Shop.Supplier.SupplierRankThemes model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SupplierRankThemes(");
            builder.Append("RankId,ThemeId)");
            builder.Append(" values (");
            builder.Append("@RankId,@ThemeId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4), new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RankId;
            cmdParms[1].Value = model.ThemeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierRankThemes DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierRankThemes themes = new Maticsoft.Model.Shop.Supplier.SupplierRankThemes();
            if (row != null)
            {
                if ((row["RankId"] != null) && (row["RankId"].ToString() != ""))
                {
                    themes.RankId = int.Parse(row["RankId"].ToString());
                }
                if ((row["ThemeId"] != null) && (row["ThemeId"].ToString() != ""))
                {
                    themes.ThemeId = int.Parse(row["ThemeId"].ToString());
                }
            }
            return themes;
        }

        public bool Delete(int RankId, int ThemeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierRankThemes ");
            builder.Append(" where RankId=@RankId and ThemeId=@ThemeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4), new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            cmdParms[1].Value = ThemeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int RankId, int ThemeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SupplierRankThemes");
            builder.Append(" where RankId=@RankId and ThemeId=@ThemeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4), new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            cmdParms[1].Value = ThemeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select RankId,ThemeId ");
            builder.Append(" FROM Shop_SupplierRankThemes ");
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
            builder.Append(" RankId,ThemeId ");
            builder.Append(" FROM Shop_SupplierRankThemes ");
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
            builder.Append(")AS Row, T.*  from Shop_SupplierRankThemes T ");
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
            return DbHelperSQL.GetMaxID("RankId", "Shop_SupplierRankThemes");
        }

        public Maticsoft.Model.Shop.Supplier.SupplierRankThemes GetModel(int RankId, int ThemeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 RankId,ThemeId from Shop_SupplierRankThemes ");
            builder.Append(" where RankId=@RankId and ThemeId=@ThemeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4), new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = RankId;
            cmdParms[1].Value = ThemeId;
            new Maticsoft.Model.Shop.Supplier.SupplierRankThemes();
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
            builder.Append("select count(1) FROM Shop_SupplierRankThemes ");
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

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierRankThemes model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SupplierRankThemes set ");
            builder.Append("RankId=@RankId,");
            builder.Append("ThemeId=@ThemeId");
            builder.Append(" where RankId=@RankId and ThemeId=@ThemeId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RankId", SqlDbType.Int, 4), new SqlParameter("@ThemeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RankId;
            cmdParms[1].Value = model.ThemeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


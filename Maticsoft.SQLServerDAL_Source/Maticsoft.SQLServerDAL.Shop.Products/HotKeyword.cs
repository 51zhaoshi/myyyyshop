namespace Maticsoft.SQLServerDAL.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class HotKeyword : IHotKeyword
    {
        public int Add(Maticsoft.Model.Shop.Products.HotKeyword model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_HotKeywords(");
            builder.Append("Keywords,CategoryId)");
            builder.Append(" VALUES (");
            builder.Append("@Keywords,@CategoryId)");
            builder.Append(";SELECT @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@CategoryId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Keywords;
            cmdParms[1].Value = model.CategoryId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Delete(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_HotKeywords ");
            builder.Append(" WHERE Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string Idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_HotKeywords ");
            builder.Append(" WHERE Id in (" + Idlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_HotKeywords");
            builder.Append(" WHERE Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Id,Keywords,CategoryId ");
            builder.Append(" FROM Shop_HotKeywords ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ");
            if (Top > 0)
            {
                builder.Append(" TOP " + Top.ToString());
            }
            builder.Append(" Id,Keywords,CategoryId ");
            builder.Append(" FROM Shop_HotKeywords ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ORDER BY " + filedOrder);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrWhiteSpace(orderby.Trim()))
            {
                builder.Append("ORDER BY T." + orderby);
            }
            else
            {
                builder.Append("ORDER BY T.Id desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_HotKeywords T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetListLeftjoinCategories(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT H.id,H.Keywords,C.Name,C.Depth FROM Shop_HotKeywords H LEFT JOIN Shop_Categories C on H.CategoryId=C.CategoryId ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "Shop_HotKeywords");
        }

        public Maticsoft.Model.Shop.Products.HotKeyword GetModel(int Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 Id,Keywords,CategoryId FROM Shop_HotKeywords ");
            builder.Append(" WHERE Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = Id;
            Maticsoft.Model.Shop.Products.HotKeyword keyword = new Maticsoft.Model.Shop.Products.HotKeyword();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["Id"] != null) && (set.Tables[0].Rows[0]["Id"].ToString() != ""))
            {
                keyword.Id = int.Parse(set.Tables[0].Rows[0]["Id"].ToString());
            }
            if ((set.Tables[0].Rows[0]["Keywords"] != null) && (set.Tables[0].Rows[0]["Keywords"].ToString() != ""))
            {
                keyword.Keywords = set.Tables[0].Rows[0]["Keywords"].ToString();
            }
            if ((set.Tables[0].Rows[0]["CategoryId"] != null) && (set.Tables[0].Rows[0]["CategoryId"].ToString() != ""))
            {
                keyword.CategoryId = new int?(int.Parse(set.Tables[0].Rows[0]["CategoryId"].ToString()));
            }
            return keyword;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_HotKeywords ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public bool Update(Maticsoft.Model.Shop.Products.HotKeyword model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_HotKeywords SET ");
            builder.Append("Keywords=@Keywords,");
            builder.Append("CategoryId=@CategoryId");
            builder.Append(" WHERE Id=@Id");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Keywords", SqlDbType.NVarChar, 50), new SqlParameter("@CategoryId", SqlDbType.Int, 4), new SqlParameter("@Id", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Keywords;
            cmdParms[1].Value = model.CategoryId;
            cmdParms[2].Value = model.Id;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


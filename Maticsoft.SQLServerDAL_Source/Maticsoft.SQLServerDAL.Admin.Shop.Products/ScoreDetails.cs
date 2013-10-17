namespace Maticsoft.SQLServerDAL.Admin.Shop.Products
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ScoreDetails : IScoreDetails
    {
        public bool Add(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO Shop_ScoreDetails(");
            builder.Append("ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate)");
            builder.Append(" VALUES (");
            builder.Append("@ScoreId,@ReviewId,@UserId,@ProductId,@Score,@CreatedDate)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ScoreId", SqlDbType.Int, 4), new SqlParameter("@ReviewId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@Score", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.ScoreId;
            cmdParms[1].Value = model.ReviewId;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.ProductId;
            cmdParms[4].Value = model.Score;
            cmdParms[5].Value = model.CreatedDate;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Delete(int ScoreId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ScoreDetails ");
            builder.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ScoreId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ScoreId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ScoreIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DELETE FROM Shop_ScoreDetails ");
            builder.Append(" WHERE ScoreId in (" + ScoreIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ScoreId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ScoreDetails");
            builder.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ScoreId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ScoreId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.ProductId,ScoreCount,Score,Score/SCoreCount AS AVE FROM( ");
            builder.Append("SELECT COUNT(*)AS ScoreCount,ProductId ");
            builder.Append("FROM Shop_ScoreDetails ");
            builder.Append("GROUP BY ProductId)A  ");
            builder.Append("LEFT JOIN (SELECT SUM(Score) AS Score,ProductId ");
            builder.Append("FROM Shop_ScoreDetails ");
            builder.Append("GROUP BY ProductId)B ON A.ProductId = B.ProductId ");
            return DbHelperSQL.Query(builder.ToString());
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate ");
            builder.Append(" FROM Shop_ScoreDetails ");
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
            builder.Append(" ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate ");
            builder.Append(" FROM Shop_ScoreDetails ");
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
                builder.Append("ORDER BY T.ScoreId desc");
            }
            builder.Append(")AS Row, T.*  FROM Shop_ScoreDetails T ");
            if (!string.IsNullOrWhiteSpace(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row BETWEEN {0} AND {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ScoreId", "Shop_ScoreDetails");
        }

        public Maticsoft.Model.Shop.Products.ScoreDetails GetModel(int ScoreId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT  TOP 1 ScoreId,ReviewId,UserId,ProductId,Score,CreatedDate FROM Shop_ScoreDetails ");
            builder.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ScoreId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ScoreId;
            Maticsoft.Model.Shop.Products.ScoreDetails details = new Maticsoft.Model.Shop.Products.ScoreDetails();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if ((set.Tables[0].Rows[0]["ScoreId"] != null) && (set.Tables[0].Rows[0]["ScoreId"].ToString() != ""))
            {
                details.ScoreId = int.Parse(set.Tables[0].Rows[0]["ScoreId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["ReviewId"] != null) && (set.Tables[0].Rows[0]["ReviewId"].ToString() != ""))
            {
                details.ReviewId = int.Parse(set.Tables[0].Rows[0]["ReviewId"].ToString());
            }
            if ((set.Tables[0].Rows[0]["UserId"] != null) && (set.Tables[0].Rows[0]["UserId"].ToString() != ""))
            {
                details.UserId = new int?(int.Parse(set.Tables[0].Rows[0]["UserId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["ProductId"] != null) && (set.Tables[0].Rows[0]["ProductId"].ToString() != ""))
            {
                details.ProductId = new long?(long.Parse(set.Tables[0].Rows[0]["ProductId"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["Score"] != null) && (set.Tables[0].Rows[0]["Score"].ToString() != ""))
            {
                details.Score = new int?(int.Parse(set.Tables[0].Rows[0]["Score"].ToString()));
            }
            if ((set.Tables[0].Rows[0]["CreatedDate"] != null) && (set.Tables[0].Rows[0]["CreatedDate"].ToString() != ""))
            {
                details.CreatedDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["CreatedDate"].ToString()));
            }
            return details;
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(1) FROM Shop_ScoreDetails ");
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

        public int GetScore(int? ReviewId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Score FROM Shop_ScoreDetails ");
            builder.AppendFormat("WHERE ReviewId={0} ", ReviewId.Value);
            object single = DbHelperSQL.GetSingle(builder.ToString());
            if (single != null)
            {
                return Convert.ToInt32(single);
            }
            return 0;
        }

        public DataSet GetScoreDetailInfo(long productId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.ProductId,Poor,Rating,HeightScore FROM ( ");
            builder.Append("SELECT (COUNT( Score)+0.0)/(SELECT COUNT(*) FROM  Shop_ScoreDetails ");
            builder.Append("WHERE ProductId=@ProductId)*100 AS Poor,ProductId FROM Shop_ScoreDetails ");
            builder.Append("WHERE Score<3  AND ProductId=@ProductId GROUP BY ProductId)A ");
            builder.Append("LEFT JOIN ( ");
            builder.Append("SELECT (COUNT( Score)+0.0)/(SELECT COUNT(*) FROM  Shop_ScoreDetails ");
            builder.Append("WHERE ProductId=@ProductId)*100 AS Rating,ProductId FROM Shop_ScoreDetails ");
            builder.Append("WHERE Score=3  AND ProductId=@ProductId  GROUP BY ProductId)B ON A.ProductId = B.ProductId ");
            builder.Append("LEFT JOIN ( ");
            builder.Append("SELECT (COUNT( Score)+0.0)/(SELECT COUNT(*) FROM  Shop_ScoreDetails ");
            builder.Append("WHERE ProductId=@ProductId)*100 AS HeightScore,ProductId FROM Shop_ScoreDetails ");
            builder.Append("WHERE Score>3  AND ProductId=@ProductId GROUP BY ProductId)C  ON A.ProductId = C.ProductId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.BigInt) };
            cmdParms[0].Value = productId;
            return DbHelperSQL.Query(builder.ToString(), cmdParms);
        }

        public bool Update(Maticsoft.Model.Shop.Products.ScoreDetails model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Shop_ScoreDetails SET ");
            builder.Append("ReviewId=@ReviewId,");
            builder.Append("UserId=@UserId,");
            builder.Append("ProductId=@ProductId,");
            builder.Append("Score=@Score,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" WHERE ScoreId=@ScoreId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ReviewId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ProductId", SqlDbType.BigInt, 8), new SqlParameter("@Score", SqlDbType.Int, 4), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@ScoreId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ReviewId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.ProductId;
            cmdParms[3].Value = model.Score;
            cmdParms[4].Value = model.CreatedDate;
            cmdParms[5].Value = model.ScoreId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


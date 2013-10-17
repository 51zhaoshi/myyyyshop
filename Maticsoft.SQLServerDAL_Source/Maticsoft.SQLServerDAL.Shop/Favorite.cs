namespace Maticsoft.SQLServerDAL.Shop
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop;
    using Maticsoft.Model.Shop;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Favorite : IFavorite
    {
        public int Add(Maticsoft.Model.Shop.Favorite model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Favorite(");
            builder.Append("Type,TargetId,UserId,Tags,Remark,CreatedDate)");
            builder.Append(" values (");
            builder.Append("@Type,@TargetId,@UserId,@Tags,@Remark,@CreatedDate)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@TargetId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@CreatedDate", SqlDbType.DateTime) };
            cmdParms[0].Value = model.Type;
            cmdParms[1].Value = model.TargetId;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.Tags;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreatedDate;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Favorite DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Favorite favorite = new Maticsoft.Model.Shop.Favorite();
            if (row != null)
            {
                if ((row["FavoriteId"] != null) && (row["FavoriteId"].ToString() != ""))
                {
                    favorite.FavoriteId = int.Parse(row["FavoriteId"].ToString());
                }
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    favorite.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["TargetId"] != null) && (row["TargetId"].ToString() != ""))
                {
                    favorite.TargetId = long.Parse(row["TargetId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    favorite.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["Tags"] != null)
                {
                    favorite.Tags = row["Tags"].ToString();
                }
                if (row["Remark"] != null)
                {
                    favorite.Remark = row["Remark"].ToString();
                }
                if ((row["CreatedDate"] != null) && (row["CreatedDate"].ToString() != ""))
                {
                    favorite.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
            }
            return favorite;
        }

        public bool Delete(int FavoriteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Favorite ");
            builder.Append(" where FavoriteId=@FavoriteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FavoriteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FavoriteId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string FavoriteIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Favorite ");
            builder.Append(" where FavoriteId in (" + FavoriteIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int FavoriteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Favorite");
            builder.Append(" where FavoriteId=@FavoriteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FavoriteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FavoriteId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public bool Exists(long targetId, int UserId, int Type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_Favorite");
            builder.Append(" where TargetId=@TargetId and UserId=@UserId and Type=@Type ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@TargetId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4) };
            cmdParms[0].Value = targetId;
            cmdParms[1].Value = UserId;
            cmdParms[2].Value = Type;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FavoriteId,Type,TargetId,UserId,Tags,Remark,CreatedDate ");
            builder.Append(" FROM Shop_Favorite ");
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
            builder.Append(" FavoriteId,Type,TargetId,UserId,Tags,Remark,CreatedDate ");
            builder.Append(" FROM Shop_Favorite ");
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
                builder.Append("order by T.FavoriteId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Favorite T ");
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
            return DbHelperSQL.GetMaxID("FavoriteId", "Shop_Favorite");
        }

        public Maticsoft.Model.Shop.Favorite GetModel(int FavoriteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FavoriteId,Type,TargetId,UserId,Tags,Remark,CreatedDate from Shop_Favorite ");
            builder.Append(" where FavoriteId=@FavoriteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@FavoriteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = FavoriteId;
            new Maticsoft.Model.Shop.Favorite();
            DataSet set = DbHelperSQL.Query(builder.ToString(), cmdParms);
            if (set.Tables[0].Rows.Count > 0)
            {
                return this.DataRowToModel(set.Tables[0].Rows[0]);
            }
            return null;
        }

        public DataSet GetProductListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM ( ");
            builder.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                builder.Append("order by favo." + orderby);
            }
            else
            {
                builder.Append("order by favo.FavoriteId desc");
            }
            builder.Append(")AS Row, ");
            builder.Append(" favo.FavoriteId AS FavoriteId, favo.CreatedDate AS CreatedDate ,prod.ProductId as ProductId , prod.ProductName AS ProductName,prod.SaleStatus AS SaleStatus , prod.ThumbnailUrl1 AS ThumbnailUrl1");
            builder.Append(" from Shop_Favorite AS favo LEFT JOIN  Shop_Products AS prod ON  favo.TargetId=prod.ProductId ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) tab");
            builder.AppendFormat(" WHERE tab.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) FROM Shop_Favorite ");
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

        public bool Update(Maticsoft.Model.Shop.Favorite model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Favorite set ");
            builder.Append("Type=@Type,");
            builder.Append("TargetId=@TargetId,");
            builder.Append("UserId=@UserId,");
            builder.Append("Tags=@Tags,");
            builder.Append("Remark=@Remark,");
            builder.Append("CreatedDate=@CreatedDate");
            builder.Append(" where FavoriteId=@FavoriteId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.SmallInt, 2), new SqlParameter("@TargetId", SqlDbType.BigInt, 8), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@Tags", SqlDbType.NVarChar, 100), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@CreatedDate", SqlDbType.DateTime), new SqlParameter("@FavoriteId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Type;
            cmdParms[1].Value = model.TargetId;
            cmdParms[2].Value = model.UserId;
            cmdParms[3].Value = model.Tags;
            cmdParms[4].Value = model.Remark;
            cmdParms[5].Value = model.CreatedDate;
            cmdParms[6].Value = model.FavoriteId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


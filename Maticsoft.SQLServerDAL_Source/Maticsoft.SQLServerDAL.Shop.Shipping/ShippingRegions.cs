namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ShippingRegions : IShippingRegions
    {
        public bool Add(Maticsoft.Model.Shop.Shipping.ShippingRegions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ShippingRegions(");
            builder.Append("ModeId,GroupId,RegionId)");
            builder.Append(" values (");
            builder.Append("@ModeId,@GroupId,@RegionId)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4), new SqlParameter("@GroupId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ModeId;
            cmdParms[1].Value = model.GroupId;
            cmdParms[2].Value = model.RegionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingRegions DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Shipping.ShippingRegions regions = new Maticsoft.Model.Shop.Shipping.ShippingRegions();
            if (row != null)
            {
                if ((row["ModeId"] != null) && (row["ModeId"].ToString() != ""))
                {
                    regions.ModeId = int.Parse(row["ModeId"].ToString());
                }
                if ((row["GroupId"] != null) && (row["GroupId"].ToString() != ""))
                {
                    regions.GroupId = int.Parse(row["GroupId"].ToString());
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    regions.RegionId = int.Parse(row["RegionId"].ToString());
                }
            }
            return regions;
        }

        public bool Delete(int ModeId, int RegionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingRegions ");
            builder.Append(" where ModeId=@ModeId and RegionId=@RegionId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ModeId;
            cmdParms[1].Value = RegionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool Exists(int ModeId, int RegionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ShippingRegions");
            builder.Append(" where ModeId=@ModeId and RegionId=@RegionId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ModeId;
            cmdParms[1].Value = RegionId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ModeId,GroupId,RegionId ");
            builder.Append(" FROM Shop_ShippingRegions ");
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
            builder.Append(" ModeId,GroupId,RegionId ");
            builder.Append(" FROM Shop_ShippingRegions ");
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
                builder.Append("order by T.RegionId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ShippingRegions T ");
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
            return DbHelperSQL.GetMaxID("ModeId", "Shop_ShippingRegions");
        }

        public Maticsoft.Model.Shop.Shipping.ShippingRegions GetModel(int ModeId, int RegionId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ModeId,GroupId,RegionId from Shop_ShippingRegions ");
            builder.Append(" where ModeId=@ModeId and RegionId=@RegionId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ModeId;
            cmdParms[1].Value = RegionId;
            new Maticsoft.Model.Shop.Shipping.ShippingRegions();
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
            builder.Append("select count(1) FROM Shop_ShippingRegions ");
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

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingRegions model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ShippingRegions set ");
            builder.Append("GroupId=@GroupId");
            builder.Append(" where ModeId=@ModeId and RegionId=@RegionId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupId", SqlDbType.Int, 4), new SqlParameter("@ModeId", SqlDbType.Int, 4), new SqlParameter("@RegionId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.GroupId;
            cmdParms[1].Value = model.ModeId;
            cmdParms[2].Value = model.RegionId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ShippingGroup : IShippingGroup
    {
        public int Add(Maticsoft.Model.Shop.Shipping.ShippingGroup model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ShippingGroup(");
            builder.Append("ModeId,Price,AddPrice)");
            builder.Append(" values (");
            builder.Append("@ModeId,@Price,@AddPrice)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@AddPrice", SqlDbType.Money, 8) };
            cmdParms[0].Value = model.ModeId;
            cmdParms[1].Value = model.Price;
            cmdParms[2].Value = model.AddPrice;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingGroup DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Shipping.ShippingGroup group = new Maticsoft.Model.Shop.Shipping.ShippingGroup();
            if (row != null)
            {
                if ((row["GroupId"] != null) && (row["GroupId"].ToString() != ""))
                {
                    group.GroupId = int.Parse(row["GroupId"].ToString());
                }
                if ((row["ModeId"] != null) && (row["ModeId"].ToString() != ""))
                {
                    group.ModeId = int.Parse(row["ModeId"].ToString());
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    group.Price = decimal.Parse(row["Price"].ToString());
                }
                if ((row["AddPrice"] != null) && (row["AddPrice"].ToString() != ""))
                {
                    group.AddPrice = new decimal?(decimal.Parse(row["AddPrice"].ToString()));
                }
            }
            return group;
        }

        public bool Delete(int GroupId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingGroup ");
            builder.Append(" where GroupId=@GroupId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupId", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string GroupIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingGroup ");
            builder.Append(" where GroupId in (" + GroupIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int GroupId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ShippingGroup");
            builder.Append(" where GroupId=@GroupId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupId", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select GroupId,ModeId,Price,AddPrice ");
            builder.Append(" FROM Shop_ShippingGroup ");
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
            builder.Append(" GroupId,ModeId,Price,AddPrice ");
            builder.Append(" FROM Shop_ShippingGroup ");
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
                builder.Append("order by T.GroupId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ShippingGroup T ");
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
            return DbHelperSQL.GetMaxID("GroupId", "Shop_ShippingGroup");
        }

        public Maticsoft.Model.Shop.Shipping.ShippingGroup GetModel(int GroupId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 GroupId,ModeId,Price,AddPrice from Shop_ShippingGroup ");
            builder.Append(" where GroupId=@GroupId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@GroupId", SqlDbType.Int, 4) };
            cmdParms[0].Value = GroupId;
            new Maticsoft.Model.Shop.Shipping.ShippingGroup();
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
            builder.Append("select count(1) FROM Shop_ShippingGroup ");
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

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingGroup model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ShippingGroup set ");
            builder.Append("ModeId=@ModeId,");
            builder.Append("Price=@Price,");
            builder.Append("AddPrice=@AddPrice");
            builder.Append(" where GroupId=@GroupId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@AddPrice", SqlDbType.Money, 8), new SqlParameter("@GroupId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.ModeId;
            cmdParms[1].Value = model.Price;
            cmdParms[2].Value = model.AddPrice;
            cmdParms[3].Value = model.GroupId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


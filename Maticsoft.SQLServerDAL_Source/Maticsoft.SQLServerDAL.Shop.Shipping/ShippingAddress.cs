namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ShippingAddress : IShippingAddress
    {
        public int Add(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ShippingAddress(");
            builder.Append("RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone)");
            builder.Append(" values (");
            builder.Append("@RegionId,@UserId,@ShipName,@Address,@Zipcode,@EmailAddress,@TelPhone,@CelPhone)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@Address", SqlDbType.NVarChar, 300), new SqlParameter("@Zipcode", SqlDbType.NVarChar, 20), new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CelPhone", SqlDbType.NVarChar, 50) };
            cmdParms[0].Value = model.RegionId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.ShipName;
            cmdParms[3].Value = model.Address;
            cmdParms[4].Value = model.Zipcode;
            cmdParms[5].Value = model.EmailAddress;
            cmdParms[6].Value = model.TelPhone;
            cmdParms[7].Value = model.CelPhone;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingAddress DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Shipping.ShippingAddress address = new Maticsoft.Model.Shop.Shipping.ShippingAddress();
            if (row != null)
            {
                if ((row["ShippingId"] != null) && (row["ShippingId"].ToString() != ""))
                {
                    address.ShippingId = int.Parse(row["ShippingId"].ToString());
                }
                if ((row["RegionId"] != null) && (row["RegionId"].ToString() != ""))
                {
                    address.RegionId = int.Parse(row["RegionId"].ToString());
                }
                if ((row["UserId"] != null) && (row["UserId"].ToString() != ""))
                {
                    address.UserId = int.Parse(row["UserId"].ToString());
                }
                if (row["ShipName"] != null)
                {
                    address.ShipName = row["ShipName"].ToString();
                }
                if (row["Address"] != null)
                {
                    address.Address = row["Address"].ToString();
                }
                if (row["Zipcode"] != null)
                {
                    address.Zipcode = row["Zipcode"].ToString();
                }
                if (row["EmailAddress"] != null)
                {
                    address.EmailAddress = row["EmailAddress"].ToString();
                }
                if (row["TelPhone"] != null)
                {
                    address.TelPhone = row["TelPhone"].ToString();
                }
                if (row["CelPhone"] != null)
                {
                    address.CelPhone = row["CelPhone"].ToString();
                }
            }
            return address;
        }

        public bool Delete(int ShippingId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingAddress ");
            builder.Append(" where ShippingId=@ShippingId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ShippingId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ShippingIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingAddress ");
            builder.Append(" where ShippingId in (" + ShippingIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ShippingId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ShippingAddress");
            builder.Append(" where ShippingId=@ShippingId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ShippingId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ShippingId,RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone ");
            builder.Append(" FROM Shop_ShippingAddress ");
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
            builder.Append(" ShippingId,RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone ");
            builder.Append(" FROM Shop_ShippingAddress ");
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
                builder.Append("order by T.ShippingId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ShippingAddress T ");
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
            return DbHelperSQL.GetMaxID("ShippingId", "Shop_ShippingAddress");
        }

        public Maticsoft.Model.Shop.Shipping.ShippingAddress GetModel(int ShippingId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ShippingId,RegionId,UserId,ShipName,Address,Zipcode,EmailAddress,TelPhone,CelPhone from Shop_ShippingAddress ");
            builder.Append(" where ShippingId=@ShippingId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ShippingId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ShippingId;
            new Maticsoft.Model.Shop.Shipping.ShippingAddress();
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
            builder.Append("select count(1) FROM Shop_ShippingAddress ");
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

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingAddress model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ShippingAddress set ");
            builder.Append("RegionId=@RegionId,");
            builder.Append("UserId=@UserId,");
            builder.Append("ShipName=@ShipName,");
            builder.Append("Address=@Address,");
            builder.Append("Zipcode=@Zipcode,");
            builder.Append("EmailAddress=@EmailAddress,");
            builder.Append("TelPhone=@TelPhone,");
            builder.Append("CelPhone=@CelPhone");
            builder.Append(" where ShippingId=@ShippingId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RegionId", SqlDbType.Int, 4), new SqlParameter("@UserId", SqlDbType.Int, 4), new SqlParameter("@ShipName", SqlDbType.NVarChar, 50), new SqlParameter("@Address", SqlDbType.NVarChar, 300), new SqlParameter("@Zipcode", SqlDbType.NVarChar, 20), new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 100), new SqlParameter("@TelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@CelPhone", SqlDbType.NVarChar, 50), new SqlParameter("@ShippingId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RegionId;
            cmdParms[1].Value = model.UserId;
            cmdParms[2].Value = model.ShipName;
            cmdParms[3].Value = model.Address;
            cmdParms[4].Value = model.Zipcode;
            cmdParms[5].Value = model.EmailAddress;
            cmdParms[6].Value = model.TelPhone;
            cmdParms[7].Value = model.CelPhone;
            cmdParms[8].Value = model.ShippingId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Shipping;
    using Maticsoft.Model.Shop.Shipping;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ShippingType : IShippingType
    {
        public int Add(Maticsoft.Model.Shop.Shipping.ShippingType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_ShippingType(");
            builder.Append("Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn)");
            builder.Append(" values (");
            builder.Append("@Name,@Weight,@AddWeight,@Price,@AddPrice,@Description,@DisplaySequence,@ExpressCompanyName,@ExpressCompanyEn)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@AddWeight", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@AddPrice", SqlDbType.Money, 8), new SqlParameter("@Description", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), new SqlParameter("@ExpressCompanyEn", SqlDbType.NVarChar, 500) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Weight;
            cmdParms[2].Value = model.AddWeight;
            cmdParms[3].Value = model.Price;
            cmdParms[4].Value = model.AddPrice;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.DisplaySequence;
            cmdParms[7].Value = model.ExpressCompanyName;
            cmdParms[8].Value = model.ExpressCompanyEn;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Shipping.ShippingType DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Shipping.ShippingType type = new Maticsoft.Model.Shop.Shipping.ShippingType();
            if (row != null)
            {
                if ((row["ModeId"] != null) && (row["ModeId"].ToString() != ""))
                {
                    type.ModeId = int.Parse(row["ModeId"].ToString());
                }
                if (row["Name"] != null)
                {
                    type.Name = row["Name"].ToString();
                }
                if ((row["Weight"] != null) && (row["Weight"].ToString() != ""))
                {
                    type.Weight = int.Parse(row["Weight"].ToString());
                }
                if ((row["AddWeight"] != null) && (row["AddWeight"].ToString() != ""))
                {
                    type.AddWeight = new int?(int.Parse(row["AddWeight"].ToString()));
                }
                if ((row["Price"] != null) && (row["Price"].ToString() != ""))
                {
                    type.Price = decimal.Parse(row["Price"].ToString());
                }
                if ((row["AddPrice"] != null) && (row["AddPrice"].ToString() != ""))
                {
                    type.AddPrice = new decimal?(decimal.Parse(row["AddPrice"].ToString()));
                }
                if (row["Description"] != null)
                {
                    type.Description = row["Description"].ToString();
                }
                if ((row["DisplaySequence"] != null) && (row["DisplaySequence"].ToString() != ""))
                {
                    type.DisplaySequence = int.Parse(row["DisplaySequence"].ToString());
                }
                if (row["ExpressCompanyName"] != null)
                {
                    type.ExpressCompanyName = row["ExpressCompanyName"].ToString();
                }
                if (row["ExpressCompanyEn"] != null)
                {
                    type.ExpressCompanyEn = row["ExpressCompanyEn"].ToString();
                }
            }
            return type;
        }

        public bool Delete(int ModeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingType ");
            builder.Append(" where ModeId=@ModeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ModeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ModeIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_ShippingType ");
            builder.Append(" where ModeId in (" + ModeIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ModeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_ShippingType");
            builder.Append(" where ModeId=@ModeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ModeId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ModeId,Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn ");
            builder.Append(" FROM Shop_ShippingType ");
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
            builder.Append(" ModeId,Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn ");
            builder.Append(" FROM Shop_ShippingType ");
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
                builder.Append("order by T.ModeId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_ShippingType T ");
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
            return DbHelperSQL.GetMaxID("ModeId", "Shop_ShippingType");
        }

        public Maticsoft.Model.Shop.Shipping.ShippingType GetModel(int ModeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ModeId,Name,Weight,AddWeight,Price,AddPrice,Description,DisplaySequence,ExpressCompanyName,ExpressCompanyEn from Shop_ShippingType ");
            builder.Append(" where ModeId=@ModeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ModeId;
            new Maticsoft.Model.Shop.Shipping.ShippingType();
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
            builder.Append("select count(1) FROM Shop_ShippingType ");
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

        public bool Update(Maticsoft.Model.Shop.Shipping.ShippingType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_ShippingType set ");
            builder.Append("Name=@Name,");
            builder.Append("Weight=@Weight,");
            builder.Append("AddWeight=@AddWeight,");
            builder.Append("Price=@Price,");
            builder.Append("AddPrice=@AddPrice,");
            builder.Append("Description=@Description,");
            builder.Append("DisplaySequence=@DisplaySequence,");
            builder.Append("ExpressCompanyName=@ExpressCompanyName,");
            builder.Append("ExpressCompanyEn=@ExpressCompanyEn");
            builder.Append(" where ModeId=@ModeId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@Weight", SqlDbType.Int, 4), new SqlParameter("@AddWeight", SqlDbType.Int, 4), new SqlParameter("@Price", SqlDbType.Money, 8), new SqlParameter("@AddPrice", SqlDbType.Money, 8), new SqlParameter("@Description", SqlDbType.NVarChar, 0xfa0), new SqlParameter("@DisplaySequence", SqlDbType.Int, 4), new SqlParameter("@ExpressCompanyName", SqlDbType.NVarChar, 500), new SqlParameter("@ExpressCompanyEn", SqlDbType.NVarChar, 500), new SqlParameter("@ModeId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.Weight;
            cmdParms[2].Value = model.AddWeight;
            cmdParms[3].Value = model.Price;
            cmdParms[4].Value = model.AddPrice;
            cmdParms[5].Value = model.Description;
            cmdParms[6].Value = model.DisplaySequence;
            cmdParms[7].Value = model.ExpressCompanyName;
            cmdParms[8].Value = model.ExpressCompanyEn;
            cmdParms[9].Value = model.ModeId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


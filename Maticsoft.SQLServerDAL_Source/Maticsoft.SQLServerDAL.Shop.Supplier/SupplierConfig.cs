namespace Maticsoft.SQLServerDAL.Shop.Supplier
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Supplier;
    using Maticsoft.Model.Shop.Supplier;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SupplierConfig : ISupplierConfig
    {
        public int Add(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SupplierConfig(");
            builder.Append("KeyName,Value,KeyType,Description,SupplierId)");
            builder.Append(" values (");
            builder.Append("@KeyName,@Value,@KeyType,@Description,@SupplierId)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyName", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@KeyType", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@SupplierId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.KeyName;
            cmdParms[1].Value = model.Value;
            cmdParms[2].Value = model.KeyType;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.SupplierId;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Supplier.SupplierConfig DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Supplier.SupplierConfig config = new Maticsoft.Model.Shop.Supplier.SupplierConfig();
            if (row != null)
            {
                if ((row["ID"] != null) && (row["ID"].ToString() != ""))
                {
                    config.ID = int.Parse(row["ID"].ToString());
                }
                if (row["KeyName"] != null)
                {
                    config.KeyName = row["KeyName"].ToString();
                }
                if (row["Value"] != null)
                {
                    config.Value = row["Value"].ToString();
                }
                if ((row["KeyType"] != null) && (row["KeyType"].ToString() != ""))
                {
                    config.KeyType = new int?(int.Parse(row["KeyType"].ToString()));
                }
                if (row["Description"] != null)
                {
                    config.Description = row["Description"].ToString();
                }
                if ((row["SupplierId"] != null) && (row["SupplierId"].ToString() != ""))
                {
                    config.SupplierId = int.Parse(row["SupplierId"].ToString());
                }
            }
            return config;
        }

        public bool Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierConfig ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string IDlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SupplierConfig ");
            builder.Append(" where ID in (" + IDlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SupplierConfig");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,KeyName,Value,KeyType,Description,SupplierId ");
            builder.Append(" FROM Shop_SupplierConfig ");
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
            builder.Append(" ID,KeyName,Value,KeyType,Description,SupplierId ");
            builder.Append(" FROM Shop_SupplierConfig ");
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
            builder.Append(")AS Row, T.*  from Shop_SupplierConfig T ");
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
            return DbHelperSQL.GetMaxID("ID", "Shop_SupplierConfig");
        }

        public Maticsoft.Model.Shop.Supplier.SupplierConfig GetModel(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ID,KeyName,Value,KeyType,Description,SupplierId from Shop_SupplierConfig ");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = ID;
            new Maticsoft.Model.Shop.Supplier.SupplierConfig();
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
            builder.Append("select count(1) FROM Shop_SupplierConfig ");
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

        public bool Update(Maticsoft.Model.Shop.Supplier.SupplierConfig model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SupplierConfig set ");
            builder.Append("KeyName=@KeyName,");
            builder.Append("Value=@Value,");
            builder.Append("KeyType=@KeyType,");
            builder.Append("Description=@Description,");
            builder.Append("SupplierId=@SupplierId");
            builder.Append(" where ID=@ID");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@KeyName", SqlDbType.NVarChar, 50), new SqlParameter("@Value", SqlDbType.NVarChar, -1), new SqlParameter("@KeyType", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 200), new SqlParameter("@SupplierId", SqlDbType.Int, 4), new SqlParameter("@ID", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.KeyName;
            cmdParms[1].Value = model.Value;
            cmdParms[2].Value = model.KeyType;
            cmdParms[3].Value = model.Description;
            cmdParms[4].Value = model.SupplierId;
            cmdParms[5].Value = model.ID;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


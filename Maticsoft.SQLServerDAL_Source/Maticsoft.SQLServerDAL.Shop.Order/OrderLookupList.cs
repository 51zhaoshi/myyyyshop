namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrderLookupList : IOrderLookupList
    {
        public int Add(Maticsoft.Model.Shop.Order.OrderLookupList model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrderLookupList(");
            builder.Append("Name,SelectMode,Description)");
            builder.Append(" values (");
            builder.Append("@Name,@SelectMode,@Description)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@SelectMode", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 0x3e8) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.SelectMode;
            cmdParms[2].Value = model.Description;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Order.OrderLookupList DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderLookupList list = new Maticsoft.Model.Shop.Order.OrderLookupList();
            if (row != null)
            {
                if ((row["LookupListId"] != null) && (row["LookupListId"].ToString() != ""))
                {
                    list.LookupListId = int.Parse(row["LookupListId"].ToString());
                }
                if (row["Name"] != null)
                {
                    list.Name = row["Name"].ToString();
                }
                if ((row["SelectMode"] != null) && (row["SelectMode"].ToString() != ""))
                {
                    list.SelectMode = int.Parse(row["SelectMode"].ToString());
                }
                if (row["Description"] != null)
                {
                    list.Description = row["Description"].ToString();
                }
            }
            return list;
        }

        public bool Delete(int LookupListId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderLookupList ");
            builder.Append(" where LookupListId=@LookupListId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LookupListId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string LookupListIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderLookupList ");
            builder.Append(" where LookupListId in (" + LookupListIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int LookupListId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_OrderLookupList");
            builder.Append(" where LookupListId=@LookupListId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LookupListId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select LookupListId,Name,SelectMode,Description ");
            builder.Append(" FROM Shop_OrderLookupList ");
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
            builder.Append(" LookupListId,Name,SelectMode,Description ");
            builder.Append(" FROM Shop_OrderLookupList ");
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
                builder.Append("order by T.LookupListId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_OrderLookupList T ");
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
            return DbHelperSQL.GetMaxID("LookupListId", "Shop_OrderLookupList");
        }

        public Maticsoft.Model.Shop.Order.OrderLookupList GetModel(int LookupListId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 LookupListId,Name,SelectMode,Description from Shop_OrderLookupList ");
            builder.Append(" where LookupListId=@LookupListId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LookupListId;
            new Maticsoft.Model.Shop.Order.OrderLookupList();
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
            builder.Append("select count(1) FROM Shop_OrderLookupList ");
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

        public bool Update(Maticsoft.Model.Shop.Order.OrderLookupList model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_OrderLookupList set ");
            builder.Append("Name=@Name,");
            builder.Append("SelectMode=@SelectMode,");
            builder.Append("Description=@Description");
            builder.Append(" where LookupListId=@LookupListId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@SelectMode", SqlDbType.Int, 4), new SqlParameter("@Description", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@LookupListId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.Name;
            cmdParms[1].Value = model.SelectMode;
            cmdParms[2].Value = model.Description;
            cmdParms[3].Value = model.LookupListId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


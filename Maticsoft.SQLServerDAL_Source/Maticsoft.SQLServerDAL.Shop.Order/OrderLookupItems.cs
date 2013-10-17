namespace Maticsoft.SQLServerDAL.Shop.Order
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Order;
    using Maticsoft.Model.Shop.Order;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class OrderLookupItems : IOrderLookupItems
    {
        public int Add(Maticsoft.Model.Shop.Order.OrderLookupItems model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_OrderLookupItems(");
            builder.Append("LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark)");
            builder.Append(" values (");
            builder.Append("@LookupListId,@Name,@IsInputRequired,@InputTitle,@AppendMoney,@CalculateMode,@Remark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@IsInputRequired", SqlDbType.Bit, 1), new SqlParameter("@InputTitle", SqlDbType.NVarChar, 20), new SqlParameter("@AppendMoney", SqlDbType.Money, 8), new SqlParameter("@CalculateMode", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300) };
            cmdParms[0].Value = model.LookupListId;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.IsInputRequired;
            cmdParms[3].Value = model.InputTitle;
            cmdParms[4].Value = model.AppendMoney;
            cmdParms[5].Value = model.CalculateMode;
            cmdParms[6].Value = model.Remark;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Order.OrderLookupItems DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Order.OrderLookupItems items = new Maticsoft.Model.Shop.Order.OrderLookupItems();
            if (row != null)
            {
                if ((row["LookupItemId"] != null) && (row["LookupItemId"].ToString() != ""))
                {
                    items.LookupItemId = int.Parse(row["LookupItemId"].ToString());
                }
                if ((row["LookupListId"] != null) && (row["LookupListId"].ToString() != ""))
                {
                    items.LookupListId = int.Parse(row["LookupListId"].ToString());
                }
                if (row["Name"] != null)
                {
                    items.Name = row["Name"].ToString();
                }
                if ((row["IsInputRequired"] != null) && (row["IsInputRequired"].ToString() != ""))
                {
                    if ((row["IsInputRequired"].ToString() == "1") || (row["IsInputRequired"].ToString().ToLower() == "true"))
                    {
                        items.IsInputRequired = true;
                    }
                    else
                    {
                        items.IsInputRequired = false;
                    }
                }
                if (row["InputTitle"] != null)
                {
                    items.InputTitle = row["InputTitle"].ToString();
                }
                if ((row["AppendMoney"] != null) && (row["AppendMoney"].ToString() != ""))
                {
                    items.AppendMoney = new decimal?(decimal.Parse(row["AppendMoney"].ToString()));
                }
                if ((row["CalculateMode"] != null) && (row["CalculateMode"].ToString() != ""))
                {
                    items.CalculateMode = new int?(int.Parse(row["CalculateMode"].ToString()));
                }
                if (row["Remark"] != null)
                {
                    items.Remark = row["Remark"].ToString();
                }
            }
            return items;
        }

        public bool Delete(int LookupItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderLookupItems ");
            builder.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LookupItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string LookupItemIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_OrderLookupItems ");
            builder.Append(" where LookupItemId in (" + LookupItemIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int LookupItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_OrderLookupItems");
            builder.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LookupItemId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select LookupItemId,LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark ");
            builder.Append(" FROM Shop_OrderLookupItems ");
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
            builder.Append(" LookupItemId,LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark ");
            builder.Append(" FROM Shop_OrderLookupItems ");
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
                builder.Append("order by T.LookupItemId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_OrderLookupItems T ");
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
            return DbHelperSQL.GetMaxID("LookupItemId", "Shop_OrderLookupItems");
        }

        public Maticsoft.Model.Shop.Order.OrderLookupItems GetModel(int LookupItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 LookupItemId,LookupListId,Name,IsInputRequired,InputTitle,AppendMoney,CalculateMode,Remark from Shop_OrderLookupItems ");
            builder.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = LookupItemId;
            new Maticsoft.Model.Shop.Order.OrderLookupItems();
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
            builder.Append("select count(1) FROM Shop_OrderLookupItems ");
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

        public bool Update(Maticsoft.Model.Shop.Order.OrderLookupItems model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_OrderLookupItems set ");
            builder.Append("LookupListId=@LookupListId,");
            builder.Append("Name=@Name,");
            builder.Append("IsInputRequired=@IsInputRequired,");
            builder.Append("InputTitle=@InputTitle,");
            builder.Append("AppendMoney=@AppendMoney,");
            builder.Append("CalculateMode=@CalculateMode,");
            builder.Append("Remark=@Remark");
            builder.Append(" where LookupItemId=@LookupItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@LookupListId", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NVarChar, 100), new SqlParameter("@IsInputRequired", SqlDbType.Bit, 1), new SqlParameter("@InputTitle", SqlDbType.NVarChar, 20), new SqlParameter("@AppendMoney", SqlDbType.Money, 8), new SqlParameter("@CalculateMode", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@LookupItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.LookupListId;
            cmdParms[1].Value = model.Name;
            cmdParms[2].Value = model.IsInputRequired;
            cmdParms[3].Value = model.InputTitle;
            cmdParms[4].Value = model.AppendMoney;
            cmdParms[5].Value = model.CalculateMode;
            cmdParms[6].Value = model.Remark;
            cmdParms[7].Value = model.LookupItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


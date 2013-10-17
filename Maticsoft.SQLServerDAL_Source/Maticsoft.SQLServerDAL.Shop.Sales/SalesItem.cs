namespace Maticsoft.SQLServerDAL.Shop.Sales
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop.Sales;
    using Maticsoft.Model.Shop.Sales;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SalesItem : ISalesItem
    {
        public int Add(Maticsoft.Model.Shop.Sales.SalesItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_SalesItem(");
            builder.Append("RuleId,ItemType,UnitValue,RateValue)");
            builder.Append(" values (");
            builder.Append("@RuleId,@ItemType,@UnitValue,@RateValue)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@ItemType", SqlDbType.Int, 4), new SqlParameter("@UnitValue", SqlDbType.Int, 4), new SqlParameter("@RateValue", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RuleId;
            cmdParms[1].Value = model.ItemType;
            cmdParms[2].Value = model.UnitValue;
            cmdParms[3].Value = model.RateValue;
            object single = DbHelperSQL.GetSingle(builder.ToString(), cmdParms);
            if (single == null)
            {
                return 0;
            }
            return Convert.ToInt32(single);
        }

        public Maticsoft.Model.Shop.Sales.SalesItem DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Sales.SalesItem item = new Maticsoft.Model.Shop.Sales.SalesItem();
            if (row != null)
            {
                if ((row["ItemId"] != null) && (row["ItemId"].ToString() != ""))
                {
                    item.ItemId = int.Parse(row["ItemId"].ToString());
                }
                if ((row["RuleId"] != null) && (row["RuleId"].ToString() != ""))
                {
                    item.RuleId = int.Parse(row["RuleId"].ToString());
                }
                if ((row["ItemType"] != null) && (row["ItemType"].ToString() != ""))
                {
                    item.ItemType = int.Parse(row["ItemType"].ToString());
                }
                if ((row["UnitValue"] != null) && (row["UnitValue"].ToString() != ""))
                {
                    item.UnitValue = int.Parse(row["UnitValue"].ToString());
                }
                if ((row["RateValue"] != null) && (row["RateValue"].ToString() != ""))
                {
                    item.RateValue = int.Parse(row["RateValue"].ToString());
                }
            }
            return item;
        }

        public bool Delete(int ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesItem ");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteByRuleId(int ruleId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesItem ");
            builder.Append(" where RuleId=@RuleId ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ruleId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public bool DeleteList(string ItemIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_SalesItem ");
            builder.Append(" where ItemId in (" + ItemIdlist + ")  ");
            return (DbHelperSQL.ExecuteSql(builder.ToString()) > 0);
        }

        public bool Exists(int ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Shop_SalesItem");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ItemId;
            return DbHelperSQL.Exists(builder.ToString(), cmdParms);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ItemId,RuleId,ItemType,UnitValue,RateValue ");
            builder.Append(" FROM Shop_SalesItem ");
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
            builder.Append(" ItemId,RuleId,ItemType,UnitValue,RateValue ");
            builder.Append(" FROM Shop_SalesItem ");
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
                builder.Append("order by T.ItemId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_SalesItem T ");
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
            return DbHelperSQL.GetMaxID("ItemId", "Shop_SalesItem");
        }

        public Maticsoft.Model.Shop.Sales.SalesItem GetModel(int ItemId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ItemId,RuleId,ItemType,UnitValue,RateValue from Shop_SalesItem ");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@ItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = ItemId;
            new Maticsoft.Model.Shop.Sales.SalesItem();
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
            builder.Append("select count(1) FROM Shop_SalesItem ");
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

        public bool Update(Maticsoft.Model.Shop.Sales.SalesItem model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_SalesItem set ");
            builder.Append("RuleId=@RuleId,");
            builder.Append("ItemType=@ItemType,");
            builder.Append("UnitValue=@UnitValue,");
            builder.Append("RateValue=@RateValue");
            builder.Append(" where ItemId=@ItemId");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@RuleId", SqlDbType.Int, 4), new SqlParameter("@ItemType", SqlDbType.Int, 4), new SqlParameter("@UnitValue", SqlDbType.Int, 4), new SqlParameter("@RateValue", SqlDbType.Int, 4), new SqlParameter("@ItemId", SqlDbType.Int, 4) };
            cmdParms[0].Value = model.RuleId;
            cmdParms[1].Value = model.ItemType;
            cmdParms[2].Value = model.UnitValue;
            cmdParms[3].Value = model.RateValue;
            cmdParms[4].Value = model.ItemId;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}


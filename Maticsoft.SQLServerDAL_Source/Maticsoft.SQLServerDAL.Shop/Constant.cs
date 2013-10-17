namespace Maticsoft.SQLServerDAL.Shop
{
    using Maticsoft.DBUtility;
    using Maticsoft.IDAL.Shop;
    using Maticsoft.Model.Shop;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Constant : IConstant
    {
        public bool Add(Maticsoft.Model.Shop.Constant model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Shop_Constant(");
            builder.Append("Type,DataDate,MaxValue,Remark)");
            builder.Append(" values (");
            builder.Append("@Type,@DataDate,@MaxValue,@Remark)");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@DataDate", SqlDbType.DateTime), new SqlParameter("@MaxValue", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300) };
            cmdParms[0].Value = model.Type;
            cmdParms[1].Value = model.DataDate;
            cmdParms[2].Value = model.MaxValue;
            cmdParms[3].Value = model.Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public Maticsoft.Model.Shop.Constant DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.Constant constant = new Maticsoft.Model.Shop.Constant();
            if (row != null)
            {
                if ((row["Type"] != null) && (row["Type"].ToString() != ""))
                {
                    constant.Type = int.Parse(row["Type"].ToString());
                }
                if ((row["DataDate"] != null) && (row["DataDate"].ToString() != ""))
                {
                    constant.DataDate = DateTime.Parse(row["DataDate"].ToString());
                }
                if ((row["MaxValue"] != null) && (row["MaxValue"].ToString() != ""))
                {
                    constant.MaxValue = int.Parse(row["MaxValue"].ToString());
                }
                if (row["Remark"] != null)
                {
                    constant.Remark = row["Remark"].ToString();
                }
            }
            return constant;
        }

        public bool Delete()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Shop_Constant ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Type,DataDate,MaxValue,Remark ");
            builder.Append(" FROM Shop_Constant ");
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
            builder.Append(" Type,DataDate,MaxValue,Remark ");
            builder.Append(" FROM Shop_Constant ");
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
                builder.Append("order by T.OrderId desc");
            }
            builder.Append(")AS Row, T.*  from Shop_Constant T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                builder.Append(" WHERE " + strWhere);
            }
            builder.Append(" ) TT");
            builder.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(builder.ToString());
        }

        public Maticsoft.Model.Shop.Constant GetModel()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Type,DataDate,MaxValue,Remark from Shop_Constant ");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[0];
            new Maticsoft.Model.Shop.Constant();
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
            builder.Append("select count(1) FROM Shop_Constant ");
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

        public bool Update(Maticsoft.Model.Shop.Constant model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Shop_Constant set ");
            builder.Append("Type=@Type,");
            builder.Append("DataDate=@DataDate,");
            builder.Append("MaxValue=@MaxValue,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ");
            SqlParameter[] cmdParms = new SqlParameter[] { new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@DataDate", SqlDbType.DateTime), new SqlParameter("@MaxValue", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 300) };
            cmdParms[0].Value = model.Type;
            cmdParms[1].Value = model.DataDate;
            cmdParms[2].Value = model.MaxValue;
            cmdParms[3].Value = model.Remark;
            return (DbHelperSQL.ExecuteSql(builder.ToString(), cmdParms) > 0);
        }
    }
}

